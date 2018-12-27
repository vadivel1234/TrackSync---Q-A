
$(document).on("click", "#searchbutton", function (event) {
    var sqlQuery = $('#searchtext').val().trim();
    if (sqlQuery != null && sqlQuery != "") {
        GridSearch()
    }
});

$("#searchbutton").keydown(function (e) {
    if (e.keyCode == 13) {
        var sqlQuery = $('#searchbutton').val().trim();
        if (sqlQuery != null && sqlQuery != "") {
            GridSearch()
        }
    }
});

function GridSearch() {
    if (!$("#gridsearchResult").hasClass("e-grid")) {
        var serachQuery = $('#searchtext').val().trim();;
        var dataSource = ej.DataManager({
            url: "/searchcollection/" + serachQuery, adaptor: "UrlAdaptor"
        });

        $("#gridsearchResult").ejGrid({
            dataSource: dataSource,
            allowPaging: true,
            allowFiltering: true,
            allowSorting: true,
            allowSelection: true,
            pageSettings: { pageSize: 10 },
            actionBegin: function (args) {
                $(".searchresult").css("display", "block");
            },
            columns: [
                        { field: "source", headerText: "Source", width: 90 },
                        { field: "questionID", headerText: 'Question Id', width: 90 },
                        { field: "title", headerText: 'Title', width: 80 },
                         { field: "creationDate", headerText: 'Created Date', format: "{0:MM/dd/yyyy}", width: 80 },
                        { field: "viewCount", headerText: 'View Count', width: 80 },
                        { field: "Score", headerText: 'Score', width: 80 }

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
