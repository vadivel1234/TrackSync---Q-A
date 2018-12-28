
var startDate = "";
var endDate = "";

function DefaultDate() {
    var start = moment().subtract(1, 'days').startOf('day')
    var end = moment().subtract(1, 'days').endOf('day')
    $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    startDate = start.format('DD/MM/YYYY h:mm A');
    endDate = end.format('DD/MM/YYYY h:mm A');
}

function datechange(start, end) {
    $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    startDate = start.format('DD/MM/YYYY h:mm A');
    endDate = end.format('DD/MM/YYYY h:mm A');
    GridSearch();
}

$(document).on("click", "#searchbutton", function (event) {
    var sqlQuery = $('#searchtext').val().trim();
    if (sqlQuery != null && sqlQuery != "") {
        GridSearch()
    }
});

$("#searchtext").keydown(function (e) {
    if (e.keyCode == 13) {
        var sqlQuery = $('#searchtext').val().trim();
        if (sqlQuery != null && sqlQuery != "") {
            GridSearch()
        }
    }
});

$(document).ready(function () {

    $('#reportrange').daterangepicker({
        startDate: startDate,
        endDate: endDate,
        locale: { cancelLabel: 'Clear' },
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, datechange);

    $('#reportrange').on('cancel.daterangepicker', function (ev, picker) {
        $('#reportrange span').html('Select Date');
        startDate = "";
        endDate = "";
        GridSearch();
    });

});

function GridSearch() {
    if (!$("#gridsearchResult").hasClass("e-grid")) {
        var serachQuery = $('#searchtext').val().trim();
        var dataSource = ej.DataManager({
            url: "/searchcollection", adaptor: "UrlAdaptor"
        });

        $("#gridsearchResult").ejGrid({
            dataSource: dataSource,
            allowPaging: true,
            allowFiltering: true,
            allowSorting: true,
            allowSelection: true,
            pageSettings: { pageSize: 10 },
            actionBegin: function (args) {
                this.model.query._params.push({ key: "$field", value: $('#searchtext').val().trim() });
                $(".searchresult").css("display", "block");
                $("#gridsearchResult").showWaitingPopUp();
                if (startDate != null && endDate != null) {
                    this.model.query._params.push({ key: "$startdate", value: startDate });
                    this.model.query._params.push({ key: "$enddate", value: endDate });
                }
            },
            actionComplete: function (args) {
                $("#gridsearchResult").removeWaitingPopUp();
            },
            columns: [
                        { field: "source", headerText: "Source", width: 40 },
                        { field: "questionID", headerText: 'Question Id', width: 50, template: "<a href='{{:link}}' target='_blank'>{{:questionID}}</a>" },
                        { field: "title", headerText: 'Title', width: 80, template: "<a href='{{:link}}' target='_blank'>{{:title}}</a>" },
                        { field: "creationDate", headerText: 'Created Date', format: "{0:MM/dd/yyyy}", width: 40, allowFiltering: false },
                        { field: "lastActivityDate", headerText: 'Last Activity', format: "{0:MM/dd/yyyy}", width: 40, allowFiltering: false },
                        { field: "viewCount", headerText: 'View Count', width: 30 },
                        { field: "score", headerText: 'Score', width: 30 }

            ]
        });

    }
    else {
        var gridObj = $("#gridsearchResult").data("ejGrid");
        if (gridObj != null) {
            gridObj._renderGridHeader;
            gridObj.refreshContent(false);
        }
    }
}
