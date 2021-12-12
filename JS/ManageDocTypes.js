var False = false;
var True = true;
var defaultTextLang = 'ar';
var addEmptyRows = 0;
//window.onload = function () {
//    document.onclick = function (e) {
//        if (e.target.className !== 'fas fa-ellipsis-v btn-view-option' && e.target.className !== 'fas fa-ellipsis-v btn-view-option view-option-customPermission' && e.target.className !== 'btn-edit' && e.target.className !== 'btn-delete' && e.target.className !== 'btn-setting' && e.target.className !== 'btn-hide' && e.target.className !== 'btn-hide active') {
//            //element clicked wasn't the div; hide the div
//            $('.btn-view-option').closest('.resize-item-holder').removeClass('open-option');

//        }
//    };
//};
$(document).on("click", ".btn-view-option", function (e) {
    //alert(e.target.className);
    if ($(this).closest('.resize-item-holder').hasClass('open-option') == true) {
        //element clicked wasn't the div; hide the div
        $(this).closest('.resize-item-holder').removeClass('open-option');
    }
    else {
        $(this).closest('.resize-item-holder').addClass('open-option');
    }
});
//$(document).on("click", ".add-new-value", function (e) {
//    $('.default-values-holder .value-item').removeClass('value-item-active');
//    $(".add-new-value").before('<div class="value-item value-item-active"> <i class="fas fa-times-circle btn-remove-value"></i> <span class="value-name" value="">اختر القيمة</span> </div>');
//});
$(function () {

    'use strict';

    // open more options

    $('.btn-view-option').click(function () {
        if ($(this).closest('.resize-item-holder').hasClass('open-option')) {
            $(this).closest('.resize-item-holder').removeClass('open-option');
        } else {
            $('.resize-item-holder').removeClass('open-option');
            $(this).closest('.resize-item-holder').addClass('open-option');
        }
    });
    // Popup add-new-form
    $('#addNew').click(function () {
        debugger;
        $('#divList').fadeOut();
        $('#divAdd').fadeIn();
        $('.new-page-section').fadeOut();
        $('#lblFormMode').text("اضافة نموذج");
        $("#slctDefaultWF").val("-32768");
        $('#DocTypeNumber').val("");
        $('#DocTypeDesc').val("");
        $('#DocTypeDescAr').val("");
        $('#docTypeIsActive').prop('checked', 'false');
    });

    $('#btnCancel').click(function () {
        $('#divList').fadeIn();
        $('#divAdd').fadeOut();
        $('.new-page-section').fadeOut();
    })

    // Popup add fields
    //$('.add-new-field').click(function () {
    //    clearPopupData();
    //    $('.popup-screen.popup-fields').attr('id', $(this).attr('id'));
    //    $('.popup-screen.popup-fields').fadeIn();


    //});

    $('.btn-preview .popup-head i.close-popup, .popup-fields .popup-head i.close-popup').click(function () {
        $('.popup-screen').attr('id', 0);
        $('.popup-screen').fadeOut();
    });

    // Popup prevew fields
    $('.btn-preview').click(function () {
        var previewcontent = $('.holder-newpage .preview-holder').html();
        $(".popup-preview .popup-content-preview").html(previewcontent);
        $('.popup-content-preview .resize-item-holder .item-option,.popup-content-preview .add-new-field, .popup-content-preview .ui-resizable-handle,.popup-content-preview  .resize-item-holder-hidden').remove();
        $('.popup-preview').fadeIn();
    });

    // Popup defaultText
    $('.btn-default-text').click(function () {
        defaultTextLang = $(this).attr('data-id');
        $(".default-values-holder").html('');
        //$('.popup-screen.popup-defaultText').fadeIn();
        $(".popup-defaultText").modal('show');
        if ($("#pInputDefaultTexts").val() != "") {
            var defText = $("#pInputDefaultTexts").val();
            var defDev = $("#pInputDefaultTexts").attr("data-val").replace('#expr:this =', '');
            var collection = defText.split('+');
            var collectiondev = defDev.split('+');
            var html = '';
            for (var i = 0; i < collection.length; i++) {
                if (i == 0) {
                    html += '<div class="value-item value-item-active"> <i class="fas fa-times-circle btn-remove-value"></i> <span class="value-name" value="' + collectiondev[i].replace(/'/g, '"') + '">' + collection[i].replace(/'/g, '"') + '</span> </div>';
                }
                else {
                    html += '<div class="value-item"> <i class="fas fa-times-circle btn-remove-value"></i> <span class="value-name" value="' + collectiondev[i].replace(/'/g, '"') + '">' + collection[i].replace(/'/g, '"') + '</span> </div>';
                }
            }
            $(".default-values-holder").html(html + '<span class="add-new-value">+</span>');
        }
        else {
            $(".default-values-holder").html('<div class="value-item value-item-active"> <i class="fas fa-times-circle btn-remove-value"></i> <span class="value-name" value="">اختر القيمة</span> </div> <span class="add-new-value">+</span>');
        }
        $('.add-new-value').on("click", function () {
            $('.default-values-holder .value-item').removeClass('value-item-active');
            $(".add-new-value").before('<div class="value-item value-item-active"> <i class="fas fa-times-circle btn-remove-value"></i> <span class="value-name" value="">اختر القيمة</span> </div>');
            //$('.default-values-holder .value-item').click(function () {
            //    $('.default-values-holder .value-item').removeClass('value-item-active')
            //    $(this).addClass('value-item-active');
            //});

            // $('.add-new-value').hide();
            ////// Remove item
            $('.value-item .btn-remove-value').click(function () {
                $(this).closest('.value-item ').remove();
            });

        });
        ////// Remove item
        $('.value-item .btn-remove-value').click(function () {
            $(this).closest('.value-item ').remove();
        });

    });
    $('.popup-defaultText').on('show.bs.modal', function (e) {
        // do something...
        //alert('modal show');
        var collection = $("#divMetaBody").find(".resize-item-customPermission");
        var html = '';
        for (var i = 0; i < collection.length; i++) {
            html += '<span class="value-name-setting" value="' + $(collection[i]).attr("id") + '" onclick="OnClickvalueNameSetting(this);">' + $(collection[i]).find('.newpage-label').html() + '</span>';
        }
        $("#tab-1").html(html);
    });
    $('.popup-defaultText .popup-head i.close-popup').click(function () {
        $(".popup-defaultText").modal('hide');
        //$('.popup-defaultText').fadeOut();
    });


    // Popup terms
    $('.resize-item-holder .item-option span.btn-setting').click(function () {
        $('.popup-screen.popup-terms').fadeIn();
    });

    $('.popup-terms .popup-head i.close-popup').click(function () {
        $('.popup-terms').fadeOut();
    });

    $('.check-write').change(function () {
        if ($(this).prop('checked')) {
            $('.check-read').prop('checked', true);
        } else {
            $('.check-read').prop('checked', false);
        }
    });
    $('.check-write').trigger('change');


    // Popup close when click outside

    $('.popup-screen').click(function () {
        $(this).fadeOut();
    });

    $('.popup-screen .popup-content').click(function (e) {
        e.stopPropagation();
    });



    // hide item field
    $('.resize-item-holder .item-option .btn-hide').click(function () {
        $(this).toggleClass('active').closest('.resize-item-holder').toggleClass('resize-item-holder-hidden');

    });


    //// tabs of setting default text

    $('.list-tabs-holder ul.tabs li').click(function () {
        var tab_id = $(this).attr('data-tab');

        $('.list-tabs-holder ul.tabs li').removeClass('current');
        $('.value-tabs-holder .tab-content').removeClass('current');

        $(this).addClass('current');
        $("#" + tab_id).addClass('current');
    });

    $('.value-settings-section .value-name-setting').click(function () {
        $('.value-settings-section .value-name-setting').removeClass('value-setting-active');
        $(this).addClass('value-setting-active');
    })

    //// add default text

    $('.add-new-value').on("click", function () {
        $('.default-values-holder .value-item').removeClass('value-item-active');
        $(".add-new-value").before('<div class="value-item value-item-active"> <i class="fas fa-times-circle btn-remove-value"></i> <span class="value-name" value="">اختر القيمة</span> </div>');


        //$('.default-values-holder .value-item').click(function () {
        //    $('.default-values-holder .value-item').removeClass('value-item-active')
        //    $(this).addClass('value-item-active');
        //});

        // $('.add-new-value').hide();

        ////// Remove item
        $('.value-item .btn-remove-value').click(function () {
            $(this).closest('.value-item ').remove();
        });
    });

    //// add value from setting to item

    $(".value-settings-section .value-name-setting").click(function () {
        var value = $(this).attr('value');
        $(".default-values-holder .value-item.value-item-active .value-name").attr("value", value);
        var text = $(this).text();
        $(".default-values-holder .value-item.value-item-active .value-name").text(text);
        $('.add-new-value').show();
    });

    //////  add value from input to item

    $(".value-enter-setting .btn-value-enter").click(function () {
        var value = $('.input-value-enter').val();
        $(".default-values-holder .value-item.value-item-active .value-name").attr("value", value);
        $(".default-values-holder .value-item.value-item-active .value-name").text('"' + value + '"');
        $('.add-new-value').show();
        $('.input-value-enter').val('');
    });

    $('.default-values-holder .value-item').click(function () {
        $('.default-values-holder .value-item').removeClass('value-item-active')
        $(this).addClass('value-item-active');
    });


    ////////////// End add default text 

    ////// Remove item

    $('input[type=radio][name=premessionType]').change(function () {
        var popupId = $('.popup-screen.popup-terms').attr('id')
        var metaID = GetMetaId(popupId);
        if (this.value == 'Inherit') {
            $('#divInheritPermission').show();
            $('#divCustomPermission').hide();
            $('#slctGroups').removeAttr('onchange');
            $('#slctGroups').removeAttr('change');
            $('.trMetaInheritPermission').remove();
            $('#slctUsers').val(0);
            $('#slctGroups').val('0');
            GetMetaInheritPermissions(metaID)
        }
        else if (this.value == 'Custom') {
            $('#divCustomPermission').show();
            $('#divInheritPermission').hide();
            GetMetaCustomPermissions(metaID);
        }
    });

    $('#slctGroups').change(function () {
        var popupId = $('.popup-screen.popup-terms').attr('id')
        var metaID = GetMetaId(popupId);
        onSlctPermission(metaID, $("#slctGroups option:selected").html(), $(this).val(), 'g');
    });

    $('#slctUsers').change(function () {
        var popupId = $('.popup-screen.popup-terms').attr('id')
        var metaID = GetMetaId(popupId);
        onSlctPermission(metaID, $("#slctUsers option:selected").html(), $(this).val(), 'u');
    });

});
function OnClickvalueNameSetting(xthis) {
    var value = $(xthis).attr('value');
    $(".default-values-holder .value-item.value-item-active .value-name").attr("value", value);
    var text = $(xthis).text();
    $(".default-values-holder .value-item.value-item-active .value-name").text(text);
    $('.add-new-value').show();
    $(".value-name-setting").removeClass('value-setting-active');
    $(xthis).addClass('value-setting-active');
}
function callSortable() {
    // sortable

    $(".sortable-holder").sortable({
        connectWith: ".sortable-holder",
        cancel: ".append-item,.btn-view-option",
        items: ".resize-item-holder:not(.unsortable)",
        placeholder: 'ui-sortable-placeholder',
        receive: function (event, ui) {
            debugger;
            // so if > 6
            if ($(this).children('.resize-item-holder').length > 6) {
                $(ui.sender).sortable('cancel');
            }
            var collection = $(".row-field-holder");
            for (let index = 0; index < collection.length; index++) {
                const element = collection[index];
                if ($(element).find('.resize-item-holder').length == 0) {
                    $(element).remove();
                }
                else if ($(element).find('.resize-item-holder').length >= 6) {
                    $(element).addClass('full-row');
                } else if ($(element).find('.resize-item-holder').length < 6) {
                    $(element).removeClass('full-row');
                }
            }
        },
        start: function (event, ui) {
           // debugger;
            $(".ui-sortable-placeholder").css({
                'width': $(ui['item']).css('width'),
                'height': $(ui['item']).css('height')
            });
        }
    });
    $(".sortable-holder").disableSelection();


    // resizable


    var container = $(".holder-newpage");
    //var numberOfCol = 3;
    //$(".resize-item-holder").css('width', 100 / numberOfCol + '%');

    var sibTotalWidth;
    $('.resize-item-holder').resizable({
        containment: ".sortable-holder",
        handles: "w, e",
        start: function (event, ui) {
            sibTotalWidth = ui.originalSize.width + ui.originalElement.next().outerWidth();
        },
        stop: function (event, ui) {
            var cellPercentWidth = 100 * ui.originalElement.outerWidth() / container.innerWidth();
            $(this).attr('id', ChangeWidth($(this).attr('id'), cellPercentWidth));
            ui.originalElement.css('width', cellPercentWidth + '%');
            var nextCell = ui.originalElement.next();
            var nextPercentWidth = 100 * nextCell.outerWidth() / container.innerWidth();
            nextCell.css('width', nextPercentWidth + '%');
            $(nextCell).attr('id', ChangeWidth($(nextCell).attr('id'), nextPercentWidth));
        },
        resize: function (event, ui) {
            ui.originalElement.next().width(sibTotalWidth - ui.size.width);
            ui.position.left = ui.originalPosition.left;
            ui.size.width = (ui.size.width -
                ui.originalSize.width) +
                ui.originalSize.width;
            if (ui.size.width < 400) {
                $('.sortable-holder').find('.ui-resizable-resizing').addClass('small-item');
            } else {
                $('.sortable-holder').find('.ui-resizable-resizing').removeClass('small-item');
            }
        },
    });


}
function callAddNewFiled(xthis) {
    clearPopupData();
    $('.popup-screen.popup-fields').attr('id', $(xthis).attr('id'));
    $('.popup-screen.popup-fields').fadeIn();
}
$(document).ready(function () {
     callSortable();
    //PageListener();
    //$('.resize-item-customPermission').each(function () {
    //    alert($(this).find('.ui-resizable-handle.ui-resizable-w').val());//.addClass('resize-item-holder-customPermission');
    //});
});
var metas;
var documentTypeId = "";
function onSlctControlChange(slctControl) {


    if (slctControl) {
        var selectedValue = slctControl.value;
        ShowDivCtrl(selectedValue);

    } else {
        $(".ctrlDiv").hide();
    }
}
function ShowDivCtrl(selectedValue) {
    $(".ctrlDiv").hide();

    if (selectedValue == 2 || selectedValue == 3 || selectedValue == 4) {
        divCtrlItemList
        $("#divCtrlItemList").show();
    }
    else {
        if (selectedValue == 7) {
            myMap();
        }
        $("#divCtrlID" + selectedValue).show();
    }
}
function SaveMeta() {
    var dataRequest = GetSaveMetaRequest();
    var pctrlID = document.getElementById('pctrlID');
    var ctrlID = pctrlID.options[pctrlID.selectedIndex].value;
    if (dataRequest) {
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "manageDocTypes.aspx/SaveMeta",
            data: dataRequest,
            dataType: "json",
            success: function (response) {

                if (response.d.State) {
                    if (ctrlID == 8) {
                        saveImage(response.d.Result);
                    }
                    onClickDocTypeMetas();
                    clearPopupData();

                }
                else {
                    alert(response.d.Description);
                }

            },
            error: function (error) {
                alert(error.d);

            }
        });

    }
}
function GetSaveMetaRequest() {

    var popupId = $('.popup-screen.popup-fields').attr('id');
    var metaID = 0;

    var metaDesc = document.getElementById('pmetaDesc').value;
    var metaDescAr = document.getElementById('pmetaDescAr').value;

    var pDataType = document.getElementById('pDataType');
    var metaDataType = pDataType.options[pDataType.selectedIndex].value;

    var required = document.getElementById('prequired').checked;
    var visible = document.getElementById('pvisible').checked;

    var orderSeq = 0;

    var pctrlID = document.getElementById('pctrlID');
    var ctrlID = pctrlID.options[pctrlID.selectedIndex].value;

    var defaultTexts = "";
    var defaultValues = "";
    var defaultArTexts = "";
    if (defaultTexts != '') {
        defaultValues = $("#pInputDefaultTexts").attr("data-val");
    }
    if (defaultArTexts != '') {
        defaultValues = $("#pInputDefaultArTexts").attr("data-val");
    }
    var columnSeq = 0;
    var metaIdFK = 0;
    var width = 0;
    var permissionType = "Inherit";
    var isNewRow = false;
    var isNewColumn = false;
    if (popupId.startsWith("newRow")) {
        isNewRow = true;
        isNewColumn = true;
        orderSeq = Number(popupId.substring(6));
        columnSeq = 0;
    }
    if (popupId.startsWith("Row") && popupId.includes("newColumn")) {
        isNewRow = false;
        isNewColumn = true;
        var strPoup = popupId.split("newColumn");
        orderSeq = Number(strPoup[0].substring(3));
        columnSeq = Number(strPoup[1]);
    }
    if (popupId.startsWith('metaID')) {
        debugger;
        metaID = GetMetaId(popupId);
        metaIdFK = GetMetaIdFk(popupId);
        var objMeta = GetMetaObj(metaID, metaIdFK);

        orderSeq = objMeta.orderSeq;
        columnSeq = objMeta.columnSeq;
        permissionType = objMeta.permissionType;
    }
    if (ctrlID == 1) {
        defaultTexts = $("#pInputDefaultTexts").val();
        defaultValues = "";
        defaultArTexts = $("#pInputDefaultArTexts").val();
        if (defaultTexts != '') {
            defaultValues = $("#pInputDefaultTexts").attr("data-val");
        }
        else {
            defaultValues = $("#pInputDefaultArTexts").attr("data-val");
        }
        //defaultTexts = document.getElementById('pInputDefaultTexts').value;
        //defaultArTexts = document.getElementById('pInputDefaultArTexts').value;
    }
    else if (ctrlID == 2 || ctrlID == 3 || ctrlID == 4) {

        var divCtrlItems = document.getElementById('divCtrlItems');
        if (divCtrlItems.children.length > 0) {
            defaultTexts = "";
            defaultValues = "";
            defaultArTexts = "";

            for (var i = 0; i < divCtrlItems.children.length; i++) {
                var rowItem = divCtrlItems.children[i];
                if (isHasRealValue(defaultTexts)) {
                    defaultTexts = defaultTexts + ';';
                }
                if (isHasRealValue(defaultArTexts)) {
                    defaultArTexts = defaultArTexts + ';';
                }
                if (isHasRealValue(defaultValues)) {
                    defaultValues = defaultValues + ';'
                }
                defaultTexts = defaultTexts + rowItem.children[0].children[1].value;
                defaultArTexts = defaultArTexts + rowItem.children[1].children[1].value;
                if (isHasRealValue(rowItem.children[2].children[1].value)) {
                    defaultValues = defaultValues + rowItem.children[2].children[1].value;
                } else {
                    defaultValues = defaultTexts;
                }

            }

        }
    }
    else if (ctrlID == 6) {
        var tblRowNumber = document.getElementById('tblRowNumber').value;
        var tblColumnNumber = document.getElementById('tblColumnNumber').value;
        defaultTexts = 'tblRowNumber_' + tblRowNumber + ',' + 'tblColumnNumber_' + tblColumnNumber;
        defaultArTexts = 'tblRowNumber_' + tblRowNumber + ',' + 'tblColumnNumber_' + tblColumnNumber;
    }
    else if (ctrlID == 7) {
        var lat = document.getElementById('lat').value;
        var lng = document.getElementById('lng').value;
        defaultTexts = lat + ',' + lng;
        defaultArTexts = lat + ',' + lng;
    }
    else if (ctrlID == 9) {
        defaultTexts = document.getElementById('pLink').value;
        defaultArTexts = document.getElementById('pLink').value;
    }
    var dataRequest = '{"metaID" :' + metaID + ', "docTypID" :' + documentTypeId + ', "metaDesc" :"' + metaDesc + '", "metaDataType" : "' + metaDataType + '", "required" :' + required +
        ',"orderSeq" :' + orderSeq + ', "ctrlID" :' + ctrlID + ', "defaultTexts" : "' + defaultTexts + '" , "defaultValues" : "' + defaultValues + '", "visible" :' + visible
        + ', "metaDescAr" : "' + metaDescAr + '" , "defaultArTexts" : "' + defaultArTexts + '" , "columnSeq" :' + columnSeq + ',"metaIdFK":' + metaIdFK + ', "width":' + width + ', "permissionType":"' + permissionType + '","isNewRow" :' + isNewRow + ',"isNewColumn" :' + isNewColumn + '}';
    return dataRequest;
}
function GetMetaObj(metaID, metaIdFK) {
    if (metaIdFK > 0) {
        var objTableMeta = metas.filter(function (m) {
            return m.metaID == metaIdFK;
        })[0];
        return objTableMeta.tableCtrls.filter(function (m) {
            return m.metaID == metaID;
        })[0];
    }
    else {
        return metas.filter(function (m) {
            return m.metaID == metaID;
        })[0];
    }


}
function onClickSaveDocType() {
    var pctrlID = document.getElementById('slctDefaultWF');
    var defaultWFId = pctrlID.options[pctrlID.selectedIndex].value;

    var docTypeId = GetNumber($('#DocTypeNumber').val());
    var docTypeDesc = GetRealValue($('#DocTypeDesc').val());
    var docTypeDescAr = GetRealValue($('#DocTypeDescAr').val());
    var docTypeIsActive = document.getElementById('docTypeIsActive').checked;
    if (docTypeDesc != "" && docTypeDescAr != "") {
        var dataRequest = '{"docTypeId" :' + docTypeId + ', "docTypeDesc" :"' + docTypeDesc + '", "docTypeDescAr" :"' + docTypeDescAr + '" , "defaultWFId" :' + defaultWFId + ', "docTypeIsActive" :' + docTypeIsActive + '}';
        if (dataRequest) {
            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: "manageDocTypes.aspx/SaveDocType",
                data: dataRequest,
                dataType: "json",
                success: function (response) {
                    if (response.d.State) {
                        var msg = lang == 'ar' ? 'تم الحفظ' : "Successfully saved";
                        bootbox.alert(msg);
                        //location.reload();
                        onClickDocTypeMetas(response.d.Result);
                    }
                    else {
                        alert(response.d.Description);
                    }

                },
                error: function (error) {
                    alert(error.d);

                }
            });

        }
    }
    else {
        var color = "red";
        if (docTypeDesc == "")
            $('#DocTypeDesc').css('border-color', color);
        if (docTypeDescAr == "")
            $('#DocTypeDescAr').css('border-color', color);
    }
}
function onClickDocTypeMetas(docTypeId) {
    debugger;
    $('#divMetaBody').empty();
    if (docTypeId) {
        documentTypeId = docTypeId;
    }
    GetDocType(documentTypeId);
    var dataRequest = '{"documentTypeId" :' + documentTypeId + '}';
    if (documentTypeId && documentTypeId > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "manageDocTypes.aspx/GetDocTypeMetas",
            data: dataRequest,
            dataType: "json",
            success: function (response) {
              
                ViewMetas(response.d);
                DisplayMap();
            },
            error: function (error) {
                alert(error.d);

            }
        });

    }
}
function GetDocType(documentTypeId) {
    var dataRequest = '{"documentTypeId" :' + documentTypeId + '}';
    $.ajax({
        type: "POST",
        contentType: "application/json;charset=utf-8",
        url: "manageDocTypes.aspx/GetDocType",
        data: dataRequest,
        dataType: "json",
        success: function (response) {

            if (response.d.State) {
                loadDocTypePopup(response.d.Result);
            }
            else {
                alert(response.d.Description);
            }

        },
        error: function (error) {
            alert(error.d);

        }
    });
}
function GetMetaCustomPermissions(metaId) {
    debugger;
    var dataRequest = '{"metaId" :' + metaId + '}';
    $.ajax({
        type: "POST",
        contentType: "application/json;charset=utf-8",
        url: "manageDocTypes.aspx/GetMetaCustomPermissions",
        data: dataRequest,
        dataType: "json",
        success: function (response) {
            if (response.d.State) {
                for (var i = 0; i < response.d.Result.length; i++) {
                    var typeName = '';
                    if (response.d.Result[i].PerType == 'u')
                        typeName = isLangEnglish ? "User" : "مستخدم";
                    else
                        typeName = isLangEnglish ? "Group" : "مجموعة";
                    CreateTr(metaId, response.d.Result[i].Name, response.d.Result[i].ID, response.d.Result[i].PerType, typeName, response.d.Result[i].AllowRead, response.d.Result[i].AllowEdit);
                }
            }
            else {
                alert(response.d.Description);
            }
        },
        error: function (error) {
            alert(error.d);

        }
    });
}
function GetMetaInheritPermissions(metaId) {
    debugger;
    var dataRequest = '{"metaId" :' + metaId + '}';
    $.ajax({
        type: "POST",
        contentType: "application/json;charset=utf-8",
        url: "manageDocTypes.aspx/GetMetaInheritPermissions",
        data: dataRequest,
        dataType: "json",
        success: function (response) {
            if (response.d.State) {
                for (var i = 0; i < response.d.Result.length; i++) {
                    var typeName = '';
                    if (response.d.Result[i].PerType == 'u')
                        typeName = isLangEnglish ? "User" : "مستخدم";
                    else
                        typeName = isLangEnglish ? "Group" : "مجموعة";
                    CreateInheritPermissionTr(metaId, response.d.Result[i].Name, response.d.Result[i].ID, response.d.Result[i].PerType, typeName, response.d.Result[i].AllowRead, response.d.Result[i].AllowEdit);
                }
            }
            else {
                alert(response.d.Description);
            }
        },
        error: function (error) {
            alert(error.d);

        }
    });
}
function GetMetaInheritToCustomPermissions(metaId) {
    debugger;
    var dataRequest = '{"metaId" :' + metaId + '}';
    $.ajax({
        type: "POST",
        contentType: "application/json;charset=utf-8",
        url: "manageDocTypes.aspx/GetMetaInheritPermissions",
        data: dataRequest,
        dataType: "json",
        success: function (response) {
            if (response.d.State) {
                for (var i = 0; i < response.d.Result.length; i++) {
                    var typeName = '';
                    if (response.d.Result[i].PerType == 'u')
                        typeName = isLangEnglish ? "User" : "مستخدم";
                    else
                        typeName = isLangEnglish ? "Group" : "مجموعة";
                    CreateTr(metaId, response.d.Result[i].Name, response.d.Result[i].ID, response.d.Result[i].PerType, typeName, response.d.Result[i].AllowRead, response.d.Result[i].AllowEdit);
                }
            }
            else {
                alert(response.d.Description);
            }
        },
        error: function (error) {
            alert(error.d);

        }
    });
}
function UpdateInheritToCustomPermissions() {
    $('#divCustomPermission').show();
    $('#divInheritPermission').hide();
    $('#Inherit').removeAttr('checked');
    $("#Custom").attr('checked', 'checked');
    var popupId = $('.popup-screen.popup-terms').attr('id')
    var metaID = GetMetaId(popupId);
    GetMetaInheritToCustomPermissions(metaID);
}
function loadDocTypePopup(docType) {
    debugger;
    $("#slctDefaultWF").val(docType.DefaultWFId);
    $('#DocTypeNumber').val(docType.DocTypeId);

    $('#DocTypeDesc').val(docType.DocTypeDesc);
    $('#DocTypeDescAr').val(docType.DocTypeDescAr);
    $('#docTypeIsActive').prop('checked', docType.DocTypeIsActive);
    $('.max-width-holder').fadeIn();
    $('#divList').fadeOut();
    $('#divAdd').fadeIn();
    $('.new-page-section').fadeIn();
    $('#lblFormMode').text("تعديل نموذج");
}

