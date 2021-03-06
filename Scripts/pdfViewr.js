

var myApp = new AWSF({
    spinner: true,
    postBackPreloader: true,
    pTrigger: false,
    panelClose: true,
    dataMessage: true,
    postAutoPreloader: false,
    spinnerColor: "#3498DB"
});
myApp.init();
myApp.showPreloader();
var pdfPath = $("#hdnpath").val();
//var pdfPath = "http://www.orimi.com/pdf-test.pdf";
var signture = $("#hdnsignture").val();
var documentId = $("#hdndocument").val();
var userId = $("#hdnuser").val();
var hw = "";
var hh = "";
var ht = "";
var hl = "";
var thePdf;
var isCalled = false;
var scale = 1.5;
var origninW = "";
var origninH = "";
var currentPage = 1;
var lastPage = 0;
var targetElm = "";
// atob() is used to convert base64 encoded PDF to binary-like data.
// (See also https://developer.mozilla.org/en-US/docs/Web/API/WindowBase64/
// Base64_encoding_and_decoding.)
//var pdfData = atob(
//  'JVBERi0xLjcKCjEgMCBvYmogICUgZW50cnkgcG9pbnQKPDwKICAvVHlwZSAvQ2F0YWxvZwog' +
//  'IC9QYWdlcyAyIDAgUgo+PgplbmRvYmoKCjIgMCBvYmoKPDwKICAvVHlwZSAvUGFnZXMKICAv' +
//  'TWVkaWFCb3ggWyAwIDAgMjAwIDIwMCBdCiAgL0NvdW50IDEKICAvS2lkcyBbIDMgMCBSIF0K' +
//  'Pj4KZW5kb2JqCgozIDAgb2JqCjw8CiAgL1R5cGUgL1BhZ2UKICAvUGFyZW50IDIgMCBSCiAg' +
//  'L1Jlc291cmNlcyA8PAogICAgL0ZvbnQgPDwKICAgICAgL0YxIDQgMCBSIAogICAgPj4KICA+' +
//  'PgogIC9Db250ZW50cyA1IDAgUgo+PgplbmRvYmoKCjQgMCBvYmoKPDwKICAvVHlwZSAvRm9u' +
//  'dAogIC9TdWJ0eXBlIC9UeXBlMQogIC9CYXNlRm9udCAvVGltZXMtUm9tYW4KPj4KZW5kb2Jq' +
//  'Cgo1IDAgb2JqICAlIHBhZ2UgY29udGVudAo8PAogIC9MZW5ndGggNDQKPj4Kc3RyZWFtCkJU' +
//  'CjcwIDUwIFRECi9GMSAxMiBUZgooSGVsbG8sIHdvcmxkISkgVGoKRVQKZW5kc3RyZWFtCmVu' +
//  'ZG9iagoKeHJlZgowIDYKMDAwMDAwMDAwMCA2NTUzNSBmIAowMDAwMDAwMDEwIDAwMDAwIG4g' +
//  'CjAwMDAwMDAwNzkgMDAwMDAgbiAKMDAwMDAwMDE3MyAwMDAwMCBuIAowMDAwMDAwMzAxIDAw' +
//  'MDAwIG4gCjAwMDAwMDAzODAgMDAwMDAgbiAKdHJhaWxlcgo8PAogIC9TaXplIDYKICAvUm9v' +
//  'dCAxIDAgUgo+PgpzdGFydHhyZWYKNDkyCiUlRU9G');
var currentContextXValue = 0;
var currentContextYValue = 0;
var percntage = 0;
$(function () {
    GetAllSigntures();
    GetAllBarcods();
    $(function () {
        //signture context
        $.contextMenu({
            selector: '#viewer',
            position: function (opt, x, y) {
                currentContextXValue = x;
                currentContextYValue = y;
                //alert(x + ',' + y);
                opt.$menu.css({ top: y, left: x });
            },
            callback: function (key, options, e) {

                var m = "clicked: " + key;
                //if (key == "cut") {
                //    oldFolderPath = [];
                //    var divpathList = $("#divpathbar a");
                //    for (var i = 0; i < divpathList.length; i++) {
                //        oldFolderPath.push($(divpathList[i]).data("id"));
                //    }
                //    isExistCopy = true;
                //    copiedtaskid = 0;
                //    copiedcontainerid = $(this).data("id");
                //    toastr.success(elementMsg, '');
                //    ShowtoggleCopyPaste(1);//show paste btn
                //}
                if (key == 'addbarcode') {

                    //left: (rect.left + window.scrollX) - $('#viewer').offset().left,
                    //    top: (rect.top + window.scrollY) - $('#viewer').offset().top
                    var top = currentContextYValue;
                    var left = currentContextXValue;
                    hl = left;
                    ht = top;
                    AddLable();
                }
                if (key == "addsigne") {
                    //var top = $('#viewer').scrollTop() + currentContextYValue - $('#viewer').offset().top;
                    //var left = currentContextXValue - $('#viewer').offset().left;
                    //hl = left;
                    //ht = top;
                    var top = currentContextYValue;
                    var left = currentContextXValue;
                    hl = left;
                    ht = top;
                    AddSignture();
                    // $('#viewer').dblclick();
                }
                if (key == "delete") {
                    var id = $(this).data("id");
                    DeleteLable(id);
                }
            },
            items: {
                "addbarcode": { name: "اضافة ليبل", icon: "fa-barcode" },
                "addsigne": { name: "اضافة توقيع", icon: "fa-pencil", },
                "quit": {
                    name: "خروج", icon: function () {
                        return 'context-menu-icon context-menu-icon-quit';
                    }
                }
            }
        });
        $.contextMenu({
            selector: '.context-menu[data-user=' + userId + ']',
            callback: function (key, options, e) {
                var m = "clicked: " + key;
                if (key == "edit") {
                    optype = 0;
                    origninW = $(this).width();
                    origninH = $(this).height()
                    var sigid = $(this).data("id");
                    var dialog = bootbox.dialog({
                        title: '',
                        message: '<div class="form-group"> <label for="">النسبة المئوية</label> <select class="form-control" id="ddelimage" onclick="changeImage();"> <option value="200">200%</option> <option value="100">100%</option> <option value="90">90%</option> <option value="80">80%</option> <option value="70">70%</option> <option value="60">60%</option> <option value="50">50%</option> <option value="40">40%</option> <option value="30">30%</option> <option value="20">20%</option> <option value="10">10%</option> </select> </div><div class="form-group" style="display:none;"> <label for="">العرض</label> <input type="text" class="form-control" id="txtwidth" value="' + $(this).width() + '"> </div> <div class="form-group" style="display:none;"> <label for="">الارتفاع</label> <input type="text" class="form-control" id="txtheight" value="' + $(this).height() + '"> </div>',
                        buttons: {
                            cancel: {
                                label: "الغاء",
                                className: 'btn-danger',
                                callback: function () {
                                }
                            },
                            noclose: {
                                label: "تحديد صفحات",
                                className: 'btn-warning',
                                callback: function () {
                                    optype = 0;
                                    percntage = Number($("#ddelimage").val());
                                    targetElm = $("#dragsign-" + sigid + "");
                                    drawPageHtml('resizepage');
                                }
                            },
                            ok: {
                                label: "حفظ",
                                className: 'btn-info',
                                callback: function () {
                                    percntage = Number($("#ddelimage").val());
                                    $("#txtheight").val(((Number(origninH) * (percntage / 100))))
                                    $("#txtwidth").val(((Number(origninW) * (percntage / 100))))
                                    hw = Math.round($("#txtwidth").val());
                                    hh = Math.round($("#txtheight").val());
                                    UpdateSize(sigid);
                                    $(".context-menu[data-id='" + sigid + "']").css('width', hw + 'px');
                                    $(".context-menu[data-id='" + sigid + "']").css('height', hh + 'px');
                                }
                            }
                        }
                    });
                }
                if (key == "delete") {
                    optype = 0;
                    var sigid = $(this).data("id");
                    var dialog = bootbox.dialog({
                        title: 'تأكيد الحذف',
                        message: '<p></p>',
                        buttons: {
                            cancel: {
                                label: "الغاء",
                                className: 'btn-danger',
                                callback: function () {
                                }
                            },
                            noclose: {
                                label: "تحديد صفحات",
                                className: 'btn-warning',
                                callback: function () {
                                    optype = 0;
                                    percntage = Number($("#ddelimage").val());
                                    targetElm = $("#dragsign-" + sigid + "");
                                    drawPageHtml('deletepage');
                                }
                            },
                            ok: {
                                label: "حذف",
                                className: 'btn-info',
                                callback: function () {
                                    DeleteSigntures(sigid);
                                }
                            }
                        }
                    });

                }
                if (key == "copy") {
                    optype = 0;
                    targetElm = $("#dragsign-" + $(this).data("id") + "");
                    drawPageHtml('copypage');
                }
            },
            items: {
                "edit": { name: "تغير الحجم", icon: "fa-picture-o" },
                "delete": { name: "حذف", icon: "fa-trash-o", },
                "copy": { name: "تكرار", icon: "fa-copy", },
                "quit": {
                    name: "خروج", icon: function () {
                        return 'context-menu-icon context-menu-icon-quit';
                    }
                }
            }
        });
        $.contextMenu({
            selector: '.drag-lable',
            callback: function (key, options, e) {
                var m = "clicked: " + key;
                //if (key == "cut") {
                //    oldFolderPath = [];
                //    var divpathList = $("#divpathbar a");
                //    for (var i = 0; i < divpathList.length; i++) {
                //        oldFolderPath.push($(divpathList[i]).data("id"));
                //    }
                //    isExistCopy = true;
                //    copiedtaskid = 0;
                //    copiedcontainerid = $(this).data("id");
                //    toastr.success(elementMsg, '');
                //    ShowtoggleCopyPaste(1);//show paste btn
                //}

                if (key == "edit") {
                    optype = 1;
                    origninW = $(this).width();
                    origninH = $(this).height();
                    var sigid = $(this).data("id");
                    var dialog = bootbox.dialog({
                        title: '',
                        message: '<div class="form-group"> <label for="">النسبة المئوية</label> <select class="form-control" id="ddelimage" onclick="changeImage();"> <option value="200">200%</option> <option value="100">100%</option> <option value="90">90%</option> <option value="80">80%</option> <option value="70">70%</option> <option value="60">60%</option> <option value="50">50%</option> <option value="40">40%</option> <option value="30">30%</option> <option value="20">20%</option> <option value="10">10%</option> </select> </div><div class="form-group" style="display:none;"> <label for="">العرض</label> <input type="text" class="form-control" id="txtwidth" value="' + $(this).width() + '"> </div> <div class="form-group" style="display:none;"> <label for="">الارتفاع</label> <input type="text" class="form-control" id="txtheight" value="' + $(this).height() + '"> </div>',
                        buttons: {
                            cancel: {
                                label: "الغاء",
                                className: 'btn-danger',
                                callback: function () {
                                }
                            },
                            noclose: {
                                label: "تحديد صفحات",
                                className: 'btn-warning',
                                callback: function () {
                                    optype = 1;
                                    percntage = Number($("#ddelimage").val());
                                    targetElm = $("#drag-" + sigid + "");
                                    drawPageHtml('resizepage');
                                    //console.log('Custom button clicked');
                                    //return false;
                                }
                            },
                            ok: {
                                label: "حفظ",
                                className: 'btn-info',
                                callback: function () {
                                    percntage = Number($("#ddelimage").val());
                                    $("#txtheight").val(((Number(origninH) * (percntage / 100))))
                                    $("#txtwidth").val(((Number(origninW) * (percntage / 100))))
                                    hw = Math.round($("#txtwidth").val());
                                    hh = Math.round($("#txtheight").val());
                                    UpdateSizeBarcode(sigid);
                                    $(".drag-lable[data-id='" + sigid + "']").css('width', hw + 'px');
                                    $(".drag-lable[data-id='" + sigid + "']").css('height', hh + 'px');
                                }
                            }
                        }
                    });
                }
                if (key == "delete") {
                    optype = 0;
                    var sigid = $(this).data("id");
                    var dialog = bootbox.dialog({
                        title: 'تأكيد الحذف',
                        message: '<p></p>',
                        buttons: {
                            cancel: {
                                label: "الغاء",
                                className: 'btn-danger',
                                callback: function () {
                                }
                            },
                            noclose: {
                                label: "تحديد صفحات",
                                className: 'btn-warning',
                                callback: function () {
                                    optype = 1;
                                    percntage = Number($("#ddelimage").val());
                                    targetElm = $("#drag-" + sigid + "");
                                    drawPageHtml('deletepage');
                                    //console.log('Custom button clicked');
                                    //return false;
                                }
                            },
                            ok: {
                                label: "حذف",
                                className: 'btn-info',
                                callback: function () {
                                    DeleteBarcode(sigid);
                                }
                            }
                        }
                    });
                }
                if (key == "copy") {
                    optype = 1;
                    targetElm = $("#drag-" + $(this).data("id") + "");
                    drawPageHtml('copypage');

                }
            },
            items: {
                "edit": { name: "تغير الحجم", icon: "fa-picture-o" },
                "delete": { name: "حذف", icon: "fa-trash-o", },
                "copy": { name: "تكرار", icon: "fa-copy", },
                "quit": {
                    name: "خروج", icon: function () {
                        return 'context-menu-icon context-menu-icon-quit';
                    }
                }
            }
        });
        $('.context-menu').on('click', function (e) {
            //console.log('clicked', this);
        })
    })
})
function changeImage() {
    //var percntage = $("#ddelimage").val();
    //$("#txtwidth").val(((Number($("#txtwidth").val()) * (percntage / 100))));
    //$("#txtheight").val(((Number($("#txtheight").val()) * (percntage / 100))));
}
// Loaded via <script> tag, create shortcut to access PDF.js exports.
var pdfjs = window['pdfjs-dist/build/pdf'];

