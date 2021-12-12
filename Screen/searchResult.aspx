<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DocumentsMaster.master" AutoEventWireup="true" CodeBehind="searchResult.aspx.cs" Inherits="dms.Screen.searchResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitle" runat="server">
<%= (Session["lang"].ToString() == "0") ? "Search" : "بحث"%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Label ID="Label3" runat="server" Text="Search : "></asp:Label>
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
&nbsp;<asp:LinkButton ID="lnkSearch" runat="server" CssClass="button" onclick="lnkSearch_Click">
<img border="0" src="../Images/Icons/Search-icon.png" align="absmiddle" />
<%= (Session["lang"].ToString() == "0") ? "Search" : "بحث"%></asp:LinkButton>
&nbsp;-
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="button" 
        NavigateUrl="~/Screen/advancedSearch.aspx"><%= (Session["lang"].ToString() == "0") ? "Advanced Search" : "بحث متقدم"%></asp:HyperLink>
    <br />

    <div style="width:100%; background-color:#5C5C5C; color:White">
    <table>
        <tr>

            <td valign="middle">
              <%= (Session["lang"].ToString() == "0") ? "Sort By :" : "الترتيب حسب"%>   </td><td valign="middle">
                <asp:DropDownList ID="drpSortBy" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="drpSortBy_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="docID">Serial</asp:ListItem>
                    <asp:ListItem Value="docName">Document Name</asp:ListItem>
                    <asp:ListItem Value="docTypID">Document Type</asp:ListItem>
                    <asp:ListItem Value="addedDate">Added Date</asp:ListItem>
                    <asp:ListItem Value="addedUserID">Added User</asp:ListItem>
                    <asp:ListItem Value="modifyDate">Modify Date</asp:ListItem>
                </asp:DropDownList>
                </td><td valign="middle">
            &nbsp;<asp:RadioButtonList ID="rdoOrderType" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="rdoOrderType_SelectedIndexChanged" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value=" ">Ascending</asp:ListItem>
                    <asp:ListItem Value="desc">Descending</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    </div>
    <br />

    <asp:GridView ID="grdDocuments" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" onpageindexchanging="grdDocuments_PageIndexChanging" 
        onselectedindexchanged="grdDocuments_SelectedIndexChanged" PageSize="50" 
        Width="100%" onrowdeleting="grdDocuments_RowDeleting">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="docID" HeaderText="Serial" />
            <asp:TemplateField HeaderText="Document Name">
                <ItemTemplate>
                    <a href="../Screen/documentInfo.aspx?docID=<%#c.encrypt(Eval("docID").ToString()) %>">
                        <%# Eval("docName") %>
                    </a>
                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Folder">
                <ItemTemplate>
                    <asp:Label ID="lblFolderName" runat="server" 
                        Text='<%# getFolderName(Eval("fldrID").ToString()) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# getDocTypeDesc(c.convertToInt32(Eval("docTypID"))) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="addedDate" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Added Date" />
            <asp:TemplateField HeaderText="Added By">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# c.getUserName(c.convertToInt32(Eval("addedUserID"))) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="modifyDate" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Modify Date" />
            <asp:CommandField SelectText="Show" ShowSelectButton="True" 
            ButtonType="Image" SelectImageUrl="../Images/Icons/doc-Icon.png" />
            <asp:CommandField ShowDeleteButton="True" Visible="false" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="outOfForm" runat="server">
</asp:Content>