function ViewMetas(responsemetas) {
    metas = responsemetas;
    var divMetaBody = document.getElementById("divMetaBody");
    var rowNumber = -1;
    var isNewRow = true;
    for (var i = 0; i < responsemetas.length; i++) {
        var obj = responsemetas[i];
        if (rowNumber < obj.orderSeq) {
            isNewRow = true;
        }
        else {
            isNewRow = false;
        }
        var divRowName = 'Row' + rowNumber;
        if (isNewRow) {
            rowNumber += 1;
            divRowName = 'Row' + rowNumber;
            createNewRow(divMetaBody, rowNumber);
        }
        var divRow = document.getElementById(divRowName);
        divRow.appendChild(getCtrlDiv(obj));

    }
    // PageListener();
    callSortable();
}
function createNewRow(divMetaBody, rowNumber) {

    var divRowName = 'Row' + rowNumber;
    var divRow = document.createElement('div');
    divRow.setAttribute('class', 'row-field-holder');
    divRow.appendChild(getDivUnsortable(divRowName));
    divRow.appendChild(getDivSortable(divRowName));
    divRow.appendChild(getLastDivinRow(rowNumber));
    divMetaBody.appendChild(divRow);

    //if (addEmptyRows == 0) {
        //var divRowName2 = 'Row' + ((rowNumber + 1) + 996);
        //var divRow2 = document.createElement('div');
        //divRow2.setAttribute('class', 'row-field-holder');
        //divRow2.appendChild(getDivUnsortable(divRowName2));
        //divRow2.appendChild(getDivSortable(divRowName2));
        //divRow2.appendChild(getLastDivinRow(rowNumber + 1));
        //divMetaBody.appendChild(divRow2);
    //}
}
function getDivSortable(divRowName) {
    var newDivSortable = document.createElement('div');
    newDivSortable.className = "sortable-holder ui-sortable";
    newDivSortable.setAttribute('id', divRowName);

    return newDivSortable;
}
function getDivUnsortable(divRowName) {
    var newDivUnsortable = document.createElement('div');
    newDivUnsortable.className = "add-new-field add-new-field-row unsortable";
    newDivUnsortable.innerHTML = '+';
    newDivUnsortable.id = divRowName + "newColumn0";
    newDivUnsortable.onclick = function () {
        //this.parentElement.removeChild(this);
        callAddNewFiled(this);
    };
    return newDivUnsortable;
}
function getLastDivinRow(rowNumber) {
    var newLastDiv = document.createElement('div');
    newLastDiv.className = "add-new-field append-row";
    newLastDiv.innerHTML = '+';
    newLastDiv.id = "newRow" + (rowNumber + 1);
    newLastDiv.onclick = function () {
        //this.parentElement.removeChild(this);
        callAddNewFiled(this);
    };
    return newLastDiv;
}
function getCtrlDiv(obj) {
    debugger;
    var parentCtrlDiv = document.createElement('div');
    if (obj.visible == false) {
        parentCtrlDiv.className = "resize-item-holder resize-item-holder-hidden";
    } else {
        if (obj.permissionType == "Custom")
            parentCtrlDiv.className = "resize-item-holder resize-item-customPermission css-item-" + obj.metaID +"";
        else
            parentCtrlDiv.className = "resize-item-holder";
    }

    //parentCtrlDiv.id = 'metaID' + obj.metaID + '_o' + obj.orderSeq + '_c' + obj.columnSeq + '_w' + obj.width;

    if (obj.width == 0) {
        obj.width = 30;
    }
    parentCtrlDiv.id = 'metaID' + obj.metaID + '_w' + obj.width;
    parentCtrlDiv.style = "width: " + obj.width + "%;";

    var iDivOption = document.createElement('i');
    if (obj.permissionType == "Custom")
        iDivOption.className = "fas fa-ellipsis-v btn-view-option view-option-customPermission";
    else
        iDivOption.className = "fas fa-ellipsis-v btn-view-option";
    parentCtrlDiv.appendChild(iDivOption);
    parentCtrlDiv.appendChild(getDivCtrlOption(obj));


    parentCtrlDiv.appendChild(createCtrlDiv(obj));

    var addAddNewDiv = document.createElement('div');
    addAddNewDiv.className = "add-new-field append-item";
    addAddNewDiv.innerHTML = '+';
    addAddNewDiv.id = "Row" + obj.orderSeq + "newColumn" + (obj.columnSeq + 1);
    addAddNewDiv.onclick = function () {
        //this.parentElement.removeChild(this);
        callAddNewFiled(this);
    };
    parentCtrlDiv.appendChild(addAddNewDiv);
    return parentCtrlDiv;
}
function getDivCtrlOption(obj) {

    var optionalCtrlDiv = document.createElement('div');
    optionalCtrlDiv.className = "item-option";

    optionalCtrlDiv.appendChild(getOptionButtons("openMetaonPopup(" + obj.metaID + ",0);", "btn-edit", "fas fa-edit", "Edit", "تعديل"));
    optionalCtrlDiv.appendChild(getOptionButtons("onClickDeleteMeta(" + obj.metaID + ");", "btn-delete", "fas fa-trash-alt", "Delete", "حذف"));
    optionalCtrlDiv.appendChild(getOptionButtons("openMetaPermissinOnPopup(" + obj.metaID + ",'" + obj.metaDesc + "','" + obj.metaDescAr + "');", "btn-setting", "fas fa-cog", "Validations", "الصلاحيات"));
    if (obj.visible) {
        optionalCtrlDiv.appendChild(getOptionButtons("onClickHideMeta(" + obj.metaID + ");", "btn-hide", "far fa-eye", "Hide", "إخفاء"));
    } else {
        optionalCtrlDiv.appendChild(getOptionButtons("onClickShowMeta(" + obj.metaID + ");", "btn-hide", "far fa-eye", "Show", "إظهار"));
    }


    optionalCtrlDiv.appendChild(getOptionButtons("onClickDuplicationMeta(" + obj.metaID + ");", "btn-hide", "far fa-clone", "Repeat", "تكرار"));
    return optionalCtrlDiv;
}
function getOptionButtons(onClickFunction, spanClass, iClass, EnDesc, ArDesc) {

    var i = document.createElement('i');
    i.className = iClass;

    var span = document.createElement('span');
    span.className = spanClass;

    if (isHasRealValue(onClickFunction)) {
        span.setAttribute('onclick', onClickFunction);
    }

    span.appendChild(i);
    span.innerHTML = isLangEnglish ? EnDesc : ArDesc;

    return span;
}
function openMetaonPopup(metaID, metaIdFK) {
    debugger;
    var obj = GetMetaObj(metaID, metaIdFK);
    var idfk = GetNumber(metaIdFK);
    var popupID = "metaID" + metaID + "_metaIdFK" + idfk;
    if (idfk > 0) {
        //$("#pctrlID option:[]").attr('disabled', 'disabled');
        $("#pctrlID option[value='5']").attr("disabled", "disabled");
        $("#pctrlID option[value='6']").attr("disabled", "disabled");
        $("#pctrlID option[value='7']").attr("disabled", "disabled");
        $("#pctrlID option[value='8']").attr("disabled", "disabled");
        $("#pctrlID option[value='9']").attr("disabled", "disabled");
    } else {
        $("#pctrlID option[value='5']").removeAttr("disabled");
        $("#pctrlID option[value='6']").removeAttr("disabled");
        $("#pctrlID option[value='7']").removeAttr("disabled");
        $("#pctrlID option[value='8']").removeAttr("disabled");
        $("#pctrlID option[value='9']").removeAttr("disabled");
    }
    $('.popup-screen.popup-fields').attr('id', popupID);
    $('.popup-screen.popup-fields').fadeIn();

    document.getElementById('pmetaDesc').value = obj.metaDesc;
    document.getElementById('pmetaDescAr').value = obj.metaDescAr;

    $("#pctrlID").val(obj.ctrlID);
    //document.getElementById('pctrlID').selectedValue = obj.ctrlID;
    document.getElementById('prequired').checked = obj.required;
    document.getElementById('pvisible').checked = obj.visible;

    $("#pDataType").val(obj.metaDataType);
    //document.getElementById('pDataType').selectedValue = obj.metaDataType;
    ShowDivCtrl(obj.ctrlID);

    if (obj.ctrlID == 1) {
        document.getElementById('pInputDefaultTexts').value = obj.defaultTexts;
        $("#pInputDefaultTexts").attr("data-val", obj.defaultValues);
        document.getElementById('pInputDefaultArTexts').value = obj.defaultArTexts;
        $("#pInputDefaultArTexts").attr("data-val", obj.defaultValues);
    }
    else if (obj.ctrlID == 2 || obj.ctrlID == 3 || obj.ctrlID == 4) {

        var lstText = isLangEnglish ? obj.lstDefaultTexts : obj.lstDefaultArTexts;
        for (var i = 0; i < obj.lstDefaultTexts.length; i++) {
            var text = obj.lstDefaultTexts[i];
            var artext = obj.lstDefaultArTexts[i];
            var value = obj.lstDefaultValues[i];
            AddDivCtrlItem(text, artext, value);
        }
    }
    else if (obj.ctrlID == 6) {
        debugger;
        var rowNum = GetTblRowNumber(obj.defaultTexts);
        var colNum = GetTblColumnNumber(obj.defaultTexts);
        $('#tblRowNumber').val(rowNum);
        $('#tblColumnNumber').val(colNum);
        $('#tblColumnNumber').attr('min', colNum);
    }
    else if (obj.ctrlID == 7) {
        var loc = obj.defaultTexts.split(',');
        var lat = loc[0];
        var lng = loc[1];
        $('#lat').val(lat);
        $('#lng').val(lng);

        addMarker(lat, lng);
    }
    else if (obj.ctrlID == 8) {
        var src = "../MetaUploader/" + obj.defaultTexts;
        $("#myUploadedImg").attr("src", src);
    }
    else if (obj.ctrlID == 9) {
        document.getElementById('pLink').value = obj.defaultTexts;
    }
    hideResizeIfShown(metaID);
}
function hideResizeIfShown(val) {
    var elm = ".css-item-" + val;
    if ($(elm).hasClass('open-option')) {
        $(elm).removeClass('open-option');
    }
}
function openMetaPermissinOnPopup(metaID, metaDesc, metaDescAr) {
    debugger;
    var popupID = "metaID" + metaID;
    var obj = GetMetaObj(metaID, 0);
    $('#PermissinTitle').text("صلاحيات - " + metaDescAr);
    $('.popup-screen.popup-terms').attr('id', popupID);
    $('.popup-screen.popup-terms').fadeIn();
    $('#slctGroups').removeAttr('onchange');
    $('#slctGroups').removeAttr('change');
    $('.trMetaPermission').remove();
    $('.trMetaInheritPermission').remove();
    $('#slctUsers').val(0);
    $('#slctGroups').val('0');
    //document.getElementById("slctGroups").addEventListener("change", function () {
    //    $('#slctUsers').val(0);
    //    onSlctPermission(metaID, $("#slctGroups option:selected").html(), $(this).val(), 'g');
    //});

    //$('#slctUsers').removeAttr('onchange');
    //$('#slctUsers').removeAttr('change');
    //document.getElementById("slctUsers").addEventListener("change", function () {
    //    $('#slctGroups').val('0');
    //    onSlctPermission(metaID, $("#slctUsers option:selected").html(), $(this).val(), 'u');
    //});
    if (obj.permissionType == "Custom") {
        $('#divCustomPermission').show();
        $('#divInheritPermission').hide();
        $('#Inherit').removeAttr('checked');
        $("#Custom").attr('checked', 'checked');
        $("#Custom").change();
        GetMetaCustomPermissions(metaID);
    }
    else {
        $('#divInheritPermission').show();
        $('#divCustomPermission').hide();
        $('#Custom').removeAttr('checked');
        $("#Inherit").attr('checked', 'checked');
        $("#Inherit").change();
        GetMetaInheritPermissions(metaID);
    }
    hideResizeIfShown(metaID);
    //$('input[type=radio][name=premessionType]').change(function () {
    //    if (this.value == 'Inherit') {
    //        $('#divCustomPermission').hide();
    //    }
    //    else if (this.value == 'Custom') {
    //        $('#divCustomPermission').show();
    //    }
    //});
}
function createCtrlDiv(obj) {

    var div = document.createElement('div');

    if (obj.ctrlID == 6) {
        div.className = "item-field-holder edit-table-btns";
    }
    else {
        div.className = "item-field-holder";
    }
    var divContent = document.createElement('div');
    divContent.className = "output-content-holder";

    var label = document.createElement('label');
    label.className = "newpage-label";
    label.innerHTML = isLangEnglish ? obj.metaDesc : obj.metaDescAr;
    divContent.appendChild(label);


    if (obj.ctrlID != 5) {
        CreateCtrlElement(divContent, obj);
    }

    div.appendChild(divContent);
    return div;
}
function CreateCtrlElement(divContent, obj) {


    if (obj.ctrlID == 1) {
        CreateInput(divContent, obj);
    }
    else if (obj.ctrlID == 2) {
        CreateDropDownList(divContent, obj);
    }
    else if (obj.ctrlID == 3) {
        CreateCheckBoxList(divContent, obj);
    }
    else if (obj.ctrlID == 4) {
        CreateRadioList(divContent, obj);
    }
    else if (obj.ctrlID == 6) {
        CreateTable(divContent, obj);
    }
    else if (obj.ctrlID == 7) {
        CreateMap(divContent, obj);
    }
    else if (obj.ctrlID == 8) {
        CreateImage(divContent, obj);
    }
    else if (obj.ctrlID == 9) {
        CreateLink(divContent, obj);
    }
}
function CreateInput(divContent, obj) {
    var input = document.createElement('input');
    input.className = "newpage-input";
    if (obj.metaDataType == "DateTime") {

        input.setAttribute("type", "datetime");
    }
    else if (obj.metaDataType == "Int32" || obj.metaDataType == "Decimal") {
        input.setAttribute("type", "number");
    }
    else {
        input.setAttribute("type", "text");
    }
    input.value = "";
    if (!isLangEnglish) {

        if (obj.defaultArTexts) {
            input.value = obj.defaultArTexts;
        }
    }
    if (input.value == "" || isLangEnglish) {
        input.value = obj.defaultTexts;
    }
    divContent.appendChild(input);
}
function CreateDropDownList(divContent, obj) {
    var lstText = isLangEnglish ? obj.lstDefaultTexts : obj.lstDefaultArTexts;
    var slct = document.createElement('select');
    slct.className = "newpage-select";


    for (var i = 0; i < lstText.length; i++) {
        var text = lstText[i];
        var value = obj.lstDefaultValues.length > i ? obj.lstDefaultValues[i] : text;
        var opt = document.createElement('option');
        opt.value = value;
        opt.innerHTML = text;
        slct.appendChild(opt);
    }
    divContent.appendChild(slct);
}
function CreateCheckBoxList(divContent, obj) {
    var lstText = isLangEnglish ? obj.lstDefaultTexts : obj.lstDefaultArTexts;
    for (var i = 0; i < lstText.length; i++) {

        var text = lstText[i];
        var value = obj.lstDefaultValues.length > i ? obj.lstDefaultValues[i] : text;

        var div = document.createElement('div');
        div.className = "output-check-holder";

        var input = document.createElement('input');
        input.setAttribute("id", "checkbox" + i);
        input.setAttribute("name", "checkbox" + i);
        input.setAttribute("type", "checkbox");
        input.setAttribute("value", value);
        div.appendChild(input);

        var label = document.createElement('label');
        label.setAttribute("for", "checkbox" + i);
        label.innerHTML = text;
        div.appendChild(label);

        divContent.appendChild(div);
    }
}
function CreateRadioList(divContent, obj) {
    var lstText = isLangEnglish ? obj.lstDefaultTexts : obj.lstDefaultArTexts;
    for (var i = 0; i < lstText.length; i++) {

        var text = lstText[i];
        var value = obj.lstDefaultValues.length > i ? obj.lstDefaultValues[i] : text;

        var div = document.createElement('div');
        div.className = "output-check-holder";

        var input = document.createElement('input');
        input.setAttribute("id", "radio" + i);
        input.setAttribute("name", "radio" + obj.metaID);
        input.setAttribute("type", "radio");
        input.setAttribute("value", value);
        div.appendChild(input);

        var label = document.createElement('label');
        label.setAttribute("for", "radio" + i);
        label.innerHTML = text;
        div.appendChild(label);

        divContent.appendChild(div);
    }
}
function CreateTable(divContent, obj) {
    debugger;
    var table = document.createElement('table');
    table.className = "my-table";
    var tbody = document.createElement('tbody');
    var trHeader = document.createElement('tr');
    for (var i = 0; i < obj.tableCtrls.length; i++) {

        var th = document.createElement('th');

        //th.appendChild(getiElement("fas fa-edit", "openMetaonPopup(" + obj.tableCtrls[i].metaID + "," + obj.metaID + ");"));
        //th.appendChild(getiElement("fas fa-trash-alt", "onClickDeleteMeta(" + obj.tableCtrls[i].metaID + ");"));

        var str = '<i class="fas fa-edit" onclick="openMetaonPopup(' + obj.tableCtrls[i].metaID + ',' + obj.metaID + ')"></i><i class="fas fa-trash-alt" onclick="onClickDeleteMeta(' + obj.tableCtrls[i].metaID + ')"></i>'
        str += ' ' + obj.tableCtrls[i].metaDescAr + ' - ' + obj.tableCtrls[i].metaDesc;
        //th.appendChild(getOptionButtons("openMetaonPopup(" + obj.tableCtrls[i].metaID + "," + obj.metaID + ");", "btn-edit", "fas fa-edit", "Edit", "تعديل"));
        //th.appendChild(getOptionButtons("onClickDeleteMeta(" + obj.tableCtrls[i].tableCtrls[i].metaID + ");", "btn-delete", "fas fa-trash-alt", "Delete", "حذف"));
        //th.innerHTML = str + isLangEnglish ? obj.tableCtrls[i].metaDesc : obj.tableCtrls[i].metaDescAr;
        th.innerHTML = str;
        trHeader.appendChild(th);
    }
    tbody.appendChild(trHeader);
    var trNumber = GetTblRowNumber(obj.defaultTexts);
    for (var r = 0; r < trNumber; r++) {
        var tr = document.createElement('tr');
        for (var i = 0; i < obj.tableCtrls.length; i++) {
            var td = document.createElement('td');
            CreateCtrlElement(td, obj.tableCtrls[i]);
            tr.appendChild(td);
        }
        tbody.appendChild(tr);
    }

    table.appendChild(tbody);
    divContent.appendChild(table);
}
function CreateMap(divContent, obj) {
    var divMap = document.createElement('div');
    var mapId = 'map' + obj.metaID;
    divMap.setAttribute('id', mapId);
    divMap.className = "output-map-holder map";
    divMap.innerHTML = obj.defaultTexts
    divMap.style = "width: 100 %; height: 410px";
    //var loc = obj.defaultTexts.split(',');
    //var lat = loc[0];
    //var lng = loc[1];
    //createMetaMap(divMap, lat, lng);

    divContent.appendChild(divMap);
}
function CreateImage(divContent, obj) {
    var img = document.createElement('img');
    img.setAttribute('src', "../MetaUploader/" + obj.defaultTexts);
    img.className = "output-img-holder";
    divContent.appendChild(img);
}
function CreateLink(divContent, obj) {
    var a = document.createElement('a');
    a.setAttribute('href', obj.defaultTexts);
    a.className = "output-link-holder";
    if (isLangEnglish) {
        a.innerHTML = obj.metaDesc;
    }
    else {
        a.innerHTML = obj.metaDescAr;
    }

    divContent.appendChild(a);
}

