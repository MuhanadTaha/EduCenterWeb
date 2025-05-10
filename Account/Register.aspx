<%@ Page Title="تسجيل حساب جديد" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EduCenterWeb.Account.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- تضمين Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .register-container {
            max-width: 500px;
            margin: 80px auto;
            padding: 30px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
    </style>

    <div class="register-container">
        <h3 class="text-center mb-4">إنشاء حساب جديد</h3>

        <!-- اسم كامل -->
        <div class="mb-3 text-center">
            <label class="form-label">الاسم الكامل</label>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-select mx-auto" Style="max-width: 300px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server" ControlToValidate="txtFullName"
                ErrorMessage="الاسم مطلوب" ToolTip="" CssClass="text-danger" />
        </div>

        <!-- اسم المستخدم -->
        <div class="mb-3 text-center">
            <label class="form-label">اسم المستخدم</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-select mx-auto" Style="max-width: 300px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                ErrorMessage="اسم المستخدم مطلوب" ToolTip="" CssClass="text-danger" />
        </div>

        <!-- كلمة المرور -->
        <div class="mb-3 text-center">
            <label class="form-label">كلمة المرور</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-select mx-auto" Style="max-width: 300px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="كلمة المرور مطلوبة" ToolTip="" CssClass="text-danger" />
        </div>

        <!-- نوع المستخدم -->
        <div class="mb-3 text-center">
            <label class="form-label">نوع المستخدم</label>
            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select mx-auto" Style="max-width: 300px">
                <asp:ListItem Text="طالب" Value="Student" />
                <asp:ListItem Text="معلم" Value="Teacher" />
            </asp:DropDownList>
        </div>

        <%--<!-- المركز التعليمي -->
        <div class="mb-3 text-center">
            <label class="form-label">المركز التعليمي</label>
            <asp:DropDownList ID="ddlCenters" runat="server" CssClass="form-select mx-auto" Style="max-width: 300px" class="text-center" />
        </div>--%>

        <!-- زر التسجيل -->
        <div class="mb-3 text-center">
            <asp:Button ID="btnRegister" runat="server" Text="تسجيل" CssClass="btn btn-success w-75" OnClick="btnRegister_Click" />
        </div>

        <!-- رسائل الخطأ -->
        <div class="mb-3 text-center">
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block mt-3 text-center" />
        </div>
    </div>

</asp:Content>
