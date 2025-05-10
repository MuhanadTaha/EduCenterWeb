<%@ Page Title="تسليم التاسك" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmitAssignment.aspx.cs" Inherits="EduCenterWeb.SubmitAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">تسليم التاسك</h2>

        <div class="row justify-content-center">
            <div class="col-md-6">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

                <asp:FileUpload ID="fuAssignmentFile" runat="server" CssClass="form-control mb-3" />
                
                <asp:Button ID="btnSubmit" runat="server" Text="رفع الملف" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div> 
</asp:Content>
