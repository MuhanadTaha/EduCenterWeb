using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EduCenterWeb
{
    public partial class ViewSubmissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["StudentID"] != null)
                {
                    int studentId;
                    if (int.TryParse(Request.QueryString["StudentID"], out studentId))
                    {
                        LoadSubmissions(studentId);
                    }
                    else
                    {
                        lblMessage.Text = "معرف الطالب غير صالح.";
                    }
                }
                else
                {
                    lblMessage.Text = "لم يتم تحديد الطالب.";
                }
            }
        }

        private void LoadSubmissions(int studentId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["EduCenterConnection"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT a.Title, s.FilePath, s.SubmissionDate
                    FROM AssignmentSubmissions s
                    INNER JOIN Assignments a ON s.AssignmentID = a.AssignmentID
                    WHERE s.StudentID = @StudentID
                    ORDER BY s.SubmissionDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvSubmissions.DataSource = dt;
                gvSubmissions.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMessage.Text = "لم يتم العثور على تسليمات لهذا الطالب.";
                }
            }
        }
    }
}
