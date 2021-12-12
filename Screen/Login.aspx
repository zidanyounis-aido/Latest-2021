<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="dms.Screen.Login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>شركة اشراق الريادة</title>
    <link href="../assets/<%= dms.sysSettings.getSettingValue("client") %>/css/theme.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/loginCSS.css" rel="stylesheet" type="text/css" />
    <link href="../fonts/fontawesome/css/all.min.css" rel="stylesheet" />
    <style>
        .username {
            background: url(../images/usermamefield.gif) no-repeat top;
            width: 222px;
            height: 32px;
        }

        .g-recaptcha div {
            width: 100% !important;
        }
    </style>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
    </style>
    <script src="<%= "https://www.google.com/recaptcha/api.js?hl=" + GetrecaptchaLanguage() %>"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style>
        /* general style */
        .loader-holder {
            position: absolute;
            left: 0;
            right: 0;
            bottom: 0;
            top: 0;
            z-index: 999999999;
            background: rgb(255 255 255 / 53%);
        }

            .loader-holder img {
                width: 100px;
                position: fixed;
                top: 50%;
                right: 50%;
                transform: translate(50%, -50%);
            }
    </style>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <div id="wrapper">
            <%--       <div class="lang-holder">
                <i class="fas fa-chevron-down icon-down"></i>

                <i class="fas fa-globe-europe icon-lang"></i>
            </div>--%>
            <asp:DropDownList ID="ddlSelectLang" Style="display: none;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectLang_SelectedIndexChanged">
                <asp:ListItem Value="0">English</asp:ListItem>
                <asp:ListItem Value="1" Selected="True">العربيه</asp:ListItem>
            </asp:DropDownList>

            <div class="lang-holder">
                <i class="fas fa-chevron-down icon-down"></i>
                <span class="lang" id="langTxt">العربيه</span>
                <i class="fas fa-globe-europe icon-lang"></i>
                <ul class="dropdown-menu">
                    <li data-id="0" onclick="$('#ddlSelectLang').val($(this).attr('data-id'));$('#ddlSelectLang').change();">English</li>
                    <li data-id="1" onclick="$('#ddlSelectLang').val($(this).attr('data-id'));$('#ddlSelectLang').change();">العربيه</li>
                </ul>
            </div>

            <div id="text" runat="server">
                <asp:Image runat="server" ID="imgCompany" Visible="true" ImageUrl="../Images/CompanyLogo.png" AlternateText="Basrah Health Logo" Style="width: 150px" />
                <br />
                <div>نظام إدارة الأعمال</div>
                <div>Business Management System</div>
            </div>
            <asp:Panel runat="server" ID="box" DefaultButton="btnsubmit">
                <div class="elements">
                    <asp:TextBox runat="server" ID="txtUserName" class="username" value="Username" onfocus="if (this.value=='Username'){this.value=''}" onblur="if (this.value==''){this.value='Username'}" />
                    <%--<asp:TextBox runat="server" ID="txtPassword" TextMode="Password" class="password" value="" />--%>
                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" class="password" value="•••••••••" onfocus="if (this.value=='•••••••••'){this.value=''}" onblur="if (this.value==''){this.value='•••••••••'}" />
                    <br />
                    <asp:RadioButtonList ID="rdoLang" runat="server" RepeatDirection="Horizontal"
                        Font-Size="12px" Style="margin: 0px auto;" Visible="false">
                        <asp:ListItem Value="0">English</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">عربي</asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="g-recaptcha" data-sitekey="6LedeQgaAAAAAMw8RoLh1yiB0HsJrNMVK_89W2sH">
                    </div>
                    <div style="padding-top: 10px;">
                        <asp:Label ID="lblResult" runat="server" ForeColor="#CC0000" Font-Size="12px"></asp:Label>
                    </div>
                    <div style="padding-top: 10px;">
                        <asp:Button runat="server" ID="btnsubmit" class="button" Text="Login | دخول" Style="float: none !important"
                            OnClick="lnkLogin_Click" />
                    </div>
                </div>
        </asp:Panel>
            <br />
        <div class="logo">
            <img src="../images/DMSLogo.png" height="50px" style="margin-top: 18px" />
            <br />
            <br />
            Powered by HudHud &copy;

        </div>
        </div>
    </form>
    <div class="loader-holder" style="display: none;">
        <img src="/Assets/UIKIT/img/loader.svg" class="loader-img">
    </div>

    <script type="text/javascript">
        function ShowProgress() {
            $(".loader-holder").show();
            //setTimeout(function () {
            //    $(".loader-holder").hide();
            //}, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#langTxt").html($("#ddlSelectLang").find('option:delected').text());
            $("#langTxt").html($("#ddlSelectLang").find('option:selected').text());
            $('.lang-holder').on('click', function (e) {
                e.stopImmediatePropagation();
                $('.lang-holder .dropdown-menu').removeClass('open');
                $(this).children('.lang-holder .dropdown-menu').toggleClass('open');
            });

            $(document).on('click', function (e) {
                e.stopImmediatePropagation();
                $('.lang-holder .dropdown-menu').removeClass('open');
            });

            ////// Selected Option
            $('.lang-holder').on('click', '.dropdown-menu li', function (e) {
                e.stopImmediatePropagation();
                $(this).addClass('active').siblings().removeClass('active');
                $('.lang-holder .dropdown-menu').removeClass('open');
                $('.lang-holder .lang').html($('li.active').html());

            });

        });
    </script>
    <script type="text/javascript">
        (function () {
            var s = document.createElement("script");
            s.type = "text/javascript"; s.async = true; s.src = 'https://api.klynd.com/api/project/5ffc82d8d885527b987f34c5/603543447817f724e738eb62/' + btoa(window.location.host);
            s.onload = (function () { window.top.postMessage({ type: 'scriptLoaded' }, "*"); });
            localStorage.setItem('klPID', JSON.stringify('603543447817f724e738eb62'));
            var x = document.getElementsByTagName('script')[0]; x.parentNode.insertBefore(s, x);
        })();
    </script>
</body>
</html>
