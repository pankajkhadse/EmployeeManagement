<%@ Page Title="Departments"
Language="C#"
MasterPageFile="~/Site.Master"
AutoEventWireup="true"
CodeBehind="Department.aspx.cs"
Inherits="EmployeeManagement.Department" %>

<asp:Content
ID="Content1"
ContentPlaceHolderID="MainContent"
runat="server">

<div class="management-container">


    <div class="page-header">

        <h1>
            <i class="bi bi-building me-2"></i>
            Department Management
        </h1>

        <p>
            Assign and manage departments for your employees.
        </p>

    </div>



    <div class="form-card">

        <h3>
            Department Details
        </h3>


        
        <asp:HiddenField
            ID="hfDepartmentEmpId"
            runat="server" />



        <div class="mb-3">

            <asp:Label
                ID="lblEmployee"
                runat="server"
                Text="Employee"
                CssClass="form-label-custom">
            </asp:Label>


            <asp:DropDownList
                ID="ddlEmployee"
                runat="server"
                CssClass="form-select">

                <asp:ListItem
                    Text="-- Select Employee --"
                    Value="">
                </asp:ListItem>

            </asp:DropDownList>


            <asp:RequiredFieldValidator
                ID="rfvEmployee"
                runat="server"
                ControlToValidate="ddlEmployee"
                InitialValue=""
                ErrorMessage="Please select an employee."
                CssClass="text-danger"
                Display="Dynamic">
            </asp:RequiredFieldValidator>

        </div>


      

        <div class="mb-3">

            <asp:Label
                ID="lblDptName"
                runat="server"
                Text="Department Name"
                CssClass="form-label-custom">
            </asp:Label>


            <asp:TextBox
                ID="txtDptName"
                runat="server"
                CssClass="form-input">
            </asp:TextBox>


            <asp:RequiredFieldValidator
                ID="rfvDptName"
                runat="server"
                ControlToValidate="txtDptName"
                ErrorMessage="Department name is required."
                CssClass="text-danger"
                Display="Dynamic">
            </asp:RequiredFieldValidator>


            <asp:RegularExpressionValidator
                ID="revDptName"
                runat="server"
                ControlToValidate="txtDptName"
                ValidationExpression="^[A-Za-z ]+$"
                ErrorMessage="Department name should contain letters and spaces only."
                CssClass="text-danger"
                Display="Dynamic">
            </asp:RegularExpressionValidator>

        </div>


        

        <asp:ValidationSummary
            ID="vsDepartment"
            runat="server"
            CssClass="text-danger mb-3"
            HeaderText="Please fix the following errors:" />


     

        <asp:Button
            ID="btnAdd"
            runat="server"
            Text="Add Department"
            CssClass="btn btn-primary"
            OnClick="btnAdd_Click" />


      

        <asp:Button
            ID="btnUpdate"
            runat="server"
            Text="Update Department"
            CssClass="btn btn-success"
            OnClick="btnUpdate_Click"
            Visible="false" />


      

        <asp:Button
            ID="btnCancel"
            runat="server"
            Text="Cancel"
            CssClass="btn btn-secondary ms-2"
            OnClick="btnCancel_Click"
            CausesValidation="false"
            Visible="false" />


        

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="d-block mt-3">
        </asp:Label>

    </div>


  

    <div class="table-card">

        <h3>

            <i class="bi bi-table me-2"></i>

            Department Records

        </h3>


        <div class="table-responsive">

            <asp:GridView
                ID="gvDepartments"
                runat="server"
                CssClass="data-grid"
                AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField
                        DataField="Emp_Id"
                        HeaderText="Employee ID" />


                    <asp:BoundField
                        DataField="Emp_Name"
                        HeaderText="Employee Name" />


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
                                CommandArgument='<%# Eval("Emp_Id") %>'
                                OnCommand="btnEdit_Command"
                                CausesValidation="false"
                                CssClass="btn btn-sm btn-outline-primary me-1" />


                            <asp:Button
                                ID="btnDelete"
                                runat="server"
                                Text="Delete"
                                CommandArgument='<%# Eval("Emp_Id") %>'
                                OnCommand="btnDelete_Command"
                                CausesValidation="false"
                                OnClientClick="return confirm('Are you sure you want to delete this department?');"
                                CssClass="btn btn-sm btn-outline-danger" />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>


   
    <div class="text-center mt-4">

        <a href="Default.aspx"
           class="btn btn-outline-secondary">

            <i class="bi bi-arrow-left me-1"></i>

            Dashboard

        </a>


        <a href="Employee.aspx"
           class="btn btn-outline-primary ms-2">

            <i class="bi bi-people me-1"></i>

            Employee Management

        </a>

    </div>

</div>


</asp:Content>
