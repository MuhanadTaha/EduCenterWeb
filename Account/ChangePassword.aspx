<%@ Page Title="تغيير كلمة المرور" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="EduCenterWeb.Account.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="card p-4">
            <h3 class="mb-3">تغيير كلمة المرور</h3>

            <asp:Label ID="lblMessage" runat="server" CssClass="d-block mb-3"></asp:Label>

            <div class="mb-3">
                <label class="form-label">كلمة المرور الحالية:</label>
                <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">كلمة المرور الجديدة:</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">تأكيد كلمة المرور الجديدة:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="text-center">
                <asp:Button ID="btnUpdate" runat="server" Text="تحديث كلمة المرور" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>
