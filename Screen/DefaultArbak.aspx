<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DocumentsMaster.master" AutoEventWireup="true" CodeBehind="DefaultArbak.aspx.cs" Inherits="dms.Screen.DefaultArbak" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            resizeDiv();
            window.onresize = function () {
                resizeDiv();
            }

        });
        function resizeDiv() {
            resizeTbl();
            document.getElementById("grdDocumentsDiv").style.height = String(window.innerHeight - 215) + "px";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitle" runat="server">
    <%= (Session["lang"].ToString() == "0") ? "Recent Documents" : "أحدث المستندات"%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="padding-line">
        <asp:Panel ID="Panel1" DefaultButton="lnkSearch" runat="server">
            <asp:Label ID="Label3" runat="server" Text="Search : "></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            &nbsp;
            <asp:LinkButton ID="lnkSearch" runat="server" CssClass="button" OnClick="lnkSearch_Click">
<i class="fas fa-search"></i>
<%= (Session["lang"].ToString() == "0") ? "Search" : "بحث"%></asp:LinkButton>
            &nbsp;-
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="button"
        NavigateUrl="~/Screen/advancedSearch.aspx"><i class="fas fa-search-plus"></i> <%= (Session["lang"].ToString() == "0") ? "Advanced Search" : "بحث متقدم"%></asp:HyperLink>

        </asp:Panel>
    </div>


    <div class="horizantalBar">
        <table>
            <tr>

                <td valign="middle">
                    <span class="padding-line">
                        <%= (Session["lang"].ToString() == "0") ? "Sort By :" : "الترتيب حسب"%> </span></td>
                <td valign="middle">
                    <asp:DropDownList ID="drpSortBy" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="drpSortBy_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="docID">Serial</asp:ListItem>
                        <asp:ListItem Value="docName">Document Name</asp:ListItem>
                        <asp:ListItem Value="docTypID">Document Type</asp:ListItem>
                        <asp:ListItem Value="addedDate">Added Date</asp:ListItem>
                        <asp:ListItem Value="addedUserID">Added User</asp:ListItem>
                        <asp:ListItem Value="modifyDate">Modify Date</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="middle">&nbsp;<asp:RadioButtonList ID="rdoOrderType" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="rdoOrderType_SelectedIndexChanged"
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="asc">Ascending</asp:ListItem>
                    <asp:ListItem Selected="True" Value="desc">Descending</asp:ListItem>
                </asp:RadioButtonList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatusFilter" runat="server" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="in process" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Archived" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Late" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div class="padding-line">
        <%= (Session["lang"].ToString() == "0") ? "Page Number :" : "الصفحة"%>
     :
        <asp:DropDownList ID="drpPager1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPager1_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div id="grdDocumentsDiv">
        <asp:GridView ID="grdDocuments" CssClass="grdDocuments" ClientIDMode="Static" runat="server" AllowPaging="False"
            HeaderStyle-CssClass="FixedHeader"
            AutoGenerateColumns="False"
            GridLines="Vertical" OnPageIndexChanging="grdDocuments_PageIndexChanging"
            OnSelectedIndexChanged="grdDocuments_SelectedIndexChanged"
            OnRowDeleting="grdDocuments_RowDeleting">
            <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="docID" HeaderText="Serial" ItemStyle-Width="65px" HeaderStyle-Width="65px" />
                <asp:TemplateField HeaderText="Document Name" ItemStyle-Width="200px" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <a href="../Screen/documentInfo.aspx?docID=<%#c.encrypt(Eval("docID").ToString()) %>" style='color: <%# Eval("Color")%>'>
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
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="statusName" HeaderText="Status" ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="LeftTime" HeaderText="Remaining time" ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                <asp:CommandField SelectText="<i class='fas fa-edit' style='color:blue'></i>" ShowSelectButton="True" ItemStyle-Width="25px" HeaderStyle-Width="25px"
                    ButtonType="Link" ItemStyle-HorizontalAlign="Center" />
                <asp:CommandField ShowDeleteButton="True" Visible="false" ItemStyle-Width="25px" HeaderStyle-Width="25px" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
            <%--<HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />--%>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <%--<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
            <%--<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />--%>
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="outOfForm" runat="server">
</asp:Content>

