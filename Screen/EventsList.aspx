<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="EventsList.aspx.cs" Inherits="dms.Screen.EventsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Screen/fullcalendar/lib/moment.min.js"></script>
    <style>
        .disabled {
            border: 1px solid #999999;
            background-color: #cccccc;
            color: #666666;
            pointer-events: none;
        } 
        .modal-header
        {
            border-bottom:none;
        }
        .modal-footer
        {
            border-top:none;
        }
        .modal-title
        {
            color:rgba(var(--main-color), 1);
        }
        .btn-dialog-alert-done {
            background: rgba(var(--main-color), 1);
            border: none;
            outline: none;
            color: #fff;
            margin-inline-end: auto;
            padding: 5px 30px;
            border-radius: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input type="hidden" id="createdByHidden" value="<%= Session["userId"].ToString() %>" />
    <div class="page-content">
        <ul class="pages_nav">
            <li><a href="#"><%= HudhudResources.Resources.EventsList_Events %></a></li>
        </ul>
        <div class="white-holder">
            <a class="btn-main" onclick="openAddEvent();">
                <div class="btn-main-wrapper">
                    <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                        <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                    </svg>
                    <%= HudhudResources.Resources.EventsList_AddNew %>
                </div>
            </a>
            <div id='calendar' class="calender-events-page"></div>
        </div>
    </div>

    <!-- Modal add event  -->
    <div class="modal fade my-modal my-modal-lg" id="add-event-modal" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= HudhudResources.Resources.EventsList_AddNewEvent %></h4>
                </div>
                <div class="modal-body modal-body-padding">
                    <div class="row">
                        <input type="hidden" id="createdByHidden" value="<%= Session["userId"].ToString() %>" />
                        <input type="hidden" id="eventId" />
                        <div class="col-xs-6">
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.EventsList_EventTitle %></label>
                                <input class="main-input for-event" id="addEventName" type="text">
                            </div>
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.EventsList_EventBeginDate %></label>
                                <div class="main-field-holder main-field-holder-two">
                                    <input class="main-input for-event" id="txtaddeventStart" type="date">
                                    <input class="main-input for-event" id="txtaddeventStartTime" type="time">
                                </div>
                            </div>
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.EventsList_EventEndDate %></label>
                                <div class="main-field-holder main-field-holder-two">
                                    <input class="main-input for-event" id="txtaddeventEnd" type="date">
                                    <input class="main-input for-event" id="txtaddeventEndTime" type="time">
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-6">
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.EventsList_details %></label>
                                <textarea id="addEventDesc" class="main-textarea textarea-lg-two for-event"></textarea>
                            </div>

                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.EventsList_color %></label>
                                <input class="main-input for-event" id="txtaddcolor" type="color" value="#000000">
                            </div>
                        </div>
                        <div class="col-xs-6 for-todo">
                            <div class="main-field-holder">
                                <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Mark as done" : "اكمال المهمة"%></label>
                                <%--   <textarea id="addEventDesc" class="main-textarea textarea-lg-two "></textarea>--%>
                                <input id="chkDone" onclick="markAsDone();" type="checkbox" />
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="start-side">
                        <a class="btn-main btn-add-calendar" onclick="addCalendarEvent();">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg"
                                    width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                    <path id="Path_7052" data-name="Path 7052"
                                        d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z"
                                        transform="translate(441.623 -26.003)" fill="#fff">
                                    </path>
                                    <path id="Path_7053" data-name="Path 7053"
                                        d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z"
                                        transform="translate(262.872 -26.003)" fill="#fff">
                                    </path>
                                    <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z"
                                        transform="translate(441.623 -366.989)" fill="#fff">
                                    </path>
                                    <path id="Path_7055" data-name="Path 7055"
                                        d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z"
                                        transform="translate(565.001 -26.003)" fill="#fff">
                                    </path>
                                    <path id="Path_7056" data-name="Path 7056"
                                        d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z"
                                        transform="translate(441.623 -304.815)" fill="#fff">
                                    </path>
                                </svg>
                                <%= HudhudResources.Resources.EventsList_save %>
                            </div>
                        </a>
                        <a class="btn-main btn-edit-calendar" style="display: none;" onclick="editCalendarEvent();">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg"
                                    width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                                    <path id="Path_7052" data-name="Path 7052"
                                        d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z"
                                        transform="translate(441.623 -26.003)" fill="#fff">
                                    </path>
                                    <path id="Path_7053" data-name="Path 7053"
                                        d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z"
                                        transform="translate(262.872 -26.003)" fill="#fff">
                                    </path>
                                    <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z"
                                        transform="translate(441.623 -366.989)" fill="#fff">
                                    </path>
                                    <path id="Path_7055" data-name="Path 7055"
                                        d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z"
                                        transform="translate(565.001 -26.003)" fill="#fff">
                                    </path>
                                    <path id="Path_7056" data-name="Path 7056"
                                        d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z"
                                        transform="translate(441.623 -304.815)" fill="#fff">
                                    </path>
                                </svg>
                                <%= HudhudResources.Resources.EventsList_save %>
                            </div>
                        </a>
                        <a class="btn-main btn-white" data-dismiss="modal">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg"
                                    width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                    <g id="Group_2175" data-name="Group 2175">
                                        <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244"
                                            r="11.244" fill="#f4f4f4">
                                        </circle>
                                        <g id="Group_2166" data-name="Group 2166"
                                            transform="translate(7.496 7.496)">
                                            <line id="Line_28" data-name="Line 28" y2="11.745"
                                                transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198"
                                                stroke-linecap="round" stroke-width="2">
                                            </line>
                                            <line id="Line_29" data-name="Line 29" x2="11.745"
                                                transform="translate(0) rotate(45)" fill="none" stroke="#8f9198"
                                                stroke-linecap="round" stroke-width="2">
                                            </line>
                                        </g>
                                    </g>
                                </svg>

                                <%= HudhudResources.Resources.EventsList_Retreat %>
                            </div>
                        </a>
                    </div>

                    <div class="end-side">
                        <a class="btn-main btn-delete-calendar" style="display: none;">
                            <div class="btn-main-wrapper">
                                <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg"
                                    width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                    <path id="Path_7057" data-name="Path 7057"
                                        d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z"
                                        transform="translate(-63.122 -124.487)" fill="#fff">
                                    </path>
                                    <path id="Path_7058" data-name="Path 7058"
                                        d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)"
                                        fill="#fff">
                                    </path>
                                </svg>
                                <%= HudhudResources.Resources.EventsList_delete %>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="dialog-alert" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="dialog-alert-msg"></h4>
                </div>
                <!-- <div class="modal-body">
                            </div> -->
                <div class="modal-footer">
                    <button type="button" class="btn-dialog-alert-done" data-dismiss="modal"><%= (Session["lang"].ToString() == "0") ? "Ok" : "نعم"%></button>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    <script src="/Assets/UIKIT/js/modules/events-module.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
