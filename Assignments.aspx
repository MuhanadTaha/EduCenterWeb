<%@ Page Title="عرض التاسكات" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="EduCenterWeb.Assignments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">التاسكات الخاصة بالدورة</h2>

        <div class="row">
            <!-- جدول عرض التاسكات الخاصة بالدورة -->
            <div class="col-12">
                <asp:GridView ID="gvAssignments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="عنوان التاسك" SortExpression="Title" />
                        <asp:BoundField DataField="DueDate" HeaderText="تاريخ الاستحقاق" SortExpression="DueDate" DataFormatString="{0:dd/MM/yyyy}" />

                       
                        <asp:TemplateField HeaderText="ملف التاسك">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkFile" runat="server"
                                    Text="تحميل الملف"
                                    NavigateUrl='<%# Eval("AssignmentFile", "{0}") %>'
                                    Target="_blank"
                                    Visible='<%# !string.IsNullOrEmpty(Eval("AssignmentFile").ToString()) %>'
                                    CssClass="btn btn-info btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>

                     
                        <asp:TemplateField HeaderText="رفع التاسك">
                            <ItemTemplate>
                                <asp:Button ID="btnSubmitTask" runat="server" Text="رفع التاسك" CommandArgument='<%# Eval("AssignmentID") %>' OnClick="SubmitTask_Click" CssClass="btn btn-success" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>



            </div>
        </div>
    </div>
</asp:Content>
