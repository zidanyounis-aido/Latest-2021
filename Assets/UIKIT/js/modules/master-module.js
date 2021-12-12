//import { off } from "cldrjs";

//page load
$(function () {
    loadNotification();
    loadInbox();
})
function loadNotification() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/GetNewNotifications",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var html = "";
            var dayName = lang == 'en' ? 'day' : 'يوم';
            for (var i = 0; i < jsdata.length; i++) {
                //html += "                                                <div class=\"avatar\" data-id='" + jsdata[i].Id + "' style='overflow: inherit!important'>";
                //html += "                                                    <img src=\"/Assets/UIKIT/img/bell.png\" class=\"bg-cover \" >";
                //html += "                                                <\/div>";
                //html += "                                                <p class=\"msg\">" + jsdata[i].Name;
                //html += "                                                <\/p>";
                //html += "                                                <p class=\"info\">";
                //html += "                                                    <span class=\"group\">";
                //html += "                                                        <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\"";
                //html += "                                                            id=\"Group_1787\" data-name=\"Group 1787\" width=\"27.34\" height=\"22.534\"";
                //html += "                                                            viewBox=\"0 0 27.34 22.534\">";
                //html += "                                                            <path id=\"Path_6934\" data-name=\"Path 6934\"";
                //html += "                                                                d=\"M255.352,77.4h11.835a.41.41,0,0,0,.41-.41V75.41a.41.41,0,0,0-.41-.41H254.222a.41.41,0,0,0-.334.649l1.13,1.582A.411.411,0,0,0,255.352,77.4Z\"";
                //html += "                                                                transform=\"translate(-240.258 -73.398)\" fill=\"#484848\">";
                //html += "                                                            <\/path>";
                //html += "                                                            <path id=\"Path_6935\" data-name=\"Path 6935\"";
                //html += "                                                                d=\"M8.909,45H2.225A2.225,2.225,0,0,0,0,47.225V65.31a2.225,2.225,0,0,0,2.225,2.225H25.116A2.225,2.225,0,0,0,27.34,65.31V52.832a2.225,2.225,0,0,0-2.225-2.225H15.2a2.225,2.225,0,0,1-1.81-.932l-2.674-3.744A2.225,2.225,0,0,0,8.909,45Z\"";
                //html += "                                                                transform=\"translate(0 -45)\" fill=\"#484848\">";
                //html += "                                                            <\/path>";
                //html += "                                                        <\/svg><\/span><span class=\"time\"><svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\"";
                //html += "                                                            id=\"Group_2143\" data-name=\"Group 2143\" width=\"19\" height=\"19\"";
                //html += "                                                            viewBox=\"0 0 19 19\">";
                //html += "                                                            <path id=\"Path_6985\" data-name=\"Path 6985\"";
                //html += "                                                                d=\"M9.5,0A9.5,9.5,0,1,0,19,9.5,9.511,9.511,0,0,0,9.5,0Zm0,17.812A8.312,8.312,0,1,1,17.812,9.5,8.322,8.322,0,0,1,9.5,17.812Z\"";
                //html += "                                                                fill=\"#484848\">";
                //html += "                                                            <\/path>";
                //html += "                                                            <path id=\"Path_6986\" data-name=\"Path 6986\"";
                //html += "                                                                d=\"M241.187,96H240v6.183l3.736,3.736.84-.84-3.388-3.388Z\"";
                //html += "                                                                transform=\"translate(-231.094 -92.438)\" fill=\"#484848\">";
                //html += "                                                            <\/path>";
                //html += "                                                        <\/svg>";
                //html += "                                                            " + jsdata[i].beforedays + " " + dayName +" <\/span>";
                //html += "                                                <\/p>";
                //html += "                                            <\/div>";
                //var inboxText = lang == 'ar' ? 'البريد' : "Inbox";
                html += "    <div class=\"nav-dropholder-item\" data-id='" + jsdata[i].Id + "'  >";
                html += "                                        <div class=\"avatar\">";
                //html += "                                            <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                html += "                                                    <img src=\"/Assets/UIKIT/img/bell.png\" class=\"bg-cover \" >";
                html += "                                        <\/div>";
                html += "                                        <p class=\"msg\">";
                // html += "    <span class=\"user-name\">زيدان يونس<\/span>";
                html += "                                       " + jsdata[i].Name + "";
                html += "                                         ";
                html += "                                             <span class=\"remove-item\"";
                html += "                     data-id='" + jsdata[i].ID + "'         onclick='removeNotifyItem(this,event);'                       \"><svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\"";
                html += "                                                    viewBox=\"0 0 11.963 11.963\">";
                html += "                                                    <g id=\"Group_21\" data-name=\"Group 21\"";
                html += "                                                        transform=\"translate(5.981 -3.153) rotate(45)\">";
                html += "                                                        <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\"";
                html += "                                                            transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\"";
                html += "                                                            stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                html += "                                                        <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\"";
                html += "                                                            transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\"";
                html += "                                                            stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                html += "                                                    <\/g>";
                html += "                                                <\/svg><\/span><\/p>";
                html += "                                        <p class=\"info\"><span class=\"time\"><svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\"";
                html += "                                                    id=\"Group_2143\" data-name=\"Group 2143\" width=\"19\" height=\"19\"";
                html += "                                                    viewBox=\"0 0 19 19\">";
                html += "                                                    <path id=\"Path_6985\" data-name=\"Path 6985\"";
                html += "                                                        d=\"M9.5,0A9.5,9.5,0,1,0,19,9.5,9.511,9.511,0,0,0,9.5,0Zm0,17.812A8.312,8.312,0,1,1,17.812,9.5,8.322,8.322,0,0,1,9.5,17.812Z\"";
                html += "                                                        fill=\"#484848\"><\/path>";
                html += "                                                    <path id=\"Path_6986\" data-name=\"Path 6986\"";
                html += "                                                        d=\"M241.187,96H240v6.183l3.736,3.736.84-.84-3.388-3.388Z\"";
                html += "                                                        transform=\"translate(-231.094 -92.438)\" fill=\"#484848\"><\/path>";
                html += "                                                <\/svg> " + jsdata[i].beforedays + " " + dayName + "<\/span><\/p>";
                html += "                                    <\/div>";


            }
            if (jsdata.length == 0) {
                if (lang == 'ar') {
                    html += "                                                <div class=\"avatar\" >";
                    html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                    html += "                                                <\/div>";
                    html += "                                                <p class=\"msg\" style='margin: 20px;'> لا توجد اشعارات";

                    html += "                                                <\/p>";
                    html += "                                            <\/div>";
                }
                else {
                    html += "                                                <div class=\"avatar\" >";
                    html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                    html += "                                                <\/div>";
                    html += "                                                <p class=\"msg\" style='margin: 20px;'> There are no notifications";

                    html += "                                                <\/p>";
                    html += "                                            <\/div>";
                }
            }
            $("#div-notification-header").html(html);

        },
        error: function (result) {
            // alert("Error");
        }
    });
}

