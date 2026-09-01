using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Employee : System.Web.UI.Page
    {
        // ============================================
        // PAGE LOAD
        // ============================================

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
            }
        }


        // ============================================
        // ADD EMPLOYEE
        // ============================================

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Stop if validation fails
            if (!Page.IsValid)
            {
                return;
            }


            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            string query = @"
                INSERT INTO Employee
                (
                    Emp_Name,
                    Emp_Age,
                    Emp_Salary
                )
                VALUES
                (
                    @Name,
                    @Age,
                    @Salary
                )";


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {

                    // Employee Name

                    command.Parameters.Add(
                        "@Name",
                        SqlDbType.VarChar,
                        100).Value =
                        txtName.Text.Trim();


                    // Employee Age

                    command.Parameters.Add(
                        "@Age",
                        SqlDbType.Int).Value =
                        Convert.ToInt32(txtAge.Text);


                    // Employee Salary

                    command.Parameters.Add(
                        "@Salary",
                        SqlDbType.Decimal).Value =
                        Convert.ToDecimal(txtSalary.Text);


                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }


            // Success message

            lblMessage.Text =
                "Employee added successfully.";


            // Clear form

            ClearForm();


            // Refresh GridView

            LoadEmployees();
        }


        // ============================================
        // LOAD ALL EMPLOYEES
        // ============================================

        private void LoadEmployees()
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            // LEFT JOIN allows employees without
            // a department to also appear.

            string query = @"
                SELECT
                    e.Emp_Id,
                    e.Emp_Name,
                    e.Emp_Age,
                    e.Emp_Salary,
                    ISNULL(d.Dpt_Name, 'Department Not Assigned') AS Dpt_Name
                FROM Employee e
                LEFT JOIN Department d
                    ON e.Emp_Id = d.Emp_Id";


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter =
                       new SqlDataAdapter(
                           query,
                           connection))
                {
                    DataTable dt =
                        new DataTable();


                    adapter.Fill(dt);


                    gvEmployees.DataSource =
                        dt;


                    gvEmployees.DataBind();
                }
            }
        }


        // ============================================
        // LOAD SINGLE EMPLOYEE
        // ============================================

        private void LoadEmployeeById(int empId)
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            string query = @"
                SELECT
                    Emp_Name,
                    Emp_Age,
                    Emp_Salary
                FROM Employee
                WHERE Emp_Id = @EmpId";


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlCommand command =
                       new SqlCommand(
                           query,
                           connection))
                {

                    command.Parameters.Add(
                        "@EmpId",
                        SqlDbType.Int).Value =
                        empId;


                    connection.Open();


                    using (SqlDataReader reader =
                           command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            txtName.Text =
                                reader["Emp_Name"]
                                .ToString();


                            txtAge.Text =
                                reader["Emp_Age"]
                                .ToString();


                            txtSalary.Text =
                                reader["Emp_Salary"]
                                .ToString();
                        }
                    }
                }
            }
        }


        // ============================================
        // EDIT EMPLOYEE
        // ============================================

        protected void btnEdit_Command(
            object sender,
            CommandEventArgs e)
        {
            int empId =
                Convert.ToInt32(
                    e.CommandArgument);


            // Store ID for Update

            hfEmpId.Value =
                empId.ToString();


            // Load employee data

            LoadEmployeeById(empId);


            // Switch Add → Update

            btnAdd.Visible =
                false;


            btnUpdate.Visible =
                true;
        }


        // ============================================
        // UPDATE EMPLOYEE
        // ============================================

        protected void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            // Stop if validation fails

            if (!Page.IsValid)
            {
                return;
            }


            // Get Employee ID

            int empId =
                Convert.ToInt32(
                    hfEmpId.Value);


            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            string query = @"
                UPDATE Employee
                SET
                    Emp_Name = @Name,
                    Emp_Age = @Age,
                    Emp_Salary = @Salary
                WHERE Emp_Id = @EmpId";


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                using (SqlCommand command =
                       new SqlCommand(
                           query,
                           connection))
                {

                    // Name

                    command.Parameters.Add(
                        "@Name",
                        SqlDbType.VarChar,
                        100).Value =
                        txtName.Text.Trim();


                    // Age

                    command.Parameters.Add(
                        "@Age",
                        SqlDbType.Int).Value =
                        Convert.ToInt32(
                            txtAge.Text);


                    // Salary

                    command.Parameters.Add(
                        "@Salary",
                        SqlDbType.Decimal).Value =
                        Convert.ToDecimal(
                            txtSalary.Text);


                    // Employee ID

                    command.Parameters.Add(
                        "@EmpId",
                        SqlDbType.Int).Value =
                        empId;


                    connection.Open();


                    command.ExecuteNonQuery();
                }
            }


            lblMessage.Text =
                "Employee updated successfully.";


            // Clear form

            ClearForm();


            // Clear hidden ID

            hfEmpId.Value = "";


            // Switch Update → Add

            btnAdd.Visible =
                true;

            btnUpdate.Visible =
                false;


            // Refresh GridView

            LoadEmployees();
        }


        // ============================================
        // DELETE EMPLOYEE
        // ============================================

        protected void btnDelete_Command(
            object sender,
            CommandEventArgs e)
        {
            int empId =
                Convert.ToInt32(
                    e.CommandArgument);


            string connectionString =
                ConfigurationManager
                .ConnectionStrings["EmployeeDbConnection"]
                .ConnectionString;


            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();


                // ========================================
                // CHECK DEPARTMENT
                // ========================================

                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Department
                    WHERE Emp_Id = @EmpId";


                using (SqlCommand checkCommand =
                       new SqlCommand(
                           checkQuery,
                           connection))
                {

                    checkCommand.Parameters.Add(
                        "@EmpId",
                        SqlDbType.Int).Value =
                        empId;


                    int departmentCount =
                        Convert.ToInt32(
                            checkCommand.ExecuteScalar());


                    // Department exists

                    if (departmentCount > 0)
                    {
                        lblMessage.Text =
                            "Cannot delete this employee because a department is assigned to this employee. Delete the department first.";

                        return;
                    }
                }


                // ========================================
                // DELETE EMPLOYEE
                // ========================================

                string deleteQuery = @"
                    DELETE FROM Employee
                    WHERE Emp_Id = @EmpId";


                using (SqlCommand deleteCommand =
                       new SqlCommand(
                           deleteQuery,
                           connection))
                {

                    deleteCommand.Parameters.Add(
                        "@EmpId",
                        SqlDbType.Int).Value =
                        empId;


                    deleteCommand.ExecuteNonQuery();
                }
            }


            lblMessage.Text =
                "Employee deleted successfully.";


            // Refresh GridView

            LoadEmployees();
        }


        // ============================================
        // CLEAR FORM
        // ============================================

        private void ClearForm()
        {
            txtName.Text = "";

            txtAge.Text = "";

            txtSalary.Text = "";
        }
    }
}