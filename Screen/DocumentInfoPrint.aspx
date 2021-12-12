<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentInfoPrint.aspx.cs" Inherits="dms.Screen.DocumentInfoPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="shortcut icon" href="~/favicon.ico" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://code.jquery.com/ui/1.8.22/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="/Assets/UIKIT/css/bootstrap.css">
    <link rel="stylesheet" runat="server" id="linkBootstrap" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.8.18/themes/base/minified/jquery-ui.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Swiper/5.4.5/css/swiper.min.css" />
    <link rel="stylesheet" href="/Assets/UIKIT/css/main.min.css" />
    <link rel="stylesheet" href="/Assets/UIKIT/css/MainStyle-RTL.css" />
    <link rel="stylesheet" runat="server" id="linkLtr" />
    <style>
        :root {
            --main-color: 0, 114, 255;
        }

        a:hover, ae:focus {
            text-decoration: none !important;
        }

        .required_input label {
            color: #484848 !important;
        }

        .radio-input-holder label {
            margin-right: 5px;
            margin-left: 5px;
        }

        .white-holder {
            border-radius: 0px !important;
        }
    </style>

    <script>
        $(document).ready(function () {
            window.print();

        });
    </script>

      <style type="text/css">
          .my-table td {
            border:1px solid #989898;
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
</head>
<body class="body-subpage" style="height: 1000px; background: #FFF; direction:rtl">
    <form id="form1" runat="server">
       <table style="width:100%" cellpadding="3" cellspacing="0" dir="rtl">
        <tr>
            <td style="width:20%;">
                <%= (Session["lang"].ToString() == "0") ? "Document ID:" : "رقم الملف"%></td>
            <td style="width:30%;">

                <asp:TextBox ID="txtDocID"  runat="server"  ReadOnly="true"
                    TabIndex="2"></asp:TextBox>
            </td>
        
            <td style="width:20%">
                <asp:Label ID="lblDocName" runat="server" Text="Document Name"></asp:Label>
                </td>
            <td style="width:30%;">
                <asp:Label ID="txtDocName" runat="server"></asp:Label>
            </td>
        </tr>
      
    </table>
    
    <asp:Panel ID="pnlDocMetas" runat="server" CssClass="tblMetas"></asp:Panel>
    </form>
</body>
</html>