function Save() {
    addEmptyRows = 1;
    var dataRequest = GetMetasPositionsAndWidth();
    if (dataRequest) {
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "manageDocTypes.aspx/Save",
            data: JSON.stringify({ request: dataRequest }),
            dataType: "json",
            success: function (response) {
                var msg = lang == 'ar' ? 'تم الحفظ' : "Successfully saved";
                bootbox.alert(msg);
                if (response.d == "") {
                    onClickDocTypeMetas();
                }
                else {
                    alert(response.d);
                }

            },
            error: function (error) {
                alert(error.responseText);

            }
        });

    }
}
function GetMetasPositionsAndWidth() {
    let dataList = [];
    const rowDivs = document.getElementsByClassName('sortable-holder ui-sortable');
    for (let i = 0; i < rowDivs.length; i++) {
        var metaDivs = rowDivs[i].children;
        for (var j = 0; j < metaDivs.length; j++) {
            var objMeta = new Object();
            objMeta.orderSeq = i;
            objMeta.columnSeq = j;
            objMeta.metaID = GetMetaId(metaDivs[j].id);
            objMeta.width = GetWidth(metaDivs[j].id);

            dataList.push(objMeta);
        }
    }
    return dataList;

}
function GetMetaId(strId) {
    return Number(strId.split("_")[0].substring(6));
}
function GetMetaIdFk(strId) {
    var lstStr = strId.split("_");
    for (var i = 0; i < lstStr.length; i++) {
        if (lstStr[i].startsWith("metaIdFK")) {
            return Number(lstStr[i].substring(8));
        }
    }
    return 0;
}
function GetWidth(strId) {
    return strId.split("_")[1].substring(1);
}
function ChangeWidth(strId, width) {

    return 'metaID' + GetMetaId(strId) + '_w' + width;
}
function removeDivCtrlItem(divItemId) {

    $("#" + divItemId).remove();

    //var list = document.getElementById(parent);
    //list.removeChild(list.children[indexId]);
}
function AddDivCtrlItem(text, artext, value) {

    var divCtrlItemList = document.getElementById('divCtrlItems');
    var divItemId = 'divCtrlItem' + divCtrlItemList.children.length;
    var divItem = document.createElement('div');
    divItem.className = 'popup-field-holder row-input-holder';
    divItem.setAttribute('id', divItemId);
    if (isLangEnglish) {
        divItem.appendChild(CreateDivRowInputItem('English Text', text));
        divItem.appendChild(CreateDivRowInputItem('Arabic Text', artext));
        divItem.appendChild(CreateDivRowInputItem('Value', value));
    } else {
        divItem.appendChild(CreateDivRowInputItem('النص انجليزي', text));
        divItem.appendChild(CreateDivRowInputItem('النص عربى', artext));
        divItem.appendChild(CreateDivRowInputItem('القيمة', value));
    }


    var aRemove = document.createElement('a');
    aRemove.setAttribute("onclick", 'removeDivCtrlItem("' + divItemId + '");');
    var spanRemove = document.createElement('span');
    spanRemove.className = 'btn-remove-value';

    var iRemove = document.createElement('i');
    iRemove.className = "fas fa-trash";
    spanRemove.appendChild(iRemove);

    aRemove.appendChild(spanRemove);
    divItem.appendChild(aRemove);

    divCtrlItemList.appendChild(divItem);

}
function CreateDivRowInputItem(labelInerrHtml, InputValue) {

    var div = document.createElement('div');
    div.className = 'row-input-items';
    var label = document.createElement('label');
    label.className = 'label';
    label.innerHTML = labelInerrHtml;
    div.appendChild(label);

    var input = document.createElement('input');
    input.className = "input";
    input.setAttribute("type", "text");
    if (InputValue) {
        input.value = InputValue;
    }
    div.appendChild(input);
    return div;
}
var _URL = window.URL || window.webkitURL;
$("#pImage").on('change', function () {

    var file, img;
    if ((file = this.files[0])) {
        img = new Image();
        img.onload = function () {
            //saveImage(file);
        };
        img.onerror = function () {
            alert("Not a valid file:" + file.type);
        };
        img.src = _URL.createObjectURL(file);
        $("#myUploadedImg").prop("src", img.src);
    }
});
function saveImage(metaId) {
    if ($('#pImage')[0].files.length > 0) {
        var formData = new FormData();
        formData.append('file', $('#pImage')[0].files[0]);
        formData.append('metaId', metaId)
        $.ajax({
            type: "POST",
            url: '../MangeForm/FileUploader.ashx?metaId=' + encodeURIComponent(metaId),
            data: formData,
            success: function (status) {
                if (status != 'error') {
                    var my_path = "../MetaUploader/" + status;
                    $("#myUploadedImg").attr("src", my_path);
                }
            },
            processData: false,
            contentType: false,
            error: function () {
                alert("Whoops something went wrong!");
            }
        });
    }
}

