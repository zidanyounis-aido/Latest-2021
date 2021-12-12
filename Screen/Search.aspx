<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="dms.Screen.SearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <style type="text/css">
        :root {
            --main-color: #007aff;
        }

        .search-cat-title .num {
            background: var(--main-color);
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
            color: var(--main-color);
            font-size: 15px;
            margin-inline-end: auto;
        }

        .btn-done-model {
            background: var(--main-color);
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
            background: var(--main-color);
        }

            .my-table tbody tr th:hover .btn-sort-table path {
                fill: #fff;
                stroke: #fff;
                stroke-width: 2px;
            }

        .my-table tbody tr:hover td a.tr-comment svg circle {
            fill: var(--main-color);
        }

        .my-table tbody tr:hover td a.tr-edit svg path {
            fill: var(--main-color);
        }

        .my-table tbody tr:hover td a.tr-remove {
            background: var(--main-color) !important;
        }

        .my-table tbody tr {
            transition: 0.3s ease;
        }

            .my-table tbody tr:hover {
                background: #E5F1FF;
            }

            .my-table tbody tr td {
                padding: 20px 5px;
                font-weight: bold;
                color: #484848;
                transition: 0.3s ease;
                word-break: break-word;
            }

            .my-table tbody tr:hover td {
                color: var(--main-color);
            }


            .my-table tbody tr td a {
                display: inline-block;
                height: 22px;
                width: 22px;
                margin: 0 5px;
                cursor: pointer;
            }

        .show-all {
            color: #007aff !important;
            font-weight: bold;
            text-decoration: none;
            cursor: pointer;
        }

            .show-all::after {
                content: none !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">

        <ul class="pages_nav">
            <li><a href="#">نتائج البحث</a></li>
        </ul>

        <div class="row row-search">
            <div class="col-xs-2">
                <div class="search-cat-title">
                    <svg xmlns="http://www.w3.org/2000/svg" id="Group_1787" data-name="Group 1787" width="27.34"
                        height="22.534" viewBox="0 0 27.34 22.534">
                        <path id="Path_6934" data-name="Path 6934"
                            d="M255.352,77.4h11.835a.41.41,0,0,0,.41-.41V75.41a.41.41,0,0,0-.41-.41H254.222a.41.41,0,0,0-.334.649l1.13,1.582A.411.411,0,0,0,255.352,77.4Z"
                            transform="translate(-240.258 -73.398)" fill="#484848">
                        </path>
                        <path id="Path_6935" data-name="Path 6935"
                            d="M8.909,45H2.225A2.225,2.225,0,0,0,0,47.225V65.31a2.225,2.225,0,0,0,2.225,2.225H25.116A2.225,2.225,0,0,0,27.34,65.31V52.832a2.225,2.225,0,0,0-2.225-2.225H15.2a2.225,2.225,0,0,1-1.81-.932l-2.674-3.744A2.225,2.225,0,0,0,8.909,45Z"
                            transform="translate(0 -45)" fill="#484848">
                        </path>
                    </svg>
                    <span class="name">المجلدات</span>
                    <span id="spanDocumentsCount" runat="server" class="num">+10</span>
                </div>
            </div>
            <div class="col-xs-10">
                <asp:GridView ID="gvDocuments" runat="server" CssClass="table my-table"
                    PageSize="3" AllowSorting="True" OnSorting="gvDocuments_Sorting"
                    OnRowCommand="gvDocuments_RowCommand" AllowPaging="True" AllowCustomPaging="True" DataKeyNames="docID"
                    AutoGenerateColumns="False" OnPageIndexChanging="gvDocuments_PageIndexChanging" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                    <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Right"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <Columns>
                        <asp:TemplateField SortExpression="docID">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkID" runat="server" CommandName="Sort" CommandArgument="docID">تسلسل
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
                                <%# Eval("docID") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="docName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkdocName" runat="server" CommandName="Sort" CommandArgument="docName">اسم الملف
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
                                <%# Eval("docName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FolderName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkFolderName" runat="server" CommandName="Sort" CommandArgument="FolderName">المجلد
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
                                <%# Eval("FolderName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DocTypeName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkDocTypeName" runat="server" CommandName="Sort" CommandArgument="DocTypeName">النوع
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
                                <%# Eval("DocTypeName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="addedDate">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkaddedDate" runat="server" CommandName="Sort" CommandArgument="addedDate">الإضافة
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
                                <%# Eval("addedDateOnly") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="AddedBy">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkAddedBy" runat="server" CommandName="Sort" CommandArgument="AddedBy">المستخدم
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
                                <%# Eval("AddedBy") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="modifyDate">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkmodifyDate" runat="server" CommandName="Sort" CommandArgument="modifyDate">تاريخ التعديل
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
                                <%# Eval("modifyDateOnly") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="tr-btn-holder">
                                    <a class="tr-edit" href="../Screen/documentInfo.aspx?docID=<%# c.encrypt(Eval("docID").ToString()) %>">
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
                            <HeaderTemplate>
                                <a id="btnShowAllDocuments" runat="server" onserverclick="btnShowAllDocuments_ServerClick" class="show-all" style="color: #007aff">عرض الكل</a>
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row row-search">
            <div class="col-xs-2">
                <div class="search-cat-title">
                    <svg xmlns="http://www.w3.org/2000/svg" width="29.123" height="29.61" viewBox="0 0 29.123 29.61">
                        <path id="Path_6926" data-name="Path 6926"
                            d="M64.809,39.362V25.921a2.767,2.767,0,0,0-2.763-2.763h-2.91V21.542A1.343,1.343,0,0,0,57.793,20.2H57.58a1.342,1.342,0,0,0-1.343,1.342v1.616H45.96V21.542A1.343,1.343,0,0,0,44.618,20.2H44.4a1.342,1.342,0,0,0-1.343,1.341h0v1.616H40.183a2.765,2.765,0,0,0-2.763,2.763V45.264a2.767,2.767,0,0,0,2.763,2.764H56.045a6.163,6.163,0,0,0,8.765-8.666Zm-2.354,6.36a.561.561,0,0,1-.793,0l-.819-.819-.863-.863a.558.558,0,0,1-.164-.4V40.28a.56.56,0,1,1,1.121,0v3.131l.751.751.767.767A.561.561,0,0,1,62.455,45.722ZM40.543,29.846H61.688v7.777a6.034,6.034,0,0,0-1.311-.145,6.174,6.174,0,0,0-6.167,6.166,6.007,6.007,0,0,0,.135,1.259h-13.8Z"
                            transform="translate(-37.42 -20.2)" fill="#484848">
                        </path>
                    </svg>
                    <span class="name">المهام</span>
                    <span id="spanTasksCount" runat="server" class="num">+10</span>
                </div>
            </div>
            <div class="col-xs-10">
                <asp:GridView ID="gvTasks" runat="server" CssClass="table my-table"
                    PageSize="3" AllowSorting="True" OnSorting="gvTasks_Sorting"
                    OnRowCommand="gvTasks_RowCommand" AllowPaging="True" AllowCustomPaging="True" DataKeyNames="Id"
                    AutoGenerateColumns="False" OnPageIndexChanging="gvTasks_PageIndexChanging" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                    <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Right"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <Columns>
                        <asp:TemplateField SortExpression="IsComplete">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkIsComplete" runat="server" CommandName="Sort" CommandArgument="IsComplete">انتهت ؟
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
                                <asp:CheckBox AutoPostBack="true" Checked='<%# Eval("IsComplete") %>'
                                    OnCheckedChanged="gvTasks_CheckedChanged"
                                    ID="CheckBox1" runat="server" CommandArgument='<%# Eval("Id") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="TaskName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkTaskName" runat="server" CommandName="Sort" CommandArgument="TaskName">اسم المهمة
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
                                <%# Eval("TaskName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="TaskDate">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkTaskDate" runat="server" CommandName="Sort" CommandArgument="TaskDate">تاريخ المهمة
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
                                <%# Eval("TaskDateOnly") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="TaskDate">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkTaskTime" runat="server" CommandName="Sort" CommandArgument="TaskDate">الوقت
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
                                <%# ToTime(Eval("TaskTimeOnly")) %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="AssignTo">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkAssignTo" runat="server" CommandName="Sort" CommandArgument="AssignTo">موجهة الى
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
                                <%# Eval("AssignTo") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="CreatedBy">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkCreatedBy" runat="server" CommandName="Sort" CommandArgument="CreatedBy">انشئت من
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
                                <%# Eval("CreatedBy") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="TaskType">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkTaskType" runat="server" CommandName="Sort" CommandArgument="TaskType">درجة الأهمية
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
                                <%# Eval("TaskType") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="tr-btn-holder">
                                    <a class="tr-edit" href="AddToDoList.aspx?view=true&id=<%# Eval("Id") + (Request.QueryString["docID"] != null ? "&docID=" + Request.QueryString["docID"] : "") + "&PreviousPage=Search.aspx?CODEN=" + ViewState["SearchText"] %>">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="22.506" height="22.506" viewBox="0 0 22.506 22.506">
                                            <path id="Path_6947" data-name="Path 6947" d="M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z" transform="translate(-0.001)" fill="#0072ff"></path>
                                        </svg></a>
                                    <a class="tr-remove" href="#" id="btnShowDeleteDialog" runat="server" onserverclick="btnShowDeleteDialog_ServerClick">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                            <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                                <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                                <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                            </g>
                                        </svg></a>

                                </div>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <a id="btnShowAllTasks" runat="server" onserverclick="btnShowAllTasks_ServerClick" class="show-all" style="color: #007aff">عرض الكل</a>
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row row-search">
            <div class="col-xs-2">
                <div class="search-cat-title">
                    <svg xmlns="http://www.w3.org/2000/svg" width="28" height="27.07" viewBox="0 0 28 27.07">
                        <path id="Subtraction_26" data-name="Subtraction 26"
                            d="M7.636,27.071V19.992h-4.5A3.158,3.158,0,0,1,0,16.821V3.218A3.158,3.158,0,0,1,3.137.046L24.863,0A3.158,3.158,0,0,1,28,3.172v13.6a3.158,3.158,0,0,1-3.137,3.172L14,19.992,7.637,27.069ZM5.727,13.513a.643.643,0,0,0,0,1.287H22.273a.643.643,0,0,0,0-1.287Zm0-3.861a.643.643,0,0,0,0,1.287H22.273a.643.643,0,0,0,0-1.287Zm0-3.861a.643.643,0,0,0,0,1.287H14a.643.643,0,0,0,0-1.287Z"
                            fill="#484848">
                        </path>
                    </svg>
                    <span class="name">الصادر و الوارد</span>
                    <span id="spanOutgoingAndIncomingDocumentsCount" runat="server" class="num">+10</span>
                </div>
            </div>
            <div class="col-xs-10">
                <asp:GridView ID="gvOutgoingAndIncomingDocuments" runat="server" CssClass="table my-table"
                    PageSize="3" AllowSorting="True" OnSorting="gvOutgoingAndIncomingDocuments_Sorting"
                    OnRowCommand="gvOutgoingAndIncomingDocuments_RowCommand" AllowPaging="True" AllowCustomPaging="True" DataKeyNames="docID"
                    AutoGenerateColumns="False" OnPageIndexChanging="gvOutgoingAndIncomingDocuments_PageIndexChanging" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                    <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Right"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <Columns>
                        <asp:TemplateField SortExpression="docID">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkID" runat="server" CommandName="Sort" CommandArgument="docID">تسلسل
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
                                <%# Eval("docID") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="docName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkdocName" runat="server" CommandName="Sort" CommandArgument="docName">اسم الملف
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
                                <%# Eval("docName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FolderName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkFolderName" runat="server" CommandName="Sort" CommandArgument="FolderName">المجلد
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
                                <%# Eval("FolderName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DocTypeName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkDocTypeName" runat="server" CommandName="Sort" CommandArgument="DocTypeName">النوع
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
                                <%# Eval("DocTypeName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="addedDate">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkaddedDate" runat="server" CommandName="Sort" CommandArgument="addedDate">الإضافة
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
                                <%# Eval("addedDateOnly") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="AddedBy">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkAddedBy" runat="server" CommandName="Sort" CommandArgument="AddedBy">المستخدم
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
                                <%# Eval("AddedBy") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="modifyDate">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkmodifyDate" runat="server" CommandName="Sort" CommandArgument="modifyDate">تاريخ التعديل
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
                                <%# Eval("modifyDateOnly") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="tr-btn-holder">
                                    <a class="tr-edit" href="../Screen/documentInfo.aspx?docID=<%# c.encrypt(Eval("docID").ToString()) %>">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="22.506" height="22.506" viewBox="0 0 22.506 22.506">
                                            <path id="Path_6947" data-name="Path 6947" d="M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z" transform="translate(-0.001)" fill="#0072ff"></path>
                                        </svg></a>
                                    <a class="tr-remove" href="#" id="btnShowDeleteDialog" runat="server" onserverclick="btnShowDeleteDialog_ServerClick">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                            <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                                <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                                <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                            </g>
                                        </svg></a>

                                </div>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <a id="btnShowAllOutgoingAndIncomingDocuments" runat="server" onserverclick="btnShowAllOutgoingAndIncomingDocuments_ServerClick" class="show-all" style="color: #007aff">عرض الكل</a>
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row row-search">
            <div class="col-xs-2">
                <div class="search-cat-title">
                    <svg id="election-event-on-a-calendar-with-star-symbol" xmlns="http://www.w3.org/2000/svg"
                        width="27.388" height="27.824" viewBox="0 0 27.388 27.824">
                        <g id="Group_1653" data-name="Group 1653">
                            <path id="Path_6083" data-name="Path 6083"
                                d="M40.714,57.454l-.427,2.535a.924.924,0,0,0,1.34.974l2.28-1.19,2.28,1.19a.925.925,0,0,0,1.341-.974L47.1,57.455l1.836-1.8a.925.925,0,0,0-.512-1.576L45.88,53.7l-1.146-2.3a.925.925,0,0,0-1.657,0l-1.144,2.3-2.544.377a.925.925,0,0,0-.512,1.576Z"
                                transform="translate(-30.213 -39.542)" fill="#484848">
                            </path>
                            <path id="Path_6084" data-name="Path 6084"
                                d="M25.6,2.958H22.69V1.342A1.342,1.342,0,0,0,21.347,0h-.213a1.342,1.342,0,0,0-1.342,1.342V2.958H9.516V1.342A1.342,1.342,0,0,0,8.174,0H7.961A1.342,1.342,0,0,0,6.618,1.342V2.958H3.74A2.766,2.766,0,0,0,.977,5.721v19.34A2.767,2.767,0,0,0,3.74,27.824H25.6a2.767,2.767,0,0,0,2.763-2.763V5.721A2.767,2.767,0,0,0,25.6,2.958ZM25.242,24.7H4.1V9.645H25.242Z"
                                transform="translate(-0.977)" fill="#484848">
                            </path>
                        </g>
                    </svg>
                    <span class="name">الاحداث</span>
                    <span id="spanEventsCount" runat="server" class="num">+10</span>
                </div>
            </div>
            <div class="col-xs-10">
                <asp:GridView ID="gvEvents" runat="server" CssClass="table my-table"
                    PageSize="3"
                    AllowSorting="True" OnRowCommand="gvEvents_RowCommand"
                    OnSorting="gvEvents_Sorting" OnPageIndexChanging="gvEvents_PageIndexChanging"
                    AllowPaging="True" AllowCustomPaging="True" DataKeyNames="event_id"
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                    <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Right"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <Columns>
                        <asp:TemplateField SortExpression="title">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnktitle" runat="server" CommandName="Sort" CommandArgument="title">عنوان الحدث
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
                                <%# Eval("title") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="event_start">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkevent_start" runat="server" CommandName="Sort" CommandArgument="event_start">بداية الحدث
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
                                <%# Eval("event_start") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="event_end">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkevent_end" runat="server" CommandName="Sort" CommandArgument="event_end">نهاية الحدث
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
                                <%# Eval("event_end") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="UserName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkUserName" runat="server" CommandName="Sort" CommandArgument="UserName">انشئت من
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
                                <%# Eval("UserName") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="tr-btn-holder">
                                    <a class="tr-edit" href="EventsList.aspx?event_id=<%# Eval("event_id") %>">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="22.506" height="22.506" viewBox="0 0 22.506 22.506">
                                            <path id="Path_6947" data-name="Path 6947" d="M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z" transform="translate(-0.001)" fill="#0072ff"></path>
                                        </svg></a>
                                    <a class="tr-remove" href="#" id="btnShowDeleteDialog" runat="server" onserverclick="btnShowDeleteDialog_ServerClick">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                            <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                                <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                                <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                            </g>
                                        </svg></a>
                                </div>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <a id="btnShowAllEvents" runat="server" onserverclick="btnShowAllEvents_ServerClick" class="show-all" style="color: #007aff">عرض الكل</a>
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>


        <!-- Modal tr Remove-->
        <dialog id="tr-remove-event" style="border: none; padding: 0px 0px;">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabelEvent">هل أنت متأكد من الحذف ؟</h4>
                    </div>
                    <!-- <div class="modal-body">
                    </div> -->
                    <div class="modal-footer">
                        <button type="button" class="btn-done-model" id="btnDeleteEvent" runat="server" onserverclick="btnDeleteEvent_ServerClick">نعم</button>
                        <asp:HiddenField ID="hEventID" runat="server" />
                        <button type="button" class="btn-close-model" onclick="document.getElementById('tr-remove-event').close();">
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
                            تراجع
                        </button>
                    </div>
                </div>
            </div>
        </dialog>
        <dialog id="tr-remove-task" style="border: none; padding: 0px 0px;">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabelTask">هل أنت متأكد من الحذف ؟</h4>
                    </div>
                    <!-- <div class="modal-body">
                    </div> -->
                    <div class="modal-footer">
                        <button type="button" class="btn-done-model" id="btnDeleteTask" runat="server" onserverclick="btnDeleteTask_ServerClick">نعم</button>
                        <asp:HiddenField ID="hTaskID" runat="server" />
                        <button type="button" class="btn-close-model" onclick="document.getElementById('tr-remove-task').close();">
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
                            تراجع
                        </button>
                    </div>
                </div>
            </div>
        </dialog>
        <dialog id="tr-remove-document" style="border: none; padding: 0px 0px;">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabelDocument">هل أنت متأكد من الحذف ؟</h4>
                    </div>
                    <!-- <div class="modal-body">
                    </div> -->
                    <div class="modal-footer">
                        <button type="button" class="btn-done-model" id="btnDeleteDocument" runat="server" onserverclick="btnDeleteDocument_ServerClick">نعم</button>
                        <asp:HiddenField ID="hDocumentID" runat="server" />
                        <button type="button" class="btn-close-model" onclick="document.getElementById('tr-remove-document').close();">
                            <span class="icon-close">
                                <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963"
                                    viewBox="0 0 11.963 11.963">
                                    <g id="Group_21_3" data-name="Group 21"
                                        transform="translate(5.981 -3.153) rotate(45)">
                                        <line id="Line_28_3" data-name="Line 28" y2="12.918" transform="translate(6.459)"
                                            fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2">
                                        </line>
                                        <line id="Line_29_3" data-name="Line 29" x2="12.918"
                                            transform="translate(0 6.459)" fill="none" stroke="#fff"
                                            stroke-linecap="round" stroke-width="2">
                                        </line>
                                    </g>
                                </svg>
                            </span>
                            تراجع
                        </button>
                    </div>
                </div>
            </div>
        </dialog>
        <script>
            function showTaskRemoveModal() {
                document.getElementById("tr-remove-task").showModal();
            }
            function showEventRemoveModal() {
                document.getElementById("tr-remove-event").showModal();
            }
            function showDocumentRemoveModal() {
                document.getElementById("tr-remove-document").showModal();
            }
        </script>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
