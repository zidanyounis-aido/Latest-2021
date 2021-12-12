<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="dms.Screen.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/canvasjs-1.6.2/jquery.canvasjs.min.js" type="text/javascript"></script>
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
    <asp:Literal ID="ltrScript2" runat="server"></asp:Literal>
    <style>
        body {
            background-color: #f2f2f2 !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var tabTitle = $("#tab_title"),
            tabContent = $("#tab_content"),
            tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='ui-icon ui-icon-close' role='presentation'>Remove Tab</span></li>",
            tabCounter = 2;
        var tabs = $("#tabs");
        var lang =<%= (Session["lang"].ToString() == "0") ? "'en'" : "'ar'"%>;
        function openFileDocument(xthis) {
            //var x = $(".menuDiv")[1];
            //var title = $(x).find(".imgcont").attr('title');
            //addTab('1', 'المجلدات', 'defaultAr', 99)
            var title = 'Folders';
            if (lang == "ar") {
                title = 'المجلدات';
            }
            parent.addTab('1', title, 'documentInfo.aspx?docID=' + $(xthis).attr("data-id") + '', 99);
            //parent
        }
        function saveCanvasToImage(canvasId, type) {
            canvasId = canvasId || '#canvastwo';
            //var canvas = document.getElementById("canvastwo");
            //document.getElementById("theimage").src = canvas.toDataURL();
            //Canvas2Image.saveAsPNG(canvas);
            var canvas = document.querySelector(canvasId);
            var dataURL = canvas.toDataURL("image/" + type, 1.0);
            if (type != 'print') {
                var imgName = 'dashboard.' + type;
                downloadImage(dataURL, imgName);
            }
            else {
                var windowContent = '<!DOCTYPE html>';
                windowContent += '<html>'
                windowContent += '<head><title>Print canvas</title></head>';
                windowContent += '<body>'
                windowContent += '<img src="' + dataURL + '">';
                windowContent += '</body>';
                windowContent += '</html>';
                var printWin = window.open('', '', 'width=340,height=260');
                printWin.document.open();
                printWin.document.write(windowContent);
                printWin.document.close();
                printWin.focus();
                printWin.print();
                printWin.close();
            }
        }
        // Save | Download image
        function downloadImage(data, filename = 'untitled.jpeg') {
            var a = document.createElement('a');
            a.href = data;
            a.download = filename;
            document.body.appendChild(a);
            a.click();
        }
    </script>

    <style>
        .fc-timegrid-event {
            pointer-events: none;
        }

        .fc-timegrid-event-harness {
            pointer-events: none;
        }

        td.fc-timegrid-slot.fc-timegrid-slot-lane {
            pointer-events: none;
        }

        .fc-non-business {
            background: #FFF !important;
        }

        .fc-v-event {
            opacity: 0.75;
            border-left: 6px solid rgba(0,0,0, 0.5) !important;
            /* border-left: solid 4px #000 !important; */
            /*            display: block;
            border: 1px solid #3788d8;
            border: 1px solid var(--fc-event-border-color,#3788d8);
            background-color: #3788d8;
            background-color: var(--fc-event-bg-color,#3788d8);*/
        }

        aside.aside-event .fc-toolbar-chunk .fc-toolbar-title {
            font-size: 11px !important;
        }

        a.fc-timegrid-event.fc-v-event.fc-event.fc-event-start.fc-event-end.fc-event-today.fc-event-future {
            /* border: none !important;*/
            border-radius: 0px !important;
        }

        .home-block .home-block-ul li:last-of-type {
            /*border-bottom: 1px solid #d9d9d9 !important;*/
        }

        .fc-scrollgrid-sync-table {
            display: none !important;
        }

        .fc-col-header {
            display: none !important;
        }

        .page-content.home-page {
            background-color: #f8f8f8;
        }
    </style>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content home-page">
        <div class="main-slider swiper-container" style="background: #007aff !important;">
            <div class="swiper-wrapper">
                <div class="swiper-slide">
                    <h3 class="slider-title">اهلا بك <span runat="server" id="lblWelcomeDashBoad"></span></h3>
                    <p class="slider-dec">
                        نموذج نصي يستخدم منذ القرن السادس عشر بديلا للمحتوى. بدلا من كتابة “اكتب
                        المحتوى هنا” مرارا وتكرارا، تقدم أداة الأبجدية توزيع شبه طبيعي للكلمات والحروف
                    </p>
                </div>

                <%-- <div class="swiper-slide">
                    <h3 class="slider-title">اهلا بك 2</h3>
                    <p class="slider-dec"> نموذج نصي يستخدم منذ القرن السادس عشر بديلا للمحتوى. بدلا من كتابة “اكتب
                        المحتوى هنا” مرارا وتكرارا، تقدم أداة الأبجدية توزيع شبه طبيعي للكلمات والحروف</p>
                </div>

                <div class="swiper-slide">
                    <h3 class="slider-title">اهلا بك 3</h3>
                    <p class="slider-dec"> نموذج نصي يستخدم منذ القرن السادس عشر بديلا للمحتوى. بدلا من كتابة “اكتب
                        المحتوى هنا” مرارا وتكرارا، تقدم أداة الأبجدية توزيع شبه طبيعي للكلمات والحروف</p>
                </div>--%>
            </div>

            <div class="main-slider-arrow swiper-button-next">
                <svg xmlns="http://www.w3.org/2000/svg" width="8.231"
                    height="15.279" viewBox="0 0 8.231 15.279">
                    <g id="Group_2126" data-name="Group 2126" transform="translate(8.231 15.279) rotate(180)">
                        <g id="Group_2125" data-name="Group 2125" transform="translate(0 0)">
                            <path id="Path_6981" data-name="Path 6981"
                                d="M8.059,7.22,1,.165A.588.588,0,0,0,.172,1l6.64,6.64-6.64,6.64A.588.588,0,1,0,1,15.107L8.059,8.052A.588.588,0,0,0,8.059,7.22Z"
                                fill="#484848">
                            </path>
                        </g>
                    </g>
                </svg>
            </div>
            <div class="main-slider-arrow swiper-button-prev">
                <svg id="Group_3108" data-name="Group 3108"
                    xmlns="http://www.w3.org/2000/svg" width="8.231" height="15.279" viewBox="0 0 8.231 15.279">
                    <g id="Group_2125" data-name="Group 2125" transform="translate(0 0)">
                        <path id="Path_6981" data-name="Path 6981"
                            d="M8.059,7.22,1,.165A.588.588,0,0,0,.172,1l6.64,6.64-6.64,6.64A.588.588,0,1,0,1,15.107L8.059,8.052A.588.588,0,0,0,8.059,7.22Z"
                            fill="#484848">
                        </path>
                    </g>
                </svg>
            </div>

            <div class="btn-main-slider-arrow">
                <svg xmlns="http://www.w3.org/2000/svg" width="16.983" height="16.366" viewBox="0 0 16.983 16.366">
                    <g id="Group_3108" data-name="Group 3108" transform="translate(-382.637 -457.964)">
                        <path id="Path_6976" data-name="Path 6976"
                            d="M0,6.763V2.593l.967.967L4.526,0,6.763,2.237,3.2,5.8l.967.967Z"
                            transform="translate(399.62 464.727) rotate(180)" fill="#fff" />
                        <path id="Path_6977" data-name="Path 6977"
                            d="M6.763,6.763V2.593L5.8,3.56,2.237,0,0,2.237,3.56,5.8l-.967.967Z"
                            transform="translate(389.4 464.727) rotate(180)" fill="#fff" />
                        <path id="Path_6978" data-name="Path 6978"
                            d="M0,0V4.17L.967,3.2l3.56,3.559L6.763,4.526,3.2.967,4.17,0Z"
                            transform="translate(399.62 474.33) rotate(180)" fill="#fff" />
                        <path id="Path_6979" data-name="Path 6979"
                            d="M6.763,0V4.17L5.8,3.2l-3.56,3.56L0,4.526,3.56.967,2.593,0Z"
                            transform="translate(389.4 474.33) rotate(180)" fill="#fff" />
                    </g>
                </svg>

            </div>
        </div>

        <div class="row">
            <div class="col-xs-6">
                <div class="home-block">
                    <div class="home-block-item-title">
                        <span class="title"><%= (Session["lang"].ToString() == "0") ? "Inbox" : "البريد"%> <span class="num" id="spnInboxCount" runat="server">0</span></span>
                        <a onclick="parent.addTab('6','<%= (Session["lang"].ToString() == "0") ? "Inbox" : "البريد"%>','myWorkflowDocs',99)"><%= (Session["lang"].ToString() == "0") ? "Show All" : "عرض الكل"%></a>
                    </div>
                    <ul class="home-block-ul" style="height: 320px;">
                        <asp:Panel ID="pnlInbox" runat="server">
                            <asp:Repeater ID="rptInbox" runat="server">
                                <ItemTemplate>
                                    <li onclick="parent.addTab('6','<%= (Session["lang"].ToString() == "0") ? "Inbox" : "البريد"%>','myWorkflowDocs',0,<%# Eval("ID") %>)" style="border-bottom: 1px solid #d9d9d9 !important;">
                                        <div class="item-info">
                                            <p class="item-name"><%# Session["lang"].ToString() == "0"? Eval("docTypDesc") : Eval("docTypDescAr") %>  </p>
                                            <p class="item-dec"><%# Eval("docName") %></p>
                                            <div class="files-holder">
                                                <asp:ListView ID="lstFiles" runat="server" DataSource='<%# Eval("FilesList") %>' ItemPlaceholderID="addressPlaceHolder">
                                                    <ItemTemplate>
                                                        <span class="item-file"><%# string.IsNullOrEmpty(Eval("DocumentFileName").ToString()) ? string.Concat(Eval("docName"), "-", Eval("version")) : Eval("DocumentFileName").ToString()%></span>
                                                        <%--  <span class="item-file">file2.jpg</span>--%>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <span class="more-files"><%# Eval("versionCount") %></span>
                                            </div>
                                        </div>
                                        <div class="item-date">
                                            <%# DateTime.Parse(Eval("receiveDate").ToString()).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo(Eval("Culture").ToString())) %>
                                            <br />
                                            <%# DateTime.Parse(Eval("receiveDate").ToString()).ToString("hh:mm tt") %>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                        <asp:Panel ID="pnlEmptyInbox" runat="server" Visible="false" Style="text-align: center; padding-top: 110px;">
                            <li>
                                <div class="item-info" style="margin: auto;">
                                    <p class="item-name"><%= (Session["lang"].ToString() == "0") ? "You have no messages" : "ليس لديك رسائل"%></p>

                                </div>
                                <div class="item-date">
                                </div>
                            </li>
                        </asp:Panel>
                    </ul>
                </div>
            </div>
            <div class="col-xs-6">
                <div class="home-block">
                    <div class="home-block-item-title">
                        <span class="title"><%= (Session["lang"].ToString() == "0") ? "Tasks" : "المهام"%> <span class="num" id="tasksListcount" runat="server">0</span></span>
                        <a onclick="parent.addTab('30','<%= (Session["lang"].ToString() == "0") ? "Tasks" : "المهام"%>','ToDoList',99)"><%= (Session["lang"].ToString() == "0") ? "Show All" : "عرض الكل"%></a>
                    </div>
                    <ul class="home-block-ul" style="height: 320px;">
                        <asp:Panel ID="PnlTodo" runat="server">
                            <asp:Repeater ID="rptTodoList" runat="server">
                                <ItemTemplate>
                                    <li onclick="parent.addTab('30','<%= (Session["lang"].ToString() == "0") ? "Tasks" : "المهام"%>','AddToDoList',<%# Eval("Id") %>)" style="border-bottom: 1px solid #d9d9d9 !important;">
                                        <div class="item-info">
                                            <p class="item-name"><%# Eval("TaskName") %></p>
                                            <p class="item-dec"><%# Eval("Description") %></p>
                                        </div>
                                        <div class="item-date">
                                            <%--<%# Eval("TaskDate") %>--%>
                                            <%# Eval("TaskDate").ToString() != "" ? DateTime.Parse(Eval("TaskDate").ToString()).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo((Session["lang"].ToString() == "0") ? "en-US" : "ar-AE")) :"" %>
                                            <br />
                                            <%# Eval("TaskDate").ToString() != "" ? DateTime.Parse(Eval("TaskDate").ToString()).ToString("hh:mm tt") : ""%>
                                            <span class="item-file" style="display: inline-block; color: rgba(0, 114, 255, 1); border: 1px solid rgba(0, 114, 255, 1); border-radius: 20px; margin: 0 5px; padding: 0px 10px; font-size: 13px; display: block; margin-top: 5px;"><%# (Session["lang"].ToString() == "0") ? Eval("StsEn") : Eval("StsAr")%></span>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                        <asp:Panel ID="pnlEmptyTasks" runat="server" Visible="false" Style="text-align: center; padding-top: 110px;">
                            <li>
                                <div class="item-info" style="margin: auto;">
                                    <p class="item-name"><%= (Session["lang"].ToString() == "0") ? "You have no tasks" : "ليس لديك مهام"%></p>

                                </div>
                                <div class="item-date">
                                </div>
                            </li>
                        </asp:Panel>
                    </ul>
                    <div class="home-block-add"><a onclick="parent.addTab('30','<%= (Session["lang"].ToString() == "0") ? "Tasks" : "المهام"%>','AddToDoList')"><%= (Session["lang"].ToString() == "0") ? "Add New Task" : "إضافة مهمة جديدة"%></a></div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6">
                <div class="home-block">
                    <div class="home-block-item-title">
                        <span class="title"><%= (Session["lang"].ToString() == "0") ? "Latest documents" : "احدث المستندات"%>
                            <span class="num" style="display: none">+100</span></span>
                        <a onclick="parent.addTab('1','<%= (Session["lang"].ToString() == "0") ? "Latest documents" : "احدث المستندات"%>','defaultAr',99)"><%= (Session["lang"].ToString() == "0") ? "Show All" : "عرض الكل"%></a>
                    </div>
                    <ul class="home-block-ul" style="height: 320px;">
                        <asp:Panel ID="pnlUploads" runat="server">
                            <asp:Repeater ID="rptUploads" runat="server">
                                <ItemTemplate>
                                    <li data-id="<%# Eval("docId") %>" onclick="openFileDocument(this);" style="border-bottom: 1px solid #d9d9d9 !important;">
                                        <div class="item-info">
                                            <p class="item-name"><%# Eval("docName") %></p>
                                            <p class="item-dec"><%# (Session["lang"].ToString() == "0") ? Eval("fldrName") :  Eval("fldrNameAr")%></p>
                                        </div>
                                        <div class="item-date">
                                            <%# Eval("docDate").ToString() != "" ? DateTime.Parse(Eval("docDate").ToString()).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo((Session["lang"].ToString() == "0") ? "en-US" : "ar-AE")) :"" %>
                                            <br />
                                            <%# Eval("docDate").ToString() != "" ? DateTime.Parse(Eval("docDate").ToString()).ToString("hh:mm tt") : ""%>
                                        </div>

                                    </li>
                                </ItemTemplate>

                            </asp:Repeater>

                        </asp:Panel>
                        <asp:Panel ID="pnlEmptyUploads" runat="server" Visible="false" Style="text-align: center; padding-top: 110px;">
                            <li>
                                <div class="item-info" style="margin: auto">
                                    <p class="item-name"><%= (Session["lang"].ToString() == "0") ? "No uploaded documents" : "لا يوجد مستندات محملة"%></p>
                                </div>
                                <div class="item-date">
                                </div>
                            </li>
                        </asp:Panel>
                    </ul>

                    <div class="home-block-add"><a onclick="parent.addTab('1','<%= (Session["lang"].ToString() == "0") ? "Latest documents" : "احدث المستندات"%>','defaultAr',99)"><%= (Session["lang"].ToString() == "0") ? "Add a new document" : "إضافة مستند جديد"%></a></div>
                </div>
            </div>

            <div class="col-xs-6">
                <div class="home-block">
                    <div class="home-block-item-title">
                        <span class="title"><%= (Session["lang"].ToString() == "0") ? "Correspondences" : "الصادر والوارد"%> <span
                            class="num" runat="server" id="spnInOutCount"></span></span>
                        <a onclick="parent.addTab('28','<%= (Session["lang"].ToString() == "0") ? "Correspondences" : "الصادر والوارد"%>','DefaultDiwan',99)"><%= (Session["lang"].ToString() == "0") ? "Show All" : "عرض الكل"%></a>
                    </div>
                    <ul class="home-block-ul" style="height: 320px;">
                        <asp:Panel ID="pnlInOutCome" runat="server">
                            <asp:Repeater ID="rptInOutCome" runat="server">
                                <ItemTemplate>
                                    <li data-id="<%# Eval("docId") %>" onclick="openFileDocument(this);" style="border-bottom: 1px solid #d9d9d9 !important;">
                                        <div class="item-info">
                                            <p class="item-name"><%# Eval("docName") %></p>
                                        </div>
                                        <div class="item-date">

                                            <%# Eval("docDate").ToString() != "" ? DateTime.Parse(Eval("docDate").ToString()).ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo((Session["lang"].ToString() == "0") ? "en-US" : "ar-AE")) :"" %>
                                            <br />
                                            <%# Eval("docDate").ToString() != "" ? DateTime.Parse(Eval("docDate").ToString()).ToString("hh:mm tt") : ""%>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                        <asp:Panel ID="pnlEmptyInOutCome" runat="server" Visible="false" Style="text-align: center; padding-top: 110px;">
                            <li>
                                <div class="item-info" style="margin: auto;">
                                    <p class="item-name"><%= (Session["lang"].ToString() == "0") ? "No uploaded documents" : "لا يوجد مستندات محملة"%></p>

                                </div>
                                <div class="item-date">
                                </div>
                            </li>
                        </asp:Panel>
                    </ul>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6">
                <div class="home-block chart-block">
                    <div class="home-block-item-title"><span class="title"><%= (Session["lang"].ToString() == "0") ? "Documents uploaded 30 days ago" : "المستندات المرفوعة منذ 30 يوما"%> </span></div>
                    <ul class="home-block-ul">
                        <li class="btn-charts-dropdown">
                            <div class="dropdown">
                                <div id="dLabel" type="button" data-toggle="dropdown" aria-haspopup="true"
                                    aria-expanded="false">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="19.557" height="14"
                                        viewBox="0 0 19.557 14">
                                        <g id="Group_2008" data-name="Group 2008" transform="translate(1 1)">
                                            <line id="Line_25" data-name="Line 25" x2="17.557" fill="none"
                                                stroke="#484848" stroke-linecap="round" stroke-width="2" />
                                            <line id="Line_26" data-name="Line 26" x2="17.557"
                                                transform="translate(0 6)" fill="none" stroke="#484848"
                                                stroke-linecap="round" stroke-width="2" />
                                            <line id="Line_27" data-name="Line 27" x2="17.557"
                                                transform="translate(0 12)" fill="none" stroke="#484848"
                                                stroke-linecap="round" stroke-width="2" />
                                        </g>
                                    </svg>

                                </div>
                                <ul class="dropdown-menu" aria-labelledby="dLabel">
                               <li onclick="saveCanvasToImage('#canvasone','print');">Print</li>
                                    <li onclick="saveCanvasToImage('#canvasone','jpeg');">Save as JPEG</li>
                                    <li onclick="saveCanvasToImage('#canvasone','png');">Save as PNG</li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <%--  <div id="chartContainer" style="height: 300px; width: 460px;"></div>--%>
                            <canvas id="canvasone" class="home-charts"></canvas>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="col-xs-6">
                <div class="home-block chart-block">
                    <div class="home-block-item-title"><span class="title"><%= (Session["lang"].ToString() == "0") ? "The number of documents in each folder" : "عدد المستندات في كل مجلد"%> </span></div>
                    <ul class="home-block-ul">
                        <li class="btn-charts-dropdown">
                            <div class="dropdown">
                                <div id="dLabel" type="button" data-toggle="dropdown" aria-haspopup="true"
                                    aria-expanded="false">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="19.557" height="14"
                                        viewBox="0 0 19.557 14">
                                        <g id="Group_2008" data-name="Group 2008" transform="translate(1 1)">
                                            <line id="Line_25" data-name="Line 25" x2="17.557" fill="none"
                                                stroke="#484848" stroke-linecap="round" stroke-width="2" />
                                            <line id="Line_26" data-name="Line 26" x2="17.557"
                                                transform="translate(0 6)" fill="none" stroke="#484848"
                                                stroke-linecap="round" stroke-width="2" />
                                            <line id="Line_27" data-name="Line 27" x2="17.557"
                                                transform="translate(0 12)" fill="none" stroke="#484848"
                                                stroke-linecap="round" stroke-width="2" />
                                        </g>
                                    </svg>

                                </div>
                                <ul class="dropdown-menu" aria-labelledby="dLabel">
                                    <li onclick="saveCanvasToImage('#canvastwo','print');">Print</li>
                                    <li onclick="saveCanvasToImage('#canvastwo','jpeg');">Save as JPEG</li>
                                    <li onclick="saveCanvasToImage('#canvastwo','png');">Save as PNG</li>
                                </ul>
                            </div>

                            <p class="charts-calc-num"><%= (Session["lang"].ToString() == "0") ? "Total" : "المجموع"%> <span runat="server" id="lblparchartcount">0</span> </p>
                        </li>
                        <li>
                            <%--  <div id="pieContainer" style="height: 300px; width: 460px;"></div>--%>
                            <canvas id="canvastwo" class="home-charts"></canvas>
                            <img id="theimage" style="display: none" />
                        </li>
                    </ul>
                </div>
            </div>

        </div>

        <aside class="aside-event">

            <div class="calender-event-cover">
                <div class="calender-event-holder">
                    <div class="aside-event-title" style="margin-top: 25px;">
                        <span class="title" style="font-size: 16px;"><%= (Session["lang"].ToString() == "0") ? "Calendar" : "التقويم"%> </span>
                        <a onclick="parent.addTab('31','<%= (Session["lang"].ToString() == "0") ? "Events" : "الاحداث"%>','EventsList',99)" style="font-size: 12px;"><%= (Session["lang"].ToString() == "0") ? "Show All" : "عرض الكل"%></a>
                    </div>
                    <div id='calendar-aside'></div>
                </div>
            </div>

            <div class="btn-collapse-aside-event">
                <svg xmlns="http://www.w3.org/2000/svg" width="8.231" height="15.279" viewBox="0 0 8.231 15.279">
                    <g id="Group_2126" data-name="Group 2126" transform="translate(8.231 15.279) rotate(180)">
                        <g id="Group_2125" data-name="Group 2125" transform="translate(0 0)">
                            <path id="Path_6981" data-name="Path 6981"
                                d="M8.059,7.22,1,.165A.588.588,0,0,0,.172,1l6.64,6.64-6.64,6.64A.588.588,0,1,0,1,15.107L8.059,8.052A.588.588,0,0,0,8.059,7.22Z"
                                fill="#484848">
                            </path>
                        </g>
                    </g>
                </svg>
            </div>
        </aside>
    </div>

    <div class="block" id="divpnlInbox" runat="server" style="display: none;">
        <div class="title main-title"><%= (Session["lang"].ToString() == "0") ? "Inbox" : "البريد"%></div>
        <div class="content">
        </div>
    </div>
    <div class="block" id="divpnlFolder" runat="server" style="display: none;">
        <div class="title main-title"><%= (Session["lang"].ToString() == "0") ? "Recently Uploaded" : "أحدث المستندات"%></div>
        <div class="content">
            <%--          <asp:Panel ID="pnlUploads" runat="server">
                <asp:Repeater ID="rptUploads" runat="server">
                    <ItemTemplate>
                        <div class="message">
                            <a href="#!" data-id="<%# Eval("docId") %>" onclick="openFileDocument(this);">
                                <i class="fas fa-file-alt" style="color: #d8914a"></i>
                                <%# Eval("docName") %>
                            </a>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>

            </asp:Panel>--%>

            <%-- <asp:Panel ID="pnlEmptyUploads" runat="server" Visible="false" HorizontalAlign="Center">
                <%= (Session["lang"].ToString() == "0") ? "No uploaded documents" : "لا يوجد مستندات محملة"%>
                <br />
                <img width="100%" src="../Images/Trash-2-Empty-icon.png" />
            </asp:Panel>--%>
        </div>
    </div>
    <%--<div class="block" style="width: 460px !important; display: none;">
        <div class="title main-title"><%= (Session["lang"].ToString() == "0") ? "Recent 30 Days Uploads" : "المستندات المرفوعة منذ 30 يوماً"%></div>
        <div class="content" style="overflow: hidden !important">
            <div id="chartContainer" style="height: 300px; width: 460px;"></div>
        </div>
    </div>--%>
    <%--   <div class="block" style="width: 460px !important; display: none;">
        <div class="title main-title"><%= (Session["lang"].ToString() == "0") ? "Number of docs in each folder" : "عدد المستندات في كل مجلد"%></div>
        <div class="content" style="overflow: hidden !important">
            <div id="pieContainer" style="height: 300px; width: 460px;"></div>
        </div>
    </div>--%>
    <%--<div class="block" style="width: 460px !important; display: none;">
        <div class="title main-title"><%= (Session["lang"].ToString() == "0") ? "Latest Tasks" : "احدث المهمات"%></div>
        <div class="content" style="overflow: hidden !important">
            <div id="tbllatestasks" runat="server">
                <table style="width: 100%">
                    <thead>
                        <tr>
                            <th><%= (Session["lang"].ToString() == "0") ? "Task" : "المهمة"%></th>
                            <th><%= (Session["lang"].ToString() == "0") ? "Date" : "تاريخ الانجاز"%></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView ID="lstLatestTasks" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <th><%# Eval("TaskName") %></th>
                                    <th><%# Eval("TaskDate") %></th>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </tbody>
                </table>
            </div>
            <asp:Panel ID="Panel1" runat="server" Style="text-align: center; padding-top: 10%;">
                <asp:Label ID="lbltable" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </div>
    </div>--%>
    <script src="/Assets/UIKIT/js/modules/dahboard-module.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">

</asp:Content>
