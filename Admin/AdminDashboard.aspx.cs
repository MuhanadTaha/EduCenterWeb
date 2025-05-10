using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EduCenterWeb.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        private SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ConnectionString);

           
            
            if (!IsPostBack)
            {
                
                LoadCenters();
                LoadCourses();
                LoadTeachers();
                LoadTeachers2();
                LoadStudents();
                LoadAds();

            }
        }

        private void LoadCenters()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CenterId, CenterName FROM EducationalCenters", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CentersGrid.DataSource = dt;
                CentersGrid.DataBind();

                NewCourseCenter.DataSource = dt;
                NewCourseCenter.DataTextField = "CenterName";
                NewCourseCenter.DataValueField = "CenterId";
                NewCourseCenter.DataBind();
                NewCourseCenter.Items.Insert(0, new ListItem("اختر مركزًا", "0"));


                //Ad
                AdCenterDropDown.DataSource = dt;
                AdCenterDropDown.DataTextField = "CenterName";
                AdCenterDropDown.DataValueField = "CenterId";
                AdCenterDropDown.DataBind();
                AdCenterDropDown.Items.Insert(0, new ListItem("اختر مركزًا", "0"));

                
            }
            catch (Exception ex)
            {
                Response.Write("خطأ في المراكز: " + ex.Message);
            }
            finally
            {

                con.Close();
                LoadCourses();
                
            }
        }

        protected void AddCenterBtn_Click(object sender, EventArgs e)
        {
            string centerName = NewCenterName.Text.Trim();

            if (!string.IsNullOrEmpty(centerName))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO EducationalCenters (CenterName) VALUES (@CenterName)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CenterName", centerName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // يمكنك إضافة تسجيل الأخطاء هنا
                }
                finally
                {
                    con.Close();
                    LoadCenters();
                    NewCenterName.Text = "";
                }
            }
        }

        protected void CentersGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CentersGrid.EditIndex = e.NewEditIndex;
            LoadCenters();
        }

        protected void CentersGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CentersGrid.EditIndex = -1;
            LoadCenters();
        }

        protected void CentersGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int centerId = Convert.ToInt32(CentersGrid.DataKeys[e.RowIndex].Value);
            string newName = ((TextBox)CentersGrid.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim();

            try
            {
                con.Open();
                string query = "UPDATE EducationalCenters SET CenterName = @CenterName WHERE CenterId = @CenterId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CenterName", newName);
                cmd.Parameters.AddWithValue("@CenterId", centerId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // يمكنك إضافة تسجيل الأخطاء هنا
            }
            finally
            {
                con.Close();
                CentersGrid.EditIndex = -1;
                LoadCenters();
            }
        }

        protected void CentersGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int centerId = Convert.ToInt32(CentersGrid.DataKeys[e.RowIndex].Value);

            try
            {
                con.Open();
                string query = "DELETE FROM EducationalCenters WHERE CenterId = @CenterId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CenterId", centerId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // يمكنك إضافة تسجيل الأخطاء هنا
            }
            finally
            {
                con.Close();
                LoadCenters();
            }
        }

        // تعديل الكود لإضافة وتعديل وحذف الدورات
        private void LoadCourses()
        {
            try
            {
                con.Open();
                string query = @"
                    SELECT C.CourseID, C.CourseName, U.FullName AS TeacherName, EC.CenterName
                    FROM Courses C
                    JOIN Users U ON C.TeacherId = U.UserID
                    JOIN EducationalCenters EC ON C.CenterId = EC.CenterId";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                CoursesGrid.DataSource = dt;
                CoursesGrid.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("خطأ في الدورات: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    

        protected void AddCourseBtn_Click(object sender, EventArgs e)
        {
            string courseName = NewCourseName.Text.Trim();
            int teacherId = Convert.ToInt32(NewCourseTeacher.SelectedValue); // المعلم المختار
            int centerId = Convert.ToInt32(NewCourseCenter.SelectedValue); // المركز المختار

            if (!string.IsNullOrEmpty(courseName) && teacherId > 0 && centerId > 0)
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO Courses (CourseName, TeacherId, CenterId) VALUES (@CourseName, @TeacherId, @CenterId)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    cmd.Parameters.AddWithValue("@CenterId", centerId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // يمكنك إضافة تسجيل الأخطاء هنا
                }
                finally
                {
                    con.Close();
                    LoadCourses(); // إعادة تحميل الدورات بعد الإضافة
                }
            }
        }

        protected void CoursesGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CoursesGrid.EditIndex = e.NewEditIndex;
            LoadCourses();
        }

        protected void CoursesGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CoursesGrid.EditIndex = -1;
            LoadCourses();
        }

        //protected void CoursesGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int courseId = Convert.ToInt32(CoursesGrid.DataKeys[e.RowIndex].Value);
        //    string newName = ((TextBox)CoursesGrid.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim();
        //    string teacherName = ((DropDownList)CoursesGrid.Rows[e.RowIndex].Cells[2].Controls[0]).SelectedItem.Text;
        //    string centerName = ((DropDownList)CoursesGrid.Rows[e.RowIndex].Cells[3].Controls[0]).SelectedItem.Text;

        //    try
        //    {
        //        con.Open();
        //        string query = "UPDATE Courses SET CourseName = @CourseName, TeacherName = @TeacherName, CenterName = @CenterName WHERE CourseID = @CourseID";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@CourseName", newName);
        //        cmd.Parameters.AddWithValue("@TeacherName", teacherName);
        //        cmd.Parameters.AddWithValue("@CenterName", centerName);
        //        cmd.Parameters.AddWithValue("@CourseID", courseId);
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        // يمكنك إضافة تسجيل الأخطاء هنا
        //    }
        //    finally
        //    {
        //        con.Close();
        //        CoursesGrid.EditIndex = -1;
        //        LoadCourses();
        //    }
        //}

        protected void CoursesGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int courseId = Convert.ToInt32(CoursesGrid.DataKeys[e.RowIndex].Value);

            try
            {
                con.Open();
                string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // يمكنك إضافة تسجيل الأخطاء هنا
            }
            finally
            {
                con.Close();
                LoadCourses();
            }
        }


        // الدالة لتحميل المعلمين
        private void LoadTeachers()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserID, FullName FROM Users WHERE Role = 'Teacher'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                NewCourseTeacher.DataSource = dt;
                NewCourseTeacher.DataTextField = "FullName";
                NewCourseTeacher.DataValueField = "UserID";
                NewCourseTeacher.DataBind();
                NewCourseTeacher.Items.Insert(0, new ListItem("اختر معلمًا", "0"));


            }
            catch (Exception ex)
            {
                Response.Write("خطأ في تحميل المعلمين: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadTeachers2()
        {
            try
            {
                con.Open();
                string query = "SELECT UserID, Username, FullName FROM Users WHERE Role = 'Teacher'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TeachersGrid.DataSource = dt;
                TeachersGrid.DataBind();
            }
            catch (Exception ex)
            {
                // سجل الخطأ أو أظهر رسالة مناسبة
            }
            finally
            {
                con.Close();
            }
        }




        // دالة لتفعيل وضع التعديل عند الضغط على زر التعديل
        protected void TeachersGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TeachersGrid.EditIndex = e.NewEditIndex;
            LoadTeachers();
            LoadTeachers2();
        }

        // دالة لإلغاء وضع التعديل عند الضغط على زر إلغاء التعديل
        protected void TeachersGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TeachersGrid.EditIndex = -1;
            LoadTeachers();
            LoadTeachers2();
        }

        // دالة لتحديث بيانات المعلم عند حفظ التعديلات
        protected void TeachersGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int teacherId = Convert.ToInt32(TeachersGrid.DataKeys[e.RowIndex].Value);
            string newName = ((TextBox)TeachersGrid.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim();

            try
            {
                con.Open();
                string query = "UPDATE Teachers SET TeacherName = @TeacherName WHERE TeacherID = @TeacherID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherName", newName);
                cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // يمكنك إضافة تسجيل الأخطاء هنا
            }
            finally
            {
                con.Close();
                TeachersGrid.EditIndex = -1;
                LoadTeachers();
                LoadTeachers2();
            }
        }

        // دالة لحذف المعلم
        protected void TeachersGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(TeachersGrid.DataKeys[e.RowIndex].Value);

            try
            {
                con.Open();
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الخطأ أو إظهاره للمستخدم
            }
            finally
            {
                con.Close();
                LoadTeachers();
                LoadTeachers2(); // تأكد أنك تستدعي الدالة الصحيحة اللي بترجع المعلمين
            }
        }


        private void LoadStudents()
        {
            try
            {
                con.Open();
                string query = "SELECT UserID, FullName, Username FROM Users WHERE Role = 'Student'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                StudentsGrid.DataSource = dt;
                StudentsGrid.DataBind();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ إن لزم
            }
            finally
            {
                con.Close();
            }
        }


        protected void StudentsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int studentId = Convert.ToInt32(StudentsGrid.DataKeys[e.RowIndex].Value);

            try
            {
                con.Open();
                string query = "DELETE FROM Users WHERE UserID = @UserID AND Role = 'Student'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", studentId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الخطأ هنا
            }
            finally
            {
                con.Close();
                LoadStudents(); // إعادة تحميل القائمة بعد الحذف
            }
        }


        private void LoadAds()
        {
            try
            {
                con.Open();
                string query = @"
            SELECT A.AnnouncementID, A.Title, A.Content, A.PostedDate, C.CenterName
            FROM Announcements A
            Left JOIN EducationalCenters C ON A.CenterID = C.CenterId";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                AdsGrid.DataSource = dt;
                AdsGrid.DataBind();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ
            }
            finally
            {
                con.Close();
            }
        }


        protected void AddAdBtn_Click(object sender, EventArgs e)
        {
            string title = NewAdTitle.Text.Trim();
            string content = NewAdContent.Text.Trim();
            int centerId = Convert.ToInt32(AdCenterDropDown.SelectedValue);

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(content) && centerId > 0)
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO Announcements (Title, Content, PostedDate, CenterID) VALUES (@Title, @Content, GETDATE(), @CenterID)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Content", content);
                    cmd.Parameters.AddWithValue("@CenterID", centerId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // سجل الخطأ
                }
                finally
                {
                    con.Close();
                    LoadAds();
                    NewAdTitle.Text = "";
                    NewAdContent.Text = "";
                }
            }
        }


        protected void AdsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int announcementId = Convert.ToInt32(AdsGrid.DataKeys[e.RowIndex].Value);

            try
            {
                con.Open();
                string query = "DELETE FROM Announcements WHERE AnnouncementID = @ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", announcementId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // سجل الخطأ
            }
            finally
            {
                con.Close();
                LoadAds();
            }
        }









    }
}
