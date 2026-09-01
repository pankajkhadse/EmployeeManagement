using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Department : System.Web.UI.Page
    {
        // ============================================
        // PAGE LOAD
        // ============================================

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
                LoadDepartments();

                btnAdd.Visible = true;
                btnUpdate.Visible = false;
            }
        }


        // ============================================
        // LOAD EMPLOYEES INTO DROPDOWN
        // ============================================

        private void LoadEmployees(int? selectedEmployeeId = null)
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;

            string query;

            if (selectedEmployeeId.HasValue)
            {
                // EDIT MODE
                // Show employees who have no department
                // OR the employee currently being edited

                query = @"
            SELECT
                e.Emp_Id,
                e.Emp_Name
            FROM Employee e
            LEFT JOIN Department d
                ON e.Emp_Id = d.Emp_Id
            WHERE d.Emp_Id IS NULL
               OR e.Emp_Id = @EmpId
            ORDER BY e.Emp_Name";
            }
            else
            {
                // ADD MODE
                // Show only employees without a department

                query = @"
            SELECT
                e.Emp_Id,
                e.Emp_Name
            FROM Employee e
            LEFT JOIN Department d
                ON e.Emp_Id = d.Emp_Id
            WHERE d.Emp_Id IS NULL
            ORDER BY e.Emp_Name";
            }

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    if (selectedEmployeeId.HasValue)
                    {
                        command.Parameters.AddWithValue(
                            "@EmpId",
                            selectedEmployeeId.Value);
                    }

                    connection.Open();

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        ddlEmployee.Items.Clear();

                        ddlEmployee.Items.Add(
                            new ListItem(
                                "-- Select Employee --",
                                ""));

                        while (reader.Read())
                        {
                            ddlEmployee.Items.Add(
                                new ListItem(
                                    reader["Emp_Name"].ToString(),
                                    reader["Emp_Id"].ToString()));
                        }
                    }
                }
            }
        }

        // ============================================
        // ADD DEPARTMENT
        // ============================================

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            int empId =
                Convert.ToInt32(ddlEmployee.SelectedValue);

            string departmentName =
                txtDptName.Text.Trim();

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();


                // ============================================
                // CHECK EMPLOYEE EXISTS
                // ============================================

                string employeeCheckQuery = @"
                SELECT COUNT(*)
                FROM Employee
                WHERE Emp_Id = @EmpId";

                using (SqlCommand command =
                       new SqlCommand(
                           employeeCheckQuery,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@EmpId",
                        empId);

                    int employeeCount =
                        Convert.ToInt32(
                            command.ExecuteScalar());

                    if (employeeCount == 0)
                    {
                        lblMessage.Text =
                            "Employee does not exist.";

                        return;
                    }
                }


                // ============================================
                // CHECK DEPARTMENT ALREADY EXISTS
                // ============================================

                string departmentCheckQuery = @"
                SELECT COUNT(*)
                FROM Department
                WHERE Emp_Id = @EmpId";

                using (SqlCommand command =
                       new SqlCommand(
                           departmentCheckQuery,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@EmpId",
                        empId);

                    int departmentCount =
                        Convert.ToInt32(
                            command.ExecuteScalar());

                    if (departmentCount > 0)
                    {
                        lblMessage.Text =
                            "A department is already assigned to this employee.";

                        return;
                    }
                }


                // ============================================
                // INSERT DEPARTMENT
                // ============================================

                string insertQuery = @"
                INSERT INTO Department
                (
                    Emp_Id,
                    Dpt_Name
                )
                VALUES
                (
                    @EmpId,
                    @DptName
                )";

                using (SqlCommand command =
                       new SqlCommand(
                           insertQuery,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@EmpId",
                        empId);

                    command.Parameters.AddWithValue(
                        "@DptName",
                        departmentName);

                    command.ExecuteNonQuery();
                }
            }


            // ============================================
            // SUCCESS
            // ============================================

            lblMessage.Text =
                "Department added successfully.";

            ClearForm();

            LoadEmployees();

            LoadDepartments();
        }


        // ============================================
        // LOAD ALL DEPARTMENTS
        // ============================================

        private void LoadDepartments()
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;

            string query = @"
            SELECT
                e.Emp_Id,
                e.Emp_Name,
                d.Dpt_Name
            FROM Department d
            INNER JOIN Employee e
                ON d.Emp_Id = e.Emp_Id
            ORDER BY e.Emp_Name";


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter =
                       new SqlDataAdapter(
                           query,
                           connection))
                {
                    System.Data.DataTable dt =
                        new System.Data.DataTable();

                    adapter.Fill(dt);

                    gvDepartments.DataSource = dt;

                    gvDepartments.DataBind();
                }
            }
        }


        // ============================================
        // EDIT DEPARTMENT
        // ============================================

        protected void btnEdit_Command(
     object sender,
     CommandEventArgs e)
        {
            int empId =
                Convert.ToInt32(e.CommandArgument);

            hfDepartmentEmpId.Value =
                empId.ToString();

            // Reload dropdown including the employee
            // currently being edited
            LoadEmployees(empId);

            LoadDepartmentByEmployeeId(empId);

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
        }

        // ============================================
        // LOAD SINGLE DEPARTMENT
        // ============================================

        private void LoadDepartmentByEmployeeId(
            int empId)
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;

            string query = @"
            SELECT
                Emp_Id,
                Dpt_Name
            FROM Department
            WHERE Emp_Id = @EmpId";


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlCommand command =
                       new SqlCommand(
                           query,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@EmpId",
                        empId);

                    connection.Open();

                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Select employee in dropdown
                            string employeeId =
                             reader["Emp_Id"].ToString();

                            if (ddlEmployee.Items.FindByValue(employeeId) != null)
                            {
                                ddlEmployee.SelectedValue = employeeId;
                            }

                            // Load department name
                            txtDptName.Text =
                                reader["Dpt_Name"].ToString();
                        }
                    }
                }
            }
        }


        // ============================================
        // UPDATE DEPARTMENT
        // ============================================

        protected void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }


            if (string.IsNullOrEmpty(
                hfDepartmentEmpId.Value))
            {
                lblMessage.Text =
                    "Please select a department to update.";

                return;
            }


            int originalEmpId =
                Convert.ToInt32(
                    hfDepartmentEmpId.Value);


            int selectedEmpId =
                Convert.ToInt32(
                    ddlEmployee.SelectedValue);


            string departmentName =
                txtDptName.Text.Trim();


            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();


                // ============================================
                // IF EMPLOYEE WAS CHANGED DURING EDIT
                // CHECK WHETHER NEW EMPLOYEE ALREADY HAS DEPARTMENT
                // ============================================

                if (selectedEmpId != originalEmpId)
                {
                    string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Department
                    WHERE Emp_Id = @EmpId";

                    using (SqlCommand checkCommand =
                           new SqlCommand(
                               checkQuery,
                               connection))
                    {
                        checkCommand.Parameters.AddWithValue(
                            "@EmpId",
                            selectedEmpId);

                        int count =
                            Convert.ToInt32(
                                checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            lblMessage.Text =
                                "The selected employee already has a department.";

                            return;
                        }
                    }
                }


                // ============================================
                // UPDATE
                // ============================================

                string updateQuery = @"
                UPDATE Department
                SET
                    Emp_Id = @NewEmpId,
                    Dpt_Name = @DptName
                WHERE Emp_Id = @OldEmpId";


                using (SqlCommand command =
                       new SqlCommand(
                           updateQuery,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@NewEmpId",
                        selectedEmpId);

                    command.Parameters.AddWithValue(
                        "@DptName",
                        departmentName);

                    command.Parameters.AddWithValue(
                        "@OldEmpId",
                        originalEmpId);

                    command.ExecuteNonQuery();
                }
            }


            // ============================================
            // SUCCESS
            // ============================================
            
            lblMessage.Text =
                "Department updated successfully.";

            ClearForm();

            hfDepartmentEmpId.Value = "";

            btnAdd.Visible = true;
            btnUpdate.Visible = false;

            // Reload only employees without departments
            LoadEmployees();

            LoadDepartments();
        }


        // ============================================
        // DELETE DEPARTMENT
        // ============================================

        protected void btnDelete_Command(
            object sender,
            CommandEventArgs e)
        {
            int empId =
                Convert.ToInt32(e.CommandArgument);


            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            string query = @"
            DELETE FROM Department
            WHERE Emp_Id = @EmpId";


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlCommand command =
                       new SqlCommand(
                           query,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@EmpId",
                        empId);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }


            lblMessage.Text =
                "Department deleted successfully.";

            LoadDepartments();
        }


        // ============================================
        // CANCEL UPDATE
        // ============================================

        protected void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            ClearForm();

            hfDepartmentEmpId.Value = "";

            btnAdd.Visible = true;

            btnUpdate.Visible = false;

            btnCancel.Visible = false;

            lblMessage.Text = "";

            LoadEmployees();
        }


        // ============================================
        // CLEAR FORM
        // ============================================

        private void ClearForm()
        {
            ddlEmployee.SelectedIndex = 0;

            txtDptName.Text = "";
        }
    }


}
