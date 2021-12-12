<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.Master" AutoEventWireup="true" CodeBehind="workflowManage.aspx.cs" Inherits="dms.Admin.workflowManage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="../screen/subIcons.aspx?CODEN=2&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? "Settings" : "الاعدادات"%> </a></li>
        <li><a href="../admin/workflowManage.aspx?CODEN=17"><%= (Session["lang"].ToString() == "0") ? " Workflow" : "مسارات العمل "%></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .col-xs-3 {
            width: 100%;
        }

        .row tr td {
            width: 20%;
        }

        .expand-modal {
            display: none;
        }

        .modal-dialog {
            margin: 0px 0px !important;
        }

        .comment-item {
            border-style: none !important;
            border-color: #ffffff !important;
        }

        .modal-title {
            font-weight: bold;
            color: rgba(var(--main-color), 1);
            font-size: 15px;
            margin-inline-end: auto;
        }

        .btn-done-model {
            background: rgba(var(--main-color), 1);
            border: none;
            outline: none;
            color: #fff;
            margin-inline-end: auto;
            padding: 5px 30px;
            border-radius: 20px;
            float: right;
        }

        .btn-close-model {
            border: none;
            outline: none;
            background: transparent;
            display: flex;
            color: #7c7c7c;
            float: left;
        }

        .icon-close {
            background: #e9e9e9;
            width: 20px;
            display: inline-block;
            height: 20px;
            border-radius: 50px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 7px;
        }

        .modal-header {
            border-bottom: 0px solid #ffffff;
        }

        .modal-footer {
            border-top: 0px solid #ffffff;
        }

        .dropdown-main {
            height: 35px;
        }
    </style>
    <style type="text/css">
        .style1 {
            width: 32px;
            height: 32px;
        }

        .m-r-20 {
            margin-right: 20px;
        }

        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
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
            width: 600px;
            max-width: 800px;
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
    <script type="text/ecmascript">
        var lang =<%= (Session["lang"].ToString() == "0") ? "'en'" : "'ar'"%>;
        $(document).ready(function () {
            $(".timeline").sortable({
                placeholder: "timeline-placeholder",
                change: function (event, ui) {
                    //alert('sort change');
                    //var start_pos = ui.item.data('start_pos');
                    //var index = ui.placeholder.index();
                    //if (start_pos < index) {
                    //    $('#sortable li:nth-child(' + index + ')').addClass('highlights');
                    //} else {
                    //    $('#sortable li:eq(' + (index + 1) + ')').addClass('highlights');
                    //}
                },
                update: function (event, ui) {
                    var workList = [];
                    var collection = $(".timeline").find("li");
                    for (var i = 0; i < collection.length; i++) {
                        var work = { Id: $(collection[i]).data("id"), Index: $(collection[i]).data("seqno") };
                        workList.push(work);
                    }
                    //set new indexs
                    var collection = $(".timeline").find("li");
                    for (var i = 1; i <= collection.length; i++) {
                        $(collection[i]).attr("data-seqno", i);
                    }
                    $.ajax({
                        type: "POST",
                        url: "/AjexServer/ajexresponse.aspx/SaveWorkFlowPositions",
                        data: "{jsonData:'" + JSON.stringify(workList) + "'}",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            console.log(data);
                        },
                        error: function (result) {
                            console.log("error");
                        }
                    });
                }
            });
            $(".timeline").disableSelection();
        });
        function openWorkFlowModel() {
            $("#addPathlblText").html('إضافة مسارات العمل');
            //$("#ContentPlaceHolder1_ContentPlaceHolder1_divdrpBranchID").hide();
            //$("#ContentPlaceHolder1_ContentPlaceHolder1_divdrpCompanyID").hide();
            $("#ContentPlaceHolder1_ContentPlaceHolder1_hdnSeqNo").val(0);
            $("#ContentPlaceHolder1_ContentPlaceHolder1_txtSeqNo").val(0);
            $("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientType").val(1);
            $("#ContentPlaceHolder1_ContentPlaceHolder1_dropDurationType").val(1);
            $("#ContentPlaceHolder1_ContentPlaceHolder1_txtDuration").val(0);
            $("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientType").trigger('change');
            setTimeout(function () {
                if ($("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientType").val() == 1) {
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").hide();
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").hide();
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").val($("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID option:first").val());
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").val($("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID option:first").val());
                }
                $('#ContentPlaceHolder1_ContentPlaceHolder1_chkEndOfPath').prop('checked', false);
                $('#ContentPlaceHolder1_ContentPlaceHolder1_chkNewDet').prop('checked', true);
                MicroModal.init()
                MicroModal.show('modal-1');
            }, 1000);

        }
        function editWorkFlowModel(xthis) {
            $("#addPathlblText").html('تعديل مسار العمل');
            $("#ContentPlaceHolder1_ContentPlaceHolder1_hdnSeqNo").val($(xthis).attr("data-seqNo"));
            $("#ContentPlaceHolder1_ContentPlaceHolder1_txtSeqNo").val($(xthis).attr("data-seqNo"));
            $("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientType").val($(xthis).attr("data-recipientType"));
            $("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientType").trigger('change');
            setTimeout(function () {
                if ($("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientType").val() == 1) {
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").hide();
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").hide();
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").val($("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID option:first").val());
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").val($("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID option:first").val());
                }
                if ($(xthis).attr("data-endofpath") != "False") {
                    $('#ContentPlaceHolder1_ContentPlaceHolder1_chkEndOfPath').prop('checked', true);
                }
                else {
                    $('#ContentPlaceHolder1_ContentPlaceHolder1_chkEndOfPath').prop('checked', false);
                }
                $('#ContentPlaceHolder1_ContentPlaceHolder1_chkNewDet').prop('checked', false);
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientID").val($(xthis).attr("data-recipientID"));
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").val($(xthis).attr("data-branchID"));
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").val($(xthis).attr("data-companyID"));
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpApproveType").val($(xthis).attr("data-ApproveType"));
                $("#ContentPlaceHolder1_ContentPlaceHolder1_dropDurationType").val($(xthis).attr("data-DurationType"));
                var duration = $(xthis).attr("data-Duration");
                if ($(xthis).attr("data-DurationType") == 2) {
                    duration = Number(duration) / 24;
                }
                $("#ContentPlaceHolder1_ContentPlaceHolder1_txtDuration").val(duration);
                MicroModal.init()
                MicroModal.show('modal-1');
            }, 1000);
        }
        $(document).on("change", "#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientType", function (e) {
            if ($(this).val() == 1) {
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").hide();
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").hide();
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").val($("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID option:first").val());
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").val($("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID option:first").val());
            }
            else {
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpBranchID").show();
                $("#ContentPlaceHolder1_ContentPlaceHolder1_drpCompanyID").show();
            }
        });
        function CheckSelectUser() {
            //alrt('x');
            if ($("#ContentPlaceHolder1_ContentPlaceHolder1_drpRecipientID").val() > 0) {
                //check add duration
                if ($("#ContentPlaceHolder1_ContentPlaceHolder1_txtDuration").val() == "") {
                    $("#ContentPlaceHolder1_ContentPlaceHolder1_lblDurationx").html(lang == 'ar' ? 'يرجي ادخال مدة الاجراء' : "Please enter Duration");
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                $("#ContentPlaceHolder1_ContentPlaceHolder1_lblDet").html(lang == 'ar' ? 'يرجى اخيار المستلم' : "Please select user");
                return false;
            }
        }
    </script>

    <div id="divdlWorkFlows" runat="server" class="white-holder" style="padding-top: 15px;">
        <asp:DataList ID="dlWorkFlows" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
            Width="100%" CssClass="row"
            OnItemCommand="dlWorkFlows_ItemCommand">
            <ItemTemplate>
                <asp:LinkButton ID="lnkSelectPath" runat="server" CommandName="Select" CommandArgument='<%# Eval("pathId") %>' CausesValidation="false">
                    <div class="col-xs-3">
                        <div class="<%# int.Parse(Eval("pathId").ToString())==0? "select-setting-item-holder add-select-item-holder" : "select-setting-item-holder" %>">
                            <div class="select-setting-icon">
                                <span id="spanIcon" runat="server" visible='<%# int.Parse(Eval("pathId").ToString())==0? false : true %>'><%# GetFirstLastChar(((Session["lang"].ToString() == "0") ? Eval("pathDesc") : Eval("pathDescAr")).ToString()) %></span>
                                <svg style='display:<%# (int.Parse(Eval("pathId").ToString())==0? "inline" : "none")  %>' xmlns="http://www.w3.org/2000/svg" width="8" height="8" viewBox="0 0 8 8">
                                    <g id="Group_3023" data-name="Group 3023" transform="translate(-1664.841 -367.841)">
                                        <circle id="Ellipse_598" data-name="Ellipse 598" cx="4" cy="4" r="4" transform="translate(1664.841 367.841)" fill="#484848"></circle>
                                        <g id="Group_3020" data-name="Group 3020" transform="translate(1668.87 370.266) rotate(45)">
                                            <g id="Group_2166" data-name="Group 2166" transform="translate(0)">
                                                <line id="Line_28" data-name="Line 28" y2="3.59" transform="translate(2.539) rotate(45)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="0.5"></line>
                                                <line id="Line_29" class="make-plus" data-name="Line 29" x2="3.59" transform="rotate(45)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="0.5"></line>
                                            </g>
                                        </g>
                                    </g>
                                </svg>
                            </div>
                            <p class="select-setting-title"><%# (Session["lang"].ToString() == "0") ? Eval("pathDesc") : Eval("pathDescAr")%></p>
                        </div>
                    </div>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div id="divDetails" runat="server" class="white-holder">
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title">
                        <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add Workflow Path"
                            CssClass="formModeTitleCSS"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-xs-4">
            </div>
            <div class="col-xs-4">
                <div class="main-field-holder required_input">
                    <label class="main-label">
                        <asp:Label ID="lblPathDescAr" runat="server" Text="Path Description (Arabic)"></asp:Label></label>
                    <asp:TextBox ID="txtPathDescAr" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtPathDescAr"
                        ErrorMessage="Type Description is Required">*</asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="col-xs-4">
                <div class="main-field-holder">
                    <label class="main-label">
                        <asp:Label ID="lblPathDesc" runat="server" Text="Path Description (English)"></asp:Label></label>
                    <asp:TextBox ID="txtPathDesc" runat="server" CssClass="main-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtPathDesc"
                        ErrorMessage="Type Description is Required">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div>
                <div class="start-side">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-main">
                        <div class="btn-main-wrapper">
                            <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                            </svg>
                            <span id="lblSave1" runat="server">حفظ</span>
                        </div>
                    </asp:LinkButton>
                    <asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>

                    <a class="btn-main btn-white">
                        <div class="btn-main-wrapper" style="display: none;">
                            <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg" width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                <g id="Group_2175" data-name="Group 2175">
                                    <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244" r="11.244" fill="#f4f4f4"></circle>
                                    <g id="Group_2166" data-name="Group 2166" transform="translate(7.496 7.496)">
                                        <line id="Line_28" data-name="Line 28" y2="11.745" transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                        <line id="Line_29" data-name="Line 29" x2="11.745" transform="translate(0) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                    </g>
                                </g>
                            </svg>
                            <span id="lblRetreat1" runat="server">تراجع</span>
                        </div>
                    </a>
                </div>
                <div class="end-side">
                    <a class="btn-main" data-toggle="modal" data-target="#remove-confirm" style="display: none;">
                        <div class="btn-main-wrapper">
                            <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                            </svg>
                            <span id="lblSurvey1" runat="server">مسح</span>
                        </div>
                    </a>
                </div>
            </div>

        </div>
        <hr class="my-hr">
        <div id="divAddNewWFPath" runat="server" visible="false">
            <a class="btn-main" onclick='openWorkFlowModel(0); return false;'>
                <div class="btn-main-wrapper">
                    <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                        <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                    </svg>
                    <span id="lblAddNew" runat="server">إضافة خطوة</span>
                </div>
            </a>
        </div>
        <ul class="sort-path-holder ui-sortable">
            <asp:ListView ID="ListViewWorkFlow" runat="server">
                <ItemTemplate>
                    <li class="path-li" data-id="<%#Eval("id") %>" data-pathid="<%#Eval("pathID") %>" data-seqno="<%#Eval("seqNo") %>">
                        <div class="path-item">
                            <div class="path-control ">
                                <a class="path-edit" data-durationtype="<%#Eval("durationType") %>" data-duration="<%#Eval("duration") %>" data-id="<%#Eval("id") %>" data-recipienttype="<%#Eval("recipientType") %>" data-recipientid="<%#Eval("recipientID") %>" data-endofpath="<%#Eval("endOfPath") %>" data-approvetype="<%#Eval("ApproveType") %>" data-companyid="<%#Eval("companyID") %>" data-branchid="<%#Eval("branchID") %>" data-seqno="<%#Eval("SeqNo") %>" onclick="editWorkFlowModel(this); return false;">
                                    <svg id="Group_3228" data-name="Group 3228" xmlns="http://www.w3.org/2000/svg" width="16.997" height="16.997" viewBox="0 0 16.997 16.997">
                                        <g id="Group_1820" data-name="Group 1820" transform="translate(0)">
                                            <path id="Path_6947" data-name="Path 6947" d="M8.5,0A8.5,8.5,0,1,0,17,8.5,8.5,8.5,0,0,0,8.5,0Zm4.04,6.116-.807.807L10.089,5.28,9.465,5.9l1.643,1.643-4.03,4.03L5.435,9.933l-.623.623L6.455,12.2l-.4.4-.008-.008a.318.318,0,0,1-.2.146l-1.532.342a.319.319,0,0,1-.38-.38l.341-1.532a.319.319,0,0,1,.146-.2l-.008-.008L10.9,4.472a.244.244,0,0,1,.345,0l1.3,1.3A.244.244,0,0,1,12.539,6.116Z" transform="translate(-0.001)" fill="#8f9198"></path>
                                        </g>
                                    </svg>
                                </a>
                                <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("id")%>' CommandName="ThisBtnClick" OnClick="RowDeleting">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16.165" height="16.165" viewBox="0 0 16.165 16.165">
                                                <path id="Path_7155" data-name="Path 7155" d="M13.8,2.367A8.083,8.083,0,0,0,2.367,13.8,8.083,8.083,0,0,0,13.8,2.367Zm-2.219,8.095a.789.789,0,1,1-1.116,1.116L8.083,9.2,5.7,11.579a.789.789,0,1,1-1.116-1.116l2.38-2.38L4.587,5.7A.789.789,0,0,1,5.7,4.587l2.38,2.38,2.38-2.38A.789.789,0,1,1,11.579,5.7L9.2,8.083Z" fill="#8f9198"></path>
                                            </svg>
                                </asp:LinkButton>
                            </div>
                            <svg class="svg-user" id="Group_3249" data-name="Group 3249" xmlns="http://www.w3.org/2000/svg" width="29" height="29" viewBox="0 0 29 29">
                                <g id="Group_3248" data-name="Group 3248">
                                    <path id="Path_7158" data-name="Path 7158" d="M14.5,0A14.5,14.5,0,1,0,29,14.5,14.517,14.517,0,0,0,14.5,0Zm0,4.229a6.948,6.948,0,1,1-6.948,6.948A6.956,6.956,0,0,1,14.5,4.229Zm0,22.354a12.059,12.059,0,0,1-9.656-4.849,22.752,22.752,0,0,1,9.656-2.4,22.753,22.753,0,0,1,9.656,2.4A12.059,12.059,0,0,1,14.5,26.583Z"></path>
                                </g>
                            </svg>
                            <p class="user-name"><%# getdrpRecipientID(c.convertToString(Eval("recipientID")),c.convertToString(Eval("recipientType"))) %></p>
                            <p class="job-title"><%# getdrpRecipientType(c.convertToString(Eval("recipientType"))) %></p>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </ul>
    </div>
    <table style="width: 100%; display: none;" id="tblEditForm" border="0">
        <tr>
            <td colspan="4"
                style="font-size: 18px; color: #003366; background-color: #f6b727; height: 30px; padding-left: 10px;"></td>

        </tr>
        <tr style="display: none">
            <td>
                <asp:Label ID="lblPathId" runat="server" Text="Path ID"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtPathId" runat="server" Width="50px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="display: none">
                <asp:Label ID="lblFldrId" runat="server" Text="Folder"></asp:Label></td>
            <td style="display: none">
                <asp:DropDownList ID="drpFldrId" runat="server">
                </asp:DropDownList>
            </td>
            <td style="display: none">
                <asp:Label ID="lblDocTypId" runat="server" Text="Document Type"></asp:Label></td>
            <td style="display: none">
                <asp:DropDownList ID="drpDocTypId" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="display: none">
            <td></td>
            <td colspan="2">
                <asp:RadioButtonList ClientIDMode="Static" ID="rdoSaveMethod" runat="server"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">New Workflow</asp:ListItem>
                    <asp:ListItem Value="1">Exsit Workflow</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="3"></td>
        </tr>
    </table>
    <div id="tblDetailsForm" style="display: none" runat="server">
        <asp:Panel runat="server" ID="pnl1">
            <div class="modal micromodal-slide" id="modal-1" aria-hidden="true">
                <div class="modal__overlay" tabindex="-1" data-micromodal-close>
                    <div class="modal__container" role="dialog" aria-modal="true" aria-labelledby="modal-1-title">
                        <div class="modal-header">
                            <h4 class="modal-title" id="addPathlblText">إضافة مسارات العمل </h4>
                        </div>
                        <main class="" id="modal-1-content">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body modal-body-padding">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="main-field-holder">
                                                    <asp:Label ID="lblRecipientType" runat="server" Text="Recipient Type" CssClass="main-label"></asp:Label>
                                                    <asp:DropDownList ID="drpRecipientType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpRecipientType_SelectedIndexChanged" CssClass="dropdown-main dropdown">
                                                        <asp:ListItem Value="1">User</asp:ListItem>
                                                        <asp:ListItem Value="2">Group</asp:ListItem>
                                                        <asp:ListItem Value="3">Position</asp:ListItem>
                                                        <asp:ListItem Value="4">Unit</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="main-field-holder" runat="server" id="divdrpCompanyID" >
                                                    <asp:Label  runat="server" Text="Department" CssClass="main-label"><%= (Session["lang"].ToString() == "0") ? "Company" : "الشركة"%></asp:Label>
                                                    <asp:DropDownList ID="drpCompanyID" runat="server" CssClass="dropdown-main dropdown">
                                                    </asp:DropDownList>
                                                </div>
                                                 <div class="main-field-holder">
                                                    <asp:Label ID="lblApproveType" runat="server" Text="Approval Type" CssClass="main-label"></asp:Label>
                                                    <asp:DropDownList ID="drpApproveType" runat="server" CssClass="dropdown-main dropdown">
                                                        <asp:ListItem Value="1">All Must Approve</asp:ListItem>
                                                        <asp:ListItem Value="2">Voting</asp:ListItem>
                                                        <asp:ListItem Value="3">One Approve Only</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="main-field-holder">
                                                    <div class="radio-input-holder">
                                                        <asp:CheckBox ID="chkEndOfPath" runat="server" />
                                                        <asp:Label ID="lblEndOfPath" runat="server" Text="Decision-maker"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-xs-6">
                                                <div class="main-field-holder">
                                                    <asp:Label ID="lblRecipientID" runat="server" Text="Recipient" CssClass="main-label"></asp:Label>
                                                    <asp:DropDownList ID="drpRecipientID" runat="server" CssClass="dropdown-main dropdown">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblDet" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="main-field-holder" runat="server" id="divdrpBranchID" >
                                                    <asp:Label  runat="server" Text="Section" CssClass="main-label"><%= (Session["lang"].ToString() == "0") ? "Branch" : "القسم"%></asp:Label>
                                                    <asp:DropDownList ID="drpBranchID" runat="server" CssClass="dropdown-main dropdown">
                                                    </asp:DropDownList>
                                                </div>
                                               
                                                <div class="main-field-holder">
                                                    <asp:Label ID="lblDuration" runat="server" Text="Duration" CssClass="main-label"></asp:Label>
                                                    <asp:TextBox ID="txtDuration" CssClass="main-input" runat="server" Width="50px"></asp:TextBox>

                                                    <asp:DropDownList ID="dropDurationType" CssClass="dropdown-main dropdown" style="display:inline !important" runat="server" Width="70px">
                                                        <asp:ListItem Value="1">Hour</asp:ListItem>
                                                        <asp:ListItem Value="2">Day</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                        ControlToValidate="txtDuration" runat="server" ForeColor="Red"
                                                        ErrorMessage="ارقام فقط"
                                                        ValidationExpression="\d+">
                                                    </asp:RegularExpressionValidator>
                                                    <span id="ContentPlaceHolder1_ContentPlaceHolder1_lblDurationx" style="color: Red; display: block"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <table class="add-class" style="width: 400px; border-collapse: separate; border-spacing: 0 11px;" border="0" cellspacing="0">
                                        <tr style="display: none">
                                            <td style="background-color: #5C5C5C; color: #FFFFFF; font-weight: bold" width="0">
                                                <asp:Label ID="lblSeqNo" runat="server" Text="Seq No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSeqNo" runat="server" Width="30px"></asp:TextBox>
                                                <asp:HiddenField ID="hdnSeqNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr style="display: none !important;">
                                            <td style="background-color: #5C5C5C; color: #FFFFFF; font-weight: bold">
                                                <asp:Label ID="lblApproveType0" runat="server" Text="Actions"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkNewDet" runat="server" Checked="True" Text="New Path" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                        </main>
                        <div class="modal-footer">
                            <div class="start-side" style="float: right">
                                <a id="btnSave" runat="server" onclick="return CheckSelectUser();" onserverclick="LinkButton2_Click" class="btn-main">
                                    <div class="btn-main-wrapper">
                                        <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                            <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                            <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                            <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                            <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                            <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                                        </svg>
                                        <span id="lblSave2" runat="server">حفظ</span>
                                    </div>
                                </a>
                            </div>
                            <div class="end-side">
                                <a class="btn-main btn-white" data-dismiss="modal" onclick="hideMicroModal();">
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
                                        <span id="lblRetreat2" runat="server">تراجع</span>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none;">
                <asp:GridView ID="grdWFDet" runat="server" Width="100%"
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                    OnRowDeleting="grdWFDet_RowDeleting"
                    OnSelectedIndexChanged="grdWFDet_SelectedIndexChanged" ShowHeader="False">
                    <AlternatingRowStyle BackColor="Gainsboro" />
                    <Columns>
                        <asp:BoundField DataField="seqNo" HeaderText="seqNo">
                            <ItemStyle Width="8%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="recipientType">
                            <ItemTemplate>
                                <asp:Label ID="lblRcType" runat="server"
                                    Text='<%# getdrpRecipientType(c.convertToString(Eval("recipientType"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="recipientID">
                            <ItemTemplate>
                                <asp:Label ID="lblRcpID" runat="server"
                                    Text='<%# getdrpRecipientID(c.convertToString(Eval("recipientID")),c.convertToString(Eval("recipientType"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="companyID">
                            <ItemTemplate>
                                <asp:Label ID="lblCmpID" runat="server"
                                    Text='<%# getdrpCompanyID(c.convertToString(Eval("companyID"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="branchID">
                            <ItemTemplate>
                                <asp:Label ID="lblBrncID" runat="server"
                                    Text='<%# getdrpBranchID(c.convertToString(Eval("branchID"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:CheckBoxField HeaderText="endOfPath" DataField="endOfPath">
                            <ItemStyle Width="7%" />
                        </asp:CheckBoxField>
                        <asp:TemplateField HeaderText="approveType">
                            <ItemTemplate>
                                <asp:Label ID="lblApprvID" runat="server"
                                    Text='<%# getdrpApproveType(c.convertToString(Eval("ApproveType"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image"
                            SelectImageUrl="../Images/icons/file-edit-icon.png" />
                        <asp:CommandField DeleteText="Remove"
                            ButtonType="Image" DeleteImageUrl="../Images/Icons/Actions-stop-icon.png"
                            ShowDeleteButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#F6B727" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </div>
            <div>
                <%--      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>

                <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </asp:Panel>
    </div>
    <!-- Modal tr Remove-->
    <dialog id="tr-remove" style="border: none; padding: 0px 0px;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">هل أنت متأكد من الحذف ؟</h4>
                </div>
                <!-- <div class="modal-body">
                    </div> -->
                <div class="modal-footer">
                    <button type="button" class="btn-done-model" id="btnDeleteWFPath" runat="server" onserverclick="btnDeleteWFPath_ServerClick">نعم</button>
                    <button type="button" class="btn-close-model" onclick="document.getElementById('tr-remove').close();">
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
    </dialog>
    <input type="hidden" id="hwfID" runat="server" />
    <script>
        function showRemoveModal(Id) {
            document.getElementById("tr-remove").showModal();
        }
        function hideMicroModal() {
            $('#modal-1').removeClass('is-open');
            $(".loading").hide();
        }
    </script>
</asp:Content>
