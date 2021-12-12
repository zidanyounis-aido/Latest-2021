<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="dms.plugins.Reminders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="<%= (Session["lang"].ToString() == "0") ? "../css/styleNew.css" : "../css/styleNewAr.css"%>" rel="stylesheet" />
    <link href="../fonts/fontawesome/css/all.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="../assets/<%= dms.sysSettings.getSettingValue("client") %>/css/theme.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
          <asp:Repeater ID="rptReminders" runat="server">
            <HeaderTemplate>
                <table class="grdDocuments" <%# Session["lang"].ToString() == "0"?"":"style='direction:rtl;text-align:right'" %>>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="border-bottom: 1px #dad8d8 solid !important;">
                        <a href="" onclick="window.parent.addTab('1&docID=<%#c.encrypt(Eval("docID").ToString()) %>','المجلدات','documentInfo',99)">
                            <%# Eval("ReminderText") %>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