// The workerSrc property shall be specified.
pdfjs.GlobalWorkerOptions.workerSrc = 'Scripts/pdfJS/pdf.worker.js';

// Using DocumentInitParameters object to load binary data.
var loadingTask = pdfjs.getDocument(pdfPath);//pdfjs.getDocument({ data: pdfData });
var totalPages = 1;

loadingTask.promise.then(function (pdf) {

    console.log('PDF loaded');
    thePdf = pdf;
    viewer = document.getElementById('viewer');
    totalPages = pdf.numPages;
    for (page = 1; page <= pdf.numPages; page++) {
        myApp.showPreloader();
        canvas = document.createElement("canvas");
        canvas.className = 'pdf-page-canvas';
        var pagename = 'page' + page;
        canvas.id = pagename;
        viewer.appendChild(canvas);
        //canvas = watermarkedDataURL(canvas, "It's Mine!");
        // createWM(canvas);
        renderPage(page, canvas);
    }
    setTimeout(function () {
        //alert("load file ended");
        // allow drag for all items
        allowDrag();
    }, 1500);
    setTimeout(function () {
        for (page = 1; page <= totalPages; page++) {
            //var pagename = 'page' + page;
            addCanavas(page);
        }
    }, 1000);
    //// Fetch the first page
    //var pageNumber = 1;
    //pdf.getPage(pageNumber).then(function (page) {
    //    console.log('Page loaded');
    //    var scale = 1.5;
    //    var viewport = page.getViewport(scale);
    //    // Prepare canvas using PDF page dimensions
    //    var canvas = document.getElementById('the-canvas');
    //    var context = canvas.getContext('2d');
    //    canvas.height = viewport.height;
    //    canvas.width = viewport.width;
    //    // Render PDF page into canvas context
    //    var renderContext = {
    //        canvasContext: context,
    //        viewport: viewport
    //    };
    //    var renderTask = page.render(renderContext);
    //    renderTask.then(function () {
    //        console.log('Page rendered');
    //    });
    //});
}, function (reason) {
    // PDF loading error
    console.error(reason);
});
function addCanavas(pageIndex) {
    var pageName = 'page' + pageIndex;
    var container = document.getElementById("viewer")
    var origCanvas = document.getElementById(pageName);
    var wmCanvas = document.createElement("canvas");
    var marginValue = pageIndex != 1 ? origCanvas.height * (pageIndex - 1) : 0;
    wmCanvas.id = "watermark";
    wmCanvas.width = origCanvas.width;
    wmCanvas.height = origCanvas.height;
    if (marginValue != 0)
        wmCanvas.setAttribute("style", "position:absolute;margin-top:" + marginValue + "px;")
    else {
        wmCanvas.setAttribute("style", "position:absolute;")
    }

    if (container.firstChild)
        container.insertBefore(wmCanvas, container.firstChild);
    else
        container.appendChild(wmCanvas);

    var wmContext = wmCanvas.getContext('2d');
    wmContext.globalAlpha = 0.5;

    // setup text for filling
    wmContext.font = "72px Comic Sans MS";
    wmContext.fillStyle = "gray";
    // get the metrics with font settings
    var metrics = wmContext.measureText("WaterMark Demo");
    var width = metrics.width;
    // height is font size
    var height = 72;

    // change the origin coordinate to the middle of the context
    wmContext.translate(origCanvas.width / 2, origCanvas.height / 2);
    // rotate the context (so it's rotated around its center)
    wmContext.rotate(-Math.atan(origCanvas.height / origCanvas.width));
    // as the origin is now at the center, just need to center the text
    //wmContext.style.marginTop = '1000px';
    var waterMark = $("#hdnCurrentName").val();
    wmContext.fillText(waterMark, -width / 2, height / 2);
}
var pageHeight = 0;
function renderPage(pageNumber, canvas) {
    thePdf.getPage(pageNumber).then(function (page) {
        viewport = page.getViewport(scale);
        // page.getViewport(canvas.width / page.getViewport(1.0).width);
        canvas.height = viewport.height;
        canvas.width = viewport.width;
        page.render({ canvasContext: canvas.getContext('2d'), viewport: viewport });
        //call signture
        $(".context-menu").show();
        myApp.hidePreloader();
        if (pageHeight == 0) {
            pageHeight = Number($('#page1').attr('height'));
        }
        //alert();
    });
}

