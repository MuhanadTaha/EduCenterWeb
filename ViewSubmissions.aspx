<%@ Page Title="تسليمات الطالب" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewSubmissions.aspx.cs" Inherits="EduCenterWeb.ViewSubmissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">تسليمات الطالب</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

        <asp:GridView ID="gvSubmissions" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="عنوان التاسك" />
                <asp:BoundField DataField="SubmissionDate" HeaderText="تاريخ التسليم" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:TemplateField HeaderText="رابط الملف">
                    <ItemTemplate>
                        <a href='<%# Eval("FilePath") %>' target="_blank" class="btn btn-sm btn-success">عرض</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
