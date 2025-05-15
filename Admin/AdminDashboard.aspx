<%@ Page Title="لوحة تحكم المدير" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="EduCenterWeb.Admin.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- إضافة ملفات JavaScript الخاصة بـ Bootstrap -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

    <div class="container mt-5">
        <h2 class="text-center mb-4">لوحة تحكم المدير</h2>

        <!-- تبويبات Bootstrap -->
        <ul class="nav nav-tabs mb-4" id="adminTabs" role="tablist">
            <li class="nav-item">
                <a class="nav-link active" id="centers-tab" data-toggle="tab" href="#centers" role="tab">المراكز</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="courses-tab" data-toggle="tab" href="#courses" role="tab">الدورات</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="teachers-tab" data-toggle="tab" href="#teachers" role="tab">المعلمين</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="students-tab" data-toggle="tab" href="#students" role="tab">الطلاب</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="ads-tab" data-toggle="tab" href="#ads" role="tab">الإعلانات</a>
            </li>
        </ul>

        <div class="tab-content" id="adminTabsContent">
            <!-- المراكز -->
            <div class="tab-pane fade show active" id="centers" role="tabpanel">
                <h4>إدارة المراكز</h4>

                <asp:GridView ID="CentersGrid" runat="server" CssClass="table table-bordered"
                    AutoGenerateColumns="False" DataKeyNames="CenterId"
                    OnRowEditing="CentersGrid_RowEditing"
                    OnRowUpdating="CentersGrid_RowUpdating"
                    OnRowCancelingEdit="CentersGrid_RowCancelingEdit"
                    OnRowDeleting="CentersGrid_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="CenterId" HeaderText="الرقم" ReadOnly="True" />
                        <asp:BoundField DataField="CenterName" HeaderText="اسم المركز" />
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>

                <div class="form-inline mt-3">
                    <asp:TextBox ID="NewCenterName" runat="server" CssClass="form-control mr-2" Placeholder="اسم مركز جديد" ></asp:TextBox>
                    
                    <asp:Button ID="AddCenterBtn" runat="server" Text="إضافة" CssClass="btn btn-success" OnClick="AddCenterBtn_Click" />
                </div>
            </div>

            <div class="tab-pane fade" id="courses" role="tabpanel">
                <h4>إدارة الدورات</h4>
                <asp:GridView ID="CoursesGrid" runat="server" CssClass="table table-bordered"
                    AutoGenerateColumns="False" DataKeyNames="CourseID"
                    OnRowEditing="CoursesGrid_RowEditing"
                 
                    OnRowCancelingEdit="CoursesGrid_RowCancelingEdit"
                    OnRowDeleting="CoursesGrid_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="CourseID" HeaderText="الرقم" ReadOnly="True" />
                        <asp:BoundField DataField="CourseName" HeaderText="اسم الدورة" />
                        <asp:BoundField DataField="TeacherName" HeaderText="اسم المعلم" />
                        <asp:BoundField DataField="CenterName" HeaderText="اسم المركز" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>

                <div class="form-inline mt-3">
                    <asp:TextBox ID="NewCourseName" runat="server" CssClass="form-control mr-2" Placeholder="اسم الدورة الجديدة"></asp:TextBox>
                    <asp:DropDownList ID="NewCourseTeacher" runat="server" CssClass="form-control mr-2">
                    </asp:DropDownList>
                    <asp:DropDownList ID="NewCourseCenter" runat="server" CssClass="form-control mr-2">
                    </asp:DropDownList>
                    <asp:Button ID="AddCourseBtn" runat="server" Text="إضافة" CssClass="btn btn-success" OnClick="AddCourseBtn_Click" />
                </div>
            </div>

            <div class="tab-pane fade" id="teachers" role="tabpanel">
                <h4>إدارة المعلمين</h4>

                <asp:GridView ID="TeachersGrid" runat="server" CssClass="table table-bordered"
                    AutoGenerateColumns="False" DataKeyNames="UserID"
                    OnRowDeleting="TeachersGrid_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="رقم المعلم" ReadOnly="True" />
                        <asp:BoundField DataField="FullName" HeaderText="اسم المعلم" />
                        <asp:BoundField DataField="Username" HeaderText="اسم المستخدم" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>

            </div>


            <!-- الأقسام الأخرى -->



            <div class="tab-pane fade" id="students" role="tabpanel">
                <h4>قائمة الطلاب</h4>

                <asp:GridView ID="StudentsGrid" runat="server" CssClass="table table-bordered"
                    AutoGenerateColumns="False" DataKeyNames="UserID"
                    OnRowDeleting="StudentsGrid_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="رقم الطالب" ReadOnly="True" />
                        <asp:BoundField DataField="FullName" HeaderText="الاسم الكامل" />
                        <asp:BoundField DataField="Username" HeaderText="اسم المستخدم" />

                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>


            <div class="tab-pane fade" id="ads" role="tabpanel">
                <h4>إدارة الإعلانات</h4>

                <asp:GridView ID="AdsGrid" runat="server" CssClass="table table-bordered"
                    AutoGenerateColumns="False" DataKeyNames="AnnouncementID"
                    OnRowDeleting="AdsGrid_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="AnnouncementID" HeaderText="الرقم" ReadOnly="True" />
                        <asp:BoundField DataField="Title" HeaderText="العنوان" />
                        <asp:BoundField DataField="Content" HeaderText="المحتوى" />
                        <asp:BoundField DataField="PostedDate" HeaderText="تاريخ النشر" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="CenterName" HeaderText="اسم المركز" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>

                <div class="form-inline mt-3">
                    <asp:TextBox ID="NewAdTitle" runat="server" CssClass="form-control mr-2" Placeholder="عنوان الإعلان"></asp:TextBox>
                    <asp:TextBox ID="NewAdContent" runat="server" CssClass="form-control mr-2" Placeholder="محتوى الإعلان"></asp:TextBox>
                    <asp:DropDownList ID="AdCenterDropDown" runat="server" CssClass="form-control mr-2"></asp:DropDownList>
                    <asp:Button ID="AddAdBtn" runat="server" Text="إضافة إعلان" CssClass="btn btn-success" OnClick="AddAdBtn_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
