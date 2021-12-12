jQuery(document).ready(function ($) {
    if (window.history && window.history.pushState) {
        window.history.pushState(null, "", window.location.href);
        $(window).on('popstate', function () {
            window.history.pushState(null, "", window.location.href);
        });
    }
});
$(document).on("click", ".menuDiv", function (e) {
    $(".menuDiv").removeClass("menu-active");
    $(this).addClass("menu-active");
});
$(document).on("click", "#btncloseall", function (e) {
    e.preventDefault();
    $(".menuDiv").removeClass("menu-active");
    $(".ui-tabs-nav").find("li").remove();
    $("#tabs").find("div").remove();
});

$(document).on("mouseenter", ".menuDiv", function (e) {
    e.preventDefault();
    minmaxbyid2(1);
    $('.iframeClass').iframeTracker({
        blurCallback: function () {
            minmaxbyid(0);
        }
    });
});

$(document).on("click", "#tabs", function (e) {
    e.preventDefault();
});

$(document).on("mouseleave", "#iconscontent", function (e) {
    e.preventDefault();
    minmaxbyid(0);
    $('.iframeClass').iframeTracker({
        blurCallback: function () {
            minmaxbyid(0);
        }
    });
});
setInterval(function () {
    CheckNewTasks();
}, 10000);

var lastodoid = 0;
function CheckNewTasks() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/CheckNewTasks",
        data: "{}",
        dataType: "json",
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (lastodoid == 0) {
                lastodoid = jsdata.Id;
            }
            else if (jsdata.Id > lastodoid) {
                lastodoid = jsdata.Id;
                var str = "لديك مهمة جديدة" + " " + jsdata.Name;
                $.toast({
                    heading: 'Information',
                    text: str,
                    icon: 'info',
                    loader: true,        // Change it to false to disable loader
                    loaderBg: '#9EC600'  // To change the background
                })
            }
            else {
                //alert("خطا ف الحذف");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
//$(document).on("mouseout", "#iconscontent", function (e) {
//    e.preventDefault();
//    minmaxbyid(0);
//});
//$(document).on("click", "#iconscontent", function (e) {
//    e.preventDefault();
//   // animCalled = false;
//    minmaxbyid(0);
//    //$(this).off('hover');
//});