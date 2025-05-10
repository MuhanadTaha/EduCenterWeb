using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace EduCenterWeb
{
    public partial class AddAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CourseID"] == null)
                {
                    lblMessage.Text = "لم يتم تحديد الدورة.";
                    btnAddAssignment.Enabled = false;
                }
            }
        }

        protected void btnAddAssignment_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CourseID"] == null)
            {
                lblMessage.Text = "الدورة غير معروفة.";
                return;
            }

            int courseId = int.Parse(Request.QueryString["CourseID"]);
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            DateTime dueDate;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(txtDueDate.Text) || !DateTime.TryParse(txtDueDate.Text, out dueDate))
            {
                lblMessage.Text = "يرجى تعبئة جميع الحقول بشكل صحيح.";
                return;
            }

            string filePath = string.Empty;

            if (fuAssignmentFile.HasFile)
            {
                // الحصول على اسم الملف الأصلي
                string fileName = Path.GetFileName(fuAssignmentFile.FileName);

                // إضافة التاريخ والوقت (ساعة، دقيقة، ثانية) إلى اسم الملف
                string uniqueFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + Path.GetExtension(fileName);

                // تحديد المسار الذي سيتم حفظ الملف فيه
                filePath = "Uploads/Assignments/" + uniqueFileName;

                // حفظ الملف على السيرفر
                fuAssignmentFile.SaveAs(Server.MapPath(filePath));
            }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EduCenterConnection"].ToString()))
            {
                string query = "INSERT INTO Assignments (CourseID, Title, Description, DueDate, AssignmentFile) " +
                               "VALUES (@CourseID, @Title, @Description, @DueDate, @AssignmentFile)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@DueDate", dueDate);
                cmd.Parameters.AddWithValue("@AssignmentFile", filePath); // إضافة مسار الملف

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            lblMessage.CssClass = "text-success";
            lblMessage.Text = "تمت إضافة التاسك بنجاح.";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtDueDate.Text = "";
        }
    }
}
