var statistic = function () {
    var self = self || {};

    self.drowNewReturnedClientChart = function () {
        $('.chart').easyPieChart({
            easing: 'easeOutBounce',
            lineWidth: 12,
            scaleLength: 8,
            size: 120,
            lineCap: "square",
            trackColor: "#FAC552",
            barColor: "#E9422E",
            animate: 2000,
            onStep: function (from, to, percent) {
                $(this.el).find('.percent').text(Math.round(percent));
            }
        });
    };

    self.drowAuthorizeTypeChart = function () {
        var data = [
            { label: '<span class="label-facebook"><i class="fa fa-facebook-official"></i> Facebook</span>', data: 19.5, color: "#335397" },
            { label: '<span class="label-twitter"><i class="fa fa-twitter-square"></i> Twitter</span>', data: 4.5, color: "#00c7f7" },
            { label: '<span class="label-email"><i class="fa fa fa-envelope"></i> Эл. почта</span>', data: 36.6, color: "#E9422E" }
        ];

        $.plot('#authorize-chart', data, {
            series: {
                pie: {
                    radius: 1,
                    innerRadius: 0.7,
                    show: true,
                }
            },
            legend: {
                show: true
            },
            colors: ["#335397", "#00c7f7", "#E9422E"],
            grid: {
                hoverable: true
            },
            stroke: {
                width: 0.8,
                color: '#808080'
            },
            tooltip: true,
            tooltipOpts: {
                content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
                shifts: {
                    x: 20,
                    y: 0
                }
            },
            defaultTheme: false
        });

        $('.legend .legendColorBox').hide();
    };

    self.drowAvarageDialogsPerDay = function (result) {
        if (result.IsSuccess) {
            $('#dialogs-per-day-chart').empty();
            window.AvarageDialogsPerDayChart = Morris.Line({
                element: 'dialogs-per-day-chart',
                data: result.Data,
                xkey: 'FormatedDate',
                ykeys: ['DialogsCount'],
                labels: ['Диалогов'],
                resize: true
            });
        }
        else {
            NotifyError(result.Message);
        }       
    };

    self.drowAvarageDialogsDurationPerDay = function (result) {
        if (result.IsSuccess) {
            $('#dialogs-duration-per-day-chart').empty();
            window.AvarageDialogsDurationPerDayChart = Morris.Line({
                element: 'dialogs-duration-per-day-chart',
                data: result.Data,
                xkey: 'FormatedDate',
                ykeys: ['DurationInMinutes'],
                labels: ['минут'],
                lineColors: ['#167f39'],
                resize: true
            });
        }
        else {
            NotifyError(result.Message);
        }
    };

    self.drowMessageCountDifference = function (result) {
        if (result.IsSuccess) {
            $('#message-count-difference-chart').empty();
            Morris.Bar({
                element: 'message-count-difference-chart',
                data: result.Data,
                xkey: '2016',
                ykeys: ['MessagesInCurrect', 'MessagesInPrevios'],
                labels: ['сообщений в текущем', 'сообщений в предыдущем']
               
            });
        }
        else {
            NotifyError(result.Message);
        }
    }

    return self;
}();

$(window).on('resize', function () {
    if (!window.recentResize) {
        window.AvarageDialogsPerDayChart.redraw();
        window.AvarageDialogsDurationPerDayChart.redraw();
        window.recentResize = true;
        setTimeout(function () { window.recentResize = false; }, 250);
    }
});