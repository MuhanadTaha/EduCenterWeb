<%@ Page Title="تفاصيل الدورة" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="EduCenterWeb.CourseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center mb-4">عرض الطلاب المسجلين في الدورة</h2>

    <!-- عرض الرسائل -->
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-3"></asp:Label>

    <asp:GridView ID="StudentsGrid" runat="server" CssClass="table table-bordered"
        AutoGenerateColumns="False" OnRowCommand="StudentsGrid_RowCommand">
        <Columns>
            <asp:BoundField DataField="FullName" HeaderText="اسم الطالب" SortExpression="FullName" />
            <asp:BoundField DataField="Username" HeaderText="اسم المستخدم" SortExpression="Username" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnViewSubmissions" runat="server" Text="عرض التسليمات"
                        CommandName="ViewSubmissions" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-info" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
