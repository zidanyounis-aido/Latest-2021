<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PdfLauncher.aspx.cs" Inherits="dms.PdfLauncher" %>

<!DOCTYPE html>
<!--
Copyright 2012 Mozilla Foundation

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

Adobe CMap resources are covered by their own copyright but the same license:

    Copyright 1990-2015 Adobe Systems Incorporated.

See https://github.com/adobe-type-tools/cmap-resources
-->
<html dir="ltr" mozdisallowselectionprint>
<head>
    <!-- Mirrored from mozilla.github.io/pdf.js/web/viewer.html by HTTrack Website Copier/3.x [XR&CO'2014], Fri, 16 Mar 2018 15:19:46 GMT -->
    <!-- Added by HTTrack -->
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <!-- /Added by HTTrack -->

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="google" content="notranslate">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>PDF.js viewer</title>
    <!-- Latest compiled and minified CSS -->
    <link href="Scripts/jQuery-contextMenu/jquery.contextMenu.css" rel="stylesheet" />
    <link href="css/awsf.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <%--    <link rel="stylesheet" type="text/css" href="https://printjs-4de6.kxcdn.com/print.min.css">--%>

    <link href="viewer.css" rel="stylesheet" />
    <style>
        .context-menu {
            display: none;
        }

        .img-fluid {
            width: 100%;
            border: 1px solid;
        }
    </style>
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <%--    <link href="css/bootstrap-image-checkbox.css" rel="stylesheet" />--%>
    <style>
        /* canvas {
            z-index: -2 !important;
        }*/

        /* .drag-drop {
            z-index: 1 !important;
            position: absolute !important;
            border: 0.3px solid;
            padding-bottom: 5px;
        }*/
        .drag-lable {
            z-index: 1 !important;
            position: absolute !important;
            border: 0.3px solid;
            /*padding-bottom: 5px;*/
        }

        .context-menu {
            display: inline !important;
        }
        /*.navbar-fixed-left {
            width: 96px;
            position: fixed;
            border-radius: 0;
            height: 100%;
        }

            .navbar-fixed-left .navbar-nav > li {
                float: none; 
                width: 139px;
            }

            .navbar-fixed-left + .container {
                padding-left: 160px;
            }

            .navbar-fixed-left .navbar-nav > li > .dropdown-menu {
                margin-top: -50px;
                margin-left: 140px;
            }*/
        @media print {
            @page {
                margin: 0;
            }

            body {
                margin-left: -2.6cm;
                margin-right: -2.6cm;
            }
        }
    </style>
</head>

<body tabindex="1" class="loadingInProgress" style="background: #404040 !important; background-image: url(images/texture.png);">
    <input type="hidden" id="hdnpath" runat="server" />
    <input type="hidden" id="hdnsignture" runat="server" />
    <input type="hidden" id="hdnDocLable" runat="server" />
    <input type="hidden" id="hdnDocserial" runat="server" />
    <input type="hidden" id="hdnDocname" runat="server" />
    <input type="hidden" id="hdnDoctype" runat="server" />
    <input type="hidden" id="hdndocument" runat="server" />
    <input type="hidden" id="hdnuser" runat="server" />
    <div style="display:none;">
        <canvas id="code128"></canvas>
    </div>

    <div id="outerContainer">
        <div id="sidebarContainer" style="position: fixed;">
            <div id="toolbarSidebar">
                <div class="splitToolbarButton toggled">
                    <button id="viewThumbnail" class="toolbarButton" title="Show Thumbnails" tabindex="2" data-l10n-id="thumbs">
                        <span data-l10n-id="thumbs_label">Thumbnails</span>
                    </button>
                    <%--<button id="viewOutline" class="toolbarButton" title="Show Document Outline (double-click to expand/collapse all items)" tabindex="3" data-l10n-id="document_outline">
                        <span data-l10n-id="document_outline_label">Document Outline</span>
                    </button>
                    <button id="viewAttachments" class="toolbarButton" title="Show Attachments" tabindex="4" data-l10n-id="attachments">
                        <span data-l10n-id="attachments_label">Attachments</span>
                    </button>--%>
                </div>
            </div>
            <div id="sidebarContent">
                <div id="thumbnailView" style="overflow: hidden !important;">
                </div>
                <div id="outlineView" class="hidden">
                </div>
                <div id="attachmentsView" class="hidden">
                </div>
            </div>
            <div id="sidebarResizer" class="hidden"></div>
        </div>
        <!-- sidebarContainer -->
        <!-- sidebarContainer -->
        <div id="mainContainer">
            <!-- secondaryToolbar -->

            <div class="toolbar" style="position: fixed;">
                <div id="toolbarContainer">
                    <div id="toolbarViewer">
                        <div id="toolbarViewerLeft">
                            <button id="sidebarToggle" class="toolbarButton" title="بدّل ظهور الشريط الجانبي" tabindex="11" data-l10n-id="toggle_sidebar">
                                <span data-l10n-id="toggle_sidebar_label">بدّل ظهور الشريط الجانبي</span>
                            </button>
                            <%--                            <div class="toolbarButtonSpacer"></div>
                            <button id="viewFind" class="toolbarButton" title="ابحث في المستند" tabindex="12" data-l10n-id="findbar">
                                <span data-l10n-id="findbar_label">ابحث</span>
                            </button>--%>
                            <div class="splitToolbarButton hiddenSmallView">
                                <button class="toolbarButton pageUp" title="الصفحة السابقة" id="previous" tabindex="13" data-l10n-id="previous" onclick="goBack();">
                                    <span data-l10n-id="previous_label">السابقة</span>
                                </button>
                                <div class="splitToolbarButtonSeparator"></div>
                                <button class="toolbarButton pageDown" title="الصفحة التالية" id="next" tabindex="14" data-l10n-id="next" onclick="goNext();">
                                    <span data-l10n-id="next_label">التالية</span>
                                </button>
                            </div>
                            <input type="number" id="pageNumber" class="toolbarField pageNumber" title="صفحة" value="1">
                            <span id="numPages" class="toolbarLabel">0</span>
                        </div>
                        <div id="toolbarViewerRight">
                            <%--  <button id="presentationMode" class="toolbarButton presentationMode hiddenLargeView" title="انتقل لوضع العرض التقديمي" tabindex="31" data-l10n-id="presentation_mode">
                                <span data-l10n-id="presentation_mode_label">وضع العرض التقديمي</span>
                            </button>

                            <button id="openFile" class="toolbarButton openFile hiddenLargeView" title="افتح ملفًا" tabindex="32" data-l10n-id="open_file">
                                <span data-l10n-id="open_file_label">افتح</span>
                            </button>--%>

                            <button id="print" class="toolbarButton print hiddenMediumView" title="اطبع" tabindex="33" data-l10n-id="print" onclick="window.print();">
                                <span data-l10n-id="print_label">اطبع</span>
                            </button>

                            <%--<button id="download" class="toolbarButton download hiddenMediumView" title="نزّل" tabindex="34" data-l10n-id="download">
                                <span data-l10n-id="download_label">نزّل</span>
                            </button>
                            <a href="#page=7&amp;zoom=auto,-49,234" id="viewBookmark" class="toolbarButton bookmark hiddenSmallView" title="المنظور الحالي (انسخ أو افتح في نافذة جديدة)" tabindex="35" data-l10n-id="bookmark">
                                <span data-l10n-id="bookmark_label">المنظور الحالي</span>
                            </a>

                            <div class="verticalToolbarSeparator hiddenSmallView"></div>

                            <button id="secondaryToolbarToggle" class="toolbarButton" title="الأدوات" tabindex="36" data-l10n-id="tools">
                                <span data-l10n-id="tools_label">الأدوات</span>
                            </button>--%>
                        </div>
                        <div id="toolbarViewerMiddle">
                            <%--                            <div class="splitToolbarButton">
                                <button id="zoomOut" class="toolbarButton zoomOut" title="بعّد" tabindex="21" data-l10n-id="zoom_out">
                                    <span data-l10n-id="zoom_out_label">بعّد</span>
                                </button>
                                <div class="splitToolbarButtonSeparator"></div>
                                <button id="zoomIn" class="toolbarButton zoomIn" title="قرّب" tabindex="22" data-l10n-id="zoom_in">
                                    <span data-l10n-id="zoom_in_label">قرّب</span>
                                </button>
                            </div>
                            <span id="scaleSelectContainer" class="dropdownToolbarButton" style="min-width: 92px; max-width: 92px;">
                                <select id="scaleSelect" title="التقريب" tabindex="23" data-l10n-id="zoom" style="min-width: 114px;">
                                    <option id="pageAutoOption" title="" value="auto" selected="selected" data-l10n-id="page_scale_auto">تقريب تلقائي</option>
                                    <option id="pageActualOption" title="" value="page-actual" data-l10n-id="page_scale_actual">الحجم الفعلي</option>
                                    <option id="pageFitOption" title="" value="page-fit" data-l10n-id="page_scale_fit">ملائمة الصفحة</option>
                                    <option id="pageWidthOption" title="" value="page-width" data-l10n-id="page_scale_width">عرض الصفحة</option>
                                    <option id="customScaleOption" title="" value="custom" disabled="disabled" hidden="true"></option>
                                    <option title="" value="0.5" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 50 }">50٪</option>
                                    <option title="" value="0.75" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 75 }">75٪</option>
                                    <option title="" value="1" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 100 }">100٪</option>
                                    <option title="" value="1.25" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 125 }">125٪</option>
                                    <option title="" value="1.5" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 150 }">150٪</option>
                                    <option title="" value="2" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 200 }">200٪</option>
                                    <option title="" value="3" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 300 }">300٪</option>
                                    <option title="" value="4" data-l10n-id="page_scale_percent" data-l10n-args="{ &quot;scale&quot;: 400 }">400٪</option>
                                </select>
                            </span>--%>
                        </div>
                    </div>
                    <div id="loadingBar" style="width: calc(100% - 17px);" class="hidden">
                        <div class="progress" style="height: 100%; width: 100%;">
                            <div class="glimmer">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--            <div class="navbar navbar-inverse navbar-fixed-left">
                <div id="myList" style="overflow-y: auto; height: 700px;">
                </div>
            </div>--%>
            <div class="container">
                <div class="row" style="margin-top: 50px;">
                    <div class="col-md-12">
                        <div id="viewerContainer" tabindex="0" style="text-align: center !important; margin: auto !important; margin-top: 5%;">

                            <div id="viewer" class="pdfViewer singlePageView dropzone nopadding">
                            </div>
                        </div>
                    </div>
                    <%--                <div class="col-md-1">

                </div>--%>
                </div>
                <!--content here-->
            </div>

        </div>
        <!-- mainContainer -->
        <!-- overlayContainer -->
    </div>
    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
    <div id="pageModal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">اداره الصفحات</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" onclick="if($(this).is(':checked')){$('.page-checkbox').prop('checked',true);}else{$('.page-checkbox').prop('checked',false);}">
                        <label class="form-check-label" for="flexCheckDefault">
                            اختيار الكل
                        </label>
                    </div>
                    <div class="row div-list-pages" style="overflow-y: scroll; height: 500px;">
                        <%--<div class="col-md-3">
                            <div class="custom-control custom-checkbox image-checkbox">
                                <input type="checkbox" class="custom-control-input" id="ck1a"> page1
                                <label class="custom-control-label" for="ck1a">
                                    <img src="https://iqbalfn.github.io/bootstrap-image-checkbox/img/annie-spratt.jpg" alt="#" class="img-fluid">
                                </label>
                            </div>
                        </div>--%>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" onclick="saveChanges();">حفظ التغييرات</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">اغلاق</button>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hdnCurrentName" runat="server" />
    <!-- outerContainer -->
    <div id="printContainer"></div>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/interact.js/1.0.2/interact.min.js"></script>
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <%--<script src="https://mozilla.github.io/pdf.js/build/pdf.js"></script>--%>
    <script src="Scripts/pdfJS/pdf.js"></script>
    <%-- <script src="JS/build/pdf.js"></script>--%>
    <script src="https://kendo.cdn.telerik.com/2017.2.621/js/jquery.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2017.2.621/js/jszip.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2017.2.621/js/kendo.all.min.js"></script>
    <script src="Scripts/bootbox.min.js"></script>
    <script src="Scripts/custom-awsf.js"></script>
    <!--query context-->
    <script src="Scripts/jQuery-contextMenu/jquery.contextMenu.js"></script>
    <script src="Scripts/pdfViewr.js"></script>
    <script src="viewer.js"></script>
    <script src="JS/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/jsbarcode/3.3.20/JsBarcode.all.min.js"></script>

    <%--  <script>
        function printDiv() {
            printJS('printJS-form', 'html')
        }
    </script--%>>

    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript">
        var currentUserName = Session["userName"].ToString();
    </script>--%>
</body>
<!-- Mirrored from mozilla.github.io/pdf.js/web/viewer.html by HTTrack Website Copier/3.x [XR&CO'2014], Fri, 16 Mar 2018 15:19:48 GMT -->
</html>

