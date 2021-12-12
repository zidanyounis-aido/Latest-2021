<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="subList.aspx.cs" Inherits="Araneas_ERP.screen.subList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%" cellpadding="0" cellspacing="0" style="height: 100%">
    <tr>
        <td style="width:20%; background-color: #FFFFFF; border-right-style: double; border-right-width: 2px; border-right-color: #003366;" 
            valign="top">
            <asp:TreeView ID="trvCategories" runat="server" 
                onselectednodechanged="trvCategories_SelectedNodeChanged" 
                ontreenodepopulate="trvCategories_TreeNodePopulate">
            </asp:TreeView>
        </td>
        <td valign="top">
            <div id="pnlTable">
                <asp:GridView ID="grdEntities" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    onselectedindexchanged="grdEntities_SelectedIndexChanged" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="CODEN" HeaderText="CODEN" />
                        <asp:BoundField DataField="FullNameL" HeaderText="Full Name" />
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <a id="lnkSelect" 
                                    href="javascript:showDialog('<%#Eval("CODEN") %>','<%#Eval("FullName") %>','propertiesPage')">Select</a>
                            </ItemTemplate>
                        </asp:TemplateField>
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
            </div>
            <div id="pnlIcons">
                <asp:Repeater ID="Repeater1" runat="server">
                </asp:Repeater>
            </div>
        </td>
    </tr>

</table>
</asp:Content>
