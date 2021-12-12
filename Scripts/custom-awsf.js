/// <reference path="jquery.min.js" />
//AWSF.JS V 2.2
//...........
// change log

(function () {
    "use strict";
    window.AWSF = function (params) {
        var app = this;
        app.version = "2.0";

        app.params = {
            spinner: true,
            postBackPreloader: true,
            pTrigger: true,
            panelClose: true,
            dataMessage: true,
            postAutoPreloader: false,
            spinnerColor: "#A5A5A5",

        };

        for (var param in params) {
            app.params[param] = params[param];
        }



        app.init = function () {
            if (app.params.spinner) {
                if (!$("#AWSFspinner").length)
                    $("body").append("<div id=\"AWSFspinner\" class=\"spinner\" style=\"display: none\"><div class=\"rect1\"></div><div class=\"rect2\"></div><div class=\"rect3\"></div><div class=\"rect4\"></div><div class=\"rect5\"></div></div>");
                $(".spinner > div").css("background-color", app.params.spinnerColor);
            }

            if (app.params.pTrigger) {
                $("[data-pTrigger]").on("click", function () {
                    var $pSelector = $(this).attr("data-pTrigger");
                    window.location.href = $($pSelector).attr("href");
                });
            } else {
                $("[data-pTrigger]").on("click", function () { });
            }

            if (app.params.panelClose) {

                $("body").on("click", ".panel-heading > a.close", function () {
                    $(this).parent().parent().fadeOut();
                });
            }
            if (app.params.dataMessage) {
                $(".message").click(function () {
                    app.msgbox("رسالة", $(this).attr("data-message"));
                });
            }
            if (app.params.postBackPreloader) {
                $("input[onclick*='PostBack'], a[href*='PostBack']").click(function () {
                    app.showPreloader();
                });
            }
        };
        app.init();

        app.pTrigger = function ($pSelector) {
            window.location.href = $($pSelector).attr("href");
        };
        app.showPreloader = function () {
            $("#AWSFspinner").fadeIn();
        };
        app.hidePreloader = function () {
            $("#AWSFspinner").fadeOut();
        };
        app.scrollToElement = function (selector, subtract) {
            var time = 500;
            subtract = subtract || 60;
            var offset = $(selector).offset();
            $("html, body").animate({
                scrollTop: offset.top - subtract
            }, time);
        };
        app.confirm = function (message, title, okCallback, cancelCallback) {
            cancelCallback = cancelCallback || function () { };
            bootbox.dialog({
                message: message,
                title: title,
                buttons: {
                    success: {
                        label: "موافق",
                        className: "btn-success",
                        callback: function () {
                            okCallback();
                        }
                    },
                    main: {
                        label: "إلغاء",
                        className: "btn-default",
                        callback: function () {
                            cancelCallback();
                        }
                    }
                }
            });
        };
        app.sendPost = function (url, data, callbak, onerror, RequestType, Preloader) {
            RequestType = RequestType || "POST";
            Preloader = Preloader || false;
            if (Preloader)
                app.showPreloader();
            $.ajax({
                type: RequestType,
                url: url,
                contentType: "application/x-www-form-urlencoded;charset=UTF8",
                dataType: "html",
                data: data,
                success: function (data) {
                    callbak(data);
                    if (Preloader || app.params.postAutoPreloader)
                        app.hidePreloader();
                },
                error: function () {
                    onerror();
                    if (Preloader || app.params.postAutoPreloader)
                        app.hidePreloader();
                }
            });
        };
        app.msgbox = function (title, text) {
            bootbox.dialog({
                message: text,
                title: title,
                buttons: {
                    close: {
                        label: "اغلاق",
                        className: "btn-default"
                    }
                }
            });
        };
        app.getRandomStr = function (count) {
            var text = "";
            var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (var i = 0; i < count; i++)
                text += possible.charAt(Math.floor(Math.random() * possible.length));

            return text;
        };
        app.popup = function (link, width, height, left, top) {
            width = width || "1000";
            height = height || "600";
            left = left || "50";
            top = top || "50";
            window.open(link, "", "width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + ",resizable=no,menubar=no,location=no,scrollbars=yes");
        };
        app.validateInputs = function () {
            var len = arguments.length;
            if (len == 0) return true;
            //
            for (var i = 0; i < len; i++) {
                var currentDom = $(arguments[i]);
                if (currentDom.val() == null || currentDom.val() == "")
                    return false;
            }
            return true;
        };
    };
})();