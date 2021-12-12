<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.Master" AutoEventWireup="true" CodeBehind="departmentsManage.aspx.cs" Inherits="dms.Admin.departmentsManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
    <style>
        .new-drop {
            padding: 4px;
            border-radius: 20px;
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

        .ContentPlaceHolder1_ContentPlaceHolder1_trvDep_0 {
            margin-right: 5px !important;
        }
    </style>
    <script type="text/javascript">
        function showIcon(obj) {
            var dropdown = obj;
            var myindex = dropdown.selectedIndex;
            var SelValue = dropdown.options[myindex].value;

            document.getElementById("imgIcon").src = "../Images/dbIcons/" + SelValue + "-32.png";
            document.getElementById("imgIcon").style.visibility = "visible";
        }

        $(document).ready(function () {
            var currActive = parseInt(document.getElementById("hdnActivePanel").value);
            //alert(currActive);
            $("#accordion").accordion({ active: currActive, collapsible: true });

        });

        function changeActive(num) {
            document.getElementById("hdnActivePanel").value = String(num);
        }
        //        jQuery(document).ready(function () {
        //            $('.accordion .head').click(function () {
        //                $(this).next().toggle('slow');
        //                return false;
        //            }).next().hide();
        //        });


    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="../screen/subIcons.aspx?CODEN=2&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? "Settings" : "الاعدادات"%> </a></li>
        <li><a href="../admin/departmentsManage.aspx?CODEN=9"><%= (Session["lang"].ToString() == "0") ? " Departments" : "الهيكلية "%></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnActivePanel" ClientIDMode="Static" runat="server" Value="0" />
    <div class="white-holder" runat="server" id="divList" style="padding-top: 15px;">
        <div class="control-side-holder">
            <div class="start-side">
                <a class="btn-main" runat="server" onserverclick="btnAdd_ServerClick">
                    <div class="btn-main-wrapper">
                        <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                            <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Add New Department" : "اضافة إدارة جديد"%>
                    </div>
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <asp:TreeView ID="trvDep" runat="server"
                     OnTreeNodePopulate="trvDep_TreeNodePopulate"
                    OnSelectedNodeChanged="trvDep_SelectedNodeChanged" Font-Size="14px">
                </asp:TreeView>
            </div>
        </div>
    </div>
    <div id="divDetails" runat="server" visible="false" class="white-holder">
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title">
                        <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add New Department"
                            CssClass="formModeTitleCSS"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-xs-4" style="display:none;">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblDepartmentID" runat="server" Text="Department ID"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtDepartmentID" runat="server" CssClass="main-input" Width="50px" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblDepartmentName" runat="server" Text="Department Name (English)"></asp:Label>
                    </label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtDepartmentName" ErrorMessage="" CssClass="required"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="main-input" Width="300px"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblDepartmentNameAr" runat="server" Text="Department Name (Arabic)"></asp:Label>
                    </label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtDepartmentNameAr" ErrorMessage="" CssClass="required"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtDepartmentNameAr" runat="server" CssClass="main-input" Width="300px"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblHeadUserID" runat="server" Text="Head User"></asp:Label>
                    </label>
                    <asp:DropDownList ID="drpHeadUserID" runat="server" CssClass="new-drop">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblParentDepartmentID" runat="server" Text="Section"></asp:Label>
                    </label>
                    <asp:DropDownList ID="drpParentDepartmentID" runat="server" CssClass="new-drop">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblDropParentID" runat="server" Text="Parent"></asp:Label>
                    </label>
                    <asp:DropDownList ID="dropParentID" runat="server" CssClass="new-drop">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-4" style="display: none;">
                <asp:RadioButtonList ID="rdoSaveMethod" runat="server" ClientIDMode="Static"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">New Department</asp:ListItem>
                    <asp:ListItem Value="1">Exsit Department</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="control-side-holder control-side-holder-footer">
            <div class="start-side">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-main">
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
                &nbsp;   
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
                <a class="btn-main" runat="server" data-toggle="modal" data-target="#remove-confirm" id="btnDelete" onserverclick="btnDelete_ServerClick">
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
</asp:Content>
