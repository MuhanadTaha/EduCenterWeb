using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EduCenterWeb.Teacher
{
    public partial class TeacherDashboard : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Role"] != null && Session["Role"].ToString() != "Teacher" || Session["UserID"] == null)
            {
                // إعادة التوجيه إذا لم يكن المستخدم مسجل دخول
                Response.Redirect("~/Account/Login.aspx");
            }
     
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserID"]);
                    LoadTeacherName(userId);
                    LoadCourses();
                   // LoadTeacherCourses(userId);
                }
                else
                {
                    // إعادة التوجيه إذا لم يكن المستخدم مسجل دخول
                    Response.Redirect("~/Account/Login.aspx");
                }
                
            }
        }

        private void LoadCourses()
        {
            try
            {
                con.Open();

                // ملاحظة: يجب أن تكون المعلم مسجل الدخول، هنا مؤقتاً TeacherId = 1
                int teacherId = Convert.ToInt32(Session["UserID"]);


                string query = @"
                    SELECT c.CourseID, c.CourseName, ec.CenterName,
                           (SELECT COUNT(*) FROM Enrollments sc WHERE sc.CourseID = c.CourseID) AS StudentCount
                    FROM Courses c
                    INNER JOIN EducationalCenters ec ON c.CenterId = ec.CenterId
                    WHERE c.TeacherId = @TeacherId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CoursesGrid.DataSource = dt;
                CoursesGrid.DataBind();
            }
            catch (Exception ex)
            {
                // سجل الخطأ إن وجد
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadTeacherName(int userId)
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
