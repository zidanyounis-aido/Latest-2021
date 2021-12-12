<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="date.aspx.cs" Inherits="dms.date" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-3.3.1.js" integrity="sha256-2Kok7MbOyxpgUVvAk/HJ2jigOSYS2auK4Pfzbm7uH60=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="/jquryClendar/js/jquery.calendars.js"></script>
    <script type="text/javascript" src="/jquryClendar/js/jquery.calendars.plus.js"></script>
    <script id="islamcCalnederjs" src="/jquryClendar/js/jquery.calendars.islamic.js"></script>

    <link rel="stylesheet" type="text/css" href="/jquryClendar/css/jquery.calendars.picker.css">
    <script type="text/javascript" src="/jquryClendar/js/jquery.plugin.js"></script>
    <script type="text/javascript" src="/jquryClendar/js/jquery.calendars.picker.js"></script>
    <%--$.calendars.instance('gregorian');--%>
    <script type="text/javascript">
        var mylang = 'gregorian';
        $(function () {
           // document.getElementById('spnlblcheckbox').innerHTML = 'gregorian';
            $('#txtdatajs').calendarsPicker({ calendar: $.calendars.instance('<%= (Session["lang"].ToString() == "islamic") ? "islamic" : "gregorian"%>') });
            //document.getElementById('spnlblcheckbox').innerHTML = <%= (Session["lang"].ToString() == "islamic") ? "islamic" : "gregorian"%>;

            //$('#datajs').calendarsPicker();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="form-check">
                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                <label class="form-check-label" for="exampleCheck1" id="spnlblcheckbox" runat="server"></label>
            </div>
            <br />
            <input id="txtdatajs" runat="server" type="text" value="" /><br />
            <label style="color:red" runat="server" id="lblotherdate"></label>
            <br /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="Get Other Date" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
