using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EduCenterWeb
{
    public partial class CourseDetails : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["courseId"] != null)
                {
                    int courseId = Convert.ToInt32(Request.QueryString["courseId"]);
                    LoadStudents(courseId);
                }
                else
                {
                    // إعادة التوجيه إذا لم يتم تحديد CourseID
                    Response.Redirect("~/Account/TeacherDashboard.aspx");
                }
            }
        }

        private void LoadStudents(int courseId)
        {
            try
            {
                con.Open();
                string query = @"
                    SELECT u.UserID, u.FullName, u.Username
                    FROM Users u
                    INNER JOIN Enrollments e ON u.UserID = e.StudentID
                    WHERE u.Role = 'Student' AND e.CourseID = @CourseID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                StudentsGrid.DataSource = dt;
                StudentsGrid.DataBind();
            }
            catch (Exception ex)
            {
                // سجل الخطأ إن وجد
                lblMessage.Text = "حدث خطأ في تحميل الطلاب: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void StudentsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewSubmissions")
            {
                try
                {
                    int studentId = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("~/ViewSubmissions.aspx?StudentID=" + studentId);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "حدث خطأ أثناء الانتقال إلى التسليمات: " + ex.Message;
                }
            }
        }
    }
}
