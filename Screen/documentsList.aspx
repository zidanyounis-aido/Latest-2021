<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DocumentsMaster.master" AutoEventWireup="true" CodeBehind="documentsList.aspx.cs" Inherits="dms.Screen.documentsList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_trvFoldersn0Nodes table tbody tr td {
            padding:4px 0px 4px 0px !important;
        }
        div#ContentPlaceHolder1_trvFolders img {
            width: 13px;
            margin-left: 7px;
            margin-bottom: 8px;
        }

        div#ContentPlaceHolder1_trvFolders a {
            color: #242424;
            font-size: 14px;
        }

        .new-drop {
            padding: 4px;
            border-radius: 20px;
        }

        .my-table tbody tr td a.tr-remove:hover {
            background-color:red !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTitle" runat="server">
    <asp:Image ID="fldrIcon" runat="server" ImageAlign="AbsMiddle"
        ImageUrl="../Images/Icons/folder-documents-icon.png" Visible="false" />
    &nbsp;
        <ul class="pages_nav">
            <li><a style="cursor:pointer;" onclick="location.href='defaultAr.aspx?CODEN=1&dlgid=2&indexId=0'"><%= (Session["lang"].ToString() == "0") ? "Folders" : "المجلدات"%></a></li>
            <%--  <li><a href="#">مديرعام</a></li>--%>
            <li>
                <a>
                    <asp:Literal ID="lblFolderName" runat="server" Text="Label"></asp:Literal></a>
            </li>
        </ul>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="control-side-holder">
        <div class="start-side">
            <asp:HyperLink ID="lnkAddDoc" runat="server" CssClass="btn-main">
                <div class="btn-main-wrapper">
                    <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                        <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff" />
                    </svg>
                  <%= (Session["lang"].ToString() == "0") ? "Add New " : "اضافة  جديد"%> 
                    </div>
            </asp:HyperLink>
            <asp:HyperLink ID="linkAddOutcome" runat="server" CssClass="btn-main">
                <div class="btn-main-wrapper">
                    <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                        <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff" />
                    </svg>
                  <%= (Session["lang"].ToString() == "0") ? "Add Outcoming Document " : "اضافة  ملف وارد"%> 
                    </div>
            </asp:HyperLink>

            <div class="search-main">
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search"></asp:TextBox>
                <div class="btn-search-main" onclick="document.getElementById('<%= lnkSearch.ClientID %>').click()">
                    <svg xmlns="http://www.w3.org/2000/svg" width="17.293" height="17.293" viewBox="0 0 17.293 17.293">
                        <g id="search" transform="translate(0.5 0.5)">
                            <circle id="Oval" cx="7.149" cy="7.149" r="7.149" fill="none" stroke="#b2b2b2" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1"></circle>
                            <path id="Path" d="M3.888,3.888,0,0" transform="translate(12.199 12.199)" fill="none" stroke="#b2b2b2" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1"></path>
                        </g>
                    </svg>
                </div>
            </div>
            <asp:HyperLink ID="lnkAdvanceSearch" runat="server" CssClass="btn-main" NavigateUrl="~/Screen/advancedSearch.aspx"><i class="fas fa-search-plus"></i> <%= (Session["lang"].ToString() == "0") ? "Advanced Search" : "بحث متقدم"%></asp:HyperLink>
        </div>

        <div class="end-side">
            <div class="dropdown-main dropdown">
                <asp:DropDownList CssClass="new-drop" ID="drpPager1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPager1_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="dropdown-main dropdown">
                <asp:DropDownList CssClass="new-drop" ID="ddlStatusFilter" runat="server" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="in process" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Archived" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Late" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="dropdown-main dropdown">
                <asp:DropDownList CssClass="new-drop" ID="drpSortBy" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpSortBy_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="docID">Serial</asp:ListItem>
                    <asp:ListItem Value="docName">Document Name</asp:ListItem>
                    <asp:ListItem Value="docTypID">Document Type</asp:ListItem>
                    <asp:ListItem Value="addedDate">Added Date</asp:ListItem>
                    <asp:ListItem Value="addedUserID">Added User</asp:ListItem>
                    <asp:ListItem Value="modifyDate">Modify Date</asp:ListItem>
                </asp:DropDownList>
            </div>
            <a class="btn-main" runat="server" causesvalidation="false" id="btnexportexcel" onserverclick="btnexportexcel_ServerClick">
                <div class="btn-main-wrapper">
                    <svg xmlns="http://www.w3.org/2000/svg" width="12.728" height="16.477" viewBox="0 0 12.728 16.477" onclick="fnExcelReport();">
                        <g id="surface1" transform="translate(-58.885 0.998)">
                            <path id="Path_7050" data-name="Path 7050" d="M338.5,19.06h2.14l-3.269-3.43v2.306A1.123,1.123,0,0,0,338.5,19.06Z" transform="translate(-269.502 -16.092)" fill="#fff" />
                            <path id="Path_7051" data-name="Path 7051" d="M67.167,1.844V-1h-6.7A1.594,1.594,0,0,0,58.885.584V13.9a1.594,1.594,0,0,0,1.587,1.582h9.555A1.594,1.594,0,0,0,71.613,13.9V3.666h-2.62A1.822,1.822,0,0,1,67.167,1.844Zm-4.708,5.68a.35.35,0,0,1,.493.017L64.9,9.634V4.656a.349.349,0,1,1,.7,0V9.638l1.949-2.092a.349.349,0,1,1,.51.475L65.5,10.763a.349.349,0,0,1-.257.109.356.356,0,0,1-.257-.109L62.433,8.021A.365.365,0,0,1,62.459,7.524Zm6.726,5.3a.35.35,0,0,1-.349.349H61.662a.349.349,0,0,1,0-.7h7.179A.346.346,0,0,1,69.185,12.82Z" fill="#fff" />
                        </g>
                    </svg>
                    <%= (Session["lang"].ToString() == "0") ? "Export" : "تصدير"%>
                </div>
            </a>

        </div>
    </div>
    <div id="grdDocumentsDiv">
        <asp:GridView ID="grdDocuments" CssClass="table my-table" ClientIDMode="Static" runat="server" AllowPaging="False"
            AutoGenerateColumns="False"
            GridLines="Vertical" OnPageIndexChanging="grdDocuments_PageIndexChanging"
            OnSelectedIndexChanged="grdDocuments_SelectedIndexChanged"
            OnRowDeleting="grdDocuments_RowDeleting">
            <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="dataId" DataField="docID" HeaderText="Serial" ItemStyle-Width="65px" HeaderStyle-Width="65px" />
                <asp:TemplateField HeaderText="Document Name" ItemStyle-Width="200px" HeaderStyle-Width="200px">
                    <ItemTemplate>
                         <%# Eval("typeId").ToString() == "0" ? "":Eval("typeId").ToString() == "1" ? "<i style='color:green' class='fa fa-arrow-up'></i>" : "<i style='color:red' class='fa fa-arrow-down' ></i>" %>
                        <a href="../Screen/documentInfo.aspx?docID=<%#c.encrypt(Eval("docID").ToString()) %>" style='color: <%# Eval("Color")%>'>
                            <%# Eval("docName") %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Type" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"
                            Text='<%# getDocTypeDesc(c.convertToInt32(Eval("docTypID"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="addedDate" DataFormatString="{0:dd/MM/yyyy}"
                    HeaderText="Added Date" ItemStyle-Width="85px" HeaderStyle-Width="85px" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Added By" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"
                            Text='<%# c.getUserName(c.convertToInt32(Eval("addedUserID"))) %>'></asp:Label>
                        <asp:HiddenField ID="hdnWfStatus" runat="server"
                            Value='<%# Eval("WfStatus") %>' />
                        <asp:HiddenField ID="hdnWfStartDateTime" runat="server"
                            Value='<%# Eval("WfStartDateTime") %>' />
                        <asp:HiddenField ID="hdnWfTimeFrame" runat="server"
                            Value='<%# Eval("WfTimeFrame") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="modifyDate" DataFormatString="{0:dd/MM/yyyy}"
                    HeaderText="Modify Date" ItemStyle-Width="85px" HeaderStyle-Width="85px" />
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="Meta4" HeaderText="To" ItemStyle-Width="100px" HeaderStyle-Width="100px" Visible="false" />
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="Meta2" HeaderText="Date" ItemStyle-Width="100px" HeaderStyle-Width="100px" Visible="false" />
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="statusName" HeaderText="Status" ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="LeftTime" HeaderText="Remaining time" ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                <asp:CommandField SelectText="<svg xmlns='http://www.w3.org/2000/svg' width='22.506' height='22.506' viewBox='0 0 22.506 22.506'> <path id='Path_6947' data-name='Path 6947' d='M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z' transform='translate(-0.001)' fill='#0072ff'></path> </svg>" ShowSelectButton="True" ItemStyle-Width="25px" HeaderStyle-Width="25px"
                    ButtonType="Link" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="tr-edit" />
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="25px" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>

                        <asp:LinkButton ID="DeleteButton" runat="server" OnClientClick="return false;" Text="<svg xmlns='http://www.w3.org/2000/svg' width='11.963' height='11.963' viewBox='0 0 11.963 11.963'> <g id='Group_21' data-name='Group 21' transform='translate(5.981 -3.153) rotate(45)'> <line id='Line_28' data-name='Line 28' y2='12.918' transform='translate(6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> <line id='Line_29' data-name='Line 29' x2='12.918' transform='translate(0 6.459)' fill='none' stroke='#fff' stroke-linecap='round' stroke-width='2'></line> </g> </svg>"
                            CssClass="tr-remove" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
            <%--<HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />--%>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <%--<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
    <script src="/Assets/UIKIT/js/modules/defaultar-module.js"></script>
    <div style="display: none">
        <div class="padding-line">

            <asp:Label ID="Label3" runat="server" Text="Search : "></asp:Label>
            &nbsp;<asp:LinkButton ID="lnkSearch" runat="server" CssClass="button" OnClick="lnkSearch_Click">
<i class="fas fa-search"></i>
 <%= (Session["lang"].ToString() == "0") ? "Search" : "بحث"%></asp:LinkButton>
            &nbsp;-
            <%--    <asp:HyperLink ID="lnkAdvanceSearch" runat="server" CssClass="button"
        NavigateUrl="~/Screen/advancedSearch.aspx">
        <i class="fas fa-search-plus"></i>
        <%= (Session["lang"].ToString() == "0") ? "Advanced Search" : "بحث متقدم"%></asp:HyperLink>--%>
        </div>
        <div class="horizantalBar">
            <table>
                <tr>
                    <td></td>
                    <td style="width: 50px"></td>
                    <td valign="middle">
                        <%= (Session["lang"].ToString() == "0") ? "Sort By :" : "ترتيب حسب"%>  </td>
                    <td valign="middle"></td>
                    <td valign="middle">&nbsp;<asp:RadioButtonList ID="rdoOrderType" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="rdoOrderType_SelectedIndexChanged"
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value=" ">Ascending</asp:ListItem>
                        <asp:ListItem Value="desc" Selected="True">Descending</asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                    <td></td>
                </tr>
            </table>
            <div style="float: left">تصدير</div>
        </div>
        <div class="padding-line">
            <%= (Session["lang"].ToString() == "0") ? "Page Number " : "الصفحة"%>
     :
        
        </div>

    </div>
    <!-- Modal tr Remove-->
    <div class="modal fade my-modal" id="tr-remove" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= (Session["lang"] == "0") ? "Confirm Delete ?" : "هل أنت متأكد من الحذف ؟"%></h4>
                </div>
                <!-- <div class="modal-body">
                    </div> -->
                <div class="modal-footer">
                    <input type="hidden" id="hdnDociId" runat="server" value="0" />
                    <asp:LinkButton runat="server" CssClass="btn-done-model" CommandArgument="0" OnClick="delDocumnetRowbtn_ServerClick"  ID="lnkButtonDelete">OK</asp:LinkButton>
                    <asp:Button Visible="false" runat="server" ID="delDocumnetRowbtn1" CssClass="btn-done-model"  Text="Ok" CommandArgument="0" OnClientClick="return true;" />
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
                        <%= (Session["lang"] == "0") ? "Cancel" : "تراجع"%>
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
