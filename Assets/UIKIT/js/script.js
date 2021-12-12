/*global $, console*/



$(function () {

    'use strict';

    //// dropdown on dropdown

    $('.nav-dropholder .droptwo').on("click", function (e) {
        $(this).find('.dropdown-menu').toggle();
        e.stopPropagation();
        // e.preventDefault();
    });

    //// open aside menu
    $('.btn-collapse-aside-menu').on("click", function () {
        $('body').toggleClass('aside-menu-open');
    });

    //// close aside event
    $('.btn-collapse-aside-event').on("click", function () {
        $('body').toggleClass('aside-event-close');
    });

    //// list-folders aside
    $('.list-folders li .folder-title').on("click", function () {
        $(this).closest('li').toggleClass('open-folder').siblings().removeClass('open-folder');
    });
    $('.list-folders li:has(> ul)').addClass('has-folder');

    //// dropdown selected
    $(".dropdown-main .dropdown-menu li").click(function () {
        $(this).closest('.dropdown-main').find(".dropdown-title").text($(this).text());
    });

    //// adv search
    $('.btn-more-search').on("click", function () {
        $(this).closest('.more-search-footer').toggleClass('open-adv-search');
    });


    //// pages taps
    var swiper = new Swiper('.pages-tabs-holder', {
        slidesPerView: 'auto',
        slidesPerGroup: 1,
        navigation: {
            nextEl: '.pages-tabs-arrow.swiper-button-next',
            prevEl: '.pages-tabs-arrow.swiper-button-prev',
        },
    });

    //// Sortable taps
    //$("#sortableTabs").sortable({
    //    axis: "x",
    //});

    //// home slider
    var swiper = new Swiper('.main-slider', {
        slidesPerView: 1,
        slidesPerGroup: 1,
        effect: 'fade',
        navigation: {
            nextEl: '.main-slider-arrow.swiper-button-next',
            prevEl: '.main-slider-arrow.swiper-button-prev',
        },
    });

    //// tooltip script
    $('[data-toggle="tooltip"]').tooltip();

    //// nav search
    $(".nav-search-input input").focusin(function () {
        $(this).closest('.nav-search-holder').addClass('input-search-focus');
    });

    $(".nav-search-input input").focusout(function () {
        $(this).closest('.nav-search-holder').removeClass('input-search-focus');
    });

    //// collaps slider
    $(".btn-main-slider-arrow").click(function () {
        $('.main-slider').toggleClass('small-slider');
    });

    /////sortable-path
    //$("#sortable-path").sortable();
    //$("#sortable-path").disableSelection();














});

/////// aside events

document.addEventListener('DOMContentLoaded', function () {


});

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}
/////// aside events

document.addEventListener('DOMContentLoaded', function () {

});