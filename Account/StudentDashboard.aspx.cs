using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EduCenterWeb.Student
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudentCourses();
            }

            if (Session["UserID"] != null)
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                LoadStudentName(userId);

            }
            else
            {
                // إعادة التوجيه إذا لم يكن المستخدم مسجل دخول
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        // تحميل الدورات التي الطالب مسجل فيها
        private void LoadStudentCourses()
        {
            // تأكد من أن الـ UserID موجود في الجلسة
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            int studentID = Convert.ToInt32(Session["UserID"]);

            string query = "SELECT c.CourseID, c.CourseName FROM Enrollments e " +
                           "JOIN Courses c ON e.CourseID = c.CourseID WHERE e.StudentID = @StudentID";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                gvCourses.DataSource = reader;
                gvCourses.DataBind();
            }
        }

        // زر عرض التاسكات للدورة
        protected void ViewAssignments_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int courseID = int.Parse(btn.CommandArgument);

            // توجه إلى صفحة عرض التاسكات الخاصة بالدورة
            Response.Redirect($"~/Assignments.aspx?CourseID={courseID}");
        }


        private void LoadStudentName(int userId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT FullName FROM Users WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    lblWelcome.Text = "مرحبًا، " + result.ToString();
                }
            }
        }
    }
}
