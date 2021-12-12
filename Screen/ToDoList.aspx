<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ToDoList.aspx.cs" Inherits="dms.Screen.ToDoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <link href="../Assets/bootstrap-5.1.3-dist/css/bootstrap.css" rel="stylesheet" />
    <link href="../Assets/bootstrap-5.1.3-dist/css/build.css" rel="stylesheet" />--%>
    <style type="text/css">
        .my-table tbody tr .Center {
            text-align:center;
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
    <style>
        .FixedHeader {
            background: #F9F9F9 !important;
            border-top: 0px solid transparent !important;
        }

            .FixedHeader th {
                border-top: 0px solid transparent !important;
            }

        .GridPager td {
            padding: 5px 0px !important;
        }

        .GridPager a, .GridPager span {
            width: 30px;
            height: 30px;
            display: inline-block;
            padding: 5px 12px;
            margin-right: 4px;
            border-radius: 20px;
            border: solid 1px #c0c0c0;
            background: #e9e9e9;
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
        }

        .GridPager a {
            background-color: transparent !important;
            color: #337ab7 !important;
            border: 0px solid #ffffff;
        }

        .GridPager span {
            background: #337ab7;
            color: #ffffff !important;
            border: 1px solid #3AC0F2;
        }

        .custom-checkbox .custom-control-label input[type=checkbox] ::before {
  border-radius: 999px;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Modal search task -->
    <div class="modal fade my-modal my-modal-lg" id="search-tasks" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= HudhudResources.Resources.Screen_ToDoList_Thesearch %></h4>
                </div>
                <div class="modal-body modal-body-padding">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_themission %></label>
                                <input id="txtTask" runat="server" class="main-input" type="text" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_Date %></label>
                                <input id="txtDate" runat="server" class="main-input" type="date" />
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_Addressedto %></label>
                                <asp:DropDownList ID="ddlAssignTo" CssClass="dropdown-main dropdown" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-4">
                            <div class="main-field-holder">
                                <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_Case %></label>
                                <asp:RadioButtonList
                                    OnSelectedIndexChanged="Button2_Click" CssClass="radio-input-holder"
                                    ID="rblStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" RepeatLayout="Flow">
                                    <asp:ListItem Value="-1">الكل</asp:ListItem>
                                    <asp:ListItem Value="0">القائمة</asp:ListItem>
                                    <asp:ListItem Value="1">المنتهي</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSearch" runat="server" onserverclick="Button2_Click" type="button" class="btn-done-model">نعم</button>
                    <button type="button" class="btn-close-model" data-dismiss="modal">
                        <span class="icon-close">
                            <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                    <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                </g>
                            </svg>
                        </span>
                        تراجع
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="white-holder tab-pane active" id="edit-tasks" style="border-radius: 0px;">
        <div class="control-side-holder">
            <div class="start-side">
                <a id="btnAddNew" runat="server" onserverclick="Button1_Click" class="btn-main">
                    <div class="btn-main-wrapper">
                        <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                            <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                        </svg>
                        <%= HudhudResources.Resources.Screen_ToDoList_AddNew %><span id="spanAddNew" runat="server"></span>
                    </div>
                </a>



                <a class="btn-main" data-toggle="modal" data-target="#search-tasks">
                    <div class="btn-main-wrapper">
                        <svg xmlns="http://www.w3.org/2000/svg" width="17.293" height="17.293" viewBox="0 0 17.293 17.293">
                            <g id="search" transform="translate(0.5 0.5)">
                                <circle id="Oval" cx="7.149" cy="7.149" r="7.149" fill="none" stroke="#fff" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1"></circle>
                                <path id="Path" d="M3.888,3.888,0,0" transform="translate(12.199 12.199)" fill="none" stroke="#fff" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1"></path>
                            </g>
                        </svg>
                        <%= HudhudResources.Resources.Screen_ToDoList_Search %>
                    </div>
                </a>
            </div>

            <div class="end-side">

                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropdown-main dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Text="عرض 5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="عرض 10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="عرض 20" Value="20"></asp:ListItem>
                </asp:DropDownList>


<%--                  <div class="dropdown-main dropdown">
                    <div id="drop2" class="btn-dropdown-holder" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="dropdown-title"><%= HudhudResources.Resources.Screen_ToDoList_filtering %></span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="12.204" height="7.118" viewBox="0 0 12.204 7.118">
                            <g id="Group_3106" data-name="Group 3106" transform="translate(11.704 0.556) rotate(90)">
                                <g id="Group_2125" data-name="Group 2125">
                                    <path id="Path_6981" data-name="Path 6981" d="M5.88,5.268.732.12A.429.429,0,0,0,.126.727L4.97,5.571.126,10.416a.429.429,0,1,0,.607.607L5.88,5.875A.429.429,0,0,0,5.88,5.268Z" fill="#8f9198" stroke="#8f9198" stroke-width="1"></path>
                                </g>
                            </g>
                        </svg>

                    </div>

                  <ul class="dropdown-menu" aria-labelledby="drop2">
                        <li><%= HudhudResources.Resources.Screen_ToDoList_Bydate %></li>
                        <li><%= HudhudResources.Resources.Screen_ToDoList_Bygenre %></li>
                    </ul>
                </div>--%>
                <div class="export-holder">
                    <i class="fas fa-filter"></i>
                    <asp:DropDownList ID="drpFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFilter_SelectedIndexChanged">
                        <asp:ListItem Value="all">عرض جميع المهام</asp:ListItem>
                        <asp:ListItem Value="0">عرض المهام القائمة</asp:ListItem>
                        <asp:ListItem Value="1">عرض المهام المنتهية</asp:ListItem>
                    </asp:DropDownList>
                    <i class="fas fa-chevron-down"></i>
                </div>

                <div class="export-holder" style="display: none;">
                    <i class="fas fa-chevron-down icon-down"></i>
                    <asp:DropDownList ID="ddlExport" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExport_ServerChange">
                        <asp:ListItem Value="">تصدير</asp:ListItem>
                        <asp:ListItem Value="PDF">PDF</asp:ListItem>
                        <asp:ListItem Value="EXCEL">EXCEL</asp:ListItem>
                    </asp:DropDownList>
                    <i class="fas fa-chevron-down"></i>
                    <%--<i class="fas fa-file-export icon-export"></i>--%>
                </div>
                <div class="dropdown-main dropdown">
                    <svg xmlns="http://www.w3.org/2000/svg" width="12.728" height="16.477" viewBox="0 0 12.728 16.477" style="margin-inline-start: 11px;">
                        <g id="surface1" transform="translate(-58.885 0.998)">
                            <path style="fill: #6c6c6c;" id="Path_7050" data-name="Path 7050" d="M338.5,19.06h2.14l-3.269-3.43v2.306A1.123,1.123,0,0,0,338.5,19.06Z" transform="translate(-269.502 -16.092)" fill="#fff"></path>
                            <path style="fill: #6c6c6c;" id="Path_7051" data-name="Path 7051" d="M67.167,1.844V-1h-6.7A1.594,1.594,0,0,0,58.885.584V13.9a1.594,1.594,0,0,0,1.587,1.582h9.555A1.594,1.594,0,0,0,71.613,13.9V3.666h-2.62A1.822,1.822,0,0,1,67.167,1.844Zm-4.708,5.68a.35.35,0,0,1,.493.017L64.9,9.634V4.656a.349.349,0,1,1,.7,0V9.638l1.949-2.092a.349.349,0,1,1,.51.475L65.5,10.763a.349.349,0,0,1-.257.109.356.356,0,0,1-.257-.109L62.433,8.021A.365.365,0,0,1,62.459,7.524Zm6.726,5.3a.35.35,0,0,1-.349.349H61.662a.349.349,0,0,1,0-.7h7.179A.346.346,0,0,1,69.185,12.82Z" fill="#fff"></path>
                        </g>
                    </svg>
                    <div id="drop2" class="btn-dropdown-holder" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="dropdown-title"><%= HudhudResources.Resources.Screen_ToDoList_Export %></span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="12.204" height="7.118" viewBox="0 0 12.204 7.118">
                            <g id="Group_3106" data-name="Group 3106" transform="translate(11.704 0.556) rotate(90)">
                                <g id="Group_2125" data-name="Group 2125">
                                    <path id="Path_6981" data-name="Path 6981" d="M5.88,5.268.732.12A.429.429,0,0,0,.126.727L4.97,5.571.126,10.416a.429.429,0,1,0,.607.607L5.88,5.875A.429.429,0,0,0,5.88,5.268Z" fill="#8f9198" stroke="#8f9198" stroke-width="1"></path>
                                </g>
                            </g>
                        </svg>
                    </div>
                    <ul class="dropdown-menu" aria-labelledby="drop2">
                        <li><a id="btnExportPDF" runat="server" onserverclick="btnExportPDF_ServerClick">PDF</a></li>
                        <li><a id="btnExportEXCEL" runat="server" onserverclick="btnExportEXCEL_ServerClick">EXCEL</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <asp:GridView ID="gvTaskLists" runat="server" CssClass="table my-table"
            PageSize="5"
            AllowSorting="True"
            OnSorting="gridView_Sorting"
            OnRowCommand="gvTaskLists_OnRowCommand" OnRowDataBound="gvTaskLists_RowDataBound"
            AllowPaging="True" DataKeyNames="Id"
            AutoGenerateColumns="False" OnPageIndexChanging="gvTaskLists_PageIndexChanging" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
            <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center"></HeaderStyle>
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <Columns>
                <asp:TemplateField SortExpression="IsComplete" ItemStyle-CssClass="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkIsComplete" runat="server" CommandName="Sort" CommandArgument="IsComplete"><%# HudhudResources.Resources.Screen_ToDoList_Ending %> ؟
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="custom-control custom-checkbox">
                            <asp:CheckBox AutoPostBack="true" CssClass="custom-control-input" Checked='<%# Eval("IsComplete") %>'
                                    OnCheckedChanged="gvTaskLists_CheckedChanged" 
                                    ID="CheckBox1" runat="server" CommandArgument='<%# Eval("Id") %>' />

                              <label class="custom-control-label" for="customCheck3">
        
                                </label>
                            </div>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="TaskName">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkTaskName" runat="server" CommandName="Sort" CommandArgument="TaskName"><%# HudhudResources.Resources.Screen_ToDoList_TaskName %>
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hTaskName" runat="server" Value='<%# Eval("TaskName") %>' />
                        <span title="<%# Eval("Description") %>" class='<%# Convert.ToBoolean(Eval("IsComplete"))?"completed-task":"notcompleted-task" %>'><%# Eval("TaskName") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="TaskDate" ItemStyle-CssClass="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkTaskDate" runat="server" CommandName="Sort" CommandArgument="TaskDate"><%# HudhudResources.Resources.Screen_ToDoList_Dateofmission %>
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class='<%# Convert.ToBoolean(Eval("IsComplete"))?"completed-task":"notcompleted-task" %>'><%# DateTime.Parse(Eval("TaskDate").ToString()).ToString("dd/MM/yyyy") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="TaskDate" ItemStyle-CssClass="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkTaskTime" runat="server" CommandName="Sort" CommandArgument="TaskDate"><%# HudhudResources.Resources.Screen_ToDoList_Time %>
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class='<%# Convert.ToBoolean(Eval("IsComplete"))?"completed-task":"notcompleted-task" %>'><%# Eval("TaskTime") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="AssignTo" ItemStyle-CssClass="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkAssignTo" runat="server" CommandName="Sort" CommandArgument="AssignTo"><%# HudhudResources.Resources.Screen_ToDoList_Addressedto %>
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class='<%# Convert.ToBoolean(Eval("IsComplete"))?"completed-task":"notcompleted-task" %>'> <%# Eval("AssignTo") %> </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="CreatedBy" ItemStyle-CssClass="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkCreatedBy" runat="server" CommandName="Sort" CommandArgument="CreatedBy"><%# HudhudResources.Resources.Screen_ToDoList_Createdfrom %>
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class='<%# Convert.ToBoolean(Eval("IsComplete"))?"completed-task":"notcompleted-task" %>'> <%# Eval("CreatedBy") %> </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="TaskType" ItemStyle-CssClass="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkTaskType" runat="server" CommandName="Sort" CommandArgument="TaskType"><%# HudhudResources.Resources.Screen_ToDoList_prioritylevel %>
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <span class='<%# Convert.ToBoolean(Eval("IsComplete"))?"completed-task":"notcompleted-task" %>'>  <%# Eval("TaskType") %> </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="NumberOfComments" ItemStyle-CssClass="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkNumberOfComments" runat="server" CommandName="Sort" CommandArgument="NumberOfComments"><%# HudhudResources.Resources.NumberOfComments %>
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <span class='<%# Convert.ToBoolean(Eval("IsComplete"))?"completed-task":"notcompleted-task" %>'>   <%# Eval("NumberOfComments") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hCreatedUserID" runat="server" Value='<%# Eval("CreatedUserID") %>' />
                        <div class="tr-btn-holder">
                            <a id="btnShowComment" runat="server" onserverclick="btnShowComment_ServerClick" class="tr-comment">
                                <svg xmlns="http://www.w3.org/2000/svg" width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                    <g id="Group_3169" data-name="Group 3169" transform="translate(-145 -599.019)">
                                        <g id="Group_3168" data-name="Group 3168" transform="translate(145 599.019)">
                                            <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244" r="11.244" fill="#ebebeb" />
                                        </g>
                                        <path id="Path_7154" data-name="Path 7154" d="M-298.893,46h-8.242A2.869,2.869,0,0,0-310,48.865v4.426A2.819,2.819,0,0,0-307.973,56l1.384,1.384a.41.41,0,0,0,.29.12.41.41,0,0,0,.289-.12l1.27-1.27h5.847a2.869,2.869,0,0,0,2.866-2.865V48.865A2.869,2.869,0,0,0-298.893,46Zm-7.129,5.782a.409.409,0,0,1,.409-.409h5.2a.409.409,0,0,1,.409.409.409.409,0,0,1-.409.409h-5.2A.409.409,0,0,1-306.022,51.782Zm5.608-1.038h-5.2a.409.409,0,0,1-.409-.409.409.409,0,0,1,.409-.409h5.2a.409.409,0,0,1,.409.409A.409.409,0,0,1-300.414,50.744Z" transform="translate(459.794 558.84)" fill="#8f9198" />
                                    </g>
                                </svg>

                            </a>
                            <a id="btnEdit" runat="server" class="tr-edit" href='<%# "AddToDoList.aspx?view=true&id=" + Eval("Id") + (Request.QueryString["docID"] != null ? "&docID=" + Request.QueryString["docID"] : "") %>'>
                                <svg xmlns="http://www.w3.org/2000/svg" width="22.506" height="22.506" viewBox="0 0 22.506 22.506">
                                    <path id="Path_6947" data-name="Path 6947" d="M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z" transform="translate(-0.001)" fill="#0072ff"></path>
                                </svg></a>
                            <a class="tr-remove" id="btnShowDeleteDialog" runat="server" onserverclick="btnShowDeleteDialog_ServerClick">
                                <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                    <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                        <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                        <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                    </g>
                                </svg></a>

                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HiddenField ID="hToDoListId" runat="server" />
        <asp:HiddenField ID="hCommentToDoListId" runat="server" />
        <asp:HiddenField ID="hToDoListName" runat="server" />
    </div>
    <div class="row" style="text-align: center;">
        <asp:Label ID="lblNoResult" runat="server" Text="Label" Visible="false"></asp:Label>
    </div>
    <!-- Modal tr cooment -->
    <dialog id="tr-comment" style="border: none; padding: 0px 0px;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><span id="taskName" runat="server"></span></h4>
                    <div class="expand-modal">
                        <svg id="Group_2116" data-name="Group 2116" xmlns="http://www.w3.org/2000/svg" width="33" height="33" viewBox="0 0 33 33">
                            <circle id="Ellipse_555" data-name="Ellipse 555" cx="16.5" cy="16.5" r="16.5" fill="#d7d7d7"></circle>
                            <g id="Group_2115" data-name="Group 2115" transform="translate(6.454 6.827)">
                                <path id="Path_6976" data-name="Path 6976" d="M0,7.742V2.968L1.107,4.075,5.181,0,7.742,2.561,3.667,6.635,4.774,7.742Z" transform="translate(19.441 7.742) rotate(180)" fill="#fff"></path>
                                <path id="Path_6977" data-name="Path 6977" d="M7.742,7.742V2.968L6.635,4.075,2.561,0,0,2.561,4.075,6.635,2.968,7.742Z" transform="translate(7.742 7.742) rotate(180)" fill="#fff"></path>
                                <path id="Path_6978" data-name="Path 6978" d="M0,0V4.774L1.107,3.667,5.181,7.742l2.561-2.56L3.667,1.107,4.774,0Z" transform="translate(19.441 18.735) rotate(180)" fill="#fff"></path>
                                <path id="Path_6979" data-name="Path 6979" d="M7.742,0V4.774L6.635,3.667,2.561,7.742,0,5.182,4.075,1.107,2.968,0Z" transform="translate(7.742 18.735) rotate(180)" fill="#fff"></path>
                            </g>
                        </svg>

                    </div>

                </div>
                <div class="modal-body modal-body-padding">
                    <div class="main-field-holder">
                        <label class="main-label"><%= HudhudResources.Resources.Screen_ToDoList_addacomment %></label>
                        <textarea id="txtComment" runat="server" class="main-textarea textarea-lg-two"></textarea>
                    </div>


                    <div class="left-btn-holder">
                        <a id="btnAddComment" runat="server" onserverclick="btnAddComment_ServerClick" class="btn-main" data-dismiss="modal">
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
                        OnRowCommand="gvTaskListComments_OnRowCommand"
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
                </div>
                <div class="modal-footer">
                    <div class="start-side">
                    </div>

                    <div class="end-side">


                        <a class="btn-main btn-white" data-dismiss="modal" onclick="document.getElementById('tr-comment').close();">
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

                                <%= HudhudResources.Resources.Close %>
                            </div>
                        </a>

                    </div>
                </div>
            </div>
        </div>
    </dialog>
    <!-- Modal tr Remove-->
    <dialog id="tr-remove" style="border: none; padding: 0px 0px;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel"><%= HudhudResources.Resources.Screen_ToDoList_Areyousurethedeletion %></h4>
                </div>
                <!-- <div class="modal-body">
                    </div> -->
                <div class="modal-footer">
                    <button type="button" class="btn-done-model" id="btnDeleteTask" runat="server" onserverclick="btnDeleteTask_ServerClick"><%= HudhudResources.Resources.Screen_ToDoList_Yeah %></button>
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
                    <button type="button" class="btn-close-model" onclick="document.getElementById('tr-removeComment').close();" onserverclick="btnShowCommentUsingCurrentToDoListId_ServerClick" runat="server">
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
        function showCommentModal() {
            document.getElementById("tr-comment").showModal();
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