function loadInbox() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/GetNewInbox",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var html = "";
            for (var i = 0; i < jsdata.length; i++) {
                //var html = "";
                //html += "       <div class=\"nav-dropholder-item\" >";
                //html += "                                        <div class=\"avatar\">";
                //html += "                                            <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                //html += "                                        <\/div>";
                //html += "                                        <p class=\"msg\"><span class=\"user-name\">زيدان يونس<\/span>هذا النص مثال لى نص هذا";
                //html += "                                            النص مثال لى نص هذا النص مثال لى نص هذا النص مثال لى نص هذا النص مثال لى نص";
                //html += "                                            هذا النص مثال لى نص هذا النص مثال لى نص هذا النص مثال لى نص هذا النص مثال لى";
                //html += "                                            نص <span class=\"remove-item";
                //html += "                                            \"><svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\"";
                //html += "                                                    viewBox=\"0 0 11.963 11.963\">";
                //html += "                                                    <g id=\"Group_21\" data-name=\"Group 21\"";
                //html += "                                                        transform=\"translate(5.981 -3.153) rotate(45)\">";
                //html += "                                                        <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\"";
                //html += "                                                            transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\"";
                //html += "                                                            stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                //html += "                                                        <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\"";
                //html += "                                                            transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\"";
                //html += "                                                            stroke-linecap=\"round\" stroke-width=\"2\"><\/line>";
                //html += "                                                    <\/g>";
                //html += "                                                <\/svg><\/span><\/p>";
                //html += "                                        <p class=\"info\"><span class=\"time\"><svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\"";
                //html += "                                                    id=\"Group_2143\" data-name=\"Group 2143\" width=\"19\" height=\"19\"";
                //html += "                                                    viewBox=\"0 0 19 19\">";
                //html += "                                                    <path id=\"Path_6985\" data-name=\"Path 6985\"";
                //html += "                                                        d=\"M9.5,0A9.5,9.5,0,1,0,19,9.5,9.511,9.511,0,0,0,9.5,0Zm0,17.812A8.312,8.312,0,1,1,17.812,9.5,8.322,8.322,0,0,1,9.5,17.812Z\"";
                //html += "                                                        fill=\"#484848\"><\/path>";
                //html += "                                                    <path id=\"Path_6986\" data-name=\"Path 6986\"";
                //html += "                                                        d=\"M241.187,96H240v6.183l3.736,3.736.84-.84-3.388-3.388Z\"";
                //html += "                                                        transform=\"translate(-231.094 -92.438)\" fill=\"#484848\"><\/path>";
                //html += "                                                <\/svg> 8س<\/span><\/p>";
                //html += "                                    <\/div>";
                // var date = new Date(jsdata[i].receiveDate);
                //var str = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
                var inboxText = lang == 'ar' ? 'البريد' : "Inbox";
                html += "       <div class=\"nav-dropholder-item\" data-id='" + jsdata[i].ID + "'  >";
                html += "                                                <div class=\"avatar\" data-id='" + jsdata[i].ID + "'  onclick='openInboxByPass(this);'  style='overflow: inherit!important'>";
                html += "                                                    <img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkHK5WEBJN-5DSCywBzxWnchQSNbJ_lVhoKA&usqp=CAU\' class=\"bg-cover \" >";
                html += "                                                <\/div>";
                if (lang == 'ar') {
                    html += "                                                <p class=\"msg\" data-id='" + jsdata[i].ID + "'  onclick='openInboxByPass(this);'><span class='user-name'>" + jsdata[i].docTypDescAr + "</span>" + jsdata[i].docName;
                }
                else {
                    html += "                                                <p class=\"msg\" data-id='" + jsdata[i].ID + "'  onclick='openInboxByPass(this);'><span class='user-name'>" + jsdata[i].docTypDesc + "</span>" + jsdata[i].docName;
                }
                html += "                                                    <span class=\"remove-item\"";
                html += "               data-id='" + jsdata[i].ID + "'         onclick='removeInboxItem(this,event);'                    \">";
                html += "                                                <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"11.963\" height=\"11.963\"";
                html += "                                                    viewBox=\"0 0 11.963 11.963\">";
                html += "                                                    <g id=\"Group_21\" data-name=\"Group 21\"";
                html += "                                                        transform=\"translate(5.981 -3.153) rotate(45)\">";
                html += "                                                        <line id=\"Line_28\" data-name=\"Line 28\" y2=\"12.918\"";
                html += "                                                            transform=\"translate(6.459)\" fill=\"none\" stroke=\"#fff\"";
                html += "                                                            stroke-linecap=\"round\" stroke-width=\"2\">";
                html += "                                                        <\/line>";
                html += "                                                        <line id=\"Line_29\" data-name=\"Line 29\" x2=\"12.918\"";
                html += "                                                            transform=\"translate(0 6.459)\" fill=\"none\" stroke=\"#fff\"";
                html += "                                                            stroke-linecap=\"round\" stroke-width=\"2\">";
                html += "                                                        <\/line>";
                html += "                                                    <\/g>";
                html += "                                                <\/svg><\/span>";
                html += "                                                <\/p>";
                html += "                                                <p class=\"info\">";
                html += "                                                    <span class=\"group\">";
                html += "                                                        <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\"";
                html += "                                                            id=\"Group_1787\" data-name=\"Group 1787\" width=\"27.34\" height=\"22.534\"";
                html += "                                                            viewBox=\"0 0 27.34 22.534\">";
                html += "                                                            <path id=\"Path_6934\" data-name=\"Path 6934\"";
                html += "                                                                d=\"M255.352,77.4h11.835a.41.41,0,0,0,.41-.41V75.41a.41.41,0,0,0-.41-.41H254.222a.41.41,0,0,0-.334.649l1.13,1.582A.411.411,0,0,0,255.352,77.4Z\"";
                html += "                                                                transform=\"translate(-240.258 -73.398)\" fill=\"#484848\">";
                html += "                                                            <\/path>";
                html += "                                                            <path id=\"Path_6935\" data-name=\"Path 6935\"";
                html += "                                                                d=\"M8.909,45H2.225A2.225,2.225,0,0,0,0,47.225V65.31a2.225,2.225,0,0,0,2.225,2.225H25.116A2.225,2.225,0,0,0,27.34,65.31V52.832a2.225,2.225,0,0,0-2.225-2.225H15.2a2.225,2.225,0,0,1-1.81-.932l-2.674-3.744A2.225,2.225,0,0,0,8.909,45Z\"";
                html += "                                                                transform=\"translate(0 -45)\" fill=\"#484848\">";
                html += "                                                            <\/path>";
                html += "                                                        <\/svg><\/span><span class=\"time\"><svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\"";
                html += "                                                            id=\"Group_2143\" data-name=\"Group 2143\" width=\"19\" height=\"19\"";
                html += "                                                            viewBox=\"0 0 19 19\">";
                html += "                                                            <path id=\"Path_6985\" data-name=\"Path 6985\"";
                html += "                                                                d=\"M9.5,0A9.5,9.5,0,1,0,19,9.5,9.511,9.511,0,0,0,9.5,0Zm0,17.812A8.312,8.312,0,1,1,17.812,9.5,8.322,8.322,0,0,1,9.5,17.812Z\"";
                html += "                                                                fill=\"#484848\">";
                html += "                                                            <\/path>";
                html += "                                                            <path id=\"Path_6986\" data-name=\"Path 6986\"";
                html += "                                                                d=\"M241.187,96H240v6.183l3.736,3.736.84-.84-3.388-3.388Z\"";
                html += "                                                                transform=\"translate(-231.094 -92.438)\" fill=\"#484848\">";
                html += "                                                            <\/path>";
                html += "                                                        <\/svg>";
                html += "                                                            " + jsdata[i].receiveDateStr + "<\/span>";
                html += "                                                <\/p>";
                html += "                                            <\/div>";

            }
            if (jsdata.length == 0) {
                if (lang == 'ar') {
                    html += "                                                <div class=\"avatar\" >";
                    html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                    html += "                                                <\/div>";
                    html += "                                                <p class=\"msg\" style='margin: 20px;'> لا يوجد بريد";

                    html += "                                                <\/p>";
                    html += "                                            <\/div>";
                }
                else {
                    html += "                                                <div class=\"avatar\" >";
                    html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                    html += "                                                <\/div>";
                    html += "                                                <p class=\"msg\" style='margin: 20px;'> There are no Inbox";

                    html += "                                                <\/p>";
                    html += "                                            <\/div>";
                }
            }
            $("#div-inbox-header").html(html);
            if ($("#div-inbox-header").find(".nav-dropholder-item").length > 0) {
                $(".icon-mail").addClass('notif-active');
            }
            else {
                $(".icon-mail").removeClass('notif-active');
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function showSearchDlg() {
    var saerchStr = lang == 'ar' ? 'بحث' : "Search";
    addTab($("#txtSearch").val(), saerchStr, 'Search');
}
function GetAllSearch() {
    if ($("#txtSearch").val() != "") {
        $(".search-block2").html('');
        $(".search-empty").hide();
        $(".search-block1").hide();
        $(".search-block2").hide();
        $(".search-loader").show();
        $.ajax({
            type: "POST",
            url: "/AjexServer/ajexresponse.aspx/GetAllSearch",
            data: "{str:'" + $("#txtSearch").val() + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $(".search-loader").hide();
                var jsdata = JSON.parse(data.d);
                if (jsdata.EvenetsList.length == 0 && jsdata.DocumentsList.length == 0 && jsdata.TasksList.length == 0) {
                    $(".search-empty").show();
                    $(".search-block1").hide();
                    $(".search-block2").hide();
                    return;
                }
                $(".search-block1").show();
                $(".search-block2").show();
                //get all files
                var html = "";
                for (var i = 0; i < jsdata.EvenetsList.length; i++) {
                    html += " <li data-name='" + jsdata.EvenetsList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                    html += "                                        <p class=\"result-name\">" + jsdata.EvenetsList[i].Name + "<\/p>";
                    html += "                                        <div class=\"icon-search\" >";
                    html += "                                            <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"17.293\" height=\"17.293\"";
                    html += "                                                viewBox=\"0 0 17.293 17.293\">";
                    html += "                                                <g id=\"search\" transform=\"translate(0.5 0.5)\">";
                    html += "                                                    <circle id=\"Oval\" cx=\"7.149\" cy=\"7.149\" r=\"7.149\" fill=\"none\"";
                    html += "                                                        stroke=\"#b2b2b2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                    html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                    html += "                                                    <path id=\"Path\" d=\"M3.888,3.888,0,0\"";
                    html += "                                                        transform=\"translate(12.199 12.199)\" fill=\"none\" stroke=\"#b2b2b2\"";
                    html += "                                                        stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                    html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                    html += "                                                <\/g>";
                    html += "                                            <\/svg>";
                    html += "                                        <\/div>";
                    html += "                                    <\/li>";
                }
                for (var i = 0; i < jsdata.TasksList.length; i++) {
                    html += " <li data-name='" + jsdata.TasksList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                    html += "                                        <p class=\"result-name\">" + jsdata.TasksList[i].Name + "<\/p>";
                    html += "                                        <div class=\"icon-search\" >";
                    html += "                                            <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"17.293\" height=\"17.293\"";
                    html += "                                                viewBox=\"0 0 17.293 17.293\">";
                    html += "                                                <g id=\"search\" transform=\"translate(0.5 0.5)\">";
                    html += "                                                    <circle id=\"Oval\" cx=\"7.149\" cy=\"7.149\" r=\"7.149\" fill=\"none\"";
                    html += "                                                        stroke=\"#b2b2b2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                    html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                    html += "                                                    <path id=\"Path\" d=\"M3.888,3.888,0,0\"";
                    html += "                                                        transform=\"translate(12.199 12.199)\" fill=\"none\" stroke=\"#b2b2b2\"";
                    html += "                                                        stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                    html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                    html += "                                                <\/g>";
                    html += "                                            <\/svg>";
                    html += "                                        <\/div>";
                    html += "                                    <\/li>";
                }

                for (var i = 0; i < jsdata.DocumentsList.length; i++) {
                    html += " <li data-name='" + jsdata.DocumentsList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                    html += "                                        <p class=\"result-name\">" + jsdata.DocumentsList[i].Name + "<\/p>";
                    html += "                                        <div class=\"icon-search\" >";
                    html += "                                            <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"17.293\" height=\"17.293\"";
                    html += "                                                viewBox=\"0 0 17.293 17.293\">";
                    html += "                                                <g id=\"search\" transform=\"translate(0.5 0.5)\">";
                    html += "                                                    <circle id=\"Oval\" cx=\"7.149\" cy=\"7.149\" r=\"7.149\" fill=\"none\"";
                    html += "                                                        stroke=\"#b2b2b2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                    html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                    html += "                                                    <path id=\"Path\" d=\"M3.888,3.888,0,0\"";
                    html += "                                                        transform=\"translate(12.199 12.199)\" fill=\"none\" stroke=\"#b2b2b2\"";
                    html += "                                                        stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                    html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                    html += "                                                <\/g>";
                    html += "                                            <\/svg>";
                    html += "                                        <\/div>";
                    html += "                                    <\/li>";
                }
                $(".search-block1").find("ul").html(html);
                if (jsdata.EvenetsList.length > 0) {
                    var html = "";
                    html += "<div class=\"search-result-block-title\" onclick=\"callTap(31);\">";
                    html += "                                    <p class=\"title\">" + $('.menu-div[data-id="31"]').find('.menu-title').html() + "<\/p>";
                    html += "                                    <a class=\"view-all\" >عرض الكل<\/a>";
                    html += "                                <\/div>";
                    html += "<ul>";
                    for (var i = 0; i < jsdata.EvenetsList.length; i++) {
                        html += " <li data-name='" + jsdata.EvenetsList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                        html += "                                        <p class=\"result-name\">" + jsdata.EvenetsList[i].Name + "<\/p>";
                        html += "                                        <div class=\"icon-search\" data-name='" + jsdata.EvenetsList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                        html += "                                            <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"17.293\" height=\"17.293\"";
                        html += "                                                viewBox=\"0 0 17.293 17.293\">";
                        html += "                                                <g id=\"search\" transform=\"translate(0.5 0.5)\">";
                        html += "                                                    <circle id=\"Oval\" cx=\"7.149\" cy=\"7.149\" r=\"7.149\" fill=\"none\"";
                        html += "                                                        stroke=\"#b2b2b2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                        html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                        html += "                                                    <path id=\"Path\" d=\"M3.888,3.888,0,0\"";
                        html += "                                                        transform=\"translate(12.199 12.199)\" fill=\"none\" stroke=\"#b2b2b2\"";
                        html += "                                                        stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                        html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                        html += "                                                <\/g>";
                        html += "                                            <\/svg>";
                        html += "                                        <\/div>";
                        html += "                                    <\/li>";
                    }
                    html += "</ul>";
                    $(".search-block2").append(html);
                }

                if (jsdata.TasksList.length > 0) {
                    var html = "";
                    html += "<div class=\"search-result-block-title\" onclick=\"callTap(30);\">";
                    html += "                                    <p class=\"title\">" + $('.menu-div[data-id="30"]').find('.menu-title').html() + "<\/p>";
                    html += "                                    <a class=\"view-all\" >عرض الكل<\/a>";
                    html += "                                <\/div>";
                    html += "<ul>";
                    for (var i = 0; i < jsdata.TasksList.length; i++) {
                        html += " <li data-name='" + jsdata.TasksList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                        html += "                                        <p class=\"result-name\">" + jsdata.TasksList[i].Name + "<\/p>";
                        html += "                                        <div class=\"icon-search\" data-name='" + jsdata.TasksList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                        html += "                                            <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"17.293\" height=\"17.293\"";
                        html += "                                                viewBox=\"0 0 17.293 17.293\">";
                        html += "                                                <g id=\"search\" transform=\"translate(0.5 0.5)\">";
                        html += "                                                    <circle id=\"Oval\" cx=\"7.149\" cy=\"7.149\" r=\"7.149\" fill=\"none\"";
                        html += "                                                        stroke=\"#b2b2b2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                        html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                        html += "                                                    <path id=\"Path\" d=\"M3.888,3.888,0,0\"";
                        html += "                                                        transform=\"translate(12.199 12.199)\" fill=\"none\" stroke=\"#b2b2b2\"";
                        html += "                                                        stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                        html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                        html += "                                                <\/g>";
                        html += "                                            <\/svg>";
                        html += "                                        <\/div>";
                        html += "                                    <\/li>";
                    }
                    html += "</ul>";
                    $(".search-block2").append(html);
                }

                if (jsdata.DocumentsList.length > 0) {
                    var html = "";
                    html += "<div class=\"search-result-block-title\" onclick=\"callTap(1);\">";
                    html += "                                    <p class=\"title\">" + $('.menu-div[data-id="1"]').find('.menu-title').html() + "<\/p>";
                    html += "                                    <a class=\"view-all\" >عرض الكل<\/a>";
                    html += "                                <\/div>";
                    html += "<ul>";
                    for (var i = 0; i < jsdata.DocumentsList.length; i++) {
                        html += " <li data-name='" + jsdata.DocumentsList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                        html += "                                        <p class=\"result-name\">" + jsdata.DocumentsList[i].Name + "<\/p>";
                        html += "                                        <div class=\"icon-search\" data-name='" + jsdata.DocumentsList[i].Name + "' onclick='bindThisFileToSearch(this);'>";
                        html += "                                            <svg xmlns=\"http:\/\/www.w3.org\/2000\/svg\" width=\"17.293\" height=\"17.293\"";
                        html += "                                                viewBox=\"0 0 17.293 17.293\">";
                        html += "                                                <g id=\"search\" transform=\"translate(0.5 0.5)\">";
                        html += "                                                    <circle id=\"Oval\" cx=\"7.149\" cy=\"7.149\" r=\"7.149\" fill=\"none\"";
                        html += "                                                        stroke=\"#b2b2b2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                        html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                        html += "                                                    <path id=\"Path\" d=\"M3.888,3.888,0,0\"";
                        html += "                                                        transform=\"translate(12.199 12.199)\" fill=\"none\" stroke=\"#b2b2b2\"";
                        html += "                                                        stroke-linecap=\"round\" stroke-linejoin=\"round\"";
                        html += "                                                        stroke-miterlimit=\"10\" stroke-width=\"1\" \/>";
                        html += "                                                <\/g>";
                        html += "                                            <\/svg>";
                        html += "                                        <\/div>";
                        html += "                                    <\/li>";
                    }
                    html += "</ul>";
                    $(".search-block2").append(html);
                }
            },
            error: function (result) {
                $(".search-loader").hide();
                // alert("Error");
            }
        });
    }
    else {
        //$("")
    }
}
function callTap(tapId) {
    $('.menu-div[data-id="' + tapId + '"]').click();
}
function bindThisFileToSearch(xthis) {
    $("#txtSearch").val($(xthis).attr("data-name"));
    showSearchDlg();
}
function removeInboxItem(xthis, e) {
    e.stopPropagation();
    e.preventDefault();
    var id = $(xthis).attr("data-id");
    var msg = lang == 'ar' ? 'تاكيد حذف ' : 'Confirm Delete';

    bootbox.confirm({
        message: msg,
        locale: lang,
        callback: function (result) {
            if (result) {
                var id = $(xthis).attr("data-id");
                $.ajax({
                    type: "POST",
                    url: "/AjexServer/ajexresponse.aspx/RemoveInboxItem",
                    data: "{id:'" + id + "'}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                        if (jsdata != false) {
                            $("#div-inbox-header").find('.nav-dropholder-item[data-id="' + id + '"]').remove();
                            if ($("#div-inbox-header").find(".nav-dropholder-item").length > 0) {
                                $(".icon-mail").addClass('notif-active');
                            }
                            else {
                                $(".icon-mail").removeClass('notif-active');
                            }

                        }
                        else {
                            alert("خطا ف الحذف");
                        }
                    },
                    error: function (result) {
                    }
                });
            }
        }
    });
}

