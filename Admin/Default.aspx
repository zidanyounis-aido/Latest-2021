<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dms.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 128px;
            height: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightBar" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="pageTitle" runat="server">
        Admin Section
                </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2>
    Welcome to Araneas DMS Admin section
</h2>
<br />
<h3>Please select your section :</h3>
<br />
    <table cellspacing="10" width="600px">
        <tr>
            <td valign="top" align="center">
                <a href="manageFolders.aspx">
                <img src="../Images/archive.png" border="0" style="border-width: 0px" /></a></td>
            <td valign="top" align="center">
                <a href="manageFolders.aspx">
                <img src="../Images/Diwan.png" border="0" style="border-width: 0px" /></a></td>
            <td valign="top" align="center">
                <a href="workflowManage.aspx">
                <img src="../Images/Workflow.png" border="0" /></a></td>
            <td>
                <img alt="" class="style1" src="../Images/Statistics-icon.png" /></td>
        </tr>
        <tr>
            <td valign="middle" align="center"><a href="manageFolders.aspx">Archiving</a></td>
            <td valign="middle" align="center"><a href="manageFolders.aspx">Diwan</a></td>
            <td valign="middle" align="center"><a href="workflowManage.aspx">Workflow</a></td>
            <td align="center">
                <asp:HyperLink ID="HyperLink1" runat="server">Reports and Statistics</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
