<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="UsersLogs.aspx.cs" Inherits="dms.Admin.UsersLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%= (Session["lang"].ToString() == "0") ? "User" : "المستخدم"%>
    <asp:DropDownList ID="drpUsers" runat="server"></asp:DropDownList>
    <br />
    <%= (Session["lang"].ToString() == "0") ? "From Date" : "من تاريخ"%> :
    <asp:TextBox ID="txtFromDate" runat="server" CssClass="dateFeild"></asp:TextBox>
    <%= (Session["lang"].ToString() == "0") ? "To Date" : "إلى تاريخ"%> :
    <asp:TextBox ID="txtToDate" runat="server" CssClass="dateFeild"></asp:TextBox>
    <asp:Label ID="lblRes" runat="server" Text=""></asp:Label>
    <br />
    <asp:LinkButton ID="btnShow" runat="server" CssClass="button" onclick="btnShow_Click"  >
        <img border="0" src="../Images/Icons/Search-icon.png" align="absmiddle" />
        <%= (Session["lang"].ToString() == "0") ? "Show Logs" : "عرض الحركات"%></asp:LinkButton>
    <br />
    <h2><%= (Session["lang"].ToString() == "0") ? "Actions" : "تعديلات"%></h2>
    <asp:GridView ID="grdShowAllDBEvents" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="eventDateTime" HeaderText="Date Time" />
            <asp:BoundField DataField="FBActionTypeDescE" HeaderText="Action" />
            <asp:BoundField DataField="tableName" HeaderText="Effected Table" />
            <asp:BoundField DataField="parameters" HeaderText="Parameters" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <br />
    <h2><%= (Session["lang"].ToString() == "0") ? "Browsing" : "التصفح"%></h2>
    <asp:GridView ID="grdShowAllBrowsingEvents" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="eventDateTime" HeaderText="Date Time" />
            <asp:BoundField DataField="programName" HeaderText="Program Name" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <br />
    <h2><%= (Session["lang"].ToString() == "0") ? "Logins" : "الدخول"%></h2>
    <asp:GridView ID="grdShowAllLoginEvents" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="eventDateTime" HeaderText="Date Time" />
            <asp:BoundField DataField="IPAddress" HeaderText="IP Address" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
