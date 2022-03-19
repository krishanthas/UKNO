<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/AuthorizationMasterPage.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IFRS.WEB.Views.Login" %>

<%--https://codepen.io/ainalem/pen/EQXjOR--%>

<asp:Content ID="Content1" ContentPlaceHolderID="AuthorizationMasterPageContent" runat="server">
    <script src="../Scripts/anime.min.js"></script>
    <style>
        /*.container.body-content {
            max-width: unset !important;
        }*/

        @font-face {
            font-family: 'Inter UI';
            font-style: normal;
            font-weight: 400;
            src: url("../Fonts/Inter-Regular.woff2?v=3.13") format("woff2"), url("./Fonts/Inter-Regular.woff?v=3.13") format("woff");
        }

        @font-face {
            font-family: 'Inter UI';
            font-style: normal;
            font-weight: 900;
            src: url("../Fonts/Inter-Black.woff2?v=3.13") format("woff2"), url("./Fonts/Inter-Black.woff?v=3.13") format("woff");
        }

        ::selection {
            background: #2d2f36;
        }

        ::-webkit-selection {
            background: #2d2f36;
        }

        ::-moz-selection {
            background: #2d2f36;
        }

        body {
            background: white;
            font-family: "Inter UI", sans-serif;
            margin: 0;
            padding: 20px;
        }

        .page {
            background: #e2e2e5;
            display: flex;
            flex-direction: column;
            height: calc(100% - 40px);
            position: absolute;
            place-content: center;
            width: calc(100% - 40px);
        }

        @media (max-width: 767px) {
            .page {
                height: auto;
                margin-bottom: 20px;
                padding-bottom: 20px;
            }
        }

        .login-container {
            display: flex;
            height: 320px;
            margin: 0 auto;
            width: 640px;
        }

        @media (max-width: 767px) {
            .login-container {
                flex-direction: column;
                height: 630px;
                width: 320px;
            }
        }

        .left {
            background: white;
            height: calc(100% - 40px);
            top: 20px;
            position: relative;
            width: 50%;
        }

        @media (max-width: 767px) {
            .left {
                height: 100%;
                left: 20px;
                width: calc(100% - 40px);
                max-height: 270px;
            }
        }

        .login {
            font-size: 50px;
            font-weight: 900;
            margin: 0px 0px 30px 30px;
        }

        .eula {
            color: #999;
            font-size: 12px;
            line-height: 1.5;
            margin: 10px 40px 6px 10px;
        }

        .right {
            /*background: #474a59;*/
            background: #00a2ea;
            box-shadow: 0px 0px 40px 16px rgba(0, 0, 0, 0.22);
            color: #f1f1f2;
            position: relative;
            width: 50%;
        }

        @media (max-width: 767px) {
            .right {
                flex-shrink: 0;
                height: 100%;
                width: 100%;
                max-height: 350px;
            }
        }

        svg {
            position: absolute;
            width: 320px;
        }

        path {
            fill: none;
            stroke: url(#linearGradient);
            stroke-width: 4;
            stroke-dasharray: 240 1386;
        }

        .form {
            margin: 40px;
            position: absolute;
            width: 75%;
        }

            .form span.RadInput_Default {
                width: 100% !important;
                padding-bottom: 15px;
            }

        .label {
            color: white;
            display: block;
            font-size: 14px;
            height: 16px;
            margin-top: 20px;
            margin-bottom: 5px;
        }

        .input {
            background: transparent;
            border: 0;
            color: black !important;
            background-color: #dadada !important;
            font-size: 20px;
            height: 30px;
            line-height: 30px;
            outline: none !important;
            width: 100%;
        }

        input::-moz-focus-inner {
            border: 0;
        }

        #submit {
            color: #707075;
            margin-top: 40px;
            transition: color 300ms;
        }

            #submit:focus {
                color: #f2f2f2;
            }

            #submit:active {
                color: #d0d0d2;
            }
    </style>
    <div class="page">
        <div class="login-container">
            <div class="left">
                <img src="../Images/logo.jpg" style="width: 50%; margin: 4% 6%;" />
                <img src="../Images/home_img.PNG" style="width: 44%; margin: 0% 0% 0% 8%;" />
                <%--<div class="login">SLFRS</div>--%>
                <div class="eula">Sri Lanka Financial Reporting Standards</div>
                <footer style="font-size: 10px; float: left; margin: 0px 0px 0px 15px;">&copy; Copyright 2020 Pan Asia Bank</footer>
            </div>
            <div class="right">
                <svg viewBox="0 0 320 300">
                    <defs>
                        <linearGradient
                            inkscape:collect="always"
                            id="linearGradient"
                            x1="13"
                            y1="193.49992"
                            x2="307"
                            y2="193.49992"
                            gradientUnits="userSpaceOnUse">
                            <stop
                                style="stop-color: white;"
                                offset="0"
                                id="stop876" />
                            <stop
                                style="stop-color: lightcyan;"
                                offset="1"
                                id="stop878" />
                        </linearGradient>
                    </defs>
                    <path d="m 40,120.00016 239.99984,-3.2e-4 c 0,0 24.99263,0.79932 25.00016,35.00016 0.008,34.20084 -25.00016,35 -25.00016,35 h -239.99984 c 0,-0.0205 -25,4.01348 -25,38.5 0,34.48652 25,38.5 25,38.5 h 215 c 0,0 20,-0.99604 20,-25 0,-24.00396 -20,-25 -20,-25 h -190 c 0,0 -20,1.71033 -20,25 0,24.00396 20,25 20,25 h 168.57143" />
                </svg>
                <div class="form">
                    <telerik:RadLabel ID="LabelUserId" runat="server" Text="User Id" CssClass="label"></telerik:RadLabel>
                    <telerik:RadTextBox ID="TextBoxUserId" runat="server" CssClass="input"></telerik:RadTextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RadDatePickerForecastDateRequiredFieldValidator" runat="server" Display="Dynamic" ControlToValidate="TextBoxUserId" ErrorMessage="User Id cannot be empty" Style="color: red; font-size: small;" CssClass="validators" ValidationGroup="validators"></asp:RequiredFieldValidator>
                    <telerik:RadLabel ID="LabelPassword" runat="server" Text="Password" CssClass="label"></telerik:RadLabel>
                    <telerik:RadTextBox ID="TextBoxPassword" TextMode="Password" runat="server" CssClass="input"></telerik:RadTextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="TextBoxPassword" ErrorMessage="Password cannot be empty" Style="color: red; font-size: small;" CssClass="validators" ValidationGroup="validators"></asp:RequiredFieldValidator>
                    <telerik:RadButton OnClick="ButtonLogin_OnClick" SingleClick="true" SingleClickText="Processing..." UseSubmitBehavior="false" ID="ButtonSubmit" runat="server" Text="Login" ValidationGroup="validators" Style="float: right; margin-top: 10%;"></telerik:RadButton>
                    <telerik:RadLabel ID="LabelError" CssClass="LabelError" runat="server" Text="" Skin='<%#this.Skin%>'></telerik:RadLabel>
                </div>
            </div>
        </div>
    </div>


    <script>
        var current = null;
        document.querySelector('#<%= TextBoxUserId.ClientID %>').addEventListener('focus', function (e) {
            if (current) current.pause();
            current = anime({
                targets: 'path',
                strokeDashoffset: {
                    value: 0,
                    duration: 700,
                    easing: 'easeOutQuart'
                },
                strokeDasharray: {
                    value: '240 1386',
                    duration: 700,
                    easing: 'easeOutQuart'
                }
            });
        });
        document.querySelector('#<%= TextBoxPassword.ClientID %>').addEventListener('focus', function (e) {
            if (current) current.pause();
            current = anime({
                targets: 'path',
                strokeDashoffset: {
                    value: -336,
                    duration: 700,
                    easing: 'easeOutQuart'
                },
                strokeDasharray: {
                    value: '240 1386',
                    duration: 700,
                    easing: 'easeOutQuart'
                }
            });
        });
        document.querySelector('#<%= ButtonSubmit.ClientID %>').addEventListener('focus', function (e) {
            if (current) current.pause();
            current = anime({
                targets: 'path',
                strokeDashoffset: {
                    value: -730,
                    duration: 700,
                    easing: 'easeOutQuart'
                },
                strokeDasharray: {
                    value: '530 1386',
                    duration: 700,
                    easing: 'easeOutQuart'
                }
            });
        });
    </script>

    <%--<telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>' Style="display: none;">
        <Rows>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn Span="12" SpanSm="12" SpanXs="12" Style="text-align: center;">
                        <div class="site-title">
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Sri Lanka Financial Reporting Standards (SLFRS)"></telerik:RadLabel>

                        </div>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="12" SpanSm="12" SpanXs="12" Style="text-align: center;">
                        <div class="login-form-container">
                            <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Skin='<%#this.Skin%>' CssClass="login-form">
                                <Rows>
                                    <telerik:LayoutRow RowType="Generic">
                                        <Columns>
                                            <telerik:LayoutColumn Style="text-align: center;">
                                                <telerik:RadLabel ID="LabelUserId" runat="server" Text="User Id" Skin='<%#this.Skin%>'></telerik:RadLabel>
                                                <telerik:RadTextBox ID="TextBoxUserId" runat="server" Skin='<%#this.Skin%>'></telerik:RadTextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RadDatePickerForecastDateRequiredFieldValidator" runat="server" Display="Dynamic" ControlToValidate="TextBoxUserId" ErrorMessage="User Id cannot be empty" Style="color: red; font-size: small;" CssClass="validators" ValidationGroup="validators"></asp:RequiredFieldValidator>
                                            </telerik:LayoutColumn>
                                            <telerik:LayoutColumn Style="text-align: center;">
                                                <telerik:RadLabel ID="LabelPassword" runat="server" Text="Password" Skin='<%#this.Skin%>'></telerik:RadLabel>
                                                <telerik:RadTextBox ID="TextBoxPassword" TextMode="Password" runat="server" Skin='<%#this.Skin%>'></telerik:RadTextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="TextBoxPassword" ErrorMessage="Password cannot be empty" Style="color: red; font-size: small;" CssClass="validators" ValidationGroup="validators"></asp:RequiredFieldValidator>
                                            </telerik:LayoutColumn>
                                            <telerik:LayoutColumn Style="text-align: center;">
                                                <telerik:RadButton OnClick="ButtonLogin_OnClick" SingleClick="true" SingleClickText="Processing..." UseSubmitBehavior="false" ID="RadButton1" runat="server" Text="Login" ValidationGroup="validators" Skin='<%#this.Skin%>'></telerik:RadButton>
                                            </telerik:LayoutColumn>
                                            <telerik:LayoutColumn Style="text-align: center;">
                                                <telerik:RadLabel ID="LabelError" CssClass="LabelError" runat="server" Text="" Skin='<%#this.Skin%>'></telerik:RadLabel>
                                            </telerik:LayoutColumn>
                                        </Columns>
                                    </telerik:LayoutRow>
                                </Rows>
                            </telerik:RadPageLayout>
                        </div>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>--%>
</asp:Content>
