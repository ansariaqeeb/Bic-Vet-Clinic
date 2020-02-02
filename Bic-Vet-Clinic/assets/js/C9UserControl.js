(function ($) {
    var url = '';
    var grid_data = null;                              //For table name
    var IsMultiSelect = false;                         //Parameter for multi select 
    var Controlid;                                     //Cntrole 

    //This function will run on call of this function
    $.c9HelpControl = function (params) {
        var ParamName = new Array();  //Array to store parameter name
        var ParamVal = new Array();     //Array to store parameter value
        var HColName = new Array();     //Array to store heading column to show
        var HColVal = new Array();      //Array to store heading column value
        var HColWidth = new Array();      //Array to store Columns width
        var SearchParamName = '';
        var MDivId = '';
        //Unloading the data from table
        $('#c9HCntrl').empty();
        $('#c9HCntrl').remove();
        if ($('#c9HCntrl').length) {
            // A confirm is already shown on the page: 
            return false;
        }

        //Reading all parameters provided from outside
        $.each(params, function (name, obj) {
            if (name == 'Parameters') {
                $.each(params.Parameters, function (name, obj) {
                    ParamName.push(name);
                    ParamVal.push(obj == null ? "" : obj);
                });
            } else if (name == 'Columns') {
                $.each(params.Columns, function (name, obj) {
                    HColName.push(name);
                    HColVal.push(obj == null ? "" : obj);
                });
            } else if (name == 'Width') {
                $.each(params.Width, function (name, obj) {
                    HColWidth.push(obj); 
                });
            }
            else if (name == 'MultiSelect') {
                IsMultiSelect = obj;
            } else if (name == 'Controlid') {
                Controlid = obj;
            } else if (name == 'Url') {
                url = obj;
            } else if (name == 'SearchParamName') {
                SearchParamName = obj;
            } else if (name == 'DivId') {
                MDivId = obj;
            }
        });
        //Creating buttons 
        var buttonHTML = '';
        $.each(params.buttons, function (name, obj) {
            // Generating the markup for buttons: 
            if (name == 'OK') {
                buttonHTML += '<button type="button" class="btn btn-minier btn-info" title="' + name + '" id="btn' + name + '" value="' + name + '" ><i class="fa fa-check bigger-120"></i></button>';//class="btn btn-xs btn-inverse"
            } else {
                buttonHTML += '<button type="button" class="btn btn-minier btn-danger" title="' + name + '" id="btn' + name + '" value="' + name + '" ><i class="fa fa-remove bigger-120"></i></button>';// class="btn btn-xs"
            }
            if (!obj.action) {
                obj.action = function () { };
            }
        });

        //Wraping div around the text box
        if ($('#mainDiv' + Controlid).length) {
            // A confirm is already shown on the page:  
        } else {
            $("#" + Controlid).wrap("<div id='mainDiv" + Controlid + "' style='margin:0;padding:0;'></div>");
        }

        //Hide idve when click on outside div
        $(document).click(function (event) {
            if (!$(event.target).parents("#c9HCntrl").length && !$(event.target).parents("#mainDiv" + Controlid).length) {
                $.c9HelpControl.hide();
            }
        });

        //Generating table and div html 
        var markup = [
         '<div id="c9HCntrl" style="position:absolute;z-index:99999;width:500px;height:210px;box-shadow: 3px 3px 3px #D3D3D3;border: 1px solid #5897fb;background-color:#fff;">',
         '<table id="Maintable" height="100%" width="100%"><tbody><tr><td colspan="2" style="padding:0;margin:0">',
         '<table id="grid-table"></table></td></tr>',
         '<tr height="30px" style="border-top:1px solid #777"><td align="left" style="padding:1px;margin:0">',
         '<span id="SearchSpan" class="input-icon"><input type="text" style="width:300px;height:29px" class="nav-search-input" autocomplete="off" placeholder="Search ..." id="txtc9hcntrlSearch"><i class="ace-icon fa fa-search nav-search-icon"></i></span>',
         '</td>',
         '<td style="padding:3px;margin:0"><div id="confirmButtons" style="text-align:right">',
         buttonHTML,
         '</div></td></tr></tbody></table></div>'
        ].join('');

        var DivId = $("#" + Controlid).parent("div").attr("id");

        //Appending table to div
        $(markup).hide().appendTo('#' + DivId).fadeIn();

        if (ParamName.length > 0) {
            for (var i = 0; i < ParamName.length; i++) {
                if (SearchParamName == ParamName[i]) {
                    $('#txtc9hcntrlSearch').val(ParamVal[i]);
                }
            } 
        } 
        $('#txtc9hcntrlSearch').focus();
        $("#txtc9hcntrlSearch").putCursorAtEnd();
        //Search text box key up envent
        $("#txtc9hcntrlSearch").on("keyup", function (e) {
            //TAB: 9,ENTER: 13,ESC: 27,SPACE: 32,LEFT: 37,UP: 38,RIGHT: 39,DOWN: 40,SHIFT: 16,
            //CTRL: 17,ALT: 18,PAGE_UP: 33,PAGE_DOWN: 34,HOME: 36,END: 35,BACKSPACE: 8,DELETE: 46,
            var txtSearch = $(this).val();
            var keyPress = e.keyCode;
            var Code = new Array(9, 13, 27, 32, 37, 38, 39, 40, 16, 17, 18, 33, 34, 36, 35, 46);
            var isCharecter = false;

            //Checking key code if other then charecter then doesnt reload the data
            for (var i = 0; i < Code.length; i++) {
                if (Code[i] != keyPress) {
                    isCharecter = true;
                } else {
                    isCharecter = false;
                    if (keyPress == 13) {
                        $('#btnOK').click().trigger();
                    } else if (keyPress == 38 || keyPress == 40) {
                        var grid = jQuery("#grid-table");

                        ids = grid.jqGrid("getDataIDs");
                        if (ids && ids.length > 0)
                            grid.jqGrid("setSelection", ids[0]);
                        $('#grid-table' + ' tr[id=' + ids[0] + ']').focus();

                        //$("#grid-table").jqGrid('bindKeys', {
                        //    scrollingRows: true
                        //});

                    }
                    break;
                }
            }
            if (isCharecter) {
                var URL = '';
                //setting the values for url,columns and colModal 
                //Desc = txtSearch;
                // { showhelp: ShowHelp, Itemid: Itemid, Desc: Desc, XmlFile: XmlFile }; 

                if (ParamName.length > 0 && ParamVal.length > 0) {
                    var urlParam = url + '?';
                    for (var i = 0; i < ParamName.length; i++) {
                        if (ParamName[i] == SearchParamName) {
                            urlParam = urlParam + '' + ParamName[i] + '=' + txtSearch + '';
                        } else {
                            urlParam = urlParam + '' + ParamName[i] + '=' + ParamVal[i] + '';
                        }

                        if (i != ParamName.length - 1) {
                            urlParam = urlParam + "&";
                        }
                    } 
                }
                URL = urlParam;//"/CommonLibrary/_FillItemCode";//?showhelp=" + ShowHelp + "&Itemid=" + Itemid + "&Desc=" + Desc + "&XmlFile=" + XmlFile
                //$('#c9HCntrl').hide();
                //Getting json form data base
                $.getJSON(URL, function (data) {
                    var colNames = ''; var colModel = '';

                    grid_data = data;
                    var colval = new Array();
                    $.each(data, function (index, value) {
                        $.each(value, function (index, val) {
                            colval.push(index);
                        });
                        return false;
                    });

                    if (colval.length > 0) {
                        var strColMod = '[';
                        var strColName = '[';
                        for (var i = 0; i < colval.length; i++) {
                            strColMod = strColMod + '{"name":"' + colval[i] + '", "index":"' + colval[i] + '", "editable": "false","width":"200px;", "resizable":"false"}';
                            strColName = strColName + '"' + colval[i] + '"';
                            if (i != colval.length - 1) {
                                strColMod = strColMod + ",";
                                strColName = strColName + ",";
                            }
                        }
                        strColMod = strColMod + ']';
                        strColName = strColName + ']';
                        colModel = JSON.parse(strColMod);

                        if (HColName.length > 0 && HColVal.length > 0) {

                            var CustColumns = '[';
                            var HideCol = '[';
                            var x = -1;
                            for (var i = 0; i < colval.length; i++) {
                                x = -1;
                                for (var j = 0; j < HColName.length; j++) {

                                    if (colval[i] == HColName[j]) {
                                        x = j;
                                        break;
                                    }
                                } 
                                if (x == -1) {
                                    HideCol = HideCol + '"' + colval[i] + '",';
                                    CustColumns = CustColumns + '"' + colval[i] + '",';
                                } else {
                                    CustColumns = CustColumns + '"' + HColVal[x] + '",';
                                }

                            }
                            CustColumns = CustColumns.substring(0, CustColumns.length - 1) + ']';
                            HideCol = HideCol.substring(0, HideCol.length - 1) + ']';
                            colNames = JSON.parse(CustColumns);
                            HideCol = JSON.parse(HideCol); 
                           
                        } else {
                            colNames = JSON.parse(strColName);
                        }

                    } else {

                        var strColMod = '[';
                        var strColName = '[';
                        for (var i = 0; i < HColVal.length; i++) {
                            strColMod = strColMod + '{"name":"' + HColVal[i] + '", "index":"' + HColVal[i] + '", "editable": "false","width":"80", "resizable":"false"}';
                            strColName = strColName + '"' + HColVal[i] + '"';
                            if (i != HColVal.length - 1) {
                                strColMod = strColMod + ",";
                                strColName = strColName + ",";
                            }
                        }
                        strColMod = strColMod + ']';
                        strColName = strColName + ']';
                        colModel = JSON.parse(strColMod);


                        var CustColumns = '['; 
                        for (var i = 0; i < HColVal.length; i++) {
                            CustColumns = CustColumns + '"' + HColVal[i] + '",';
                        }
                        CustColumns = CustColumns.substring(0, CustColumns.length - 1) + ']';
                        colNames = JSON.parse(CustColumns);  
                    }
                    
                   
                    var grid_selector = "#grid-table";

                    $(grid_selector).jqGrid('GridUnload');
                    //Loadng data to jqgrid
                    jQuery(grid_selector).jqGrid({
                        data: grid_data,
                        datatype: "local",
                        colNames: colNames,
                        colModel: colModel,
                        viewrecords: false,
                        altRows: false,
                        multiselect: IsMultiSelect,
                        multiboxonly: false,
                        add: false,
                        del: false,
                        edit: false,
                        autowidth: true, 
                        loadtext: "Loading Data, Please Wait...",
                        onSelectRow: function (id, status) {

                        },
                        ondblClickRow: function (rowid) {
                            $('#btnOK').click().trigger();
                        },
                        gridComplete: function () {

                        },
                        loadComplete: function (data) {
                            jQuery(grid_selector).jqGrid('hideCol', HideCol); 

                            $(grid_selector).jqGrid('setGridWidth', 495);


                            //For setting width to columns
                            var i = 0;
                            var TDIndex = new Array();    //Array for index of table TD 
                            $('.ui-jqgrid-labels > th').each(function () { 
                                if ($(this).css('display') != 'none') { 
                                    TDIndex.push(i);
                                }   
                                i++;
                            }); 
                             
                            //For loop for adding width to th and td of table 
                            for (var i = 0; i < TDIndex.length; i++) {  
                                $('.ui-jqgrid-labels > th:eq(' + TDIndex[i] + ')').css('width', HColWidth[i]) 
                                $(grid_selector + ' tr').find("td:eq(" + TDIndex[i] + ")").each(function () { $(this).css('width', HColWidth[i]); }) 
                            }

                            var grid = jQuery(grid_selector);
                            ids = grid.jqGrid("getDataIDs");
                            if (ids && ids.length > 0)
                                grid.jqGrid("setSelection", ids[0]);
                            $(grid_selector).jqGrid('bindKeys', {
                                onEnter: function () {
                                    $('#btnOK').click().trigger();
                                }
                            });
                            //For div left position 
                            showtable(MDivId,Controlid);
                            //$('#c9HCntrl').show();
                        },
                        loadError: function (xhr, st, err) {
                            var error_msg = xhr.responseText
                            var msg = "Some errors occurred during processing:"
                            msg += '\n\n' + error_msg
                            alert(msg)
                        }
                    });
                    //$(grid_selector).jqGrid('setGridWidth', '495');

                    //$(grid_selector).jqGrid('setColProp', 'text', { width: 200 });
                     
                });
            }
        });
        $('#txtc9hcntrlSearch').keyup();


        var buttons = $('#confirmButtons button[type=button]');
        i = 0;

        $.each(params.buttons, function (name, obj) {
            buttons.eq(i++).click(function (data) {
                // Calling the action attribute when a
                // click occurs, and hiding the confirm. 
                $('#c9HCntrl').hide();
                obj.action($.c9HelpControl.getData());
                $.c9HelpControl.hide();
                return false;
            });
        });
    }
    //for putting cursor at the end of search text
    jQuery.fn.putCursorAtEnd = function () {
        return this.each(function () {
            $(this).focus()
            // If this function exists...
            if (this.setSelectionRange) {
                // Double the length because Opera is inconsistent about whether a carriage return is one character or two. Sigh.
                var len = $(this).val().length * 2;
                this.setSelectionRange(len, len);
            } else {
                // ... otherwise replace the contents with itself
                // (Doesn't work in Google Chrome) 
                $(this).val($(this).val());
            }
            // Scroll to the bottom, in case we're in a tall textarea
            // (Necessary for Firefox and Google Chrome)
            this.scrollTop = 999999;

        });

    };
    //hiding div 
    $.c9HelpControl.hide = function () {
        $("#c9HCntrl").empty();
        $("#c9HCntrl").remove();
    }

    //returning selected data
    $.c9HelpControl.getData = function () {
        if (IsMultiSelect) {
            //For multi select
            var rowData = new Array();
            var selectedrows = $('#grid-table').jqGrid('getGridParam', 'selarrrow');
            if (selectedrows.length) {
                for (var i = 0; i < selectedrows.length; i++) {
                    rowData.push($('#grid-table').jqGrid('getRowData', selectedrows[i]));
                    
                }
            }
        } else {
            //For single select
            var rowId = $('#grid-table').jqGrid('getGridParam', 'selrow');
            var rowData = jQuery($('#grid-table')).getRowData(rowId);
        }
        return (rowData);
    }

    //Div design for display
    function showtable(MDivId, Controlid) {
        var txtPosition = $('#' + Controlid).position();
        var txtoffset = $('#' + Controlid).offset();
        var txtLeftPos = txtPosition.left;
        var txtTopPos = txtPosition.top;
        var txtleftOffset = txtoffset.left;
        var txttopOffset = txtoffset.top;  
        var docViewLeft = $(window).scrollLeft();
        var docViewRight = docViewLeft + $(window).width();


        var cssTop = ($('#' + Controlid).height() + 10) + "px 1px 0px 1px";
        var cssBottom = "1px 1px " + ($('#' + Controlid).height() + 20) + "px 1px"; 

        var cntrlWidht = $('#' + Controlid).width();

        var leftpos = parseFloat(txtleftOffset) + parseFloat(cntrlWidht) - 700;

        var mindivposL = docViewRight - 500;

        //For top height of control
        if (txttopOffset == 0) {
            $("#c9HCntrl").css("top", $('#' + Controlid).height() + 10 + "px");
        } else {
            var top = $('#' + Controlid).height() + 12 + txtTopPos;
            $("#c9HCntrl").css("top", top + "px");
        }


        if (txtleftOffset == 0) {
            $("#c9HCntrl").css("left", "0px");
        } else if (parseFloat(mindivposL) <= parseFloat(txtLeftPos)) {
            $("#c9HCntrl").css("left", leftpos + "px");
        } else if (parseFloat(mindivposL) <= parseFloat(txtleftOffset)) {
            $("#c9HCntrl").css("left", leftpos + "px");
        } else if (parseFloat(txtleftOffset) < parseFloat(mindivposL)) {
            $("#c9HCntrl").css("left", txtLeftPos + "px");
            //$("#c9HCntrl").css("margin", cssTop);
        }
       
        
        if (!isDivWidthVisible("#c9HCntrl", Controlid)) { 
            var out = txtLeftPos + cntrlWidht - 500;
            $("#c9HCntrl").css("left", out + "px"); 
        }
        if (MDivId != '') { 
            var divPosition = $('#' + MDivId).position();
            var divoffset = $('#' + MDivId).offset();
            var divLeftPos = divPosition.left;
            var divTopPos = divPosition.top;
            var divleftOffset = divoffset.left;
            var divtopOffset = divoffset.top;
            var divWidth = divLeftPos + $('#' + MDivId).width();

            var CntrlPos = $("#c9HCntrl").position();
            var Cntrloffset = $('#c9HCntrl').offset();
            var CntrlLeftPos = CntrlPos.left;
            var CntrlTopPos = CntrlPos.top;
            var CntrlleftOffset = Cntrloffset.left;
            var CntrltopOffset = Cntrloffset.top;
            var CntrlWidth = CntrlleftOffset + $('#c9HCntrl').width(); 
          
            if (divWidth < CntrlWidth) {
                var out = txtLeftPos + cntrlWidht - 500;
                $("#c9HCntrl").css("left", out + "px");
            } 
        } 
    }

})(jQuery);

function isDivWidthVisible(elem, Controlid) {
    var docViewLeft = $(window).scrollLeft();
    var docViewRight = docViewLeft + $(window).width();
    var elemLeft = $("#" + Controlid).offset().left;
    var elemRight = elemLeft + 500; 
     
    return ((elemRight >= docViewLeft) && (elemLeft <= docViewRight)
      && (elemRight <= docViewRight) && (elemLeft >= docViewLeft));
}
 