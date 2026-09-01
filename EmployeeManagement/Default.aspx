<%@ Page Title="Dashboard"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="EmployeeManagement._Default" %>


<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="dashboard-wrapper">

        <!-- HERO -->

        <div class="dashboard-hero">

            <h1>
                Welcome to Employee Management
            </h1>

            <p>
                Manage employees and departments
                from one simple dashboard.
            </p>

        </div>


        <!-- MANAGEMENT CARDS -->

        <div class="row g-4">


            <!-- EMPLOYEE CARD -->

            <div class="col-md-6">

                <div class="management-card">

                    <div class="card-icon employee-icon">

                        <i class="bi bi-person-badge"></i>

                    </div>

                    <h3>
                        Employees
                    </h3>

                    <p>
                        Add, update, view and delete
                        employee records.
                    </p>

                    <a href="Employee.aspx"
                       class="btn btn-primary">

                        Manage Employees
                        <i class="bi bi-arrow-right ms-1"></i>

                    </a>

                </div>

            </div>


            <!-- DEPARTMENT CARD -->

            <div class="col-md-6">

                <div class="management-card">

                    <div class="card-icon department-icon">

                        <i class="bi bi-building"></i>

                    </div>

                    <h3>
                        Departments
                    </h3>

                    <p>
                        Assign and manage departments
                        for your employees.
                    </p>

                    <a href="Department.aspx"
                       class="btn btn-success">

                        Manage Departments
                        <i class="bi bi-arrow-right ms-1"></i>

                    </a>

                </div>

            </div>

        </div>


        <!-- INFORMATION -->

        <div class="table-card mt-4">

            <h3>
                <i class="bi bi-info-circle me-2"></i>
                System Overview
            </h3>

            <p class="text-muted mb-0">

                Use the navigation menu above or the
                management cards to manage employees
                and their department assignments.

            </p>

        </div>

    </div>

</asp:Content>