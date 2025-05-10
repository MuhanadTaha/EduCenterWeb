using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace EduCenterWeb
{
    public partial class SubmitAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["AssignmentID"] == null)
            {
                lblMessage.Text = "لم يتم تحديد التاسك.";
                btnSubmit.Enabled = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (fuAssignmentFile.HasFile && Request.QueryString["AssignmentID"] != null)
            {
                // الحصول على AssignmentID من الـ QueryString
                int assignmentId = int.Parse(Request.QueryString["AssignmentID"]);

                // أخذ StudentID من السيشن
                int studentId = Convert.ToInt32(Session["UserID"]); // تأكد من وجودها في السيشن عند تسجيل الدخول

                string fileName = Path.GetFileName(fuAssignmentFile.FileName);
                // إضافة التاريخ والوقت إلى اسم الملف لتجنب الكتابة فوقه
                string uniqueFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + Path.GetExtension(fileName);
                string savePath = Server.MapPath("~/Uploads/Assignments/") + uniqueFileName;

                try
                {
                    // حفظ الملف على السيرفر
                    fuAssignmentFile.SaveAs(savePath);

                    // حفظ البيانات في قاعدة البيانات
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ToString()))
                    {
                        string query = "INSERT INTO AssignmentSubmissions (AssignmentID, StudentID, FilePath, SubmissionDate) " +
                                       "VALUES (@AssignmentID, @StudentID, @FilePath, GETDATE())";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                        cmd.Parameters.AddWithValue("@StudentID", studentId);
                        cmd.Parameters.AddWithValue("@FilePath", "Uploads/Assignments/" + uniqueFileName); // حفظ المسار الفريد للملف

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    lblMessage.CssClass = "text-success";
                    lblMessage.Text = "تم رفع الملف بنجاح.";
                }
                catch (Exception ex)
                {
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = "حدث خطأ أثناء رفع الملف: " + ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "يرجى اختيار ملف لرفعه.";
            }
        }
    }
}
