<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DiwanMaster.master" AutoEventWireup="true" CodeBehind="DefaultDiwan.aspx.cs" Inherits="dms.Screen.DefaultDiwan" %>

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

        .my-table tbody tr td {
            border-left: none !important;
            border-right: none !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a style="cursor:pointer;" onclick="location.href='defaultAr.aspx?CODEN=1&dlgid=2&indexId=0'"><%= (Session["lang"].ToString() == "0") ? "Folders" : "المجلدات"%></a></li>
        <li><a><%= (Session["lang"].ToString() == "0") ? "Recent Documents" : "الصادر "%></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="control-side-holder">
        <div class="start-side">
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
            <asp:HyperLink ID="HyperLink1" runat="server" Style="display: none" CssClass="btn-main" NavigateUrl="~/Screen/advancedSearch.aspx"><i class="fas fa-search-plus"></i> <%= (Session["lang"].ToString() == "0") ? "Advanced Search" : "بحث متقدم"%></asp:HyperLink>
        </div>
        <div class="end-side">
            <div class="dropdown-main dropdown">
                <asp:DropDownList ID="drpPager1" CssClass="new-drop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPager1_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="dropdown-main dropdown">
                <asp:DropDownList ID="drpSortBy" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpSortBy_SelectedIndexChanged" CssClass="new-drop">
                    <asp:ListItem Selected="True" Value="docID">Serial</asp:ListItem>
                    <asp:ListItem Value="docName">Document Name</asp:ListItem>
                    <asp:ListItem Value="docTypID">Document Type</asp:ListItem>
                    <asp:ListItem Value="addedDate">Added Date</asp:ListItem>
                    <asp:ListItem Value="addedUserID">Added User</asp:ListItem>
                    <asp:ListItem Value="modifyDate">Modify Date</asp:ListItem>
                </asp:DropDownList>
            </div>
            <%--         <a class="btn-main" runat="server" causesvalidation="false" id="btnexportexcel" onserverclick="btnexportexcel_ServerClick">
                <div class="btn-main-wrapper">
                    <svg xmlns="http://www.w3.org/2000/svg" width="12.728" height="16.477" viewBox="0 0 12.728 16.477" onclick="fnExcelReport();">
                        <g id="surface1" transform="translate(-58.885 0.998)">
                            <path id="Path_7050" data-name="Path 7050" d="M338.5,19.06h2.14l-3.269-3.43v2.306A1.123,1.123,0,0,0,338.5,19.06Z" transform="translate(-269.502 -16.092)" fill="#fff" />
                            <path id="Path_7051" data-name="Path 7051" d="M67.167,1.844V-1h-6.7A1.594,1.594,0,0,0,58.885.584V13.9a1.594,1.594,0,0,0,1.587,1.582h9.555A1.594,1.594,0,0,0,71.613,13.9V3.666h-2.62A1.822,1.822,0,0,1,67.167,1.844Zm-4.708,5.68a.35.35,0,0,1,.493.017L64.9,9.634V4.656a.349.349,0,1,1,.7,0V9.638l1.949-2.092a.349.349,0,1,1,.51.475L65.5,10.763a.349.349,0,0,1-.257.109.356.356,0,0,1-.257-.109L62.433,8.021A.365.365,0,0,1,62.459,7.524Zm6.726,5.3a.35.35,0,0,1-.349.349H61.662a.349.349,0,0,1,0-.7h7.179A.346.346,0,0,1,69.185,12.82Z" fill="#fff" />
                        </g>
                    </svg>
                    <%= (Session["lang"].ToString() == "0") ? "Export" : "تصدير"%>
                </div>
            </a>--%>
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
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="docID" HeaderText="#" ItemStyle-Width="65px" HeaderStyle-Width="65px" />
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="serial" HeaderText="Code" ItemStyle-Width="65px" HeaderStyle-Width="65px" />
                <asp:TemplateField HeaderText="Document Name" ItemStyle-Width="200px" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <a href="../Screen/documentInfo.aspx?docID=<%#c.encrypt(Eval("docID").ToString()) %>" style='color: <%# Eval("Color")%>; width: 100%;'>
                            <%# Eval("docName") %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Folder" ItemStyle-Width="75px" HeaderStyle-Width="75px">
                    <ItemTemplate>
                        <asp:Label ID="lblFolderName" runat="server"
                            Text='<%# getFolderName(Eval("fldrID").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Type" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"
                            Text='<%# getDocTypeDesc(c.convertToInt32(Eval("docTypID"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="addedDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="85px" HeaderStyle-Width="85px"
                    HeaderText="Added Date" Visible="false" />

                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Added By" ItemStyle-Width="100px" HeaderStyle-Width="100px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"
                            Text='<%# c.getUserName(c.convertToInt32(Eval("addedUserID"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="modifyDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="95px" HeaderStyle-Width="95px"
                    HeaderText="Modify Date" ItemStyle-HorizontalAlign="Center" />
                <%--<asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="statusName" HeaderText="Status" ItemStyle-Width="100px" HeaderStyle-Width="100px" />--%>
                <%--<asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="LeftTime" HeaderText="Remaining time" ItemStyle-Width="100px" HeaderStyle-Width="100px" />--%>
                <asp:CommandField SelectText="<svg xmlns='http://www.w3.org/2000/svg' width='22.506' height='22.506' viewBox='0 0 22.506 22.506'> <path id='Path_6947' data-name='Path 6947' d='M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z' transform='translate(-0.001)' fill='#0072ff'></path> </svg>" ShowSelectButton="True" ItemStyle-Width="25px" HeaderStyle-Width="25px"
                    ButtonType="Link" ItemStyle-HorizontalAlign="Center" />
                <asp:CommandField ShowDeleteButton="True" Visible="false" ItemStyle-Width="25px" HeaderStyle-Width="25px" />
            </Columns>
            <EditRowStyle />
            <FooterStyle />
            <%--<HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />--%>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <%--<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
            <%--<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />--%>
            <SortedAscendingCellStyle />
            <SortedAscendingHeaderStyle />
            <SortedDescendingCellStyle />
            <SortedDescendingHeaderStyle />
        </asp:GridView>
    </div>



    <script src="/Assets/UIKIT/js/modules/defaultar-module.js"></script>
    <div style="display: none;">
        <div class="padding-line">
            <asp:Panel ID="Panel1" DefaultButton="lnkSearch" runat="server">
                <asp:Label ID="Label3" runat="server" Text="Search : "></asp:Label>
                <%--  <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>--%>
                &nbsp;<asp:LinkButton ID="lnkSearch" runat="server" CssClass="button" OnClick="lnkSearch_Click">
<i class="fas fa-search"></i>
<%= (Session["lang"].ToString() == "0") ? "Search" : "بحث"%></asp:LinkButton>
                &nbsp;-
                <%--    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="button"
        NavigateUrl="~/Screen/advancedSearch.aspx"><i class="fas fa-search-plus"></i> <%= (Session["lang"].ToString() == "0") ? "Advanced Search" : "بحث متقدم"%></asp:HyperLink>--%>
            </asp:Panel>
        </div>


        <div class="horizantalBar">
            <table>
                <tr>

                    <td valign="middle">
                        <span class="padding-line">
                            <%= (Session["lang"].ToString() == "0") ? "Sort By :" : "الترتيب حسب"%> </span></td>
                    <td valign="middle">
                        <%--    <asp:DropDownList ID="drpSortBy" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="drpSortBy_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="docID">Serial</asp:ListItem>
                            <asp:ListItem Value="docName">Document Name</asp:ListItem>
                            <asp:ListItem Value="docTypID">Document Type</asp:ListItem>
                            <asp:ListItem Value="addedDate">Added Date</asp:ListItem>
                            <asp:ListItem Value="addedUserID">Added User</asp:ListItem>
                            <asp:ListItem Value="modifyDate">Modify Date</asp:ListItem>
                        </asp:DropDownList>--%>
                    </td>
                    <td valign="middle">&nbsp;<asp:RadioButtonList ID="rdoOrderType" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="rdoOrderType_SelectedIndexChanged"
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="asc">Ascending</asp:ListItem>
                        <asp:ListItem Selected="True" Value="desc">Descending</asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div class="padding-line">
            <%= (Session["lang"].ToString() == "0") ? "Page Number :" : "الصفحة"%>
     :
        <%--<asp:DropDownList ID="drpPager1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPager1_SelectedIndexChanged"></asp:DropDownList>--%>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="outOfForm" runat="server">
</asp:Content>

