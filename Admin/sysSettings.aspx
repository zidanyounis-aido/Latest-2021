<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.master" AutoEventWireup="true" CodeBehind="sysSettings.aspx.cs" Inherits="dms.Admin.sysSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        .dropdown-main {
            height: 35px;
        }
    </style>
    <script>
        function ShowHidRes(display) {
            document.getElementById("divRes").style.display = display;
        }
    </script>
    <div class="tab-pane active" id="SystemSettings">
        <!-- Nav tabs -->
        <ul class="ul-edit-doc-tabs" role="tablist">
            <li role="presentation" class=""><a id="lnkLicense" runat="server" href="#License" onclick="ShowHidRes('none')" role="tab" data-toggle="tab">License
            </a>
            </li>
            <li role="presentation" class=""><a id="lnkPasswordPolicy" runat="server" href="#Passwordpolicy" onclick="ShowHidRes('none')" role="tab" data-toggle="tab">Password Policy
            </a>
            </li>
            <li role="presentation" class=""><a id="lnkSettings" runat="server" href="#Settings" onclick="ShowHidRes('none')" role="tab" data-toggle="tab">Settings
            </a>
            </li>
            <li role="presentation" class="active"><a id="lnkEmailSettings" runat="server" href="#EmailSettings" onclick="ShowHidRes('none')" role="tab" data-toggle="tab">Email Settings
            </a>
            </li>
        </ul>
        <div class="tab-content">
            <div id="divRes" style="width: 100%; text-align: center;">
                <asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
            </div>
            <div class="white-holder tab-pane" id="License">
                <div class="max-width-holder">

                    <div class="col-xs-12">
                        <div class="main-field-holder">
                            <div id="lblEditLicense" runat="server" class="main-title">Edit License </div>
                        </div>
                    </div>
                    <div class="col-xs-4"></div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label id="lblAllowedUsersCount" runat="server" class="main-label">Allowed Users Count</label>
                            <input class="main-input" type="text" id="txtAllowedUsersCount" runat="server">
                        </div>

                        <div class="main-field-holder">
                            <label id="lblSystemActiveDate" runat="server" class="main-label">System Active Date</label>
                            <input class="main-input" type="text" id="txtSystemActive" runat="server">
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label id="lblSystemActive" runat="server" class="main-label">System Active</label>
                            <input class="main-input" type="text" id="txtSystemActiveDate" runat="server">
                        </div>
                    </div>
                </div>
                <div class="control-side-holder control-side-holder-footer">
                    <div class="start-side">
                        <a id="btnSaveLicense" runat="server" onserverclick="btnSave_Click" class="btn-main">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                    <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                    <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                    <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                    <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                    <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                                </svg>
                                <span id="lblSave1" runat="server">حفظ</span>
                            </div>
                        </a>

                        <a class="btn-main btn-white">
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

                                <span id="lblRetreat1" runat="server">تراجع</span>
                            </div>
                        </a>
                    </div>

                    <div class="end-side">


                        <a class="btn-main" data-toggle="modal" data-target="#remove-confirm">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                    <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                    <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                                </svg>
                                <span id="lblSurvey1" runat="server">مسح</span>
                            </div>
                        </a>

                    </div>
                </div>
            </div>
            <div class="white-holder tab-pane" id="Passwordpolicy">
                <div class="max-width-holder">
                    <div class="col-xs-12">
                        <div class="main-field-holder">
                            <div id="lblEditPasswordpolicy" runat="server" class="main-title">Edit Password policy</div>
                        </div>
                    </div>
                    <div class="col-xs-4"></div>
                    <div class="col-xs-4">
                        <div class="main-field-holder required_input">
                            <label id="lblPasswordStrength" runat="server" class="main-label">Password Strength </label>
                            <asp:DropDownList ID="drpPasswordStrength" runat="server" CssClass="dropdown-main dropdown">
                                <asp:ListItem Value="1">Weak (Allowed any input)</asp:ListItem>
                                <asp:ListItem Value="2">Medium (Must contains Alphanumeric)</asp:ListItem>
                                <asp:ListItem Value="3">Strong (Must contains Alphanumeric and Symbols)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="main-field-holder">
                            <label id="lblPasswordminimumlength" runat="server" class="main-label">Password minimum length </label>
                            <input class="main-input" type="text" id="txtPasswordLength" runat="server">
                        </div>
                    </div>

                    <div class="col-xs-4">

                        <div class="main-field-holder">
                            <label id="lblPasswordAgeDays" runat="server" class="main-label">Password Age (Days) </label>
                            <input class="main-input" type="text" id="txtPasswordAgeDays" runat="server">
                        </div>
                        <div class="main-field-holder">


                            <div class="radio-input-holder">
                                <input type="checkbox" id="chkFirstLoginChangePassword" runat="server">
                                <label id="lblFirstLoginChangePassword" runat="server" for="1111">First Login Change Password</label>
                            </div>
                            <div class="radio-input-holder">
                                <input type="checkbox" id="chkPasswordAllowStartSpace" runat="server">
                                <label id="lblAllowStartSpace" runat="server" for="2222">Allow Start Space</label>
                            </div>
                            <div class="radio-input-holder">
                                <input type="checkbox" id="chkAllowUsernamePasswordMatch" runat="server">
                                <label id="lblAllowUsernamePasswordMatch" runat="server" for="2222">Allow Username Password Match</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="control-side-holder control-side-holder-footer">
                    <div class="start-side">
                        <a id="A1" runat="server" onserverclick="btnSave_Click" class="btn-main">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                    <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                    <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                    <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                    <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                    <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                                </svg>
                                <span id="lblSave2" runat="server">حفظ</span>
                            </div>
                        </a>

                        <a class="btn-main btn-white">
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

                                <span id="lblRetreat2" runat="server">تراجع</span>
                            </div>
                        </a>
                    </div>

                    <div class="end-side">


                        <a class="btn-main" data-toggle="modal" data-target="#remove-confirm">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                    <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                    <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                                </svg>
                                <span id="lblSurvey2" runat="server">مسح</span>
                            </div>
                        </a>

                    </div>
                </div>
            </div>

            <div class="white-holder tab-pane" id="Settings">
                <div class="max-width-holder">
                    <div class="col-xs-12">
                        <div class="main-field-holder">
                            <div id="lblEditSettings" runat="server" class="main-title">Edit Settings</div>
                        </div>
                    </div>
                    <div class="col-xs-4"></div>
                    <div class="col-xs-4">
                        <div class="main-field-holder required_input">
                            <label id="lblSessionTimeoutMinutes" runat="server" class="main-label">Session Timeout (Minutes) </label>
                            <input class="main-input" type="text" id="txtSessionTimeoutMinutes" runat="server">
                        </div>

                    </div>

                    <div class="col-xs-4">

                        <div class="main-field-holder">
                            <label id="lblLockTimeOut" runat="server" class="main-label">Lock Time Out </label>
                            <input class="main-input" type="text" id="txtLockTimeOut" runat="server">
                        </div>
                    </div>
                </div>
                <div class="control-side-holder control-side-holder-footer">
                    <div class="start-side">
                        <a id="A2" runat="server" onserverclick="btnSave_Click" class="btn-main">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                    <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                    <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                    <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                    <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                    <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                                </svg>
                                <span id="lblSave3" runat="server">حفظ</span>
                            </div>
                        </a>

                        <a class="btn-main btn-white">
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

                                <span id="lblRetreat3" runat="server">تراجع</span>
                            </div>
                        </a>
                    </div>

                    <div class="end-side">


                        <a class="btn-main" data-toggle="modal" data-target="#remove-confirm">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                    <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                    <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                                </svg>
                                <span id="lblSurvey3" runat="server">مسح</span>
                            </div>
                        </a>

                    </div>
                </div>
            </div>

            <div class="white-holder tab-pane active" id="EmailSettings">
                <div class="max-width-holder">
                    <div class="col-xs-12">
                        <div class="main-field-holder">
                            <div id="lblEditEmailSettings" runat="server" class="main-title">Edit Email Settings</div>
                        </div>
                    </div>
                    <div class="col-xs-4"></div>
                    <div class="col-xs-4">
                        <div class="main-field-holder required_input">
                            <label id="lblOutgoingMailServer" runat="server" class="main-label">Outgoing Mail Server </label>
                            <asp:TextBox ID="txtOutgoingMailServer" runat="server" class="main-input"></asp:TextBox>
                        </div>

                        <div class="main-field-holder">
                            <label id="lblWorkflowEmail" runat="server" class="main-label">Workflow Email </label>
                            <asp:TextBox ID="txtWorkflowEmail" runat="server" class="main-input"></asp:TextBox>
                        </div>

                        <div class="main-field-holder">
                            <label id="lblSystemEmail" runat="server" class="main-label">System Email </label>
                            <input class="main-input" type="text" id="txtSystemEmail" runat="server">
                        </div>
                        <div class="main-field-holder">
                            <label id="lblWorkflowEmailBody" runat="server" class="main-label">Workflow Email Body </label>
                            <textarea class="main-textarea textarea-lg-two" id="txtWorkflowEmailBody" runat="server"></textarea>
                        </div>
                    </div>

                    <div class="col-xs-4">

                        <div class="main-field-holder">
                            <label id="lblWorkflowEmailSubject" runat="server" class="main-label">Workflow Email Subject </label>
                            <input class="main-input" type="text" id="txtWorkflowEmailSubject" runat="server">
                        </div>

                        <div class="main-field-holder">
                            <label id="lblWorkflowEmailPassword" runat="server" class="main-label">Workflow Email Password </label>
                            <input class="main-input" type="text" id="txtWorkflowEmailPassword" runat="server">
                        </div>

                        <div class="main-field-holder">
                            <label id="lblSystemEmailPassword" runat="server" class="main-label">System Email Password </label>
                            <input class="main-input" type="text" id="txtSystemEmailPassword" runat="server">
                        </div>
                        <div class="main-field-holder">
                            <label id="lblSystemEmailSignature" runat="server" class="main-label">System Email Signature </label>
                            <textarea class="main-textarea textarea-lg-two" id="txtSystemEmailSignature" runat="server"></textarea>
                        </div>
                    </div>
                </div>
                <div class="control-side-holder control-side-holder-footer">
                    <div class="start-side">
                        <a id="A3" runat="server" onserverclick="btnSave_Click" class="btn-main">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                    <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                    <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                    <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                    <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                    <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                                </svg>
                                <span id="lblSave4" runat="server">حفظ</span>
                            </div>
                        </a>

                        <a class="btn-main btn-white">
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

                                <span id="lblRetreat4" runat="server">تراجع</span>
                            </div>
                        </a>
                    </div>

                    <div class="end-side">


                        <a class="btn-main" data-toggle="modal" data-target="#remove-confirm">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                    <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                    <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                                </svg>
                                <span id="lblSurvey4" runat="server">مسح</span>
                            </div>
                        </a>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnID" runat="server" />
    <asp:HiddenField ID="hdnAllowedUsersCount" runat="server" />
    <asp:HiddenField ID="hdnSystemActive" runat="server" />
    <asp:HiddenField ID="hdnSystemActiveDate" runat="server" />
    <asp:HiddenField ID="hdnSystemEmailPassword" runat="server" />
    <asp:HiddenField ID="hdnWorkflowEmailPassword" runat="server" />
</asp:Content>

