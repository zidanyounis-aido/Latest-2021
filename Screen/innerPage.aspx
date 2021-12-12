<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="innerPage.aspx.cs" Inherits="Araneas_ERP.screen.innerPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
<tr>
    <td valign="top">
        <asp:GridView ID="grdEntities" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="CODEN" HeaderText="Code" />
                <asp:BoundField DataField="FNAME" HeaderText="First Name" />
                <asp:BoundField DataField="LNAME" HeaderText="Last Name" />
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

<asp:Panel runat="server" ID="pnl1"><table style="width:100%;" border="0"><tr>
<td> <asp:Label ID="lblCODEN" runat="server" Text="CODEN"></asp:Label></td>
<td><asp:TextBox ID="txtCODEN" runat="server"></asp:TextBox></td>
<td> <asp:Label ID="lblOWNER" runat="server" Text="OWNER"></asp:Label></td>
<td><asp:TextBox ID="txtOWNER" runat="server"></asp:TextBox></td>
</tr><tr>
<td> <asp:Label ID="lblCODET" runat="server" Text="CODET"></asp:Label></td>
<td>
    <asp:DropDownList ID="drpCODET" runat="server">
    </asp:DropDownList>
        </td>
<td> <asp:Label ID="lblFNAME" runat="server" Text="FNAME"></asp:Label></td>
<td><asp:TextBox ID="txtFNAME" runat="server"></asp:TextBox></td>
</tr><tr>
<td> <asp:Label ID="lblSNAME" runat="server" Text="SNAME"></asp:Label></td>
<td><asp:TextBox ID="txtSNAME" runat="server"></asp:TextBox></td>
<td> <asp:Label ID="lblMNAME" runat="server" Text="MNAME"></asp:Label></td>
<td><asp:TextBox ID="txtMNAME" runat="server"></asp:TextBox></td>
</tr><tr>
<td> <asp:Label ID="lblLNAME" runat="server" Text="LNAME"></asp:Label></td>
<td><asp:TextBox ID="txtLNAME" runat="server"></asp:TextBox></td>
<td> <asp:Label ID="lblFNAMEL" runat="server" Text="FNAMEL"></asp:Label></td>
<td><asp:TextBox ID="txtFNAMEL" runat="server"></asp:TextBox></td>
</tr><tr>
<td> <asp:Label ID="lblSNAMEL" runat="server" Text="SNAMEL"></asp:Label></td>
<td><asp:TextBox ID="txtSNAMEL" runat="server"></asp:TextBox></td>
<td> <asp:Label ID="lblMNAMEL" runat="server" Text="MNAMEL"></asp:Label></td>
<td><asp:TextBox ID="txtMNAMEL" runat="server"></asp:TextBox></td>
</tr><tr>
<td> <asp:Label ID="lblLNAMEL" runat="server" Text="LNAMEL"></asp:Label></td>
<td><asp:TextBox ID="txtLNAMEL" runat="server"></asp:TextBox></td>
<td> <asp:Label ID="lblNAME_SHORT" runat="server" Text="NAME_SHORT"></asp:Label></td>
<td><asp:TextBox ID="txtNAME_SHORT" runat="server"></asp:TextBox></td>
</tr><tr>
<td> <asp:Label ID="lblIMAGE" runat="server" Text="IMAGE"></asp:Label></td>
<td><asp:TextBox ID="txtIMAGE" runat="server"></asp:TextBox></td>
<td> <asp:Label ID="lblICON" runat="server" Text="ICON"></asp:Label></td>
<td><asp:TextBox ID="txtICON" runat="server"></asp:TextBox></td>
</tr><tr>
<td> <asp:Label ID="lblSTATUS" runat="server" Text="STATUS"></asp:Label></td>
<td><asp:TextBox ID="txtSTATUS" runat="server"></asp:TextBox></td>
</tr></table></asp:Panel>
</asp:Content>
