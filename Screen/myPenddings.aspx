<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="myPenddings.aspx.cs" Inherits="dms.Screen.myPenddings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<asp:Literal ID="ltrScripts" runat="server"></asp:Literal>
<style>
    body{background-color:#ffffff; background-image:none;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitle" runat="server">
         <%= (Session["lang"].ToString() == "0") ? "My Pendings" : "في انتظاري"%> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRightBar" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField HeaderText="Form Type" />
            <asp:BoundField HeaderText="Sent Date" />
            <asp:CommandField ShowSelectButton="True" />
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">


    <b> <%= (Session["lang"].ToString() == "0") ? " Details:" : "تفاصيل :"%></b>


    <asp:Table ID="tblDocMetas" runat="server">
    </asp:Table><br />
    <b><%= (Session["lang"].ToString() == "0") ? "Files:" : "ملفات :"%></b><br />


    <asp:Table ID="tblDocMetas0" runat="server">
    </asp:Table><br />
    <b><%= (Session["lang"].ToString() == "0") ? " Workflow History:" : "تاريخ سير العمل :"%>
    </b>


    <asp:Table ID="tblDocMetas1" runat="server">
    </asp:Table>
    <br />
    <br />

    <table width="100%">
        <tr>
            <td valign="top" width="15%">
               <%= (Session["lang"].ToString() == "0") ? "Notes:" : "ملاحظات :"%> 
            </td>
            <td>
                <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td> <%= (Session["lang"].ToString() == "0") ? "Action" : "الاجراء"%></td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
    <asp:LinkButton ID="LinkButton3" runat="server"  CssClass="button"
        onclientclick="araneasFillAutos()" onclick="LinkButton3_Click">
        <img border="0" src="../Images/Icons/resultset-next-icon.png" align="absmiddle" />
        <%= (Session["lang"].ToString() == "0") ? "Submit" : "ارسال"%></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="OutOfDesign" runat="server">
</asp:Content>
