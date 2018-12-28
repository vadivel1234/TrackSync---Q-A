
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
            },
            actionComplete: function (args) {
                $("#gridsearchResult").removeWaitingPopUp();
            },
            columns: [
                        { field: "source", headerText: "Source", width: 40 },
                        { field: "questionID", headerText: 'Question Id', width: 50, template: "<a href='{{:link}}' target='_blank'>{{:questionID}}</a>" },
                        { field: "title", headerText: 'Title', width: 80, template: "<a href='{{:link}}' target='_blank'>{{:title}}</a>" },
                        { field: "creationDate", headerText: 'Created Date', format: "{0:MM/dd/yyyy}", width: 40 },
                        { field: "lastActivityDate", headerText: 'Last Activity', format: "{0:MM/dd/yyyy}", width: 40 },
                        { field: "viewCount", headerText: 'View Count', width: 30 },
                        { field: "Score", headerText: 'Score', width: 30 }

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
