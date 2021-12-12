<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.Master" AutoEventWireup="true" CodeBehind="positionsManage.aspx.cs" Inherits="dms.Admin.positionsManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        a:active, a:focus, a:hover {
            outline: none !important;
        }

        :active, :focus , :hover {
            outline: none !important;
            -moz-outline-style: none !important;
        }

        a:active, a:focus,a:hover {
            outline: 0 !important;
            border: none !important;
            -moz-outline-style: none !important; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="../screen/subIcons.aspx?CODEN=2&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? "Settings" : "الاعدادات"%> </a></li>
        <li><a href="../admin/positionsManage.aspx?CODEN=10"><%= (Session["lang"].ToString() == "0") ? "Job Titles" : "المسميات الوظيفية"%></a></li>
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
            <asp:ListView ID="dlPositions" runat="server" CausesValidation="false" DataKeyNames="PositionID" OnSelectedIndexChanging="dlPositions_SelectedIndexChanging">
                <ItemTemplate>
                    <div class="col-xs-3">
                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("PositionID") %>' CausesValidation="false">
                        <div class="select-setting-item-holder">
                            <div class="select-setting-icon">
                                <span><%# (Session["lang"].ToString() == "0") ? SafeSmartSubstring(Eval("PositionTitle").ToString()) : SafeSmartSubstring(Eval("PositionTitleAr").ToString()) %>  </span>
                            </div>
                            <p class="select-setting-title"><%# (Session["lang"].ToString() == "0") ? Eval("PositionTitle") : Eval("PositionTitleAr")%> </p>
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
                        <asp:Label ID="lblPositionID" runat="server" Text="Position ID"></asp:Label></label>
                    <asp:TextBox ID="txtPositionID" runat="server" CssClass="main-input" ReadOnly="True"></asp:TextBox>

                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblPositionTitle" runat="server"
                            Text="Position Title (English)"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtPositionTitle" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtPositionTitle" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblPositionTitleAr" runat="server"
                            Text="Position Title (Arabic)"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtPositionTitleAr" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtPositionTitleAr"
                        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-xs-4" style="display: none;">
                <div class="main-field-holder required_input">
                    <asp:RadioButtonList ID="rdoSaveMethod" runat="server"
                        RepeatDirection="Horizontal" ClientIDMode="Static">
                        <asp:ListItem Selected="True" Value="0">New Position</asp:ListItem>
                        <asp:ListItem Value="1">Exsit Position</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
           
        </div>
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
    <asp:GridView ID="grdPositions" runat="server" AutoGenerateColumns="False"
        CellPadding="4" GridLines="None"
        OnSelectedIndexChanged="grdPositions_SelectedIndexChanged"
        ForeColor="#333333" Visible="false">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="PositionID" HeaderText="ID" />
            <asp:BoundField DataField="PositionTitle" HeaderText="Position Name" />
            <asp:CommandField SelectText="Edit" ShowSelectButton="True" ButtonType="Image"
                SelectImageUrl="~/Images/icons/file-edit-icon.png" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5C5C5C" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Content>

