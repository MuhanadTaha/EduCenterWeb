using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace EduCenterWeb
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAvailableCourses();
                LoadAdvertisements();
            }
        }

        private void LoadAvailableCourses()
        {
            try
            {
                con.Open();
                string query = @"SELECT C.CourseID, C.CourseName, U.FullName AS TeacherName, EC.CenterName
                    FROM Courses C
                    JOIN Users U ON C.TeacherId = U.UserID
                    JOIN EducationalCenters EC ON C.CenterId = EC.CenterId";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                CoursesGrid.DataSource = dt;
                CoursesGrid.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("حدث خطأ أثناء تحميل الدورات: " + ex.Message, true);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadAdvertisements()
        {
            try
            {
                con.Open();
                string query = "SELECT Title, Content, PostedDate FROM [Announcements]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                AdvertisementsRepeater.DataSource = dt;
                AdvertisementsRepeater.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("حدث خطأ أثناء تحميل الإعلانات: " + ex.Message, true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void CoursesGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Enroll")
            {
                if (Session["UserID"] != null && Session["Role"] != null &&
                    string.Equals(Session["Role"].ToString(), "Student", StringComparison.OrdinalIgnoreCase))
                {
                    int studentId = Convert.ToInt32(Session["UserID"]);
                    int courseId = Convert.ToInt32(e.CommandArgument);

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString))
                    {
                        string checkQuery = "SELECT COUNT(*) FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
                        string insertQuery = "INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate) VALUES (@StudentID, @CourseID, @EnrollmentDate)";

                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@StudentID", studentId);
                        checkCmd.Parameters.AddWithValue("@CourseID", courseId);

                        conn.Open();
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists == 0)
                        {
                            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@StudentID", studentId);
                            insertCmd.Parameters.AddWithValue("@CourseID", courseId);
                            insertCmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                            insertCmd.ExecuteNonQuery();

                            ShowMessage("تم التسجيل في الدورة بنجاح.");
                        }
                        else
                        {
                            ShowMessage("أنت مسجل بالفعل في هذه الدورة.");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }

        private void ShowMessage(string message, bool isError = false)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isError ? "alert alert-danger" : "alert alert-success";
            lblMessage.CssClass += " d-block"; // تأكد من ظهور العنصر
        }
    }
}
