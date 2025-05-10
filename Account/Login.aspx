<%@ Page Title="تسجيل الدخول" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EduCenterWeb.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card mt-5">
                    <div class="card-body">
                        <h3 class="text-center">تسجيل الدخول</h3>

                        <!-- اسم المستخدم -->
                        <div class="mb-3 text-center">
                            <label for="txtUsername" class="form-label d-block">اسم المستخدم</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control mx-auto" style="max-width: 300px;" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="txtUsername"
                                    ErrorMessage="اسم المستخدم مطلوب" ToolTip="" CssClass="text-danger" />
                        </div>

                        <!-- كلمة المرور -->
                        <div class="mb-3 text-center">
                            <label for="txtPassword" class="form-label d-block">كلمة المرور</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mx-auto" style="max-width: 300px;" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="كلمة المرور مطلوبة" ToolTip="" CssClass="text-danger" />
                        </div>

                        <!-- زر تسجيل الدخول -->
                        <div class="mb-3 text-center">
                            <asp:Button ID="btnLogin" runat="server" Text="تسجيل الدخول" CssClass="btn btn-primary w-75" OnClick="btnLogin_Click" />
                        </div>

                        <!-- رسالة الخطأ -->
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block mt-3 text-center" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
