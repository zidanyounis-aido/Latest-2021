var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;
var sharedEventId = 0;
var sharedEventColor = "";


function updateEvent(event, element) {
    //alert(event.description);
    sharedEventId = event.id;
    sharedEventColor = event.backgroundColor;
    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;

    $('#updatedialog').dialog('open');
    $("#eventName").val(event.title);
    //alert(event.backgroundColor.replace("#",""));
    $("#txteditcolor").val(event.backgroundColor.replace("#", ""));
    $("#eventDesc").val(event.description);
    $("#eventId").val(event.id);
    $("#eventStart").text("" + event.start.toLocaleString());
    $('#txteventStart').datetime('destroy');
    $('#txteventEnd').datetime('destroy');
    $('#txteventStart').datetime({
        datetime: new Date(event.start),
    });
    if (event.end === null) {
        //$("#eventEnd").text("");
        $('#txteventStart').datetime();
    }
    else {
        $('#txteventEnd').datetime({
            datetime: event.end,
        });
        //$("#eventEnd").text("" + event.end.toLocaleString());
    }
    //"#f68b1e"
    if (sharedEventColor != '#1dd1a1') {
        $('.as-done').hide();
        $('.not-done').show();
        $('.ui-dialog-buttonpane button').button().show();
        if (Number($("#createdByHidden").val()) == 1 || (Number(currentUpdateEvent.CreatedBy) == Number($("#createdByHidden").val()))) {
            $('.ui-dialog-buttonpane button').button().show();
        }
        else {
            $('.ui-dialog-buttonpane button').button().hide();
        }
    }
    else {
        $('.as-done').show();
        $('.not-done').hide();
        $('.ui-dialog-buttonpane button').button().hide();
    }
    //jQuery('#datetimepicker').datetimepicker();

    //$("#datetimepicker").datepicker({ dateFormat: "dd/mm/yy" });
    return false;
}

