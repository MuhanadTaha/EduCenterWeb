using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace EduCenterWeb.Account
{
    public partial class ChangePassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // لو ما كان في تسجيل دخول، ممكن ترجع لصفحة الدخول
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (newPassword != confirmPassword)
            {
                lblMessage.Text = "كلمة المرور الجديدة وتأكيدها لا يتطابقان.";
                lblMessage.CssClass = "alert alert-danger";
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString))
            {
                // استعلام للتحقق من كلمة المرور الحالية
                string checkQuery = @"
            SELECT PasswordHash 
            FROM Users 
            WHERE UserID = @UserID 
            AND PasswordHash = HASHBYTES('SHA2_256', @CurrentPassword)";

                SqlCommand cmd = new SqlCommand(checkQuery, con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@CurrentPassword", currentPassword); // تشفير كلمة المرور بواسطة SQL مباشرة

                con.Open();
                object result = cmd.ExecuteScalar(); // فحص إذا كانت كلمة المرور صحيحة

                if (result != null)
                {
                    // إذا كانت كلمة المرور القديمة صحيحة، نقوم بتحديثها
                    string updateQuery = "UPDATE Users SET PasswordHash = HASHBYTES('SHA2_256', @NewPassword) WHERE UserID = @UserID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@UserID", userId);
                    updateCmd.Parameters.AddWithValue("@NewPassword", newPassword); // تخزين كلمة المرور المشفرة

                    updateCmd.ExecuteNonQuery();
                    lblMessage.Text = "تم تغيير كلمة المرور بنجاح.";
                    lblMessage.CssClass = "alert alert-success";
                }
                else
                {
                    lblMessage.Text = "كلمة المرور الحالية غير صحيحة.";
                    lblMessage.CssClass = "alert alert-danger";
                }

                con.Close();
            }
        }

    }
}
