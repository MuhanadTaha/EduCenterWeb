<%@ Page Title="إضافة تاسك" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAssignment.aspx.cs" Inherits="EduCenterWeb.AddAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">إضافة تاسك جديد للدورة</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

        <div class="form-group">
            <label for="txtTitle">عنوان التاسك:</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtDescription">الوصف:</label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtDueDate">تاريخ الاستحقاق:</label>
            <asp:TextBox ID="txtDueDate" runat="server" TextMode="Date" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="fuAssignmentFile">رفع الملف:</label>
            <asp:FileUpload ID="fuAssignmentFile" runat="server" CssClass="form-control" />
        </div>

        <asp:Button ID="btnAddAssignment" runat="server" Text="إضافة" CssClass="btn btn-primary mt-3" OnClick="btnAddAssignment_Click" />
    </div>
</asp:Content>
