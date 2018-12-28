function showWaitingPopupForElement(elementId) {
    $("#" + elementId).showWaitingPopUp();
}
function removeWaitingPopupFromElement(elementId) {
    $("#" + elementId).removeWaitingPopUp();
}
(function ($) {
    jQuery.fn.showWaitingPopUp = function (defaultValue) {
        var defaults = {
            opacityRange: 0.4,
            loadingImage: "//cdn.syncfusion.com/content/images/Support/images/spinner.gif",
            color: "#F8F8F8",
            center: 'false',
            isGridsection:'false'
        };

        var options = $.extend(defaults, defaultValue);
        if (options.isGridsection == 'false') {
            var tag = '<div id="_waiting-popup_" style="display: none; height: 100%; position: absolute; top: 0; width: 100%; left: 0">' +
                              '<div style="background-color:' + options.color + '; height: 100%; opacity:' + options.opacityRange + '; width: 100%; filter: alpha(opacity= ' + (0 * 100) + ');"> </div>' +
                              '<img src="' + options.loadingImage + '" alt="loading" style="position: absolute;"/>' +
                              '</div>';
        }
        else {
            var tag = '<div id="_waiting-popup_" style="display: none; height: 100%; position: absolute; top: 0; width: 100%; left: 0">' +
                              '<div style="background-color:' + options.color + '; height: 100%;opacity:' + options.opacityRange + '; width: 100%; filter: alpha(opacity= ' + (options.opacityRange * 100) + ');"> </div>' +
                              '<img src="' + options.loadingImage + '" alt="loading" style="position: absolute;"/>' +
                              '</div>';
        }
        $(this).append(tag);
        $(this).css('position', 'relative');
        $(this).find('#_waiting-popup_').css('display', 'block');
        var width = $(this).outerWidth();
        var height = $(this).outerHeight();
        var _top;
        var _left;
        try {
            if (width != null && height != null) {

                if (options.isGridsection == 'true') {
                    if (height < 350) {
                        _top = (height / 2) - ($('#_waiting-popup_').find('img').height() / 2);
                    }
                    else {
                        _top = 200;
                    }
                }
                else if (options.center == "false") {
                    _top = (height / 2) - ($('#_waiting-popup_').find('img').height() / 2);
                    $(this).find('#_waiting-popup_').find('img').css("position", "absolute");
                }
               
                else {
                    _top = $(window).height() / 2;
                    $(this).find('#_waiting-popup_').find('img').css("position", "fixed");
                    $(this).find('#_waiting-popup_').css("z-index", '999')
                }
                _left = (width / 2) - ($('#_waiting-popup_').find('img').width() / 2);
                $(this).find('#_waiting-popup_').find('img').css('top', _top).css('left', _left);
                
                
            }
        } catch (e) {
        }
    };

    jQuery.fn.removeWaitingPopUp = function () {
        $(this).find('#_waiting-popup_').remove();
        $(this).css('position', '');
        return true;
    };
})(jQuery);