<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DMS.screen._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script src="../JS/jquery.iconmenu.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var minCount = 0;
        var minis = new Array();
        function minimizeWindow(CODEN, PageName) {
            minis[minCount] = new Array();
            minis[minCount][0] = CODEN;
            minis[minCount][1] = PageName;
            $("#dialog_" + CODEN).fadeOut();
            minCount += 1;
            $("#minCount").html(minCount);
        }
        var showMinFlg = false;

        function minPnlClick() {
            if (showMinFlg == true) { hideMinis(); }
            else { showMinis(); }
        }

        function showMinis() {
            var dCount = 0;
            $("#miniCont").html('');
            for (var i = 0; i < minCount; i++) {
                var dlg = document.getElementById("min_" + String(i))
                if (dlg == null) {
                    var s = "<table dir='ltr' class='closedwindow' id='min_" + String(i) + "' style='color:black;width:100px;display:none; position:absolute;z-index:99;top:" + String(($(window).height()) - 130) + "px'><tr>"
                        + "<td align='center' onClick='hideMinis();showMinDialog(" + String(i) + "," + String(minis[i][0]) + ");'>"
                        + "<img border='0' id='ImageMin' style='width:48px' src='../Images/blue_Icons/window-icon.png'"
                        + " title='" + minis[i][1] + "' /><br /><b>" + minis[i][1] + "</b>"
                        + "</td>    </tr></table>";
                    $("#miniCont").append(s);

                    $("#min_" + String(i)).css('display', 'table');
                    $("#min_" + String(i)).animate({ opacity: 1, left: String((i + 2) * 80) + "px" }, 'slow');
                }
            }

            showMinFlg = true;
        }

        function showMinDialog(Index, CODEN) {
            $("#dialog_" + String(CODEN)).fadeIn();
            minis.splice(Index, 1);
            minCount -= 1;
            $("#minCount").html(minCount);
        }

        function hideMinis() {
            for (var i = 0; i < minCount; i++) {
                $("#min_" + String(i)).css('display', 'table');
                $("#min_" + String(i)).animate({ opacity: 0, left: "-70px" }, 'slow');
            }
            showMinFlg = false;
        }

        var dialogCount = 0;
        function showDialog(CODEN, Title, PageName, _Width, _Height) {
            if (_Width == NaN)
                _Width = 850;
            if (_Height == NaN)
                _Height = 500;

            dialogCount += 1;

            addTab(CODEN, Title, PageName);
        }

        function refreshDialog(CODEN, PageName) {
            //document.getElementById("frame_" + CODEN).src = "../OWL/" + PageName + ".aspx?CODEN=" + CODEN;
            var url = document.getElementById("frame_" + CODEN).contentWindow.location.href;
            //alert(url);
            document.getElementById("frame_" + CODEN).src = url;
        }

        function activeDialog(CODEN) {
            $(".ui-widget-content").zIndex(10);
            $(".ui-widget-content").css("border:solid 1px #cccccc");

            $("#dialog_" + CODEN).zIndex(11);
            $("#dialog_" + CODEN).css("border:solid 1px #000000");
        }

        function hideDialog(CODEN) {

            $("#dialog_" + CODEN).fadeOut();
        }

    </script>
    <script>
        function changetab(PreCoden, CurCoden, PreName, CurName, PreEnName, CurEnName) {
            //var test = document.getElementsByClassName(".page-tab-active .pagetab-name");
            var tabActive = document.getElementsByClassName("page-tab-active")[0];
            tabActive.innerHTML = tabActive.innerHTML.replaceAll("\"" + PreCoden + "\"", "\"" + CurCoden + "\"").replaceAll("tab-" + PreCoden, "tab-" + CurCoden).replaceAll(PreName, CurName).replaceAll(PreEnName, CurEnName);
            tabActive.setAttribute('data-id', CurCoden);
            tabActive.setAttribute('data-tab', 'tab-' + CurCoden);
            //alert(test.innerHTML);
            //var nodes = document.getElementsByClassName("page-tab-active");
            //alert(nodes.length);
            //for (var i = 0; i < nodes.length; i++) {
            //    var node = nodes[i].getElementsByClassName("pagetab-name");
            //    alert(node.innerHTML);
            //    for (var j = 0; j < node.length; i++) {
            //        node.innerHTML = "from only £00.00<br>";
            //    }
            //}
        }
    </script>
    <style>
        .iframeClass {
            border-top: solid 1px #aaaaaa;
            border-left: 0px;
            border-right: 0px;
            border-bottom: 0px;
        }
    </style>

    <script>
        var buffer = 20; //scroll bar buffer
        var iframe = document.getElementById('ifm');

        function pageY(elem) {
            return elem.offsetParent ? (elem.offsetTop + pageY(elem.offsetParent)) : elem.offsetTop;
        }

        function resizeIframe() {
            var height = document.documentElement.clientHeight;
            $(".iframeClass").height(height - 120);
            //            height -= pageY($("iframe")) + buffer;
            //            height = (height < 0) ? 0 : height;
            //            $("iframe").css("height", height + 'px');
        }

        // .onload doesn't work with IE8 and older.
        $(document).ready(function () {
            resizeIframe();
            $("iframe").ready(resizeIframe);

            window.onresize = resizeIframe;
        });
    </script>
    <script>
        var tabTitle = $("#tab_title"),
            tabContent = $("#tab_content"),
            tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close' role='presentation'>Remove Tab</span></li>",
            tabCounter = 2;

        var tabs;
        function SaveTab(CODEN, Title, PageName, isSide, indexId) {
            var value = sessionStorage.getItem("currentTabs"); //retrieve array
            var varTabs = [];
            if (value != null && value != "")
                varTabs = JSON.parse(value);
            var varTab = { CODEN: CODEN, Title: Title, PageName: PageName, isSide: isSide, indexId: indexId };
            if (!CheckTabAllreadySaved(varTabs, varTab))
                varTabs.push(varTab);
            sessionStorage.setItem("currentTabs", JSON.stringify(varTabs)); //store array
            console.log(JSON.parse(sessionStorage.getItem("currentTabs")));
        }
        function CheckTabAllreadySaved(varTabs, varTab) {
            for (var i = 0; i < varTabs.length; i++) {
                if (varTabs[i].CODEN == varTab.CODEN && varTabs[i].Title == varTab.Title)
                    return true;
            }
            return false;
        }
        function DeleteTab(CODEN, Title) {
            debugger;
            var value = sessionStorage.getItem("currentTabs"); //retrieve array
            var varTabs = [];
            if (value != null && value != "") {
                varTabs = JSON.parse(value);
                for (i = 0; i < varTabs.length; i++) {
                    if (varTabs[i].CODEN == CODEN && varTabs[i].Title == Title) {
                        varTabs.splice(i, 1);
                    }
                }
                sessionStorage.setItem("currentTabs", JSON.stringify(varTabs)); //store array
            }
        }
        // addTab form: calls addTab function on submit and closes the dialog
        function addTab(CODEN, Title, PageName, isSide, indexId) {
            debugger;
            SaveTab(CODEN, Title, PageName, isSide, 0);
            $(".menu-div").removeClass("menu-active");
            $(".menu-div[data-id='" + CODEN + "']").addClass("menu-active");
            if (indexId == null)
                indexId = 0;
            // first add tab on menuv
            $("#sortableTabs").find("li").removeClass('page-tab-active');
            $("#tabs").find(".tab-content").hide();
            var tabName = "tab-" + CODEN;
            var existTabIndex = checkLiRedundancy(CODEN, Title);
            if (existTabIndex == 0) {
                var liHtml = "";
                liHtml += "<li  class=\"item swiper-slide swiper-no-swiping ui-state-default swiper-slide-next page-tab-active\"  data-id='" + CODEN + "' data-title='" + Title + "' data-index='" + indexId + "' data-tab='" + tabName + "'>";
                liHtml += "                        <div class=\"item-pagetab-wrapper\" >";
                liHtml += "                            <div class=\"btn-close-pagetabs\" data-id='" + CODEN + "' data-index='" + indexId + "' data-tab='" + tabName + "' data-title='" + Title + "'  onclick='closeThisTab(this);'>";
                liHtml += "                                <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\" viewBox=\"0 0 11.963 11.963\">";
                liHtml += "                                    <g id=\"Group_21\" data-name=\"Group 21\" transform=\"translate(5.981 -3.153) rotate(45)\">";
                liHtml += "                                        <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\" transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                liHtml += "                                        <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\" transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                liHtml += "                                    <\/g>";
                liHtml += "                                <\/svg>";
                liHtml += "                            <\/div>";
                liHtml += "                            <span class=\"pagetab-name\" data-id='" + CODEN + "' data-index='" + indexId + "' data-tab='" + tabName + "' onclick='callThisTab(this);'> " + Title + "<\/span>";
                liHtml += "                            <div class=\"btn-duplicate-pagetabs\" data-id='" + CODEN + "' data-index='" + indexId + "' data-tab='" + tabName + "' data-title='" + Title + "' data-page='" + PageName + "' onclick='duplicateThisTab(this);' style='background: #007aff; color: #FFF;'>";
                //liHtml += "                                <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\" viewBox=\"0 0 11.963 11.963\">";
                //liHtml += "                                    <g id=\"Group_21\" data-name=\"Group 21\" transform=\"translate(5.981 -3.153) rotate(45)\">";
                //liHtml += "                                        <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\" transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                //liHtml += "                                        <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\" transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                //liHtml += "                                    <\/g>";
                //liHtml += "                                <\/svg>";
                liHtml += '<i class="fa fa-copy"></i>';
                liHtml += "                            <\/div>";
                liHtml += "                        <\/div>";
                liHtml += "";
                liHtml += "                    <\/li>";
                if (lang == 'ar') {
                    $("#sortableTabs").prepend(liHtml);
                }
                else {
                    $("#sortableTabs").append(liHtml);
                }
            }
            else {
                $("#sortableTabs").find("li[data-id='" + CODEN + "'][data-index='" + indexId + "']").addClass("page-tab-active");
            }
            //call active tab
            if (existTabIndex == 0 && PageName == "AddToDoList" && isSide != undefined) {
                var tabContentHtml = "";
                tabContentHtml += " <div id=\"tabs-" + CODEN + "\" data-index='" + indexId + "' class='tab-content' style=\"height: 550px;\">";
                tabContentHtml += "            <iframe id=\"ifm\" src='" + PageName + ".aspx?view=true&id=" + isSide + "' class=\"iframe-subpage\"><\/iframe>";
                tabContentHtml += "        <\/div>";
                $("#tabs").append(tabContentHtml);
                $("#tabs").find(".tab-content[id='tabs-" + CODEN + "'][data-index='" + indexId + "']").show();
            }
            else if (existTabIndex == 0) {
                var tabContentHtml = "";
                tabContentHtml += " <div id=\"tabs-" + CODEN + "\" data-index='" + indexId + "' class='tab-content' style=\"height: 550px;\">";
                tabContentHtml += "            <iframe id=\"ifm\" src='" + PageName + ".aspx?CODEN=" + CODEN + "&dlgid=" + String(tabCounter) + "&indexId=" + indexId + "' class=\"iframe-subpage\"><\/iframe>";
                tabContentHtml += "        <\/div>";
                $("#tabs").append(tabContentHtml);
                $("#tabs").find(".tab-content[id='tabs-" + CODEN + "'][data-index='" + indexId + "']").show();
            }
            else {
                $("#tabs").find(".tab-content[id='tabs-" + CODEN + "'][data-index='" + indexId + "']").show();
            }
            if ($("body").hasClass('aside-menu-open')) {
                $(".btn-collapse-aside-menu").click();
            }

            try {
                var swiper = new Swiper('.pages-tabs-holder', {
                    slidesPerView: 'auto',
                    slidesPerGroup: 1,
                    navigation: {
                        nextEl: '.pages-tabs-arrow.swiper-button-next',
                        prevEl: '.pages-tabs-arrow.swiper-button-prev',
                    },
                });
            } catch (e) {

            }
            try {
                var el = document.getElementById('sortableTabs');
                var sortable = Sortable.create(el, {
                    swapThreshold: 1,
                    animation: 150
                });
                //$('#sortableTabs').sortable({
                //    revert: true,
                //    axis: 'x'
                //});

                //var list = $('ul#sortableTabs');
                //var listItems = list.children('#sortableTabs>li');
                //list.append(listItems.get().reverse());

            } catch (e) {

            }
        }
        // actual addTab function: adds new tab using the input from the form above
        function checkLiRedundancy(id, Title) {
            //var countUp = 1;
            //var collection = $(".ui-tabs-nav").find("li");
            //for (var i = 0; i < collection.length; i++) {
            //    if ($(collection[i]).find("a").html().indexOf(title) != -1) {
            //        countUp++;
            //    }
            //}
            //return countUp;
            var lngth = $("#sortableTabs").find("li[data-id='" + id + "'][data-title='" + Title + "']").length;
            return lngth;
        }
        function getFirstLiRedundancy(title) {
            var countUp = 1;
            var collection = $(".ui-tabs-nav").find("li");
            for (var i = 0; i < collection.length; i++) {
                if ($(collection[i]).find("a").html().indexOf(title) != -1) {
                    return $(collection[i]).find("a").attr("href");
                    break;
                }
            }
            return "";
        }
        // addTab button: just opens the dialog
        //            $("#add_tab")
        //      .button()
        //      .click(function () {
        //          dialog.dialog("open");
        //      });

        // close icon: removing the tab on click
        $(document).ready(function () {
            var value = sessionStorage.getItem("currentTabs"); //retrieve array
            var varTabs = [];
            if (value != null && value != "") {
                varTabs = JSON.parse(value);
                for (i = 0; i < varTabs.length; i++) {
                    addTab(varTabs[i].CODEN, varTabs[i].Title, varTabs[i].PageName, varTabs[i].isSide, varTabs[i].indexId);
                }
            }
        });

        function callThisTab(xthis) {
            $("#sortableTabs").find("li").removeClass('page-tab-active');
            $("#tabs").find(".tab-content").hide();
            $("#sortableTabs").find("li[data-id='" + $(xthis).attr("data-id") + "'][data-index='" + $(xthis).attr("data-index") + "']").addClass("page-tab-active");
            $("#tabs").find(".tab-content[id='tabs-" + $(xthis).attr("data-id") + "'][data-index='" + $(xthis).attr("data-index") + "']").show();
            if ($("body").hasClass('aside-menu-open')) {
                $(".btn-collapse-aside-menu").click();
            }
            var swiper = new Swiper('.pages-tabs-holder', {
                slidesPerView: 'auto',
                slidesPerGroup: 1,
                navigation: {
                    nextEl: '.pages-tabs-arrow.swiper-button-next',
                    prevEl: '.pages-tabs-arrow.swiper-button-prev',
                },
            });
        }
        function closeThisTab(xthis) {
            debugger;
            var isLast = $("#sortableTabs").find("li:last").html().replace(/\s/g, "") == $(xthis).closest("li").html().replace(/\s/g, "") ? true : false;
            if (lang == 'en') {
                let li = xthis.closest('li'); // get reference
                let nodes = Array.from(xthis.closest('ul').children); // get array
                let index = nodes.indexOf(li) - 1;
                $("#sortableTabs").find("li[data-id='" + $(xthis).attr("data-id") + "'][data-index='" + $(xthis).attr("data-index") + "']").remove();
                $("#tabs").find(".tab-content[id='tabs-" + $(xthis).attr("data-id") + "'][data-index='" + $(xthis).attr("data-index") + "']").remove();
                //$(".aside-menu").find("ul").find("li:first").click();


                var prevoiostab = $("#sortableTabs").find(nodes[index]);
                //if (isLast) // this last item clicked
                callThisTab(prevoiostab);
            }
            else {
                let li = xthis.closest('li'); // get reference
                let nodes = Array.from(xthis.closest('ul').children); // get array
                let index = nodes.indexOf(li) + 1;
                $("#sortableTabs").find("li[data-id='" + $(xthis).attr("data-id") + "'][data-index='" + $(xthis).attr("data-index") + "']").remove();
                $("#tabs").find(".tab-content[id='tabs-" + $(xthis).attr("data-id") + "'][data-index='" + $(xthis).attr("data-index") + "']").remove();
                //$(".aside-menu").find("ul").find("li:first").click();


                var prevoiostab = $("#sortableTabs").find(nodes[index]);
                // if (isLast) // this last item clicked
                callThisTab(prevoiostab);
            }
            //let olddataId = $(xthis).attr("data-id");
            //$("#tabs").find("li[data-id='" + olddataId + "']").setAttribute('class', 'menu-div');;
            //let dataId = prevoiostab.attr("data-id");
            //$("#tabs").find("li[data-id='" + dataId + "']").setAttribute('class', 'menu-div menu-active');;
            DeleteTab($(xthis).attr("data-id"), $(xthis).attr("data-title"))
        }
        function closeAllTabs() {
            var collection = $("#sortableTabs").find("li");
            for (var i = 0; i < collection.length; i++) {
                if ($(collection[i]).attr("data-id") != 32) {
                    $("#sortableTabs").find("li[data-id='" + $(collection[i]).attr("data-id") + "'][data-index='" + $(collection[i]).attr("data-index") + "']").remove();
                    $("#tabs").find(".tab-content[id='tabs-" + $(collection[i]).attr("data-id") + "'][data-index='" + $(collection[i]).attr("data-index") + "']").remove();
                }
            }
            sessionStorage.setItem("currentTabs", ""); //empty array
            activeDashBoard();
            // var dashname = lang == 'ar' ? 'لوحة القيادة' : 'DashBoard';
            // addTab('32', dashname, 'Dashboard', 99);
            // addTab('32', dashname, 'Dashboard', 99)
            //refresh swipper
            var swiper = new Swiper('.pages-tabs-holder', {
                slidesPerView: 'auto',
                slidesPerGroup: 1,
                navigation: {
                    nextEl: '.pages-tabs-arrow.swiper-button-next',
                    prevEl: '.pages-tabs-arrow.swiper-button-prev',
                },
            });
        }
        function activeDashBoard() {
            $("#sortableTabs").find("li[data-id='32']").addClass('page-tab-active');
            $("#tabs").find(".tab-content[id='tabs-32'][data-index='0']").show();
        }
        function duplicateThisTab(xthis) {
            $("#sortableTabs").find("li").removeClass('page-tab-active');
            $("#tabs").find(".tab-content").hide();
            var id = $(xthis).attr("data-id");
            var title = $(xthis).attr("data-title");
            var existTabIndex = checkLiRedundancy(id, title);
            var PageName = $(xthis).attr("data-page");
            existTabIndex = existTabIndex + 1;
            title = title + " " + existTabIndex;
            var tabName = "tab-" + id;
            // add new tab
            SaveTab(id, title, PageName, "99", existTabIndex);
            var liHtml = "";
            liHtml += "<li  class=\"item swiper-slide swiper-no-swiping ui-state-default swiper-slide-next page-tab-active\"  data-id='" + id + "' data-title='" + title + "' data-index='" + existTabIndex + "' data-tab='" + tabName + "'>";
            liHtml += "                        <div class=\"item-pagetab-wrapper\">";
            liHtml += "                            <div class=\"btn-close-pagetabs\" data-id='" + id + "' data-index='" + existTabIndex + "' data-tab='" + tabName + "' data-title='" + title + "'  onclick='closeThisTab(this);'>";
            liHtml += "                                <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\" viewBox=\"0 0 11.963 11.963\">";
            liHtml += "                                    <g id=\"Group_21\" data-name=\"Group 21\" transform=\"translate(5.981 -3.153) rotate(45)\">";
            liHtml += "                                        <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\" transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
            liHtml += "                                        <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\" transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
            liHtml += "                                    <\/g>";
            liHtml += "                                <\/svg>";
            liHtml += "                            <\/div>";
            liHtml += "                            <span class=\"pagetab-name\" data-id='" + id + "' data-index='" + existTabIndex + "' data-tab='" + tabName + "' onclick='callThisTab(this);'> " + title + "<\/span>";
            //liHtml += "                            <div class=\"btn-duplicate-pagetabs\" data-id='" + id + "' data-index='" + existTabIndex + "' data-tab='" + tabName + "' data-title='" + title + "' data-page='" + PageName + "' >";
            //liHtml += "                                <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\" viewBox=\"0 0 11.963 11.963\">";
            //liHtml += "                                    <g id=\"Group_21\" data-name=\"Group 21\" transform=\"translate(5.981 -3.153) rotate(45)\">";
            //liHtml += "                                        <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\" transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
            //liHtml += "                                        <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\" transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
            //liHtml += "                                    <\/g>";
            //liHtml += "                                <\/svg>";
            //liHtml += "                            <\/div>";
            liHtml += "                        <\/div>";
            liHtml += "";
            liHtml += "                    <\/li>";
            if (lang == 'ar') {
                $("#sortableTabs").prepend(liHtml);
            }
            else {
                $("#sortableTabs").append(liHtml);
            }
            // add new tab content
            var tabContentHtml = "";
            tabContentHtml += " <div id=\"tabs-" + id + "\" data-index='" + existTabIndex + "' class='tab-content' style=\"height: 550px;\">";
            tabContentHtml += "            <iframe id=\"ifm\" src='" + PageName + ".aspx?CODEN=" + id + "&dlgid=" + String(existTabIndex) + "&indexId=' class=\"iframe-subpage\"><\/iframe>";
            tabContentHtml += "        <\/div>";
            $("#tabs").append(tabContentHtml);
            $("#tabs").find(".tab-content[id='tabs-" + id + "'][data-index='" + existTabIndex + "']").show();
            try {
                var el = document.getElementById('sortableTabs');
                var sortable = Sortable.create(el, {
                    swapThreshold: 1,
                    animation: 150
                });
                //$('#sortableTabs').sortable({
                //    revert: true,
                //    axis: 'x'
                //});

                //var list = $('ul#sortableTabs');
                //var listItems = list.children('#sortableTabs>li');
                //list.append(listItems.get().reverse());

            } catch (e) {

            }
            var swiper = new Swiper('.pages-tabs-holder', {
                slidesPerView: 'auto',
                slidesPerGroup: 1,
                navigation: {
                    nextEl: '.pages-tabs-arrow.swiper-button-next',
                    prevEl: '.pages-tabs-arrow.swiper-button-prev',
                },
            });
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="hTabs" type="hidden" name="hTabs" />
    <aside class="aside-menu">
        <ul>
            <asp:Repeater ID="rptMainIcons" runat="server">
                <ItemTemplate>
                    <li class="menu-div" data-id="<%# DataBinder.Eval(Container.DataItem, "programID") %>" onclick="addTab('<%# DataBinder.Eval(Container.DataItem, "programID") %>','<%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%>','<%# DataBinder.Eval(Container.DataItem, "programURL") %>',99)">
                        <a title="<%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%>">
                            <div class="menu-icon">
                                <%# DataBinder.Eval(Container.DataItem, "svg") %>
                                <%--<i class='fas fa-<%# DataBinder.Eval(Container.DataItem, "iconCss") %>'></i>--%>
                            </div>
                            <span class="menu-title"><%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%></span>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="btn-collapse-aside-menu">
            <svg xmlns="http://www.w3.org/2000/svg" width="8.231" height="15.279" viewBox="0 0 8.231 15.279">
                <g id="Group_2126" data-name="Group 2126" transform="translate(8.231 15.279) rotate(180)">
                    <g id="Group_2125" data-name="Group 2125" transform="translate(0 0)">
                        <path id="Path_6981" data-name="Path 6981"
                            d="M8.059,7.22,1,.165A.588.588,0,0,0,.172,1l6.64,6.64-6.64,6.64A.588.588,0,1,0,1,15.107L8.059,8.052A.588.588,0,0,0,8.059,7.22Z"
                            fill="#484848" />
                    </g>
                </g>
            </svg>
        </div>
    </aside>
    <footer>
        <%= (Session["lang"].ToString() == "0") ? "All rights reserved 2020" : "جميع الحقوق محفوظة 2020 لشركة"%> <span><%= (Session["lang"].ToString() == "0") ? "HudHud" : "هدهد"%> ©</span>
    </footer>

    <div id="tabs">
        <div id="tabs-32" data-index="0" class="tab-content" style="height: 550px;">
            <iframe id="ifm" src="../screen/Dashboard.aspx" class="iframe-subpage"></iframe>
        </div>
    </div>
    <div onclick="minPnlClick()" id="pnlMin" style="display: none; width: 150px; height: 150px; overflow: hidden; position: fixed; bottom: -30px; right: 30px; z-index: 99">
        <span id="minCount">0
        </span>
        <canvas id="myCanvas" width="100px" height="100px"></canvas>
    </div>
    <script>
        var canvas = document.getElementById('myCanvas');
        var context = canvas.getContext('2d');
        var centerX = canvas.width / 2;
        var centerY = canvas.height / 2;
        var redius = 50;
        context.beginPath();
        context.arc(centerX, centerY, redius, 0, 2 * Math.PI, false);
        //context.lineTo(centerX, centerY);
        context.lineWidth = 5;
        //context.fillStyle = '#8b7660';
        //var grd = context.createRadialGradient(centerX, centerY, 150, centerX, centerY, 10);
        // grd.addColorStop(0, "#8b7660");
        //grd.addColorStop(1, "#8b7660");
        //context.fillStyle = grd;
        context.fill();
        //        context.strokeStyle = '#cccccc';
        //        context.stroke();
    </script>
</asp:Content>
