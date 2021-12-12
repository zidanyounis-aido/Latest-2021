<%@ Page MasterPageFile="~/Masters/subPages.Master" Language="C#" AutoEventWireup="true" CodeBehind="NewPage.aspx.cs" Inherits="Araneas_ERP.screen.subIcons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/newpage.css" rel="stylesheet" type="text/css" />
    <link href="<%= (Session["lang"].ToString() == "0") ? "../css/newpage-ltr.css" : ""%>" rel="stylesheet" />
    <script src="../Scripts/newpage.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- html here -->
    <div class="new-page-section">

        <div class="new-page-aside">
            <a class="aside-addform add-new-form"><i class="fas fa-plus"></i> اضافة نموذج  </a>
            <ul class="new-page-aside-ul">
                <li class="aside-list-title">النماذج مضافة</li>
                <li class="aside-list"><span class="title">نموذج شئون الموظفين </span> <span class="edit"><i class="fas fa-pen"></i></span></li>
                <li class="aside-list"><span class="title">نموذج شئون الموظفين </span> <span class="edit"><i class="fas fa-pen"></i></span></li>
                <li class="aside-list"><span class="title">نموذج شئون الموظفين </span> <span class="edit"><i class="fas fa-pen"></i></span></li>
                <li class="aside-list"><span class="title">نموذج شئون الموظفين </span> <span class="edit"><i class="fas fa-pen"></i></span></li>
                <li class="aside-list"><span class="title">نموذج شئون الموظفين </span> <span class="edit"><i class="fas fa-pen"></i></span></li>
            </ul>
        </div>
        <div class="holder-newpage">
            <div class="add-new-field">+</div>
            <div class="preview-holder">
                <div class="row-field-holder">
                    <div class="add-new-field add-new-field-row unsortable">+</div>
                    <div class="sortable-holder">
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار مربع نص -مع تغير ال type ألى date , number, text حسب
                                ألاختيار
                            </p>
                            <div class="output-content-holder">
                                <label class="newpage-label">الوصف عربي</label>
                                <input type="date" class="newpage-input">
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار قائمة منسدله</p>
                            <div class="output-content-holder">
                                <label class="newpage-label">الوصف عربي</label>
                                <select class="newpage-select">
                                    <option>القيمة الاولى</option>
                                    <option>القيمة الثانية</option>
                                    <option>القيمة الثالثة</option>
                                </select>
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار مربع تحقيق</p>
                            <div class="output-content-holder">
                                <label class="newpage-label">الوصف عربي</label>
    
                                <div class="output-check-holder">
                                    <input type="checkbox" id="checkbox1" name="checkbox1" value="Bike">
                                    <label for="checkbox1"> أسم القيمة 1</label>
                                </div>
    
                                <div class="output-check-holder">
                                    <input type="checkbox" id="checkbox2" name="checkbox2" value="Car">
                                    <label for="checkbox2">أسم القيمة 2</label>
                                </div>
    
                                <div class="output-check-holder">
                                    <input type="checkbox" id="checkbox3" name="checkbox3" value="Boat">
                                    <label for="checkbox3">أسم القيمة 3</label>
                                </div>
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                    
                </div>
    
                <div class="add-new-field append-row">+</div>
            </div>
                <div class="row-field-holder">
                    <div class="add-new-field add-new-field-row unsortable">+</div>
                    <div class="sortable-holder">
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار اختيار متعدد</p>
                            <div class="output-content-holder">
                                <label class="newpage-label">الوصف عربي</label>
                                <div class="output-check-holder">
                                    <input type="radio" id="radio1" name="gender" value="radio1">
                                    <label for="radio1">أسم القيمة 1</label>
                                </div>
    
                                <div class="output-check-holder">
                                    <input type="radio" id="radio2" name="gender" value="radio2">
                                    <label for="radio2">أسم القيمة 2</label>
                                </div>
    
                                <div class="output-check-holder">
                                    <input type="radio" id="radio3" name="gender" value="radio3">
                                    <label for="radio3">أسم القيمة 3</label>
                                </div>
    
    
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار عنوان</p>
                            <div class="output-content-holder">
                                <h1 class="output-title-holder">الوصف عربي</h1>
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار عنوان</p>
                            <div class="output-content-holder">
                                <label class="newpage-label">الوصف عربي</label>
                                <img src="https://static.ed.edmunds-media.com/unversioned/unit-gw/homepage-hero/2020/mazda-3-2020-640x296.jpg"
                                    class="output-img-holder">
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                </div>
    
                <div class="add-new-field append-row">+</div>
            </div>
    
    
                <div class="row-field-holder">
                    <div class="add-new-field add-new-field-row unsortable">+</div>
                    <div class="sortable-holder">
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار موقع بالخريطة</p>
                            <div class="output-content-holder">
                                <label class="newpage-label">الوصف عربي</label>
                                <iframe
                                    src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d12762.793104984878!2d30.729351865317!3d36.89756572885605!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14c3855a620c5fd9%3A0x3d1fbd96c241c87e!2zWWVuaWfDvG4sIDA3MzEwIE11cmF0cGHFn2Ev2KPZhti32KfZhNmK2KfYjCDYqtix2YPZitin!5e0!3m2!1sar!2seg!4v1591211650257!5m2!1sar!2seg"
                                    class="output-map-holder" frameborder="0" style="border:0;" allowfullscreen=""
                                    aria-hidden="false" tabindex="0"></iframe>
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار رابط</p>
                            <div class="output-content-holder">
                                <a class="output-link-holder">الوصف عربي</a>
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار خط فاصل</p>
                            <div class="output-content-holder">
                                <div class="output-hr-holder"></div>
                            </div>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                </div>
    
                <div class="add-new-field append-row">+</div>
            </div>
                <div class="row-field-holder">
                    <div class="add-new-field add-new-field-row unsortable">+</div>
                <div class="sortable-holder">
    
                    <div class="resize-item-holder">
                        <i class="fas fa-ellipsis-v btn-view-option"></i>
                        <div class="item-option">
                            <span class="btn-edit"><i class="fas fa-edit"></i> تعديل</span>
                            <span class="btn-delete"><i class="fas fa-trash-alt"></i> حذف</span>
                            <span class="btn-setting"><i class="fas fa-cog"></i> الصلاحيات</span>
                            <span class="btn-hide"><i class="far fa-eye"></i> إخفاء</span>
                            <span class="btn-hide"><i class="far fa-clone"></i> تكرار</span>
                        </div>
    
                        <div class="item-field-holder">
                            <p class="hint-for-backend">اذا اختار - جدول</p>
                            <table class="my-table">
                                <tr>
                                    <th> عمود 1</th>
                                    <th> عمود 2</th>
                                    <th> عمود 3</th>
                                    <th> عمود 4</th>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="output-content-holder">
                                            <select class="newpage-select">
                                                <option>القيمة الاولى</option>
                                                <option>القيمة الثانية</option>
                                                <option>القيمة الثالثة</option>
                                            </select>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="output-content-holder"><input type="date" class="newpage-input"></div>
                                    </td>
    
                                    <td>
                                        <div class="output-content-holder"><input type="number" class="newpage-input"></div>
                                    </td>
    
                                    <td>
                                        <div class="output-content-holder"><input type="text" class="newpage-input"></div>
                                    </td>
    
                                </tr>
                                <tr>
                                    <td>
                                        <div class="output-content-holder">
                                            <select class="newpage-select">
                                                <option>القيمة الاولى</option>
                                                <option>القيمة الثانية</option>
                                                <option>القيمة الثالثة</option>
                                            </select>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="output-content-holder"><input type="date" class="newpage-input"></div>
                                    </td>
    
                                    <td>
                                        <div class="output-content-holder"><input type="number" class="newpage-input"></div>
                                    </td>
    
                                    <td>
                                        <div class="output-content-holder"><input type="text" class="newpage-input"></div>
                                    </td>
    
                                </tr>
                            </table>
                        </div>
                        <div class="add-new-field append-item">+</div>
                    </div>
    
                </div>
    
                <div class="add-new-field append-row">+</div>
            </div>
    
    
    
            </div>
    
    
    
            <div class="newpage-footer">
                <a class="btn-add-newpage">إضافة</a>
                <a class="btn-preview">معاينة</a>
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
                    <input type="text" class="input">
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        الوصف عربي
                    </label>
                    <input type="text" class="input">
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        أداة التحكم
                    </label>
                    <select class="select">
                        <option>مربع نص</option>
                        <option>قائمة منسدلة </option>
                        <option>مربع تحقيق </option>
                        <option>اختيار متعدد </option>
                        <option>عنوان </option>
                        <option>صورة </option>
                        <option>موقع بالخريطة </option>
                        <option>رابط </option>
                        <option>جدول </option>
                        <option>خط فاصل </option>
                    </select>
                </div>
                <div class="popup-field-holder-two">
                    <div class="popup-field-holder">
                        <label class="label" for="checkrequired">
                            مطلوب
                        </label>
                        <input type="checkbox" class="input-checkbox" id="checkrequired">
                    </div>

                    <div class="popup-field-holder">
                        <label class="label" for="checkvisible">
                            مرئي
                        </label>
                        <input type="checkbox" checked="checked" class="input-checkbox" id="checkvisible">
                    </div>
                </div>

                <p class="popup-center-title"><span>اعدادات أداة التحكم</span></p>

                <p class="hint-for-backend">اذا اختار اداه التحكم - مربع نص</p>

                <div class="popup-field-holder">
                    <label class="label">
                        نوع البيانات
                    </label>
                    <select class="select">
                        <option> نص</option>
                        <option>تاريخ</option>
                        <option>رقم صحيح</option>
                        <option>رقم عشرى</option>
                    </select>
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        النص الافتراضي
                    </label>
                    <input type="text" class="input"><span class="btn-default-text"><i class="fas fa-bars"></i></span>
                </div>

                <p class="hint-for-backend">اذا اختار اداه التحكم - قائمة منسدلة او مربع تحقق او اختيار متعدد</p>

                <div class="popup-field-holder row-input-holder">
                    <div class="row-input-items">
                        <label class="label">
                            الاسم عربى
                        </label>
                        <input type="text" class="input">
                    </div>

                    <div class="row-input-items">
                        <label class="label">
                            الاسم انجليزي
                        </label>
                        <input type="text" class="input">
                    </div >

                    <div class="row-input-items">
                        <label class="label">
                            اسم القيمة
                        </label>
                        <input type="text" class="input">
                    </div>
                    <span class="btn-remove-value"><i class="fas fa-trash"></i></span>
                </div>

                <div class="popup-field-holder">
                    <div class="btn-add-value">+</div>
                </div>

                <p class="hint-for-backend">اذا اختار اداه التحكم - صورة</p>

                <div class="popup-field-holder">
                    <label class="label">
                        اختر الصورة
                    </label>
                    <input type="file" class="input">
                </div>

                <div class="popup-field-holder">
                    <label class="label">

                    </label>
                    <img src="https://static.ed.edmunds-media.com/unversioned/unit-gw/homepage-hero/2020/mazda-3-2020-640x296.jpg"
                        class="setting-img-preview">
                </div>

                <p class="hint-for-backend">اذا اختار اداه التحكم - موقع بالخريطة</p>

                <div class="popup-field-holder popup-map-holder">
                    <iframe
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d12762.793104984878!2d30.729351865317!3d36.89756572885605!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14c3855a620c5fd9%3A0x3d1fbd96c241c87e!2zWWVuaWfDvG4sIDA3MzEwIE11cmF0cGHFn2Ev2KPZhti32KfZhNmK2KfYjCDYqtix2YPZitin!5e0!3m2!1sar!2seg!4v1591211650257!5m2!1sar!2seg"
                        class="setting-map" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false"
                        tabindex="0"></iframe>
                </div>


                <p class="hint-for-backend">اذا اختار اداه التحكم - رابط</p>
                <div class="popup-field-holder">
                    <label class="label">
                        نسخ الرابط
                    </label>
                    <input type="text" class="input">
                </div>

                <p class="hint-for-backend">اذا اختار اداه التحكم - جدول</p>
                <div class="popup-field-holder">
                    <label class="label">
                        عدد الاسطر
                    </label>
                    <input type="number" class="input">
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        عدد الاعمدة
                    </label>
                    <input type="number" class="input">
                </div>


                <table class="my-table">
                    <tr>
                        <th> <i class="fas fa-edit"></i> <i class="fas fa-trash-alt"></i> عمود 1</th>
                        <th> <i class="fas fa-edit"></i> <i class="fas fa-trash-alt"></i> عمود 2</th>
                        <th> <i class="fas fa-edit"></i> <i class="fas fa-trash-alt"></i> عمود 3</th>
                        <th> <i class="fas fa-edit"></i> <i class="fas fa-trash-alt"></i> عمود 4</th>
                    </tr>
                    <tr>
                        <td>
                            <div class="output-content-holder">
                                <select class="newpage-select">
                                    <option>القيمة الاولى</option>
                                    <option>القيمة الثانية</option>
                                    <option>القيمة الثالثة</option>
                                </select>
                            </div>
                        </td>
                        <td>
                            <div class="output-content-holder"><input type="date" class="newpage-input"></div>
                        </td>

                        <td>
                            <div class="output-content-holder"><input type="number" class="newpage-input"></div>
                        </td>

                        <td>
                            <div class="output-content-holder"><input type="text" class="newpage-input"></div>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <div class="output-content-holder">
                                <select class="newpage-select">
                                    <option>القيمة الاولى</option>
                                    <option>القيمة الثانية</option>
                                    <option>القيمة الثالثة</option>
                                </select>
                            </div>
                        </td>
                        <td>
                            <div class="output-content-holder"><input type="date" class="newpage-input"></div>
                        </td>

                        <td>
                            <div class="output-content-holder"><input type="number" class="newpage-input"></div>
                        </td>

                        <td>
                            <div class="output-content-holder"><input type="text" class="newpage-input"></div>
                        </td>

                    </tr>
                </table>

                <p class="hint-for-backend">عند الضغط على تعديل عمود</p>

                <div class="popup-field-holder">
                    <label class="label">
                        الوصف انجليزي
                    </label>
                    <input type="text" class="input">
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        الوصف عربي
                    </label>
                    <input type="text" class="input">
                </div>

                <div class="popup-field-holder">
                    <label class="label">
                        أداة التحكم
                    </label>
                    <select class="select">
                        <option>مربع نص</option>
                        <option>قائمة منسدلة </option>
                        <option>مربع تحقيق </option>
                        <option>اختيار متعدد </option>
                        <option>عنوان </option>
                        <option>صورة </option>
                        <option>موقع بالخريطة </option>
                        <option>رابط </option>
                        <option>خط فاصل </option>
                    </select>
                </div>
                <div class="popup-field-holder-two">
                    <div class="popup-field-holder">
                        <label class="label" for="checkrequired">
                            مطلوب
                        </label>
                        <input type="checkbox" class="input-checkbox" id="checkrequired">
                    </div>

                    <div class="popup-field-holder">
                        <label class="label" for="checkvisible">
                            مرئي
                        </label>
                        <input type="checkbox" checked="checked" class="input-checkbox" id="checkvisible">
                    </div>
                </div>


                <a class="btn-main-full">إضافة</a>
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
                                <li class="tab-link current" data-tab="tab-1"> حقل النموذج</li>
                                <li class="tab-link" data-tab="tab-2"> الرقم التسلسلي</li>
                                <li class="tab-link" data-tab="tab-3"> معلومات المستخدم الحالي</li>
                                <li class="tab-link" data-tab="tab-4"> التاريخ الحالي</li>
                                <li class="tab-link" data-tab="tab-5"> مهام النص</li>
                                <li class="tab-link" data-tab="tab-6"> كتابة قيمة</li>
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
                                    <i class="fas fa-check btn-value-enter"></i>
                                </div>
                            </div>

                        </div>


                    </div>


                </div>


                <a class="btn-main-full">حفظ</a>
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

            <!-- popup to add-new-form -->

            <div class="popup-screen popup-newForm">

                <div class="popup-holder">
                    <div class="popup-head">
                        <span class="popup-title">اضافة نموذج</span>
                        <i class="fas fa-times close-popup"></i>
                    </div>
                    <div class="popup-content">
                        <div class="popup-field-holder">
                            <label class="label">
                                رقم
                            </label>
                            <input type="num" class="input">
                        </div>
        <div class="popup-field-holder">
                            <label class="label">
                                الوصف انجليزي
                            </label>
                            <input type="text" class="input">
                        </div>
        
                        <div class="popup-field-holder">
                            <label class="label">
                                الوصف عربي
                            </label>
                            <input type="text" class="input">
                        </div>
        
                        <div class="popup-field-holder">
                            <label class="label">مسار العمل الافتراضي</label>
                            <select class="select">
                                <option>مربع نص</option>
                                <option>قائمة منسدلة </option>
                                <option>مربع تحقيق </option>
                                <option>اختيار متعدد </option>
                                <option>عنوان </option>
                                <option>صورة </option>
                                <option>موقع بالخريطة </option>
                                <option>رابط </option>
                                <option>جدول </option>
                                <option>خط فاصل </option>
                            </select>
                        </div>
                        <div class="popup-field-holder-two">
                            <div class="popup-field-holder" style="
            /* width: 100%; */
            margin: 0 15px 16px;
        ">
                                <label class="label" for="checkrequired">فعال</label>
                                <input type="checkbox" class="input-checkbox" id="checkrequired">
                            </div>
        
                            
                        </div>
        
                        <a class="btn-main-full">إضافة</a>
                    </div>
                </div>
            </div>

</asp:Content>