using System;
using System.Data.SqlClient;
using System.Configuration;

namespace EduCenterWeb.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadCenters(); // تحميل المراكز التعليمية في الـ Dropdown
            }
        }

        // تحميل المراكز التعليمية من قاعدة البيانات
        //private void LoadCenters()
        //{
        //    string connStr = ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        string query = "SELECT CenterID, CenterName FROM EducationalCenters";
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        ddlCenters.DataSource = reader;
        //        ddlCenters.DataValueField = "CenterID";
        //        ddlCenters.DataTextField = "CenterName";
        //        ddlCenters.DataBind();
        //    }
        //}

        // عند الضغط على زر التسجيل
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // أولاً: تحقق إن كان اسم المستخدم موجود مسبقاً
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", txtUsername.Text);

                try
                {
                    conn.Open();
                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        lblMessage.CssClass = "text-danger";
                        lblMessage.Text = "اسم المستخدم مستخدم بالفعل. الرجاء اختيار اسم مختلف.";
                        return;
                    }

                    // إذا لم يكن موجوداً، قم بإدخاله
                    string insertQuery = @"INSERT INTO Users (Username, PasswordHash, FullName, Role)
                                   VALUES (@username, HASHBYTES('SHA2_256', @password), @fullName, @role)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    insertCmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    insertCmd.Parameters.AddWithValue("@fullName", txtFullName.Text);
                    insertCmd.Parameters.AddWithValue("@role", ddlRole.SelectedValue);
                    //insertCmd.Parameters.AddWithValue("@centerId", ddlCenters.SelectedValue);

                    insertCmd.ExecuteNonQuery();
                    lblMessage.CssClass = "text-success";
                    lblMessage.Text = "تم التسجيل بنجاح! يمكنك تسجيل الدخول الآن.";
                }
                catch (Exception ex)
                {
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = "حدث خطأ أثناء التسجيل: " + ex.Message;
                }
            }
        }

    }
}