function clearPopupData() {

    var popupId = $('.popup-screen.popup-fields').attr('id');
    var popup = document.getElementById(popupId);


    $('.popup-screen.popup-fields').removeAttr('style');

    $('.popup-screen.popup-fields').removeAttr('id');

    document.getElementById('pmetaDesc').value = "";
    document.getElementById('pmetaDescAr').value = "";

    document.getElementById('pctrlID').selectedIndex = "0";
    document.getElementById('prequired').checked = false;
    document.getElementById('pvisible').checked = true;

    document.getElementById('pDataType').selectedIndex = "0";
    document.getElementById('pInputDefaultTexts').value = "";
    document.getElementById('pInputDefaultArTexts').value = "";
    document.getElementById('divCtrlItems').innerHTML = ''
    document.getElementById('lat').value = ''
    document.getElementById('lng').value = ''
    document.getElementById('map').innerHTML = '24.774265,46.738586';
    $("#myUploadedImg").prop("src", '#');

    $('#pImage').val("");
    $('#pLink').val("");
    $(".ctrlDiv").hide();

    $("#pctrlID option[value='5']").removeAttr("disabled");
    $("#pctrlID option[value='6']").removeAttr("disabled");
    $("#pctrlID option[value='7']").removeAttr("disabled");
    $("#pctrlID option[value='8']").removeAttr("disabled");
    $("#pctrlID option[value='9']").removeAttr("disabled");
}
function isHasRealValue(str) {
    if (str && str != '' && str !== '') {
        return true;
    }
    return false;
}