function updateEventDateTime(start, end, id) {
    // alert(sharedEventId);
    //2018-05-01 03:00:00.000
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/updateEventDateTime",
        data: "{id:'" + id + "',start:'" + moment(start, moment.ISO_8601).format('YYYY-MM-DD HH:mm') + "',end:'" + moment(end, moment.ISO_8601).format('YYYY-MM-DD HH:mm') + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            $('#calendar').fullCalendar('refetchEvents');
            if (jsdata) {
                //$(".ui-icon-closethick").click()
                //$('#calendar').fullCalendar('rerenderEvents');
            }
            else {

            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function updateSuccess(updateResult) {
    //alert(updateResult);
}

function deleteSuccess(deleteResult) {
    //alert(deleteResult);
}

function addSuccess(addResult) {
    // if addresult is -1, means event was not added
    //    alert("added key: " + addResult);

    if (addResult != -1) {
        $('#calendar').fullCalendar('refetchEvents');
    }
}

function UpdateTimeSuccess(updateResult) {
    //alert(updateResult);
}

function selectDate(start, end, allDay) {
    //alert($('#addDialog').dialog('isOpen'));
    if ($('#addDialog').dialog('isOpen') == false && $('#updatedialog').dialog('isOpen') == false) {
        $('#addDialog').dialog('open');
        $("#addEventStartDate").text("" + start.toLocaleString());

        $("#addEventEndDate").text("" + end.toLocaleString());
        $('#txtaddeventStart').datetime('destroy');
        $('#txtaddeventEnd').datetime('destroy');
        $('#txtaddeventStart').datetime({
            datetime: start.toLocaleString(),
        });
        $('#txtaddeventEnd').datetime({
            datetime: end.toLocaleString(),
        });
        //$("#txtaddeventStart").val(start.toLocaleString());
        //$("#txtaddeventEnd").val(end.toLocaleString());
        addStartDate = start;
        addEndDate = end;
        globalAllDay = allDay;
    }

}

function updateEventOnDropResize(event, allDay) {

    //alert("allday: " + allDay);
    var eventToUpdate = {
        id: event.id,
        start: event.start
    };

    if (event.end === null) {
        eventToUpdate.end = eventToUpdate.start;
    }
    else {
        eventToUpdate.end = event.end;
    }

    var endDate;
    if (!event.allDay) {
        endDate = new Date(eventToUpdate.end + 60 * 60000);
        endDate = endDate.toJSON();
    }
    else {
        endDate = eventToUpdate.end.toJSON();
    }

    eventToUpdate.start = eventToUpdate.start.toJSON();
    eventToUpdate.end = eventToUpdate.end.toJSON(); //endDate;
    eventToUpdate.allDay = event.allDay;

    PageMethods.UpdateEventTime(eventToUpdate, UpdateTimeSuccess);
}

function eventDropped(event, dayDelta, minuteDelta, allDay, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function eventResized(event, dayDelta, minuteDelta, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function checkForSpecialChars(stringToCheck) {
    //var pattern = /[^A-Za-z0-9 ]/;
    //return pattern.test(stringToCheck); 
    return false;
}

function isAllDay(startDate, endDate) {
    var allDay;

    if (startDate.format("HH:mm:ss") == "00:00:00" && endDate.format("HH:mm:ss") == "00:00:00") {
        allDay = true;
        globalAllDay = true;
    }
    else {
        allDay = false;
        globalAllDay = false;
    }

    return allDay;
}

function qTipText(start, end, description) {
    var text;

    if (end !== null)
        text = "<strong>من:</strong> " + start.format("MM/DD/YYYY hh:mm T") + "<br/><strong>الى:</strong> " + end.format("MM/DD/YYYY hh:mm T") + "<br/><br/>" + description;
    else
        text = "<strong>من:</strong> " + start.format("MM/DD/YYYY hh:mm T") + "<br/><strong>الى:</strong><br/><br/>" + description;

    return text;
}

$(document).ready(function () {
    $.getScript("../Scripts/jscolor.js", function () {
   
    });

    PageMethods.set_path("EventsList.aspx");

    // update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "تعديل": function () {
                //alert(currentUpdateEvent.title);
                va
                var eventToUpdate = {
                    id: currentUpdateEvent.id,
                    title: $("#eventName").val(),
                    description: $("#eventDesc").val(),
                    backgroundColor: $("#txteditcolor").val()
                };
                if (checkForSpecialChars(eventToUpdate.title) || checkForSpecialChars(eventToUpdate.description)) {
                    alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                }
                else {
                    PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
                    $(this).dialog("close");
                    currentUpdateEvent.title = $("#eventName").val();
                    currentUpdateEvent.description = $("#eventDesc").val();
                    currentUpdateEvent.start = $("#txteventStart").datetime('getTime');
                    currentUpdateEvent.end = $("#txteventEnd").datetime('getTime');
                    currentUpdateEvent.backgroundColor = $("#txteditcolor").val();
                    currentUpdateEvent.borderColor = $("#txteditcolor").val();
                    updateEventDateTime(currentUpdateEvent.start, currentUpdateEvent.end, currentUpdateEvent.id);
                    $('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
                }

            },
            "حذف": function () {

                if (confirm("هل انت متاكد من حذف الموعد؟")) {
                    PageMethods.deleteEvent($("#eventId").val(), deleteSuccess);
                    $(this).dialog("close");
                    $('#calendar').fullCalendar('removeEvents', $("#eventId").val());
                }
            }
        }
    });

    //add dialog
    $('#addDialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "اضافة": function () {
                var std = new Date($("#txtaddeventStart").val() + " " + $("#txtaddeventStartTime").val());
                var endd = new Date($("#txtaddeventEnd").val() + " " + $("#txtaddeventEndTime").val() );
                var tod = new Date();
                if (std >= tod) {
                    if (endd >= std) {
                        var eventToAdd = {
                            title: $("#addEventName").val(),
                            description: $("#addEventDesc").val(),
                            start: std.toJSON(),
                            end: endd.toJSON(),
                            allDay: isAllDay(std, endd),
                            CreatedBy: $('#createdByHidden').val(),
                            backgroundColor: $('#txtaddcolor').val()
                        };

                        if (checkForSpecialChars(eventToAdd.title) || checkForSpecialChars(eventToAdd.description)) {
                            alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                        }
                        else {
                            //alert("sending " + eventToAdd.title);

                            PageMethods.addEvent(eventToAdd, addSuccess);
                            $(this).dialog("close");
                        }
                    }
                    else {
                        alert("تاريخ نهاية الحدث اقل من تاريخ بدء الحدث");
                    }
                }
                else {
                   //var cip = new cip();
                    alert("تاريخ الحدث لا يمكن ان يكون اقل من اليوم");
                }

            }
        }
    });

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var options = {
        weekday: "long", year: "numeric", month: "short",
        day: "numeric", hour: "2-digit", minute: "2-digit"
    };

    $('#calendar').fullCalendar({
        eventLimit: true,
        theme: true,
        lang: 'ar-sa',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        defaultView: 'month',
        eventClick: updateEvent,
        selectable: true,
        // timeFormat: 'h:mm',
        views: {
            month: { // name of view
                titleFormat: 'MMMM / YYYY'
                // other view-specific options here
            },
            agendaDay: { // name of view
                titleFormat: 'DD  MMMM / M / YYYY',
                timeFormat: 'h:mm',
                // other view-specific options here
            },
            agendaWeek: { // name of view
                titleFormat: 'd  MMMM / M / YYYY',
                timeFormat: 'h:mm',
                // other view-specific options here
            },

        },
        slotLabelFormat: [
  'MMMM YYYY', // top level of text
  'HH:mm'        // lower level of text
        ],
        selectHelper: true,
        select: selectDate,
        editable: true,
        events: "JsonResponse.ashx",
        eventDrop: eventDropped,
        eventResize: eventResized,
        eventRender: function (event, element) {
            //alert(event.title);
            element.qtip({
                content: {
                    text: qTipText(event.start, event.end, event.description),
                    title: '<strong>' + event.title + '</strong>'
                },
                position: {
                    my: 'bottom left',
                    at: 'top right'
                },
                style: { classes: 'qtip-shadow qtip-rounded' }
            });
            $(".fc-time-grid-container").css('height', '400px');
        }
    });


    //ar
    var el1 = $($('.ui-button .ui-icon')[0]);
    var el2 = $($('.ui-button .ui-icon')[1]);

    if (el1.attr('class') == "ui-icon ui-icon-circle-triangle-w") {
        el1.removeClass('ui-icon ui-icon-circle-triangle-w');
        el1.addClass('ui-icon ui-icon-circle-triangle-e');
    } else {
        el1.removeClass('ui-icon ui-icon-circle-triangle-e');
        el1.addClass('ui-icon ui-icon-circle-triangle-w');
    }


    if (el2.attr('class') == "ui-icon ui-icon-circle-triangle-w") {
        el2.removeClass('ui-icon ui-icon-circle-triangle-w');
        el2.addClass('ui-icon ui-icon-circle-triangle-e');
    } else {
        el2.removeClass('ui-icon ui-icon-circle-triangle-e');
        el2.addClass('ui-icon ui-icon-circle-triangle-w');
    }

});
$(document).on('click', '.fc-agendaDay-button', function (event) {
    // alert('day click');
    $(".fc-time-grid-container").css('height', '400px');
})
$(document).on('click', '.fc-agendaWeek-button', function (event) {
    // alert('day click');
    $(".fc-time-grid-container").css('height', '400px');
})
/*
   $('#calendar').fullCalendar({
        theme: true,
        lang: 'ar-sa',
        header: {
            left: 'prev,next today customBtn',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        customButtons: {
            customBtn: {
                text: 'Custom Button',
                click: function() {
                    alert('This custom button is hot! 🔥\nNow go have fun!');
                }
            }
        },
        defaultView: 'agendaWeek',
        eventClick: updateEvent,
        selectable: true,
        selectHelper: true,
        select: selectDate,
        editable: true,
        events: "JsonResponse.ashx",
        eventDrop: eventDropped,
        eventResize: eventResized,
        eventRender: function(event, element) {
            //alert(event.title);
            element.qtip({
                content: {
                    text: qTipText(event.start, event.end, event.description),
                    title: '<strong>' + event.title + '</strong>'
                },
                position: {
                    my: 'bottom left',
                    at: 'top right'
                },
                style: { classes: 'qtip-shadow qtip-rounded' }
            });
        }
    });

*/