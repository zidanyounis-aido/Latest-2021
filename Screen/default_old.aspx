<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="default_old.aspx.cs" Inherits="DMS.screen._default_old" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/jquery.iconmenu.js" type="text/javascript"></script>
<script>
    $(function () {

        $('#pnlMin').css({ "position": "absolute", "top": "no-repeat", "top": String(($(window).height())-150 ) + "px", "left": "-150px"
        });
        //$('#main-content').css({ 'height': (($(document).height()) - 162) + 'px' });


        $(window).resize(function () {
            $('#pnlMin').css({ "position": "absolute", "top": "no-repeat", "top": String(($(window).height())-150) + "px", "left": "-150px"
            });

        });
    });

    $(function () {
        $(".ui-widget-content").hide();
    });
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
        if (showMinFlg == true)
        { hideMinis(); }
        else
        { showMinis(); }
    }

    function showMinis() {
        var dCount = 0;
        $("#miniCont").html('');
        for (var i = 0; i < minCount; i++) {
                var dlg = document.getElementById("min_" + String(i))
                if (dlg == null) {
                    var s = "<table dir='<%= (Session["lang"].ToString() == "0") ? "ltr" : "rtl"%>' id='min_" + String(i) + "' style='color:black;width:85px;display:none; position:absolute;z-index:50000;top:" + String(($(window).height()) - 110) + "px'><tr>"
            + "<td align='center' onClick='hideMinis();showMinDialog(" + String(i) + "," + String(minis[i][0]) + ");'>"
            + "<img border='0' id='ImageMin' style='width:64px' src='../Images/blue_Icons/window-icon.png'"
            + " title='" + minis[i][1] + "' /><br /><b>" + minis[i][1] + "</b>"
            + "</td>    </tr></table>";
                    $("#miniCont").append(s);

                    $("#min_" + String(i)).css('display', 'table');
                    $("#min_" + String(i)).animate({ opacity: 1, left: String((i + 2) * 100) + "px" }, 'slow');
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
        //$("document").add("<div>
        //$("#mainCont").append(
        var dlg = document.getElementById("dialog_" + String(dialogCount))
        if (dlg == null) {
            var s = "<div onmousedown=\"activeDialog('" + String(dialogCount) + "')\" style='position:absolute; border:1px solid gray;background-color: rgb(255, 255, 255);' id=\"dialog_" + String(dialogCount) + "\" class=\"ui-widget-content\" >";
            s += " <table dir='<%= (Session["lang"].ToString() == "0") ? "ltr" : "rtl"%>' cellpadding='0' cellspacing='0' width='100%'";
            s += " style='height:39px;background-color:#959595;'>";
            s += " <tr><td valign='middle' align='<%= (Session["lang"].ToString() == "0") ? "left" : "right"%>' style='padding-left:5px; width:26px'>";
            s += " <img style='height:32px; margin-left: 5px;margin-right: 5px;' src='../Images/SmallIcons/icon" + CODEN + ".png' /></td>";
            s += " <td valign='middle' style='padding-left:5px'><span style='text-shadow:1px 1px 1px #000;color:white;text-align: <%= (Session["lang"].ToString() == "0") ? "left" : "right"%>;font-size: 19px;font-weight: bold;'> " + Title + "</span>";
            s += " </td><td align='<%= (Session["lang"].ToString() == "0") ? "right" : "left"%>' valign='bottom' style='padding-right:10px'>";
            s += " <img title='Minimize Window' width='32px' style='cursor:pointer' src='../Images/blue_Icons/minimize-icon.png' onclick=\"minimizeWindow('" + String(dialogCount) + "','" + Title + "')\" /> ";
            s += " <img title='Refresh Window' width='32px' style='cursor:pointer' src='../Images/blue_Icons/refresh-Icon.png' onclick=\"refreshDialog('" + String(dialogCount) + "','" + PageName + "')\" /> ";
            s += " <img title='Close Window' width='32px' style='cursor:pointer' src='../Images/blue_Icons/close-Icon.png' onclick=\"hideDialog('" + String(dialogCount) + "')\" /></td></tr></table>";
            s += " <iframe onclick=\"activeDialog('" + String(dialogCount) + "')\" style=\"background-color:#ffffff;border: 0px;\" id=\"frame_" + String(dialogCount) + "\" src='subPages.aspx'></iframe>";
            s += " </div>";

            $("#mainCont").append(s);
        }
        var defWidth = _Width;
        var defHeigh = _Height;
        $("#dialog_" + String(dialogCount)).width(0);
        $("#dialog_" + String(dialogCount)).height(0);
        $("#dialog_" + String(dialogCount)).css("top", String(($(window).height() / 2) - (defHeigh / 2)) + "px");
        $("#dialog_" + String(dialogCount)).css("left", String(($(window).width() / 2) - (defWidth / 2)) + "px");
        $("#dialog_" + String(dialogCount)).show();
        $("#dialog_" + String(dialogCount)).animate({ width: defWidth, height: defHeigh }, "slow");
        $("#frame_" + String(dialogCount)).animate({ width: defWidth - 6, height: defHeigh - 50 }, "slow");
        $("#dialog_" + String(dialogCount)).draggable();
        $("#dialog_" + String(dialogCount)).resizable({
            resize: function (event, ui) {
                var currentId = $(this).attr('id');
                var n = currentId.split("_");
                $("#frame_" + n[1]).height($(this).height() - 50);
                $("#frame_" + n[1]).width($(this).width() - 6);
            }
        });
        document.getElementById("frame_" + String(dialogCount)).src = "../Screen/" + PageName + ".aspx?CODEN=" + CODEN + "&dlgid=" + String(dialogCount);
        activeDialog(String(dialogCount));
    }

    function refreshDialog(CODEN, PageName) {
        //document.getElementById("frame_" + CODEN).src = "../Screen/" + PageName + ".aspx?CODEN=" + CODEN;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="mainCont">de</div>
<div id="miniCont"></div>
<div style="height:50px;"></div>
    <asp:Repeater ID="rptMainIcons" runat="server">
    <HeaderTemplate>
        <div id="iconscontent">
			<ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
        <a  href="javascript:showDialog('<%# DataBinder.Eval(Container.DataItem, "programID") %>','<%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%>','<%# DataBinder.Eval(Container.DataItem, "programURL") %>',<%# DataBinder.Eval(Container.DataItem, "windowWidth") %>,<%# DataBinder.Eval(Container.DataItem, "windowHeight") %>)">
        <span class="imgcont">
        <img border="0" id="Image1"  src='../Images/icons/icon<%# DataBinder.Eval(Container.DataItem, "programID") %>.png'
        title='<%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%>' />
        </span>
        <br />
         <span><%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%>
         </span>
         </a>
         
         </li>
    
    </ItemTemplate>
    <FooterTemplate>
        </ul>
		</div> 
    </FooterTemplate>
    </asp:Repeater>
    
    <div onclick="minPnlClick()" id="pnlMin" style=" width:300px; height:150px; overflow:hidden">
     <span id="minCount" style="position:relative; top:100px; left:180px; color:Red; z-index:1000; text-shadow:1px 1px 1px #000;color:white; font-size:34px">
    0
    </span>
    <canvas id="myCanvas" width="300px" height="300px">
        
    </canvas></div>
    <script>

        var canvas = document.getElementById('myCanvas');
        var context = canvas.getContext('2d');
        var centerX = canvas.width / 2;
        var centerY = canvas.height / 2;
        var redius = 150;
        context.beginPath();
        context.arc(centerX, centerY, redius, 1.5 * Math.PI, 0, false);
        context.lineTo(centerX, centerY);
        context.lineWidth = 5;
        var grd = context.createRadialGradient(centerX, centerY, 150, centerX, centerY, 10);
        grd.addColorStop(0, "#cccccc");
        grd.addColorStop(1, "#7b93ad");
        context.fillStyle = grd;
        context.fill();
        //        context.strokeStyle = '#cccccc';
        //        context.stroke();
    
    </script>
</asp:Content>
