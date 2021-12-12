<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="eForm.aspx.cs" Inherits="dms.Screen.eForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<asp:Literal ID="ltrScripts" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
<img align="absmiddle" alt="" class="style1" 
            src="../Images/Icons/File-icon.png" /><%= (Session["lang"].ToString() == "0") ? "Forms" : "اشكال"%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightBar" runat="server">
<b><%= (Session["lang"].ToString() == "0") ? "Select a Forms:" : "تحديد شكل"%></b><br /><br />
    <asp:GridView ID="grdEForms" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" GridLines="None" 
        onselectedindexchanged="grdTypes_SelectedIndexChanged" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="formID" HeaderText="ID" />
            <asp:BoundField DataField="formName" HeaderText="Name" />
            <asp:CommandField SelectText="Fill" ShowSelectButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5C5C5C" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />

<SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Panel ID="pnlDetails" Visible="false" runat="server">

<asp:Table ID="tblDocMetas" runat="server">
</asp:Table>
       <%= (Session["lang"].ToString() == "0") ? "Submit To :" : "ارسال الى :"%> &nbsp;
        <asp:DropDownList ID="drpRecipientType" runat="server" AutoPostBack="True" 
            onselectedindexchanged="drpRecipientType_SelectedIndexChanged">
            <asp:ListItem Value="1">User</asp:ListItem>
            <asp:ListItem Value="2">Group</asp:ListItem>
            <asp:ListItem Value="3">Position</asp:ListItem>
            <asp:ListItem Value="4">Department</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="drpRecipientID" runat="server">
        </asp:DropDownList>
        <br />
    <asp:LinkButton ID="LinkButton3" CssClass="btnSave" runat="server" 
        onclientclick="araneasFillAutos()" onclick="LinkButton3_Click">
        <img border="0" src="../Images/Icons/resultset-next-icon.png" align="absmiddle" />
       <%= (Session["lang"].ToString() == "0") ? "Submit" : "ارسال"%> </asp:LinkButton>
    </asp:Panel>
</asp:Content>
