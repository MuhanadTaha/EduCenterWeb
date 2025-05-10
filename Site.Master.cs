using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EduCenterWeb
{
    public partial class SiteMaster : MasterPage
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Authantication();
                //Authorization();
            }
        }

        protected void btnOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/Account/Login.aspx");
        }


        protected void Authantication()
        {
            string userId = Convert.ToString(Session["UserID"]);
            string role = Convert.ToString(Session["Role"]);

            string str = "SELECT UserID FROM Users WHERE UserID = @UserID";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@UserID", userId);

            con.Open();
            string Result = Convert.ToString(cmd.ExecuteScalar());
            con.Close();

            if (string.IsNullOrWhiteSpace(Result))
            {
                btnChangPass.Visible = false;
                lnkDashboard.Visible = false;
            }
            else
            {
                btnOut.Visible = true;
                btnLogin.Visible = false;
                btnRegister.Visible = false;
                btnChangPass.Visible = true;

                if (!string.IsNullOrEmpty(role))
                {
                    role = role.ToLower();
                    switch (role)
                    {
                        case "admin":
                            lnkDashboard.NavigateUrl = "~/Admin/AdminDashboard.aspx";
                            lnkDashboard.Visible = true;
                            break;
                        case "teacher":
                            lnkDashboard.NavigateUrl = "~/Account/TeacherDashboard.aspx";
                            lnkDashboard.Visible = true;
                            break;
                        case "student":
                            lnkDashboard.NavigateUrl = "~/Account/StudentDashboard.aspx";
                            lnkDashboard.Visible = true;
                            break;
                        default:
                            lnkDashboard.Visible = false;
                            break;
                    }
                }
            }
        }


    }
}