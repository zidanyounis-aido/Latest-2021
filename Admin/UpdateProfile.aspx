<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="dms.Admin.UpdateProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/croppie.css" rel="stylesheet" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Swiper/5.4.5/css/swiper.min.css" />
    <script src="../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../JS/croppie.js" type="text/javascript"></script>
    <style type="text/css">
        :root {
            --main-color: #007aff;
        }

        @font-face {
            font-family: 'TheSans';
            src: url('../fonts/TheSans-Bold.woff2') format('woff2'), url('../fonts/TheSans-Bold.woff') format('woff'), url('../fonts/TheSans-Bold.ttf') format('truetype');
            font-weight: bold;
            font-style: normal;
            font-display: swap;
        }

        @font-face {
            font-family: 'TheSans';
            src: url('../fonts/TheSans-Plain.woff2') format('woff2'), url('../fonts/TheSans-Plain.woff') format('woff'), url('../fonts/TheSans-Plain.ttf') format('truetype');
            font-weight: normal;
            font-style: normal;
            font-display: swap;
        }

        body {
            font-size: 14px;
            background-color: #fff;
            font-family: 'TheSans', sans-serif;
        }

        body {
            direction: rtl;
        }

        .modal-header {
            border: none;
            padding: 20px 15px;
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .modal-title {
            font-weight: bold;
            color: var(--main-color);
            font-size: 15px;
            margin-inline-end: auto;
        }

        .cr-slider {
            display: inline-block !important;
            width: inherit !important;
        }

        .modal-footer {
            border-top: none !important;
        }

        .main-field-holder {
            margin: 15px 0;
        }

            .main-field-holder .main-input {
                width: 100%;
                border: 1px solid #cacaca;
                border-radius: 20px;
                padding: 7px;
                outline: none;
                height: 35px;
            }

            .main-field-holder .main-label {
                display: block;
                padding: 0;
                font-size: 14px;
                margin: 0 0 10px;
                text-overflow: ellipsis;
                overflow: hidden;
                white-space: nowrap;
                color: #828282;
                font-weight: normal;
            }

        .modal-footer {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border: none;
            text-align: left;
        }

            .modal-footer .btn-done-model {
                background: var(--main-color);
                border: none;
                outline: none;
                color: #fff;
                padding: 5px 20px;
                border-radius: 20px;
            }

            .modal-footer .btn-clear-model {
                background: var(--main-color);
                border: none;
                outline: none;
                color: #fff;
                padding: 5px 20px;
                border-radius: 20px;
                cursor: pointer;
                text-decoration: none;
            }

            .modal-footer .btn-close-model {
                border: none;
                outline: none;
                background: transparent;
                margin-inline-end: auto;
                display: flex;
                color: #7c7c7c;
            }

                .modal-footer .btn-close-model .icon-close {
                    background: #e9e9e9;
                    width: 20px;
                    display: inline-block;
                    height: 20px;
                    border-radius: 50px;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    margin: 0 7px;
                }

        .avatar-holder {
            position: relative;
            overflow: hidden;
            height: 120px;
            width: 120px;
            margin: auto;
            overflow: hidden;
            border-radius: 60px;
            cursor: pointer;
        }

            .avatar-holder img {
                height: 120px;
            }

        .filelabel {
            width: 20px;
            position: absolute;
            top: 100px;
            left: 305px;
            padding: 5px;
            transition: border 300ms ease;
            cursor: pointer;
            text-align: center;
            margin: 0;
        }

            .filelabel i {
                display: block;
                font-size: 30px;
                padding-bottom: 5px;
            }

            .filelabel i,
            .filelabel .title {
                color: transparent;
                transition: 200ms color;
                width: 40px;
                border-radius: 10px;
            }

            .filelabel:hover i,
            .filelabel:hover .title {
                color: #000;
                background-color: #fff;
            }

        #croppie-input {
            display: none;
        }

        .avatar-holder:hover .icon-upload-avatar {
            opacity: 1;
        }

        .icon-upload-avatar {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            background: rgb(0 0 0 / 29%);
            opacity: 0;
            transition: 0.3s ease;
        }

        .required {
            display: none;
        }

            .required[style*=visible] + input,
            .required[style*=visible] + select,
            .required[style*=visible] + textarea {
                background-color: #ffcccc;
                border: 1px solid #ff0000;
            }

            .required[style*=inline] + input,
            .required[style*=inline] + select,
            .required[style*=inline] + textarea {
                background-color: #ffcccc;
                border: 1px solid #ff0000;
            }
    </style>
