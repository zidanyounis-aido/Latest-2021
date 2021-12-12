<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DocumentsMaster.master" AutoEventWireup="true" CodeBehind="documentInfo.aspx.cs" Inherits="dms.Screen.documentInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <style type="text/css">
        #ContentPlaceHolder1_trvFoldersn0Nodes table tbody tr td {
            padding: 4px 0px 4px 0px !important;
        }

        div#ContentPlaceHolder1_trvFolders img {
            width: 13px;
            margin-left: 7px;
            margin-bottom: 8px;
        }

        div#ContentPlaceHolder1_trvFolders a {
            color: #242424;
            font-size: 14px;
        }

        .new-drop {
            padding: 4px;
            border-radius: 20px;
        }

        .style1 {
            width: 32px;
            height: 32px;
        }

        .m-r-20 {
            margin-right: 20px;
        }

        .ui-dialog {
            display: block;
            z-index: 1001;
            outline: 0px;
            position: absolute;
            height: auto;
            width: 679px !important;
            top: 211px;
            left: 202px !important;
        }


        #workFlowDialog {
            border-left: solid 1px #5C5C5C;
            border-right: solid 1px #5C5C5C;
            border-bottom: solid 1px #5C5C5C;
            width: auto;
            min-height: 88.16px;
            height: auto;
            background: #FFF;
        }
        /**************************\
  Basic Modal Styles
\**************************/
        .add-class select {
            width: 100%;
        }

        .modal {
            font-family: -apple-system,BlinkMacSystemFont,avenir next,avenir,helvetica neue,helvetica,ubuntu,roboto,noto,segoe ui,arial,sans-serif;
        }

        .modal__overlay {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: rgba(0,0,0,0.6);
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .modal__container {
            background-color: #fff;
            padding: 30px;
            max-width: 500px;
            max-height: 100vh;
            border-radius: 4px;
            overflow-y: auto;
            box-sizing: border-box;
        }

        .modal__header {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .modal__title {
            margin-top: 0;
            margin-bottom: 0;
            font-weight: 600;
            font-size: 1.25rem;
            line-height: 1.25;
            color: #00449e;
            box-sizing: border-box;
        }

        .modal__close {
            background: transparent;
            border: 0;
        }

        .modal__header .modal__close:before {
            content: "\2715";
        }

        .modal__content {
            margin-top: 2rem;
            margin-bottom: 2rem;
            line-height: 1.5;
            color: rgba(0,0,0,.8);
        }

        .modal__btn {
            font-size: .875rem;
            padding-left: 1rem;
            padding-right: 1rem;
            padding-top: .5rem;
            padding-bottom: .5rem;
            background-color: #e6e6e6;
            color: rgba(0,0,0,.8);
            border-radius: .25rem;
            border-style: none;
            border-width: 0;
            cursor: pointer;
            -webkit-appearance: button;
            text-transform: none;
            overflow: visible;
            line-height: 1.15;
            margin: 0;
            will-change: transform;
            -moz-osx-font-smoothing: grayscale;
            -webkit-backface-visibility: hidden;
            backface-visibility: hidden;
            -webkit-transform: translateZ(0);
            transform: translateZ(0);
            transition: -webkit-transform .25s ease-out;
            transition: transform .25s ease-out;
            transition: transform .25s ease-out,-webkit-transform .25s ease-out;
        }

            .modal__btn:focus, .modal__btn:hover {
                -webkit-transform: scale(1.05);
                transform: scale(1.05);
            }

        .modal__btn-primary {
            background-color: #00449e;
            color: #fff;
        }



        /**************************\
  Demo Animation Style
\**************************/
        @keyframes mmfadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes mmfadeOut {
            from {
                opacity: 1;
            }

            to {
                opacity: 0;
            }
        }

        @keyframes mmslideIn {
            from {
                transform: translateY(15%);
            }

            to {
                transform: translateY(0);
            }
        }

        @keyframes mmslideOut {
            from {
                transform: translateY(0);
            }

            to {
                transform: translateY(-10%);
            }
        }

        .micromodal-slide {
            display: none;
        }

            .micromodal-slide.is-open {
                display: block;
            }

            .micromodal-slide[aria-hidden="false"] .modal__overlay {
                animation: mmfadeIn .3s cubic-bezier(0.0, 0.0, 0.2, 1);
                z-index: 99999 !important;
            }

            .micromodal-slide[aria-hidden="false"] .modal__container {
                animation: mmslideIn .3s cubic-bezier(0, 0, .2, 1);
            }

            .micromodal-slide[aria-hidden="true"] .modal__overlay {
                animation: mmfadeOut .3s cubic-bezier(0.0, 0.0, 0.2, 1);
            }

            .micromodal-slide[aria-hidden="true"] .modal__container {
                animation: mmslideOut .3s cubic-bezier(0, 0, .2, 1);
            }

            .micromodal-slide .modal__container,
            .micromodal-slide .modal__overlay {
                will-change: transform;
            }

        .p-t-15 {
            padding-top: 15px;
        }

        .relative {
            position: relative;
        }

        .timeline {
            position: relative;
            margin: 50px;
            padding: 0;
            list-style: none;
            counter-reset: section;
            width: 693px;
            margin: auto;
            z-index: 0 !important;
        }

            .timeline:before {
                content: '';
                position: absolute;
                top: 0;
                bottom: 0;
                width: 3px;
                background: #fdb839;
                left: 32px;
                margin: 0;
                border-radius: 2px;
            }

            .timeline > li {
                position: relative;
                margin-right: 10px;
                margin-bottom: 50px;
                padding-top: 18px;
                box-sizing: border-box;
            }

                .timeline > li:before,
                .timeline > li:after {
                    display: block;
                }

        .ui-sortable-helper {
            box-shadow: 0px 0px 41px rgba(0, 0, 0, 0.08);
            background: #fff;
        }

            .ui-sortable-helper .handle {
                display: none;
            }

        .timeline > li:not(.ui-sortable-helper):before {
            counter-increment: section;
            content: counter(section);
            background: #fdb839;
            width: 70px;
            height: 70px;
            position: absolute;
            top: 0;
            border-radius: 50%;
            left: -1px;
            display: flex;
            justify-content: center;
            align-items: center;
            color: #Fff;
            font-size: 22px;
            font-weight: bold;
            border: 15px solid #fff;
            box-sizing: border-box;
        }

        .timeline > li:after {
            clear: both;
        }

        .timeline > li > .timeline-item {
            margin-top: 0;
            color: #444;
            margin-left: 60px;
            margin-right: 15px;
            padding: 0;
        }

        .timeline > li > .fa,
        .timeline > li > .glyphicon,
        .timeline > li > .ion {
            width: 30px;
            height: 30px;
            font-size: 15px;
            line-height: 30px;
            position: absolute;
            color: #fff;
            background: #fdb839;
            border-radius: 50%;
            text-align: center;
            left: 18px;
            top: 0;
        }

        .radio-thumbnail > label {
            border-radius: 4px;
        }

        .radio-thumbnail > input {
            display: none;
        }

        .radio-thumbnail > :checked + .thumbnail {
            border: 4px solid #21BB05;
            border-radius: 4px;
        }

            .radio-thumbnail > :checked + .thumbnail > span {
                border-radius: 0;
            }

        .radio-thumbnail > :disabled + .thumbnail {
            opacity: .5;
        }

        .radio-thumbnail > :checked + .thumbnail:before {
            content: "\f00c";
            font-family: fontawesome;
            position: absolute;
            top: -10px;
            right: -10px;
            font-size: 15px;
            z-index: 1;
            color: #fff;
            background: #21bb05;
            width: 26px;
            height: 26px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            transition: 0.3s all;
        }

        .thumbnail {
            padding: 0;
            margin: 0;
            width: 192px;
            height: 140px;
            border: 0;
            position: relative;
        }

            .thumbnail > span {
                position: absolute;
                /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#000000+0,000000+100&0+0,0.65+100 */
                background: -moz-linear-gradient(top, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.65) 100%);
                /* FF3.6-15 */
                background: -webkit-linear-gradient(top, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.65) 100%);
                /* Chrome10-25,Safari5.1-6 */
                background: linear-gradient(to bottom, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.65) 100%);
                /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#00000000', endColorstr='#a6000000', GradientType=0);
                /* IE6-9 */
                top: 0;
                bottom: 0;
                left: 0;
                right: 0;
                margin: auto;
                width: 100%;
                height: 100%;
                color: #fff;
                font-size: 20px;
                font-weight: 400;
                padding: 97px 0 0 16px;
                text-align: left;
                border-radius: 4px;
                border: 0;
            }

        .uxc__f-tags .btn.btn-default {
            border-color: #fdb839;
            background: transparent;
            color: #fdb839;
        }

        .uxc__f-tags .btn {
            margin-right: 15px;
        }

            .uxc__f-tags .btn.btn-default:hover,
            .uxc__f-tags .btn.btn-default:focus,
            .uxc__f-tags .btn.btn-default:active,
            .uxc__f-tags .btn.btn-default.active {
                border-color: #fdb839;
                background: #fdb839;
                color: #fff;
                box-shadow: 0px 0px 0px;
                transition: all 0.3s;
            }

        label.btn.btn-default.active:before {
            content: "\f00c";
            font-family: fontawesome;
            position: absolute;
            top: 7px;
            right: -10px;
            font-size: 13px;
            z-index: 1;
            color: #fff;
            background: #21bb05;
            width: 20px;
            height: 20px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            transition: 0.3s all;
        }

        .f-cancel {
            position: absolute;
            top: 0;
            right: 0;
            color: #ABABAB;
            border: 1px solid #ABABAB;
            width: 25px;
            height: 25px;
            border-radius: 50%;
            display: flex;
            justify-content: center;
            align-items: center;
            transition: all 0.3s;
        }

            .f-cancel:hover {
                border-color: #fdb839;
                color: #fdb839;
                cursor: pointer;
            }

        .add-n-f {
            padding: 30px;
            display: flex;
            justify-content: center;
            align-items: center;
            border: 2px dotted #989898;
            color: #989898;
            border-radius: 4px;
            margin: 0 30px 0 77px;
            transition: all 0.3s;
            cursor: pointer;
        }

            .add-n-f i {
                border: 1px dotted #989898;
                width: 25px;
                height: 25px;
                margin-right: 10px;
                display: flex;
                justify-content: center;
                align-items: center;
                border-radius: 50%;
            }

            .add-n-f:hover {
                border-color: #fdb839;
                color: #fdb839;
            }

                .add-n-f:hover i {
                    color: #fdb839;
                    border-color: #fdb839;
                }

        .handle {
            color: #000;
            cursor: move;
            left: -8px;
            width: 13px;
            height: 40px;
            position: absolute;
            top: 16px;
            background-image: -webkit-repeating-radial-gradient(center center, rgba(0, 0, 0, 0.26), rgba(0, 0, 0, 0.23) 1px, transparent 1px, transparent 100%);
            background-image: -moz-repeating-radial-gradient(center center, rgba(0,0,0,.2), rgba(0,0,0,.2) 1px, transparent 1px, transparent 100%);
            background-image: -ms-repeating-radial-gradient(center center, rgba(0,0,0,.2), rgba(0,0,0,.2) 1px, transparent 1px, transparent 100%);
            background-image: repeating-radial-gradient(center center, rgba(0,0,0,.2), rgba(0,0,0,.2) 1px, transparent 1px, transparent 100%);
            -webkit-background-size: 3px 3px;
            -moz-background-size: 3px 3px;
            background-size: 3px 3px;
        }

        .timeline-placeholder {
            height: 150px;
            border: 2px dashed #ddd;
            left: 71px;
            top: -2px;
        }

            .timeline-placeholder:before {
                left: -75px !important;
            }

            .timeline-placeholder:after {
                content: "Drop it here";
                text-align: center;
                top: 23px;
                position: relative;
                font-size: 39px;
                color: rgba(221, 221, 221, 0.38);
            }

        #sortable {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 60%;
        }

            #sortable li {
                margin: 0 3px 3px 3px;
                padding: 0.4em;
                padding-left: 1.5em;
                font-size: 1.4em;
                height: 18px;
            }

                #sortable li span {
                    position: absolute;
                    margin-left: -1.3em;
                }


        /*=================================================================
Override Bootstrap Tabs
==================================================================*/

        /* Tabs panel */
        .tabbable-panel {
            padding: 10px;
        }

        .tabbable-line > .nav-tabs {
            border: none;
            margin: 0px;
            display: flex;
            justify-content: space-between;
        }

            .tabbable-line > .nav-tabs > li {
                margin: 0px 2px 2px 0;
                transition: 0.5s ease;
            }

                .tabbable-line > .nav-tabs > li > a {
                    border: 0;
                    margin-right: 0;
                    color: #A9A9A9;
                    padding: 4px 0px;
                }

                    .tabbable-line > .nav-tabs > li > a > i {
                        color: #a6a6a6;
                    }

                .tabbable-line > .nav-tabs > li.open, .tabbable-line > .nav-tabs > li:hover {
                    border-bottom: 4px solid @warning-dark;
                }

                    .tabbable-line > .nav-tabs > li.open > a, .tabbable-line > .nav-tabs > li:hover > a {
                        border: 0;
                        background: none !important;
                        color: #333333;
                    }

                        .tabbable-line > .nav-tabs > li.open > a > i, .tabbable-line > .nav-tabs > li:hover > a > i {
                            color: #a6a6a6;
                        }

                    .tabbable-line > .nav-tabs > li.open .dropdown-menu, .tabbable-line > .nav-tabs > li:hover .dropdown-menu {
                        margin-top: 0px;
                    }

                .tabbable-line > .nav-tabs > li.active {
                    border-bottom: 4px solid #fdb839;
                    position: relative;
                    transition: 0.5s ease;
                }

                    .tabbable-line > .nav-tabs > li.active > a {
                        border: 0;
                        color: #333333;
                    }

                        .tabbable-line > .nav-tabs > li.active > a > i {
                            color: #404040;
                        }

        .tabbable-line > .tab-content {
            margin-top: -3px;
            background-color: #fff;
            border: 0;
            border-top: 1px solid #eee;
            padding: 15px 0;
        }

        .portlet .tabbable-line > .tab-content {
            padding-bottom: 0;
        }

        .timeleft:before {
            content: " ";
            height: 0;
            position: absolute;
            top: 22px;
            width: 0;
            z-index: 1;
            right: 35px;
            border: medium solid white;
            border-width: 10px 0 10px 10px;
            border-color: transparent transparent transparent white;
        }

        .containertime {
            width: 382px;
            border: solid 3px #fdb839;
            padding: 10px;
            /* margin-left: -24%; */
            margin-right: 11%;
            background: #ffffff;
            border-radius: 5px;
        }

        .timeright:before {
            content: " ";
            height: 0;
            position: absolute;
            top: 18px;
            width: 0;
            z-index: 1;
            left: 28%;
            border: solid 1px #FFF;
            border-width: 10px 10px 10px 0;
            border-color: transparent #fdb839 transparent transparent;
        }
    </style>
    <style>
        .frm-item {
            width: 100%;
            border: 1px solid #cacaca;
            border-radius: 20px;
            padding: 7px;
            outline: none;
            height: 35px;
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
        th {
        border: 1px solid rgb(204, 204, 204); padding: 8px;background: rgb(0, 122, 255); color: rgb(255, 255, 255); 
        }

        .attattachment-item-holder
        {
        height: auto !important;
        }

        .ui-accordion .ui-accordion-header {
            font-size: 14px;
            font-family: 'TheSans', sans-serif;
            padding: 5px 15px;
            font-weight: bold;
        }

        .ui-accordion .ui-accordion-content {
            padding:0px !important;
        }
    </style>

    
    <script src="../JS/micromodal.min.js"></script>
    <script language="javascript" type="text/javascript">
        var Success;
        var lang =<%= (Session["lang"].ToString() == "0") ? "'en'" : "'ar'"%>;
        //window.onload = function () {
        //    csxi.AutoZoom = true;
        //    csxi.ScaletoGray = true;
        //}

        //function OpenClick() {
        //    if (csxi.LoadDialog()) {
        //        document.form1.savebutton.disabled = false;
        //        document.form1.clearbutton.disabled = false;
        //        document.form1.rotatebutton.disabled = false;
        //    }
        //}

        //function ClearClick() {
        //    Initialisation();
        //    csxi.Clear();
        //}

        //function RotateClick() {
        //    csxi.Rotate(90.0);
        //}

        //function ScanClick() {
        //    csxi.SelectTwainDevice();
        //    csxi.Acquire();
        //    if (csxi.ImageLoaded) {
        //        document.getElementById("showScan").style.display = "inline";
        //    }
        //}
        $(document).on("click", ".lnkopenFile", function (e) {
            var docId = $(this).attr("data-id");
            var docV = $(this).attr("data-verstion");
            var docUser = $(this).attr("data-user");
            var docExt = $(this).attr("data-ext");
            var docDisplay = $(this).attr("data-display");
            var currentURL = location.protocol + '//' + location.host;
            if (docExt == 'pdf' || docDisplay == '') {
                var link = currentURL + "/PdfLauncher.aspx?docID=" + docId + "&ver=" + docV + "&userID=" + docUser + "";
                var convasWidth = 0;
                var convasHeight = 0;
                $("#iframePdfPreviewer").on('load', function () {
                    convasWidth = $(this).contents().find(".pdf-page-canvas").wdith();
                    convasHeight = $(this).contents().find(".pdf-page-canvas").height();
                });
                var dymicWidth = 1380;
                if (lang == 'en') {
                    dymicWidth = 1380;
                }
                //if (convasWidth > 900 && convasWidth < 1000) { //this good
                //    dymicWidth = 1375;
                //}
                //else if (convasWidth > 1000) {
                //    //this subtract
                //    var diff = convasWidth - 918;
                //    dymicWidth = 1375 - diff;
                //}
                //else {
                //    //this add
                //    var diff = 918 - convasWidth;
                //    dymicWidth = 1375 + diff;
                //}
                var cssStyle = 'border: none;transform-origin: right top;width:100% !important;height:850px';
                if (lang != 'ar') {
                    cssStyle = 'border: none;transform-origin: left top;width:100% !important;height:850px';
                }
                var html = "<iframe src='" + link + "' width='" + dymicWidth + "px' style='" + cssStyle + "' id='iframePdfPreviewer'></iframe>";
                $("#divIframViewer").html(html);
            }
            else {
                var docUrl = $(this).attr("data-url");
                $("#divIframViewer").html('<a href="' + docUrl + '"><%= HudhudResources.Resources.Screen_documentInfo_DownloadFile %> </a>');
                
                //if (lang == 'ar') {
                //    $("#divIframViewer").html('<a href="' + docUrl + '">????? ????? </a>');
                //}
                //else {
                //    $("#divIframViewer").html('<a href="' + docUrl + '">Download file </a>');
                //}
            }
        })

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
       


        function addReminder(metaID) {
            $(".popup-overlay, .popup-content").addClass("active");
            var x = event.clientX;     // Get the horizontal coordinate
            var y = event.clientY;     // Get the vertical coordinate
            $(".popup-overlay, .popup-content").css("left", x - 200);
            $(".popup-overlay, .popup-content").css("top", y);
            $("#hdnReminderMetaID").val(String(metaID));
        }

        $("#closeBtn, .popup-overlay").on("click", function () {
            $(".popup-overlay, .popup-content").removeClass("active");
        });

        $(document).ready(
            function() {
                $("#accordion").accordion({
                    collapsible: true
                    //heightStyle: "content"
                });
        } );
    
    </script>
    <style type="text/css">
        .popup-overlay {
            visibility: hidden;
        }

        .popup-content {
            visibility: hidden;
        }

        .popup-overlay.active {
            visibility: visible;
            z-index: 500;
            position: absolute;
            width: 400px;
            background-color: #fff;
            border: solid 1px #ccc;
            padding: 10px;
        }

        .popup-content.active {
            visibility: visible;
        }

        .cellUnderline {
            border-bottom: 1px dashed #ccc;
        }

        .blueTxt {
            font-weight: bold;
            color: #003366;
            width: 25%;
        }

        .optionDiv {
            padding: 5px 15px;
            border-top: 1px dotted #f3f1f1;
        }

        .w3-table, .w3-table-all {
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            display: table;
        }

        .w3-table-all {
            border: 1px solid #ccc;
        }

            .w3-bordered tr, .w3-table-all tr {
                border-bottom: 1px solid #ddd;
            }

        .w3-striped tbody tr:nth-child(even) {
            background-color: #f1f1f1;
        }

        .w3-table-all tr:nth-child(odd) {
            background-color: #fff;
        }

        .w3-table-all tr:nth-child(even) {
            background-color: #f1f1f1;
        }

        .w3-hoverable tbody tr:hover, .w3-ul.w3-hoverable li:hover {
            background-color: #ccc;
        }

        .w3-centered tr th, .w3-centered tr td {
            text-align: center;
        }

        .w3-table td, .w3-table th, .w3-table-all td, .w3-table-all th {
            padding: 8px 8px;
            display: table-cell;
            text-align: left;
            vertical-align: top;
        }

        .w3-striped tbody tr:nth-child(even) {
            background-color: #f1f1f1;
        }
    </style>
    <script type="text/javascript">
        function PrintElem(elem) {
            Popup($(elem).html());
        }

        function Popup(data) {
            var mywindow = window.open('', 'SISCOM_Documents', 'height=400,width=600');
            mywindow.document.write('<html><head><title>SISCOM Documents</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.print();
            //mywindow.close();

            //return true;
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
            //        alert(newType);
            //        alert(currentValue);
            //        alert(newValue);
            if (newValue % 1 == 0) {
                document.getElementById("txtWfTimeFrame").value = String(newValue);
            }
            else {
                document.getElementById("txtWfTimeFrame").value = newValue.toFixed(2);
            }
            document.getElementById("tftype").value = e.options[e.selectedIndex].value;
        }
        function openAddAttach() {
            MicroModal.init()
            MicroModal.show('attach-modal-1');
        }
    </script>
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
    </style>
    <style>
        .tooltip {
            font-family: Arial !important;
            font-size: 13px;
            /*padding:10px;*/
        }

            .tooltip .tooltip-inner {
                /*  padding:10px;*/
                /* font-family: Arial !important;*/
                /*    background-color: #ffc;
                color: #c00;
                */
                /* min-width:300px;*/
                min-height: 30px;
            }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageTitle" runat="server">
    <%--<i class="fas fa-file-alt"></i>--%>
    <ul class="pages_nav">
        <li><a style="cursor: pointer;" href="defaultAr.aspx?CODEN=1&dlgid=2&indexId=0'">
            <%= HudhudResources.Resources.Screen_documentInfo_Folders %>
            </a></li>
        <li>
            <asp:HyperLink ID="lnkParent" runat="server"></asp:HyperLink>
            <%--<label runat="server" id="lblPrent"></label>--%>
        </li>
        <li>
                <asp:Label ID="lblFolderName" CssClass="tree" runat="server" Text="Edit Document"></asp:Label>
        </li>
    </ul>
    
        <asp:Label ID="lblDocCount" runat="server" Text="1 Attachment(s)"
            Font-Size="12px" Visible="false"></asp:Label>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <!-- micro modal -->
    <div class="modal micromodal-slide" id="attach-modal-1" aria-hidden="true">
        <div class="modal__overlay" tabindex="-1" data-micromodal-close>
            <div class="modal__container" role="dialog" aria-modal="true" aria-labelledby="modal-1-title">
                <header class="modal__header">
                    <h2 class="modal__title" id="modal-1-title">
                        <%= HudhudResources.Resources.Screen_documentInfo_AttachNewFile %>
                    </h2>
                    <button class="modal__close" aria-label="Close modal" data-micromodal-close></button>
                </header>
                <main class="modal__content" id="modal-1-content">
                    <div style="background-color: #ccc">
                        <i class="fas fa-folder-plus icon-button"></i>
                        <asp:Label ID="Label1" runat="server" Text="Add new attachment:"></asp:Label>
                        <div class="optionDiv">
                            <i class="fas fa-file-upload icon-button"></i>
                            <%= HudhudResources.Resources.Screen_documentInfo_AttachmentClassification %>
                        </div>
                        <div class="optionDiv">
                            <i class="fas fa-file-image icon-button"></i>
                            <%= HudhudResources.Resources.Screen_documentInfo_FileFormat %> :
                            <span style="display: none; cursor: pointer" id="showScan" onclick="showScanned()">
                                <%= HudhudResources.Resources.Screen_documentInfo_ShowScannedImage %>
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="button" OnClick="LinkButton4_Click" OnClientClick="return UploadClick();">
                                 <img border="0" src="../Images/Icons/Actions-go-up-icon.png" align="absmiddle" />
                                    <%= HudhudResources.Resources.Screen_documentInfo_Upload %>
                                </asp:LinkButton>
                        </div>
                    </div>
                </main>
                <footer class="modal__footer">
                </footer>
            </div>
        </div>
    </div>
    <!-- Modal Add file-->
    <div class="modal fade my-modal my-modal-lg" id="add-file" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="background-color: rgba(0,0,0,0.4);">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">
                        <%= HudhudResources.Resources.Screen_documentInfo_AttachNewFile %>
                    </h4>
                </div>
                <div class="modal-body">
                    <div>
                        <!-- Nav tabs -->
                        <ul class="ul-files-upload" role="tablist">
                            <li role="presentation" class="active">
                                <a href="#upload" role="tab" data-toggle="tab" onclick="$('#btnScanFile').hide();$('#ContentPlaceHolder1_ContentPlaceHolderBody_LinkButton3').show();">
                                    <svg id="Group_2701" data-name="Group 2701" xmlns="http://www.w3.org/2000/svg" width="22.139" height="22.139" viewBox="0 0 22.139 22.139">
                                        <g id="Group_2345" data-name="Group 2345" transform="translate(10.147 10.147)">
                                            <path id="Path_7011" data-name="Path 7011" d="M240.666,234.67a6,6,0,1,0,6,6A6,6,0,0,0,240.666,234.67Zm3.072,5.973a.691.691,0,0,1-.973.1l-1.407-1.151V243.2a.692.692,0,1,1-1.384,0v-3.613l-1.407,1.151a.692.692,0,0,1-.876-1.071l2.537-2.076a.691.691,0,0,1,.876,0l2.536,2.076A.69.69,0,0,1,243.737,240.643Z" transform="translate(-234.67 -234.67)" fill="#8f9198"></path>
                                        </g>
                                        <g id="Group_2346" data-name="Group 2346">
                                            <path id="Path_7012" data-name="Path 7012" d="M12.223,0H2.537A2.537,2.537,0,0,0,0,2.537v14.3a2.537,2.537,0,0,0,2.537,2.537H9.511a7.089,7.089,0,0,1-.747-3.229c.04-2.811,1.841-6.639,6-7.25V2.537A2.537,2.537,0,0,0,12.223,0Zm.16,8.871H2.5a.5.5,0,1,1,0-.995h9.884a.5.5,0,1,1,0,.995Zm0-2.511H2.5a.5.5,0,1,1,0-.995h9.884a.5.5,0,1,1,0,.995Zm0-2.511H2.5a.5.5,0,1,1,0-.995h9.884a.5.5,0,1,1,0,.995Z" fill="#8f9198"></path>
                                        </g>
                                    </svg>
                                    <%= HudhudResources.Resources.Screen_documentInfo_UploadingFile %>
                            </li>
                            <li role="presentation" class="">
                                <a href="#scan" role="tab" data-toggle="tab" onclick="$('#btnScanFile').show();$('#ContentPlaceHolder1_ContentPlaceHolderBody_LinkButton3').hide();">
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
                                    <%= HudhudResources.Resources.Screen_documentInfo_Scanning %>
                            </li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="upload">
                                <div class="row upload-file-holder">
                                    <div class="col-xs-4">
                                        <div class="upload-file-input-holder">
                                            <asp:FileUpload ID="fluFile" runat="server" CssClass="input-file-hidden" AllowMultiple="true" onchange="drawFiles(this);" />
                                            <p><%= HudhudResources.Resources.Screen_documentInfo_ChooseFile %></p>
                                            <p><%= HudhudResources.Resources.Screen_documentInfo_Or %></p>
                                            <p><%= HudhudResources.Resources.Screen_documentInfo_DragAndDropYourFileHere %></p>
                                        </div>
                                        <div class="main-field-holder">
                                            <label class="main-label">
                                                <%= HudhudResources.Resources.Screen_documentInfo_AttachmentClassification %>
                                            </label>
                                            <asp:DropDownList ID="drpDocGroupID" CssClass="new-drop" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xs-8">
                                        <div class="files-area">
                                            <div class="file-empty" id="fileEmpty" runat="server" visible="true">
                                                <%= HudhudResources.Resources.Screen_documentInfo_ThereAreNoUploadedFiles %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="scan">
                                <div class="row upload-file-holder">
                                    <div class="col-xs-4">
                                        <a href="Resources/DynamsoftServiceSetup.exe">
                                            <%= HudhudResources.Resources.Screen_documentInfo_DownloadPlugin %>
                                        </a>
                                        <div class="main-field-holder">
                                            <label class="main-label">
                                                <%= HudhudResources.Resources.Screen_documentInfo_AttachmentClassification %>
                                            </label>
                                            <asp:DropDownList runat="server" CssClass="new-drop" ID="drpFormat" ClientIDMode="Static">
                                                <asp:ListItem Value="pdf">PDF</asp:ListItem>
                                                <asp:ListItem Value="jpg">JPEG</asp:ListItem>
                                                <asp:ListItem Value="tiff">TIFF multi pages</asp:ListItem>
                                                <asp:ListItem Value="png">PNG</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <select id="source"></select>
                                        </div>
                                    </div>
                                    <div class="col-xs-8">
                                        <div class="files-area files-area-scan">
                                            <div class="file-empty" id="fileEmpty2" runat="server" visible="true">
                                                <%= HudhudResources.Resources.Screen_documentInfo_ThereAreNoUploadedFiles %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--             <asp:LinkButton ID="LinkButton3" CssClass="button" runat="server" OnClick="LinkButton3_Click"
                                TabIndex="21">
                            <i class="fas fa-file-upload"></i>
                            <%= (Session["lang"].ToString() == "0") ? "Upload" : "&#1578;&#1581;&#1605;&#1610;&#1604;"%>
                            </asp:LinkButton>--%>
                    <asp:LinkButton ID="LinkButton3" CausesValidation="false" runat="server" OnClick="LinkButton3_Click" CssClass="btn-done-model">
                        <%= HudhudResources.Resources.Screen_documentInfo_Save %></asp:LinkButton>
                    <input type="button" id="btnScanFile" style="display: none" name="scanbutton" value="Scan Image" class="btn-done-model" onclick="AcquireImage();" />
                    <%--<button type="button" runat="server" id="btnScanFile" style="display: none" class="btn-done-model" causesvalidation="false" onclick="AcquireImage();"><%= (Session["lang"].ToString() == "0") ? "Scan" : "??? "%> </button>--%>
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
                        <%= HudhudResources.Resources.Screen_documentInfo_Cancel %>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!-- new ui -->
    <ul class="ul-edit-doc-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#edit-info" role="tab" data-toggle="tab">
            <svg xmlns="http://www.w3.org/2000/svg" width="16.706" height="19.234" viewBox="0 0 16.706 19.234">
                <g id="Group_2681" data-name="Group 2681" transform="translate(-96 -34.728)">
                    <path id="Path_7066" data-name="Path 7066" d="M110.352,58.884a3,3,0,0,1-2.558-4.554H98.529A2.531,2.531,0,0,0,96,56.859V70.19a2.53,2.53,0,0,0,2.529,2.529H109.1a2.531,2.531,0,0,0,2.529-2.529V58.6A2.981,2.981,0,0,1,110.352,58.884Zm-3.089,11.076h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.218h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Z" transform="translate(0 -18.757)" fill="#8f9198"></path>
                    <g id="Group_2680" data-name="Group 2680" transform="translate(108.047 34.728)">
                        <g id="Group_2679" data-name="Group 2679" transform="translate(0 0)">
                            <path id="Path_7067" data-name="Path 7067" d="M379.493,35.41a2.33,2.33,0,1,0,.682,1.647A2.314,2.314,0,0,0,379.493,35.41Zm-1.647-.045a.5.5,0,1,1-.5.5A.5.5,0,0,1,377.846,35.365Zm.637,3.185h-1.274v-.273h.273V36.912h-.273v-.273h1v1.638h.273Z" transform="translate(-375.516 -34.728)" fill="#8f9198"></path>
                        </g>
                    </g>
                </g>
            </svg>
            <%= HudhudResources.Resources.Screen_documentInfo_DocumentInformation %>
        </a>
        </li>
        <li role="presentation"><a href="#edit-attachments" onclick="openLastElement();" role="tab" data-toggle="tab">
            <svg xmlns="http://www.w3.org/2000/svg" width="20.218" height="17.691" viewBox="0 0 20.218 17.691">
                <g id="Group_2678" data-name="Group 2678" transform="translate(0 -42.667)">
                    <path id="Path_7064" data-name="Path 7064" d="M19.376,108.874v1.584a4.633,4.633,0,0,1-9.267,0v-2.106H8.77a.22.22,0,0,1-.143-.059l-1.2-1.2a1.46,1.46,0,0,0-1.036-.43H2.317A2.315,2.315,0,0,0,0,108.984v10.53a2.315,2.315,0,0,0,2.317,2.317H17.9a2.315,2.315,0,0,0,2.317-2.317v-8.845A2.341,2.341,0,0,0,19.376,108.874Z" transform="translate(0 -61.473)" fill="#8f9198"></path>
                    <path id="Path_7065" data-name="Path 7065" d="M301.615,51.934a2.952,2.952,0,0,1-2.948-2.948V44.562a1.9,1.9,0,0,1,3.791,0v4.212a.632.632,0,0,1-1.264,0V44.562a.632.632,0,0,0-1.264,0v4.423a1.685,1.685,0,0,0,3.37,0V43.3a.632.632,0,1,1,1.264,0v5.686A2.952,2.952,0,0,1,301.615,51.934Z" transform="translate(-286.873)" fill="#8f9198"></path>
                </g>
            </svg>
            <%= HudhudResources.Resources.Screen_documentInfo_Attachments %>
        </a>
        </li>

        <li role="presentation" class=""><a href="#edit-tasks " role="tab" data-toggle="tab">
            <svg xmlns="http://www.w3.org/2000/svg" width="17.369" height="17.66" viewBox="0 0 17.369 17.66">
                <path id="Path_6926" data-name="Path 6926" d="M53.755,31.629V23.612a1.65,1.65,0,0,0-1.648-1.648H50.372V21a.8.8,0,0,0-.8-.8h-.127a.8.8,0,0,0-.8.8v.964H42.513V21a.8.8,0,0,0-.8-.8h-.127a.8.8,0,0,0-.8.8h0v.964H39.068a1.649,1.649,0,0,0-1.648,1.648V35.149A1.65,1.65,0,0,0,39.068,36.8h9.46a3.676,3.676,0,0,0,5.227-5.168Zm-1.4,3.793a.335.335,0,0,1-.473,0l-.488-.488-.515-.515a.333.333,0,0,1-.1-.236V32.176a.334.334,0,1,1,.668,0v1.868l.448.448.457.457A.334.334,0,0,1,52.351,35.422ZM39.283,25.953H51.894v4.638a3.6,3.6,0,0,0-.782-.087,3.682,3.682,0,0,0-3.678,3.678,3.582,3.582,0,0,0,.08.751H39.283Z" transform="translate(-37.42 -20.2)" fill="#8f9198"></path>
            </svg>
            <%= HudhudResources.Resources.Screen_documentInfo_ToDoList %>
        </a>
        </li>
        <li role="presentation" class=""><a href="#edit-events" role="tab" data-toggle="tab">
            <svg id="election-event-on-a-calendar-with-star-symbol" xmlns="http://www.w3.org/2000/svg" width="16.543" height="16.806" viewBox="0 0 16.543 16.806">
                <g id="Group_1653" data-name="Group 1653">
                    <path id="Path_6083" data-name="Path 6083" d="M39.877,54.853l-.258,1.531a.558.558,0,0,0,.809.588l1.377-.719,1.377.719a.559.559,0,0,0,.81-.588l-.258-1.531,1.109-1.088a.559.559,0,0,0-.309-.952L43,52.586,42.305,51.2a.559.559,0,0,0-1,0l-.691,1.391-1.536.228a.559.559,0,0,0-.309.952Z" transform="translate(-33.534 -44.034)" fill="#8f9198"></path>
                    <path id="Path_6084" data-name="Path 6084" d="M15.851,1.787H14.092V.811A.811.811,0,0,0,13.281,0h-.129a.811.811,0,0,0-.811.811v.976H6.135V.811A.811.811,0,0,0,5.324,0H5.2a.811.811,0,0,0-.811.811v.976H2.646A1.671,1.671,0,0,0,.977,3.455V15.137a1.671,1.671,0,0,0,1.669,1.669H15.851a1.671,1.671,0,0,0,1.669-1.669V3.455A1.671,1.671,0,0,0,15.851,1.787Zm-.218,13.133H2.864V5.826h12.77Z" transform="translate(-0.977 0)" fill="#8f9198"></path>
                </g>
            </svg>
            <%= HudhudResources.Resources.Screen_documentInfo_Events %>
        </a>
        </li>
        <li role="presentation" class=""><a href="#edit-pathes" role="tab" data-toggle="tab">
            <svg xmlns="http://www.w3.org/2000/svg" width="16.706" height="19.234" viewBox="0 0 16.706 19.234">
                <g id="Group_2681" data-name="Group 2681" transform="translate(-96 -34.728)">
                    <path id="Path_7066" data-name="Path 7066" d="M110.352,58.884a3,3,0,0,1-2.558-4.554H98.529A2.531,2.531,0,0,0,96,56.859V70.19a2.53,2.53,0,0,0,2.529,2.529H109.1a2.531,2.531,0,0,0,2.529-2.529V58.6A2.981,2.981,0,0,1,110.352,58.884Zm-3.089,11.076h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.218h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Z" transform="translate(0 -18.757)" fill="#8f9198"></path>
                    <g id="Group_2680" data-name="Group 2680" transform="translate(108.047 34.728)">
                        <g id="Group_2679" data-name="Group 2679" transform="translate(0 0)">
                            <path id="Path_7067" data-name="Path 7067" d="M379.493,35.41a2.33,2.33,0,1,0,.682,1.647A2.314,2.314,0,0,0,379.493,35.41Zm-1.647-.045a.5.5,0,1,1-.5.5A.5.5,0,0,1,377.846,35.365Zm.637,3.185h-1.274v-.273h.273V36.912h-.273v-.273h1v1.638h.273Z" transform="translate(-375.516 -34.728)" fill="#8f9198"></path>
                        </g>
                    </g>
                </g>
            </svg>
            <%= HudhudResources.Resources.Screen_documentInfo_DocumentWorkflow %>
        </a>
        </li>

        <li role="presentation" class=""><a href="#edit-transfer" role="tab" data-toggle="tab">
            <svg xmlns="http://www.w3.org/2000/svg" width="16.706" height="19.234" viewBox="0 0 16.706 19.234">
                <g id="Group_2681" data-name="Group 2681" transform="translate(-96 -34.728)">
                    <path id="Path_7066" data-name="Path 7066" d="M110.352,58.884a3,3,0,0,1-2.558-4.554H98.529A2.531,2.531,0,0,0,96,56.859V70.19a2.53,2.53,0,0,0,2.529,2.529H109.1a2.531,2.531,0,0,0,2.529-2.529V58.6A2.981,2.981,0,0,1,110.352,58.884Zm-3.089,11.076h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.218h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Z" transform="translate(0 -18.757)" fill="#8f9198"></path>
                    <g id="Group_2680" data-name="Group 2680" transform="translate(108.047 34.728)">
                        <g id="Group_2679" data-name="Group 2679" transform="translate(0 0)">
                            <path id="Path_7067" data-name="Path 7067" d="M379.493,35.41a2.33,2.33,0,1,0,.682,1.647A2.314,2.314,0,0,0,379.493,35.41Zm-1.647-.045a.5.5,0,1,1-.5.5A.5.5,0,0,1,377.846,35.365Zm.637,3.185h-1.274v-.273h.273V36.912h-.273v-.273h1v1.638h.273Z" transform="translate(-375.516 -34.728)" fill="#8f9198"></path>
                        </g>
                    </g>
                </g>
            </svg>
            <%= HudhudResources.Resources.Screen_documentInfo_ShareAttachments %>
        </a>
        </li>

        <li role="presentation" class=""><a href="#edit-signature" role="tab" data-toggle="tab">
            <svg xmlns="http://www.w3.org/2000/svg" width="23.333" height="20.691" viewBox="0 0 23.333 20.691">
                <g id="Group_2690" data-name="Group 2690" transform="translate(0 -28.984)">
                    <g id="Group_2683" data-name="Group 2683" transform="translate(0 41.781)">
                        <g id="Group_2682" data-name="Group 2682">
                            <path id="Path_7068" data-name="Path 7068" d="M13.3,312.432a2.013,2.013,0,0,1-1.032.552l-3.173.635a2.018,2.018,0,0,1-2.414-1.935l-1.4,1.191a1.425,1.425,0,0,1-.927.345,1.443,1.443,0,0,1-.606-.135,1.421,1.421,0,0,1-.834-1.3v-.575a.089.089,0,0,0-.136-.076L0,312.865v4.159a.673.673,0,0,0,.673.673H15.256a.673.673,0,0,0,.673-.673V309.8Z" transform="translate(0 -309.803)" fill="#8f9198"></path>
                        </g>
                    </g>
                    <g id="Group_2685" data-name="Group 2685" transform="translate(0 33.97)">
                        <g id="Group_2684" data-name="Group 2684" transform="translate(0 0)">
                            <path id="Path_7069" data-name="Path 7069" d="M.673,138.4a.673.673,0,0,0-.673.673v8.614L2.072,146.4a1.435,1.435,0,0,1,2.191,1.22v.575a.089.089,0,0,0,.147.068l1.929-1.643a.673.673,0,0,1,.615-.136l.4-2a2.013,2.013,0,0,1,.552-1.032l5.047-5.047H.673Z" transform="translate(0 -138.4)" fill="#8f9198"></path>
                        </g>
                    </g>
                    <g id="Group_2687" data-name="Group 2687" transform="translate(16.949 28.984)">
                        <g id="Group_2686" data-name="Group 2686">
                            <path id="Path_7070" data-name="Path 7070" d="M378.1,30.45l-1.269-1.269a.673.673,0,0,0-.952,0l-.793.793-.159-.159a.673.673,0,0,0-.952,0l-2.062,2.062,3.49,3.49,2.062-2.062a.673.673,0,0,0,0-.952l-.159-.159.793-.793A.673.673,0,0,0,378.1,30.45Z" transform="translate(-371.908 -28.984)" fill="#8f9198"></path>
                        </g>
                    </g>
                    <g id="Group_2689" data-name="Group 2689" transform="translate(8.026 32.83)">
                        <g id="Group_2688" data-name="Group 2688" transform="translate(0 0)">
                            <path id="Path_7071" data-name="Path 7071" d="M184.091,113.375l-7.139,7.139a.674.674,0,0,0-.184.344l-.635,3.173a.673.673,0,0,0,.792.792l3.173-.635a.673.673,0,0,0,.344-.184l7.139-7.139Z" transform="translate(-176.12 -113.375)" fill="#8f9198"></path>
                        </g>
                    </g>
                </g>
            </svg>
            <%= HudhudResources.Resources.Screen_documentInfo_Signatures %>
        </a>
        </li>
        <%-- <li role="presentation" class=""><a href="#edit-transfer" role="tab" data-toggle="tab">
                            <svg id="Group_2691" data-name="Group 2691" xmlns="http://www.w3.org/2000/svg" width="15.095" height="15.095" viewBox="0 0 15.095 15.095">
                                <path id="Path_7072" data-name="Path 7072" d="M13.365,0H1.73A1.731,1.731,0,0,0,0,1.73V13.365a1.731,1.731,0,0,0,1.73,1.73H13.365a1.731,1.731,0,0,0,1.73-1.73V1.73A1.731,1.731,0,0,0,13.365,0Zm.472,13.365a.472.472,0,0,1-.472.472H1.73a.472.472,0,0,1-.472-.472V1.73a.472.472,0,0,1,.472-.472H13.365a.472.472,0,0,1,.472.472Z" fill="#8f9198"></path>
                                <path id="Path_7073" data-name="Path 7073" d="M30,38V50.913H43.119V38Zm2.889,9.257a.629.629,0,0,1,0-1.258h5.032V44.9a.472.472,0,0,1,.763-.371l2.2,1.73a.472.472,0,0,1,0,.742l-2.2,1.73a.472.472,0,0,1-.763-.371v-1.1Zm7.547-4.4H35.4v1.1a.472.472,0,0,1-.763.371l-2.2-1.73a.472.472,0,0,1,0-.742l2.2-1.73a.472.472,0,0,1,.763.371v1.1h5.032a.629.629,0,0,1,0,1.258Z" transform="translate(-29.116 -36.88)" fill="#8f9198"></path>
                            </svg>
                            ?????
                        </a>
                    </li>--%>
    </ul>
    <div class="tab-content">
        <!-- back here -->
        <div class="white-holder tab-pane active" id="edit-info">
            <div class="max-width-holder">
                <div class="col-xs-4">
                    <div class="main-field-holder">
                        <asp:HiddenField ID="hdnURL" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hdnDocPath" runat="server" />
                        <asp:HiddenField ID="hdnUserCode" ClientIDMode="Static" runat="server" />
                        <label class="main-label">
                            <%= HudhudResources.Resources.Screen_documentInfo_DocumentID %></label>
                        <asp:TextBox ID="txtDocID" ReadOnly="True" runat="server" CssClass="main-input"
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

                <div class="col-xs-4" runat="server" id="divForExport" visible="false">
                    <div class="main-field-holder">
                        <label class="main-label">
                            <asp:Label ID="lblExport" runat="server" Text=""></asp:Label>
                        </label>
                        <asp:TextBox ID="txtExportCode" ReadOnly="True" runat="server" CssClass="main-input"
                            TabIndex="2"></asp:TextBox>
                    </div>
                    <%--<asp:TextBox  runat="server" CssClass="main-input" TabIndex="3" ReadOnly="true"></asp:TextBox>--%>
                </div>
                <div class="col-xs-4"></div>
                <asp:Panel ID="pnlDocMetas" runat="server" CssClass="tblMetas" Width="100%"></asp:Panel>
            </div>
            <div class="control-side-holder control-side-holder-footer">
                <div class="start-side">
                    <asp:LinkButton ID="lnkSaveDoc" CssClass="btn-main" runat="server" OnClick="lnkSaveDoc_Click"
                        TabIndex="22">
                    <div class="btn-main-wrapper">
                            <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                            </svg>
                            <%= HudhudResources.Resources.Screen_documentInfo_Save %>
                        </div>
                    </asp:LinkButton>
                    <a href="defaultAr.aspx?CODEN=1&dlgid=2&indexId=undefined" class="btn-main btn-white" runat="server" id="btnback" causesvalidation="false">
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
                            <%= HudhudResources.Resources.Screen_documentInfo_Back %>
                        </div>
                    </a>
                </div>
                <div class="end-side">
                    <asp:LinkButton ID="LinkButton2" CssClass="btn-main " runat="server" OnClick="LinkButton2_Click"
                        TabIndex="23" OnClientClick="return confirm('&#1607;&#1604; &#1571;&#1606;&#1578; &#1605;&#1578;&#1571;&#1603;&#1583; &#1605;&#1606; &#1581;&#1584;&#1601; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583;&#1567;');">
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
                            <%= HudhudResources.Resources.Screen_documentInfo_Delete %>
                        </div>
                    </asp:LinkButton>
                    <asp:HyperLink class="button" runat="server" ID="lnkPrint"
                        TabIndex="23" CssClass="btn-main">
                        <div class="btn-main-wrapper">
                            <i class="fas fa-print"></i>
                            <%= HudhudResources.Resources.Screen_documentInfo_Print %>
                        </div>
                    </asp:HyperLink>

                    <asp:LinkButton class="button" runat="server" ID="lnkExport" OnClick="lnkExport_Click" 
                        TabIndex="23" CssClass="btn-main">
                        <div class="btn-main-wrapper">
                            <i class="fas fa-file-upload"></i>
                            <%= HudhudResources.Resources.Screen_documentInfo_MakeOutgoing %>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="white-holder tab-pane" id="edit-attachments">
            <div class="row">
                <div class="col-xs-3">
                    <div class="center-side">
                        <a class="btn-main" data-toggle="modal" data-target="#add-file" onclick="$('#btnScanFile').hide();$('#ContentPlaceHolder1_ContentPlaceHolderBody_LinkButton3').show();">
                            <div class="btn-main-wrapper">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                                    <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                                </svg>
                                <%= HudhudResources.Resources.Screen_documentInfo_AddNew %>
                            </div>
                        </a>
                    </div>

                    <div class="attachment-side">
                        <div id="accordion">
                        <asp:ListView runat="server" ItemPlaceholderID="placeHolderCustomer" ID="LstCategories">
                            
                            <ItemTemplate>
                                <h3>
                                    <%#  (Session["lang"].ToString() == "0") ? Eval("groupname") : Eval("groupnamear") %></h3>
                                <div  class="attattachment-item-holder">
                                    <ul class="attachment-files-list">
                                    <asp:ListView ID="lstFiles" runat="server" DataSource='<%# Eval("FilesList") %>' ItemPlaceholderID="addressPlaceHolder">
                                        <ItemTemplate>
                                            <li class="active lifile" data-id="<%# Eval("docId") %>" data-version="<%# Eval("version") %>"><%# string.IsNullOrEmpty(Eval("DocumentFileName").ToString()) ? string.Concat(Eval("docName"), "-", Eval("version")) : Eval("DocumentFileName").ToString()%>
                                                <div class="attachment-files-list-controll">
                                                    <asp:LinkButton runat="server" ID="lnkDownlod" CommandArgument='<%# string.Concat(Eval("docId"), "-", Eval("version"),"."+Eval("ext") )%>' OnClick="lnkDownlod_Click" CausesValidation="false">
                                                        <div class="btn-download-file" title="?????" data-toggle="tooltip" data-placement="top" data-id="<%# Eval("docId") %>" data-ext="<%# Eval("ext") %>" data-user="<%# Session["userID"].ToString() %>" data-verstion="<%# Eval("version") %>" >
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="19" height="20" viewBox="0 0 19 20">
                                                            <g id="Group_2675" data-name="Group 2675" transform="translate(-0.037 0)">
                                                                <ellipse id="Ellipse_578" data-name="Ellipse 578" cx="9.5" cy="10" rx="9.5" ry="10" transform="translate(0.037 0)" fill="#0072ff" />
                                                                <g id="Group_2673" data-name="Group 2673" transform="translate(4.7 4.762)">
                                                                    <g id="Group_2670" data-name="Group 2670" transform="translate(2.215)">
                                                                        <g id="Group_2669" data-name="Group 2669" transform="translate(0)">
                                                                            <path id="Path_7062" data-name="Path 7062" d="M133.034,4.617a.316.316,0,0,0-.288-.185H131.48V.317A.317.317,0,0,0,131.163,0H129.9a.317.317,0,0,0-.316.317V4.431h-1.266a.316.316,0,0,0-.238.525l2.216,2.532a.316.316,0,0,0,.476,0l2.216-2.532A.316.316,0,0,0,133.034,4.617Z" transform="translate(-127.998)" fill="#fff" />
                                                                        </g>
                                                                    </g>
                                                                    <g id="Group_2672" data-name="Group 2672" transform="translate(0 6.963)">
                                                                        <g id="Group_2671" data-name="Group 2671" transform="translate(0)">
                                                                            <path id="Path_7063" data-name="Path 7063" d="M24.229,352v1.9H17.266V352H16v2.532a.633.633,0,0,0,.633.633h8.229a.632.632,0,0,0,.633-.633V352Z" transform="translate(-16 -352)" fill="#fff" />
                                                                        </g>
                                                                    </g>
                                                                </g>
                                                            </g>
                                                        </svg>
                                                    </div>
                                                    </asp:LinkButton>
                                                    <div class="btn-edit-file lnkopenFile" data-id="<%# Eval("docIdInc") %>" data-ext="<%# Eval("ext") %>" data-user="<%# Session["userID"].ToString() %>" data-verstion="<%# Eval("version") %>" data-display="<%# Eval("display") %>" title="???" data-toggle="tooltip" data-placement="top" style='display: <%# Eval("display") %>'>
                                                        <%--<svg id="Group_1820" data-name="Group 1820" xmlns="http://www.w3.org/2000/svg" width="20.31" height="20.31" viewBox="0 0 20.31 20.31">
                                                            <path id="Path_6947" data-name="Path 6947" d="M10.156,0A10.155,10.155,0,1,0,20.311,10.155,10.155,10.155,0,0,0,10.156,0Zm4.828,7.308-.965.965L12.056,6.309l-.745.745,1.963,1.963L8.458,13.833,6.5,11.87l-.745.745,1.963,1.963-.48.48-.009-.009a.38.38,0,0,1-.243.174l-1.831.408a.381.381,0,0,1-.454-.455l.408-1.83a.381.381,0,0,1,.175-.243l-.009-.009,7.75-7.75a.291.291,0,0,1,.412,0L14.984,6.9A.291.291,0,0,1,14.983,7.308Z" transform="translate(-0.001)" fill="#0072ff" />
                                                        </svg>--%>
                                                        <svg version="1.2" baseProfile="tiny-ps" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" width="20" height="20">
	                                                        <title>New Project</title>
	                                                        <defs>
		                                                        <image  width="20" height="20" id="img1" href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUBAMAAAB/pwA+AAAAAXNSR0IB2cksfwAAACdQTFRFAAAAAHL/AHL/AHL/AHL/AHL/AHL/AHL/AHL/AHL/AHL/AHL/AHL/H0p+hwAAAA10Uk5TAGCfz/9PUN+Qj+Cg0JoE4FkAAABiSURBVHicY2BgYBAycVZlAIN0FyAoA7E4XMBgApDZAmF6MDCwuUBBAgMXjFnAIAJjujFsgTG9GY7AmM4MLnCAzIQr8EHWhmQY3IoFyBYjOQfmyAaQg7NArGUQb2Qf8dkGpAB3lzFOL0dZzQAAAABJRU5ErkJggg=="/>
		                                                        <image  width="16" height="12" id="img2" href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAMCAMAAABcOc2zAAAAAXNSR0IB2cksfwAAAM9QTFRF////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////y0Y0tQAAAEV0Uk5TAAEKIDxSBitppbeypGgqLo9rQmyMLQVMoGUeNK7crTOeSTCpixV+9f8UiKcxnIoIMsyYlJ27upkko4d9piZHBKIptmcOj4GrDQAAAKFJREFUeJxNjtcWgjAQRDdUKcYeDQrYaCpNsYui8v/fZIAX79vsOTN7AQAQxwuiKPAcghpJbimqpql6G0t17nSVXp8x0IcjwvJ4Qo3pzDQt26DzBYHlau04ruf7nhs4m+0OwiiGJPUZaQJxtIdDhpF8rA4njPD8DJfrLQjqyp1VHjmQZ0gN22pGMzYKLzkq/t8CkDctPpXYtxGrQGWel434D6kyEtML+6nfAAAAAElFTkSuQmCC"/>
	                                                        </defs>
	                                                        <style>
		                                                        tspan { white-space:pre }
	                                                        </style>
	                                                        <use id="Layer 3" href="#img1" x="0" y="0" />
	                                                        <use id="Background" href="#img2" x="2" y="4" />
                                                        </svg>
                                                    </div>
                                                    <div class="btn-remove-file" onclick="DeleteFileVersion(this);" data-id="<%# Eval("docId") %>" data-version="<%# Eval("version") %>" title="???" data-toggle="tooltip" data-placement="top">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 20 20">
                                                            <g id="Group_2676" data-name="Group 2676" transform="translate(-0.037 0)">
                                                                <circle id="Ellipse_577" data-name="Ellipse 577" cx="10" cy="10" r="10" transform="translate(0.037 0)" fill="#0072ff" />
                                                                <g id="Group_2674" data-name="Group 2674" transform="translate(5.461 5.868)">
                                                                    <path id="Path_7057" data-name="Path 7057" d="M64,133.868a1.175,1.175,0,0,0,1.174,1.174h4.694a1.175,1.175,0,0,0,1.174-1.174V128H64Z" transform="translate(-63.413 -125.653)" fill="#fff" />
                                                                    <path id="Path_7058" data-name="Path 7058" d="M37.281.587V0H34.934V.587H32V1.76h8.215V.587Z" transform="translate(-32)" fill="#fff" />
                                                                </g>
                                                            </g>
                                                        </svg>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        </div>

                        <asp:Label ID="lblgroups" runat="server" ForeColor="#CC0000"></asp:Label>

                        <asp:Label ID="lblRes" runat="server" ForeColor="Red"></asp:Label>
                        <asp:HiddenField ID="hdnLastVersion" runat="server" />
                        
                    </div>
                </div>
                <div class="col-xs-9">
                    <%--    <div>
                        <img src="img/screenshot.png" style="width: 100%;">
                    </div>--%>
                    <%--<div style="width: 185%; height: 600px;" class="attachment-preview-holder " id="divIframViewer">
                                </div>--%>
                    <div style="height: 875px;" class="attachment-preview-holder " id="divIframViewer">
                    </div>
                </div>
            </div>
        </div>

        <div class="white-holder tab-pane" id="edit-tasks">
            <iframe
                id="todoListFrame" runat="server" scrolling="no" name="todoListFrame" width="100%" height="2000" class="iframe-subpage"
                frameborder="0" style="overflow: hidden; height: 2000px">
                <p>Your browser does not support iframes.</p>
            </iframe>
        </div>

        <div class="white-holder tab-pane" id="edit-events">
            <iframe
                id="eventsIframe" runat="server" scrolling="no" name="eventsIframe" width="100%" height="2000"
                frameborder="0" style="overflow: hidden; height: 2000px" class="iframe-subpage">
                <p>Your browser does not support iframes.</p>
            </iframe>
        </div>
        <div class="white-holder tab-pane" id="edit-pathes">
            <div id="divWF" class="row">
                <div class="col-lg-6">
                <asp:Repeater ID="rptWorkflow" runat="server">
                    <HeaderTemplate>
                        <table cellspacing="0" cellpadding="5" style="width: 100%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <span style="font-weight: bold; color: #AE0000"><%# getCounter() %></span>
                            </td>
                            <td class="cellUnderline blueTxt">
                                <%= HudhudResources.Resources.Screen_documentInfo_User %>
                            </td>
                            <td class="cellUnderline">
                                <%# Eval("fullName")%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="cellUnderline blueTxt">
                                <%= HudhudResources.Resources.Screen_documentInfo_ReceiptDateTime %>
                            </td>
                            <td class="cellUnderline">
                                <%# Convert.ToDateTime( Eval("receiveDate")).ToString("dd/MM/yyyy hh:mm tt")%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="cellUnderline blueTxt">
                                <%= HudhudResources.Resources.Screen_documentInfo_Action %>
                            </td>
                            <td class="cellUnderline">
                                <%# getWFAction(c.convertToInt32( Eval("actionType")))%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="cellUnderline blueTxt">
                                <%= HudhudResources.Resources.Screen_documentInfo_ActionDateTime %>
                            </td>
                            <td class="cellUnderline">
                                <%# (c.convertToDateTime(Eval("actionDateTime")) > DateTime.Now) ? "-" : Convert.ToDateTime(Eval("actionDateTime")).ToString("dd/MM/yyyy hh:mm tt")%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="cellUnderline blueTxt">
                                <%= HudhudResources.Resources.Screen_documentInfo_ActionEndDate %>
                            </td>
                            <td class="cellUnderline">
                                <%# (Eval("EndDate") == DBNull.Value) ? "-" : Convert.ToDateTime(Eval("EndDate")).ToString("dd/MM/yyyy hh:mm tt")%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="blueTxt">
                                <%= HudhudResources.Resources.Screen_documentInfo_Notes %>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <%# Eval("userNotes")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Panel ID="pnlNoPath" runat="server" Visible="false">
                    <div style="text-align:center;">
                        <h2>
                            <%= HudhudResources.Resources.Screen_documentInfo_documentNotRelated %>
                        </h2>
                    </div>
                </asp:Panel>
                </div>
                <div class="col-lg-6">
                    <div class="col-xs-12">
                        <div class="main-field-holder">
                            <label class="main-label">
                                <%= HudhudResources.Resources.Screen_documentInfo_ToUser %>
                            </label>
                            <asp:DropDownList ID="drpUsers" CssClass="main-input" runat="server"></asp:DropDownList>
                        </div>
                        <div class="main-field-holder">
                            <label class="main-label">
                                <%= HudhudResources.Resources.Screen_documentInfo_Notes %>
                            </label>
                            <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="5" Height="120px" CssClass="main-input" runat="server"></asp:TextBox>
                        </div>
                        <div class="main-field-holder">
                            <asp:LinkButton ID="lnkForward" CssClass="btn-main" CausesValidation="false" runat="server" OnClick="lnkForward_Click">
                                <div class="btn-main-wrapper">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="17.133" height="14.277" viewBox="0 0 17.133 14.277">
                                        <path id="Path_7093" data-name="Path 7093" d="M-318.14-5.263A.536.536,0,0,0-318.7-5.3L-334.76,3.086a.537.537,0,0,0-.286.515.536.536,0,0,0,.36.466l4.465,1.526,9.509-8.131-7.358,8.865,7.483,2.558a.548.548,0,0,0,.173.029.538.538,0,0,0,.278-.078.538.538,0,0,0,.251-.378l1.963-13.206A.536.536,0,0,0-318.14-5.263Z" transform="translate(335.048 5.363)" fill="#fff"></path>
                                    </svg>
                                    <%= HudhudResources.Resources.Screen_documentInfo_Send %>
                                    
                                </div>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="white-holder tab-pane" id="edit-signature">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <asp:Panel ID="Panel1" runat="server" CssClass="modal-body">
                <table class="table my-table">
                    <thead>
                        <tr>
                            <th>
                                <%= HudhudResources.Resources.Screen_documentInfo_Name %>
                                <span class="btn-sort-table">
                                <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                    <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                        <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                            <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5">
                                                </path>
                                            </g>
                                        </g>
                                        <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                            <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                        </g>
                                    </g>
                                </svg>
                            </span>
                            </th>
                            <th>
                                <%= HudhudResources.Resources.Screen_documentInfo_SignDate %>
                                <span class="btn-sort-table">
                                <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                    <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                        <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                            <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5">
                                                </path>
                                            </g>
                                        </g>
                                        <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                            <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                        </g>
                                    </g>
                                </svg>
                            </span>
                            </th>
                            <th>
                                <%= HudhudResources.Resources.Screen_documentInfo_SignedAttachment %>
                                <span class="btn-sort-table">
                                <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                    <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                        <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                            <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5">
                                                </path>
                                            </g>
                                        </g>
                                        <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                            <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                        </g>
                                    </g>
                                </svg>
                            </span>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView runat="server" ID="lstAllSign">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: right"><%#Eval( "fullName")%></td>
                                    <td style="text-align: right"><%#Eval( "Date")%></td>
                                    <td style="text-align: right"><%#Eval( "DocumentFileName")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </tbody>
                </table>
            </asp:Panel>
            <div class="modal-footer">
                <div class="start-side">
                    <asp:LinkButton ID="lnkRefreshSign" CausesValidation="false" runat="server" OnClick="lnkRefreshSign_Click"  CssClass="btn-main">
                        <%= HudhudResources.Resources.Screen_documentInfo_Refresh %>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row" style="text-align: center;" runat="server" id="tblSigntureEmpty" visible="false">
                <%-- <asp:Label ID="lblNoResult" runat="server" Text="Label" Visible="false"></asp:Label>--%>
                <label>
                    <%= HudhudResources.Resources.Screen_documentInfo_noSignatures %>
            </div>

            
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="white-holder tab-pane" id="edit-transfer">
            <%--<div class="max-width-holder">--%>
            <div class="row">
                <div class="col-lg-6">
                    <div class="main-field-holder">
                            <label class="main-label">
                                <%= HudhudResources.Resources.Screen_documentInfo_ToEmail %>
                            </label>
                            <asp:TextBox ID="txtToEmails" CssClass="main-input" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valdReqToEmail" ValidationGroup="transfer" ControlToValidate="txtToEmails" 
                            runat="server" ErrorMessage="Please enter recipient email"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valdRegToEmail" ValidationGroup="transfer" ControlToValidate="txtToEmails" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" ErrorMessage="Wrong email format"></asp:RegularExpressionValidator>
                        </div>
                        <div class="main-field-holder">
                            <label class="main-label">
                                <%= HudhudResources.Resources.Screen_documentInfo_Subject %>
                            </label>
                            <asp:TextBox ID="txtSubject" CssClass="main-input" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="transfer"
                                ControlToValidate="txtSubject" runat="server" ErrorMessage="Please enter subject"></asp:RequiredFieldValidator>
                        </div>
                        <div class="main-field-holder">
                            <label class="main-label">
                                <%= HudhudResources.Resources.Screen_documentInfo_Attachments %>
                            </label>
                            <asp:CheckBoxList ID="chkAttachments" runat="server"></asp:CheckBoxList>
                        </div>
                    </div>
                <div class="col-lg-6">
                        <div class="main-field-holder">
                            <label class="main-label">
                                <%= HudhudResources.Resources.Screen_documentInfo_Message %>
                            </label>
                            <asp:TextBox ID="txtBody" TextMode="MultiLine" Height="150px" Rows="5" CssClass="main-input" runat="server"></asp:TextBox>
                        </div>
                </div>
            </div>
                <%--</div>--%>
            <div class="control-side-holder control-side-holder-footer">
                <div class="start-side">
                    <asp:LinkButton ID="lnkSendMail" ValidationGroup="transfer" CssClass="btn-main" runat="server" OnClick="lnkSendMail_Click">
                        <div class="btn-main-wrapper">
                            <svg xmlns="http://www.w3.org/2000/svg" width="17.133" height="14.277" viewBox="0 0 17.133 14.277">
                                <path id="Path_7093" data-name="Path 7093" d="M-318.14-5.263A.536.536,0,0,0-318.7-5.3L-334.76,3.086a.537.537,0,0,0-.286.515.536.536,0,0,0,.36.466l4.465,1.526,9.509-8.131-7.358,8.865,7.483,2.558a.548.548,0,0,0,.173.029.538.538,0,0,0,.278-.078.538.538,0,0,0,.251-.378l1.963-13.206A.536.536,0,0,0-318.14-5.263Z" transform="translate(335.048 5.363)" fill="#fff"></path>
                            </svg>
                            <%= HudhudResources.Resources.Screen_documentInfo_Send %>
                        </div>
                    </asp:LinkButton>
                </div>

            </div>
        </div>
    </div>
    <!-- old ui -->
     <asp:HiddenField ID="hdnAddedUserID" runat="server" />
    <asp:HiddenField ID="hdnFolderSeq" runat="server" />
    <asp:HiddenField ID="hdnDocTypeSeq" runat="server" />
    <asp:HiddenField ID="hdnFolderDocTypeSeq" runat="server" />
    <asp:HiddenField ID="hdnOcrContent" runat="server" />
    <asp:HiddenField ID="hdnDocExt" runat="server" />
    <asp:HiddenField ID="hdnAddedDate" runat="server" />
    <asp:HiddenField ID="hdnDocTypID" runat="server" />

    <div id="signModal" style="" title="&#1578;&#1608;&#1602;&#1610;&#1593; &#1575;&#1604;&#1605;&#1604;&#1601;">
        <table class="w3-table w3-striped" id="tblSign">
            <%--<h1>hoy</h1>--%>
        </table>
    </div>
    <div class="popup-overlay">
        <!--Creates the popup content-->
        <div class="popup-content">
            <asp:HiddenField ID="hdnReminderMetaID" ClientIDMode="Static" runat="server" />
            <a href="" id="closeBtn" class="button"><i class="fas fa-times"></i></a>
            <h2>
                <%= HudhudResources.Resources.Screen_documentInfo_SetAReminder %>
            </h2>
            <p>
                <%= HudhudResources.Resources.Screen_documentInfo_RemindMeBefore %> :
                <asp:TextBox ID="txtReminderPeriod" Width="75px" TextMode="Number" runat="server" Text="1"></asp:TextBox>
                <%= HudhudResources.Resources.Screen_documentInfo_Days %>
            </p>
            <!--popup's close button-->
            <asp:LinkButton ID="lnkSaveRemider" OnClick="lnkSaveRemider_Click" runat="server" CssClass="button">
                <%= HudhudResources.Resources.Screen_documentInfo_AddReminder %>
            </asp:LinkButton>
        </div>
    </div>
    <input type="hidden" runat="server" class="hdnDynamicTabls" id="hdnDynamicTabls" value="" />
    <script>
        var openAttachTab = 0;
        $(function () {
            drawDynamicTablsValues();
            if (openAttachTab == 1) {
                $("a[href='#edit-attachments']").click();
            }
            $('.dateFeild ').datepicker({
                format: 'dd/mm/yyyy',
                startDate: '-3d'
            });

        })
        function openLastElement() {
            $(".lnkopenFile").last().click();
        }
        function downloadFile(xthis) {
            var uri = "http://" + document.location.host + "/";
            if (uri.indexOf('localhost') != -1) {
                uri = "http://localhost:15127/";
            }
            ///Uploads/10105-1.png
            uri = uri + "/Uploads/" + $(xthis).attr("data-id") + "-" + $(xthis).attr("data-verstion") + "." + $(xthis).attr("data-ext");
            window.location.assign(uri)
        }
        function MyFunction() {
            //alert('file uploaded');
            openAttachTab = 1;
            $("a[href='#edit-attachments']").click();
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
                html += "                                                            <span class=\"file-status\">" + lang == 'ar' ? "?????" : "Completed" + "<\/span>";
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
        //public static string DeleteFileVersion(int id, int version)
        function DeleteFileVersion(xthis) {
            var msg = lang == 'ar' ? '????? ??? ?????' : 'Confirm Delete';
            //if (confirm(msg)) {
            $("#divIframViewer").empty();
            //var msg = lang == 'ar' ? '????? ??? ' : 'Confirm Delete';
            bootbox.confirm({
                message: msg,
                locale: lang,
                callback: function (result) {
                    if (result) {
                        var id = $(xthis).attr("data-id");
                        var version = $(xthis).attr("data-version");
                        $.ajax({
                            type: "POST",
                            url: "/AjexServer/ajexresponse.aspx/DeleteFileVersion",
                            data: "{id:'" + id + "',version:'" + version + "'}",
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                var jsdata = JSON.parse(data.d);
                                if (jsdata != false) {
                                    $(".lifile[data-id='" + id + "'][data-version='" + version + "']").remove();
                                }
                                else {
                                    alert("??? ? ?????");
                                }
                            },
                            error: function (result) {
                                // alert("Error");
                            }
                        });
                    }
                }
            });

            // }
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
                if (currentAddList.indexOf(dataMetaId + ',') == -1) {
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
                            obj.value += "," + $(collection[j]).val();
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
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="outOfForm">
    <div id="pnlScanner" style="display: none; background-color: #ffffff; border: 3px ridge #293955; padding: 5px; position: absolute; top: 100px; left: 250px;">
        <img alt="close" id="imgClose" src="../Images/Icons/System-Close-icon.png"
            style="cursor: pointer" onclick="hideScanned()" />
        <br />

        <object classid="clsid:5220cb21-c88d-11cf-b347-00aa00a28331">
            <param name="LPKPath" value="../csximagetrial.lpk" />
        </object>

        <!-- A second object tag identifies the csXImage control itself and allows the size of the control as displayed in the browser to be set.  -->

        <object id="csxi" classid="clsid:d7ec6ec3-1cdf-11d7-8344-00c1261173f0" codebase="../csximagetrial.ocx" width="800" height="600"></object>
        <div id="dwtcontrolContainer"></div>

        <%--<object classid="clsid:5220cb21-c88d-11cf-b347-00aa00a28331"><param name="LPKPath" value="csximage.lpk" /></object>

<!-- A second object tag identifies the csXImage control itself and allows the size of the control as displayed in the browser to be set.  -->

<object id="csxi" classid="clsid:62e57fc5-1ccd-11d7-8344-00c1261173f0" codebase="../csximage.ocx" width="800" height="600"></object>--%>
    </div>
    <%--    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>--%>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../JS/docinfo.js"></script>
</asp:Content>
