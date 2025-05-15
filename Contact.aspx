<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="EduCenterWeb.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" integrity="sha512-..." crossorigin="anonymous" referrerpolicy="no-referrer" />

    <main aria-labelledby="title" dir="rtl">
        <main class="container mt-5 mb-5">
            <div class="row justify-content-center">
                <div class="col-md-8">
                    <div class="card shadow-sm border-0">
                        <div class="card-body">
                            <h2 class="card-title text-center text-primary mb-4">اتصل بنا</h2>

                        </div>

                        <div class="mb-3">
                            <h5 class="text-dark">📍 العنوان:</h5>
                            <p>الضفة الغربية، فلسطين<br />شارع الجامعة</p>
                        </div>

                        <div class="mb-3">
                            <h5 class="text-dark"> واتساب:</h5>
                            <p>
                                <a href="https://wa.me/972599032517" class="btn btn-success" target="_blank">تواصل عبر واتساب<i class="fab fa-whatsapp me-2"></i>
                                </a>
                            </p>
                        </div>

                        <div class="mb-3">
                            <h5 class="text-dark"> فيسبوك:</h5>
                            <p>
                                <a href="https://www.facebook.com/share/19dqEJCHxp/" class="btn btn-primary" target="_blank">زيارة صفحتنا على فيسبوك <i class="fab fa-facebook-f me-2"></i>
                                </a>
                            </p>
                        </div>



                    </div>
                </div>
            </div>

        </main>
</asp:Content>
