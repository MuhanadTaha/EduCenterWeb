using System;
using System.Data.SqlClient;
using System.Configuration;

namespace EduCenterWeb.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // لا شيء حالياً
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT UserID, Role 
                    FROM Users 
                    WHERE Username = @username 
                      AND PasswordHash = HASHBYTES('SHA2_256', @password)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["UserID"]);
                    string role = reader["Role"].ToString();

                    // حفظ الـ UserID في الـ Session
                    Session["UserID"] = userId;
                    Session["Role"] = role;

                    // توجيه المستخدم حسب الدور
                    if (role == "Student")
                    {
                        Response.Redirect("~/Account/StudentDashboard.aspx");
                    }
                    else if (role == "Teacher")
                    {
                        Response.Redirect("~/Account/TeacherDashboard.aspx");
                    }
                    else if (role == "Admin")
                    {
                        Response.Redirect("~/Admin/AdminDashboard.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "اسم المستخدم أو كلمة المرور غير صحيحة.";
                }
            }
        }
    }
}
