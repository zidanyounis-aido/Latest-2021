<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="delayedWF.aspx.cs" Inherits="dms.Screen.delayedWF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--<%= (Session["lang"].ToString() == "0") ? "Delayed Documents" : "مستندات متأخرة"%>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:GridView ID="grdDocuments" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" onpageindexchanging="grdDocuments_PageIndexChanging" 
        onselectedindexchanged="grdDocuments_SelectedIndexChanged" PageSize="200" 
        Width="100%" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="docID" HeaderText="ID" />
            <asp:BoundField DataField="docName" HeaderText="Document Name" />
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
            ButtonType="Image" SelectImageUrl="../Images/Icons/doc-Icon.png"  />
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
    <asp:Panel ID="pnlEmpty" Visible="false" runat="server" HorizontalAlign="Center">
    <br /><br />
    <img src="../Images/Trash-2-Empty-icon.png" />
    
    <h1 style="color:#336699">
    <%= (Session["lang"].ToString() == "0") ? "There is no Delayed Documents " : "لا يوجد مستندات متأخرة"%>
    
    </h1>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
