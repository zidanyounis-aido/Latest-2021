<!DOCTYPE html>
<html dir="ltr" mozdisallowselectionprint>
<meta http-equiv="content-type" content="text/html;charset=utf-8" />
<head>
    <title>PDF viewer</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="google" content="notranslate">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
</head>

<body tabindex="1" class="loadingInProgress" style="background: #404040 !important; background-image: url(images/texture.png);">
    <form id="form1" runat="server">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        <div id="outerContainer">
            <!-- sidebarContainer -->
            <div id="mainContainer">
                <!-- secondaryToolbar -->
                <div class="toolbar">
                    <div id="toolbarContainer">
                        <div id="toolbarViewer">
                            <div id="toolbarViewerRight" style="text-align: center; margin: auto">
                                <a id="print" href="#" onclick="printThis();" class="toolbarButton print hiddenMediumView">
                                    <img src="images/printer-.png" style="height: 25px;" />
                                </a>
                                &nbsp;&nbsp;
                            <a id="download" href="#" onclick="exportPDF();">
                                <img src="images/download-to-storage-drive.png" style="height: 25px;" />
                            </a>
                            </div>
                        </div>
                    </div>
                </div>
                <!--content here-->
                <div id="viewerContainer" tabindex="0" style="text-align: center !important; margin: auto !important;">

                    <div id="viewer" class="pdfViewer"></div>
                </div>
            </div>
            <!-- mainContainer -->
            <!-- overlayContainer -->
        </div>
        <!-- outerContainer -->
        <div id="printContainer"></div>

        <script src="Scripts/jquery-1.10.2.min.js"></script>
        <script src="Scripts/pdf.js"></script>
        <script src="Scripts/html2pdf.bundle.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2017.2.621/js/jquery.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2017.2.621/js/jszip.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2017.2.621/js/kendo.all.min.js"></script>
        <script>
            var pdfPath = <%= Session["pdfPath"].ToString()%>;
            var signture =<%= Session["signture"].ToString()%>;
        </script>
        <script src="Scripts/pdfViewr.js"></script>
    </form>
</body>
</html>

