<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="EventsListbak.aspx.cs" Inherits="dms.Screen.EventsListbak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="EventListsStyles/jquery-ui.min.css" rel="stylesheet" />
    <link href="fullcalendar-3.9.0/fullcalendar.css" rel="stylesheet" />
    <link href="EventListsStyles/jquery.qtip.min.css" rel="stylesheet" />

    <style type='text/css'>
        body {
            margin-top: 40px;
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            overflow: hidden !important;
        }

        #calendar {
            width: 100%;
            margin: 0 auto;
        }
        /* css for timepicker */

        .fc-basic-view .fc-body .fc-row {
            height: 150px !important;
            overflow: hidden !important;
        }

        .ui-timepicker-div dl {
            text-align: left;
        }

            .ui-timepicker-div dl dt {
                height: 25px;
            }

            .ui-timepicker-div dl dd {
                margin: -25px 0 10px 65px;
            }

        .style1 {
            width: 100%;
        }

        /* table fields alignment*/
        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 10px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 10px;
        }

        .fc-month-view .fc-time {
            display: none;
        }
    </style>
    <%--<link href="../css/color-picker.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="createdByHidden" value="<%= Session["userId"].ToString() %>" />
    <div id="calendar" class="slate">
    </div>
    <div id="updatedialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;"
        title="حذف أو تعديل مهمة">
        <table class="style1">
            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Event Title" : "عنوان الحدث"%>:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" size="33" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Details" : "التفاصيل"%>:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="3"></textarea></td>
            </tr>
            <tr class="not-done" style="display: none">
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "From" : "من"%>:</td>
                <td class="alignLeft">
                    <%--<span id="eventStart"></span>--%>
                    <input id="txteventStart" type="text" />
                </td>
            </tr>
            <tr class="not-done" style="display: none">
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "To" : "إلى"%>: </td>
                <td class="alignLeft">
                    <%--<span id="eventEnd"></span>--%>
                    <input type="hidden" id="eventId" />
                    <%--<br />--%>
                    <input id="txteventEnd" type="text" />
                </td>
            </tr>

            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Color" : "اللون"%>:</td>
                <td class="alignLeft">
                    <input id="txteditcolor" class="jscolor" type="text" />
                </td>
            </tr>
            <tr class="as-done" style="display: none">
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Task Date" : "تاريخ المهمة"%>:</td>
                <td class="alignLeft">
                    <span id="eventStart"></span>
                </td>
            </tr>
            <tr class="as-done" style="display: none">
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Mark as done" : "اكمال المهمة"%>: </td>
                <td class="alignLeft" style="margin-right: 0px !important; float: right;">
                    <input id="checkBox" id="chkDone" onclick="markAsDone();" type="checkbox" />
                </td>
            </tr>
        </table>
    </div>
    <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px;" title="اضافة حدث">
        <table class="style1">
            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Event Title" : "عنوان الحدث"%>:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="33" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Details" : "التفاصيل"%>:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3"></textarea></td>
            </tr>
            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "From" : "من"%></td>
                <td class="alignLeft">
                    <span id="addEventStartDate"></span>
                    <input id="txtaddeventStart" type="text" />
                </td>
            </tr>
            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "To" : "إلى"%>:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate"></span>
                    <input id="txtaddeventEnd" type="text" />
                </td>
            </tr>
            <tr>
                <td class="alignRight"><%= (Session["lang"].ToString() == "0") ? "Color" : "اللون"%>:</td>
                <td class="alignLeft">
                    <input id="txtaddcolor" class="jscolor" value="f68b1e" type="text" />
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    <script type="text/javascript" src="fullcalendar/lib/moment.min.js"></script>
    <script type="text/javascript" src="fullcalendar/lib/jquery.min.js"></script>
    <script type="text/javascript" src="fullcalendar/lib/jquery-ui.min.js"></script>
    <script src="EventListsScripts/jquery.qtip.min.js" type="text/javascript"></script>
    <script src="fullcalendar-3.9.0/fullcalendar.js"></script>
    <script src="fullcalendar-3.9.0/locale/ar-sa.js"></script>
    <script src="../JS/calendarscript.js" type="text/javascript"></script>
    <script src="datetime-picker/datetime.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