</head>
<body style="width: 100%">
    <form id="form1" runat="server">
        <div class="container">
            <div class="modal-header">
                <h4 class="modal-title" id="lblmypersonalfile" runat="server">ملفي الشخصي</h4>
            </div>
            <div class="row text-center">
                <div id="divImagUser" style="text-align: center;" class="avatar-holder">
                    <img id="imgUser" runat="server" />
                </div>
                <div style="text-align: center;">
                    <div id="croppie" style="display: none;"></div>
                </div>
                <div id="croppie-container" style="text-align: center;">
                    <label class="filelabel">
                        <i class="fa fa-upload"></i>
                        <input type="file" id="croppie-input" accept=".jpg,.jpeg,.png" />
                    </label>
                </div>
                <div class="control-label text-center">
                    <asp:Label ID="lblUserName" runat="server" Text="User"></asp:Label>
                </div>
            </div>
            <div class="row text-right" dir="rtl">
                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <asp:Label ID="lblcellphone" runat="server" Text="Phone" CssClass="main-label"></asp:Label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="main-input"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <asp:Label ID="lblE_mail" runat="server" Text="E-Mail" CssClass="main-label"></asp:Label>
                        <asp:RegularExpressionValidator ID="revE_mailformatiswrong" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Email syntax error"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="u" Display="Dynamic" CssClass="required"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvPleaseenteryoure_mail" runat="server" ValidationGroup="u"
                            ControlToValidate="txtEmail" ErrorMessage="Email is Required" ForeColor="Red" Display="Dynamic" CssClass="required"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="main-input"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row text-right" dir="rtl">
                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <asp:Label ID="lblpasswordconfirmation" runat="server" Text="Confirm Password" CssClass="main-label"></asp:Label>
                        <asp:CompareValidator ID="cvPassworddonotmatch" runat="server"
                            ControlToCompare="txtPassword" ControlToValidate="txtRePassword"
                            ErrorMessage="Password not match" ValidationGroup="u" ForeColor="Red" Display="Dynamic" CssClass="required"></asp:CompareValidator>
                        <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"
                            CssClass="main-input"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <asp:Label ID="lblpassword" runat="server" Text="Password" CssClass="main-label"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="main-input"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="navbar navbar-fixed-bottom">
            <div class="text-center">
                <asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
            </div>
            <div class="modal-footer">
                <button type="button" id="btnSave" runat="server" onserverclick="btnSave_Click" validationgroup="u" causesvalidation="true" class="btn-done-model">
                    <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                        <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                        <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                        <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                        <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                        <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                    </svg>
                    <span id="SpanSave" runat="server">حفظ</span></button>
                <button type="button" class="btn-close-model" onclick="closeModal()">
                    <span class="icon-close">
                        <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                            <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                            </g>
                        </svg>
                    </span>
                    <span id="lblRetreat" runat="server">تراجع</span>
                </button>
                <a class="btn-clear-model" onclick="ClearTextBoxes()">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                            <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                            <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                        </svg>
                        <span id="lblSurvey" runat="server">مسح</span>
                    </div>
                </a>
            </div>
        </div>
        <asp:HiddenField ID="hdnPassword" runat="server" />
        <asp:HiddenField ID="hdnIsEmailVerfied" runat="server" />
        <asp:HiddenField ID="hdnIsMobileFirstLogin" runat="server" />
        <asp:HiddenField ID="hdnSignature" runat="server" />
        

        <input type="hidden" id="hCroppieImage" runat="server" />
        <script type="text/javascript">
            function closeModal() {
                var btnClose = parent.document.getElementById('btnClosePanlProfile');
                btnClose.click();
                return false;
            }
            var croppieDemo = $('#croppie').croppie({
                enableOrientation: true,
                viewport: {
                    width: 148,
                    height: 148,
                    type: 'circle' // or 'square'
                },
                boundary: {
                    width: 150,
                    height: 150
                },
                update: function (data) {
                    croppieDemo.croppie('result', {
                        type: 'canvas',
                        size: 'viewport'
                    }).then(function (image) {
                        $("#" + '<%= hCroppieImage.ClientID %>').val(image);
                        });
                }
            });

            $('#croppie-input').on('change', function () {
                var fileName = document.getElementById("croppie-input").value;
                var idxDot = fileName.lastIndexOf(".") + 1;
                var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
                if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {
                    $('#croppie').show();
                    $('#divImagUser').hide();
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        croppieDemo.croppie('bind', {
                            url: e.target.result
                        });
                    }
                    reader.readAsDataURL(this.files[0]);
                } else {
                    alert("Only jpg/jpeg and png files are allowed!");
                }
            });
        </script>
    </form>
    <script>
        function ClearTextBoxes() {
            document.getElementById("<%= txtEmail.ClientID %>").value = "";
            document.getElementById("<%= txtPhone.ClientID %>").value = "";
            document.getElementById("<%= txtPassword.ClientID %>").value = "";
            document.getElementById("<%= txtRePassword.ClientID %>").value = "";
        }
    </script>
</body>
</html>
