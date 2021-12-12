<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.Master" AutoEventWireup="true" CodeBehind="manageFolders.aspx.cs" Inherits="dms.Admin.manageFolders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .style1 {
            width: 32px;
            height: 32px;
        }

        .style2 {
            height: 37px;
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

        .ContentPlaceHolder1_ContentPlaceHolder1_trvFolders_0 {
            margin-right: 5px !important;
        }

        .ContentPlaceHolder1_ContentPlaceHolder1_trvFolders_0 {
            display: inline-block;
            display: flex;
            align-items: center;
            color: #242424;
            padding: 5px 0;
            border-radius: 20px;
            cursor: pointer;
            font-weight: bold;
            font-size: 14px;
        }

            .ContentPlaceHolder1_ContentPlaceHolder1_trvFolders_0:hover {
                color: rgb( 0, 114, 255);
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
        <li><a href="../admin/manageFolders.aspx?CODEN=14"><%= (Session["lang"].ToString() == "0") ? "Folders" : "المجلدات"%></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="white-holder" runat="server" id="divList" style="padding-top: 15px;">
        <div class="control-side-holder">
            <div class="start-side">
                <a class="btn-main" runat="server" onserverclick="btnAdd_ServerClick">
                    <div class="btn-main-wrapper">
                        <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                            <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Add Main " : "اضافة مجلد رئيسي "%>
                    </div>
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <asp:TreeView ID="trvFolders" runat="server"
                    OnTreeNodePopulate="trvFolders_TreeNodePopulate"
                    OnSelectedNodeChanged="trvFolders_SelectedNodeChanged" Font-Size="14px">
                </asp:TreeView>
            </div>
        </div>
    </div>
    <div id="divDetails" runat="server" visible="false" class="white-holder">
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title">
                        <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add New Folder"
                            CssClass="formModeTitleCSS"></asp:Label>
                    </div>
                </div>
            </div>
            <%--    <div class="col-xs-4"></div>--%>
            <div class="col-xs-4" style="display: none">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label1" runat="server" Text="Folder ID"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtFldrID" runat="server" CssClass="main-input" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label2" runat="server" Text="Folder Name (English)"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtFldrName" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtFldrName" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-xs-4"></div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblFldrNameAr" runat="server" Text="Folder Name (Arabic)"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtFldrNameAr" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtFldrNameAr" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label3" runat="server" Text="Parent Folder"></asp:Label>
                    </label>
                    <asp:DropDownList ID="drpFldrParentID" runat="server" CssClass="new-drop" AutoPostBack="True"
                        OnSelectedIndexChanged="drpFldrParentID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-4"></div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label5" runat="server" Text="Folder Order"></asp:Label>
                    </label>
                    <asp:DropDownList ID="drpFolderOrder" runat="server" CssClass="new-drop">
                    </asp:DropDownList>
                    <div style="display: none;">
                        <asp:CheckBox ID="chkIsDiwan" Text="Is Diwan folder" runat="server" />
                    </div>
                </div>
            </div>

            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label4" runat="server" Text="Default Form"></asp:Label>
                    </label>
                    <asp:DropDownList ID="drpDefaultDocTypID" runat="server" CssClass="new-drop">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-xs-4"></div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <%= (Session["lang"].ToString() == "0") ? "Folder Owner" : "مالك المجلد"%>
                    </label>
                    <asp:DropDownList ID="dropFolderOwnr" runat="server" CssClass="new-drop">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-4" style="display: none">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <%= (Session["lang"].ToString() == "0") ? "Icon" : "ايقونة"%>
                    </label>
                    <asp:DropDownList ID="drpIconID" onchange="showIcon(this)" runat="server" CssClass="new-drop">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="col-xs-4" style="display: none">
                <div class="main-field-holder required_input" style="margin-top: 40px;">
                    <img runat="server" clientidmode="Static" id="imgIcon" alt="" src="" style="visibility: hidden; height: 32px; width: 32px" />
                </div>
            </div>
            <div class="col-xs-4" style="display: none"></div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input" style="margin-top: 40px;">
                    <asp:CheckBox ID="chkAllowWF" Text="Allow Workflow" runat="server" />
                </div>
            </div>
            <div class="col-xs-4"></div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input" style="margin-top: 40px;">
                    <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                </div>
            </div>

            <div class="col-xs-4" style="display: none">
                <div class="main-field-holder required_input">
                    <asp:RadioButtonList ID="rdoSaveMethod" runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">New Folder</asp:ListItem>
                        <asp:ListItem Value="1">Exsit Folder</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

        </div>

        <div runat="server" id="divForUsersGroups" visible="false">
            <hr class="my-hr">
            <!-- programs -->
            <div class="max-width-holder">
                <div class="col-xs-6" style="margin-right: -155px;">
                    <div class="col-xs-4">
                        <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Groups :" : "المجموعات"%> </label>
                    </div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">

                            <div class="input-with-btn">
                                <asp:DropDownList ID="drpGrpID" CssClass="new-drop" runat="server">
                                </asp:DropDownList>
                                <a class="btn-main" id="btnAddGroup" causesvalidation="false" runat="server" onclick="return CheckDrpGroupId();" onserverclick="btnAddGroup_Click">
                                    <div class="btn-main-wrapper">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                            <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff">
                                            </path>
                                        </svg>
                                        <%= (Session["lang"].ToString() == "0") ? "Add" : " اضافة"%>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div>
                            <br>
                            <asp:GridView ID="grdGroups" runat="server" CssClass="table my-table"
                                GridLines="None" AutoGenerateColumns="False"
                                OnRowDeleting="grdGroups_RowDeleting"
                                OnRowCancelingEdit="grdGroups_RowCancelingEdit"
                                OnRowEditing="grdGroups_RowEditing" OnRowUpdating="grdGroups_RowUpdating">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:BoundField DataField="grpID" HeaderText="ID" ReadOnly="true" />
                                    <asp:BoundField DataField="grpDesc" HeaderText="Group Name" ReadOnly="true" />
                                    <asp:CheckBoxField DataField="allowInsert" HeaderText="Insert" />
                                    <asp:CheckBoxField DataField="allowUpdate" HeaderText="Update" />
                                    <asp:CheckBoxField DataField="allowDelete" HeaderText="Delete" />
                                    <asp:CommandField ShowEditButton="True" ButtonType="Image"
                                        EditImageUrl="~/Images/editicon2.png"
                                        CancelImageUrl="~/Images/Icons/Actions-stop-icon.png"
                                        UpdateImageUrl="~/Images/Icons/action-save-icon.png" />
                                    <asp:CommandField DeleteText="Remove"
                                        ButtonType="Image" DeleteImageUrl="~/Images/deleteicon1.png"
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
                    </div>
                      <div class="col-xs-4"></div>
                </div>
                <div class="col-xs-6" style="margin-right: 155px;">
                    <div class="col-xs-4">
                        <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Users" : "المستخدمين "%> </label>
                    </div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">

                            <div class="input-with-btn">
                                <asp:DropDownList ID="drpUserID" CssClass="new-drop" runat="server">
                                </asp:DropDownList>
                                <a class="btn-main" id="btnAddUser" causesvalidation="false" runat="server" onclick="return CheckDrpUserID();" onserverclick="btnAddUser_Click">
                                    <div class="btn-main-wrapper">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                            <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff">
                                            </path>
                                        </svg>
                                        <%= (Session["lang"].ToString() == "0") ? "Add" : " اضافة"%>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div>
                            <br>
                            <asp:GridView ID="grdUsers" runat="server" CssClass="table my-table"
                                GridLines="None" AutoGenerateColumns="False"
                                OnRowDeleting="grdUsers_RowDeleting"
                                OnRowCancelingEdit="grdUsers_RowCancelingEdit"
                                OnRowEditing="grdUsers_RowEditing" OnRowUpdating="grdUsers_RowUpdating">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:BoundField DataField="userID" HeaderText="ID" ReadOnly="true" />
                                    <asp:BoundField DataField="fullName" HeaderText="Full Name" ReadOnly="true" />
                                    <asp:CheckBoxField DataField="allow" HeaderText="View" />
                                    <asp:CheckBoxField DataField="allowInsert" HeaderText="Insert" />
                                    <asp:CheckBoxField DataField="allowUpdate" HeaderText="Update" />
                                    <asp:CheckBoxField DataField="allowDelete" HeaderText="Delete" />
                                    <asp:CommandField ShowEditButton="True" ButtonType="Image"
                                        EditImageUrl="~/Images/editicon2.png"
                                        CancelImageUrl="~/Images/Icons/Actions-stop-icon.png"
                                        UpdateImageUrl="~/Images/Icons/action-save-icon.png" />
                                    <asp:CommandField DeleteText="<svg xmlns='http://www.w3.org/2000/svg' width='11.963' height='11.963' viewBox='0 0 11.963 11.963'> <g id='Group_21' data-name='Group 21' transform='translate(5.981 -3.153) rotate(45)'> <line id='Line_28' data-name='Line 28' y2='12.918' transform='translate(6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> <line id='Line_29' data-name='Line 29' x2='12.918' transform='translate(0 6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> </g> </svg>"
                                        ButtonType="Image" DeleteImageUrl="~/Images/deleteicon1.png"
                                        ShowDeleteButton="True" />
                                    <%--         <asp:CommandField DeleteText="<svg xmlns='http://www.w3.org/2000/svg' width='11.963' height='11.963' viewBox='0 0 11.963 11.963'> <g id='Group_21' data-name='Group 21' transform='translate(5.981 -3.153) rotate(45)'> <line id='Line_28' data-name='Line 28' y2='12.918' transform='translate(6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> <line id='Line_29' data-name='Line 29' x2='12.918' transform='translate(0 6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> </g> </svg>"
                                ButtonType="Link"
                                ShowDeleteButton="True" ControlStyle-CssClass="tr-remove" />--%>
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
                    </div>
                    <div class="col-xs-4"></div>
                </div>

            </div>

            <!-- programs -->



        </div>

        <div class="control-side-holder control-side-holder-footer">
            <div class="start-side">
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnSave_Click" CssClass="btn-main">
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
                <a class="btn-main" runat="server" data-toggle="modal" data-target="#remove-confirm" id="btnDelete">
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
    <div style="display: none;">
        <br />
        <div id="tblDetailsForm" style="display: none" runat="server" clientidmode="Static">
            <asp:HiddenField ID="hdnActivePanel" ClientIDMode="Static" runat="server" Value="0" />
            <div id="accordion" style="width: 100%">
                <h3 onclick="changeActive(0)"><a href="#"><%= (Session["lang"].ToString() == "0") ? "Companies" : "الشركات"%></a></h3>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblCompany" runat="server" Text="Company:"></asp:Label>
                                <asp:DropDownList ID="drpCompanyID" runat="server">
                                </asp:DropDownList>
                                &nbsp;<asp:LinkButton ID="btnAddCompany" runat="server"
                                    OnClick="btnAddCompany_Click"> <img src="../Images/Icons/Actions-list-add-icon.png" border="0" align="absmiddle" /><%= (Session["lang"].ToString() == "0") ? "Add" : "اضافة"%></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" valign="top">
                                <asp:GridView ID="grdFolderCompanies" runat="server"
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                    GridLines="None" OnRowDeleting="grdFolderCompanies_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="companyID" HeaderText="ID" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                        <asp:CommandField DeleteText="Remove"
                                            ButtonType="Image" DeleteImageUrl="../Images/Icons/Actions-stop-icon.png"
                                            ShowDeleteButton="True" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <h3 onclick="changeActive(1)" style="display: none;"><a href="#"><%= (Session["lang"].ToString() == "0") ? "Branches" : "الفروع"%></a></h3>
                <div>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label8" runat="server" Text="Company:"></asp:Label>
                                &nbsp;<asp:DropDownList ID="drpCompanyID0" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpCompanyID_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;<asp:Label ID="Label9" runat="server" Text="Branch:"></asp:Label>
                                &nbsp;<asp:DropDownList ID="drpBranchID" runat="server">
                                </asp:DropDownList>
                                &nbsp;<asp:LinkButton ID="lnkAddBranch" runat="server" OnClick="lnkAddBranch_Click"> <img src="../Images/Icons/Actions-list-add-icon.png" border="0" align="absmiddle" /><%= (Session["lang"].ToString() == "0") ? "Add" : "اضافة"%></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdBranches" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" GridLines="None"
                                    OnRowDeleting="grdBranches_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="branchID" HeaderText="ID" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                        <asp:BoundField DataField="branchName" HeaderText="Branch Name" />
                                        <asp:CommandField DeleteText="Remove"
                                            ButtonType="Image" DeleteImageUrl="../Images/Icons/Actions-stop-icon.png"
                                            ShowDeleteButton="True" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <h3 onclick="changeActive(2)"><a href="#"><%= (Session["lang"].ToString() == "0") ? "Groups :" : "المجموعات"%></a></h3>

                <h3 onclick="changeActive(3)"><a href="#"><%= (Session["lang"].ToString() == "0") ? "Users :" : "المستخدمين :"%></a></h3>
                <div>
                    <table>
                    </table>
                </div>
            </div>

        </div>
        <br />
    </div>
    <script>
        $(function () {
            $(".pages_nav").find('li-dynamic').remove();
            if ($('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails:visible').length > 0) {
                if ($("#ContentPlaceHolder1_ContentPlaceHolder1_txtFldrID").val() == "") {
                    var txt = lang == 'ar' ? "اضافة المجلد" : "Add Folder";
                    var li = '<li class="li-dynamic"><a href="#">' + txt + '</a></li>';
                    $(".pages_nav").append(li);
                }
                else {
                    var txt = lang == 'ar' ? $("#ContentPlaceHolder1_ContentPlaceHolder1_txtFldrNameAr").val() : $("#ContentPlaceHolder1_ContentPlaceHolder1_txtFldrName").val();
                    var li = '<li class="li-dynamic"><a href="#">' + txt + '</a></li>';
                    $(".pages_nav").append(li);
                }
            }
        })
        function CheckDrpGroupId() {
            debugger;
            if ($("#<%=drpGrpID.ClientID%>").val() > 0) {
                return true;
            }

            $("#<%=drpGrpID.ClientID%>").css("border-color", "red");
            return false;

        }
        function CheckDrpUserID() {
            debugger;
            if ($("#<%=drpUserID.ClientID%>").val() > 0) {
                return true;
            }
            $("#<%=drpUserID.ClientID%>").css("border-color", "red");
            return false;


        }

    </script>
</asp:Content>

