<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="wfForm.aspx.cs" Inherits="dms.Screen.wfForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<asp:Literal ID="ltrScripts" runat="server"></asp:Literal>
<style>
    body{background-color:#ffffff; background-image:none;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Table ID="tblDocMetas" runat="server">
    </asp:Table>
    <br />

    <table width="100%">
        <tr>
            <td valign="top" width="15%">
               <%= (Session["lang"].ToString() == "0") ? "Notes:" : "ملاحظات"%>  
            </td>
            <td>
                <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td> <%= (Session["lang"].ToString() == "0") ? "Action" : "اجراء"%></td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
    <asp:LinkButton ID="LinkButton3" runat="server" 
        onclientclick="araneasFillAutos()" onclick="LinkButton3_Click">
        <img border="0" src="../Images/Icons/resultset-next-icon.png" align="absmiddle" />
        <%= (Session["lang"].ToString() == "0") ? "Submit" : "ارسال"%></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
