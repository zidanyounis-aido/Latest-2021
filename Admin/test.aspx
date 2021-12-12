<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="dms.Admin.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <link href="../css/ui-lightness/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).load($(function () {
            $("#uiDialog").hide();
        })
        );
        function showFolderDialog() {
            $("#uiDialog").dialog();
            $("#uiDialog").show();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="button" onclick="showFolderDialog()" />
    </div>

    <div id="uiDialog" title="Select a Folder" class="ui-widget-content">
    test
                    
                </div>
    </form>
</body>
</html>