function GetNumber(str) {
    if (isHasRealValue(str)) {
        var num = Number(str);
        if (isNaN(num)) {
            return 0;
        } else {
            return num;
        }
    } else {
        return 0;
    }
}
function GetRealValue(str) {
    if (isHasRealValue(str)) {
        return str;
    } else {
        return '';
    }

}
function onClickDeleteMeta(metaId) {
    var message = isLangEnglish ? "Are you sure to delete this meta?" : "هل انت متأكد من الحذف؟";
    bootbox.confirm({
        message: message,
        locale: lang,
        callback: function (result) {
            if (result) {
                var dataRequest = '{"metaId" :' + metaId + '}';
                if (dataRequest) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "manageDocTypes.aspx/DeleteMeta",
                        data: dataRequest,
                        dataType: "json",
                        success: function (response) {

                            if (response.d.State) {
                                onClickDocTypeMetas();
                                clearPopupData();

                            }
                            else {
                                alert(response.d.Description);
                            }

                        },
                        error: function (error) {
                            alert(error.d);

                        }
                    });

                }
            }
        }
    });
    hideResizeIfShown(metaId);
}
function onClickDuplicationMeta(metaId) {
    var message = isLangEnglish ? "Are you sure to duplicate this meta?" : "هل انت متأكد من التكرار؟";
    if (confirm(message)) {
        var dataRequest = '{"metaId" :' + metaId + '}';
        if (dataRequest) {
            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: "manageDocTypes.aspx/DuplicationMeta",
                data: dataRequest,
                dataType: "json",
                success: function (response) {

                    if (response.d.State) {
                        onClickDocTypeMetas();
                        clearPopupData();

                    }
                    else {
                        alert(response.d.Description);
                    }

                },
                error: function (error) {
                    alert(error.d);

                }
            });

        }
    }
    hideResizeIfShown(metaId);
}
function onClickShowMeta(metaId) {
    var message = isLangEnglish ? "Are you sure to visible this meta?" : "هل انت متأكد من اظهار؟";
    if (confirm(message)) {
        var dataRequest = '{"metaId" :' + metaId + '}';
        if (dataRequest) {
            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: "manageDocTypes.aspx/ShowMeta",
                data: dataRequest,
                dataType: "json",
                success: function (response) {

                    if (response.d.State) {
                        onClickDocTypeMetas();
                        clearPopupData();

                    }
                    else {
                        alert(response.d.Description);
                    }

                },
                error: function (error) {
                    alert(error.d);

                }
            });

        }
    }
}
function onClickHideMeta(metaId) {
    var message = isLangEnglish ? "Are you sure to hide this meta?" : "هل انت متأكد من الاخفاء؟";
    bootbox.confirm({
        message: message,
        locale: lang,
        callback: function (result) {
            if (result) {
                var dataRequest = '{"metaId" :' + metaId + '}';
                if (dataRequest) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "manageDocTypes.aspx/HideMeta",
                        data: dataRequest,
                        dataType: "json",
                        success: function (response) {

                            if (response.d.State) {
                                onClickDocTypeMetas();
                                clearPopupData();

                            }
                            else {
                                alert(response.d.Description);
                            }

                        },
                        error: function (error) {
                            alert(error.d);

                        }
                    });

                }
            }
        }
    });
    hideResizeIfShown(metaId);
}

