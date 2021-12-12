<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scantest.aspx.cs" Inherits="dms.Screen.scantest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"></script>
<script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="dwtcontrolContainer"></div>

        <div>
            <input type="button" value="<%= (Session["lang"].ToString() == "0") ? "Scan" : "مسح "%>" onclick="AcquireImage();"/>
<script type="text/javascript">
    function AcquireImage(){
        var DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer');
        DWObject.IfDisableSourceAfterAcquire = true;
        DWObject.SelectSource();
        DWObject.OpenSource();
        DWObject.AcquireImage();
    }
</script>




        </div>
    </form>
</body>
</html>
