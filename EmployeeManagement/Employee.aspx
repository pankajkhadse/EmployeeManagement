<%@ Page Title="Employees"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Employee.aspx.cs"
    Inherits="EmployeeManagement.Employee" %>


<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="management-container">


        <!-- PAGE HEADER -->

        <div class="page-header">

            <h1>
                <i class="bi bi-person-badge me-2"></i>
                Employee Management
            </h1>

            <p>
                Add, update, view and manage employee records.
            </p>

        </div>


        <!-- EMPLOYEE FORM -->

        <div class="form-card">

            <h3>
                Employee Details
            </h3>


            <asp:HiddenField
                ID="hfEmpId"
                runat="server" />


            <!-- NAME -->

            <div class="mb-3">

                <asp:Label
                    ID="lblName"
                    runat="server"
                    Text="Employee Name"
                    CssClass="form-label-custom">
                </asp:Label>

                <asp:TextBox
                    ID="txtName"
                    runat="server"
                    CssClass="form-input">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvName"
                    runat="server"
                    ControlToValidate="txtName"
                    ErrorMessage="Employee name is required."
                    CssClass="text-danger"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revName"
                    runat="server"
                    ControlToValidate="txtName"
                    ValidationExpression="^(?=.*[A-Za-z])[A-Za-z0-9 ]+$"
                    ErrorMessage="Name must contain at least one letter and can contain letters, numbers, and spaces."
                    CssClass="text-danger"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>

            </div>


            <!-- AGE -->

            <div class="mb-3">

                <asp:Label
                    ID="lblAge"
                    runat="server"
                    Text="Employee Age"
                    CssClass="form-label-custom">
                </asp:Label>

                <asp:TextBox
                    ID="txtAge"
                    runat="server"
                    TextMode="Number"
                    CssClass="form-input">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvAge"
                    runat="server"
                    ControlToValidate="txtAge"
                    ErrorMessage="Employee age is required."
                    CssClass="text-danger"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>

                <asp:RangeValidator
                    ID="rvAge"
                    runat="server"
                    ControlToValidate="txtAge"
                    MinimumValue="18"
                    MaximumValue="100"
                    Type="Integer"
                    ErrorMessage="Age must be between 18 and 100."
                    CssClass="text-danger"
                    Display="Dynamic">
                </asp:RangeValidator>

            </div>


            <!-- SALARY -->

            <div class="mb-3">

                <asp:Label
                    ID="lblSalary"
                    runat="server"
                    Text="Employee Salary"
                    CssClass="form-label-custom">
                </asp:Label>

                <asp:TextBox
                    ID="txtSalary"
                    runat="server"
                    TextMode="Number"
                    CssClass="form-input">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvSalary"
                    runat="server"
                    ControlToValidate="txtSalary"
                    ErrorMessage="Employee salary is required."
                    CssClass="text-danger"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>

                <asp:CompareValidator
                    ID="cvSalary"
                    runat="server"
                    ControlToValidate="txtSalary"
                    Operator="DataTypeCheck"
                    Type="Double"
                    ErrorMessage="Salary must be a valid number."
                    CssClass="text-danger"
                    Display="Dynamic">
                </asp:CompareValidator>

            </div>


            <asp:ValidationSummary
                ID="vsEmployee"
                runat="server"
                CssClass="text-danger mb-3"
                HeaderText="Please fix the following errors:" />


            <!-- ADD -->

            <asp:Button
                ID="btnAdd"
                runat="server"
                Text="Add Employee"
                CssClass="btn btn-primary"
                OnClick="btnAdd_Click" />


            <!-- UPDATE -->

            <asp:Button
                ID="btnUpdate"
                runat="server"
                Text="Update Employee"
                CssClass="btn btn-success"
                OnClick="btnUpdate_Click"
                Visible="false" />


            <!-- MESSAGE -->

            <asp:Label
                ID="lblMessage"
                runat="server">
            </asp:Label>

        </div>


        <!-- EMPLOYEE GRID -->

        <div class="table-card">

            <div class="d-flex justify-content-between
                        align-items-center mb-3">

                <h3 class="mb-0">

                    <i class="bi bi-table me-2"></i>
                    Employee Records

                </h3>

            </div>


            <div class="table-responsive">

                <asp:GridView
                    ID="gvEmployees"
                    runat="server"
                    CssClass="data-grid"
                    AutoGenerateColumns="false"
                    DataKeyNames="Emp_Id">

                    <Columns>

                        <asp:BoundField
                            DataField="Emp_Id"
                            HeaderText="ID" />

                        <asp:BoundField
                            DataField="Emp_Name"
                            HeaderText="Employee Name" />

                        <asp:BoundField
                            DataField="Emp_Age"
                            HeaderText="Age" />

                        <asp:BoundField
                            DataField="Emp_Salary"
                            HeaderText="Salary" />

                        <asp:BoundField
                            DataField="Dpt_Name"
                            HeaderText="Department" />

                        <asp:TemplateField
                            HeaderText="Actions">

                            <ItemTemplate>

                                <asp:Button
                                    ID="btnEdit"
                                    runat="server"
                                    Text="Edit"
                                    CommandName="EditEmployee"
                                    CommandArgument='<%# Eval("Emp_Id") %>'
                                    OnCommand="btnEdit_Command"
                                    CausesValidation="false"
                                    CssClass="btn btn-sm btn-outline-primary me-1" />


                                <asp:Button
                                    ID="btnDelete"
                                    runat="server"
                                    Text="Delete"
                                    CommandName="DeleteEmployee"
                                    CommandArgument='<%# Eval("Emp_Id") %>'
                                    OnCommand="btnDelete_Command"
                                    CausesValidation="false"
                                    OnClientClick="return confirm('Are you sure you want to delete this employee?');"
                                    CssClass="btn btn-sm btn-outline-danger" />

                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>


        <!-- BACK TO DASHBOARD -->

        <div class="text-center mt-4">

            <a href="Default.aspx"
               class="btn btn-outline-secondary">

                <i class="bi bi-arrow-left me-1"></i>
                Back to Dashboard

            </a>

        </div>

    </div>

</asp:Content>