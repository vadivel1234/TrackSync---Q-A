
function GridSearch() {
    if (!$("#gridsearchResult").hasClass("e-grid")) {
        var serachQuery = "grid";
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
            columns: [
                        { field: "source",  headerText: "Source", textAlign: ej.TextAlign.Right, width: 90 },
                        { field: "questionID", headerText: 'Question Id', width: 90 },
                        { field: "title", headerText: 'Title', textAlign: ej.TextAlign.Right, width: 80 }
            ]
        });

    }
    else {

    }
}

//$.views.helpers({
//    format: function (date) {
//        if (date != null) {
//            if (!isSafari) {
//                var options = {
//                    year: "numeric", month: "short", day: "numeric", hour: "2-digit", minute: "2-digit"
//                };
//                return date.toLocaleTimeString("en-us", options);
//            }
//            else {
//                return date;
//            }
//        }
//        else {
//            return null;
//        }

//    }
//});