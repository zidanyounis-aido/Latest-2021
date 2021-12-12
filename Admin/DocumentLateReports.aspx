<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="DocumentLateReports.aspx.cs" Inherits="dms.Admin.DocumentLateReports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <form id="form1" runat="server">--%>
      <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <rsweb:reportviewer id="ReportViewer1" runat="server" width="100%" Height="800px">
        </rsweb:reportviewer>
    <%--</form>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
