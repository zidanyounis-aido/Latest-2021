<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.Master" AutoEventWireup="true" CodeBehind="companisManage.aspx.cs" Inherits="dms.Admin.companisManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">

    <ul class="pages_nav">
        <li><a href="../screen/subIcons.aspx?CODEN=2&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? "Settings" : "الاعدادات"%> </a></li>
        <li><a href="../admin/companisManage.aspx?CODEN=7&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? " Companies" : "الشركات "%></a></li>
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
            <asp:ListView ID="grdCompanies" runat="server" CausesValidation="false" DataKeyNames="CompanyName" OnSelectedIndexChanging="dl_SelectedIndexChanging">
                <ItemTemplate>
                    <div class="col-xs-3">
                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("companyID") %>' CausesValidation="false">
                        <div class="select-setting-item-holder">
                            <div class="select-setting-icon">
                                <span><%# (Session["lang"].ToString() == "0") ? SafeSmartSubstring(Eval("CompanyName").ToString()) : SafeSmartSubstring(Eval("CompanyNameAr").ToString())%></span>
                            </div>
                            <p class="select-setting-title"><%# (Session["lang"].ToString() == "0") ? Eval("CompanyName") : Eval("CompanyNameAr")%> </p>
                        </div>
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <table style="width: 100%;" border="0" id="tblEditForm">

        <tr>

            <td>
                <div style="display: none">
                    <asp:RadioButtonList ID="rdoSaveMethod" ClientIDMode="Static" runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">
               New Company</asp:ListItem>
                        <asp:ListItem Value="1">
               Exsit Company</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </td>
        </tr>
    </table>
    <div id="divDetails" runat="server" visible="false" class="white-holder">
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title">
                        <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add New Company"
                            CssClass="formModeTitleCSS"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-xs-4" style="display:none;">
                <div class="main-field-holder ">
                    <label class="main-label">
                        <asp:Label ID="lblCompanyID" runat="server" Text="ID"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtCompanyID" runat="server" CssClass="main-input" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder ">
                    <label class="main-label">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Company Name (English)"></asp:Label></td>
                    </label>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="main-input"></asp:TextBox>
                    <%--<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server"
                        ControlToValidate="txtCompanyName" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder ">
                    <label class="main-label">
                        <asp:Label ID="lblCompanyNameAr" runat="server"
                            Text="Company Name (Arabic)"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtCompanyNameAr" runat="server" CssClass="main-input"></asp:TextBox>
                    <%--<asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server"
                        ControlToValidate="txtCompanyNameAr" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-xs-4" style="display: none;">
                <div class="main-field-holder ">
                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>

                    <asp:TextBox ID="txtAddress" runat="server" Width="200px"></asp:TextBox>

                    <asp:Label ID="lblZipcode" runat="server" Text="Zipcode"></asp:Label>

                    <asp:TextBox ID="txtZipcode" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder ">
                    <label class="main-label">
                        <asp:Label ID="lblTel1" runat="server" Text="Phone Number 1"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtTel1" runat="server" CssClass="main-input"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder ">
                    <label class="main-label">
                        <asp:Label ID="lblTel2" runat="server" Text="Phone Number 2"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtTel2" runat="server" CssClass="main-input"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder ">
                    <label class="main-label">
                        <asp:Label ID="lblMainEmail" runat="server" Text="Main Email"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtMainEmail" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ErrorMessage="RegularExpressionValidator" ControlToValidate="txtMainEmail"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder ">
                    <label class="main-label">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="main-input"></asp:TextBox>
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
                &nbsp;<asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
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
                <a class="btn-main" runat="server"  data-toggle="modal" data-target="#remove-confirm" id="btnDelete">
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
    <table id="tblDetailsForm" style="display: none" runat="server" clientidmode="Static">
        <tr>
            <td colspan="3">
                <h3>
                    <%= (Session["lang"].ToString() == "0") ? "Allowed Folders" : "المجلدات المسموحة"%> 
                </h3>
            </td>
        </tr>
        <tr>
            <td>
                <%= (Session["lang"].ToString() == "0") ? "Select Folder" : "اختر مجلد"%> 
                
            </td>
            <td>
                <asp:DropDownList ID="drpFolders" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton Style="padding-bottom: 10px;" CssClass="btnSave" ID="btnAddFolder" runat="server" OnClick="btnAddFolder_Click" CausesValidation="False"> <img src="../Images/Add-icon.png" border="0" align="middle" alt="Add New" />
                <%= (Session["lang"].ToString() == "0") ? "Add" : "إضافة"%> 
                </asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="3">

                <asp:GridView ID="grdCompanyFolders" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None" AutoGenerateColumns="False"
                    OnRowDeleting="grdCompanyFolders_RowDeleting">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="fldrID" HeaderText="ID" />
                        <asp:BoundField DataField="fldrName" HeaderText="Folder Name" />
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
    <div class="modal fade my-modal" id="remove-confirm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="background-color: rgba(0,0,0,0.4);">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= (Session["lang"].ToString() == "0") ? "Are you sure to delete?" : "هل أنت متأكد من الحذف ؟"%></h4>
                </div>
                <!-- <div class="modal-body">
                            </div> -->
                <div class="modal-footer">
                    <button type="button" class="btn-done-model" runat="server" onserverclick="btnDelete_ServerClick"><%= (Session["lang"].ToString() == "0") ? "Ok" : "نعم"%></button>
                    <button type="button" class="btn-close-model" data-dismiss="modal">
                        <span class="icon-close">
                            <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963"
                                viewBox="0 0 11.963 11.963">
                                <g id="Group_21" data-name="Group 21"
                                    transform="translate(5.981 -3.153) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)"
                                        fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2">
                                    </line>
                                    <line id="Line_29" data-name="Line 29" x2="12.918"
                                        transform="translate(0 6.459)" fill="none" stroke="#fff"
                                        stroke-linecap="round" stroke-width="2">
                                    </line>
                                </g>
                            </svg>
                        </span>
                        <%= (Session["lang"].ToString() == "0") ? "Cancel" : "تراجع"%>
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

