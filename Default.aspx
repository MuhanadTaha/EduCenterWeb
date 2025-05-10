<%@ Page Title="الصفحة الرئيسية" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EduCenterWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>

    <!-- الرسالة العامة -->
    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-info d-none" EnableViewState="false"></asp:Label>

    <!-- العنوان -->
    <h2 class="text-center mb-4">مرحبًا بكم في الموقع</h2>

    <!-- الدورات -->
    <h3>الدورات المتاحة</h3>
    <asp:GridView ID="CoursesGrid" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowCommand="CoursesGrid_RowCommand">
        <Columns>
            <asp:BoundField DataField="CourseName" HeaderText="اسم الدورة" />
            <asp:BoundField DataField="CenterName" HeaderText="اسم المركز" />
            <asp:BoundField DataField="TeacherName" HeaderText="اسم المدرس" />
            <asp:TemplateField HeaderText="إجراء">
                <ItemTemplate>
                    <asp:Button ID="btnEnroll" runat="server" Text="تسجيل" CommandName="Enroll" CommandArgument='<%# Eval("CourseID") %>' CssClass="btn btn-primary btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


    <div class="w3-content w3-display-container">
        <img class="mySlides" src="Content/slider1.jpg" style="width: 100%">
        <img class="mySlides" src="Content/slider2.jpg" style="width: 100%">
        <img class="mySlides" src="Content/slider3.jpg" style="width: 100%">

        <script>
            var myIndex = 0;
            carousel();

            function carousel() {
                var i;
                var x = document.getElementsByClassName("mySlides");
                for (i = 0; i < x.length; i++) {
                    x[i].style.display = "none";
                }
                myIndex++;
                if (myIndex > x.length) { myIndex = 1 }
                x[myIndex - 1].style.display = "block";
                setTimeout(carousel, 5000); // Change image every 5 seconds
            }
        </script>
    </div>

    <!-- الإعلانات -->
    <h3 class="mt-5 mb-4 text-center text-primary">الإعلانات</h3>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
        <asp:Repeater ID="AdvertisementsRepeater" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 shadow-sm border-0">
                        <div class="card-body">
                            <h5 class="card-title text-primary fw-bold">
                                <i class="bi bi-megaphone-fill"></i><%# Eval("Title") %>
                            </h5>
                            <p class="card-text text-secondary"><%# Eval("Content") %></p>
                        </div>
                        <div class="card-footer bg-transparent border-0 text-end">
                            <small class="text-muted"><%# Eval("PostedDate", "{0:yyyy/MM/dd}") %> - إعلان من المركز</small>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