function removeNotifyItem(xthis, e) {
    e.stopPropagation();
    e.preventDefault();
    //var id = $(xthis).attr("data-id");
    var msg = lang == 'ar' ? 'تاكيد حذف ' : 'Confirm Delete';
    // if (confirm(msg)) {
    bootbox.confirm({
        message: msg,
        locale: lang,
        callback: function (result) {
            if (result) {
                var id = $(xthis).attr("data-id");
                $.ajax({
                    type: "POST",
                    url: "/AjexServer/ajexresponse.aspx/RemoveNotifyItem",
                    data: "{id:'" + id + "'}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                        if (jsdata != false) {
                            $("#div-notification-header").find('.nav-dropholder-item[data-id="' + id + '"]').remove();
                        }
                        else {
                            alert("خطا ف الحذف");
                        }
                    },
                    error: function (result) {
                    }
                });
            }
        }
    });

    // }
}

function removeInboxItemAll(xthis, e) {
    e.stopPropagation();
    e.preventDefault();
    //var id = $(xthis).attr("data-id");
    var msg = lang == 'ar' ? 'تاكيد حذف ' : 'Confirm Delete';
    bootbox.confirm({
        message: msg,
        locale: lang,
        callback: function (result) {
            if (result) {
                var id = -1;
                $.ajax({
                    type: "POST",
                    url: "/AjexServer/ajexresponse.aspx/RemoveInboxItem",
                    data: "{id:'" + id + "'}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                        var html = '';
                        if (jsdata != false) {
                            $("#div-inbox-header").find('.nav-dropholder-item').remove();
                            //if (jsdata.length == 0) {
                            if (lang == 'ar') {
                                html += "                                                <div class=\"avatar\" >";
                                html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                                html += "                                                <\/div>";
                                html += "                                                <p class=\"msg\" style='margin: 20px;'> لا يوجد بريد";

                                html += "                                                <\/p>";
                                html += "                                            <\/div>";
                            }
                            else {
                                html += "                                                <div class=\"avatar\" >";
                                html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                                html += "                                                <\/div>";
                                html += "                                                <p class=\"msg\" style='margin: 20px;'> There are no Inbox";

                                html += "                                                <\/p>";
                                html += "                                            <\/div>";
                            }
                            //}
                            $("#div-inbox-header").html(html);
                            //if (jsdata.length > 0) {
                            if ($("#div-inbox-header").find(".nav-dropholder-item").length > 0) {
                                $(".icon-mail").addClass('notif-active');
                            }
                            else {
                                $(".icon-mail").removeClass('notif-active');
                            }
                            // }
                        }
                        else {
                            alert("خطا ف الحذف");
                        }
                    },
                    error: function (result) {
                    }
                });
            }
        }
    });
    //if (confirm(msg)) {

    //}
}