var popupMap;
function myMap() {
    var mapCanvas = document.getElementById('map');

    var mapOptions = {
        center: { lat: 24.774265, lng: 46.738586 },
        zoom: 6,
        mapTypeId: google.maps.MapTypeId.HYBRID
    }
    popupMap = new google.maps.Map(mapCanvas, mapOptions);



    var clickmarker = new google.maps.Marker({
        draggable: false
    });
    google.maps.event.addListener(popupMap, 'click', function (event) {
        clickmarker.setPosition(event.latLng);
        clickmarker.setMap(popupMap);
        clickmarker.setAnimation(google.maps.Animation.DROP);

        document.getElementById("lat").value = event.latLng.lat();
        document.getElementById("lng").value = event.latLng.lng();
    });

}


function addMarker(lat, lng) {
    if (lat && lng) {

        var myLatLng = new google.maps.LatLng(lat, lng);
        var marker = new google.maps.Marker({
            position: myLatLng,
            map: popupMap
        });
        marker.setMap(popupMap);
    }
}


function DisplayMap() {
    $('.map').each(function (index, Element) {
        debugger;
        var coords = $(Element).text().split(",");
        if (coords.length != 2) {
            $(this).display = "none";
            return;
        }
        var latlng = new google.maps.LatLng(parseFloat(coords[0]), parseFloat(coords[1]));
        var myOptions = {
            zoom: parseFloat(6),
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            disableDefaultUI: false,
            mapTypeControl: true,
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.SMALL
            }
        };
        var pagemap = new google.maps.Map(Element, myOptions);

        var marker = new google.maps.Marker({
            position: latlng,
            map: pagemap
        });
    });
}
function GetTblRowNumber(str) {
    return (str.split(",")[0]).split("_")[1];
}
function GetTblColumnNumber(str) {
    return (str.split(",")[1]).split("_")[1];
}
function getiElement(iClass, onClickFunction) {
    var i = document.createElement('i');
    i.className = iClass;


    if (isHasRealValue(onClickFunction)) {
        i.setAttribute('onclick', onClickFunction);
    }
    return i;
}