$('#viewer').dblclick(function (e) {
    //hl = (e.pageX + $(this).offset().left - 20);
    ////var left = hl + 'px !important';
    //ht = (e.pageY + $(this).offset().top + $("#viewerContainer").scrollTop() - 50);
    ////var top = ht + 'px !important';
    var top = $(this).scrollTop() + e.pageY - $(this).offset().top;
    var left = e.pageX - $(this).offset().left;
    hl = left;
    ht = top;
    AddSignture();
});
function RTexportPDF() {
    var element = $('#viewer')[0];
    html2pdf(element, {
        margin: 1,
        filename: 'myfile.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { dpi: 192, letterRendering: true },
        jsPDF: {
            orientation: 'p',
            unit: 'mm',
            format: 'a4',
            hotfixes: []
        }
    });
}
function R2exportPDF() {
    //alert("export pdf");
    var options = {
        "background": '#000',
        format: 'PNG',
        pagesplit: true
    }
    var pdf = new jsPDF('p', 'pt', 'a4');
    pdf.addHTML($('#viewer'), options, function () {
        pdf.save('web.pdf');
    });
    //var doc = new jsPDF('p', 'mm', 'a4');
    //var specialElementHandlers = {
    //    '#viewer': function (element, renderer) {
    //        return true;
    //    }
    //};
    //doc.fromHTML($('#viewer')[0], 15, 15, {
    //    'background': '#fff',
    //    'elementHandlers': specialElementHandlers
    //}, function () {
    //    doc.save('sample-file.pdf');
    //});
    //$('#wrapper')[0].scrollTop=200;
    //var options = {
    //    "background": '#000',
    //    format: 'PNG',
    //    pagesplit: true
    //}
    //var pdf = new jsPDF('p', 'mm', 'a4');

    //// We'll make our own renderer to skip this editor
    //var specialElementHandlers = {
    //    '#viewer': function (element, renderer) {
    //        return true;
    //    }
    //};

    //// All units are in the set measurement for the document
    //// This can be changed to "pt" (points), "mm" (Default), "cm", "in"
    //pdf.fromHTML($('#viewer')[0], 15, 15, {
    //    'width': 170,
    //    'elementHandlers': specialElementHandlers
    //});
    //pdf.save('web.pdf');
}
function demoFromHTML() {
    var pdf = new jsPDF('p', 'pt', 'letter');
    // source can be HTML-formatted string, or a reference
    // to an actual DOM element from which the text will be scraped.
    source = $('#viewerContainer')[0];

    // we support special element handlers. Register them with jQuery-style 
    // ID selector for either ID or node name. ("#iAmID", "div", "span" etc.)
    // There is no support for any other type of selectors 
    // (class, of compound) at this time.
    specialElementHandlers = {
        // element with id of "bypass" - jQuery style selector
        '#bypassme': function (element, renderer) {
            // true = "handled elsewhere, bypass text extraction"
            return true
        }
    };
    margins = {
        top: 80,
        bottom: 60,
        left: 40,
        width: 522
    };
    // all coords and widths are in jsPDF instance's declared units
    // 'inches' in this case
    pdf.fromHTML(
        source, // HTML string or DOM elem ref.
        margins.left, // x coord
        margins.top, { // y coord
        'width': margins.width, // max width of content on PDF
        'elementHandlers': specialElementHandlers
    },

        function (dispose) {
            // dispose: object with X, Y of the last line add to the PDF 
            //          this allow the insertion of new lines after html
            pdf.save('Test.pdf');
        }, margins);
}
function makePDF() {

    var quotes = document.getElementById('viewerContainer');

    html2canvas(quotes, {
        onrendered: function (canvas) {

            //! MAKE YOUR PDF
            var pdf = new jsPDF('p', 'pt', [1350, 6000]);

            for (var i = 0; i <= quotes.clientHeight / 980; i++) {
                //! This is all just html2canvas stuff
                var srcImg = canvas;
                var sX = 0;
                var sY = 980 * i; // start 980 pixels down for every new page
                var sWidth = 900;
                var sHeight = 980;
                var dX = 0;
                var dY = 0;
                var dWidth = 900;
                var dHeight = 980;

                window.onePageCanvas = document.createElement("canvas");
                onePageCanvas.setAttribute('width', 900);
                onePageCanvas.setAttribute('height', 980);
                var ctx = onePageCanvas.getContext('2d');
                // details on this usage of this function: 
                // https://developer.mozilla.org/en-US/docs/Web/API/Canvas_API/Tutorial/Using_images#Slicing
                ctx.drawImage(srcImg, sX, sY, sWidth, sHeight, dX, dY, dWidth, dHeight);

                // document.body.appendChild(canvas);
                var canvasDataURL = onePageCanvas.toDataURL("image/png", 1.0);

                var width = onePageCanvas.width;
                var height = onePageCanvas.clientHeight;

                //! If we're on anything other than the first page,
                // add another page
                if (i > 0) {
                    pdf.addPage(612, 791); //8.5" x 11" in pts (in*72)
                }
                //! now we declare that we're working on that page
                pdf.setPage(i + 1);
                //! now we add content to that page!
                pdf.addImage(canvasDataURL, 'PNG', 20, 40, (width * .62), (height * .62));

            }
            //! after the for loop is finished running, we save the pdf.
            pdf.save('Test.pdf');
        }
    });
}
function exportPDF() {
    kendo.drawing
        .drawDOM("#viewerContainer",
            {
                paperSize: "A4",
                margin: { top: "15px", bottom: "15px" },
                scale: 0.6,
                height: 500
            })
        .then(function (group) {
            kendo.drawing.pdf.saveAs(group, "Exported.pdf")
        });
    //kendo.drawing.drawDOM("#viewerContainer", {
    //    paperSize: "A4",
    //    margin: "15px",
    //    scale: 0.7
    //}).then(function (group) {
    //    kendo.drawing.pdf.saveAs(group, "filename.pdf");
    //});
}
function printThis() {
    window.print();
}

