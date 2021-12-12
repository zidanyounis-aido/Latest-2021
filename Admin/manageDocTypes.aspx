<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.master" AutoEventWireup="true" CodeBehind="manageDocTypes.aspx.cs" Inherits="dms.Admin.manageDocTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/newpage.css" rel="stylesheet" type="text/css" />
    <link href="<%= (Session["lang"].ToString() == "0") ? "../css/newpage-ltr.css" : ""%>" rel="stylesheet" />

    <style type="text/css">
        .ctrlDiv {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="../screen/subIcons.aspx?CODEN=2&dlgid=2&indexId=0"><%= (Session["lang"].ToString() == "0") ? "Settings" : "الاعدادات"%> </a></li>
        <li><a href="../admin/manageDocTypes.aspx?CODEN=15"><%= (Session["lang"].ToString() == "0") ? " Forms" : "النماذج "%></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var isLangEnglish = <%=(Session["lang"].ToString() == "0") ? "true" : "false" %>;
    </script>

    <!-- html here -->
    <div class="white-holder" id="divList" style="padding-top: 15px; display: block;">
        <div class="row">
            <div class="col-xs-3">
                <a class="aside-addform add-new-form" id="addNew">
                    <div class="select-setting-item-holder add-select-item-holder">
                        <div class="select-setting-icon">
                            <svg visibility="visible" xmlns="http://www.w3.org/2000/svg" width="8" height="8" viewBox="0 0 8 8">
                                <g id="Group_3023" data-name="Group 3023" transform="translate(-1664.841 -367.841)">
                                    <circle id="Ellipse_598" data-name="Ellipse 598" cx="4" cy="4" r="4" transform="translate(1664.841 367.841)" fill="#484848"></circle>
                                    <g id="Group_3020" data-name="Group 3020" transform="translate(1668.87 370.266) rotate(45)">
                                        <g id="Group_2166" data-name="Group 2166" transform="translate(0)">
                                            <line id="Line_28" data-name="Line 28" y2="3.59" transform="translate(2.539) rotate(45)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="0.5"></line>
                                            <line id="Line_29" class="make-plus" data-name="Line 29" x2="3.59" transform="rotate(45)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="0.5"></line>
                                        </g>
                                    </g>
                                </g>
                            </svg>
                        </div>
                        <p class="select-setting-title"><%= (Session["lang"].ToString() == "0") ? "Add Form" : "اضافة نموذج"%></p>
                    </div>
                </a>
            </div>
            <asp:ListView ID="ListViewDocTypes" runat="server" CausesValidation="false" DataKeyNames="docTypID" OnSelectedIndexChanging="ListViewDocTypes_SelectedIndexChanging">
                <ItemTemplate>
                    <div class="col-xs-3">
                        <a onclick='onClickDocTypeMetas(<%#Eval("docTypID")%>)'>
                            <div class="select-setting-item-holder">
                                <div class="select-setting-icon">
                                    <span><%# (Session["lang"].ToString() == "0") ? SafeSmartSubstring(Eval("DocTypDesc").ToString()) : SafeSmartSubstring(Eval("DocTypDescAr").ToString())%></span>
                                </div>
                                <p class="select-setting-title"><%# (Session["lang"].ToString() == "0") ? Eval("DocTypDesc") : Eval("DocTypDescAr")%> </p>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div class="white-holder" id="divAdd" style="padding-top: 15px; display: none;">
        <div class="max-width-holder">
            <div class="col-xs-12">
                <div class="main-field-holder">
                    <div class="main-title">
                        <span id="lblFormMode" class="formModeTitleCSS">اضافة نموذج</span>
                    </div>
                </div>
            </div>
            <div class="col-xs-12" style="display:none;">
                <div class="main-field-holder ">
                    <label class="main-label">
                        رقم
                    </label>
                    <input type="number" class="main-input" id="DocTypeNumber" readonly="readonly" />
                </div>
            </div>
            <div class="col-xs-6">
                <div class="main-field-holder ">
                    <label class="main-label">
                        الوصف انجليزي
                    </label>
                    <input type="text" class="main-input" id="DocTypeDesc" required="required" />
                </div>
            </div>
            <div class="col-xs-6">
                <div class="main-field-holder ">
                    <label class="main-label">
                        الوصف عربي
                    </label>
                    <input type="text" class="main-input" id="DocTypeDescAr" required="required" />
                </div>
            </div>
            <div class="col-xs-6">
                <div class="main-field-holder ">
                    <label class="main-label">
                        مسار العمل الافتراضي
                    </label>
                    <select class="dropdown-main dropdown" id="slctDefaultWF" runat="server" clientidmode="Static">
                    </select>
                </div>
            </div>
            <div class="col-xs-6">
                <div class="main-field-holder ">
                    <label class="main-label">
                        &nbsp;
                    </label>
                    <input type="checkbox" id="docTypeIsActive" />
                    <label for="checkrequired">فعال</label>
                </div>
            </div>
        </div>
        <div class="control-side-holder control-side-holder-footer">
            <div class="start-side">
                <a class="btn-main" id="SaveDocType" onclick="onClickSaveDocType()">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                            <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                            <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                            <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                            <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                            <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                        </svg>
                        <span id="lblSave1" runat="server"><%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%></span>
                    </div>
                </a>
                <a id="btnCancel" class="btn-main btn-white">
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
                        <%= (Session["lang"].ToString() == "0") ? "Cancel" : "تراجع"%>
                    </div>
                </a>
            </div>

            <div class="end-side">
                <a class="btn-main" runat="server" data-toggle="modal" data-target="#remove-confirm" id="btnDelete" style="display: none;">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg" width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                            <path id="Path_7057" data-name="Path 7057" d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z" transform="translate(-63.122 -124.487)" fill="#fff"></path>
                            <path id="Path_7058" data-name="Path 7058" d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)" fill="#fff"></path>
                        </svg>
                        <span id="lblSurvey1" runat="server"><%= (Session["lang"].ToString() == "0") ? "Delete" : "حذف"%></span>
                    </div>
                </a>
            </div>
        </div>
        <div class="new-page-section" style="display: none;">
            <div class="holder-newpage">
                <div class="add-new-field" id="newRow0" onclick="callAddNewFiled(this);">+</div>
                <div class="preview-holder" id="divMetaBody">
                </div>
                <div class="newpage-footer">
                    <a class="btn-add-newpage" onclick="Save()">حفظ</a>
                    <a class="btn-preview">معاينة</a>
                </div>
            </div>
        </div>
    </div>

    <!-- popup to add field -->

    <div class="popup-screen popup-fields">
        <div class="popup-holder">
            <div class="popup-head">
                <span class="popup-title">إضافة حقل</span>
                <i class="fas fa-times close-popup"></i>
            </div>

            <div class="popup-content">
                <div class="popup-field-holder">
                    <label class="label">
                        الوصف انجليزي
                    </label>
                    <input type="text" class="input" id="pmetaDesc" />
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        الوصف عربي
                    </label>
                    <input type="text" class="input" id="pmetaDescAr" />
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        أداة التحكم
                    </label>
                    <select class="select" id="pctrlID" runat="server" onchange="onSlctControlChange(this);" clientidmode="Static">
                    </select>
                </div>
                <div class="popup-field-holder-two">
                    <div class="popup-field-holder">
                        <label class="label" for="checkrequired">
                            مطلوب
                        </label>
                        <input type="checkbox" class="input-checkbox" id="prequired" />
                    </div>

                    <div class="popup-field-holder">
                        <label class="label" for="checkvisible">
                            مرئي
                        </label>
                        <input type="checkbox" checked="checked" class="input-checkbox" id="pvisible" />
                    </div>
                </div>

                <p class="popup-center-title"><span>اعدادات أداة التحكم</span></p>



                <div id="divCtrlID1" class="ctrlDiv">

                    <div class="popup-field-holder">
                        <label class="label">
                            نوع البيانات
                        </label>
                        <select class="select" id="pDataType">
                            <option value="String">نص</option>
                            <option value="DateTime">تاريخ</option>
                            <option value="Int32">رقم صحيح</option>
                            <option value="Decimal">رقم عشري</option>
                        </select>
                    </div>

                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Default Text" :  "النص الافتراضي" %>
                        </label>
                        <input type="text" class="input" id="pInputDefaultTexts" /><span class="btn-default-text" data-id="en"><i class="fas fa-bars"></i></span>
                    </div>
                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Default Arabic Text" :  "النص الافتراضي العربي" %>
                        </label>
                        <input type="text" class="input" id="pInputDefaultArTexts" /><span class="btn-default-text" data-id="ar"><i class="fas fa-bars"></i></span>
                    </div>
                </div>

                <div id="divCtrlItemList" class="ctrlDiv">
                    <div id="divCtrlItems">
                    </div>

                    <div class="popup-field-holder">
                        <div class="btn-add-value" onclick="AddDivCtrlItem('', '', '')">+</div>
                    </div>
                </div>
                <div id="divCtrlID8" class="ctrlDiv">

                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Select Image" :  "اختر الصورة" %>
                        </label>
                        <input type="file" class="input" id="pImage" />
                    </div>

                    <div class="popup-field-holder">
                        <label class="label">
                        </label>
                        <img id="myUploadedImg" src="#" alt='<%# (Session["lang"].ToString() == "0") ? "Select Image" :  "اختر الصورة" %>'
                            class="setting-img-preview" />
                    </div>
                </div>

                <div id="divCtrlID7" class="ctrlDiv">

                    <div class="popup-field-holder popup-map-holder">

                        <div class="popup-field-holder">
                            <label class="label">
                                <%= (Session["lang"].ToString() == "0") ? "Latitude" :  "خط العرض" %>
                            </label>
                            <input type="text" class="input" id="lat" />
                        </div>

                        <div class="popup-field-holder">
                            <label class="label">
                                <%= (Session["lang"].ToString() == "0") ? "Longitude" :  "خط الطول" %>
                            </label>
                            <input type="text" class="input" id="lng" />
                        </div>
                        <div id="map" style="width: 100%; height: 410px">24.774265,46.738586</div>

                    </div>
                </div>
                <div id="divCtrlID9" class="ctrlDiv">
                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Copy Link" :  "نسخ الرابط" %>
                        </label>
                        <input type="text" class="input" id="pLink">
                    </div>
                </div>
                <div id="divCtrlID6" class="ctrlDiv">
                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Rows Number" :  "عدد الاسطر" %>
                        </label>
                        <input type="number" class="input" id="tblRowNumber" />
                    </div>

                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Columns Number" :  "عدد الاعمدة" %>
                        </label>
                        <input type="number" class="input" id="tblColumnNumber" />
                    </div>

                </div>

                <div class="newpage-footer">
                    <a class="btn-add-newpage" onclick="SaveMeta()"><%= (Session["lang"].ToString() == "0") ? "Save" :  "حفظ" %></a>
                    <a class="close-popup" onclick="clearPopupData();"><%= (Session["lang"].ToString() == "0") ? "Cancel" :  "الغاء" %></a>
                </div>

            </div>
        </div>
    </div>


    <!-- popup to review field -->

    <div class="popup-screen popup-defaultText">

        <div class="popup-holder">
            <div class="popup-head">
                <span class="popup-title">اعدادات النص الافتراضي</span>
                <i class="fas fa-times close-popup"></i>
            </div>

            <div class="popup-content">
                <div class="default-values-section">
                    <div class="default-values-holder">
                        <div class="value-item value-item-active">
                            <i class="fas fa-times-circle btn-remove-value"></i>
                            <span class="value-name" value="">اختر القيمة</span>
                        </div>
                        <span class="add-new-value">+</span>
                    </div>
                </div>

                <div class="value-settings-section">
                    <p class="popup-center-title" style="margin-top: 0;"><span>المهام الحسابية</span></p>

                    <div class="values-calc-holder">
                        <span class="value-name-setting" value="+">+</span>
                        <span class="value-name-setting" value="-">-</span>
                        <span class="value-name-setting" value="*">*</span>
                        <span class="value-name-setting" value="/">/</span>
                        <span class="value-name-setting" value="(">(</span>
                        <span class="value-name-setting" value=")">)</span>
                        <span class="value-name-setting" value="parseInt(value)">التحويل الى عدد صحيح</span>
                    </div>

                    <div class="tabs-section">
                        <div class="list-tabs-holder">
                            <p class="popup-center-title" style="margin-top: 0;"><span>القائمة</span></p>
                            <ul class="tabs">
                                <li class="tab-link current" data-tab="tab-1">حقل النموذج</li>
                                <li class="tab-link" data-tab="tab-2">الرقم التسلسلي</li>
                                <li class="tab-link" data-tab="tab-3">معلومات المستخدم الحالي</li>
                                <li class="tab-link" data-tab="tab-4">التاريخ الحالي</li>
                                <li class="tab-link" data-tab="tab-5">مهام النص</li>
                                <li class="tab-link" data-tab="tab-6">كتابة قيمة</li>
                            </ul>
                        </div>

                        <div class="value-tabs-holder">
                            <p class="popup-center-title" style="margin-top: 0;"><span>القيم</span></p>
                            <div id="tab-1" class="tab-content current">
                                <span class="value-name-setting" value="meta_1">الراتب</span>
                                <span class="value-name-setting" value="meta_2">الضرائب</span>
                                <span class="value-name-setting" value="meta_3">اسم الموظف</span>
                                <span class="value-name-setting" value="meta_4">الصافي</span>
                                <span class="value-name-setting" value="meta_5">البلد</span>
                            </div>
                            <div id="tab-2" class="tab-content">
                                <span class="value-name-setting" value="docID">الترتيب على مستوى النظام</span>
                                <span class="value-name-setting" value="FolderSeq">الترتيب حسب المجلد</span>
                                <span class="value-name-setting" value="DocTypeSeq">الترتيب حسب نوع الملف</span>
                                <span class="value-name-setting" value="FolderDocTypeSeq">الترتيب حسب نوع الملف والمجلد
                                </span>
                            </div>
                            <div id="tab-3" class="tab-content">
                                <span class="value-name-setting" value="userID">رقم المستخدم الحالي</span>
                                <span class="value-name-setting" value="username">اسم دخول المستخدم الحالي</span>
                                <span class="value-name-setting" value="userFullname">الاسم الكامل للمستخدم
                                    الحالي</span>
                                <span class="value-name-setting" value="userPosition">المسمى الوظيفي للمستخدم
                                    الحالي</span>
                                <span class="value-name-setting" value="userDepartment">إدارة المستخدم الحالي</span>
                            </div>
                            <div id="tab-4" class="tab-content">
                                <span class="value-name-setting" value="currentDate">تاريخ اليوم</span>
                                <span class="value-name-setting" value="currentYear">السنة الحالية</span>
                                <span class="value-name-setting" value="currentYearShort">السنة الحالية (خانتين)</span>
                                <span class="value-name-setting" value="currentMonth">الشهر الحالي</span>
                                <span class="value-name-setting" value="currentMonthLong">اسم الشهر الحالي</span>
                                <span class="value-name-setting" value="currentMonthShort">الاسم المختصر للشهر
                                    الحالي</span>
                                <span class="value-name-setting" value="currentDay">اليوم</span>
                                <span class="value-name-setting" value="currentDayName">اسم اليوم</span>
                            </div>

                            <div id="tab-5" class="tab-content">
                                <span class="value-name-setting" value="String(value)">تحويل رقم إلى نص</span>
                                <span class="value-name-setting" value=" + ">اضافة الى النهاية</span>
                                <span class="value-name-setting" value="String(value).substr(0,1)">الاقتطاع من النص (حرف
                                    البداية , العدد)</span>
                                <span class="value-name-setting" value="String(value).length">العدد</span>
                            </div>

                            <div id="tab-6" class="tab-content">
                                <div class="value-enter-setting">
                                    <input placeholder="اكتب القيمة" type="text" class="input-value-enter">
                                    <i class="fas fa-check btn-value-enter" id="btn-value-enter"></i>
                                </div>
                            </div>

                        </div>


                    </div>
                </div>
                <a href="#!" class="btn-main-full" onclick="setMetaDefault();">حفظ</a>
            </div>
        </div>
    </div>


    <!-- popup to review field -->
    <div class="popup-screen popup-preview">

        <div class="popup-holder">
            <div class="popup-head">
                <span class="popup-title">معاينة الحقول</span>
                <i class="fas fa-times close-popup"></i>
            </div>

            <div class="popup-content">
                <div class="popup-content-preview">
                </div>

            </div>
        </div>
    </div>

    <div class="popup-screen popup-terms">

        <div class="popup-holder">
            <div class="popup-head">
                <span id="PermissinTitle" class="popup-title">الصلاحيات</span>
                <i class="fas fa-times close-popup"></i>
            </div>
            <div class="popup-content">
                <div class="popup-field-holder-two">
                    <div class="popup-field-holder">
                        <label class="label" for="Inherit">
                            الصلاحيات المعدة مسبقاً للنموذج
                        </label>
                        <input type="radio" class="input-checkbox" checked="checked" id="Inherit" value="Inherit" name="premessionType">
                    </div>

                    <div class="popup-field-holder">
                        <label class="label" for="Custom">
                            تخصيص صلاحيات جديدة للحقل
                        </label>
                        <input type="radio" class="input-checkbox" id="Custom" value="Custom" name="premessionType">
                    </div>

                </div>
                <div id="divCustomPermission" style="display: none;">
                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Groups" :  "المجموعات" %>
                        </label>
                        <select class="select" id="slctGroups" runat="server" clientidmode="Static">
                        </select>
                    </div>
                    <div class="popup-field-holder">
                        <label class="label">
                            <%= (Session["lang"].ToString() == "0") ? "Users" :  "المستخدمين" %>
                        </label>
                        <select class="select" id="slctUsers" runat="server" clientidmode="Static">
                        </select>
                    </div>
                    <div class="popup-field-holder">
                        <table class="my-table" style="text-align: center;" id="tblMetaPermission">
                            <tbody>
                                <tr>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Name" :  "الاسم" %></th>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Type" :  "النوع" %></th>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Show" :  "إظهار" %></th>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Update" :  "تعديل" %></th>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Delete" :  "حذف" %></th>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                    <br />
                    <a class="btn-main-full" onclick="onClickSavePermission()">حفظ</a>
                </div>
                <div id="divInheritPermission" style="display: none;">
                    <div class="popup-field-holder">
                        <table class="my-table" style="text-align: center;" id="tblMetaInheritPermission">
                            <tbody>
                                <tr>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Name" :  "الاسم" %></th>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Type" :  "النوع" %></th>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Show" :  "إظهار" %></th>
                                    <th><%= (Session["lang"].ToString() == "0") ? "Update" :  "تعديل" %></th>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                    <br />
                    <a class="btn-main-full" onclick="UpdateInheritToCustomPermissions()">تعديل</a>
                </div>
            </div>
        </div>
    </div>
   <%-- <script src="../Scripts/newpage.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../JS/ManageDocTypes.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCk_E-u_5YvITmiuDxVH9uhbIl3cskLwqA"></script>
</asp:Content>
