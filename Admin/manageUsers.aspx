<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.Master" AutoEventWireup="true" CodeBehind="manageUsers.aspx.cs" Inherits="dms.Admin.manageUsers" %>
<%@ Register Src="../Controls/folderTree.ascx" TagName="folderTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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

            .required[style*=visible] + span + input,
            .required[style*=visible] + span + select,
            .required[style*=visible] + span + textarea {
                background-color: #ffcccc;
                border: 1px solid #ff0000;
            }

            .required[style*=inline] + span + input,
            .required[style*=inline] + span + select,
            .required[style*=inline] + span + textarea {
                background-color: #ffcccc;
                border: 1px solid #ff0000;
            }

        .new-drop {
            padding: 4px;
            border-radius: 20px;
            width: 100%;
        }

        .new-main-input {
            width: 97% !important;
            border: 1px solid #cacaca;
            border-radius: 20px;
            padding: 6px;
            outline: none;
            /*height: 35px;*/
        }

        .margin-top-20 {
            margin-top: 20px;
        }

        .cellTDAr {
            margin-top: 10px;
        }

        .ContentPlaceHolder1_ContentPlaceHolder1_trvFolders_0 {
            margin-right: 5px !important;
        }

        .radio-input-holder label {
        }
    </style>
    <link href="../css/croppie.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/croppie.js"></script>
    <style type="text/css">
        #croppie {
            width: 350px;
        }

        #croppie-container {
            padding-top: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="../screen/subIcons.aspx?CODEN=2&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? "Settings" : "الاعدادات"%> </a></li>
        <li><a href="../admin/manageUsers.aspx?CODEN=12"><%= (Session["lang"].ToString() == "0") ? "Manage Users" : " إدارة المستخدمين"%></a></li>
        <li><a href="../admin/manageUsers.aspx?CODEN=12">
            <asp:Label runat="server" ID="lblusermode"></asp:Label></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: none;">
        <asp:TextBox ID="txtUserSearch" runat="server"></asp:TextBox>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"
            CausesValidation="False"><%= (Session["lang"].ToString() == "0") ? "Search" : "بحث"%></asp:LinkButton>
    </div>
    <div class="white-holder" runat="server" id="divList" style="padding-top: 15px;">
        <div class="row">
            <div class="col-xs-3">
                <a id="btnAdd" causesvalidation="false" runat="server" onserverclick="btnAdd_ServerClick">
                    <div class="select-setting-item-holder add-select-item-holder">
                        <div class="select-setting-icon">
                            <svg visibility="visible" xmlns="http://www.w3.org/2000/svg" width="8" height="8" viewBox="0 0 8 8">
                                <g id="Group_3023" data-name="Group 3023" transform="translate(-1664.841 -367.841)">
                                    <circle id="Ellipse_598" data-name="Ellipse 598" cx="4" cy="4" r="4" transform="translate(1664.841 367.841)" fill="#484848"></circle>
                                    <g id="Group_3020" data-name="Group 3020" transform="translate(1668.87 370.266) rotate(45)">
                                        <g id="Group_2166" data-name="Group 2166" transform="translate(0)">
                                            <line id="Line_28" data-name="Line 28" y2="3.59" transform="translate(2.539) rotate(45)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="0.5"></line>
                                            <line id="Line_29" class="make-plus" data-name="Line 29" x2="3.59" transform="rotate(45)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="0.5"></line>
                                        </g>
                                    </g>
                                </g>
                            </svg>
                        </div>
                        <p class="select-setting-title"><%= (Session["lang"].ToString() == "0") ? "Add New User" : "إضافة مستخدم جديد"%></p>
                    </div>
                </a>
            </div>
            <asp:ListView ID="dlusers" runat="server" CausesValidation="false" DataKeyNames="userID" OnSelectedIndexChanging="dl_SelectedIndexChanging">
                <ItemTemplate>
                    <div class="col-xs-3">
                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("userID") %>' CausesValidation="false">
                        <div class="select-setting-item-holder">
                            <div class="select-setting-icon">
                                <span><%# (Session["lang"].ToString() == "0") ? SafeSmartSubstring(Eval("fullName").ToString()) : SafeSmartSubstring(Eval("fullName").ToString()) %>  </span>
                            </div>
                            <p class="select-setting-title"><%# (Session["lang"].ToString() == "0") ? Eval("fullName") : Eval("fullName")%> </p>
                        </div>
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div id="divDetails" runat="server" visible="false" class="white-holder">
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title">
                        <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add Workflow Path"
                            CssClass="formModeTitleCSS"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-xs-4">
                <div id="divImagUser" class="avatar-holder" style="margin: 30px 0 15px;" onclick="$('#croppie-input').click();">
                    <input type="file" class="hidden">
                    <img src="/Assets/UIKIT/img/noAvatar.jpg" id="imgUser" runat="server" class="bg-cover">
                    <span class="icon-upload-avatar">
                        <svg version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 512 512" style="enable-background: new 0 0 512 512;" xml:space="preserve">
                            <g>
                                <g>
                                    <path d="M380.032,133.472l-112-128C264.992,2.016,260.608,0,256,0c-4.608,0-8.992,2.016-12.032,5.472l-112,128
                        c-4.128,4.736-5.152,11.424-2.528,17.152C132.032,156.32,137.728,160,144,160h64v208c0,8.832,7.168,16,16,16h64
                        c8.832,0,16-7.168,16-16V160h64c6.272,0,11.968-3.648,14.56-9.376C385.152,144.896,384.192,138.176,380.032,133.472z">
                                    </path>
                                </g>
                            </g>
                            <g>
                                <g>
                                    <path d="M432,352v96H80v-96H16v128c0,17.696,14.336,32,32,32h416c17.696,0,32-14.304,32-32V352H432z"></path>
                                </g>
                            </g>
                        </svg>
                    </span>
                </div>
                <div id="croppie" style="display: none; width: 100% !important;height: 380px !important;">
                </div>
                <a   id="btnEditImage" class="btn-main"  onclick="$('#croppie-input').click();">
                    <div class="btn-main-wrapper">
                       <i class="fa fa-upload"></i>
                         <span id="ContentPlaceHolder1_ContentPlaceHolder1_lblSave1" style="margin:5px;"> <%= (Session["lang"].ToString() == "0") ? "Select File" : "اختر الملف"%></span>
                    </div>
                </a>
            </div>
            <div class="col-xs-4">
                <div class="col-xs-12 col-1" style="display:none;">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label1" runat="server" Text="User ID"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtUserID" runat="server" CssClass="main-input" BorderColor="Gray"
                            BorderStyle="Solid" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-5">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label7" runat="server" Text="Full Name"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="main-input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="txtFullName" ErrorMessage=""
                            ValidationGroup="u" CssClass="required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        
                    </div>
                </div>
                <div class="col-xs-12 col-3">


                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="main-input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                            ControlToValidate="txtPassword" ErrorMessage=""
                            ValidationGroup="u" CssClass="required" Display="Dynamic" SetFocusOnError="true" Enabled="false"></asp:RequiredFieldValidator>
                        
                    </div>
                </div>
                

                <div class="col-xs-12 col-7">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label9" runat="server" Text="Company"></asp:Label>
                        </label>
                        <asp:DropDownList ID="drpCompanyID" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="drpCompanyID_SelectedIndexChanged" CssClass="new-drop">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 col-9">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label10" runat="server" Text="Department"></asp:Label>
                        </label>
                        <asp:DropDownList ID="drpDepartmentID" runat="server" CssClass="new-drop">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 col-11">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label13" runat="server" Text="E-Mail"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="main-input"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Email syntax error"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="u">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="u"
                            ControlToValidate="txtEmail" ErrorMessage="" CssClass="required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        
                    </div>
                </div>
                <div class="col-xs-12 col-13">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="LabelPhone" runat="server" Text="Phone"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="main-input"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-xs-4">

                <div class="col-xs-12 col-2">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label8" runat="server" Text="User Name"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="main-input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtUserName" ErrorMessage=""
                            ValidationGroup="u" CssClass="required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        
                    </div>
                </div>

                <div class="col-xs-12 col-4">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"
                            CssClass="main-input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRePassword" runat="server"
                            ControlToValidate="txtRePassword" ErrorMessage=""
                            ValidationGroup="u" CssClass="required" Display="Dynamic" SetFocusOnError="true" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                            ControlToCompare="txtPassword" ControlToValidate="txtRePassword"
                            ValidationGroup="u" CssClass="required" Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                        
                    </div>
                </div>

                <div class="col-xs-12 col-6">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label6" runat="server" Text="Group"></asp:Label>
                        </label>
                        <asp:DropDownList ID="drpGrpID" runat="server" CssClass="new-drop">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                            ControlToValidate="drpGrpID" ErrorMessage="" InitialValue="-32768"
                            ValidationGroup="u" CssClass="required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        
                    </div>
                </div>

                <div class="col-xs-12 col-8">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label11" runat="server" Text="Branch"></asp:Label>
                        </label>
                        <asp:DropDownList ID="drpBranchID" runat="server" CssClass="new-drop">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-xs-12 col-10">
                    <div class="main-field-holder required_input">
                        <label class="main-label">
                            <asp:Label ID="Label12" runat="server" Text="Position"></asp:Label>
                        </label>
                        <asp:DropDownList ID="drpPositionID" runat="server" CssClass="new-drop">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-xs-12 col-12">
                    <div class="main-field-holder required_input">
                        <asp:CheckBox ID="chkActive" runat="server" Text="Active"
                            CssClass="chkActiveCSS" ClientIDMode="Static" Checked="True" />
                    </div>
                </div>

                <div class="col-xs-12 col-14" style="display: none">
                    <div class="main-field-holder required_input">
                        <asp:HiddenField ID="hdnPassword" runat="server" />
                        <asp:HiddenField ID="hdnIsFirstLogin" runat="server" />
                        <asp:HiddenField ID="hdnPasswordCreationDate" runat="server" />
                        <asp:HiddenField ID="hdnPasswordModifiedDate" runat="server" />
                        <asp:HiddenField ID="hdnLastPassword" runat="server" />
                    </div>
                </div>
            </div>


            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title">
                        <%= (Session["lang"].ToString() == "0") ? "General Permissions" : "الصلاحيات العامة"%>
                        </div>
                </div>
            </div>
            <div class="col-xs-4">
            </div>
            <div class="col-xs-8">
                <div class="main-field-holder">
                    <div class="radio-input-holder multi-line">
                        <asp:CheckBox ID="chkAllowCustomWF" runat="server"
                            Text="Allow Make Custom Workflows" />
                    </div>

                    <div class="radio-input-holder multi-line" style="display:none">
                        <asp:CheckBox ID="chkAllowCreateFolders" runat="server" Text="Allow Create Folders" />
                    </div>

                    <div class="radio-input-holder multi-line" style="display:none">
                        <asp:CheckBox ID="chkAllowReplaceDocuments" runat="server" Text="Allow Replace Documents" />
                    </div>

                    <div class="radio-input-holder multi-line" style="display:none">
                        <asp:CheckBox ID="chkAllowDiwan" runat="server"
                            Text="Allow Set Workflow Timeframe" />
                    </div>
                </div>
            </div>
            <div class="col-xs-4">
            </div>
            <div class="col-xs-4" style="display: none">
                <div class="main-field-holder required_input">
                    <div class="row">
                        <%--  <div id="divImagUser" style="text-align: center;">
                        
                        </div>--%>
                        <div>
                        </div>
                        <div id="croppie-container" style="text-align: center;">
                            <input type="file" id="croppie-input" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="display: none">
                <div style="display: none">
                    <asp:RadioButtonList ID="rdoSaveMethod" ClientIDMode="Static" runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">New User</asp:ListItem>
                        <asp:ListItem Value="1">Exsit User</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <%--   <div class="col-xs-2">
                <div class="main-field-holder required_input">
                    <%= (Session["lang"].ToString() == "0") ? "Signature" : "التوقيع"%>
                </div>
            </div>--%>
            <%-- <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
            </div>--%>
        </div>
        <!-- sign -->
        <hr class="my-hr">
        <div class="row">
            <div class="max-width-holder">
                <div class="col-xs-12">
                    <div class="main-field-holder">
                        <div class="main-title">
                            <%= (Session["lang"].ToString() == "0") ? "Signature" : "التوقيع"%>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4">
                </div>

                <div class="col-xs-8">

                    <div class="upload-file-holder upload-file-holder-setting">
                        <a class="btn-main" onclick="$('#ContentPlaceHolder1_ContentPlaceHolder1_FileUpload1').click();">
                            <div class="btn-main-wrapper">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                    <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff">
                                    </path>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Select File" : " أختر الملف"%>
                            </div>
                        </a>
                        
                        <div class="files-area files-area-scan">
                            <div class="file-item" id="divfileSign" runat="server" visible="false">
                               <%-- <span class="icon-close">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                        <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                            <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                            <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                        </g>
                                    </svg>
                                </span>--%>
                                <%--<span class="file-format file-format-jpg">jpg
                                </span>--%>
                                <asp:HiddenField ID="hdnSignature" runat="server" />
                                <asp:HiddenField ID="hdnIsMobileFirstLogin" runat="server" />
                                <asp:HiddenField ID="hdnIsEmailVerfied" runat="server" />
                                <asp:HiddenField ID="hdnClientId" runat="server" />

                                <asp:Image ID="imgSign" Width="150px" runat="server" Visible="false" />
                            </div>
                        </div>
                        <asp:FileUpload ID="FileUpload1" runat="server" style="visibility:hidden" />
                    </div>
                </div>
            </div>
        </div>
        <!-- folders -->
        <div id="tblDetailsForm" style="display: none" runat="server">
            <hr class="my-hr">
            <div class="row">
            <div class="col">
            <div class="max-width-holder">
                <div class="row">
                    <div class="main-field-holder">
                        <div class="main-title"><%= (Session["lang"].ToString() == "0") ? "Permitted Folders" : "المجلدات المسموحة"%> </div>
                    </div>
                </div>
                <div class="col-xs-6">
                </div>
                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Select Folder" : "اختر مجلد"%>  </label>
                        <div class="input-with-btn">
                            <asp:DropDownList ID="drpFolders" Visible="false" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnFldrID" ClientIDMode="Static" runat="server" />
                            <asp:TextBox ID="txtFldrID" ClientIDMode="Static" onClick="showFolderDialog()" ReadOnly="true" runat="server" CssClass="main-input"></asp:TextBox>
                            <asp:LinkButton ID="btnAddFolder" CssClass="btn-main" runat="server" OnClick="btnAddFolder_Click"> 
                              <div class="btn-main-wrapper">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                    <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff">
                                    </path>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Add" : "اضافة"%>
                            </div>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            
                <br>
                <asp:GridView ID="grdUsersFolders" runat="server" CssClass="table my-table"
                    GridLines="None" AutoGenerateColumns="False"
                    OnRowCancelingEdit="grdUsersFolders_RowCancelingEdit"
                    OnRowEditing="grdUsersFolders_RowEditing"
                    OnRowUpdating="grdUsersFolders_RowUpdating"
                    OnRowDeleting="grdUsersFolders_RowDeleting">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:BoundField DataField="fldrID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="fldrName" HeaderText="Folder Name" ReadOnly="True" />
                        <asp:CheckBoxField DataField="allow" HeaderText="View" />
                        <asp:CheckBoxField DataField="allowInsert" HeaderText="Insert" />
                        <asp:CheckBoxField DataField="allowUpdate" HeaderText="Update" />
                        <asp:CheckBoxField DataField="allowDelete" HeaderText="Delete" />
                        <asp:CheckBoxField DataField="allowOutgoing" HeaderText="Create Outgoing" />
                        <asp:CheckBoxField DataField="allowIncoming" HeaderText="Add Incoming" />
                        <asp:CheckBoxField DataField="inheritSubFolders"
                        HeaderText="inherit Sub Folders" />

                        <asp:CommandField ShowEditButton="True" EditText="<svg xmlns='http://www.w3.org/2000/svg' width='22.506' height='22.506' viewBox='0 0 22.506 22.506'> <path id='Path_6947' data-name='Path 6947' d='M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z' transform='translate(-0.001)' fill='#0072ff'></path> </svg>" ButtonType="Link"
                            ControlStyle-CssClass="tr-edit" />
                        <asp:CommandField DeleteText="<svg xmlns='http://www.w3.org/2000/svg' width='11.963' height='11.963' viewBox='0 0 11.963 11.963'> <g id='Group_21' data-name='Group 21' transform='translate(5.981 -3.153) rotate(45)'> <line id='Line_28' data-name='Line 28' y2='12.918' transform='translate(6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> <line id='Line_29' data-name='Line 29' x2='12.918' transform='translate(0 6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> </g> </svg>"
                            ButtonType="Link" ControlStyle-CssClass="tr-remove"
                            ShowDeleteButton="True" />
                    </Columns>
                    <EditRowStyle />
                    <FooterStyle />
                    <HeaderStyle />
                    <PagerStyle />
                    <RowStyle />
                    <SelectedRowStyle />
                    <SortedAscendingCellStyle />
                    <SortedAscendingHeaderStyle />
                    <SortedDescendingCellStyle />
                    <SortedDescendingHeaderStyle />
                </asp:GridView>
            
            </div>
            
            <!-- programs -->
            <div class="col">
            <div id="Ttblble1" runat="server" clientidmode="Static">
                <div class="max-width-holder">
                    <div class="row">
                        <div class="main-field-holder">
                            <div class="main-title"><%= (Session["lang"].ToString() == "0") ? "Allowed programs" : "البرامج المسموحة"%>   </div>
                        </div>
                    </div>
                    <div class="col-xs-6">
                    </div>
                    <div class="col-xs-6">
                        <div class="main-field-holder">
                            <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Select a Program" : "اختر برنامج"%> </label>
                            <div class="input-with-btn">
                                <asp:DropDownList ID="drpProgramID" runat="server" CssClass="new-drop">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkAddProgram" runat="server" OnClick="lnkAddProgram_Click" CssClass="btn-main">
                             <div class="btn-main-wrapper">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                    <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff">
                                    </path>
                                </svg>

                                <%= (Session["lang"].ToString() == "0") ? "Add" : " اضافة"%>
                            </div>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                            <asp:GridView CssClass="table my-table" ID="grdPrograms" runat="server" AutoGenerateColumns="False"
                                GridLines="None"
                                OnRowDeleting="grdPrograms_RowDeleting">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:BoundField DataField="programID" HeaderText="Program ID" />
                                    <asp:BoundField DataField="programName" HeaderText="Program Name" />
                                    <asp:CommandField DeleteText="<svg xmlns='http://www.w3.org/2000/svg' width='11.963' height='11.963' viewBox='0 0 11.963 11.963'> <g id='Group_21' data-name='Group 21' transform='translate(5.981 -3.153) rotate(45)'> <line id='Line_28' data-name='Line 28' y2='12.918' transform='translate(6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> <line id='Line_29' data-name='Line 29' x2='12.918' transform='translate(0 6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> </g> </svg>"
                                        ButtonType="Link"
                                        ShowDeleteButton="True" ControlStyle-CssClass="tr-remove" />
                                </Columns>
                                <EditRowStyle />
                                <FooterStyle />
                                <HeaderStyle />
                                <PagerStyle />
                                <RowStyle />
                                <SelectedRowStyle />
                                <SortedAscendingCellStyle />
                                <SortedAscendingHeaderStyle />
                                <SortedDescendingCellStyle />
                                <SortedDescendingHeaderStyle />
                            </asp:GridView>
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            </div>
        </div>
        <!-- folders groups -->