function watermarkedDataURL(canvas, text) {
    var tempCanvas = document.createElement('canvas');
    var tempCtx = tempCanvas.getContext('2d');
    var cw, ch;
    cw = tempCanvas.width = canvas.width;
    ch = tempCanvas.height = canvas.height;
    tempCtx.drawImage(canvas, 1501, 0);
    tempCtx.font = "24px verdana";
    var textWidth = tempCtx.measureText(text).width;
    tempCtx.globalAlpha = .50;
    tempCtx.fillStyle = 'white';
    tempCtx.fillText(text, cw - textWidth - 10, ch - 20);
    tempCtx.fillStyle = 'white';
    tempCtx.fillText(text, cw - textWidth - 10 + 2, ch - 20 + 2);
    // just testing by adding tempCanvas to document
    document.body.appendChild(tempCanvas);
    return (tempCanvas.toDataURL());
}
function createWM(wmCanvas) {
    //var origCanvas = document.getElementById("page1");
    var wmContext = wmCanvas.getContext('2d');
    wmContext.globalAlpha = 0.5;
    // setup text for filling
    wmContext.font = "72px Comic Sans MS";
    wmContext.fillStyle = "red";
    // get the metrics with font settings
    var metrics = wmContext.measureText("WaterMark Demo");
    var width = metrics.width;
    // height is font size
    var height = 72;

    // change the origin coordinate to the middle of the context
    //wmContext.translate(origCanvas.width / 2, origCanvas.height / 2);
    // rotate the context (so it's rotated around its center)
    // wmContext.rotate(-Math.atan(origCanvas.height / origCanvas.width));
    // as the origin is now at the center, just need to center the text
    wmContext.fillText("WaterMark Demo", -width / 2, height / 2);
}
function GetAllSigntures() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/GetAllSigntures",
        data: "{document:'" + documentId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var html = "";
            for (var i = 0; i < jsdata.length; i++) {
                //save latest height
                if (jsdata[i].UserId == userId) {
                    if (jsdata[i].Width != undefined && jsdata[i].Width != '' && jsdata[i].Width != null) {
                        hw = jsdata[i].Width;
                        hh = jsdata[i].Height;
                    }
                }
                if (jsdata[i].Transform == "" || jsdata[i].Transform == null)
                    html += '<img id="dragsign-' + jsdata[i].Id + '"  class="context-menu drag-drop can-drop" data-type="0" data-user="' + jsdata[i].UserId + '" data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute; left: ' + jsdata[i].Left + 'px; top: ' + jsdata[i].Top + 'px"  src="' + jsdata[i].Signture + '" >';
                else {

                    var XP = jsdata[i].Transform.split(',')[0].replace('transform: translate( ', '').replace('px', '').trim();
                    var YP = jsdata[i].Transform.split(',')[1].replace(')', '').replace('px', '').replace(';', '').trim();
                    html += '<img id="dragsign-' + jsdata[i].Id + '"  class="context-menu drag-drop can-drop" data-type="0" data-user="' + jsdata[i].UserId + '" data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute;' + jsdata[i].Transform + '"  src="' + jsdata[i].Signture + '"  data-x="' + XP + '" data-y="' + YP + '">';
                }
                //html += "<img id='dragsign-" + jsdata[i].Id +"' class='context-menu drag-sign drag-drop  can-drop' data-user='" + jsdata[i].UserId + "' data-id='" + jsdata[i].Id + "' src='" + jsdata[i].Signture + "' style='width:" + jsdata[i].Width + "px;height:" + jsdata[i].Height + "px;position:absolute;' />";
            }
            $("#viewer").prepend(html);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function GetAllBarcods() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/GetAllBarcods",
        data: "{document:'" + documentId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var html = "";
            for (var i = 0; i < jsdata.length; i++) {
                var elmName = 'drag-' + jsdata[i].Id;
                //save latest height
                if (jsdata[i].UserId == userId) {
                    if (jsdata[i].Width != undefined && jsdata[i].Width != '' && jsdata[i].Width != null) {
                        hw = jsdata[i].Width;
                        hh = jsdata[i].Height;
                    }
                }

                if (jsdata[i].Transform == "" || jsdata[i].Transform == null)
                    html += '<img id="drag-' + jsdata[i].Id + '"  class="ez-resource-show__preview__image drag-drop drag-lable  can-drop" data-sort="' + jsdata[i].Sort + '" data-type="1" data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute; left: ' + jsdata[i].Left + 'px; top: ' + jsdata[i].Top + 'px"  src="' + jsdata[i].Lable + '" >';
                else {

                    var XP = jsdata[i].Transform.split(',')[0].replace('transform: translate( ', '').replace('px', '').trim();
                    var YP = jsdata[i].Transform.split(',')[1].replace(')', '').replace('px', '').replace(';', '').trim();
                    html += '<img id="drag-' + jsdata[i].Id + '"  class="ez-resource-show__preview__image drag-drop drag-lable can-drop" data-sort="' + jsdata[i].Sort + '" data-type="1" data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute;' + jsdata[i].Transform + '"  src="' + jsdata[i].Lable + '"  data-x="' + XP + '" data-y="' + YP + '">';
                }
                //html += "<img class='context-menu' data-user='" + jsdata[i].UserId + "' data-id='" + jsdata[i].Id + "' src='" + jsdata[i].Signture + "' style='width:" + jsdata[i].Width + "px;height:" + jsdata[i].Height + "px;position:absolute;left:" + jsdata[i].Left + "px;top:" + jsdata[i].Top + "px' />";
            }
            $("#viewer").prepend(html);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function AddSignture() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/AddSignture",
        data: "{width:'" + hw + "',height:'" + hh + "',top:'" + ht + "',left:'" + hl + "',document:'" + documentId + "',user:'" + userId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata != false) {
                $("#viewer").prepend('<img id="dragsign-' + jsdata + '" class="drag-drop context-menu can-drop" data-type="0" data-user="' + userId + '" data-id="' + jsdata + '" style="width: ' + hw + 'px; height: ' + hh + 'px; position: absolute;transform: translate( ' + hl + 'px, ' + ht + 'px); "  src="' + signture + '" data-x="' + hl + '" data-y="' + ht + '">');//left: ' + hl + 'px; top: ' + ht + 'px
                //$("#viewer").prepend("<img class='context-menu' data-user='" + userId + "' data-id='" + jsdata + "' src='" + signture + "' style='width:" + hw + "px;height:" + hh + "px;position:absolute;left:" + hl + "px;top:" + ht + "px' />");
                $(".context-menu").show();
            }
            else {
                alert("ليس مسجل لديك توقيع");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function UpdateLablePosition(id) {

    id = Number(id);
    var elmName = 'drag-' + id;
    var el = document.getElementById(elmName);
    var topValue = getOffset(el).top;
    var leftValue = getOffset(el).left;
    var transformValue = 'transform: translate(' + $('#' + elmName + '').css('transform').split(',')[4] + 'px,' + $('#' + elmName + '').css('transform').split(',')[5].replace(')', '') + 'px);';
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/UpdateLablePosition",
        data: "{top:'" + topValue + "',left:'" + leftValue + "',transform:'" + transformValue + "',id:" + id + "}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            console.log('update position top =>' + topValue + ",left =>" + leftValue);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function UpdateSignPosition(id) {
    id = Number(id);
    var elmName = 'dragsign-' + id;
    var el = document.getElementById(elmName);
    var topValue = getOffset(el).top;
    var leftValue = getOffset(el).left;
    var transformValue = 'transform: translate(' + $('#' + elmName + '').css('transform').split(',')[4] + 'px,' + $('#' + elmName + '').css('transform').split(',')[5].replace(')', '') + 'px);';
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/UpdateSignPosition",
        data: "{top:'" + topValue + "',left:'" + leftValue + "',transform:'" + transformValue + "',id:" + id + "}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            console.log('update position top =>' + topValue + ",left =>" + leftValue);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function AddLable() {
    var lable = generateLAbleBase64();
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/AddLable",
        data: "{width:'" + hw + "',height:'" + hh + "',top:'" + ht + "',left:'" + hl + "',document:'" + documentId + "',user:'" + userId + "',lable:'" + lable + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata != false) {
                var barcode = $('#hdnDocLable').val();
                var barcode = jsdata.split('|')[1];
                var sort = jsdata.split('|')[2];
                jsdata = jsdata.split('|')[0];

                $("#viewer").prepend('<img id="drag-' + jsdata + '" class="ez-resource-show__preview__image drag-drop drag-lable can-drop" data-sort="' + sort + '" data-type="1" data-id="' + jsdata + '" style="width: ' + hw + 'px; height: ' + hh + 'px; position: absolute;transform: translate( ' + hl + 'px, ' + ht + 'px); "  src="' + barcode + '" data-x="' + hl + '" data-y="' + ht + '">');//left: ' + hl + 'px; top: ' + ht + 'px
                //$("#viewer").prepend("<img class='context-menu' data-user='" + userId + "' data-id='" + jsdata + "' src='" + lable + "' style='width:" + hw + "px;height:" + hh + "px;position:absolute;left:" + hl + "px;top:" + ht + "px' />");
                //$(".context-menu").show();
            }
            else {
                alert("لا يوجد ليبل لهذا الملف");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function UpdateSize(id) {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/UpdateSize",
        data: "{width:'" + hw + "',height:'" + hh + "',id:'" + id + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata != false) {

            }
            else {
                //alert("ليس مسجل لديك توقيع");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function UpdateSizeBarcode(id) {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/UpdateSizeBarcode",
        data: "{width:'" + hw + "',height:'" + hh + "',id:'" + id + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata != false) {

            }
            else {
                //alert("ليس مسجل لديك توقيع");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function DeleteSigntures(id) {
    //bootbox.confirm("تأكيد الحذف ؟", function (result) {
    //    if (result) {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/DeleteSigntures",
        data: "{id:'" + id + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata != false) {
                $(".context-menu[data-id='" + id + "']").remove();
            }
            else {
                alert("خطا ف الحذف");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
    //    }
    //});

}
function DeleteBarcode(id) {
    //bootbox.confirm("تأكيد الحذف ؟", function (result) {

    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/DeleteBarcode",
        data: "{id:'" + id + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata != false) {
                $(".drag-lable[data-id='" + id + "']").remove();
            }
            else {
                alert("خطا ف الحذف");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
    //    }
    //});
}
$(document).ajaxSend(function () {
    myApp.showPreloader();
});
$(document).ajaxError(function () {
    myApp.hidePreloader();
});
$(document).ajaxStop(function () {
    myApp.hidePreloader();
});

function makeThumb(page) {
    // draw page to fit into 96x96 canvas
    var vp = page.getViewport(1);
    var canvas = document.createElement("canvas");
    canvas.width = canvas.height = 96;
    var scale = Math.min(canvas.width / vp.width, canvas.height / vp.height);
    return page.render({ canvasContext: canvas.getContext("2d"), viewport: page.getViewport(scale) }).promise.then(function () {
        return canvas;
    });
}

pdfjs.getDocument(pdfPath).promise.then(function (doc) {
    var pages = []; while (pages.length < doc.numPages) pages.push(pages.length + 1);
    return Promise.all(pages.map(function (num) {
        // create a div for each page and build a small canvas for it
        var div = document.createElement("div");
        div.setAttribute("data-index", num);
        $(".toolbarLabel").html(num);
        lastPage = num;
        document.getElementById("thumbnailView").appendChild(div);
        return doc.getPage(num).then(makeThumb)
            .then(function (canvas) {
                div.appendChild(canvas);
            });
    }));
}).catch(console.error);

$(document).on("click", "#sidebarToggle", function () {
    if ($("#outerContainer").hasClass("sidebarOpen")) {
        $("#outerContainer").removeClass("sidebarOpen");
        $(this).removeClass("toggled");
    }
    else {
        $("#outerContainer").addClass("sidebarOpen");
        $(this).addClass("toggled");
    }
});
$(document).on("click", "#thumbnailView div", function () {
    var num = Number($(this).data("index"));
    $("#thumbnailView div").removeClass('thumbnailSelectionRing');
    $(this).addClass('thumbnailSelectionRing');
    var collection = $("#viewer").find("canvas");
    for (var i = 0; i < collection.length; i++) {
        if ((i + 1) == num) {
            $('html, body').animate({
                scrollTop: $(collection[i]).offset().top
            }, 500);
        }
    }
});

$(window).scroll(function () {
    GetCurrentPageIndex();
    clearTimeout($.data(this, "scrollCheck"));
    $.data(this, "scrollCheck", setTimeout(function () {
        $(".pageNumber").val(currentPage);
        $("#thumbnailView div").removeClass('thumbnailSelectionRing');
        $("#thumbnailView").find("div[data-index='" + currentPage + "']").addClass('thumbnailSelectionRing');
    }, 250));
});
function GetCurrentPageIndex() {
    var canvasH = $('#viewer').find('canvas').attr("height");
    var scrolled = $(document).scrollTop();
    var devide = Number(scrolled) / Number(canvasH);
    currentPage = Math.round(devide) + 1;
    console.log(currentPage);
}
function goNext() {
    if (currentPage < lastPage) {
        currentPage = currentPage + 1;
        var num = currentPage;
        var collection = $("#viewer").find("canvas");
        for (var i = 0; i < collection.length; i++) {
            if ((i + 1) == num) {
                $('html, body').animate({
                    scrollTop: $(collection[i]).offset().top
                }, 500);
            }
        }
    }
}

function goBack() {
    if (currentPage > 1) {
        currentPage = currentPage - 1;
        var num = currentPage;
        var collection = $("#viewer").find("canvas");
        for (var i = 0; i < collection.length; i++) {
            if ((i + 1) == num) {
                $('html, body').animate({
                    scrollTop: $(collection[i]).offset().top
                }, 500);
            }
        }
    }
}

function allowDrag() {

    /* The dragging code for '.draggable' from the demo above
       * applies to this demo as well so it doesn't have to be repeated. */
    // enable draggables to be dropped into this
    interact('.dropzone').dropzone({
        // only accept elements matching this CSS selector
        accept: '.drag-drop',
        // Require a 100% element overlap for a drop to be possible
        overlap: 1,

        // listen for drop related events:

        ondropactivate: function (event) {
            // add active dropzone feedback
            event.target.classList.add('drop-active');
        },
        ondragenter: function (event) {
            var draggableElement = event.relatedTarget,
                dropzoneElement = event.target;

            // feedback the possibility of a drop
            dropzoneElement.classList.add('drop-target');
            draggableElement.classList.add('can-drop');
            draggableElement.classList.remove('dropped-out');
            //draggableElement.textContent = 'Dragged in';
        },
        ondragleave: function (event) {
            // remove the drop feedback style
            event.target.classList.remove('drop-target');
            event.relatedTarget.classList.remove('can-drop');
            event.relatedTarget.classList.add('dropped-out');
            //event.relatedTarget.textContent = 'Dragged out';
        },
        ondrop: function (event) {
            //event.relatedTarget.textContent = 'Dropped';
        },
        ondropdeactivate: function (event) {
            // remove active dropzone feedback
            event.target.classList.remove('drop-active');
            event.target.classList.remove('drop-target');
        }
    });

    interact('.drag-drop')
        .draggable({
            inertia: true,
            restrict: {
                restriction: "#selectorContainer",
                //endOnly: true,
                // elementRect: { top: 500, left: 100 }
            },
            //autoScroll: true,
            // dragMoveListener from the dragging demo above
            onmove: dragMoveListener,
            onend: dragEndListener
        });


    function dragMoveListener(event) {
        //
        var target = event.target,
            // keep the dragged position in the data-x/data-y attributes
            x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
            y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;
        console.log('x s => ' + x + ",y is =>" + y);
        // translate the element
        target.style.webkitTransform =
            target.style.transform = 'translate(' + x + 'px, ' + y + 'px)';

        // update the posiion attributes
        target.setAttribute('data-x', x);
        target.setAttribute('data-y', y);
        //var id = $(target).attr("data-id");
        //UpdateLablePosition(id);
    }

    function dragEndListener(event) {
        //
        try {
            var target = event.target,
                // keep the dragged position in the data-x/data-y attributes
                x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
                y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;
            var id = $(target).attr("data-id");
            if ($(target).attr("data-type") == 1) {
                UpdateLablePosition(id);
            }
            else {
                UpdateSignPosition(id);
            }
        } catch (e) {

        }
    }
    // this is used later in the resizing demo
    window.dragMoveListener = dragMoveListener;
}
function getOffset(el) {
    const rect = el.getBoundingClientRect();
    return {
        left: (rect.left + window.scrollX),
        top: (rect.top + window.scrollY)
    };
}


var op = '';
var pagesLength = 0;
function drawPageHtml(opration) {

    op = opration;
    var collection = $("#thumbnailView").find("div");
    if (opration == 'copypage') { // copy all not current
        var html = '';
        for (var i = 0; i < collection.length; i++) {
            pagesLength = Number($(collection[i]).attr("data-index"));
            var canvas = $(collection[i]).find('canvas');
            if ($(collection[i]).attr('data-index') != getPageAccorddingTotarnsform(targetElm)) {//not select the same page
                var url = canvas[0].toDataURL();
                html += "<div class=\"col-md-3\">";
                html += "                            <div class=\"custom-control custom-checkbox image-checkbox\">";
                html += "                                <input type=\"checkbox\" class=\"custom-control-input page-checkbox\" value='" + $(collection[i]).attr("data-index") + "'> الصفحة " + $(collection[i]).attr("data-index") + "";
                html += "                                <label class=\"custom-control-label\" for=\"ck1a\">";
                html += "                                    <img src=\"" + url + "\" alt=\"#\" class=\"img-fluid\">";
                html += "                                <\/label>";
                html += "                            <\/div>";
                html += "                        <\/div>";
            }
        }
        $('.div-list-pages').html(html);
        $("#pageModal").modal('show');
    }
    if (opration == 'resizepage' || opration == 'deletepage') {
        //get pages have barcode
        var barcodePagesArr = [];
        var collect = optype == 1 ? $("img[data-type=1]") : $("img[data-type=0]");
        for (var i = 0; i < collect.length; i++) {
            //var searchX = Number($(collect[i]).attr("data-x"));
            barcodePagesArr.push(getPageAccorddingTotarnsform($(collect[i])));
        }
        var html = '';
        for (var i = 0; i < collection.length; i++) {
            pagesLength = Number($(collection[i]).attr("data-index"));
            if (jQuery.inArray(Number($(collection[i]).attr("data-index")), barcodePagesArr) !== -1) {
                var canvas = $(collection[i]).find('canvas');
                var url = canvas[0].toDataURL();
                //document.getElementById('image_for_crop').appendChild(image);
                html += "<div class=\"col-md-3\">";
                html += "                            <div class=\"custom-control custom-checkbox image-checkbox\">";
                html += "                                <input type=\"checkbox\" class=\"custom-control-input page-checkbox\" value='" + $(collection[i]).attr("data-index") + "'> الصفحة " + $(collection[i]).attr("data-index") + "";
                html += "                                <label class=\"custom-control-label\" for=\"ck1a\">";
                //html += image;
                // html +=
                html += "                                    <img src=\"" + url + "\" alt=\"#\" class=\"img-fluid\">";
                html += "                                <\/label>";
                html += "                            <\/div>";
                html += "                        <\/div>";
            }
        }
        $('.div-list-pages').html(html);
        $("#pageModal").modal('show');
    }
}
var optype = 0;
function saveChanges() {

    //var elm = $(targetElm);
    var currentElmPage = getPageAccorddingTotarnsform(targetElm);
    var xVal = Number($(targetElm).attr("data-x"));
    var yVal = Number($(targetElm).attr("data-y"));

    if (op == 'copypage') { // copy all not current
        var trasnList = "";
        var collection = $(".page-checkbox");
        for (var i = 0; i < collection.length; i++) {
            if ($(collection[i]).is(":checked")) {
                var copyPage = Number($(collection[i]).val());
                if (copyPage < currentElmPage) {
                    var ynewVal = calcDistance(copyPage, currentElmPage);
                    trasnList += 'transform: translate( ' + xVal + 'px, ' + ynewVal + 'px);';
                }
                else {
                    var ynewVal = calcDistance(copyPage, currentElmPage);//yVal + ((copyPage - currentElmPage) * pageHeight);
                    trasnList += 'transform: translate( ' + xVal + 'px, ' + ynewVal + 'px);';
                }
            }
        }
        if (optype == 1) {
            CopyBarcode(trasnList);
        }
        else {
            CopySignture(trasnList);
        }
        // call server to add
    }
    if (op == 'deletepage') {
        var selectdPages = [];
        var collection = $(".page-checkbox");
        for (var i = 0; i < collection.length; i++) {
            if ($(collection[i]).is(":checked")) {
                selectdPages.push(Number($(collection[i]).val()));
            }
        }
        var collect = optype == 1 ? $("img[data-type=1]") : $("img[data-type=0]");
        for (var i = 0; i < collect.length; i++) {
            var cpage = getPageAccorddingTotarnsform($(collect[i]));
            if (jQuery.inArray(cpage, selectdPages) !== -1) {
                var sigid = $($(collect[i])).data("id")
                if (optype == 1) {
                    DeleteBarcode(sigid);
                }
                else {
                    DeleteSigntures(sigid);
                }
            }
        }
        $("#pageModal").modal('hide');
    }
    if (op == 'resizepage') {
        var selectdPages = [];
        var collection = $(".page-checkbox");
        for (var i = 0; i < collection.length; i++) {
            if ($(collection[i]).is(":checked")) {
                selectdPages.push(Number($(collection[i]).val()));
            }
        }
        var collect = optype == 1 ? $("img[data-type=1]") : $("img[data-type=0]");
        for (var i = 0; i < collect.length; i++) {

            //var searchX = Number($(collect[i]).attr("data-x"));
            var cpage = getPageAccorddingTotarnsform($(collect[i]));
            if (jQuery.inArray(cpage, selectdPages) !== -1) {
                //deleted this elemnt
                origninW = $($(collect[i])).width();
                origninH = $($(collect[i])).height();
                var sigid = $($(collect[i])).data("id");
                //$("#txtheight").val()
                //$("#txtwidth").val()
                hw = Math.round(((Number(origninW) * (percntage / 100))));
                hh = Math.round(((Number(origninH) * (percntage / 100))));
                if (optype == 1) {
                    UpdateSizeBarcode(sigid);
                    //
                    $(".drag-lable[data-id='" + sigid + "']").css('width', hw + 'px');
                    $(".drag-lable[data-id='" + sigid + "']").css('height', hh + 'px');
                }
                else {
                    UpdateSize(sigid);
                    //
                    $(".context-menu[data-id='" + sigid + "']").css('width', hw + 'px');
                    $(".context-menu[data-id='" + sigid + "']").css('height', hh + 'px');
                }
            }
        }
        $("#pageModal").modal('hide');
    }
}
function CopyBarcode(trasnList) {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/CopyBarcode",
        data: "{id:'" + $(targetElm).attr('data-id') + "',tans:'" + trasnList + "',document:'" + documentId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#pageModal").modal('hide');

            var jsdata = JSON.parse(data.d);
            var html = "";
            for (var i = 0; i < jsdata.length; i++) {
                //save latest height
                if (jsdata[i].UserId == userId) {
                    if (jsdata[i].Width != undefined && jsdata[i].Width != '' && jsdata[i].Width != null) {
                        hw = jsdata[i].Width;
                        hh = jsdata[i].Height;
                    }
                }
                if (jsdata[i].Transform == "" || jsdata[i].Transform == null)
                    html += '<img id="drag-' + jsdata[i].Id + '"  class="drag-drop drag-lable can-drop" data-sort="' + jsdata[i].Sort + '" data-type="1"  data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute; left: ' + jsdata[i].Left + 'px; top: ' + jsdata[i].Top + 'px"  src="' + jsdata[i].Lable + '" />';
                else {

                    var XP = jsdata[i].Transform.split(',')[0].replace('transform: translate( ', '').replace('px', '').trim();
                    var YP = jsdata[i].Transform.split(',')[1].replace(')', '').replace('px', '').replace(';', '').trim();
                    html += '<img id="drag-' + jsdata[i].Id + '"  class="drag-drop drag-lable can-drop" data-sort="' + jsdata[i].Sort + '" data-type="1"  data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute;' + jsdata[i].Transform + '"  src="' + jsdata[i].Lable + '"  data-x="' + XP + '" data-y="' + YP + '"/>';
                }
            }
            $("#viewer").prepend(html);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}

function CopySignture(trasnList) {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/CopySignture",
        data: "{id:'" + $(targetElm).attr('data-id') + "',tans:'" + trasnList + "',document:'" + documentId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#pageModal").modal('hide');
            var jsdata = JSON.parse(data.d);
            var html = "";
            for (var i = 0; i < jsdata.length; i++) {
                //save latest height
                if (jsdata[i].UserId == userId) {
                    if (jsdata[i].Width != undefined && jsdata[i].Width != '' && jsdata[i].Width != null) {
                        hw = jsdata[i].Width;
                        hh = jsdata[i].Height;
                    }
                }
                if (jsdata[i].Transform == "" || jsdata[i].Transform == null)
                    html += '<img id="dragsign-' + jsdata[i].Id + '"  class="context-menu drag-drop can-drop" data-type="0" data-user="' + jsdata[i].UserId + '" data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute; left: ' + jsdata[i].Left + 'px; top: ' + jsdata[i].Top + 'px"  src="' + jsdata[i].Signture + '" >';
                else {

                    var XP = jsdata[i].Transform.split(',')[0].replace('transform: translate( ', '').replace('px', '').trim();
                    var YP = jsdata[i].Transform.split(',')[1].replace(')', '').replace('px', '').replace(';', '').trim();
                    html += '<img id="dragsign-' + jsdata[i].Id + '"  class="context-menu drag-drop can-drop" data-type="0" data-user="' + jsdata[i].UserId + '" data-id="' + jsdata[i].Id + '" style="width: ' + jsdata[i].Width + 'px; height: ' + jsdata[i].Height + 'px; position: absolute;' + jsdata[i].Transform + '"  src="' + jsdata[i].Signture + '"  data-x="' + XP + '" data-y="' + YP + '">';
                }
            }
            $("#viewer").prepend(html);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function getPageAccorddingTotarnsform(el) {
    var page = 1;
    var yaxis = $(el).attr("data-y");
    if (yaxis <= pageHeight) {
        //return page;
    }
    else {
        var cTotalHeight = 0;
        var pagecount = Number($("[data-index]").length);
        var isfound = false;
        for (var i = 1; i <= pagecount; i++) {
            if (isfound == false) {
                var pageid = "#page" + i;
                cTotalHeight = cTotalHeight + Number($(pageid).attr('height'));
                if (yaxis < cTotalHeight) {
                    page = i;
                    isfound = true;
                    break;
                }
            }
        }
        //while (yaxis > pageHeight) {
        //    page++;
        //    yaxis = yaxis - pageHeight;
        //}
    }
    return page;
}
var fromtop = 0;
function calcDistance(copypage, currentpage) {

    var totalDistance = 0;
    //if (copypage < currentpage) {
    //    //go to top
    //    for (var i = currentpage; i >= copypage; i--) {
    //        var pageid = "#page" + currentpage;
    //        if (i == currentpage) {
    //            totalDistance = totalDistance + Number($(pageid).attr("data-y")) + 5;
    //        }
    //        else {
    //            //$('#page1').attr('height')
    //            totalDistance = totalDistance + Number($(pageid).attr('height')) + 5;
    //        }
    //    }
    //}
    //else {
    for (var i = 1; i <= copypage; i++) {
        var pageid = "#page" + i;
        //if (i == currentpage) {
        //    totalDistance = totalDistance + (Number($(pageid).attr('height'))) + 50;
        //}
        //else
        if (i == copypage) {
            fromtop = calcFromTop(currentpage);
            totalDistance = totalDistance + fromtop;
        }
        else {
            totalDistance = totalDistance + Number($(pageid).attr('height')) + 5;
        }
    }
    //}
    return totalDistance;
}
function calcFromTop(currentpage) {
    var ft = 0;
    currentpage = Number(currentpage);
    //var pageid = "#drag-" + pagex;
    var currentY = Number($(targetElm).attr("data-y"));
    for (var i = 1; i <= currentpage; i++) {
        var thispageid = "#page" + i;
        if (i == currentpage) {
            ft = currentY;
        }
        else {
            currentY = currentY - Number($(thispageid).attr("height"))
        }
    }
    return ft;
}
function generateLAbleBase64() {
    //debugger;
    var canvas = document.querySelector('#code128');
    var ctx = canvas.getContext('2d');
    var barcodeMarginTop = 45;
    var typeid = $("#hdnDoctype").val();
    var serial = $("#hdnDocserial").val();
    var txtDocName = $("#hdnDocname").val();
    JsBarcode("#code128", "000000" + documentId.split("-")[0], {
        margin: 5,
        marginTop: barcodeMarginTop,
        marginLeft: 25,
        marginRight: 35,
        displayValue: false,
        height: 40,
        width:1.5
    });
    ctx.font = 'bold 9px Monospace';
    ctx.textAlign = 'center';
    ctx.fillStyle = 'black';
    var txtBarCode1 = "العنوان : " + txtDocName + "";
    let yourDate = new Date()
    var datstr = yourDate.toISOString().split('T')[0];
    var txtBarCode2 = "التاريخ : " + datstr + "   " + "رقم المستند :" + documentId.split("-")[0] + '-' + getSort();
    ctx.fillText(txtBarCode1, -80, 8, canvas.width);
    ctx.fillText(txtBarCode2, -80, 20, canvas.width);
    var txtBarCode3 = "";
    if (typeid != "" && typeid != null) {
        if (typeid == "1") {
            txtBarCode3 = "رقم الصادر : " + serial;
        }
        else {
            txtBarCode3 += "رقم الوارد : " + serial;
        }
        ctx.fillText(txtBarCode3, -80, 32, canvas.width);
    }
    var canva = $('#code128');
    var url = canva[0].toDataURL();
    //alert(url);
    return url;
}
function getSort() {
    var sort = 0;
    var collection = $("img[data-type=1]");
    for (var i = 0; i < collection.length; i++) {
        if (Number($(collection[i]).attr("data-sort")) > sort) {
            sort = Number($(collection[i]).attr("data-sort"));
        }
    }
    return sort + 1;
}