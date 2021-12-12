<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="AddToDoList.aspx.cs" Inherits="dms.Screen.AddToDoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ajax__combobox_itemlist {
            text-align: center !important;
        }

        .dropdown-main {
            height: 35px;
        }

        .comment-item {
            border-style: none !important;
            border-color: #ffffff !important;
        }

        .repeart-days {
            padding: 0;
            margin: 0;
            list-style: none;
            display: flex;
        }

            .repeart-days li.active {
                background: rgba(var(--main-color), 1);
                color: #fff;
            }

            .repeart-days li {
                display: inline-block;
                padding: 2px 0;
                margin: 0 6px;
                border-radius: 22px;
                cursor: pointer;
                background: #f1f1f1;
                transition: 0.3s ease;
                width: 39px;
                height: 39px;
                display: flex;
                align-items: center;
                justify-content: center;
            }

        .required {
            display: none;
        }

            .required[style*=visible] + input,
            .required[style*=visible] + select,
            .required[style*=visible] + textarea {
                background-color: #ffcccc;
                border: 1px solid #ff0000;
            }

            .required[style*=inline] + input,
            .required[style*=inline] + select,
            .required[style*=inline] + textarea {
                background-color: #ffcccc;
                border: 1px solid #ff0000;
            }
            .export-holder {
            position: relative;
            color: #7a7a7a;
            background: #fff;
            border: 1px solid #eee;
            border-radius: 70px;
            overflow: hidden;
            padding: 0 15px;
            height: 40px;
        }

            .export-holder .icon-down {
                position: absolute;
                left: 14px;
                top: 16px;
                font-size: 11px;
            }

            .export-holder .icon-export {
                position: absolute;
                right: 13px;
                top: 12px;
            }

            .export-holder select {
                padding: 7px 25px;
                appearance: none;
                outline: none;
                border: none;
                height: 100%;
            }

        .completed-task {
            text-decoration: line-through;
        }

        .notcompleted-task {
            text-decoration: none;
        }

        .expand-modal {
            display: none;
        }

        .modal-dialog {
            margin: 0px 0px !important;
        }

        .comment-item {
            border-style: none !important;
            border-color: #ffffff !important;
        }

        .modal-title {
            font-weight: bold;
            color: rgba(var(--main-color), 1);
            font-size: 15px;
            margin-inline-end: auto;
        }

        .btn-done-model {
            background: rgba(var(--main-color), 1);
            border: none;
            outline: none;
            color: #fff;
            margin-inline-end: auto;
            padding: 5px 30px;
            border-radius: 20px;
            float: right;
        }

        .btn-close-model {
            border: none;
            outline: none;
            background: transparent;
            display: flex;
            color: #7c7c7c;
            float: left;
        }

        .icon-close {
            background: #e9e9e9;
            width: 20px;
            display: inline-block;
            height: 20px;
            border-radius: 50px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 7px;
        }

        .modal-header {
            border-bottom: 0px solid #ffffff;
        }

        .modal-footer {
            border-top: 0px solid #ffffff;
        }
        .modal-dialog {
            margin: 0px 0px !important;
        }
         .modal-header {
            border-bottom: 0px solid #ffffff;
        }

        .modal-footer {
            border-top: 0px solid #ffffff;
        }
          .dropdown-main {
            height: 35px;
        }

        .FixedHeader {
            background: #F9F9F9;
            height: 50px;
        }

            .FixedHeader th {
                padding-top: 15px !important;
            }

        .my-table tbody tr th a, .my-table tbody tr th a:active {
            text-decoration: none;
            text-align: center;
            padding: 15px 5px;
            color: #8F9198;
            cursor: pointer;
            transition: 0.3s ease;
            border: none;
            outline: none;
        }

        .my-table tbody tr th .btn-sort-table {
            cursor: pointer;
            background: #fcfcfc;
            width: 20px;
            display: inline-block;
            border-radius: 20px;
            margin: 0 1px;
            transition: 0.3s ease;
        }

            .my-table tbody tr th .btn-sort-table svg {
                width: 10px;
                height: 10px;
            }

            .my-table tbody tr th .btn-sort-table path {
                fill: #000;
                stroke: #131313;
                transition: 0.3s ease;
                stroke-width: 1px;
            }

        .my-table tbody tr th:hover .btn-sort-table {
            background: #007aff;
        }

            .my-table tbody tr th:hover .btn-sort-table path {
                fill: #fff;
                stroke: #fff;
                stroke-width: 2px;
            }
    </style>
    <script type="text/javascript">
        var lang =<%= (Session["lang"].ToString() == "0") ? "'en'" : "'ar'"%>;
        function checkDate() {
            var dateString = $("#ContentPlaceHolder1_txtDate").val(); // Oct 23
            if (dateString != "") {
                var dateObject = new Date(dateString);
                var todatDate = new Date();
                if (todatDate >= dateObject) {
                    if (lang == 'en')
                        $("#ContentPlaceHolder1_lblResult").html("The date of the task must be greater than today's date");
                    else
                        $("#ContentPlaceHolder1_lblResult").html('تاريخ المهمه لابد  ان يكون اكبر من تاريخ اليوم');
                    return false;
                }
                else {
                    // $("#ContentPlaceHolder1_lblResult").html('');
                    return true;
                }
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <ul class="pages_nav">
            <li><a id="lnkToDoList" runat="server" href="ToDoList.aspx">قائمة المهام</a></li>
            <li><a id="txtToDoAction" runat="server">إضافة مهمة جديدة</a></li>
        </ul>

        <div class="white-holder">

            <div class="max-width-holder">


                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_TaskName %></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtTaskName"
                            ErrorMessage="*"
                            ForeColor="Red" CssClass="required">
                        </asp:RequiredFieldValidator>
                        <asp:TextBox class="main-input" ID="txtTaskName" runat="server" />
                    </div>
                    <div class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_Addressedto %></label>
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator5"
                            runat="server" ErrorMessage="*"
                            InitialValue="-1" ForeColor="Red"
                            ControlToValidate="ddlAssignTo" CssClass="required"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="dropdown-main dropdown">
                        </asp:DropDownList>
                    </div>
                    <div class="main-field-holder">
                        <label class="main-label lable-date"><%= HudhudResources.Resources.Screen_ToDoList_CompletionDate %></label>
                        <div class="main-field-holder main-field-holder-two">
                            <%--          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtDate"
                                ErrorMessage="*"
                                ForeColor="Red" CssClass="required">
                            </asp:RequiredFieldValidator>--%>
                            <input id="txtDate" runat="server" class="main-input" type="date" />
                            <input id="txtTime" runat="server" class="main-input" type="time" value="12:00:00" />
                        </div>
                    </div>
                    <div class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_Repetition %></label>
                        <asp:DropDownList ID="ddlRepeatType" runat="server" CssClass="dropdown-main dropdown">
                        </asp:DropDownList>
                    </div>
                    <div id="divRepeatDays" style="display: none;" class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_weekdays %></label>
                        <ul class="repeart-days">
                            <asp:Repeater ID="rptWeekDays" runat="server">
                                <ItemTemplate>
                                    <li class='<%# Eval("class") %>' data-day='<%# Eval("WeekDay") %>' title='<%# Eval("WeekDayName") %>'><%# Eval("WeekDayFirstChar") %></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <input type="hidden" id="hRepeatWeekDays" runat="server" />
                    </div>
                    <%-- <div class="main-field-holder for-repeat" style="display: none">
                        <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Start Date" : ""%></label>
                        <div class="main-field-holder main-field-holder-two">
                            <input id="txtStartDate" runat="server" class="main-input" type="date" />
                            <input id="txtStartTime" runat="server" class="main-input" type="time" value="12:00:00" />
                        </div>
                    </div>--%>
                </div>

                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_prioritylevel %></label>
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator4"
                            runat="server" ErrorMessage="*"
                            InitialValue="-1" ForeColor="Red"
                            ControlToValidate="ddlTaskType" CssClass="required"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlTaskType" runat="server" CssClass="dropdown-main dropdown">
                        </asp:DropDownList>
                    </div>
                    <div class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_thedetails %></label>
                        <asp:TextBox ID="txtDescription" Rows="7" TextMode="MultiLine" runat="server" class="main-textarea textarea-lg-two"></asp:TextBox>
                    </div>
                    <div class="main-field-holder" runat="server" id="divIsFinished">
                        <label class="main-label"></label>
                        <div class="radio-input-holder">
                            <asp:CheckBox ID="chCompleted" runat="server" />
                            <label for="done"><%= HudhudResources.Resources.Screen_ToDoList_Ending %></label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="control-side-holder control-side-holder-footer">
                <div class="start-side">
                    <a class="btn-main" id="btnSave" runat="server" onserverclick="Button1_Click" onclick="return CheckRepeat();">
                        <div class="btn-main-wrapper">
                            <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                                <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                                <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                                <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                                <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                            </svg>
                            <%= HudhudResources.Resources.Admin_WorkFlow_save %>
                        </div>
                    </a>

                    <a class="btn-main btn-white" id="btnUndo" runat="server" onserverclick="Button2_Click" causesvalidation="false">
                        <div class="btn-main-wrapper">
                            <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg" width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                <g id="Group_2175" data-name="Group 2175">
                                    <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244" r="11.244" fill="#f4f4f4"></circle>
                                    <g id="Group_2166" data-name="Group 2166" transform="translate(7.496 7.496)">
                                        <line id="Line_28" data-name="Line 28" y2="11.745" transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                        <line id="Line_29" data-name="Line 29" x2="11.745" transform="translate(0) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                    </g>
                                </g>
                            </svg>
                            <%= HudhudResources.Resources.Screen_ToDoList_Retreat %>
                        </div>
                    </a>
                </div>
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                <div class="end-side">


                    <a class="btn-main" id="btnDelete" runat="server" onclick="showToDoListRemoveModal();">
                        <div class="btn-main-wrapper">
                            <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                                <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                            </svg>
                            <%= HudhudResources.Resources.Admin_WorkFlow_Survey %>
                        </div>
                    </a>
                </div>

            </div>

            <div id="divCommentSection" runat="server" class="max-width-holder" style="padding-top: 20px;">
                <div class="modal-body modal-body-padding" style="width: 100%">
                    <div class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_addacomment %></label>
                        <textarea id="txtComment" runat="server" class="main-textarea textarea-lg-two"></textarea>
                    </div>


                    <div class="left-btn-holder">
                        <a id="btnAddComment" runat="server" onserverclick="btnAddComment_Click" class="btn-main" data-dismiss="modal">
                            <div class="btn-main-wrapper">
                                <svg xmlns="http://www.w3.org/2000/svg" width="17.133" height="16.419" viewBox="0 0 17.133 16.419">
                                    <g id="Group_2656" data-name="Group 2656" transform="translate(568.001 -23.737)">
                                        <g id="Group_2865" data-name="Group 2865" transform="translate(-232.953 29.099)">
                                            <path id="Path_7092" data-name="Path 7092" d="M-148.38,370.362v3.311a.536.536,0,0,0,.368.509.54.54,0,0,0,.167.026.535.535,0,0,0,.431-.218l1.937-2.636Z" transform="translate(-180.421 -363.152)" fill="#fff" />
                                            <path id="Path_7093" data-name="Path 7093" d="M-318.14-5.263A.536.536,0,0,0-318.7-5.3L-334.76,3.086a.537.537,0,0,0-.286.515.536.536,0,0,0,.36.466l4.465,1.526,9.509-8.131-7.358,8.865,7.483,2.558a.548.548,0,0,0,.173.029.538.538,0,0,0,.278-.078.538.538,0,0,0,.251-.378l1.963-13.206A.536.536,0,0,0-318.14-5.263Z" fill="#fff" />
                                        </g>
                                    </g>
                                </svg>
                                <%= HudhudResources.Resources.Screen_ToDoList_send %>
                            </div>
                        </a>
                    </div>
                    <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_Comments %></label>
                    <asp:GridView ID="gvTaskListComments" runat="server" CssClass="comment-item"
                        OnRowCommand="gvTaskListComments_OnRowCommand" Width="100%"
                        AutoGenerateColumns="false" DataKeyNames="Id" EmptyDataText="<%# HudhudResources.Resources.Screen_ToDoList_nocomments %>">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="comment-item">
                                        <div class="comment-info">

                                            <p class="comment-name">
                                                <img src='<%# "/Images/Users/" + Eval("CreatedBy") + ".png" %>' style="width: 40px">
                                                '<%# GetUserName(Convert.ToInt32(Eval("CreatedBy"))) %>'
                                            </p>
                                            <p class="comment-date">
                                                <%# Eval("CreatedOn") %>
                                            </p>
                                            <div class="dropdown">
                                                <asp:LinkButton ID="lnkBtnDeleteComment" runat="server" CommandName="deletethisrow"
                                                    CssClass="button" CommandArgument='<%# Eval("Id") %>'
                                                    Visible='<%# IsThisUserCreate(Eval("CreatedBy"), Eval("IsDeleted")) %>'>
                                                    <i class="fas fa-trash-alt"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <p class="comment-msg">
                                            '<%# IsDeleted(Eval("IsDeleted")) ? HudhudResources.Resources.Screen_ToDoList_Commentdeleted : Eval("CommentText") %>'
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <span id="spanEnd" runat="server"></span>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hCommentToDoListId" runat="server" />
    <dialog id="tr-remove" style="border: none; padding: 0px 0px;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= HudhudResources.Resources.Screen_ToDoList_Areyousurethedeletion %></h4>
                </div>
                <!-- <div class="modal-body">
                    </div> -->
                <div class="modal-footer">
                    <button type="button" class="btn-done-model" id="btnDeleteTask" runat="server" onserverclick="btnDelete_ServerClick"><%= HudhudResources.Resources.Screen_ToDoList_Yeah %></button>
                    <button type="button" class="btn-close-model" onclick="document.getElementById('tr-remove').close();">
                        <span class="icon-close">
                            <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963"
                                viewBox="0 0 11.963 11.963">
                                <g id="Group_21" data-name="Group 21"
                                    transform="translate(5.981 -3.153) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)"
                                        fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2">
                                    </line>
                                    <line id="Line_29" data-name="Line 29" x2="12.918"
                                        transform="translate(0 6.459)" fill="none" stroke="#fff"
                                        stroke-linecap="round" stroke-width="2">
                                    </line>
                                </g>
                            </svg>
                        </span>
                        <%= HudhudResources.Resources.Screen_ToDoList_Retreat %>
                    </button>
                </div>
            </div>
        </div>
    </dialog>
    <dialog id="tr-removeComment" style="border: none; padding: 0px 0px;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= HudhudResources.Resources.Screen_ToDoList_Areyousurethedeletion %></h4>
                </div>
                <!-- <div class="modal-body">
                    </div> -->
                <div class="modal-footer">
                    <button type="button" class="btn-done-model" id="btnDeleteComment" runat="server" onserverclick="btnDeleteComment_ServerClick"><%= HudhudResources.Resources.Screen_ToDoList_Yeah %></button>
                    <button type="button" class="btn-close-model" onclick="document.getElementById('tr-removeComment').close();">
                        <span class="icon-close">
                            <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963"
                                viewBox="0 0 11.963 11.963">
                                <g id="Group_21" data-name="Group 21"
                                    transform="translate(5.981 -3.153) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)"
                                        fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2">
                                    </line>
                                    <line id="Line_29" data-name="Line 29" x2="12.918"
                                        transform="translate(0 6.459)" fill="none" stroke="#fff"
                                        stroke-linecap="round" stroke-width="2">
                                    </line>
                                </g>
                            </svg>
                        </span>
                        <%= HudhudResources.Resources.Screen_ToDoList_Retreat %>
                    </button>
                </div>
            </div>
        </div>
    </dialog>
    <script>
        function RepeatTypeChanged(tvalue) {
            var divRepeatDays = document.getElementById("divRepeatDays");
            if (tvalue == "WeekDays")
                divRepeatDays.style.display = "block";
            else
                divRepeatDays.style.display = "none";
            if ($("#ContentPlaceHolder1_ddlRepeatType").val() != "") {
                $(".for-repeat").show();
            }
            else {
                $(".for-repeat").hide();
            }
            CheckRepeat();
        }
        $('.repeart-days li').on("click", function (e) {
            $(this).toggleClass('active');
            var weekday = $(this).attr("data-day")
            var hRepeatWeekDays = document.getElementById('<%= hRepeatWeekDays.ClientID %>');
            if ($(this).hasClass('active')) {
                hRepeatWeekDays.value = hRepeatWeekDays.value + weekday + ',';
            }
            else {
                hRepeatWeekDays.value = hRepeatWeekDays.value.replace(weekday + ',', "");
            }
        });
        function CheckRepeat() {
            if ($("#ContentPlaceHolder1_ddlRepeatType").val() == "") {
               // $$(".lable-date").html(lang == "ar" ? "تاريخ الإنجاز" : "Completion Date")
                var msg = lang == "ar" ? "تاريخ الإنجاز" : "Completion Date";
                $(".lable-date").html(msg)
                return true;
            }
            else {
                var msg = lang == "ar" ? "تاريخ البدء" : "Start Date";
                $(".lable-date").html(msg)
                if ($("#ContentPlaceHolder1_txtDate").val() == "") {
                    $("#ContentPlaceHolder1_txtDate").css("border-color", "red");
                    return false;
                }
                if ($("#ContentPlaceHolder1_txtTime").val() == "") {
                    $("#ContentPlaceHolder1_txtTime").css("border-color", "red");
                    return false;
                }
                return true;
            }
        }
        function showToDoListRemoveModal() {
            document.getElementById("tr-remove").showModal();
        }
 function showCommentToDoListRemoveModal() {
            document.getElementById("tr-removeComment").showModal();
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