function removeNotifyItemAll(xthis, e) {
    e.stopPropagation();
    e.preventDefault();
    //var id = $(xthis).attr("data-id");
    var msg = lang == 'ar' ? 'تاكيد حذف ' : 'Confirm Delete';
    bootbox.confirm({
        message: msg,
        locale: lang,
        callback: function (result) {
            if (result) {
                var id = -1;
                $.ajax({
                    type: "POST",
                    url: "/AjexServer/ajexresponse.aspx/RemoveNotifyItem",
                    data: "{id:'" + id + "'}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                        if (jsdata != false) {
                            $("#div-notification-header").find('.nav-dropholder-item').remove();
                            //  if (jsdata.length == 0) {
                            var html = '';
                            if (lang == 'ar') {
                                html += "                                                <div class=\"avatar\" >";
                                html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                                html += "                                                <\/div>";
                                html += "                                                <p class=\"msg\" style='margin: 20px;'> لا توجد اشعارات";

                                html += "                                                <\/p>";
                                html += "                                            <\/div>";
                            }
                            else {
                                html += "                                                <div class=\"avatar\" >";
                                html += "                                                    <img src=\"img\/icon-nav-user.png\" class=\"bg-cover \">";
                                html += "                                                <\/div>";
                                html += "                                                <p class=\"msg\" style='margin: 20px;'> There are no notifications";

                                html += "                                                <\/p>";
                                html += "                                            <\/div>";
                            }
                            //   }
                            $("#div-notification-header").html(html);
                        }
                        else {
                            alert("خطا ف الحذف");
                        }
                    },
                    error: function (result) {
                    }
                });
            }
        }
    });
}

function openInboxByPass(xthis) {
    var id = $(xthis).attr("data-id");
    var inboxText = lang == 'ar' ? 'البريد' : "Inbox";
    addTab('6', inboxText, 'myWorkflowDocs', 0, id);
}