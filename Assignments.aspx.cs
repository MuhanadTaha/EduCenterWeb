using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace EduCenterWeb
{
    public partial class Assignments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int courseID = Convert.ToInt32(Request.QueryString["CourseID"]);
                LoadAssignments(courseID);
            }
        }

        public void LoadAssignments(int courseID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EduCenterConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AssignmentID, Title, DueDate, AssignmentFile FROM Assignments WHERE CourseID = @CourseID";


                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvAssignments.DataSource = dt;
                gvAssignments.DataBind();
            }
        }

        protected void SubmitTask_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int assignmentID = int.Parse(btn.CommandArgument);
            Response.Redirect($"SubmitAssignment.aspx?AssignmentID={assignmentID}");
        }
    }
}