<%--<div id="trGroups" runat="server">
                <hr class="my-hr">
                <div class="max-width-holder">
                    <div class="col-xs-12">
                        <div class="main-field-holder">
                            <div class="main-title"><%= (Session["lang"].ToString() == "0") ? "Group Folders" : "مجلدات المجموعات"%> </div>
                        </div>
                    </div>
                </div>
                <div class="">
                    <br>
                    <asp:GridView ID="grdGroupFolders" runat="server" CssClass="table my-table"
                        GridLines="None" AutoGenerateColumns="False">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:BoundField DataField="fldrID" HeaderText="ID" ReadOnly="True" />
                            <asp:BoundField DataField="fldrName" HeaderText="Folder Name" ReadOnly="True" />
                            <asp:CheckBoxField DataField="allow" HeaderText="View" />
                            <asp:CheckBoxField DataField="allowInsert" HeaderText="Insert" />
                            <asp:CheckBoxField DataField="allowUpdate" HeaderText="Update" />
                            <asp:CheckBoxField DataField="allowDelete" HeaderText="Delete" />
                        </Columns>
                        <EditRowStyle />
                        <FooterStyle />
                        <HeaderStyle />
                        <PagerStyle HorizontalAlign="Center" />
                        <RowStyle />
                        <SelectedRowStyle />
                        <SortedAscendingCellStyle />
                        <SortedAscendingHeaderStyle />
                        <SortedDescendingCellStyle />
                        <SortedDescendingHeaderStyle />
                    </asp:GridView>
                </div>
            </div>--%>
        <!-- footer input -->

        <div class="control-side-holder control-side-holder-footer">
            <div class="start-side">
                <asp:LinkButton ID="LinkButton2" OnClientClick="return checkPassword();"  runat="server" OnClick="btnSave_Click" CssClass="btn-main" ValidationGroup="u" CausesValidation="true">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                            <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                            <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                            <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                            <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                            <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                        </svg>
                        <span id="lblSave1" runat="server"><%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%></span>
                    </div>
                </asp:LinkButton>
                <asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
                <a class="btn-main btn-white" causesvalidation="false" runat="server" id="btnUndo" onserverclick="btnUndo_ServerClick">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg" width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                            <g id="Group_2175" data-name="Group 2175">
                                <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244" r="11.244" fill="#f4f4f4"></circle>
                                <g id="Group_2166" data-name="Group 2166" transform="translate(7.496 7.496)">
                                    <line id="Line_28" data-name="Line 28" y2="11.745" transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                    <line id="Line_29" data-name="Line 29" x2="11.745" transform="translate(0) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                </g>
                            </g>
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Cancel" : "تراجع"%>
                    </div>
                </a>

            </div>
            <div class="end-side">
                <a class="btn-main" causesvalidation="false" visible="false" runat="server" id="btnDelete" onserverclick="btnDelete_ServerClick">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                            <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                            <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                        </svg>
                        <span id="lblSurvey1" runat="server"><%= (Session["lang"].ToString() == "0") ? "Delete" : "حذف"%></span>
                    </div>
                </a>
            </div>
        </div>

    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
        ValidationGroup="u" />
    <input type="hidden" id="hCroppieImage" runat="server" />
    <script type="text/javascript">
        var croppieDemo = $('#croppie').croppie({
            enableOrientation: true,
            viewport: {
                width: 192,
                height: 192,
                type: 'circle' // or 'square'
            },
            boundary: {
                width: 300,
                height: 300
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
            $('#croppie').show();
            $('#divImagUser').hide();
            var reader = new FileReader();
            reader.onload = function (e) {
                croppieDemo.croppie('bind', {
                    url: e.target.result
                });
            }
            reader.readAsDataURL(this.files[0]);
        });
        function checkPassword() {
            if ($("#ContentPlaceHolder1_ContentPlaceHolder1_txtPassword").val() == "") {
                return true;
            }
            else {
                if ($("#ContentPlaceHolder1_ContentPlaceHolder1_txtPassword").val() == $("#ContentPlaceHolder1_ContentPlaceHolder1_txtRePassword").val()) {
                    return true;
                }
                else {
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_txtPassword").css("border-color", 'red');
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_txtRePassword").css("border-color", 'red');
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_txtPassword").focus();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="OutOfDesign">
    <div id="uiDialog" style="background: #ffffff" title="Select a Folder" class="ui-widget-content">
        <uc1:folderTree ID="folderTree1" runat="server" />
    </div>
</asp:Content>
