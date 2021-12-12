<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DocumentsMasterFullPage.master" AutoEventWireup="true" CodeBehind="newDocument.aspx.cs" Inherits="dms.Screen.newDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <script type="text/javascript" src="/Resources/dynamsoft.webtwain.initiate.js"></script>
    <script type="text/javascript" src="/Resources/dynamsoft.webtwain.config.js"></script>
    <script type="text/javascript">
        function fnExcelReport() {
            var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
            var textRange; var j = 0;
            tab = document.getElementById('headerTable'); // id of table

            for (j = 0; j < tab.rows.length; j++) {
                tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
                //tab_text=tab_text+"</tr>";
            }

            tab_text = tab_text + "</table>";
            tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
            tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
            tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
            }
            else                 //other browser not tested on IE 11
                sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

            return (sa);
        }
    </script>
    <script language="javascript" type="text/javascript">
        function hideScanned() {
            document.getElementById("pnlScanner").style.display = "none";
        }
        function showScanned() {
            document.getElementById("pnlScanner").style.display = "block";
        }
        function convertFrame(e) {
            var currentValue = parseFloat(document.getElementById("txtWfTimeFrame").value);
            var currentType = document.getElementById("tftype").value;
            var newType = e.value;
            var newValue;
            if (currentType == 'm') {
                switch (newType) {
                    case 'h':
                        newValue = currentValue / 60;
                        break;
                    case 'd':
                        newValue = currentValue / 3600;
                        break;
                }
            }
            if (currentType == 'h') {
                switch (newType) {
                    case 'm':
                        newValue = currentValue * 60;
                        break;
                    case 'd':
                        newValue = currentValue / 60;
                        break;
                }
            }
            if (currentType == 'd') {
                switch (newType) {
                    case 'm':
                        newValue = currentValue * 3600;
                        break;
                    case 'h':
                        newValue = currentValue * 60;
                        break;
                }
            }
            if (newValue % 1 == 0) {
                document.getElementById("txtWfTimeFrame").value = String(newValue);
            }
            else {
                document.getElementById("txtWfTimeFrame").value = newValue.toFixed(2);
            }
            document.getElementById("tftype").value = e.options[e.selectedIndex].value;
        }
        var console = window['console'] ? window['console'] : { 'log': function () { } };
        Dynamsoft.WebTwainEnv.RegisterEvent('OnWebTwainReady', Dynamsoft_OnReady); // Register OnWebTwainReady event. This event fires as soon as Dynamic Web TWAIN is initialized and ready to be used

        var DWObject;

        function Dynamsoft_OnReady() {
            debugger;
            DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer'); // Get the Dynamic Web TWAIN object that is embeded in the div with id 'dwtcontrolContainer'
            if (DWObject) {
                var count = DWObject.SourceCount; // Populate how many sources are installed in the system
                if (count == 0 && Dynamsoft.Lib.env.bMac) {
                    DWObject.CloseSourceManager();
                    DWObject.ImageCaptureDriverType = 0;
                    DWObject.OpenSourceManager();
                    count = DWObject.SourceCount;
                }

                for (var i = 0; i < count; i++) {
                    document.getElementById("source").options.add(new Option(DWObject.GetSourceNameItems(i), i));  // Add the sources in a drop-down list
                }
                DWObject.RegisterEvent('OnPostAllTransfers', function () {
                    console.log(DWObject.HowManyImagesInBuffer);
                    document.getElementById("showScan").style.display = "inline";
                });

            }
        }

        function AcquireImage() {
            debugger;
            document.getElementById("showScan").style.display = "none";
            if (DWObject) {
                var OnAcquireImageSuccess, OnAcquireImageFailure;
                OnAcquireImageSuccess = OnAcquireImageFailure = function () {
                    DWObject.CloseSource();
                };

                DWObject.SelectSourceByIndex(document.getElementById("source").selectedIndex);
                DWObject.OpenSource();
                DWObject.IfDisableSourceAfterAcquire = true;	// Scanner source will be disabled/closed automatically after the scan.
                DWObject.AcquireImage(OnAcquireImageSuccess, OnAcquireImageFailure);
            }
        }

        //Callback functions for async APIs
        function OnSuccess() {
            console.log('successful');
        }

        function OnFailure(errorCode, errorString) {
            alert(errorString);
        }

        function LoadImage() {
            if (DWObject) {
                DWObject.IfShowFileDialog = true; // Open the system's file dialog to load image
                DWObject.LoadImageEx("", EnumDWT_ImageType.IT_ALL, OnSuccess, OnFailure); // Load images in all supported formats (.bmp, .jpg, .tif, .png, .pdf). sFun or fFun will be called after the operation
            }
        }

        // OnHttpUploadSuccess and OnHttpUploadFailure are callback functions.
        // OnHttpUploadSuccess is the callback function for successful uploads while OnHttpUploadFailure is for failed ones.

        function OnHttpUploadSuccess() {
            console.log('successful');
            __doPostBack('ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$LinkButton4', '')
        }

        function OnHttpUploadFailure(errorCode, errorString, sHttpResponse) {
            alert(errorString + sHttpResponse);
        }

        function UploadClick() {
            debugger;
            if (DWObject) {
                // If no image in buffer, return the function
                if (DWObject.HowManyImagesInBuffer == 0)
                    return;

                var strHTTPServer = location.hostname; //The name of the HTTP server. For example: "www.dynamsoft.com"; 
                var strActionPage = "/SaveToFile.aspx";
                DWObject.IfSSL = false; // Set whether SSL is used
                DWObject.HTTPPort = location.port == "" ? 80 : location.port;

                var Digital = new Date();
                var uploadfilename = document.getElementById("hdnUserCode").value;
                switch ($("#drpFormat").val()) {
                    case "pdf":
                        DWObject.HTTPUploadAllThroughPostAsPDF(strHTTPServer, strActionPage, uploadfilename + ".pdf", OnHttpUploadSuccess, OnHttpUploadFailure);
                        break;
                    case "jpg":
                        if (DWObject.GetImageBitDepth(DWObject.CurrentImageIndexInBuffer) == 1)
                            DWObject.ConvertToGrayScale(DWObject.CurrentImageIndexInBuffer);
                        DWObject.HTTPUploadThroughPost(strHTTPServer, DWObject.CurrentImageIndexInBuffer, strActionPage, uploadfilename + ".jpg", OnHttpUploadSuccess, OnHttpUploadFailure);
                        break;
                    case "tiff":
                        DWObject.HTTPUploadAllThroughPostAsMultiPageTIFF(strHTTPServer, strActionPage, uploadfilename + ".tiff", OnHttpUploadSuccess, OnHttpUploadFailure);
                        break;
                    case "png":
                        if (DWObject.GetImageBitDepth(DWObject.CurrentImageIndexInBuffer) == 1)
                            DWObject.ConvertToGrayScale(DWObject.CurrentImageIndexInBuffer);
                        DWObject.HTTPUploadThroughPost(strHTTPServer, DWObject.CurrentImageIndexInBuffer, strActionPage, uploadfilename + ".png", OnHttpUploadSuccess, OnHttpUploadFailure);
                        break;
                }
            }
            return false;
        }

        function drawFiles(xthis) {
            var files = $('#ContentPlaceHolder1_ContentPlaceHolderBody_fluFile')[0].files;
            var html = '';
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var filename = file.name.split('.').shift();
                var fileext = file.name.split('.').pop();
                html += " <div class=\"file-item\">";
                html += "                                                        <a CommandName='0'  onclick=\"ButtonDelete_ClickThis(this);\">";
                html += "                                                     <span class=\"icon-close\">";
                html += "                                                    <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\" viewBox=\"0 0 11.963 11.963\">";
                html += "                                                        <g id=\"Group_21\" data-name=\"Group 21\" transform=\"translate(5.981 -3.153) rotate(45)\">";
                html += "                                                            <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\" transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                html += "                                                            <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\" transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                html += "                                                        <\/g>";
                html += "                                                    <\/svg>";
                html += "                                                <\/span>";
                html += "                                                        <\/a>";
                html += "                                                        <span class=\"file-format file-format-jpg\">" + fileext;
                html += "                                                        <\/span>";
                html += "                                                        <div class=\"file-info\">";
                html += "                                                            <span class=\"file-name\">" + filename + "<\/span>";
                html += "                                                            <span class=\"file-status\">اكتمل<\/span>";
                html += "                                                            <div class=\"progress-bar-file\">";
                html += "                                                                <span style=\"width: 100%;\"><\/span>";
                html += "                                                            <\/div>";
                html += "                                                        <\/div>";
                html += "                                                    <\/div>";

            }
            $(".files-area").html(html);
        }
        function ButtonDelete_ClickThis(xthis) {
            $(xthis).parent().remove();
        }
    </script>

    <asp:Literal ID="ltrScripts" runat="server"></asp:Literal>

    <style type="text/css">
        .style1 {
            width: 24px;
            height: 24px;
        }

        .style2 {
            width: 32px;
            height: 32px;
        }
    </style>
    <style>
        .optionBlock {
            float: left;
            width: 250px;
            height: 300px;
            border: 1px solid #808080;
            background-color: #ccc;
            border-radius: 15px;
            text-align: center;
            padding-top: 20px;
        }

        .optionContent {
            width: 100%;
            margin: 0px auto;
        }
    </style>
    <style>
        .new-drop {
            padding: 4px;
            border-radius: 20px;
        }

        .new-main-input {
            width: 97% !important;
            border: 1px solid #cacaca;
            border-radius: 20px;
            padding: 6px;
            outline: none;
            /*height: 35px;*/
        }

        .margin-top-20 {
            margin-top: 20px;
        }

        .cellTDAr {
            margin-top: 10px;
        }

        #ContentPlaceHolder1_ContentPlaceHolderBody_ctl12 {
            color: red;
            font-size: 18px;
        }

        #ContentPlaceHolder1_ContentPlaceHolderBody_ctl07 {
            color: red;
            font-size: 18px;
        }

        th {
            background: #007aff;
            color: #fff;
            border: 1px solid #ccc;
            padding: 8px;
        }

        .frm-item {
            width: 100%;
            border: 1px solid #ccc;
            padding: 5px;
            border-radius: 6px;
            outline: none;
            font-size: 14px;
            height: 36px;
        }

        .frm-drop {
            width: 100%;
            border: 1px solid #ccc;
            padding: 3px;
            border-radius: 6px;
            outline: none;
            font-size: 14px;
            height: 36px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="#"><%= (Session["lang"].ToString() == "0") ? "Folders" : "المجلدات"%></a></li>
        <li><a href="#">
            <asp:Label ID="lblParentName" runat="server" Text="New Document"></asp:Label></a></li>
        <li><a>
            <asp:Label ID="lblFolderName" runat="server" Text="New Document"></asp:Label></a></li>
    </ul>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Panel ID="pnlAddNew" runat="server">

        <asp:Panel ID="pnlAttach" runat="server">

            <div class="optionContent" style="display: none">
                <div class="optionBlock" style="margin-right: 40px; margin-left: 40px;">
                    <img align="absmiddle" alt=""
                        src="../Images/icons/skip.png" />
                    <br />
                    <br />
                    <%= (Session["lang"].ToString() == "0") ? "Skip Attaching File " : "&#1578;&#1582;&#1591;&#1610; &#1573;&#1585;&#1601;&#1575;&#1602; &#1605;&#1587;&#1578;&#1606;&#1583;"%>
                    <br />
                    <br />

                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button">
                <img border="0" src="../Images/Icons/Actions-go-up-icon.png" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Skip" : "&#1578;&#1582;&#1591;&#1610;"%> </asp:LinkButton>
                </div>

                <div class="optionBlock" style="margin-right: 40px">
                    <img align="absmiddle" alt=""
                        src="../Images/upload.png" />
                    <br />
                    <br />
                    <%= (Session["lang"].ToString() == "0") ? "Upload File " : "&#1578;&#1581;&#1605;&#1610;&#1604; &#1605;&#1604;&#1601;"%>
                    <br />
                    <br />

                    <br />
                    <br />

                </div>

                <div class="optionBlock">

                    <img align="absmiddle" alt=""
                        src="../Images/scanner.png" />
                    <br />
                    <br />

                    <h3><%= (Session["lang"].ToString() == "0") ? "Scan Document" : "&#1605;&#1587;&#1581; &#1590;&#1608;&#1574;&#1610;"%></h3>
                    <br />
                    <br />
                    <%= (Session["lang"].ToString() == "0") ? "File format" : "&#1606;&#1608;&#1593; &#1575;&#1604;&#1605;&#1604;&#1601;"%> :
         
            <%-- <asp:DropDownList runat="server" ID="drpFormat" ClientIDMode="Static">
                 <asp:ListItem Value="pdf">PDF</asp:ListItem>
                 <asp:ListItem Value="jpg">JPEG</asp:ListItem>
                 <asp:ListItem Value="tiff">TIFF multi pages</asp:ListItem>
                 <asp:ListItem Value="png">PNG</asp:ListItem>
             </asp:DropDownList>--%>

                    <select size="1" id="source"></select>

                    &nbsp;
                <span style="display: none; cursor: pointer" id="showScan" onclick="showScanned()">
                    <%= (Session["lang"].ToString() == "0") ? "Show Scanned Image" : "&#1593;&#1585;&#1590; &#1575;&#1604;&#1589;&#1608;&#1585;&#1577; &#1575;&#1604;&#1605;&#1605;&#1587;&#1608;&#1581;&#1577;"%>
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="button" OnClick="LinkButton4_Click" OnClientClick="return UploadClick();">
                    
                <img border="0" src="../Images/Icons/Actions-go-up-icon.png" align="absmiddle" />
                <%= (Session["lang"].ToString() == "0") ? "Upload" : "&#1578;&#1581;&#1605;&#1610;&#1604; "%></asp:LinkButton>



                    <%--    </div>--%>
                </span>
                </div>

                <asp:Label ID="lblStep1" runat="server" ForeColor="Red"></asp:Label>
        </asp:Panel>
        <asp:HiddenField ID="hdnURL" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnDocPath" runat="server" />
        <asp:HiddenField ID="hdnUserCode" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnScannerFlag" runat="server" Value="0" />
        <br />
        <asp:Panel runat="server" ID="pnlDocDetails" Visible="false" TabIndex="1">
            <div style="float: left; width: 20%">
                <asp:Image ID="imgFile" Width="100%" runat="server" />
                <input type="hidden" id="hdnImageUploaded" value="0" runat="server" />
            </div>
            <div class="white-holder">

                <div class="max-width-holder">

                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label class="main-label">
                                <%= (Session["lang"].ToString() == "0") ? "Document ID:" : "&#1585;&#1602;&#1605; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583;: "%>
                            </label>
                            <asp:TextBox ID="txtDocID" CssClass="main-input" ReadOnly="true" runat="server"
                                TabIndex="2"></asp:TextBox>
                            <asp:TextBox ID="txttypeId" CssClass="main-input" Style="display: none;" runat="server"
                                TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label class="main-label">
                                <asp:Label ID="lblDocName" runat="server" Text="Document Name"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtDocName" runat="server" CssClass="main-input" TabIndex="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-4" style="visibility: hidden">
                        <div class="main-field-holder">
                            <label class="main-label">Empty</label>
                            <input type="text" class="main-input" />
                        </div>
                    </div>
                    <asp:Panel ID="pnlDocMetas" CssClass="tblMetas" runat="server">
                    </asp:Panel>
                </div>

                <div class="control-side-holder control-side-holder-footer">
                    <div class="start-side">
                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="SaveChanges_Click" CssClass="btn-main"
                            OnClientClick="araneasFillAutos()" TabIndex="15">
                                  <div class="btn-main-wrapper">
                                <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                    <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                    <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                    <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                    <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                    <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                                </svg>
                                 <%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%>
                            </div>
                        </asp:LinkButton>
                        <a class="btn-main btn-white" data-toggle="modal" data-target="#remove-confirm">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg" width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                    <g id="Group_2175" data-name="Group 2175">
                                        <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244" r="11.244" fill="#f4f4f4"></circle>
                                        <g id="Group_2166" data-name="Group 2166" transform="translate(7.496 7.496)">
                                            <line id="Line_28" data-name="Line 28" y2="11.745" transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                            <line id="Line_29" data-name="Line 29" x2="11.745" transform="translate(0) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                        </g>
                                    </g>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Undo" : "تراجع"%>
                            </div>
                        </a>
                    </div>

                    <div class="end-side">
                        <a class="btn-main" data-toggle="modal" data-target="#add-file">
                            <div class="btn-main-wrapper">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                    <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Attach File" : "إضافة مرفق"%>
                            </div>
                        </a>
                        <%--   <asp:LinkButton ID="btnshowattachpanel" OnClientClick="return true;" runat="server" OnClick="btnshowattachpanel_ServerClick" CausesValidation="false" CssClass="btn-main"
                            TabIndex="15">
                                  <div class="btn-main-wrapper">
                               <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                    <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                                </svg>
                                 <%= (Session["lang"].ToString() == "0") ? "Attach File" : "إضافة مرفق"%>
                            </div>
                        </asp:LinkButton>--%>
                        <%--         <a class="btn-main"  id="btnDelete" runat="server" visible="false">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                    <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                    <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Delete" : "حذف"%>
                            </div>
                        </a>--%>
                        <a class="btn-main" id="btnClear" onclick="clearElemets();">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                    <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                    <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Clear" : "مسح"%>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <div style="float: right; width: 80%; overflow-x: auto; display: none;">
                <table>

                    <tr>
                        <td style="width: 30%; text-align: center;"><%= (Session["lang"].ToString() == "0") ? "Document ID:" : "&#1585;&#1602;&#1605; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583;: "%></td>
                        <td>
                            <%--<asp:TextBox ID="txtDocID" ReadOnly="true" runat="server" Width="50px"
                                TabIndex="2"></asp:TextBox>--%>
                            &nbsp;<asp:HyperLink ID="lnkCheck" runat="server" Visible="False">Check document</asp:HyperLink>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: center;">
                            <%--<asp:Label ID="lblDocName" runat="server" Text="Document Name"></asp:Label>:--%>
                        </td>
                        <td style="width: 300px">
                            <%--<asp:TextBox ID="txtDocName" runat="server" Width="300px" TabIndex="3"></asp:TextBox>--%></td>
                    </tr>
                    <tr style="display: none;">
                        <td style="width: 30%; text-align: center;"><%= (Session["lang"].ToString() == "0") ? "Document Type:" : "&#1606;&#1608;&#1593; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583; :"%></td>
                        <td style="display: none">
                            <asp:DropDownList ID="drpDocTypID" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="drpDocTypID_SelectedIndexChanged" TabIndex="-48">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td style="width: 30%; text-align: center;">
                            <%= (Session["lang"].ToString() == "0") ? "Folder :" : "&#1575;&#1604;&#1605;&#1580;&#1604;&#1583; :"%></td>
                        <td style="display: none">
                            <asp:DropDownList ID="drpFldrID" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="drpFldrID_SelectedIndexChanged" TabIndex="-47">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnFolderSeq" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnDocTypeSeq" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnFolderDocTypeSeq" runat="server"
                                ClientIDMode="Static" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td style="width: 30%; text-align: center;">
                            <%= (Session["lang"].ToString() == "0") ? "Action Days" : " ايام الاجراء "%>   
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtenddateCount" />
                        </td>
                    </tr>
                </table>
                <%--<asp:Table ID="tblDocMetas" runat="server">
    </asp:Table>--%>


                <br />
                <table>
                    <%--  <tr>
                        <td>
                            <%= (Session["lang"].ToString() == "0") ? "The number of days" : "عدد الايام"%>   
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtenddateCount" />
                        </td>
                    </tr>--%>
                    <tr runat="server" id="customWF" style="display: none">
                        <td>
                            <%= (Session["lang"].ToString() == "0") ? "Delegate to :" : " &#1578;&#1605;&#1585;&#1610;&#1585; &#1573;&#1604;&#1609; :"%>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpNextUser" runat="server" TabIndex="10">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none">

                        <td>
                            <%= (Session["lang"].ToString() == "0") ? "Workflow Timeframe :" : " &#1575;&#1604;&#1581;&#1583; &#1575;&#1604;&#1571;&#1602;&#1589;&#1609; &#1604;&#1605;&#1587;&#1575;&#1585; &#1575;&#1604;&#1593;&#1605;&#1604;  "%>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTFType" onChange="convertFrame(this)" runat="server"
                                ClientIDMode="Static" TabIndex="11">
                                <asp:ListItem Value="m">Minutes</asp:ListItem>
                                <asp:ListItem Value="h">Hours</asp:ListItem>
                                <asp:ListItem Value="d" Selected="True">Days</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtWfTimeFrame" runat="server" ClientIDMode="Static" Text="1"
                                Width="75" TabIndex="12"></asp:TextBox>
                            <input type="hidden" id="tftype" value="d" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">

                            <asp:CheckBox ID="chkArchiveOnly" runat="server" Text="Archive Only"
                                TabIndex="14" Visible="False" />
                            &nbsp;</td>
                        <td>
                            <%--                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="SaveChanges_Click" CssClass="button"
                                OnClientClick="araneasFillAutos()" TabIndex="15">
                                 <img border="0" src="../Images/Icons/action-save-icon.png" align="absmiddle" />
                                 <%= (Session["lang"].ToString() == "0") ? "Save Document" : "&#1581;&#1601;&#1592; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583;"%></asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlResult" Style="text-align: center" runat="server" Visible="false">
        <br />
        <asp:Label ID="lblResultFinal" runat="server"
            Text="Document has been added sucussefuly...<br/><br/>You will be automaticly redirected to folder documents page"
            Font-Size="18px"></asp:Label>
        <asp:Literal ID="ltrRedirect" runat="server"></asp:Literal>
    </asp:Panel>
    <!-- Modal Add file-->
    <div class="modal fade my-modal my-modal-lg" id="add-file" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="background-color: rgba(0,0,0,0.4);">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">إضافة مرفق جديد</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <!-- Nav tabs -->
                        <ul class="ul-files-upload" role="tablist">
                            <li role="presentation" class="active">
                                <a href="#upload" role="tab" data-toggle="tab" onclick="$('#ContentPlaceHolder1_ContentPlaceHolderBody_btnScanFile').hide();$('#ContentPlaceHolder1_ContentPlaceHolderBody_btnUploadFile').show();">
                                    <svg id="Group_2701" data-name="Group 2701" xmlns="http://www.w3.org/2000/svg" width="22.139" height="22.139" viewBox="0 0 22.139 22.139">
                                        <g id="Group_2345" data-name="Group 2345" transform="translate(10.147 10.147)">
                                            <path id="Path_7011" data-name="Path 7011" d="M240.666,234.67a6,6,0,1,0,6,6A6,6,0,0,0,240.666,234.67Zm3.072,5.973a.691.691,0,0,1-.973.1l-1.407-1.151V243.2a.692.692,0,1,1-1.384,0v-3.613l-1.407,1.151a.692.692,0,0,1-.876-1.071l2.537-2.076a.691.691,0,0,1,.876,0l2.536,2.076A.69.69,0,0,1,243.737,240.643Z" transform="translate(-234.67 -234.67)" fill="#8f9198"></path>
                                        </g>
                                        <g id="Group_2346" data-name="Group 2346">
                                            <path id="Path_7012" data-name="Path 7012" d="M12.223,0H2.537A2.537,2.537,0,0,0,0,2.537v14.3a2.537,2.537,0,0,0,2.537,2.537H9.511a7.089,7.089,0,0,1-.747-3.229c.04-2.811,1.841-6.639,6-7.25V2.537A2.537,2.537,0,0,0,12.223,0Zm.16,8.871H2.5a.5.5,0,1,1,0-.995h9.884a.5.5,0,1,1,0,.995Zm0-2.511H2.5a.5.5,0,1,1,0-.995h9.884a.5.5,0,1,1,0,.995Zm0-2.511H2.5a.5.5,0,1,1,0-.995h9.884a.5.5,0,1,1,0,.995Z" fill="#8f9198"></path>
                                        </g>
                                    </svg>
                                    تحميل ملف</a>
                            </li>
                            <li role="presentation" class="">
                                <a href="#scan" role="tab" data-toggle="tab" onclick="$('#ContentPlaceHolder1_ContentPlaceHolderBody_btnScanFile').show();$('#ContentPlaceHolder1_ContentPlaceHolderBody_btnUploadFile').hide();">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="22.235" height="22.235" viewBox="0 0 22.235 22.235">
                                        <g id="Group_2732" data-name="Group 2732" transform="translate(1263 5650.93)">
                                            <g id="Group_2348" data-name="Group 2348" transform="translate(-1263 -5650.93)">
                                                <g id="Group_2348-2" data-name="Group 2348" transform="translate(0 0)">
                                                    <path id="Path_7013" data-name="Path 7013" d="M760.9-73.85h-9.728A2.548,2.548,0,0,0,748.62-71.3v14.36a2.548,2.548,0,0,0,2.548,2.548h7a7.121,7.121,0,0,1-.75-3.243c.04-2.823,1.849-6.668,6.022-7.282V-71.3A2.548,2.548,0,0,0,760.9-73.85Zm.161,8.909h-9.927a.5.5,0,0,1-.5-.5.5.5,0,0,1,.5-.5h9.927a.5.5,0,0,1,.5.5A.5.5,0,0,1,761.056-64.941Zm0-2.522h-9.927a.5.5,0,0,1-.5-.5.5.5,0,0,1,.5-.5h9.927a.5.5,0,0,1,.5.5A.5.5,0,0,1,761.056-67.463Zm0-2.522h-9.927a.5.5,0,0,1-.5-.5.5.5,0,0,1,.5-.5h9.927a.5.5,0,0,1,.5.5A.5.5,0,0,1,761.056-69.985Z" transform="translate(-748.62 73.85)" fill="#8f9198"></path>
                                                </g>
                                                <path id="Path_7014" data-name="Path 7014" d="M989.312,160.82a6.022,6.022,0,1,0,6.022,6.022A6.028,6.028,0,0,0,989.312,160.82Zm1.811,2.56h.781a.891.891,0,0,1,.888.887v.779a.369.369,0,0,1-.368.368h0a.369.369,0,0,1-.368-.368v-.779a.151.151,0,0,0-.151-.151h-.78a.368.368,0,0,1,0-.737Zm-4.91.888a.891.891,0,0,1,.887-.888h.779a.368.368,0,0,1,0,.737H987.1a.151.151,0,0,0-.151.151v.779a.37.37,0,0,1-.368.368h0a.369.369,0,0,1-.368-.368Zm1.667,5.691H987.1a.892.892,0,0,1-.888-.887v-.779a.368.368,0,1,1,.737,0v.779a.151.151,0,0,0,.151.151h.779a.368.368,0,0,1,0,.737Zm4.911-.888a.891.891,0,0,1-.887.888h-.786a.369.369,0,0,1-.013-.737h.8a.151.151,0,0,0,.151-.151v-.779a.368.368,0,0,1,.737,0Zm-.323-2.034h-5.963a.353.353,0,0,1,0-.705h5.963a.353.353,0,0,1,0,.705Z" transform="translate(-973.099 -150.629)" fill="#8f9198"></path>
                                            </g>
                                        </g>
                                    </svg>
                                    مسح ضوئي</a>
                            </li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="upload">
                                <div class="row upload-file-holder">
                                    <div class="col-xs-4">
                                        <div class="upload-file-input-holder">
                                            <asp:FileUpload ID="fluFile" CssClass="input-file-hidden" runat="server" AllowMultiple="true" onchange="drawFiles(this);" />
                                            <%--<input type="file" class="">--%>
                                            <p>Choose file</p>
                                            <p>او</p>
                                            <p>اسحب ملفك وأفلته هنا</p>
                                        </div>
                                        <div class="main-field-holder">
                                            <label class="main-label">تصنيف المرفق</label>
                                            <asp:DropDownList ID="drpDocGroupID" CssClass="new-drop" runat="server" Style="width: 100%;"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xs-8">
                                        <div class="files-area">
                                            <div class="file-empty" id="fileEmpty" runat="server" visible="true">
                                                لا توجد ملفات مرفوعه
                                            </div>
                                            <asp:ListView ID="lstUploadFiles" runat="server" ItemPlaceholderID="PlaceHolder1">
                                                <ItemTemplate>
                                                    <div class="file-item">
                                                        <asp:LinkButton runat="server" CommandName='<%# Eval("version") %>' CommandArgument='<%# Eval("docID") %>' OnClick="ButtonDelete_Click">
                                                     <span class="icon-close">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                                        <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                                            <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                                            <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                                        </g>
                                                    </svg>
                                                </span>
                                                        </asp:LinkButton>
                                                        <span class="file-format file-format-jpg"><%# Eval("ext") %>
                                                        </span>
                                                        <div class="file-info">
                                                            <span class="file-name"><%# string.Concat(Eval("docName"), "-", Eval("version"),".",Eval("ext"))%></span>
                                                            <span class="file-status">اكتمل</span>
                                                            <div class="progress-bar-file">
                                                                <span style="width: 100%;"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div role="tabpanel" class="tab-pane" id="scan">
                                <div class="row upload-file-holder">
                                    <div class="col-xs-4">
                                        <a href="Resources/DynamsoftServiceSetup.exe">
                                            <%= (Session["lang"].ToString() == "0") ? "Download Plugin" : "&#1581;&#1605;&#1604; &#1571;&#1583;&#1575;&#1577; &#1575;&#1604;&#1605;&#1587;&#1581;"%> 
                                        </a>
                                        <div class="main-field-holder">
                                            <label class="main-label">تصنيف المرفق</label>
                                            <div class="dropdown-main dropdown">
                                                <asp:DropDownList runat="server" ID="drpFormat" ClientIDMode="Static" CssClass="new-drop" Style="width: 100%;">
                                                    <asp:ListItem Value="pdf">PDF</asp:ListItem>
                                                    <asp:ListItem Value="jpg">JPEG</asp:ListItem>
                                                    <asp:ListItem Value="tiff">TIFF multi pages</asp:ListItem>
                                                    <asp:ListItem Value="png">PNG</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-8">
                                        <div class="files-area files-area-scan">
                                            <div class="file-empty" id="fileEmpty2" runat="server" visible="true">
                                                لا توجد ملفات مرفوعه
                                            </div>
                                            <asp:ListView ID="lstUploadFiles2" runat="server" ItemPlaceholderID="PlaceHolder1">
                                                <ItemTemplate>
                                                    <div class="file-item">
                                                        <asp:LinkButton runat="server" CommandName='<%# Eval("version") %>' CommandArgument='<%# Eval("docID") %>' OnClick="ButtonDelete_Click">
                                                     <span class="icon-close">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                                        <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                                            <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                                            <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                                        </g>
                                                    </svg>
                                                </span>
                                                        </asp:LinkButton>
                                                        <span class="file-format file-format-jpg"><%# Eval("ext") %>
                                                        </span>
                                                        <div class="file-info">
                                                            <span class="file-name"><%# string.Concat(Eval("docName"), "-", Eval("version"),".",Eval("ext"))%></span>
                                                            <span class="file-status">اكتمل</span>
                                                            <div class="progress-bar-file">
                                                                <span style="width: 100%;"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnUploadFile" OnClientClick="javascript: return CheckFiles();" CausesValidation="false" runat="server" OnClick="LinkButton1_Click" CssClass="btn-done-model">حفظ</asp:LinkButton>
                    <%--<button type="button" runat="server" id="btnUploadFile" onclick="javascript: return CheckFiles();" CausesValidation="false" onserverclick="LinkButton1_Click" class="btn-done-model"></button>--%>
                    <button type="button" runat="server" id="btnScanFile" style="display: none" class="btn-done-model" causesvalidation="false" onclick="AcquireImage();"><%= (Session["lang"].ToString() == "0") ? "Scan" : "مسح "%> </button>
                    <button type="button" class="btn-close-model" data-dismiss="modal">
                        <span class="icon-close">
                            <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963"
                                viewBox="0 0 11.963 11.963">
                                <g id="Group_21" data-name="Group 21"
                                    transform="translate(5.981 -3.153) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)"
                                        fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2">
                                    </line>
                                    <line id="Line_29" data-name="Line 29" x2="12.918"
                                        transform="translate(0 6.459)" fill="none" stroke="#fff"
                                        stroke-linecap="round" stroke-width="2">
                                    </line>
                                </g>
                            </svg>
                        </span>
                        تراجع
                    </button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade my-modal" id="remove-confirm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="background-color: rgba(0,0,0,0.4);">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= (Session["lang"].ToString() == "0") ? "Are you sure to delete?" : "هل أنت متأكد من الحذف ؟"%></h4>
                </div>
                <!-- <div class="modal-body">
                            </div> -->
                <div class="modal-footer">
                    <asp:LinkButton class="btn-done-model" CausesValidation="false" runat="server" OnClick="btnDeleteDocumnet_ServerClick">
                                <%= (Session["lang"].ToString() == "0") ? "Ok" : "نعم"%>
                    </asp:LinkButton>
                    <%--<button type="button" class="btn-done-model" runat="server" onserverclick="btnDelete_ServerClick"><%= (Session["lang"].ToString() == "0") ? "Ok" : "نعم"%></button>--%>
                    <button type="button" class="btn-close-model" data-dismiss="modal">
                        <span class="icon-close">
                            <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963"
                                viewBox="0 0 11.963 11.963">
                                <g id="Group_21" data-name="Group 21"
                                    transform="translate(5.981 -3.153) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)"
                                        fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2">
                                    </line>
                                    <line id="Line_29" data-name="Line 29" x2="12.918"
                                        transform="translate(0 6.459)" fill="none" stroke="#fff"
                                        stroke-linecap="round" stroke-width="2">
                                    </line>
                                </g>
                            </svg>
                        </span>
                        <%= (Session["lang"].ToString() == "0") ? "Cancel" : "تراجع"%>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" runat="server" class="hdnDynamicTabls" id="hdnDynamicTabls" value="" />
    <script>
        function CheckFiles() {
            if ($("#ContentPlaceHolder1_ContentPlaceHolderBody_fluFile").get(0).files.length == 0) {
                alert("الرجاء اختيار ملف");
                return false;
            }
            else {
                return true;
            }
        }
        $(document).on("keyup", ".tbl-from-elment", function () {
            drawDynamicTablsValues();
        });
        function drawDynamicTablsValues() {
            var currentAddList = '';
            var hdnTextValuesArr = [];
            var tblcollection = $(".tbl-from-elment");
            for (var i = 0; i < tblcollection.length; i++) {
                var dataMetaId = $(tblcollection[i]).attr('data-meta');
                if (currentAddList.indexOf(dataMetaId+',') == -1) {
                    currentAddList += dataMetaId + ',';
                    //set object to this element
                    var obj = {};
                    obj.id = Number(dataMetaId);
                    obj.type = $(tblcollection[i]).attr('type');
                    obj.value = '';
                    //get all attr to this meta tag
                    var collection = $("[data-meta='" + dataMetaId + "']");
                    for (var j = 0; j < collection.length; j++) {
                        if (j == 0) {
                            obj.value = $(collection[j]).val();
                        }
                        else {
                            obj.value +=","+ $(collection[j]).val();
                        }
                    }
                    hdnTextValuesArr.push(obj);
                }
                else {
                    // nothing
                }
            }
            $(".hdnDynamicTabls").val(JSON.stringify(hdnTextValuesArr));
        }
    </script>
    <script>
        $(function () {
            $('.dateFeild ').datepicker({
                format: 'dd/mm/yyyy',
                startDate: '-3d'
            });
        })
        function clearElemets() {
            $('.max-width-holder').find('input:not("#ContentPlaceHolder1_ContentPlaceHolderBody_txtDocID")').val('');
            $('.max-width-holder').find('textarea').val('');
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="outOfForm">
    <div id="pnlScanner" style="display: none; background-color: #ffffff; border: 3px ridge #293955; padding: 5px; position: absolute; top: 100px; left: 250px;">
        <img alt="close" id="imgClose" src="../Images/Icons/System-Close-icon.png"
            style="cursor: pointer" onclick="hideScanned()" />
        <br />
        <div id="dwtcontrolContainer"></div>
    </div>
</asp:Content>