function onSlctPermission(metaId, selectedValueText, selectedValueId, type) {
    debugger;
    //$('.trMetaPermission').remove();

    var isAllowRead = false;
    var isAllowEdit = false;
    var typeName = '';
    var dataRequest = '';
    var url = '';

    if (type === 'g') {
        typeName = isLangEnglish ? 'Group' : 'مجموعة';
        dataRequest = '{"metaId" :' + metaId + ',"groupId" :' + selectedValueId + '}';
        url = "manageDocTypes.aspx/GetMetaGroupPermission";
    }
    else if (type === 'u') {
        typeName = isLangEnglish ? 'User' : 'مستخدم';
        dataRequest = '{"metaId" :' + metaId + ',"userId" :' + selectedValueId + '}';
        url = "manageDocTypes.aspx/GetMetaUserPermission";
    }

    if (dataRequest) {
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: url,
            data: dataRequest,
            dataType: "json",
            success: function (response) {

                if (response.d.State) {
                    isAllowRead = response.d.Result.AllowRead;
                    isAllowEdit = response.d.Result.AllowEdit;
                    CreateTr(metaId, selectedValueText, selectedValueId, type, typeName, isAllowRead, isAllowEdit);
                }
                else {
                    alert(response.d.Description);
                }

            },
            error: function (error) {
                alert(error.d);

            }
        });

    }





}
function CreateTr(metaId, selectedValueText, selectedValueId, type, typeName, isAllowRead, isAllowEdit) {
    debugger;
    var id = GetNumber(metaId).toString() + '_' + GetNumber(selectedValueId).toString() + "_" + type.toString();
    if (!trMetaPermissionExist(id)) {
        var tableRef = document.getElementById('tblMetaPermission').getElementsByTagName('tbody')[0];
        var tr = tableRef.insertRow(tableRef.rows.length);


        //var tr = document.createElement('tr');

        tr.className = "trMetaPermission";
        tr.id = id;
        var td1 = document.createElement('td');
        td1.innerHTML = selectedValueText;
        tr.appendChild(td1);
        //
        var td2 = document.createElement('td');
        td2.innerHTML = typeName;
        tr.appendChild(td2);
        //
        var td3 = document.createElement('td');
        if (isAllowRead) {
            td3.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowRead" checked="checked">';
        }
        else {
            td3.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowRead">';
        }
        tr.appendChild(td3);
        //

        var td4 = document.createElement('td');
        if (isAllowEdit) {
            td4.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowWrite" checked="checked">';
        }
        else {
            td4.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowWrite">';
        }
        tr.appendChild(td4);

        //
        var td5 = document.createElement('td');
        td5.innerHTML = '<i class="fas fa-trash-alt" onclick="onClickDeletePermission(' + metaId + ',' + selectedValueId + ',\'' + type + '\')"></i>';

        tr.appendChild(td5);
        //$('#tblMetaPermission').find('tbody').appendChild(tr)
    }
}

