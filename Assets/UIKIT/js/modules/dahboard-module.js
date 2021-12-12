


var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

var randomScalingFactor = function () {
    return Math.round(Math.random() * 50);
};









//var randomScalingFactor = function () {
//    return Math.round(Math.random() * 100);
//};




//////// run charts

window.onload = function () {
    
};

function fillLineChar() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/fillLineChar",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var canArr = [];
            for (var i = 0; i < jsdata.length; i++) {
                canArr.push(jsdata[i].Id);
            }
            var maxCount = Math.max.apply(Math, jsdata.map(function (o) { return o.Id; }));
            if (isNaN(maxCount)) {
                maxCount = 10;
            }
            else {
                if (Math.round(maxCount / 10) < 5) {
                    maxCount =   (Math.round(maxCount / 10) * 10) + 10
                }
                else {
                    maxCount = (Math.round(maxCount / 10) * 10);
                }
            }
            var config = {
                type: 'line',
                data: {
                    labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31'],
                    datasets: [{
                        label: 'My First dataset',
                        backgroundColor: window.chartColors.blue,
                        borderColor: window.chartColors.blue,
                        data: canArr,
                        fill: true,
                    }]
                },
                options: {
                    responsive: true,
                    tooltips: {
                        mode: 'index',
                        intersect: false,
                    },
                    hover: {
                        mode: 'nearest',
                        intersect: true
                    },
                    scales: {
                        xAxes: [{
                            display: true,
                            scaleLabel: {
                                display: false,
                                // labelString: 'Month'
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: false,
                                // labelString: 'Value'
                            },
                            ticks: {
                                min: 0,
                                max: maxCount,

                                // forces step size to be 5 units
                                stepSize: 5
                            }
                        }]
                    },
                    legend: {
                        display: false
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return tooltipItem.yLabel;
                            }
                        }
                    }
                }
            };
            var ctx1 = document.getElementById('canvasone').getContext('2d');
            window.myLine = new Chart(ctx1, config);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}
function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}
function fillBarChar() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/fillBarChar",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var canArr = [];
            var nameArr = [];
            var colorArr = [];
            for (var i = 0; i < jsdata.length; i++) {
                var lgdname = jsdata[i].Id + '       ' + jsdata[i].Name;
                canArr.push(jsdata[i].Id);
                nameArr.push(lgdname);
                colorArr.push(getRandomColor());
            }
            var config2 = {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: canArr,
                        backgroundColor: colorArr,
                        label: 'Dataset 1'
                    }],
                    labels: nameArr
                },
                options: {
                    responsive: true,
                    legend: {
                        position: 'left',
                    },
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    }
                }
            };
            var ctx2 = document.getElementById('canvastwo').getContext('2d');
            window.myDoughnut = new Chart(ctx2, config2);
        },
        error: function (result) {
            // alert("Error");
        }
    });
}

function loadTodayEvents() {
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/GetTodayTasks",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var eventsTB = [];
            for (var i = 0; i < jsdata.length; i++) {
                eventsTB.push({
                    title: jsdata[i].Name
                    , start: jsdata[i].Start,
                    end: jsdata[i].End
                });
            }



            var calendarEl = document.getElementById('calendar-aside');
            var date = formatDate(new Date());
            var dir = lang == 'ar' ? 'rtl' : 'ltr';
            var monthArr = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var monthArrAR = ["يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو", "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"];
            var dayArr = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
            var dayArrAR = ["الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"];
            var calendarAside = new FullCalendar.Calendar(calendarEl, {
                //plugins: [dayGridPlugin, momentPlugin],
                firstDay: date,
                locale: lang,
                defaultView: 'timeGridDay',
                direction: dir,
                initialView: 'timeGridDay',
                titleFormat: function () {
                    if (lang == 'ar') {
                        var date = new Date();
                        var day = dayArrAR[date.getDay()];
                        var month = monthArrAR[date.getMonth()];
                        return 'اليوم' + ":" + " " + day + " " + date.getDate() + " " + month;
                    }
                    else {
                        var date = new Date();
                        var day = dayArr[date.getDay()];
                        var month = monthArr[date.getMonth()];
                        return 'Today' + ":" + " " + day + " " + date.getDate() + " " + month;
                    }
                },
                headerToolbar: {
                    left: false,
                    center: 'title',
                    right: false
                },
                businessHours: {
                    // days of week. an array of zero-based day of week integers (0=Sunday)
                    daysOfWeek: [1], // Monday - Thursday
                    startTime: '01:00', // a start time (10am in this example)
                    endTime: '01:05', // an end time (6pm in this example)
                },
                selectConstraint: "businessHours",
                height: 'auto',
                navLinks: true, // can click day/week names to navigate views
                //editable: false,
                //selectable: false,
                selectMirror: true,
                nowIndicator: true,
                events: eventsTB
            });

            calendarAside.render();
        },
        error: function (result) {
            // alert("Error");
        }
    });
}

$(function () {
    fillBarChar();
    fillLineChar();
    loadTodayEvents();
})