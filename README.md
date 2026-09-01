# Employee Management System

A responsive **ASP.NET Web Forms** application for managing employees and their departments using **C#, ADO.NET, and SQL Server**.

The application provides complete CRUD operations for employees and departments, validation, department assignment, and a dashboard for easy navigation.

---

## 🚀 Features

### Dashboard

* Central dashboard for the application
* Navigation to Employee Management
* Navigation to Department Management
* Responsive and user-friendly interface

### Employee Management

* Add new employees
* View employee records
* Edit employee details
* Delete employees
* Employee name, age, and salary validation
* Displays the assigned department
* Shows **"Department Not Assigned"** when an employee has no department
* Prevents deletion of employees who have an assigned department

### Department Management

* Assign a department to an employee
* View department records
* Edit department details
* Delete departments
* Prevents assigning multiple departments to the same employee
* Only employees without a department are available for new department assignment
* Validates employee existence before department assignment

### Database

* SQL Server database
* Primary key and foreign key relationships
* Parameterized SQL queries
* ADO.NET for database connectivity
* Employee and Department tables

### UI

* Responsive design
* Bootstrap-based styling
* Clean navigation
* Responsive tables and forms
* User-friendly validation and messages

---

## 🛠️ Technologies Used

| Technology         | Purpose                   |
| ------------------ | ------------------------- |
| ASP.NET Web Forms  | Web application framework |
| C#                 | Backend programming       |
| .NET Framework 4.8 | Application framework     |
| ADO.NET            | Database connectivity     |
| SQL Server         | Database                  |
| HTML5              | Page structure            |
| CSS3               | Styling                   |
| Bootstrap          | Responsive UI             |
| Visual Studio      | Development environment   |

---

## 📂 Project Structure

```text
EmployeeManagement
│
├── App_Start
│   └── RouteConfig.cs
│
├── Content
│   └── Site.css
│
├── Default.aspx
├── Default.aspx.cs
│
├── Employee.aspx
├── Employee.aspx.cs
│
├── Department.aspx
├── Department.aspx.cs
│
├── Site.Master
├── Site.Master.cs
│
├── Global.asax
├── Global.asax.cs
│
├── Web.config
│
├── EmployeeManagement.sln
├── EmployeeManagement.csproj
│
└── README.md
```

---

## 🗄️ Database Structure

The application uses a SQL Server database named:

```text
EmployeeManagementDB
```

### Employee Table

```sql
CREATE TABLE Employee
(
    Emp_Id INT PRIMARY KEY IDENTITY(1,1),
    Emp_Name VARCHAR(100) NOT NULL,
    Emp_Age INT NOT NULL,
    Emp_Salary DECIMAL(18,2) NOT NULL
);
```

### Department Table

```sql
CREATE TABLE Department
(
    Emp_Id INT PRIMARY KEY,
    Dpt_Name VARCHAR(100) NOT NULL,

    CONSTRAINT FK_Department_Employee
        FOREIGN KEY (Emp_Id)
        REFERENCES Employee(Emp_Id)
);
```

### Relationship

```text
Employee
   |
   | Emp_Id
   ↓
Department
```

`Department.Emp_Id` is a foreign key referencing `Employee.Emp_Id`.

This ensures that a department can only be assigned to an existing employee.

---

## 🔌 Database Connection

The application uses the connection string configured in `Web.config`.

Example:

```xml
<connectionStrings>
    <add name="EmployeeDbConnection"
         connectionString="Data Source=YOUR_SERVER;Initial Catalog=EmployeeManagementDB;Integrated Security=True"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

Replace:

```text
YOUR_SERVER
```

with your SQL Server instance.

For example:

```text
.\SQLEXPRESS
```

or:

```text
(localdb)\MSSQLLocalDB
```

depending on your SQL Server installation.

---

## 🔄 Application Flow

```text
                    Dashboard
                  Default.aspx
                       |
              ┌────────┴────────┐
              ↓                 ↓
       Employee Management  Department Management
              |                 |
              ↓                 ↓
        Employee CRUD      Department CRUD
              |                 |
              └────────┬────────┘
                       ↓
                  SQL Server
                       |
             EmployeeManagementDB
```

---

## 👨‍💼 Employee CRUD

The Employee Management page supports:

```text
CREATE
  ↓
Add Employee

READ
  ↓
View Employees

UPDATE
  ↓
Edit Employee

DELETE
  ↓
Delete Employee
```

The employee list also displays the employee's department using a SQL `LEFT JOIN`.

Employees without a department are displayed as:

```text
Department Not Assigned
```

---

## 🏢 Department CRUD

Department Management supports:

```text
CREATE
  ↓
Assign Department

READ
  ↓
View Departments

UPDATE
  ↓
Edit Department

DELETE
  ↓
Delete Department
```

When adding a department, only employees who **do not already have a department** are available for selection.

This prevents duplicate department assignments.

---

## 🔐 Data Validation

The application performs validation for:

* Required employee name
* Required employee age
* Required employee salary
* Employee age format/range
* Salary format
* Required employee selection
* Required department name
* Valid employee ID
* Duplicate department assignment

ASP.NET Web Forms validation controls are used for client/server-side validation.

---

## 🛡️ Database Safety

SQL queries use **parameterized commands** instead of directly concatenating user input.

Example:

```csharp
string query = @"
    INSERT INTO Employee
    (Emp_Name, Emp_Age, Emp_Salary)
    VALUES
    (@Name, @Age, @Salary)";
```

Parameters are supplied separately:

```csharp
command.Parameters.AddWithValue("@Name", txtName.Text);
command.Parameters.AddWithValue("@Age", Convert.ToInt32(txtAge.Text));
command.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text));
```

This helps protect the application against SQL injection.

---

## ▶️ How to Run the Project

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/EmployeeManagement.git
```

### 2. Open the project

Open:

```text
EmployeeManagement.sln
```

using Visual Studio.

### 3. Configure SQL Server

Create the database:

```text
EmployeeManagementDB
```

and create the required tables using the SQL scripts above.

### 4. Configure the connection string

Open:

```text
Web.config
```

Update:

```xml
connectionString="Data Source=YOUR_SERVER;Initial Catalog=EmployeeManagementDB;Integrated Security=True"
```

with your SQL Server instance.

### 5. Build the project

In Visual Studio:

```text
Build → Build Solution
```

### 6. Run the application

Press:

```text
Ctrl + F5
```

or:

```text
F5
```

The application opens on the Dashboard.

---

## 📌 Important Notes

* This project uses **ASP.NET Web Forms**, not ASP.NET Core.
* SQL Server is required to run the application.
* The `.vs`, `bin`, and `obj` folders are excluded from Git using `.gitignore`.
* The database itself is not included in the repository.
* Configure your local SQL Server connection string before running the project.

---

## 🎯 Learning Objectives

This project demonstrates practical implementation of:

* ASP.NET Web Forms
* C# server-side programming
* ADO.NET
* SQL Server connectivity
* CRUD operations
* SQL `JOIN`
* Primary and foreign keys
* Parameterized SQL queries
* GridView
* Web Forms validation controls
* Master Pages
* Bootstrap responsive design
* Basic routing/friendly URLs
* Database constraint handling

---

## 📄 License

This project was developed by pankaj khadse for educational and assignment purposes.
