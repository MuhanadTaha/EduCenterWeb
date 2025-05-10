<%@ Page Title="لوحة تحكم المعلم" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherDashboard.aspx.cs" Inherits="EduCenterWeb.Teacher.TeacherDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblWelcome" runat="server" CssClass="h5 text-primary d-block text-center mb-4"></asp:Label>

    <div class="container mt-5">
        <h2 class="text-center mb-4">لوحة تحكم المعلم</h2>

        <asp:GridView ID="CoursesGrid" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="CourseID">
            <Columns>
                <asp:BoundField DataField="CourseName" HeaderText="اسم الدورة" />
                <asp:BoundField DataField="CenterName" HeaderText="اسم المركز" />
                <asp:TemplateField HeaderText="عدد الطلاب">
                    <ItemTemplate>
                        <asp:Label ID="StudentCountLabel" runat="server" Text='<%# Eval("StudentCount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="تفاصيل">
                    <ItemTemplate>
                        <asp:HyperLink ID="DetailsLink" runat="server" NavigateUrl='<%# "~/CourseDetails.aspx?courseId=" + Eval("CourseID") %>' Text="عرض الطلاب" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="إضافة تاسك">
                    <ItemTemplate>
                        <asp:HyperLink ID="AddAssignmentLink" runat="server"
                            NavigateUrl='<%# "~/AddAssignment.aspx?CourseID=" + Eval("CourseID") %>'
                            CssClass="btn btn-sm btn-success"
                            Text="إضافة تاسك" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
