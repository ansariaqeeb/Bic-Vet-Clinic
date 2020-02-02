(function ($) {
    var HelpId = "";
    var IsShown = false;
    var grid_body = "";//helptable cell table
    var grid_container = "";//helptable containing div
    var grid_heading = "";//helptable heading table
    var LoadDiv = "";//div created for modal popup below perticular control
    var FormTitle = "";
    var DTLURL = "";
    //This function is called when focus is set to control
    $.c9ShowHelp = function (params) {

        var arr = [];
        var ParamName = new Array();  //Array to store parameter name
        var ParamVal = new Array();//Array to store parameter value
        var ModalPopup = '';
        var Controlid = '';
        $.each(params, function (name, obj) {

            if (name == 'ControlId') {
                Controlid = obj;
            }
            if (name == 'Parameters') {
                $.each(params.Parameters, function (name, val) {
                    if (name == "HELPID") {
                        HelpId = val;
                    }
                    ParamName.push(name);
                    ParamVal.push(val == null ? "" : val);
                });
            }
        });
        //Create url to hit action
        var URL = "/LookUp/GetHelpLookUp";
        if (ParamName.length > 0 && ParamVal.length > 0) {

            var url = URL + "?";
            var dtlurl = "/LookUp/CommonHelpDetails" + "?";
            for (var i = 0; i < ParamName.length; i++) {
                url = url + ParamName[i] + '=' + ParamVal[i];
                dtlurl = dtlurl + ParamName[i] + '=' + ParamVal[i];
                if (i != ParamName.length - 1) {
                    url = url + "&";
                    dtlurl = dtlurl + "&";
                }
            }
            URL = url;
            DTLURL = dtlurl;
        }
        
        var buttonHTML = '';
        $.each(params.buttons, function (name, obj) {
            // Generating the markup for buttons: 
            if (name == 'OK') {
                buttonHTML += '<button style="margin-right:-5px;" type="button" disabled  class="btn btn-xs btn-info btn-app btn-custom btn-CustomSaveBtn"  title="' + name + '" id="btnHelp' + name + '" value="' + name + '" >Ok</button>&nbsp;&nbsp;';

            }
            else {
                buttonHTML += '<button type="button" class="btn btn-xs btn-danger btn-app btn-custom" title="' + name + '" id="btnHelp' + name + '" value="' + name + '" onclick="$.c9ShowHelp.HideModalPopup()" >Cancel</button>';
            }
            if (!obj.action) {
                obj.action = function () { };
            }
        });
        //markup for modal popup
        var popuphtml = [
        '<div id="C9HelpModalPopup" class="dynamicHelpModal">',
        '<div class="dynamicHelpModal-content">',
        '<div class="HelpHeader">',
        '<span class="BtndynamicClose" onclick="$.c9ShowHelp.HideModalPopup();">×</span>',//×
        '<h5 class="dynamicHelpHeader no-margin" style="padding: 5px; font-weight: 600; margin: 4px ! important;"></h5>',
        '</div>',
         '<div id="CharDivwaitingtr" class="widget-box-overlay white-overlay hide" style="margin-top: 3.5%!important; min-height: 200px ! important;bottom:40px;background-color:rgba(255, 255, 255, 0.6);"><div class="center"><img src="/Content/Images/ajax-loader.gif" style="margin-top: 12%;"/></div></div>',
         '<div class="dynamicHelpModal-body col-sm-12 col-xs-12">',
        '</div>',
        '<div id="dynamicHelpBtns" class="dynamicHelpModal-footer col-sm-12 col-xs-12 align-right">' + buttonHTML + '</div>',  // HelpFormBtns,
        '</div>',  // content
        '</div>',  // modalWindow";
        ].join('');


        FormTitle = $("#" + Controlid).attr("placeholder");
        grid_body = "#HelpBodytbl_" + HelpId + "";
        grid_heading = "#HelpHeadtbl_" + HelpId + "";
        grid_container = "#HelpLoadTable_" + HelpId + "";
        LoadDiv = "CommonHelpModal_" + Controlid + "";
        //Append div to open modal popup
        $("#" + Controlid).parent("div").append('<div id="' + LoadDiv + '">' + popuphtml + '</div>');
        $("#C9HelpModalPopup").css("display", "block");
        $("#C9HelpModalPopup").find('.dynamicHelpHeader').text(FormTitle);
        $("#CharDivwaitingtr").removeClass('hide'); $("#dynamicHelpBtns").addClass('hide');
        //call url to get help table data and its configuration details
        $.get(URL, function (data) {

            if (data != "" || data != null) {
                //Append div to open modal popup
                // $("#" + Controlid).parent("div").append('<div id="' + LoadDiv + '">' + popuphtml + '</div>');
                //$("#C9HelpModalPopup").css("display", "block");
                //$("#CharDivwaitingtr").removeClass('hide');
                $.c9ShowHelp.ShowModalPopup(FormTitle, data);
                $("#" + Controlid).blur();
                $("#SearchHelpTxtbox_" + HelpId + "").focus();
                //get click events of popup  OK button
                var buttons = $('#dynamicHelpBtns button[type=button]');
                i = 0; var resjson = null;
                $.each(params.buttons, function (name, obj) {
                    buttons.eq(i++).click(function (data) {
                        //if (name == "OK") {
                        //obj.action($.c9ShowHelp.ReadRowData(grid_body, grid_heading));
                        resjson = $.c9ShowHelp.ReadRowData(grid_body, grid_heading);
                        
                        if ($.isEmptyObject(resjson)) {
                                ShoCommonHelpErrMsg(1);
                                return false;
                            }
                        else {
                            obj.action(resjson);
                                $.c9ShowHelp.HideModalPopup();
                                return false;
                            }
                        //}
                       
                    });
                });

                //get row double click event
                $(grid_container).on('dblclick', function (e) {
                    if (!$(e.target).hasClass('header-cell')) {//prevent event propogation if clicked row is header row
                        $('#btnHelpOK').click();
                    }
                });
                //get click event of table row
                $(grid_container).on('click', function (e) {
                    if (!$(e.target).hasClass('header-cell')) {//prevent event propogation if clicked row is header row
                        HighLightRow($(e.target).parent('tr'));
                    }
                });
                // get keyboard events
                $(document).unbind('keydown').bind('keydown', function (e) {
                    navigateRows(e, grid_body);
                });
            }
            else {
                ShoCommonHelpErrMsg(0); $("#" + Controlid).trigger('blur');

            }
        }).fail(function () {
            $("#" + Controlid).blur();// alert("Error occured while processing");
            ShoCommonHelpErrMsg(0);
        }).success(function () {
            $(".btn-CustomSaveBtn").attr("disabled", false);
        });

        //navigate through table rows using keyboard up/down keys
        function navigateRows(e, table) {
            var highlightedRow = $(table + " .row_selected");
            var tbodyElement, trElements, nextElement, prevElement = "";
            if (highlightedRow.length > 0) // table cell is selected
            {
                tbodyElement = highlightedRow.parent('tbody');
                trElements = tbodyElement.find('tr');
                nextElement = highlightedRow.next('tr');
                prevElement = highlightedRow.prev('tr');
                switch (e.keyCode) {
                    case 13://Enter key
                        $('#btnHelpOK').click();
                        break;
                    case 27://Escape key
                        HideModalPopup();
                        break;
                    case 40://down arrow key
                        $("#SearchHelpTxtbox_" + HelpId + "").trigger('blur');
                        if (nextElement.length) {
                            HighLightRow(nextElement);
                            ($(table).parent("div")).ScrollToRow(nextElement);
                        }
                        else if (trElements.length) {
                            HighLightRow(trElements[0]);

                            ($(table).parent("div")).ScrollToRow(trElements[0]);
                        }
                        break;
                    case 38://up arrow key
                        $("#SearchHelpTxtbox_" + HelpId + "").trigger('blur');
                        if (prevElement.length) {
                            HighLightRow(prevElement);
                            ($(table).parent("div")).ScrollToRow(prevElement);
                        }
                        else if (trElements.length) {
                            HighLightRow(trElements[trElements.length - 1]);
                            ($(table).parent("div")).ScrollToRow(trElements[trElements.length - 1]);
                        }
                        break;
                }
            }
        }

        //function to highlight selected row
        function HighLightRow(el) {

            if ($(el).hasClass('clsNoHelpData')) {
                //nothing to do in this case
            }
            else {
                var selected_row = $(el).attr("data-index");
                $(el).addClass("row_selected");
                $(grid_body + " tr").each(function () {
                    var rowid = $(this).attr("data-index");
                    if (selected_row != rowid && $(this).hasClass("row_selected")) {//de-highlight remaining rows
                        $(this).removeClass("row_selected");
                    }
                });
            }
            //$($(grid_body).parent("div")).ScrollToRow(el);
        }
        //scroll table with table row selection
        $.fn.ScrollToRow = function scroll(elem) {
            $(this).scrollTop($(this).scrollTop() - $(this).offset().top + $(elem).offset().top);
            return this;
        }

    }
    //function to display error msgs----Required toastr js and css
    function ShoCommonHelpErrMsg(flag) {
        toastr.options.closeButton = true;
        toastr.options.preventDuplicates = true;
        toastr.clear();
        if (flag == 0) {//If Error occured while data loading
            toastr.error("Error Occured While Processing.", "Error", { timeOut: 5000 });
        }
        else if (flag == 1) {//If error occured while row selection/on save click
            toastr.warning("Select Record.", "Invalid Selection", { timeOut: 5000 });
        }
    }
    //function to open modal popup
    $.c9ShowHelp.ShowModalPopup = function (Title, content) {
        $("#C9HelpModalPopup").closest(".modal-content").append("<div class='C9overlay'></div>");
        $("#C9HelpModalPopup").find('.dynamicHelpHeader').text(Title);
        $("#C9HelpModalPopup").find('.dynamicHelpModal-body').html(content);
        // $('body').css("overflow", "hidden");
        // $("#CharDivwaitingtr").addClass('hide');
    }
    //function to close modal popup
    $.c9ShowHelp.HideModalPopup = function () {
        $("#C9HelpModalPopup").css("display", "none");
        $("#" + LoadDiv).remove();
        $(".C9overlay").remove();
        // $('body').css("overflow", "auto");
    }
    //function to read selected row 
   
    $.c9ShowHelp.ReadRowData = function (bodytable, headtable) {
        
        var obj = {}; var tableData = []; var tableHead = []; var newRes = {}; 
        //Get Array of all td values
        $(bodytable + " .row_selected").each(function () {
            $(this).children('td').each(function () {
                tableData.push($.trim($(this).html()).replace(/&amp;/g, "&"))
            });
        });
        //Get array of all th
        $(headtable + " th").each(function () {
            tableHead.push($.trim($(this).attr("data-colname")));
        });
        //create json array of all values and return to page
        for (var i = 0; i < tableData.length; i++) {
            obj[tableHead[i]] = tableData[i];
        }
        if (HelpId == 1) {
            var uid = obj['ITEMID'];
            var strurl = DTLURL += "&ITEMID=" + uid;
            $("#CharDivwaitingtr").removeClass('hide');
            $.ajax({
                url: strurl,
                type: "get",
                async: false,
                success: function (dtls) {
                    $("#CharDivwaitingtr").removeClass('hide');
                    if (dtls != "" || dtls != null) {
                        var newobj = dtls[0]; //$.parseJSON(dtls);
                      
                        $.each(newobj, function (k, v) {
                          
                            //$.each(obj, function (m, n) {
                            //    if (k == m) {
                            //        obj[m] = v;
                            //    }
                            //    else {
                            newRes[k] = v;
                               // }
                            //})
                        });
                    }
                    if ($.isEmptyObject(newRes)) {
                        ShoCommonHelpErrMsg(1);
                        newRes = null;
                    }
                },
                error: function () {
                    ShoCommonHelpErrMsg(0);
                    $("#CharDivwaitingtr").removeClass('hide');
                }
            });
            
            return (newRes);
        }
        else {
            if ($.isEmptyObject(obj)) {
                ShoCommonHelpErrMsg(1);
                obj = null;
            }
            return (obj);
        }
    }
})(jQuery);