function CreateInheritPermissionTr(metaId, selectedValueText, selectedValueId, type, typeName, isAllowRead, isAllowEdit) {
    debugger;
    var id = GetNumber(metaId).toString() + '_' + GetNumber(selectedValueId).toString() + "_" + type.toString();
    if (!trMetaInheritPermissionExist(id)) {
        var tableRef = document.getElementById('tblMetaInheritPermission').getElementsByTagName('tbody')[0];
        var tr = tableRef.insertRow(tableRef.rows.length);


        //var tr = document.createElement('tr');

        tr.className = "trMetaInheritPermission";
        tr.id = id;
        var td1 = document.createElement('td');
        td1.innerHTML = selectedValueText;
        tr.appendChild(td1);
        //
        var td2 = document.createElement('td');
        td2.innerHTML = typeName;
        tr.appendChild(td2);
        //
        var td3 = document.createElement('td');
        if (isAllowRead) {
            td3.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowRead" checked="checked" disabled>';
        }
        else {
            td3.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowRead" disabled>';
        }
        tr.appendChild(td3);
        //

        var td4 = document.createElement('td');
        if (isAllowEdit) {
            td4.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowWrite" checked="checked" disabled>';
        }
        else {
            td4.innerHTML = '<input type="checkbox" class="input-checkbox" id="chkAllowWrite" disabled>';
        }
        tr.appendChild(td4);
        //$('#tblMetaPermission').find('tbody').appendChild(tr)
    }
}

function onClickDeletePermission(metaId, selectedValueId, type) {
    var message = isLangEnglish ? "Are you sure to delete this meta?" : "هل انت متأكد من الحذف؟";
    if (confirm(message)) {
        var url = '';
        var dataRequest = '';
        if (type === 'g') {
            dataRequest = '{"metaId" :' + metaId + ',"groupId" :' + selectedValueId + '}';
            url = "manageDocTypes.aspx/DeleteMetaGroupPermission";
        }
        else if (type === 'u') {
            dataRequest = '{"metaId" :' + metaId + ',"userId" :' + selectedValueId + '}';
            url = "manageDocTypes.aspx/DeleteMetaUserPermission";
        }
        $('table#tblMetaPermission tr#' + metaId + '_' + selectedValueId + '_' + type).remove();
        if (dataRequest) {
            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: url,
                data: dataRequest,
                dataType: "json",
                success: function (response) {

                    if (response.d.State) {
                        //$('.trMetaPermission').remove();
                        $('#slctUsers').val(0); $('#slctGroups').val('0');
                        //GetMetaCustomPermissions(metaId);
                    }
                    else {
                        alert(response.d.Description);
                    }

                },
                error: function (error) {
                    alert(error.d);

                }
            });

        }
    }

}


function onClickSavePermission() {
    debugger;
    var pType = 'Inherit';
    if (document.getElementById('Custom').checked) {
        pType = document.getElementById('Custom').value;
    }
    if (pType === 'Inherit') {

        $.ajax({
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "manageDocTypes.aspx/SaveMetaInheritPermission",
            data: '{"metaId" :' + metaId + '}',
            dataType: "json",
            success: function (response) {

                if (response.d.State) {
                    //alert('Success');
                    //$('#slctUsers').val(0);
                    //$('#slctGroups').val('0');
                    //$('.trMetaPermission').remove();
                    $('.popup-screen.popup-terms').fadeOut();
                }
                else {
                    alert(response.d.Description);
                }

            },
            error: function (error) {
                alert(error.d);

            }
        });


    }
    else {
        debugger;
        $('.trMetaPermission').each(function () {
            var id = $(this).attr('id');
            if (id) {
                var splitIdArr = id.split('_');
                var metaId = splitIdArr[0];
                var selectedValueId = splitIdArr[1];
                var type = splitIdArr[2];
                var allowEdit = $(this).find('#chkAllowWrite').is(":checked");
                var allowRead = $(this).find('#chkAllowRead').is(":checked");
                var url = '';
                var dataRequest = '';

                if (type === 'g') {
                    dataRequest = '{"metaId" :' + metaId + ',"groupId" :' + selectedValueId + ',"allowRead" :' + allowRead + ',"allowEdit" :' + allowEdit + '}';
                    url = "manageDocTypes.aspx/SaveMetaGroupPermission";
                }
                else if (type === 'u') {
                    dataRequest = '{"metaId" :' + metaId + ',"userId" :' + selectedValueId + ',"allowRead" :' + allowRead + ',"allowEdit" :' + allowEdit + '}';
                    url = "manageDocTypes.aspx/SaveMetaUserPermission";
                }
                if (dataRequest) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: url,
                        data: dataRequest,
                        dataType: "json",
                        success: function (response) {

                            if (response.d.State) {
                                //alert('Success');
                                //$('#slctUsers').val(0); $('#slctGroups').val('0');
                                //$('.trMetaPermission').remove();
                                $('.popup-screen.popup-terms').fadeOut();
                            }
                            else {
                                alert(response.d.Description);
                            }

                        },
                        error: function (error) {
                            alert(error.d);

                        }
                    });

                }
            }
        });
        onClickDocTypeMetas();
    }
}

function trMetaPermissionExist(varid) {
    var result = false;
    $('.trMetaPermission').each(function () {
        var id = $(this).attr('id');
        if (id && id == varid) {
            result = true;
            return false;
        }
    });
    return result;
}
function trMetaInheritPermissionExist(varid) {
    var result = false;
    $('.trMetaInheritPermission').each(function () {
        var id = $(this).attr('id');
        if (id && id == varid) {
            result = true;
            return false;
        }
    });
    return result;
}



function setMetaDefault() {
    var defText = "";
    var defvalue = "#expr:this = ";
    var collection = $(".default-values-holder").find('.value-name');
    for (var i = 0; i < collection.length; i++) {
        if (i == 0) { // first index
            defText = $(collection[i]).html().replace(/"/g, "'");
            if ($(collection[i]).html().indexOf('"') != -1) {
                defvalue += "'" + $(collection[i]).attr('value') + "'";
            }
            else {
                defvalue += $(collection[i]).attr('value');
            }

        }
        else {
            defText += '+' + $(collection[i]).html().replace(/"/g, "'");
            if ($(collection[i]).html().indexOf('"') != -1) {
                defvalue += '+' + "'" + $(collection[i]).attr('value') + "'";
            }
            else {
                defvalue += '+' + $(collection[i]).attr('value');
            }
        }
    }
    if (defaultTextLang == 'en') {
        $("#pInputDefaultTexts").val(defText);
        $("#pInputDefaultTexts").attr('data-val', defvalue);
    }
    else {
        $("#pInputDefaultArTexts").val(defText);
        $("#pInputDefaultArTexts").attr('data-val', defvalue);
    }
    $(".popup-defaultText").modal('hide');
    //$('.popup-screen.popup-defaultText').fadeOut();
}