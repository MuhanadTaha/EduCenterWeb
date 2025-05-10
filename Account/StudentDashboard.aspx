<%@ Page Title="لوحة تحكم الطالب" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="EduCenterWeb.Student.StudentDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblWelcome" runat="server" CssClass="h5 text-primary d-block text-center mb-4"></asp:Label>
    <div class="container mt-5">
        <h2 class="text-center mb-4">مرحباً بك في لوحة تحكم الطالب</h2>

        <div class="row">
            <!-- جدول الدورات المسجل فيها الطالب -->
            <div class="col-12">
                <h4>الدورات المسجل فيها الطالب</h4>
                <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                        <asp:BoundField DataField="CourseName" HeaderText="اسم الدورة" SortExpression="CourseName" />
                        <asp:TemplateField HeaderText="عرض التاسكات">
                            <ItemTemplate>
                                <asp:Button ID="btnViewAssignments" runat="server" Text="عرض التاسكات"
                                    CommandArgument='<%# Eval("CourseID") %>'
                                    OnClick="ViewAssignments_Click"
                                    CssClass="btn btn-info" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
