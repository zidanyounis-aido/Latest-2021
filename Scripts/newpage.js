/*global $, console*/



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

    // Popup add fields
    $('.add-new-field').click(function () {
        $('.popup-screen.popup-fields').fadeIn();
    });

    $('.btn-preview .popup-head i.close-popup, .popup-fields .popup-head i.close-popup').click(function () {
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
        $('.popup-screen.popup-defaultText').fadeIn();
    });

    $('.popup-defaultText .popup-head i.close-popup').click(function () {
        $('.popup-defaultText').fadeOut();
    });

    // Popup add-new-form
    $('.add-new-form').click(function () {
        $('.popup-screen.popup-newForm').fadeIn();
    });

    $('.popup-newForm .popup-head i.close-popup').click(function () {
        $('.popup-newForm').fadeOut();
    });

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


        $('.default-values-holder .value-item').click(function () {
            $('.default-values-holder .value-item').removeClass('value-item-active')
            $(this).addClass('value-item-active');
        });

        $('.add-new-value').hide();

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
    $('.value-item .btn-remove-value').click(function () {
        $(this).closest('.value-item ').remove();
    });

    // sortable
    $(function () {
        $(".sortable-holder").sortable({
            connectWith: ".sortable-holder",
            cancel: ".append-item,.btn-view-option",
            items: ".resize-item-holder:not(.unsortable)",
            placeholder: 'ui-sortable-placeholder',
            receive: function (event, ui) {
                // so if > 6
                if ($(this).children('.resize-item-holder').length > 6) {
                    $(ui.sender).sortable('cancel');
                }
                var collection=$(".row-field-holder");
                for (let index = 0; index < collection.length; index++) {
                    const element = collection[index];
                  if($(element).find('.resize-item-holder').length == 0){
                    $(element).remove();
                  }
                    else if ($(element).find('.resize-item-holder').length >= 6) {  
                      $(element).addClass('full-row');
                  } else if  ($(element).find('.resize-item-holder').length < 6) {
                      $(element).removeClass('full-row');
                  }
                  
                    
                }
            },
            start: function (event, ui) {
                $(".ui-sortable-placeholder").css({
                    'width': $(ui['item']).css('width'),
                    'height': $(ui['item']).css('height')
                });
            }
        });
        $(".sortable-holder").disableSelection();
    });

    // resizable
    $(function () {

        var container = $(".holder-newpage");
        var numberOfCol = 3;
        $(".resize-item-holder").css('width', 100/numberOfCol +'%');
       
        var sibTotalWidth;
        $('.resize-item-holder').resizable({
            containment: ".sortable-holder",
            handles: "w, e",
            start: function(event, ui){
                sibTotalWidth = ui.originalSize.width + ui.originalElement.next().outerWidth();
            },
            stop: function(event, ui){     
                var cellPercentWidth=100 * ui.originalElement.outerWidth()/ container.innerWidth();
                ui.originalElement.css('width', cellPercentWidth + '%');  
                var nextCell = ui.originalElement.next();
                var nextPercentWidth=100 * nextCell.outerWidth()/container.innerWidth();
                nextCell.css('width', nextPercentWidth + '%');
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
    });




});