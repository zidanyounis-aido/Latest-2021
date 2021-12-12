<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.Master" AutoEventWireup="true" CodeBehind="manageGroups.aspx.cs" Inherits="dms.Admin.manageGroups" %>
<%@ Register Src="../Controls/folderTree.ascx" TagName="folderTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        .radio-input-holder label {
        
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="../screen/subIcons.aspx?CODEN=2&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? "Settings" : "الاعدادات"%> </a></li>
        <li><a href="../admin/manageGroups.aspx?CODEN=11"><%= (Session["lang"].ToString() == "0") ? "Manage Groups" : " إدارة المجموعات"%></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <p class="select-setting-title">إضافة جديد</p>
                    </div>
                </a>
            </div>
            <asp:ListView ID="dlgroups" runat="server" CausesValidation="false" DataKeyNames="grpID" OnSelectedIndexChanging="dl_SelectedIndexChanging">
                <ItemTemplate>
                    <div class="col-xs-3">
                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("grpID") %>' CausesValidation="false">
                        <div class="select-setting-item-holder">
                            <div class="select-setting-icon">
                                <span><%# (Session["lang"].ToString() == "0") ? SafeSmartSubstring(Eval("grpDesc").ToString()) : SafeSmartSubstring(Eval("grpDesc").ToString()) %>  </span>
                            </div>
                            <p class="select-setting-title"><%# (Session["lang"].ToString() == "0") ? Eval("grpDesc") : Eval("grpDesc")%> </p>
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
            <div class="col-xs-4" style="display:none;">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label1" runat="server" Text="Group ID"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtGrpID" runat="server" CssClass="main-input" BorderColor="Gray"
                        BorderStyle="Solid" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label2" runat="server" Text="Group Description"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtGrpDesc" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtGrpDesc" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label3" runat="server" Text="Department"></asp:Label>
                    </label>
                    <div style="display: none">
                        <asp:RadioButtonList ID="rdoSaveMethod" ClientIDMode="Static" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">New Group</asp:ListItem>
                            <asp:ListItem Value="1">Exsit Group</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <asp:DropDownList ID="drpCompanyID" CssClass="new-drop" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="Label5" runat="server" Text="Sections"></asp:Label>
                    </label>
                    <asp:DropDownList ID="drpBranchID" runat="server" CssClass="new-drop">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <hr class="my-hr">
        <!-- folders -->
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title"><%= (Session["lang"].ToString() == "0") ? "Allowed Folders" : "المجلدات المسموحة"%> </div>
                </div>
            </div>
            <div class="col-xs-4">
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder">
                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Folder" : "مجلد"%>  </label>
                    <div class="input-with-btn">
                        <asp:DropDownList ID="drpFolders" runat="server" Visible="False">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtFldrID" CssClass="main-input" ClientIDMode="Static" onClick="showFolderDialog()" ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hdnFldrID" ClientIDMode="Static" runat="server" />

                    </div>
                </div>
                <div class="main-field-holder">
                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Documents Permissions" : "ضوابط الملفات"%> </label>
                    <div class="radio-input-holder">
                        <asp:CheckBox ID="chkAllowInsert" runat="server" Text="Insert" />
                        <%--  <label for="add1">اضافة</label>--%>
                    </div>
                    <div class="radio-input-holder">
                        <asp:CheckBox ID="chkAllowUpdate" runat="server" Text="Updates" />
                    </div>
                    <div class="radio-input-holder">
                        <asp:CheckBox ID="chkAllowDelete" runat="server" Text="Delete" />
                    </div>
                </div>
                
                <div class="main-field-holder">
                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Sub Folders Permissions" : "ضوابط المجلدات الفرعية"%> </label>
                    <div class="radio-input-holder">
                        <asp:CheckBox ID="chkAllowOutgoing" runat="server" Text="Create Outgoing" />

                    </div>
                    <div class="radio-input-holder">
                        <asp:CheckBox ID="chkAllowIncoming" runat="server" Text="Add Incoming" />

                    </div>

                    <div class="radio-input-holder">
                        <asp:CheckBox ID="chkInheritSubFolders" runat="server" Text="Inhiret Subs" />
                    </div>
                </div>
                <div class="main-field-holder">
                    <a class="btn-main" id="btnAddFolder" causesvalidation="false" runat="server" onserverclick="btnAddFolder_Click">
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

        </div>
        <div class="">
            <br>
            <asp:GridView ID="grdFolders" CssClass="table my-table" runat="server" AutoGenerateColumns="False"
                GridLines="None"
                OnRowDeleting="grdFolders_RowDeleting"
                OnRowCancelingEdit="grdFolders_RowCancelingEdit"
                OnRowEditing="grdFolders_RowEditing" OnRowUpdating="grdFolders_RowUpdating">
                <AlternatingRowStyle  />
                <Columns>
                    <asp:BoundField DataField="fldrID" HeaderText="Folder ID" ReadOnly="True" />
                    <asp:BoundField DataField="fldrName" HeaderText="Folder Name" ReadOnly="True" />
                    <asp:CheckBoxField DataField="allowInsert" HeaderText="Insert" />
                    <asp:CheckBoxField DataField="allowUpdate" HeaderText="Update" />
                    <asp:CheckBoxField DataField="allowDelete" HeaderText="Delete" />
                    <asp:CheckBoxField DataField="allowOutgoing" HeaderText="Create Outgoing" />
                    <asp:CheckBoxField DataField="allowIncoming" HeaderText="Add Incoming" />
                    <asp:CheckBoxField DataField="inheritSubFolders"
                        HeaderText="inherit Sub Folders" />
                    <asp:CommandField ButtonType="Link"
                        EditText="<svg xmlns='http://www.w3.org/2000/svg' width='22.506' height='22.506' viewBox='0 0 22.506 22.506'> <path id='Path_6947' data-name='Path 6947' d='M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z' transform='translate(-0.001)' fill='#0072ff'></path> </svg>" ShowEditButton="True"
                       ControlStyle-CssClass="tr-edit"  />
                    <asp:CommandField DeleteText="<svg xmlns='http://www.w3.org/2000/svg' width='11.963' height='11.963' viewBox='0 0 11.963 11.963'> <g id='Group_21' data-name='Group 21' transform='translate(5.981 -3.153) rotate(45)'> <line id='Line_28' data-name='Line 28' y2='12.918' transform='translate(6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> <line id='Line_29' data-name='Line 29' x2='12.918' transform='translate(0 6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> </g> </svg>"
                        ButtonType="Link" ControlStyle-CssClass="tr-remove"
                        ShowDeleteButton="True" />
                </Columns>
                <EditRowStyle />
                <FooterStyle  />
                <HeaderStyle  />
                <PagerStyle  HorizontalAlign="Center" />
                <RowStyle />
                <SelectedRowStyle  />
                <SortedAscendingCellStyle />
                <SortedAscendingHeaderStyle  />
                <SortedDescendingCellStyle  />
                <SortedDescendingHeaderStyle  />
            </asp:GridView>
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        </div>
        <hr class="my-hr">
        <!-- programs -->
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title"><%= (Session["lang"].ToString() == "0") ? "Allowed Programs" : "البرامج المسموحة"%>  </div>
                </div>
            </div>
            <div class="col-xs-4">
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder">
                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Program" : "البرامج "%> </label>
                    <div class="input-with-btn">
                        <asp:DropDownList ID="drpProgramID" CssClass="new-drop" runat="server">
                        </asp:DropDownList>
                        <a class="btn-main" id="lnkAddProgram" causesvalidation="false" runat="server" onserverclick="lnkAddProgram_Click">
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
                        <EditRowStyle  />
                        <FooterStyle  />
                        <HeaderStyle />
                        <PagerStyle  />
                        <RowStyle  />
                        <SelectedRowStyle  />
                        <SortedAscendingCellStyle  />
                        <SortedAscendingHeaderStyle  />
                        <SortedDescendingCellStyle  />
                        <SortedDescendingHeaderStyle  />
                    </asp:GridView>
                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                </div>
            </div>

        </div>
        <!-- footer input -->
        <div class="control-side-holder control-side-holder-footer">
            <div class="start-side">
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton1_Click" CssClass="btn-main">
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
                        تراجع
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
                        <span id="lblSurvey1" runat="server">مسح</span>
                    </div>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="OutOfDesign">
    <div id="uiDialog" style="background: #ffffff" title="Select a Folder" class="ui-widget-content">
        <uc1:folderTree ID="folderTree1" runat="server" />
    </div>
</asp:Content>
