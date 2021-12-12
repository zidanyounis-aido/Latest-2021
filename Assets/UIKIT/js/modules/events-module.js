function openAddEvent() {
    if (lang == 'ar') {
        $("#myModalLabel").html('إضافة حدث جديد');
    }
    else {
        $("#myModalLabel").html('Add event');
    }
    $(".btn-edit-calendar").hide();
    $(".btn-add-calendar").show();
    $('.btn-delete-calendar').hide();
    $('.for-todo').hide();
    $('.for-event').removeClass('disabled');
    $('.for-event').val('');
    sharedEventId = 0;
    $("#eventId").val(0);
    $("#add-event-modal").modal('show');
}
function markAsDone() {
    // alert(sharedEventId);
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/SetComplet",
        data: "{id:'" + sharedEventId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata) {
                //$(".ui-icon-closethick").click()
                try {
                    $("#add-event-modal").modal('hide');
                    //$('#calendar').fullCalendar('refetchEvents');
                    calendar.refetchEvents();

                } catch (e) {
                    alert(e.toSrting());
                }
                //$('#calendar').fullCalendar('rerenderEvents');
            }
            else {
                alert("error please try agan later");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function deleteEvent() {
    if (confirm(lang == 'ar' ? "هل انت متاكد من حذف الموعد؟" : "Sure you want to delete the appointment?")) {
        var id = $("#eventId").val();
        PageMethods.deleteEvent($("#eventId").val(), deleteSuccess);
        $(this).dialog("close");
        var evt = calendar.getEventSources();
        removeEvents.forEach(event => {
            if (event.id == id) {
                event.remove();
            }
        });
        //$('#calendar').fullCalendar('removeEvents', $("#eventId").val());

    }
}
function updateEvent(info) {
    debugger;
    var event = info.event;
    //alert(event.description);
    sharedEventId = event.id;
    sharedEventColor = event.backgroundColor;
    if ($(this).data("qtip")) $(this).qtip("destroy");
    currentUpdateEvent = event;
    var dateOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var stDate = event.start.toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    var edDate = event.end.toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    GetEventById(sharedEventId, stDate, edDate, event.start, event.end);
    //$("#addEventName").val(event.title);

    //var evtColor = event.backgroundColor;
    //if (event.backgroundColor == 'null') {
    //    evtColor = '#3788d8';
    //}
    //$("#txtaddcolor").val(evtColor);
    //$("#addEventDesc").val(event.description);


    //$("#eventId").val(event.id);
    //$("#txtaddeventStart").val(stDate);
    //$("#txtaddeventEnd").val(edDate);
    //$("#txtaddeventStartTime").val(event.start.toTimeString().split(' ')[0]);
    //$("#txtaddeventEndTime").val(event.end.toTimeString().split(' ')[0]);
    //$("#add-event-modal").modal('show');
    //$(".btn-edit-calendar").show();
    //$(".btn-add-calendar").hide();
    //$("#chkDone").prop('checked', false);
    //$("#chkDone").removeAttr('checked');
    //if (lang == 'ar') {
    //    $("#myModalLabel").html('تعديل حدث');
    //}
    //else {
    //    $("#myModalLabel").html('Edit event');
    //}
    //if (sharedEventColor != '#808e9b') {
    //    $('.for-todo').hide();
    //    $('.for-event').removeClass('disabled');
    //    $('.btn-edit-calendar').show();
    //    $('.btn-delete-calendar').show();
    //}
    //else {
    //    $('.for-todo').show();
    //    $('.for-event').addClass('disabled');
    //    $('.btn-edit-calendar').hide();
    //    $('.btn-delete-calendar').hide();
    //}
    return false;
}

function openUpdateEvent(info) {
    var event = info.event;
    //alert(event.description);
    sharedEventId = event.id;
    sharedEventColor = event.backgroundColor;
    if ($(this).data("qtip")) $(this).qtip("destroy");
    currentUpdateEvent = event;
    var dateOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var stDate = new Date(event.start).toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    var edDate = new Date(event.end).toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    //$('#updatedialog').dialog('open');
    $("#addEventName").val(event.title);
    //alert(event.backgroundColor.replace("#",""));
    var evtColor = event.backgroundColor;
    if (event.backgroundColor == 'null') {
        evtColor = '#3788d8';
    }
    $("#txtaddcolor").val(evtColor);
    $("#addEventDesc").html(event.description);
    //alert(event.description);
    $("#eventId").val(event.id);
    $("#txtaddeventStart").val(stDate);
    $("#txtaddeventEnd").val(edDate);
    $("#txtaddeventStartTime").val(new Date(event.start).toTimeString().split(' ')[0]);
    $("#txtaddeventEndTime").val(new Date(event.end).toTimeString().split(' ')[0]);
    $("#add-event-modal").modal('show');
    $(".btn-edit-calendar").show();
    $(".btn-add-calendar").hide();
    $("#chkDone").prop('checked', false);
    $("#chkDone").removeAttr('checked');
    if (lang == 'ar') {
        $("#myModalLabel").html('تعديل حدث');
    }
    else {
        $("#myModalLabel").html('Edit event');
    }
    //$('#txteventStart').datetime('destroy');
    //$('#txteventEnd').datetime('destroy');
    //$('#txteventStart').datetime({
    //    datetime: new Date(event.start),
    //});
    //if (event.end === null) {
    //    $('#txteventStart').datetime();
    //}
    //else {
    //    $('#txteventEnd').datetime({
    //        datetime: event.end,
    //    });
    //}
    //"#f68b1e"
    if (sharedEventColor != '#808e9b') {
        $('.for-todo').hide();
        $('.for-event').removeClass('disabled');
        $('.btn-edit-calendar').show();
        $('.btn-delete-calendar').show();
    }
    else {
        $('.for-todo').show();
        $('.for-event').addClass('disabled');
        $('.btn-edit-calendar').hide();
        $('.btn-delete-calendar').hide();
    }
    return false;
}
function checkForSpecialChars(stringToCheck) {
    //var pattern = /[^A-Za-z0-9 ]/;
    //return pattern.test(stringToCheck); 
    return false;
}

function selectDate(start, end, allDay) {
    //alert($('#addDialog').dialog('isOpen'));
    //alert('x');
    //if ($('#addDialog').dialog('isOpen') == false && $('#updatedialog').dialog('isOpen') == false) {
    //    $('#addDialog').dialog('open');
    //    $("#addEventStartDate").text("" + start.toLocaleString());
    //    $("#addEventEndDate").text("" + end.toLocaleString());
    //    $('#txtaddeventStart').datetime('destroy');
    //    $('#txtaddeventEnd').datetime('destroy');
    //    $('#txtaddeventStart').datetime({
    //        datetime: start.toLocaleString(),
    //    });
    //    $('#txtaddeventEnd').datetime({
    //        datetime: end.toLocaleString(),
    //    });
    //    //$("#txtaddeventStart").val(start.toLocaleString());
    //    //$("#txtaddeventEnd").val(end.toLocaleString());
    //    addStartDate = start;
    //    addEndDate = end;
    //    globalAllDay = allDay;
    //}
    // openAddEvent();
    if (lang == 'ar') {
        $("#myModalLabel").html('إضافة حدث جديد');
    }
    else {
        $("#myModalLabel").html('Add event');
    }
    $(".btn-edit-calendar").hide();
    $(".btn-add-calendar").show();
    $('.btn-delete-calendar').hide();
    $('.for-todo').hide();
    $('.for-event').removeClass('disabled');
    $('.for-event').val('');
    sharedEventId = 0;
    $("#eventId").val(0);
    $("#txtaddeventStart").val(formatDate(new Date(start.start)));
    $("#txtaddeventEnd").val(formatDate(new Date(start.end)));
    $("#txtaddeventStartTime").val(start.start.toLocaleTimeString('it-IT'));
    $("#txtaddeventEndTime").val(start.end.toLocaleTimeString('it-IT'));
    //$("#txtaddeventStartTime").val(formatDate(new Date(start.start).toLocaleTimeString('it-IT')));
    //$("#txtaddeventEndTime").val(formatDate(new Date(start.end).toLocaleTimeString('it-IT')));
    $("#add-event-modal").modal('show');
}
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();
    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;
    return [year, month, day].join('-');
}
//////// run charts
var calendar;
var currentUpdateEvent;
window.onload = function () {
    //$.ajax({
    //    type: "POST",
    //    url: "/AjexServer/ajexresponse.aspx/BindEvenets",
    //    data: "{}",
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        //alert(data.d);
    //        var jsdata = JSON.parse(data.d);
    //        var calendarEl = document.getElementById('calendar');
    //        var date = new Date();
    //        var d = date.getDate();
    //        if (d  < 10) {
    //            d = '0' + d;
    //        }
    //        var m = date.getMonth() + 1;
    //        if (m < 10) {
    //            m = '0' + m;
    //        }
    //        var y = date.getFullYear();
    //        var options = {
    //            weekday: "long", year: "numeric", month: "short",
    //            day: "numeric", hour: "2-digit", minute: "2-digit"
    //        };
    //        var cdate = y + '-' + m + '-' + d;
    //        var calendar = new FullCalendar.Calendar(calendarEl, {
    //            initialDate: cdate,
    //            locale: lang,
    //            initialView: 'dayGridMonth',
    //            headerToolbar: {
    //                left: 'prev,next today',
    //                center: 'title',
    //                right: 'dayGridMonth,timeGridWeek,timeGridDay'
    //            },
    //            height: 'auto',
    //            navLinks: true, // can click day/week names to navigate views
    //            editable: true,
    //            selectable: true,
    //            selectMirror: true,
    //            nowIndicator: true,
    //            datesSet: function (info) {
    //                alert(JSON.stringify(info));
    //            },
    //            events: "JsonResponse.ashx",
    //            eventAfterRender: function (event, element, view) {
    //                element.css('background-color', '#FFB347');
    //            },
    //        });
    //        calendar.render();
    //    },
    //    error: function (result) {
    //        // alert("Error");
    //    }
    //});
    var calendarEl = document.getElementById('calendar');
    var date = new Date();
    var d = date.getDate();
    if (d < 10) {
        d = '0' + d;
    }
    var m = date.getMonth() + 1;
    if (m < 10) {
        m = '0' + m;
    }
    var y = date.getFullYear();
    var options = {
        weekday: "long", year: "numeric", month: "short",
        day: "numeric", hour: "2-digit", minute: "2-digit"
    };
    var cdate = y + '-' + m + '-' + d;
    calendar = new FullCalendar.Calendar(calendarEl, {
        initialDate: cdate,
        locale: lang,
        initialView: 'dayGridMonth',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        height: 'auto',
        eventClick: updateEvent,
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        selectable: true,
        select: selectDate,
        selectMirror: true,
        nowIndicator: true,
        datesSet: function (info) {
            //debugger;
            //alert(JSON.stringify(info));
        },
        events: "JsonResponse.ashx",
        eventAfterRender: function (event, element, view) {
            //debugger;
            // element.css('background-color', '#FFB347');
        },
        eventRender: function (event, element) {
            console.log(event.name);
        }
    });
    calendar.render();
    if (getParameterByName('event_id') != null) {
        LoadEventById(getParameterByName('event_id'));
    }
    //var calendarEl = document.getElementById('calendar');

    //var calendar = new FullCalendar.Calendar(calendarEl, {
    //    initialDate: '2020-06-12',
    //    initialView: 'dayGridMonth',
    //    headerToolbar: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    //    },
    //    height: 'auto',
    //    navLinks: true, // can click day/week names to navigate views
    //    editable: true,
    //    selectable: true,
    //    selectMirror: true,
    //    nowIndicator: true,
    //    events: [
    //        {
    //            title: 'All Day Event',
    //            start: '2020-06-01',
    //            color:'#000'
    //        },
    //        {
    //            title: 'Long Event',
    //            start: '2020-06-07',
    //            end: '2020-06-10'
    //            ,color: '#000'
    //        },
    //        {
    //            groupId: 999,
    //            title: 'Repeating Event',
    //            start: '2020-06-09T16:00:00'
    //            , color: '#000'
    //        },
    //        {
    //            groupId: 999,
    //            title: 'Repeating Event',
    //            start: '2020-06-16T16:00:00'
    //             ,color: '#000'
    //        },
    //        {
    //            title: 'Conference',
    //            start: '2020-06-11',
    //            end: '2020-06-13'
    //            , color: '#000'
    //        },
    //        {
    //            title: 'Meeting',
    //            start: '2020-06-12T10:30:00',
    //            end: '2020-06-12T12:30:00'
    //            , color: '#000'
    //        },
    //        {
    //            title: 'Lunch',
    //            start: '2020-06-12T12:00:00'
    //        },
    //        {
    //            title: 'Meeting',
    //            start: '2020-06-12T14:30:00'
    //        },
    //        {
    //            title: 'Happy Hour',
    //            start: '2020-06-12T17:30:00'
    //        },
    //        {
    //            title: 'Dinner',
    //            start: '2020-06-12T20:00:00'
    //        },
    //        {
    //            title: 'Birthday Party',
    //            start: '2020-06-13T07:00:00'
    //        },
    //        {
    //            title: 'Click for Google',
    //            url: 'http://google.com/',
    //            start: '2020-06-28'
    //        }
    //    ]
    //});

    //calendar.render();
};
function LoadEventById(id) {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/LoadEventById",
        data: "{id:'" + id + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            //var html = "";
            var info = {};
            info.event = jsdata[0];
            // sharedEventId = id;
            openUpdateEvent(info);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function GetEventById(id, stDate ,edDate,start,end) {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/LoadEventById",
        data: "{id:'" + id + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            //var html = "";
            var info = {};
            info.event = jsdata[0];
            var event = info.event;
                //$("#addEventName").val(event.title);

    var evtColor = event.backgroundColor;
    if (event.backgroundColor == 'null') {
        evtColor = '#3788d8';
    }
    $("#txtaddcolor").val(evtColor);
    $("#addEventDesc").val(event.description);


    $("#eventId").val(event.id);
    $("#txtaddeventStart").val(stDate);
    $("#txtaddeventEnd").val(edDate);
    $("#txtaddeventStartTime").val(start.toTimeString().split(' ')[0]);
    $("#txtaddeventEndTime").val(end.toTimeString().split(' ')[0]);
    $("#add-event-modal").modal('show');
    $(".btn-edit-calendar").show();
    $(".btn-add-calendar").hide();
    $("#chkDone").prop('checked', false);
    $("#chkDone").removeAttr('checked');
    if (lang == 'ar') {
        $("#myModalLabel").html('تعديل حدث');
    }
    else {
        $("#myModalLabel").html('Edit event');
    }
    if (sharedEventColor != '#808e9b') {
        $('.for-todo').hide();
        $('.for-event').removeClass('disabled');
        $('.btn-edit-calendar').show();
        $('.btn-delete-calendar').show();
    }
    else {
        $('.for-todo').show();
        $('.for-event').addClass('disabled');
        $('.btn-edit-calendar').hide();
        $('.btn-delete-calendar').hide();
    }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
function dialogAlert(msg) {
    $('#dialog-alert-msg').html(msg);
    var dialog = $('#dialog-alert').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#btn-dialog-alert-done').click(function () {
        $('#dialog-alert').modal('hide');
    });
}
function addCalendarEvent() {
    var eventName = $("#addEventName").val();
    var eventStartDate = $("#txtaddeventStart").val();
    var eventStartTime = $("#txtaddeventStartTime").val();
    var eventEndDate = $("#txtaddeventEnd").val();
    var eventEndTime = $("#txtaddeventEndTime").val();
    var std = new Date(eventStartDate + " " + eventStartTime);
    var endd = new Date(eventEndDate + " " + eventEndTime);
    var tod = new Date();
    var color = $('#txtaddcolor').val();
    if (eventStartDate != "" && eventStartTime != "" && eventEndDate != "" && eventEndTime != "") {
        if (std >= tod) {
            if (endd >= std) {
                var eventToAdd = {
                    title: $("#addEventName").val(),
                    description: $("#addEventDesc").val(),
                    start: std.toJSON(),
                    end: endd.toJSON(),
                    allDay: isAllDay(std, endd),
                    CreatedBy: $('#createdByHidden').val(),
                    backgroundColor: color
                    , color: color
                };

                if (checkForSpecialChars(eventToAdd.title) || checkForSpecialChars(eventToAdd.description)) {
                    dialogAlert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                }
                else {
                    //alert("sending " + eventToAdd.title);
                    PageMethods.addEvent(eventToAdd, addSuccess);
                    $(this).dialog("close");
                }
            }
            else {
                dialogAlert(lang == 'ar' ? "تاريخ نهاية الحدث اقل من تاريخ بدء الحدث" : "The date of the end of the event less from the date of the start of the event");
            }
        }
        else {
            //var cip = new cip();
            dialogAlert(lang == 'ar' ? "تاريخ الحدث لا يمكن ان يكون اقل من اليوم" : "The date of the event can not be less than a day");
        }
    }
    else {
        var color = "red";
        if (eventName == "")
            $('#addEventName').css('border-color', color);
        if (eventStartDate == "")
            $('#txtaddeventStart').css('border-color', color);
        if (eventStartTime == "")
            $('#txtaddeventStartTime').css('border-color', color);
        if (eventEndDate == "")
            $('#txtaddeventEnd').css('border-color', color);
        if (eventEndTime == "")
            $('#txtaddeventEndTime').css('border-color', color);
    }
}
$('#addEventName').focus(function () {
    $('#addEventName').css('border-color', '#cacaca');
});
$('#txtaddeventStart').focus(function () {
    $('#txtaddeventStart').css('border-color', '#cacaca');
});
$('#txtaddeventStartTime').focus(function () {
    $('#txtaddeventStartTime').css('border-color', '#cacaca');
});
$('#txtaddeventEnd').focus(function () {
    $('#txtaddeventEnd').css('border-color', '#cacaca');
});
$('#txtaddeventEndTime').focus(function () {
    $('#txtaddeventEndTime').css('border-color', '#cacaca');
});
function editCalendarEvent() {
    var eventToUpdate = {
        id: $("#eventId").val(),
        title: $("#addEventName").val(),
        description: $("#addEventDesc").val(),
        backgroundColor: $("#txtaddcolor").val()
    };
    if (checkForSpecialChars(eventToUpdate.title) || checkForSpecialChars(eventToUpdate.description)) {
        alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
    }
    else {
        PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
        var std = new Date($("#txtaddeventStart").val() + " " + $("#txtaddeventStartTime").val());
                             //txtaddeventStart                     txtaddeventStartTime
        var endd = new Date($("#txtaddeventEnd").val() + " " + $("#txtaddeventEndTime").val());
        var stdDate = new Date(std);
        var endDate = new Date(endd);
        $(this).dialog("close");
        currentUpdateEvent.id = $("#eventId").val();
        currentUpdateEvent.title = $("#eventName").val();
        currentUpdateEvent.description = $("#addEventDesc").val();
        currentUpdateEvent.start = stdDate;
        //alert(stdDate.toString());
        //alert(currentUpdateEvent.start.toString());
        currentUpdateEvent.end = endDate;
        //alert(endDate.toString());
        currentUpdateEvent.backgroundColor = $("#txteditcolor").val();
        currentUpdateEvent.borderColor = $("#txteditcolor").val();
        updateEventDateTime(stdDate, endDate, currentUpdateEvent.id);
        //$('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
    }
}
function updateSuccess(updateResult) {
    //alert(updateResult);
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
            calendar.refetchEvents();
            $("#add-event-modal").modal('hide');
            //$('#calendar').fullCalendar('refetchEvents');
            alert(jsdata);
            if (jsdata) {
                //$(".ui-icon-closethick").click()
                //$('#calendar').fullCalendar('rerenderEvents');
            }
            else {
                //nothing
            }
        },
        error: function (result) {
             alert("Error");
        }
    });
}
function checkForSpecialChars(stringToCheck) {
    //var pattern = /[^A-Za-z0-9 ]/;
    //return pattern.test(stringToCheck); 
    return false;
}
function addSuccess(addResult) {
    // if addresult is -1, means event was not added
    if (addResult != -1) {
        calendar.refetchEvents();
        $("#add-event-modal").modal('hide');
        $("#add-event-modal").find('input').val('');
        $("#add-event-modal").find('textarea').val('');
    }

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