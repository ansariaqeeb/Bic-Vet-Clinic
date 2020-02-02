(function ($) {


    //Function for show validation message
    $.c9Message = function Message(errmsg, TITLE, TYPE) {
        
        toastr.options.closeButton = true;
        toastr.options.preventDuplicates = true;
        toastr.clear();
        if (TYPE == "S") {
            toastr.success(errmsg, TITLE, { timeOut: 5000 });
        }
        else if (TYPE == "E") {

            toastr.error(errmsg, TITLE, { timeOut: 5000 });
        } else if (TYPE == "W") {
            toastr.warning(errmsg, TITLE, { timeOut: 5000 });
        }
    }
    //Function for validating input value in textbox
    $.c9Validateinput = function ValSpecialChar(event, id, charallow,ctrlId) {
        var charCode = (event.which) ? event.which : event.keyCode;
        var start = event.target.selectionStart;
        var end = event.target.selectionEnd;
        if (charallow == null || charallow == '') {
            return true;
        } else {
            if (charCode == 8 || charCode == 9) {
                return true;
            }  
            var decimal;
            if (ctrlId==undefined) {
                decimal = '';
            } else {
                decimal = $('#' + ctrlId).val();
            }

            switch (charallow.trim() + decimal.trim()) {
                case "A-Za-z"://Case for a to z capital and small without any space
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
                        return false;
                    }
                    break;
                case "A-Z"://Case for a to z capital without any space
                    if (charCode < 65 || charCode > 90) {
                        $.c9Message('Capital letters only', 'Warning', "W");
                        return false;
                    }
                    break;
                case "a-z"://Case for a to z small without any space
                    if (charCode < 97 || charCode > 122) {
                        $.c9Message('Small letters only', 'Warning', "W");
                        return false;
                    }
                    break;
                case "A-Z0-9"://Case for a to z capital and numbers without any space
                    if ((charCode < 65 || charCode > 90) && (charCode < 48 || charCode > 57)) {
                        $.c9Message('Capital letters and numbers only', 'Warning', "W");
                        return false;
                    }
                    break;
                case "A-Za-zS"://Case for a to z capital and small with space
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                        return false;
                    }
                    break;
                case "A-Za-z.":
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 46) {
                        return false;
                    }
                    break;
                case "A-Za-z-":
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 45) {
                        return false;
                    }
                    break;
                case "A-Za-z-0-9"://Case for a to z capital and small with Sign(-) and Number(0-9)
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 45 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    break;
                case "A-Za-z-.":
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && (charCode != 45 || charCode != 46)) {
                        return false;
                    }
                    break;
                case "A-Za-z0-9":
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    break;
                case "A-Za-z0-9S": //Case for a to z capital and small with space and numbers
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && (charCode < 48 || charCode > 57) && charCode != 32) {
                        return false;
                    }
                    break;
                case "A-Za-z0-9.S": //Case for a to z capital and small with numbers,dot and space  - rutuja(25-07-18)
                    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 32) {
                        return false;
                    }
                    break;
                case "0-9":
                    
                    //if the letter is not digit then display error and don't type anything
                    if (charCode != 8 && charCode != 0  && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else {
                        return true;
                    }
                    break;
                case "0-9.": 
                    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                       
                        return false;
                    }
                    //just one dot
                    if (id.value.split('.').length > 1 && charCode == 46) {
                  
                        return false;
                    }
                   
                    break;
                case "0-9.0":

                    //if the letter is not digit then display error and don't type anything
                    if (charCode != 8 && charCode != 0 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else {
                        return true;
                    }
                    break;

                    //if (charCode == 8 || charCode == 110) {
                    //    return true;
                    //} else if ((charCode != 46 || $(id).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57)) {
                    //    return false;
                    //}
                    //else if ($(id).val().indexOf('.') != -1) {
                    //    if ($(id).val().substring($(id).val().indexOf('.')).length > 0 && $(id).val() != parseFloat(0)) {
                    //        return false;
                    //    }
                    //}
                    //break;
                case "0-9.1":

                    if (charCode == 8 || charCode == 110) {
                        return true;
                    } else if ((charCode != 46 || $(id).val().indexOf('.') != -1) && start == end && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else if ($(id).val().indexOf('.') != -1) {
                        if ($(id).val().substring($(id).val().indexOf('.')).length > 1 && start == end && $(id).val() != parseFloat(0)) {
                            return false;
                        }
                    }
                    break;
                case "0-9.2":
                    
                    //alert(start+' : '+ end);
                    if (charCode == 8 || charCode == 110) {
                        return true;
                    } else if ((charCode != 46 || $(id).val().indexOf('.') != -1) && start == end && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else if ($(id).val().indexOf('.') != -1) {                     
                        if ($(id).val().substring($(id).val().indexOf('.')).length > 2 && start == end && $(id).val() != parseFloat(0)) {
                            return false;
                        }
                    }
                    break;
                case "0-9.3":
                    if (charCode == 8 || charCode == 110) {
                        return true;
                    } else if ((charCode != 46 || $(id).val().indexOf('.') != -1) && start == end && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else if ($(id).val().indexOf('.') != -1) {
                        if ($(id).val().substring($(id).val().indexOf('.')).length > 3 && start == end) {
                            return false;
                        }
                    }
                    break;
                case "0-9.4":
                    
                    if (charCode == 46 && $(id).val().trim().length == 0)
                    {
                        
                        return false;
                    }
                    else
                    if (charCode == 8) {
                    
                        return true;
                    } else if ((charCode != 46 || $(id).val().indexOf('.') != -1) && start == end && (charCode < 48 || charCode > 57)) {
                       
                        return false;
                    }
                    else if ($(id).val().indexOf('.') != -1) {
                        if ($(id).val().substring($(id).val().indexOf('.')).length > 4 && start == end) {
                            return false;
                        }
                    }
                   
                    break;
                case "0-9.5":
                    if (charCode == 8 || charCode == 110) {
                        return true;
                    } else if ((charCode != 46 || $(id).val().indexOf('.') != -1) && start == end && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else if ($(id).val().indexOf('.') != -1) {
                        if ($(id).val().substring($(id).val().indexOf('.')).length > 5 && start == end) {
                            return false;
                        }
                    }
                    break;
                case "0-9.6":
                    if (charCode == 8 || charCode == 110) {
                        return true;
                    } else if ((charCode != 46 || $(id).val().indexOf('.') != -1) && start == end && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else if ($(id).val().indexOf('.') != -1) {
                        if ($(id).val().substring($(id).val().indexOf('.')).length > 6 && start == end) {
                            return false;
                        }
                    }
                    break;
                case "%":
                    var val = $(id).val();
                    if (val > 100) {
                        $.c9Message('Value Should be less than or equal to 100 ', 'Warning', 'W');
                        $(id).val("0");
                        $(id).focus();
                        return false;
                    }
                    break;
                case "0-9-":
                    if (charCode == 8 || charCode == 0 || charCode == 46) {
                        return true;
                    } else if ((charCode != 45 || $(id).val().indexOf('-') != -1) && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    break;
                case "0-9-.":
                    if (((charCode != 45 || $(id).val().indexOf('-') != -1) && (charCode != 46 || $(id).val().indexOf('.') != -1)) && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    break;
                case "0-9-,":
                    if (charCode == 8 || charCode == 110 || charCode == 46) {
                        return true;
                    } else if ((charCode != 45 && charCode != 44) && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    break;
                case "0-9:"://condition to allow numbers and colon (:)---used for timepicker
                    if (charCode == 8 || charCode == 110 || charCode == 58) {
                        return true;
                    }
                    else if ((charCode != 58) && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    break;
                case "0-9-M"://case to allow numbers with multiple hyphen
                    if (charCode == 8 || charCode == 110 || charCode == 46) {
                        return true;
                    } else if (charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    break;
                case "Email":
                    if (IsEmail($(id).val().trim()) == false && $(id).val() != '') {
                        $(id).focus();
                        $(id).val('');
                        $.c9Message('Invalid Email Id', 'Warning', "W");
                    }
                    break;
                case "Website":
                    if (IsValidURL($(id).val().trim()) == false && $(id).val() != '') {
                        $(id).focus();
                        $(id).val('');
                        $.c9Message('Invalid URL', 'Warning', "W");
                    }
                    break;
                case "PAN":
                    if (IsValidPAN($(id).val().trim()) == false && $(id).val() != '') {
                        $(id).focus();
                        $(id).val('');
                        $.c9Message('Invalid PAN Number', 'Warning', "W");
                    }
                    break;
                case "mobileno":
                    if (IsMobileNo($(id).val().trim()) == false && $(id).val() != '') {
                        $(id).focus();
                        $(id).val('');
                        $.c9Message('Invalid Mobile No', 'Warning', "W");
                    }
                    break;
            }

            return true;
        }
    }

    //Function for validating input value not Copy Paste in textbox
    $.c9ValidateCopyPaste = function ValidateCopyPaste(ctrlId) {
        $(ctrlId).bind('cut copy paste', function (e) {
            e.preventDefault();
        });
    }
    //this code for cut copy paste action for Numeric Flied
    $.c9CheckInputType = function CheckInputType()
    {
        //alert('c9CheckInputType');
       //debugger;
       //// //set focus to first field    on 25JAN
       //// // alert('input')
       //// var allForm = $("form");
       //// var lastForm = allForm.last();
       //// var allinput = $(lastForm).find(':input').filter(':text').filter('.clsreq');   //return all input elements in that specific form.
       ////// console.log(allinput);
       //// var Flag = false; //only Once Time for INput
       //// $(allinput).each(function (index) {
       ////     var Input = $(this);
       ////     if (!Flag) {
       ////         var ID = Input.attr('id');
       ////         if (ID == "INVCNUMB" || ID == "INVCDATE") //Ignore Document Number or  Document Date
       ////         { Flag = false; } else {
       ////             if (Input.prop('readOnly') == true || Input.prop('disabled') == true)//filter data
       ////             { Flag = false; }
       ////             else {
       ////                 //set focus to first field
       ////                    if ($(Input).hasClass("select2"))//this for Select2 
       ////                             {//set focus to first field
       ////                                 $("#" + Input.attr('id')).select2("focus");
       ////                                 $("#" + Input.attr('id')).select2('focus', 'val');
       ////                                 $("#s2id_" + ID).addClass('select2-container-active');
       ////                        //$("#" + Input.attr('id')).on("select2-focus", function (e) { log("focus"); })
       ////                                 $('#' + ID).prev('.select2-container').find('.select2-input').focus();
       ////                                 console.log('ID: ' + Input.attr('id') + '     Name: ' + Input.attr('name') + '         Type: ' + Input.attr('type') + '       Value: ' + Input.val());
       ////                                 Flag = true;
       ////                             } else {
       ////                                 $("#" + ID).focus();
       ////                                 console.log('ID: ' + Input.attr('id') + '     Name: ' + Input.attr('name') + '         Type: ' + Input.attr('type') + '       Value: ' + Input.val());
       ////                                 Flag = true;
       ////                                 $("#s2id_" + ID).addClass('select2-container-active');
       ////                             }
       ////                 //$("#" + ID).focus();
       ////                 //$("#" + ID).select2("focus");
       ////                 ////$("#" + ID).select2('focus', true);
       ////                 ////for Select
       ////                 ////$("#s2id_" + ID).focus();
       ////                 ////$("#s2id_" + ID).select2("focus");
       ////                 //////for Prevs Item Trans
       ////                 //$("#s2id_" + ID).addClass('select2-container-active');
       ////                 //console.log('ID: ' + Input.attr('id') + '     Name: ' + Input.attr('name') + '         Type: ' + Input.attr('type') + '       Value: ' + Input.val());
       ////                 //Flag = true;
       ////             }
       ////         }
       ////     } else { }

       //// });

        //Input Numeric
        //this code for cut copy paste action for Numeric Flied
        $("input[c9-type='numeric']").bind('cut copy paste', function (e)
        {
            //alert('copy');
            //console.log(e);
            //debugger;

            //only for Paste event 
            var tt = e.type;
            if (e.type == "paste")
            {
                var currVal = e.originalEvent.clipboardData.getData('Text');
                var regxp = /^\d+(\.\d{0,10})?$/;
                var testing = regxp.test(currVal.trim());
                if (!regxp.test(currVal.trim()))
                { e.preventDefault(); }//not numeric
            }

        });

        ////All Form Data
        // var Flag = false; //only Once Time for INput
        //$('input, select').each(function(index){  
        //    var Input = $(this);
        //    //if (Input.is(':text'))
        //    //{
        //    //    var isReq = $(Input).hasClass("clsreq");
        //    //    console.log('ID: ' + Input.attr('id') + '     Name: ' + Input.attr('name') + '         Type: ' + Input.attr('type') + '       Value: ' + Input.val() + '    Class clsreq : ' + isReq.toString());
        //    //}
        //    if (!Flag)
        //    {
        //        if (Input.is(':text'))
        //        {
        //            var isReq = $(Input).hasClass("clsreq");
        //            if (isReq)
        //            {
        //                //INVCNUMB     INVCDATE   
        //                var ID = Input.attr('id').trim();
        //                if (ID != "INVCNUMB")//Ignore Document Number
        //                {
        //                    if (ID != "INVCDATE")//Ignore Document Date
        //                    {
        //                        if ($(Input).hasClass("select2"))//this for Select2 
        //                        {
        //                            //set focus to first field
        //                            $("#"+ID).select2("focus");
        //                            console.log('ID: ' + Input.attr('id') + '     Name: ' + Input.attr('name') + '         Type: ' + Input.attr('type') + '       Value: ' + Input.val() + '    Class clsreq : ' + isReq.toString());
        //                            Flag = true;
        //                        } else
        //                        {
        //                            ////set focus to first field
        //                            //$("#DOCENTRYTYPEID").select2("focus");
        //                            //console.log('ID: ' + Input.attr('id') + '     Name: ' + Input.attr('name') + '         Type: ' + Input.attr('type') + '       Value: ' + Input.val() + '    Class clsreq : ' + isReq.toString());
        //                            $("#" + ID).focus();
        //                            console.log('ID: ' + Input.attr('id') + '     Name: ' + Input.attr('name') + '         Type: ' + Input.attr('type') + '       Value: ' + Input.val() + '    Class clsreq : ' + isReq.toString());
        //                            Flag = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }  
        //    }
        //});

    }


    //function to Replace Special Characters
    $.c9ReplaceSpecialCharacters =function Replace_SpecialCharacters(inputStr) {
        //alert('Replace_SpecialCharactersText');
       // debugger
        //console.log('Replace_SpecialCharactersText')
        //.replace("#", "")
        //inputStr = inputStr.replace("$", "").replace("%", "").replace("^", "").replace("&", "").replace("*", "").replace("(", "").replace(")", "")
        //                   .replace(",", "").replace(".", "").replace("<", "").replace(">", "").replace("'", "").replace("\"", "").replace("!", "")
        //                   .replace("~", "").replace("`", "").replace("{", "").replace("}", "").replace("[", "").replace("]", "").replace("+", "").replace("=", "")
        //                   .replace(":", "").replace(";", "").replace("?", "").replace("|", "").replace("@@", "");

        //inputStr = inputStr.replace("<", "&lt;").replace(">", "&gt;").replace("&", "&amp;").replace("'", "&apos;").replace("\"", "&quot;");
        //.replace("\"\"", "&quot;");

        var allTexts = inputStr;
        var replaceText = "";
        for (var i = 0; i < allTexts.length; i++)
        {
           //replaceText = allTexts[i].replace(/&/g, '&amp;')
           //                .replace(/</g, '\&lt;')
           //                .replace(/>/g, '\&gt;')
           //                .replace(/"/g, '\&quot;')
            //                .replace(/'/g, '\&apos;')
            var t = allTexts[i];
            replaceText+= allTexts[i].replace("<", "&lt;")
                .replace(">", "&gt;").replace("&", "&amp;")
                .replace("'", "&apos;").replace("\"", "&quot;");

        }

        //console.log(allTexts);
        //console.log(replaceText);
        return replaceText;
    }


    //function to calculate Item Amount and Round up Unitrate,Amount
    $.c9CalculateItemAmount = function CalAmt(UNITRATEID, QTYID, NOUID, ITEMAMTID) {
        var QUANTITY = $("#" + QTYID).val() == "" ? 0 : parseFloat($("#" + QTYID).val());
        var UNITRATE = $("#" + UNITRATEID).val() == "" ? 0 : parseFloat($("#" + UNITRATEID).val());
        if (!$("#" + QTYID).hasClass('dimn')) {
            $("#" + NOUID).val(QUANTITY);
        }
        var Total = QUANTITY * UNITRATE;
        $("#" + ITEMAMTID).val(Total.toFixed($("#hdnRoundUp").val()));
        // $("#" + UNITRATEID).val(UNITRATE.toFixed($("#hdnRoundUp").val()));
    }

    //function for checking email regular expresion
    function IsEmail(email) {
        //var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var regex = /[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}/igm;
        if (!regex.test(email)) {
            return false;
        } else {
            return true;
        }
    }
    //function for checking Website URL regular expression
    function IsValidURL(url) {
       // var regex = /(https?:\/\/(?:www\.|(?!www))[^\s\.]+\.[^\s]{2,}|www\.[^\s]+\.[^\s]{2,})+$/;//^(http:\/\/www.|https:\/\/www.|ftp:\/\/www.|www.){1}([0-9A-Za-z]+\.)/;
        var regex = /^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/|www\.)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/;
        if (!regex.test(url)) {
            return false;
        } else {
            return true;
        }
    }
    //function for checking PAN number regular expression
    function IsValidPAN(num) {
        var regex = /^[A-Z]{5}\d{4}[A-Z]{1}/;
        if (!regex.test(num)) {
            return false;
        } else {
            return true;
        }
    }

    //function for checking mobile Number regular expresion
    function IsMobileNo(mobileno) {
        var regex = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})/;
        if (!regex.test(mobileno)) {
            return false;
        } else {
            return true;
        }
    }

    //Fucntion for validation on form
    $.c9CheckValidation = function CheckValidation(FORMID) {
        var validate = true;
        $('#' + FORMID + ' .clsreq').each(function () {
           
            if ($(this).hasClass("select2")) {
                var message = $(this).siblings('input:hidden').attr('placeholder');
                var val = $(this).select2("val");
                if (val == "" || val == null || val==0) {
                    validate = false;
                    $(this).find('.select2-choice').addClass('clsShowWarning');
                    $(this).select2("focus", "val");
                    $('#btnMasterSave').attr('disabled', false);
                    $('#Divwaitingtr').addClass('hide');
                    $.c9Message(message, "Warning", "W");
                    return false;
                }
            } else if ($(this).hasClass("select")) {
                var message = $(this).attr('data-placeholder');
                var val = $(this).val();
                if (val == "" || val == null) {
                    validate = false;
                    $(this).addClass('clsShowWarning');
                    $(this).focus();
                    $('#btnMasterSave').attr('disabled', false);
                    $('#Divwaitingtr').addClass('hide');
                    $.c9Message(message, "Warning", "W");
                    return false;
                }
            }
            else {
                var message = $(this).attr('placeholder');
                var val = $(this).hasClass("textarea") ? $(this).text().trim() : $(this).val().trim();
                if (val == "" || val == parseFloat(0) || val == null) {
                    validate = false;
                    $(this).addClass('clsShowWarning');
                    $(this).focus();
                    $('#btnMasterSave').attr('disabled', false);
                    $('#Divwaitingtr').addClass('hide');
                    $.c9Message(message, "Warning", "W");
                    return false;
                }
            }
        });
        return validate;
    }

    //function for saving master details in transaction
    $.c9SaveMaster = function SaveMaster(FORMID, Controller, Action, MENUID, DOCID, SLID,CURID,LoadTrans) {
        $('#btnMasterSave').attr('disabled', true);
        $('#Divwaitingtr').removeClass('hide');
       
        if ($.c9CheckValidation(FORMID)) {
            var SeiealizData = $('#' + FORMID).serialize();
            var url = "/" + Controller + "/" + Action + "/";
            $.post(url, SeiealizData, function myfunction(data) {
                var errid = data.RESULTID;
                var errmsg = data.MESSAGE;
                var TITLE = data.TITLE;
                var TYPE = data.TYPE;
                if (errid > 0) {
                    $('#MID').val(errid);
                    var elemArr = $('#' + FORMID)[0].elements;
                    $('#' + FORMID).find('.fa-file-text').removeAttr("onclick");
                    $.each(elemArr, function (n, ele) {
                        var typ = $(ele).attr("type");
                        if ($(this).hasClass('clsreqBGColor')) { $(this).removeClass('clsreqBGColor') }
                      
                        if (typ == "text" || typ == "hidden") {                         
                            $(this).attr('readonly', true);
                            $(this).removeAttr('onfocus');
                            $(this).removeAttr('onclick');
                            $(this).removeAttr('onkeyup');
                            $(this).removeAttr('onkeydown');
                            $(this).attr("data-target", "");
                            $("#" + $(this).attr("id")).select2("readonly", true);
                            if ($(this).hasClass('date-picker')) {
                                $(this).off();
                            }
                            if ($(this).hasClass('timepicker')) {//added by Supriya Kokate to off time picker
                                $(this).off();
                            }
                            
                        }
                        else if (typ == "radio" || typ == "checkbox") {
                            $(this).attr("disabled", true);
                        }
                        else if ($(ele).is("button")) {
                            $(this).attr("disabled", true);
                            $(this).removeAttr("onclick");
                        }
                        else if ($(ele).is("textarea")) {
                            $(this).attr("readonly", true);
                        }
                        else if ($(this).hasClass("select")) {
                            $(this).attr("disabled", true);
                        }
                    })
                    //$('#' + FORMID + ' input').each(function () {
                    //    var typ = $(this).attr("type");
                    //    if (typ=="text" || typ=="hidden") {
                    //        $(this).attr('readonly', true);
                    //        $(this).removeAttr('onfocus');

                    //        $("#" + $(this).attr("id")).select2("readonly", true);
                    //        //if ($(this).hasClass('select2-offscreen')) {
                    //        //    $("#" + $(this).attr("id")).select2("readonly", true);
                    //        //}
                    //        if ($(this).hasClass('date-picker')) {
                    //            $(this).off();
                    //        }
                    //    }
                    //   else if (typ == "radio") {
                    //        $(this).attr("readonly", true);
                    //    }

                    //});
                    //$('#' + FORMID + 'textarea').each(function () {
                    //    $(this).attr("readonly", true);
                    //});
                    if (TYPE != 'S') {
                        $.c9Message(errmsg, TITLE, TYPE);
                    }
                    //$('#btnMastCancel').attr('disabled', true);
                    $('#Divwaitingtr').addClass('hide');
                    var FOOTERAPPL = $('#FOOTERAPPL').val();
                    if (typeof LoadTrans === "undefined" || LoadTrans === null) {
                        LoadTrans = "LoadTrans";
                    }
                    if ((SLID == undefined || SLID == '') && (CURID == undefined || CURID == '')) {
                        var Data = { MENUID: MENUID, DOCID: DOCID }
                    } else {
                        var Data = { MENUID: MENUID, DOCID: DOCID, SLID: SLID == undefined ? 0 : $('#' + SLID).val(), CURID: CURID == undefined ? 0 : $('#' + CURID).val() }
                    } 
                    
                    $.get('/' + Controller + '/' + LoadTrans + '/', Data, function (Trdata) {
                        $('#divTrans').html(Trdata);
                        $('#MastBtnStrip').addClass('hide');
                        $.c9bindTransGrid(errid, MENUID, $('#FLAG').val());

                        $('#panel_2').click();
                        if (FOOTERAPPL) {
                            $('#Footer').removeClass('hide');
                        } 
                      
                        document.getElementById("divTrans").scrollIntoView({ block: 'start', behavior: 'smooth' });
                    });

                } else {

                    $.c9Message(errmsg, TITLE, TYPE);
                    $('#btnMasterSave').attr('disabled', false);
                    $('#Divwaitingtr').addClass('hide');
                }
            });
        }
    }
    //Function for binding and loading transaction table
    $.c9bindTransGrid = function bindTransGrid(MID, MENUID, MODE, PageNo) {
        var SearchTxt = typeof ($("#SearchTrLookup_" + MENUID)) == "undefined" ? "" : $("#SearchTrLookup_" + MENUID).val();//Added by Supriya Kokate on 16 APR 2019 to add Search Text for trans lookup
        if (PageNo == undefined)
        {
            PageNo=1
        }
        $("#TrLookupDivwaitingtr").removeClass('hide');
        var dataL = { MENUID: MENUID, RMID: MID, MODE: MODE, PageNo, SearchTxt: SearchTxt };
        $.get("/LookUp/TransData/", dataL, function (Trdata) {
            $("#tblTranslookup").html(Trdata);
            $("#TrLookupDivwaitingtr").addClass('hide');
        });
    }
    //Fuction for saving footer details
    $.c9SaveFooter = function SaveFooter(FLAG, Controller, Action, MENUID, DOCID) {
        ////alert('c9SaveFooter');
        ////debugger;
        var footerAppl = $('#FOOTERAPPL').val();
        $('#btnFNSubmitFooter').attr('disabled', true);
        $('#btnFCSubmitFooter').attr('disabled', true);
        $('#btnFooterDiscard').attr('disabled', true);
        $('#btnFooterCancel').attr('disabled', true);

        //$('#Divwaitingtr').removeClass('hide');
        ////clswaitingMaster specific loading spinner
        //var LoadingID = "#Divwaitingtr";
        //var ldObj = $('.clswaitingMaster');
        //if (ldObj.length > 0) {
        //    $('.clswaitingMaster').removeClass('hide');
        //    LoadingID = ".clswaitingMaster";
        //} else {
        //    $('#Divwaitingtr').removeClass('hide');
        //    LoadingID = "#Divwaitingtr";
        //}

        //clswaitingMaster specific loading spinner
        var LoadingID = "#Divwaitingtr";
        var ldObj = $('.clswaitingMaster');
        if (ldObj.length > 0) {
            //$('.clswaitingMaster .center i').css("margin-top", "52%"); style="position: fixed;top: 50%;"
            $('.clswaitingMaster .center i').css("position", "fixed");
            //$('.clswaitingMaster .center i').css("top", "50%");
            $('.clswaitingMaster .center i').css("margin-top", "10%");
            $('.clswaitingMaster').removeClass('hide');

            LoadingID = ".clswaitingMaster";

        } else {
            $('#Divwaitingtr').removeClass('hide');
            LoadingID = "#Divwaitingtr";
        }


        var validate = true;

        $('#frmFooter .clstaxapplicable').each(function () {
            if ($('#taxtable tr.clstax').length <= 0) {
                validate = false;
                $('#btnFNSubmitFooter').attr('disabled', false);
                $('#btnFCSubmitFooter').attr('disabled', false);
                $('#btnFooterDiscard').attr('disabled', false);
                $('#btnFooterCancel').attr('disabled', false);
                $(LoadingID).addClass('hide');
                $.c9Message('Define taxes for this document!', 'Warning', 'W');
                return false;
            }
            else {//check if account is blank for tax fields having amount > 0

                $('#taxtable tbody tr .clsTAXFIELD').each(function () {
                    var rowid = $(this).attr("data-index");
                    var TaxField = $("#DESCRIPTION_" + rowid).val();
                    var tabid = $(this).attr("data-tab");
                    //if ($("#ISHIDDEN_" + rowid).val() == 0 && ($(this).val() > 0) && $("#FCAMT_" + rowid).val() != 0 && ($("#POSTACCID_" + rowid).val() == 0 || $("#POSTACCID_" + rowid).val() == null)) {

                    //    $("#POSTACCID_" + rowid).select2('focus');
                    //    $.c9Message(TaxField + ' account should not be blank', 'Warning', 'W');
                    //    $('#Footer_' + tabid).click();
                    //    $('#btnFNSubmitFooter').attr('disabled', false);
                    //    $('#btnFCSubmitFooter').attr('disabled', false);
                    //    $('#btnFooterDiscard').attr('disabled', false);
                    //    $('#btnFooterCancel').attr('disabled', false);
                    //    $('#Divwaitingtr').addClass('hide');
                    //    validate = false;
                    //    return false;

                    //}
                });
            }

        });

        if (validate) {
            $('#frmFooter .clsreq').each(function () {
                if ($(this).hasClass("select2")) {
                    var message = $(this).siblings('input:hidden').attr('placeholder');
                    var tabid = $(this).siblings('input:hidden').attr('data-tabid');
                    var val = $(this).select2("val");
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $('#Footer_' + tabid).click();
                        $(this).select2("focus", "val");
                        $('#btnFNSubmitFooter').attr('disabled', false);
                        $('#btnFCSubmitFooter').attr('disabled', false);
                        $('#btnFooterDiscard').attr('disabled', false);
                        $('#btnFooterCancel').attr('disabled', false);
                        $(LoadingID).addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                } else if ($(this).hasClass("select")) {
                    var message = $(this).attr('data-placeholder');
                    var val = $(this).val();
                    if (val == "" || val == null) {
                        validate = false;
                        $(this).focus();
                        $('#btnFNSubmitFooter').attr('disabled', false);
                        $('#btnFCSubmitFooter').attr('disabled', false);
                        $('#btnFooterDiscard').attr('disabled', false);
                        $('#btnFooterCancel').attr('disabled', false);
                        $(LoadingID).addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
                else {
                    var message = $(this).attr('placeholder');
                    var tabid = $(this).attr('data-tabid');
                    var val = $(this).hasClass("textarea") ? $(this).text().trim() : $(this).val().trim();

                    if (message == "Enter Credit Days") { // allow to accept Zero
                        if (val == "" || val == null) {
                            validate = false;
                            $('#Footer_' + tabid).click();
                            $(this).focus();
                            $('#btnFNSubmitFooter').attr('disabled', false);
                            $('#btnFCSubmitFooter').attr('disabled', false);
                            $('#btnFooterDiscard').attr('disabled', false);
                            $('#btnFooterCancel').attr('disabled', false);
                            $(LoadingID).addClass('hide');
                            $.c9Message(message, "Warning", "W");
                            return false;
                        }
                    }
                    else {
                        if (val == "" || val == null || val == 0) {
                            validate = false;
                            $('#Footer_' + tabid).click();
                            $(this).focus();
                            $('#btnFNSubmitFooter').attr('disabled', false);
                            $('#btnFCSubmitFooter').attr('disabled', false);
                            $('#btnFooterDiscard').attr('disabled', false);
                            $('#btnFooterCancel').attr('disabled', false);
                            $(LoadingID).addClass('hide');
                            $.c9Message(message, "Warning", "W");
                            return false;
                        }
                    }
                }
            });
        }
        if (validate) {//check validation for payment terms
            var vaildFlag = true;
            $('#frmFooter #Footerpytrm .clsIsData').each(function () {
                if ($(this).is(":checked") == true) {
                    var index = $(this).attr("data-id");
                    var tabid = $(this).attr('data-tabid');
                    var amtVal = $('#FPTAMOUNT_' + index).val();
                    if (parseFloat(amtVal) == 0 || amtVal == "" || amtVal == null) {
                        vaildFlag = false;

                        $.c9Message('Amount Should Not Be Blank or Zero For Selected Payment Terms', 'Warning', 'W');
                        validate = false;
                        $('#Footer_' + tabid).click();
                        $('#btnFNSubmitFooter').attr('disabled', false);
                        $('#btnFCSubmitFooter').attr('disabled', false);
                        $('#btnFooterDiscard').attr('disabled', false);
                        $('#btnFooterCancel').attr('disabled', false);
                        $(LoadingID).addClass('hide');
                        return false;
                    }
                }
            });
        }

        if (validate) {//check Acount Linking validation-----------Added by Supriya Kokate
            if ($('#frmFooter #tblFooterAdvanceLink .clsFTRLINKINGROW').length != 0) {
                var tabid = $("#FTRTOTAL_LNKAMT").attr('data-tabid');
                var dispMsg = "";

                if ($("#FTRTOTAL_LNKAMT").text() == "" || parseFloat($("#FTRTOTAL_LNKAMT").text()) == 0) {
                    if (MENUID == 107) {//for Sales Invoice
                        dispMsg = "Advance is given against SO linked to this Invoice.Please Link Amount.";
                    }
                    if (MENUID == 97) {//for Purchase Bill
                        dispMsg = "Advance is given against PO linked to this Bill.Please Link Amount.";
                    }

                    $.c9Message(dispMsg, 'Warning', 'W');
                    validate = false;
                    $('#Footer_' + tabid).click();
                    $('#btnFNSubmitFooter').attr('disabled', false);
                    $('#btnFCSubmitFooter').attr('disabled', false);
                    $('#btnFooterDiscard').attr('disabled', false);
                    $('#btnFooterCancel').attr('disabled', false);
                    $(LoadingID).addClass('hide');
                    return false;
                }
                else if (parseFloat($("#FTRTOTAL_LNKAMT").text()) > parseFloat($("#hdnFooterFinalAmt").val())) {
                    $.c9Message("Total Linked Amount should not be greater than Document Amount.", 'Warning', 'W');
                    $('#Footer_' + tabid).click();
                    $('#btnFNSubmitFooter').attr('disabled', false);
                    $('#btnFCSubmitFooter').attr('disabled', false);
                    $('#btnFooterDiscard').attr('disabled', false);
                    $('#btnFooterCancel').attr('disabled', false);
                    $(LoadingID).addClass('hide');
                    return false;
                    validate = false;
                }
                else {
                    $('#btnFNSubmitFooter').attr('disabled', true);
                    $('#btnFCSubmitFooter').attr('disabled', true);
                    $('#btnFooterDiscard').attr('disabled', true);
                    $('#btnFooterCancel').attr('disabled', true);
                    $(LoadingID).removeClass('hide');
                    //return true;
                    validate = true;
                }
            }

        }
        if (validate) {
            setTimeout(function () {
                if ($("#TXTBOXFOCUSOUT").val() == 0 || typeof ($("#TXTBOXFOCUSOUT").val()) === "undefined") {
                    var SeiealizData = $('#frmFooter').serialize();
                    var url = "/FooterCommon/Footer/";
                    $.post(url, SeiealizData, function myfunction(data) {
                        var errid = data.RESULTID;
                        var errmsg = data.MESSAGE;
                        var TITLE = data.TITLE;
                        var TYPE = data.TYPE;

                        if (errid > 0) {
                            var url = "/" + Controller + "/FinalUpdate/";
                            var MID = $('#MID').val();
                            var FLAGF = $('#FLAG').val().trim() == "I" ? 1 : 2;
                            var data = { FLAG: FLAGF, MID: MID, MENUID: MENUID, DOCID: DOCID }
                            $.post(url, data, function (err) {
                                var errid = err.RESULTID;
                                var errmsg = err.MESSAGE;
                                var TITLE = err.TITLE;
                                var TYPE = err.TYPE;

                                if (errid > 0) {
                                    if (FLAG == '1') {
                                        var url = "/" + Controller + "/" + Action + "/" + MENUID + "/" + DOCID;
                                    } else {
                                        var lkUrl = $("#hdnLookupUrl_" + MENUID).val();//changed by supriya kokate on 14 may 2019 to get Lookup URL for menuXDoc
                                        var url = lkUrl + MENUID + "/" + DOCID;
                                    }
                                    window.location.href = url;
                                    $(LoadingID).addClass('hide');
                                } else {
                                    $(LoadingID).addClass('hide');
                                    $.c9Message(errmsg, TITLE, TYPE);
                                    $('#btnFNSubmitFooter').attr('disabled', false);
                                    $('#btnFCSubmitFooter').attr('disabled', false);
                                    $('#btnFooterDiscard').attr('disabled', false);
                                    $('#btnFooterCancel').attr('disabled', false);
                                }
                            });
                            $('#btnFNSubmitFooter').attr('disabled', false);
                            $('#btnFCSubmitFooter').attr('disabled', false);
                            $('#btnFooterDiscard').attr('disabled', false);
                            $('#btnFooterCancel').attr('disabled', false);
                            $(LoadingID).addClass('hide');
                        } else {
                            $.c9Message(errmsg, TITLE, TYPE);
                            $('#btnFNSubmitFooter').attr('disabled', false);
                            $('#btnFCSubmitFooter').attr('disabled', false);
                            $('#btnFooterDiscard').attr('disabled', false);
                            $('#btnFooterCancel').attr('disabled', false);
                            $(LoadingID).addClass('hide');
                        }
                    });
                }
                else {
                    return false;
                }
            }, 1500);

        }
    }
    //Function for calculating tax
    $.c9CalculateTax = function CalculateTax(FORMID, DIVID, NEXTTAB, PREVTAB,MENUID) {
        $("#TXTBOXFOCUSOUT").val(1);
        $("#TaxTblDivwaitingtr,#CommnTaxTblDivwaitingtr").removeClass("hide");
        var SeiealizData = $('#' + FORMID).serialize();
        var url = DIVID == '1' ? "/FooterCommon/TaxCalculate/" : "/CommonSetting/TaxCalculate?PrevTabId=" + PREVTAB + "&NextTabId=" + NEXTTAB + "&MENUID=" + MENUID;
        $.post(url, SeiealizData, function (data) {
            $('#FooterTab_' + DIVID).html(data);
            $("#TaxTblDivwaitingtr,#CommnTaxTblDivwaitingtr").addClass("hide");

        });
        $("#TXTBOXFOCUSOUT").val(0);

    }
    //Function for getting footer data
    $.c9GetFooter = function GetFooter(MENUID, DOCID, MID) {
        ////alert('c9GetFooter');
        ////debugger;
        var DocAmt = 0;
        var trval = 0;
        var i = 0;
        //clswaitingMaster specific loading spinner
        var LoadingID = "#Divwaitingtr";

        //var findID = $('#Divwaitingtr').hasClass('clswaitingMaster');
        //if (findID) {
        //    $('#Divwaitingtr .clswaitingMaster').removeClass('hide');
        //    LoadingID = "Divwaitingtr .clswaitingMaster";
        //} else {
        //    $('#Divwaitingtr').removeClass('hide');
        //    LoadingID = "Divwaitingtr";
        //}

        var ldObj = $('.clswaitingMaster');
        if (ldObj.length > 0) {
            $('.clswaitingMaster').removeClass('hide');
            LoadingID = ".clswaitingMaster";

        } else {
            $('#Divwaitingtr').removeClass('hide');
            LoadingID = "#Divwaitingtr";
        }
        ////alert('c9GetFooter');
        ////debugger;
        trval = $("#tblTransData tr").length;

        $("#FooterStrip tr td.clsValue div").each(function () {
            if ($(this).attr("data-column") == "Document Amount") {
                DocAmt = $(this).html().trim();
            } else {
                DocAmt = 0;
            }
        });

        if (trval > 1 && DocAmt != 0) {
            if ($("#ISCSAPPL").val() == 1) {
                $.get("/FooterCommon/ValidateTrans", { MENUID: MENUID, DOCID: DOCID, MID: MID }, function (data) {
                    if (data.RESULTID < 0) {

                        $(LoadingID).addClass('hide');
                        $.c9Message(data.MESSAGE, data.TITLE, data.TYPE);
                    }
                    else {
                        $('#btnTransLock').prop("disabled", true);
                        $('#btnTransCancel').prop("disabled", true);
                        $(LoadingID).removeClass('hide');
                        $.get("/FooterCommon/Footer/", { FLAG: $("#FLAG").val(), MENUID: MENUID, DOCID: DOCID, MID: MID, DOCAMT: DocAmt }, function (data) {
                            $("#FooterDetails").html(data);
                            //$('#panel_3').click();
                            //$("#collapseThree").collapse('hide');

                            var res = $("#collapseThree").is('.collapse');
                            if (res) {
                                //$("#collapseThree").collapse('show');
                                //$("#collapseThree").removeClass('.collapse').addClass('in');
                                $('#panel_3').click();
                            }
                            $.c9ScrollingDown();
                            $(LoadingID).addClass('hide');
                            document.getElementById("FooterDetails").scrollIntoView({ block: 'start', behavior: 'smooth' });
                        });
                    }
                })
            }
            else {
                $('#btnTransLock').prop("disabled", true);
                $('#btnTransCancel').prop("disabled", true);
                $(LoadingID).removeClass('hide');
                $.get("/FooterCommon/Footer/", { FLAG: $("#FLAG").val(), MENUID: MENUID, DOCID: DOCID, MID: MID, DOCAMT: DocAmt }, function (data) {
                    $("#FooterDetails").html(data);
                    //$('#panel_3').click();
                    var res = $("#collapseThree").is('.collapse');
                    if (res) {
                        //$("#collapseThree").collapse('show');
                        //$("#collapseThree").removeClass('.collapse').addClass('in');
                        $('#panel_3').click();
                    }
                    $.c9ScrollingDown();
                    $(LoadingID).addClass('hide');
                });
            }

        } else {
            $(LoadingID).addClass('hide');
            $.c9Message("Please fill atleast one transaction", "Warning", "W");
        }

    }
    //Function for loading add transaction form
    $.c9GetAddTrans = function GetAddTrans(Controller, Action, TID, MID, DOCID, MENUID, title) {
        ////alert("c9GetAddTrans");
        ////debugger;
        //for First Title load after Data
        if (typeof title === "undefined" || title === null) {
            title = "Add Transaction";
        }
        $('#hTransHead').text(title);
        $("#Divwaitingtr").removeClass('hide');
        //for Trans Loading Spinner
        // $("#Divwaitingtr_Trans").removeClass('hide');
        $("#DivwaitingTrans").removeClass('hide');
        var URL = '/' + Controller + '/' + Action + '/';
        var data = { TID: TID, MID: MID, DOCID: DOCID, MENUID: MENUID };
        $.get(URL, data, function (data) {
            $('#TransModalLoad').html(data);
            $('#RMID').val(MID);
            //for Trans Loading Spinner
            //$("#Divwaitingtr_Trans").addClass('hide');
            $("#DivwaitingTrans").addClass('hide');

        });
        $("#Divwaitingtr").addClass('hide');
    }
    //function for discarding document 
    $.c9DiscardDocument = function DiscardDocument(Controller, Action, MID, MENUID, DOCID) {
        bootbox.confirm({
            title: 'Confirm discard',
            message: "Are you sure,you want to discard this document?",
            buttons: {
                'confirm': {
                    label: 'Confirm',
                    className: 'btn-sm btn-info'
                },

                'cancel': {
                    label: 'Cancel',
                    className: 'btn-sm btn-danger'
                },
            },
            callback: function (result) {
                if (result == true) {
                    var url = "/UserControl/CallAction/";
                    var data = { DOCID: DOCID, MID: MID, MENUID: MENUID, ActionID: 3 }
                    $.get(url, data, function (err) {
                        var errid = err.RESULTID;
                        var errmsg = err.MESSAGE;
                        var TITLE = err.TITLE;
                        var TYPE = err.TYPE;
                        if (errid > 0) {
                            $.c9Message(errmsg, TITLE, TYPE);
                            var url = "/" + Controller + "/" + Action + "/" + MENUID + "/" + DOCID;
                            window.location.href = url;
                        } else {
                            $.c9Message(errmsg, TITLE, TYPE);
                        }
                    });
                }
            }
        });
    }
    //function for cancel document
    $.c9CancelDocument = function CancelDocument(MENUID, DOCID) {
        var lkUrl = $("#hdnLookupUrl_" + MENUID).val();//changed by supriya kokate on 14 may 2019 to get Lookup URL for menuXDoc
        var url = lkUrl + MENUID + "/" + DOCID;
        window.location.href = url;
    }
    //fnction for swiching panel
    $.c9SwitchPanel = function SwitchPanel(FORMID, id) {
        var validate = true;
        if (FORMID != '') {
            $('#' + FORMID + ' .clsreq').each(function () {
                if ($(this).hasClass("select2")) {
                    var message = $(this).siblings('input:hidden').attr('placeholder');
                    var val = $(this).select2("val");
                    if (val == "" || val == null) {
                        validate = false;
                        $(this).select2("focus", "val");
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                } else if ($(this).hasClass("select")) {
                    var message = $(this).attr('data-placeholder');
                    var val = $(this).val();
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).focus();
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                } else {
                    var message = $(this).attr('placeholder');
                    var val = $(this).hasClass("textarea") ? $(this).text() : $(this).val();
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).focus();
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
            });
        }

        if (!validate) {
            $("#" + id).prop("disabled", true)
        }
        else {
            $("#" + id).prop("disabled", false);
            if (id == 'panel_2') {
            } else if (id == 'panel_3') {

            }
        }
    }
    //function for saving transaction details in transaction
    $.c9SaveTransactionN = function SaveTransaction(FORMID, Controller, Action, FLAG, MID, MENUID, DOCID, ISCOMMSETTING, ITEMID, ITEMCODE, ITEMAMOUNT, TYPEID, HSNID, SLID, DIEAMT, TOOLAMT, fn,ISPAGERENDER) {
        if (ISPAGERENDER == undefined)
        {
            ISPAGERENDER=0
        }
        if (ISPAGERENDER == 0) {
            $('#btnTransSave').attr('disabled', true);
            $('#Divwaitingtr').removeClass('hide');
            var validate = true;
            $('#' + FORMID + ' .clsreq').each(function () {
                if ($(this).hasClass("select2")) {
                    var message = $(this).siblings('input:hidden').attr('placeholder');
                    var val = $(this).select2("val");
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).find('.select2-choice').addClass('clsShowWarning');
                        $(this).select2("focus", "val");
                        $('#btnTransSave').attr('disabled', false);
                        $('#Divwaitingtr').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                } else if ($(this).hasClass("select")) {
                    var message = $(this).attr('data-placeholder');
                    var val = $(this).val();
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).addClass('clsShowWarning');
                        $(this).focus();
                        $('#btnTransSave').attr('disabled', false);
                        $('#Divwaitingtr').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
                else {
                    var focus = $(this).attr('data-focus');
                    var message = $(this).attr('placeholder');
                    var val = $(this).hasClass("textarea") ? $(this).text().trim() : $(this).val().trim();

                    if (val == "" || val == 0 || val == null) {
                        validate = false;
                        if (typeof focus === "undefined" || focus === null) {
                            $(this).focus();
                        }
                        $(this).addClass('clsShowWarning');
                        $('#btnTransSave').attr('disabled', false);
                        $('#Divwaitingtr').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
            });

            if (validate) {


                var serializedData = $("#" + FORMID).serialize();
                var url = "/" + Controller + "/" + Action + "/";
                $.post(url, serializedData, function (data) {
                    if (data.RESULTID > 0) {
                        ret = data.RESULTID
                        setTimeout(function () { $('#LotTab').empty(); $('#divLot').addClass("hide"); }, 500);

                        $.c9bindTransGrid(MID, MENUID, $('#FLAG').val());
                        if (ISCOMMSETTING) {

                            var ISCSAPPL = $('#ISCSAPPL').val() == 0 ? false : true;

                            if (ISCSAPPL) {
                                if (typeof ITEMID === "undefined" || ITEMID === null) {
                                    ITEMID = 0;
                                }
                                if (typeof ITEMCODE === "undefined" || ITEMCODE === null) {
                                    ITEMCODE = "";
                                }
                                if (typeof ITEMAMOUNT === "undefined" || ITEMAMOUNT === null) {
                                    ITEMAMOUNT = 0;
                                }
                                if (typeof TYPEID === "undefined" || TYPEID === null) {
                                    TYPEID = 1;
                                }
                                if (typeof HSNID === "undefined" || HSNID === null) {
                                    HSNID = 0;
                                }
                                if (typeof SLID === "undefined" || SLID === null) {
                                    SLID = 0;
                                }

                                var url = '/CommonSetting/CommonSetting';
                                var data = { FLAG: FLAG, MID: MID, TID: data.RESULTID, DOCID: DOCID, ITEMID: ITEMID, ITEMAMOUNT: ITEMAMOUNT, TYPE: TYPEID, HSNID: HSNID, ACCOUNTID: SLID, DieAmt: DIEAMT, ToolAmt: TOOLAMT }; //, ISFREEISSUE: ISFREEISSUE, ISPROCESS: ISPROCESS, ISLINEWISE: ISLINEWISE
                                $('#SettingModalLoad').load(url, data, function (data) {
                                    $('#btnCancel').click();
                                    $('#btnSetting').click();
                                    $('#SettingHead').html('<span>' + ITEMCODE + '</span>     <span>Item Amount</span>  : <span>' + ITEMAMOUNT + '</span>');
                                    $('#Divwaitingtr').addClass('hide');
                                });
                            } else {
                                $('#btnCancel').click();
                                $('#Divwaitingtr').addClass('hide');
                            }

                        } else {
                            $('#btnCancel').click();
                            $('#Divwaitingtr').addClass('hide');
                            fn(data.RESULTID);
                        }

                    } else {
                        $('#Divwaitingtr').addClass('hide');
                        $('#btnTransSave').attr('disabled', false);
                        $.c9Message(data.MESSAGE, data.TITLE, data.TYPE);
                    }
                });
            }
        }
        else {
            //this code is for render on partial view
            $('#Divwaitingtr').removeClass('hide');
            var validate = true;
            $('#' + FORMID + ' .clsreq').each(function () {
                if ($(this).hasClass("select2")) {
                    var message = $(this).siblings('input:hidden').attr('placeholder');
                    var val = $(this).select2("val");
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).find('.select2-choice').addClass('clsShowWarning');
                        $(this).select2("focus", "val");
                        $('#btnTransSave').attr('disabled', false);
                        $('#Divwaitingtr').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                } else if ($(this).hasClass("select")) {
                    var message = $(this).attr('data-placeholder');
                    var val = $(this).val();
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).addClass('clsShowWarning');
                        $(this).focus();
                        $('#btnTransSave').attr('disabled', false);
                        $('#Divwaitingtr').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
                else {
                    var focus = $(this).attr('data-focus');
                    var message = $(this).attr('placeholder');
                    var val = $(this).hasClass("textarea") ? $(this).text().trim() : $(this).val().trim();

                    if (val == "" || val == 0 || val == null) {
                        validate = false;
                        if (typeof focus === "undefined" || focus === null) {
                            $(this).focus();
                        }
                        $(this).addClass('clsShowWarning');
                        $('#btnTransSave').attr('disabled', false);
                        $('#Divwaitingtr').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
            });

            if (validate) {
                var serializedData = $("#" + FORMID).serialize();
                var url = "/" + Controller + "/" + Action + "/";
                $.post(url, serializedData, function (data) {
                    if (data.RESULTID > 0) {
                        $('#Divwaitingtr').addClass('hide');
                        ret = data.RESULTID

                        //This is for make read only input fields of transaction part
                        var elemArr = $('#' + FORMID)[0].elements;
                        $('#' + FORMID).find('.fa-file-text').removeAttr("onclick");
                        $.each(elemArr, function (n, ele) {
                            var typ = $(ele).attr("type");
                            if ($(this).hasClass('clsreqBGColor')) { $(this).removeClass('clsreqBGColor') }

                            if (typ == "text" || typ == "hidden") {
                                $(this).attr('readonly', true);
                                $(this).removeAttr('onfocus');
                                $(this).removeAttr('onclick');
                                $(this).attr("data-target", "");
                                $("#" + $(this).attr("id")).select2("readonly", true);
                                if ($(this).hasClass('date-picker')) {
                                    $(this).off();
                                }
                            }
                            else if (typ == "radio" || typ == "checkbox") {
                                $(this).attr("disabled", true);
                            }
                            else if ($(ele).is("button")) {
                                $(this).attr("disabled", true);
                                $(this).removeAttr("onclick");
                            }
                            else if ($(ele).is("textarea")) {
                                $(this).attr("readonly", true);
                            }
                        })


                        // Load Transaction Grid because transaction part updated 

                        $.c9bindTransGrid(MID, MENUID, $('#FLAG').val());

                        //This is for common Setting

                        if (ISCOMMSETTING) {

                            var ISCSAPPL = $('#ISCSAPPL').val() == 0 ? false : true;

                            if (ISCSAPPL) {
                                if (typeof ITEMID === "undefined" || ITEMID === null) {
                                    ITEMID = 0;
                                }
                                if (typeof ITEMCODE === "undefined" || ITEMCODE === null) {
                                    ITEMCODE = "";
                                }
                                if (typeof ITEMAMOUNT === "undefined" || ITEMAMOUNT === null) {
                                    ITEMAMOUNT = 0;
                                }
                                if (typeof TYPEID === "undefined" || TYPEID === null) {
                                    TYPEID = 1;
                                }
                                if (typeof HSNID === "undefined" || HSNID === null) {
                                    HSNID = 0;
                                }
                                if (typeof SLID === "undefined" || SLID === null) {
                                    SLID = 0;
                                }

                                var url = '/CommonSetting/CommonSetting';
                                var data = { FLAG: FLAG, MID: MID, TID: data.RESULTID, DOCID: DOCID, ITEMID: ITEMID, ITEMAMOUNT: ITEMAMOUNT, TYPE: TYPEID, HSNID: HSNID, ACCOUNTID: SLID, DieAmt: DIEAMT, ToolAmt: TOOLAMT }; //, ISFREEISSUE: ISFREEISSUE, ISPROCESS: ISPROCESS, ISLINEWISE: ISLINEWISE

                                $('#loadCommonSetting').load(url, data, function (data) {
                                    $('.clsTransActions').addClass("hide");
                                   // $('#lblSettingHead').html('<span>' + ITEMCODE + '</span>     <span>Item Amount</span>  : <span>' + ITEMAMOUNT + '</span>');

                                });
                            } else {
                                $('#Divwaitingtr').addClass('hide');
                            }

                        } else {

                            $('#Divwaitingtr').addClass('hide');
                            fn(data.RESULTID);
                        }

                    } else {
                        $('#Divwaitingtr').addClass('hide');
                        $('#btnTransSave').attr('disabled', false);
                        $.c9Message(data.MESSAGE, data.TITLE, data.TYPE);
                    }
                });
            }
        }

    }


    //New Function for saving transaction details in transaction
    //$.c9SaveTransaction = function SaveTransaction(FORMID, Controller, Action, FLAG, MID, MENUID, DOCID, ISCOMMSETTING, ITEMID, ITEMCODE, ITEMAMOUNT, TYPEID, HSNID, SLID, PROJID, DIEAMT, TOOLAMT,ISPAGERENDER,fn) {
    $.c9SaveTransaction = function SaveTransaction(FORMID, Controller, Action, FLAG, MID, MENUID, DOCID, ISCOMMSETTING, ITEMID, ITEMCODE, ITEMAMOUNT, TYPEID, HSNID, SLID, DIEAMT, TOOLAMT, ISPAGERENDER, fn,DISCPERC,DISCAMT) {
        //alert('c9SaveTransaction');
        //debugger;
       
        var disc_perc = DISCPERC == undefined ? 0 : DISCPERC;
        var disc_amt = DISCAMT == undefined ? 0 : DISCAMT;
        //loading for Trans Save button
         var TransSaveBtnID = "btnTransSave";
        //flag Setting show/hide Trans Save button loading
        var TransLoadingBtn = 0;
        //GET ID for Trans Save button
        $('#' + FORMID).each(function () {
            var btnObj = $(this).find('button').filter('.btn-info');
           //debugger;
            if (btnObj.length >1)
            {
                $(btnObj).each(function () {

                    if ($(this).find('i').hasClass("fa-check")) {
                        //BtnID = $(this).attr('id');
                        TransSaveBtnID = "";
                        TransSaveBtnID = $(this).attr('id');
                    }
                });
            
            }else
            {
                if ($(btnObj).find('i').hasClass("fa-check")) {
                    //BtnID = $(this).attr('id');
                    TransSaveBtnID = "";
                    TransSaveBtnID = $(btnObj).attr('id');
                }
            }
        });
        //console.log(TransSaveBtnID);
       
        if (ISPAGERENDER == undefined) {
            ISPAGERENDER = 0
        }
        if (ISPAGERENDER == 0)
        {
            //alert('ISPAGERENDER=0');
            //debugger;
            $('#btnTransSave').attr('disabled', true);
            $('#DivwaitingtrN').removeClass('hide');
            var validate = true;
            $('#' + FORMID + ' .clsreq').each(function () {
                if ($(this).hasClass("select2")) {
                    var message = $(this).siblings('input:hidden').attr('placeholder');
                    var val = $(this).select2("val");
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).find('.select2-choice').addClass('clsShowWarning');
                        $(this).select2("focus", "val");
                        $('#btnTransSave').attr('disabled', false);
                        $('#DivwaitingtrN').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                } else if ($(this).hasClass("select")) {
                    var message = $(this).attr('data-placeholder');
                    var val = $(this).val();
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).addClass('clsShowWarning');
                        $(this).focus();
                        $('#btnTransSave').attr('disabled', false);
                        $('#DivwaitingtrN').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
                else {
                    var focus = $(this).attr('data-focus');
                    //var Tmsg = $(this).attr('data-placeholder');
                    //var Tmmsa = $(this).attr('placeholder');
                    //var TVal = $(this).val();
                    var message = $(this).attr('placeholder') == undefined || $(this).attr('placeholder')==null ? $(this).attr('data-placeholder') : $(this).attr('placeholder');
                    var val = $(this).hasClass("textarea") ? $(this).text().trim() : $(this).val() == "" || $(this).val()==null ? $(this).val() : $(this).val().trim();
                    if (val == "" || val == 0 || val == null) {
                        validate = false;
                        if (typeof focus === "undefined" || focus === null) {
                            $(this).focus();
                        }
                        $(this).addClass('clsShowWarning');
                        $('#btnTransSave').attr('disabled', false);
                        $('#DivwaitingtrN').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
            });

            if (validate) {
                //TransSaveBtnID
                //for submit button loading disabled true
                //$("#btnTransSave").attr("disabled", true);
                $('#'+TransSaveBtnID).attr("disabled", true);
                //for button on start loading 
                //$('#btnTransSave').addClass("buttonload");
                $('#' + TransSaveBtnID).addClass("buttonload");
                //$('#btnTransSave i').removeClass("fa-check").addClass("fa-spinner fa-spin");
                $('#'+TransSaveBtnID+' i').removeClass("fa-check").addClass("fa-spinner fa-spin");
               //set value 
                TransLoadingBtn = 1;

                var serializedData = $("#" + FORMID).serialize();
                var url = "/" + Controller + "/" + Action + "/";
                $.post(url, serializedData, function (data) {
                    if (data.RESULTID > 0) {
                        ret = data.RESULTID;
                        //hidden/show div footer panel
                        //$('#panel_3').click();
                     
                        $('#btnTransLock').prop("disabled", false);
                        $('#btnTransCancel').prop("disabled", false);
                        $("#FooterDetails").html("");
                        $('#footerbtndiv').addClass('hide');
                        if (FLAG == "E")
                        { $("#collapseThree").collapse('hide'); }
                        

                        // setTimeout(function () { $('#LotTab').empty(); $('#divLot').addClass("hide"); }, 500);

                        //Added by Supriya Kokate on 17 APR 2019 to clear Trans lookup Search Text on Add Mode
                        if (FLAG == "I" && $('#SearchTrLookup_' + MENUID).length != 0) {
                            $('#SearchTrLookup_' + MENUID).val("");
                        }

                        $.c9bindTransGrid(MID, MENUID, $('#FLAG').val());
                         
                        if (ISCOMMSETTING)
                        {
                            var ISCSAPPL = $('#ISCSAPPL').val() == 0 ? false : true;
                            if (ISCSAPPL)
                            {
                                if (typeof ITEMID === "undefined" || ITEMID === null) {
                                    ITEMID = 0;
                                }
                                if (typeof ITEMCODE === "undefined" || ITEMCODE === null) {
                                    ITEMCODE = "";
                                }
                                if (typeof ITEMAMOUNT === "undefined" || ITEMAMOUNT === null) {
                                    ITEMAMOUNT = 0;
                                }
                                if (typeof TYPEID === "undefined" || TYPEID === null) {
                                    TYPEID = 1;
                                }
                                if (typeof HSNID === "undefined" || HSNID === null) {
                                    HSNID = 0;
                                }
                                if (typeof SLID === "undefined" || SLID === null) {
                                    SLID = 0;
                                }
                             
                                var elemArr = $('#' + FORMID)[0].elements;
                                $('#' + FORMID).find('.fa-file-text').removeAttr("onclick");
                                $.each(elemArr, function (n, ele) {
                                    var typ = $(ele).attr("type");
                                    if ($(this).hasClass('clsreqBGColor')) { $(this).removeClass('clsreqBGColor') }

                                    if (typ == "text" || typ == "hidden") {
                                        $(this).attr('readonly', true);
                                        $(this).removeAttr('onfocus');
                                        $(this).removeAttr('onkeyup');
                                        $(this).removeAttr('onkeypress');
                                        $(this).removeAttr('onblur');
                                        $(this).removeAttr('onclick');
                                        $(this).attr("data-target", "");
                                        $("#" + $(this).attr("id")).select2("readonly", true);
                                        if ($(this).hasClass('date-picker')) {
                                            $(this).off();
                                        }
                                    }
                                    else if (typ == "radio" || typ == "checkbox") {
                                        $(this).attr("disabled", true);
                                    }
                                    else if ($(ele).is("button")) {
                                        $(this).attr("disabled", true);
                                        $(this).removeAttr("onclick");
                                    }
                                    else if ($(ele).is("textarea")) {
                                        $(this).attr("readonly", true);
                                    }
                                });
                              
                                var url = '/CommonSetting/CommonSetting';                                
                                var data = { FLAG: FLAG, MID: MID, TID: data.RESULTID, DOCID: DOCID, ITEMID: ITEMID, ITEMAMOUNT: ITEMAMOUNT, TYPE: TYPEID, HSNID: HSNID, ACCOUNTID: SLID, DieAmt: DIEAMT, ToolAmt: TOOLAMT, ISHEADSHOW: false,DISCPERC:disc_perc,DISCAMT:disc_amt }; //, ISFREEISSUE: ISFREEISSUE, ISPROCESS: ISPROCESS, ISLINEWISE: ISLINEWISE                              
                                TransLoadingBtn = 0;
                                
                                $.get(url, data, function (data) {
                                    $('#TrasBtnStrip').addClass('hide');
                                    $('#divsetting').html(data);
                                    document.getElementById("divsetting").scrollIntoView({ behavior: 'smooth' });
                                    $('#DivwaitingtrN').addClass('hide');
                                    //for submit button loading remove
                                    //// $('#btnTransSave').attr("disabled", false);
                                   
                                    //$('#btnTransSave').removeClass("buttonload");
                                    $('#'+TransSaveBtnID).removeClass("buttonload");
                                    //$('#btnTransSave i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                                    $('#'+TransSaveBtnID+' i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                                    TransLoadingBtn = 0;
                                });
                                //$('#divsetting').load(url, data, function (data) {
                                //    document.getElementById("divsetting").scrollIntoView({ behavior: 'smooth' });
                                //    $('#DivwaitingtrN').addClass('hide');
                                //    //$('#btnCancel').click();
                                //    //$('#btnSetting').click();
                                //    //$('#SettingHead').html('<span>' + ITEMCODE + '</span>     <span>Item Amount</span>  : <span>' + ITEMAMOUNT + '</span>');
                                   
                                //});
                            } else {
                                $('#btnCancel').click();
                                $('#DivwaitingtrN').addClass('hide');
                            }

                        } else {
                            $('#btnCancel').click();
                            $('#DivwaitingtrN').addClass('hide');
                            //$.c9Message(data.MESSAGE, data.TITLE, data.TYPE);    //Display Messages of Item Transe
                            if (MENUID == 153) // used in ProductFlow Trans save
                            {
                                fn(data.RESULTID);
                            }
                            
                        }

                    } else {
                        $('#DivwaitingtrN').addClass('hide');
                        $('#btnTransSave').attr('disabled', false);
                        $.c9Message(data.MESSAGE, data.TITLE, data.TYPE);
                    }
                    if (TransLoadingBtn == 1)
                    {
                        //TransSaveBtnID
                        //for submit button loading remove
                        //// $('#btnTransSave').attr("disabled", false);
                        //$('#btnTransSave').removeClass("buttonload");
                        $('#' + TransSaveBtnID).removeClass("buttonload");
                        //$('#btnTransSave i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                        $('#' + TransSaveBtnID + ' i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                    }
                   
                });
            }
        }
        else {
            //alert('ISPAGERENDER==1');
            //this code is for render on partial view
            $('#DivwaitingtrN').removeClass('hide');
            var validate = true;
            $('#' + FORMID + ' .clsreq').each(function () {
                if ($(this).hasClass("select2")) {
                    var message = $(this).siblings('input:hidden').attr('placeholder');
                    var val = $(this).select2("val");
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).find('.select2-choice').addClass('clsShowWarning');
                        $(this).select2("focus", "val");
                        $('#btnTransSave').attr('disabled', false);
                        $('#DivwaitingtrN').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                } else if ($(this).hasClass("select")) {
                    var message = $(this).attr('data-placeholder');
                    var val = $(this).val();
                    if (val == "" || val == null || val == 0) {
                        validate = false;
                        $(this).addClass('clsShowWarning');
                        $(this).focus();
                        $('#btnTransSave').attr('disabled', false);
                        $('#DivwaitingtrN').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
                else {
                    var focus = $(this).attr('data-focus');
                    var message = $(this).attr('placeholder');
                    var val = $(this).hasClass("textarea") ? $(this).text().trim() : $(this).val().trim();

                    if (val == "" || val == 0 || val == null) {
                        validate = false;
                        if (typeof focus === "undefined" || focus === null) {
                            $(this).focus();
                        }
                        $(this).addClass('clsShowWarning');
                        $('#btnTransSave').attr('disabled', false);
                        $('#DivwaitingtrN').addClass('hide');
                        $.c9Message(message, "Warning", "W");
                        return false;
                    }
                }
            });

            if (validate) {
                //for submit button loading disabled true
                //$("#btnTransSave").attr("disabled", true);
                $('#' + TransSaveBtnID).attr("disabled", true);
                //for button on start loading 
                //$('#btnTransSave').addClass("buttonload");
                $('#' + TransSaveBtnID).addClass("buttonload");
                //$('#btnTransSave i').removeClass("fa-check").addClass("fa-spinner fa-spin");
                $('#' + TransSaveBtnID + ' i').removeClass("fa-check").addClass("fa-spinner fa-spin");
                TransLoadingBtn = 1;

                var serializedData = $("#" + FORMID).serialize();
                var url = "/" + Controller + "/" + Action + "/";
                $.post(url, serializedData, function (data) {
                    if (data.RESULTID > 0)
                    {
                        $('#DivwaitingtrN').addClass('hide');
                        ret = data.RESULTID

                        //This is for make read only input fields of transaction part
                        var elemArr = $('#' + FORMID)[0].elements;
                        $('#' + FORMID).find('.fa-file-text').removeAttr("onclick");
                        $.each(elemArr, function (n, ele) {
                            var typ = $(ele).attr("type");
                            if ($(this).hasClass('clsreqBGColor')) { $(this).removeClass('clsreqBGColor') }

                            if (typ == "text" || typ == "hidden") {
                                $(this).attr('readonly', true);
                                $(this).removeAttr('onfocus');
                                $(this).removeAttr('onclick');
                                $(this).attr("data-target", "");
                                $("#" + $(this).attr("id")).select2("readonly", true);
                                if ($(this).hasClass('date-picker')) {
                                    $(this).off();
                                }
                            }
                            else if (typ == "radio" || typ == "checkbox") {
                                $(this).attr("disabled", true);
                            }
                            else if ($(ele).is("button")) {
                                $(this).attr("disabled", true);
                                $(this).removeAttr("onclick");
                            }
                            else if ($(ele).is("textarea")) {
                                $(this).attr("readonly", true);
                            }
                        });

                        // Load Transaction Grid because transaction part updated 
                        $.c9bindTransGrid(MID, MENUID, $('#FLAG').val());

                        //This is for common Setting
                            if (ISCOMMSETTING)
                            {
                                var ISCSAPPL = $('#ISCSAPPL').val() == 0 ? false : true;
                                if (ISCSAPPL) {
                                    if (typeof ITEMID === "undefined" || ITEMID === null) {
                                        ITEMID = 0;
                                    }
                                    if (typeof ITEMCODE === "undefined" || ITEMCODE === null) {
                                        ITEMCODE = "";
                                    }
                                    if (typeof ITEMAMOUNT === "undefined" || ITEMAMOUNT === null) {
                                        ITEMAMOUNT = 0;
                                    }
                                    if (typeof TYPEID === "undefined" || TYPEID === null) {
                                        TYPEID = 1;
                                    }
                                    if (typeof HSNID === "undefined" || HSNID === null) {
                                        HSNID = 0;
                                    }
                                    if (typeof SLID === "undefined" || SLID === null) {
                                        SLID = 0;
                                    }
                                    //if (typeof PROJID === "undefined" || PROJID === null) {
                                    //    PROJID = 0;
                                    //}
                                    var url = '/CommonSetting/CommonSetting';
                                    //var data = { FLAG: FLAG, MID: MID, TID: data.RESULTID, DOCID: DOCID, ITEMID: ITEMID, ITEMAMOUNT: ITEMAMOUNT, TYPE: TYPEID, HSNID: HSNID, ACCOUNTID: SLID, DieAmt: DIEAMT, ToolAmt: TOOLAMT, PROJID: PROJID }; //, ISFREEISSUE: ISFREEISSUE, ISPROCESS: ISPROCESS, ISLINEWISE: ISLINEWISE
                                    var data = { FLAG: FLAG, MID: MID, TID: data.RESULTID, DOCID: DOCID, ITEMID: ITEMID, ITEMAMOUNT: ITEMAMOUNT, TYPE: TYPEID, HSNID: HSNID, ACCOUNTID: SLID, DieAmt: DIEAMT, ToolAmt: TOOLAMT, DISCPERC: disc_perc, DISCAMT: disc_amt }; //, ISFREEISSUE: ISFREEISSUE, ISPROCESS: ISPROCESS, ISLINEWISE: ISLINEWISE
                                    TransLoadingBtn = 0;
                                    $('#loadCommonSetting').load(url, data, function (data) {
                                        $('.clsTransActions').addClass("hide");
                                        // $('#lblSettingHead').html('<span>' + ITEMCODE + '</span>     <span>Item Amount</span>  : <span>' + ITEMAMOUNT + '</span>');
                                        //for submit button loading remove
                                        //// $('#btnTransSave').attr("disabled", false);
                                        //$('#btnTransSave').removeClass("buttonload");
                                        $('#' + TransSaveBtnID).removeClass("buttonload");
                                        //$('#btnTransSave i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                                        $('#' + TransSaveBtnID + ' i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                                        TransLoadingBtn = 0;

                                    });
                                } else {
                                    $('#DivwaitingtrN').addClass('hide');
                                }

                            } else {

                                $('#DivwaitingtrN').addClass('hide');
                                fn(data.RESULTID);    // not used 
                            }

                    } else {
                        $('#DivwaitingtrN').addClass('hide');
                        $('#btnTransSave').attr('disabled', false);
                        $.c9Message(data.MESSAGE, data.TITLE, data.TYPE);
                    }

                    if (TransLoadingBtn == 1)
                    {
                        //for submit button loading remove
                        //// $('#btnTransSave').attr("disabled", false);
                        //$('#btnTransSave').removeClass("buttonload");
                        $('#' + TransSaveBtnID).removeClass("buttonload");
                        //$('#btnTransSave i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                        $('#' + TransSaveBtnID + ' i').removeClass("fa-spinner fa-spin").addClass("fa-check");
                    }

                });
            }
        }


        //try {
        //    json = JSON.parse(input)
        //} catch (e) {
        //    // invalid json input, set to null
        //    //json = null
        //}

    }


    //Function for saving line wise tax for transaction
    $.c9GetSaveLineTaxN = function SaveLineTax(MID, MENUID)
    {
      
        var validate = true;
        $('#Divwaitingtr').removeClass('hide');
        var trval = $('#taxtableLineWise tr.clstaxLW').length;

        if (trval > 0) {
            $('#taxtableLineWise tbody tr .clsLWITEMTAXFIELD').each(function () {
                var rowid = $(this).attr("data-index");
                var TaxField = $("#LWDESC_" + rowid).val();
                var tabid = $(this).attr("data-tab")
                if ($("#LWISHIDDEN_" + rowid).val() == 0 && $(this).val() > 0 && $("#LWFCAMT_" + rowid).val() != 0 && ($("#LWPOSTACCID_" + rowid).val() == 0)) {
                    $("#LWPOSTACCID_" + rowid).select2('focus');
                    $.c9Message(TaxField + ' account should not be blank', 'Warning', 'W');
                    $('#Footer_' + tabid).click();
                    $('#btnAddItemTax').attr('disabled', false);
                    $('#Divwaitingtr').addClass('hide');
                    validate = false;
                    return false;
                }
            });

            if (validate) {

                var URL = '/CommonSetting/LineWiseTax/';
                var serializedData = $("#frmTaxLineWise").serialize();
                $.post(URL, serializedData, function (data) {
                    $('#Divwaitingtr').addClass('hide');
                    if (data.RESULTID > 0) {
                        $("#divTransModal").modal('hide');
                        $("Html,body").removeClass("modal-open");
                        $("#divSettingModal").modal('hide');
                        if (MENUID==136) {
                            $('#btnTransLock').prop("disabled", false);
                            $('#btnTransCancel').prop("disabled", false);
                            $("#FooterDetails").html("");
                            $('#footerbtndiv').addClass('hide');
                        }
                        $.c9bindTransGrid(MID, $('#MENUID').val(), "I");
                    }
                    else {
                        $.c9Message(data.MESSAGE, data.TITLE, data.TYPE);
                    }
                });
            }

        }
        else {
            $('#Divwaitingtr').addClass('hide');
            $.c9Message('Please define tax for this item!', 'Warning', 'W');
        }

    }





    //Function to get Ornanisation structure tree
    $.c9OrgHelp = function OrgHelp(e, FIELDTYPEID) {
        var url = '/OrganisationStructure/LookUpList/';
        var data = { FIELDTYPEID: FIELDTYPEID }
        $.get(url, data, function myfunction(data) {
            $('#OrgStrucModalLoad').html(data);
            $("#OrgStrucModalLoad").show("slow");
            $('.clsOrgHelp').addClass("hide");
        });
    }
    //Function To Set selected Org. Structure To TextBox
    $.c9SetOrgStruct = function SetOrgStruct(ControleId, ControleDesc) {
        $('#WBSTreeList tr.selected').each(function () {
            var orgid = $(this).attr("data-tt-id");
            $('#' + ControleDesc).val("");
            $('#' + ControleId).val(orgid);
            var text = $("#TITLE_" + orgid).text();
            $('#' + ControleDesc).val(text);
            $.c9OrgClose();
        });
    }
    //Function for closing organisation structure model popup
    $.c9OrgClose = function OrgClose() {
        $("#myOrgStrucModal").modal('hide');
    }
    //function for filling unit of masurement dropdown
    $.c9FillUOM = function FillUOM(UomID, selectedVal, itemid, orgstrucid, UOMTYPEID) {

        
        $.getJSON('/CommonLibrary/_FillUOMTRANS', { FLAG: 2, UOMID: selectedVal, ITEMID: itemid, ITEMSUBGRPI: 0, DESC: "", ORGSTRUCID: orgstrucid, UOMTYPEID: UOMTYPEID == undefined ? 0 : UOMTYPEID }, function (result) {
            var ddl = $('#' + UomID);
            ddl.empty();

            $(result).each(function () {
                $(document.createElement('option'))
                    .attr('value', this.Value)
                    .text(this.Text)
                    .attr('data-formula', this.FORMULA == 1 ? true : false)
                    .appendTo(ddl);
            });

            if (selectedVal == 0) {
                ddl.prop("selectedIndex", 0);
                var element = ddl.find('option:selected');
                var formula = element.attr("data-formula");

            } else {
                ddl.val(selectedVal)
            }
        });
    }
    //function for finishing document and laoding new document
    $.c9FinishNew = function FinishNew(Controller, Action, MID, MENUID, DOCID) {
       
        $('#btnFinishNew').attr('disabled', true);
        $('#btnFinishClose').attr('disabled', true);
        $('#btnTransDiscard').attr('disabled', true);
        $('#btnTransCancel').attr('disabled', true);
        var trcount = $('#tblTransData tr').length;

        //clswaitingMaster specific loading spinner
        var LoadingID = "#Divwaitingtr";
        var ldObj = $('.clswaitingMaster');
        if (ldObj.length > 0) {
            //$('.clswaitingMaster .center i').css("margin-top", "52%"); style="position: fixed;top: 50%;"
            $('.clswaitingMaster .center i').css("position", "fixed");
            //$('.clswaitingMaster .center i').css("top", "50%");
            $('.clswaitingMaster .center i').css("margin-top", "10%");
            $('.clswaitingMaster').removeClass('hide');
            LoadingID = ".clswaitingMaster";

        } else {
            $('#Divwaitingtr').removeClass('hide');
            LoadingID = "#Divwaitingtr";
        }
        ////alert('c9FinishNew');
        ////debugger;

        if (trcount >= 2) {
            var FLAG = $('#FLAG').val().trim() == "I" ? 1 : 2;

            var url = '/' + Controller + '/FinalUpdate/';

            var data = { FLAG: FLAG, MID: MID, MENUID: MENUID, DOCID: DOCID }
            $.get(url, data, function (err) {
                var errid = err.RESULTID;
                var errmsg = err.MESSAGE;
                var TITLE = err.TITLE;
                var TYPE = err.TYPE;
                $(LoadingID).addClass('hide');
                if (errid > 0) {
                    var url = '/' + Controller + '/' + Action + '/' + MENUID + '/' + DOCID;
                    window.location.href = url;
                } else {
                    $('#btnFinishNew').attr('disabled', false);
                    $('#btnFinishClose').attr('disabled', false);
                    $('#btnTransDiscard').attr('disabled', false);
                    $('#btnTransCancel').attr('disabled', false);
                    $.c9Message(errmsg, TITLE, TYPE);
                }
            });
        } else {
            $('#btnFinishNew').attr('disabled', false);
            $('#btnFinishClose').attr('disabled', false);
            $('#btnTransDiscard').attr('disabled', false);
            $('#btnTransCancel').attr('disabled', false);
            $(LoadingID).addClass('hide');
            $.c9Message('Please fill atleast one transaction.', 'Warning', 'W');
        }
    }
    //Function for finishing document and getting user to lookup
    $.c9FinishClose = function FinishClose(Controller, MID, MENUID, DOCID) {
        $('#btnFinishNew').attr('disabled', true);
        $('#btnFinishClose').attr('disabled', true);
        $('#btnTransDiscard').attr('disabled', true);
        $('#btnTransCancel').attr('disabled', true);
        var trcount = $('#tblTransData tr').length;

        //clswaitingMaster specific loading spinner
        var LoadingID = "#Divwaitingtr";
        var ldObj = $('.clswaitingMaster');
        if (ldObj.length > 0) {
            //$('.clswaitingMaster .center i').css("margin-top", "52%"); style="position: fixed;top: 50%;"
            $('.clswaitingMaster .center i').css("position", "fixed");
            //$('.clswaitingMaster .center i').css("top", "50%");
            $('.clswaitingMaster .center i').css("margin-top", "10%");
            $('.clswaitingMaster').removeClass('hide');

            LoadingID = ".clswaitingMaster";

        } else {
            $('#Divwaitingtr').removeClass('hide');
            LoadingID = "#Divwaitingtr";
        }
        ////alert('c9FinishClose');
        ////debugger;

        if (trcount >= 2) {
            var FLAG = $('#FLAG').val().trim() == "I" ? 1 : 2;

            var url = '/' + Controller + '/FinalUpdate/';

            var data = { FLAG: FLAG, MID: MID, MENUID: MENUID, DOCID: DOCID }
            $.get(url, data, function (err) {
                var errid = err.RESULTID;
                var errmsg = err.MESSAGE;
                var TITLE = err.TITLE;
                var TYPE = err.TYPE;
                $(LoadingID).addClass('hide');
                if (errid > 0) {
                    var lkUrl = $("#hdnLookupUrl_" + MENUID).val();//changed by supriya kokate on 14 may 2019 to get Lookup URL for menuXDoc
                    var url = lkUrl + MENUID + '/' + DOCID;
                    window.location.href = url;
                } else {
                    $('#btnFinishNew').attr('disabled', false);
                    $('#btnFinishClose').attr('disabled', false);
                    $('#btnTransDiscard').attr('disabled', false);
                    $('#btnTransCancel').attr('disabled', false);
                    $.c9Message(errmsg, TITLE, TYPE);
                }
            });
        } else {
            $('#btnFinishNew').attr('disabled', false);
            $('#btnFinishClose').attr('disabled', false);
            $('#btnTransDiscard').attr('disabled', false);
            $('#btnTransCancel').attr('disabled', false);
            $(LoadingID).addClass('hide');
            $.c9Message('Please fill atleast one transaction.', 'Warning', 'W');
        }

    }
    //Function for saving line wise tax for transaction
    $.c9GetSaveLineTax = function SaveLineTax(MID) {       
        var validate = true;
        $('#Divwaitingtr').removeClass('hide');
        var trval = $('#taxtableLineWise tr.clstaxLW').length;

        if (trval > 0) {
            $('#taxtableLineWise tbody tr .clsLWITEMTAXFIELD').each(function () {
                var rowid = $(this).attr("data-index");
                var TaxField = $("#LWDESC_" + rowid).val();
                var tabid = $(this).attr("data-tab")
                if ($("#LWISHIDDEN_" + rowid).val() == 0 && $(this).val() > 0 && $("#LWFCAMT_" + rowid).val() != 0 && ($("#LWPOSTACCID_" + rowid).val() == 0)) {
                    $("#LWPOSTACCID_" + rowid).select2('focus');
                    $.c9Message(TaxField + ' account should not be blank', 'Warning', 'W');
                    $('#Footer_' + tabid).click();
                    $('#btnAddItemTax').attr('disabled', false);
                    $('#Divwaitingtr').addClass('hide');
                    validate = false;
                    return false;
                }
            });

            if (validate) {
              
                var URL = '/CommonSetting/LineWiseTax/';
                var serializedData = $("#frmTaxLineWise").serialize();
                $.post(URL, serializedData, function (data) {
                    $('#Divwaitingtr').addClass('hide');
                    if (data.RESULTID > 0) {
                        $("#divSettingModal").modal('hide');

                        $("Html,body").removeClass("modal-open");
                        $.c9bindTransGrid(MID, $('#MENUID').val(), "I");
                    }
                    else {
                        $.c9Message(data.MESSAGE, data.TITLE, data.TYPE);
                    }
                });
            }

        }
        else {
            $('#Divwaitingtr').addClass('hide');
            $.c9Message('Please define tax for this item!', 'Warning', 'W');
        }

    }
    //Function for loading trans data for view 
    $.c9GetTransView = function GetTransView(MID, MENUID) {
        $.c9bindTransGrid(MID, MENUID, "V");
    }
    //fnction for loading foorter data inview 
    $.c9GetFooterView = function GetFooterView(MENUID, DOCID, MID, DocAmt) {
        $('#Divwaitingtr').removeClass('hide');

        $.get("/FooterCommon/Footer/", { FLAG: $("#FLAG").val(), MENUID: MENUID, DOCID: DOCID, MID: MID, DOCAMT: DocAmt }, function (data) {
            $("#FooterModalLoad").html(data);
            $("#FooterModalLoad").find('.btn').attr('disabled', true);
            $('.clsdisabledatepickerbtn').prop('disabled', true);
            $('#FooterModalLoad input').each(function () {
                var type = $(this).attr('type');

                if (type != 'checkbox') {
                    $(this).attr('readonly', true);
                    $(this).removeAttr('onblur');
                    if ($(this).hasClass('select2-offscreen')) {
                        $("#" + $(this).attr("id")).select2("readonly", true);
                    } else if ($(this).hasClass('date-picker')) {
                        $(this).off();
                    } else if ($(this).hasClass('time-picker')) {
                        $(this).off();
                    } else if ($(this).attr('id') == 'CURRENCYDESC') {
                        $(this).removeAttr('onfocus');
                    } else if ($(this).hasClass('btnCalcOn')) {
                        $(this).addClass('hide');
                    }
                } else {
                    $(this).attr('disabled', true);
                }
            });
            $('#FooterModalLoad textarea').each(function () {
                $(this).attr('readonly', true);
            });
            $("#FooterModalLoad #Footerpytrm .clsIsData").each(function () {
                if ($(this).is(":checked") == false) {
                    $(this).closest("tr").hide();
                }
            })
            $('#Divwaitingtr').addClass('hide');
        });
    }
    //function To clear temp table details in case of incomplete transaction
    $.c9TransCommonRefresh = function RefreshTransTempDetails(PopupID, UID) {
        if (typeof (UID) == "undefined") {
            UID = "";
        }
        $.post("/TransCommon/DeleteTransTempDetails?UID="+UID, function (res) {
            if (res.RESULTID > 0) {
                $("#"+PopupID).modal('hide');
            }
        });
    }
    //$.c9StandardNarr = function isStandardNarr(evt, MODE, MENUID, ControleId) {

    //    var charCode = (evt.which) ? evt.which : evt.keyCode
    //    if (charCode === 113 || charCode === 0) {
    //        $('#Divwaitingtr').removeClass('hide');
    //        var URL = "/TransCommon/HELPFORSTDNARRSINGLE/";
    //        var data = { MENUID: MENUID, Mode: MODE, ControleId: ControleId };
    //        $.get(URL, data, function (data) {
    //            $('#LargeModalLoadSNTR').html(data);
    //            $('#AddStandard_boxTR').modal('show');
    //        });
    //        $('#Divwaitingtr').addClass('hide');
    //    }
    //}
    $.c9StandardNarr = function isStandardNarr(evt, MODE, MENUID, ControleId) {
        $('#Divwaitingtr').removeClass('hide');
        var URL = "/TransCommon/HELPFORSTDNARRSINGLE/";
        var data = { MENUID: MENUID, Mode: MODE, ControleId: ControleId };
        $.get(URL, data, function (data) {
            $('#LargeModalLoadSNTR').html(data);
            $('#AddStandard_boxTR').modal('show');
        });
        $('#Divwaitingtr').addClass('hide');
    }

    //Function to Load Analysis/Periodic Details
    $.c9CommonDetails = function ShowCommonDetails(ISCOSTCNTR, ISPERODIC) {
        if (ISCOSTCNTR == "true" || ISCOSTCNTR == true || ISCOSTCNTR == "True" || ISPERODIC == "True" || ISPERODIC == true || ISPERODIC == "True") {

            $('#Divwaitingtr').removeClass('hide');
            var url = "/TransCommon/CommonTrans/";
            var data = { COSTCNT: ISCOSTCNTR, PERIODIC: ISPERODIC };
            $.get(url, data, function (data) {
                $("#divCommonDtls").removeClass("hide");
                $("#divCommonDtls").html("");
                $("#divCommonDtls").html(data);
                $('#Divwaitingtr').addClass('hide');
            });
        }
        else {
            $("#divCommonDtls").addClass("hide");

        }
    }
    //this function Scrolling Down for Footer Details
    $.c9ScrollingDown = function ScrollingDownPostion()
    {
        //var WscrollTop = $(window).scrollTop();
        //var DH = $(document).height();
        ////alert('DH' + DH);
        //var WH = $(window).height();
        ////alert('WH' + WH);
        //var diff = (DH - WH);
        ////alert('diff' + diff);
        //if (diff == 0)
        //{
        //    var DivHeight = 0;
        //    $('.ClsFooter').each(function () { DivHeight += this.scrollHeight; });
        //    //alert('scrollHeight  ' + '  DivHeight  ' + DivHeight);
        //    console.log('DivHeight  ' + DivHeight);
        //    diff = DivHeight + 50;
        //}
        //$('html, body').animate({ scrollTop: diff }, 700);
        //alert('c9ScrollingDown  ' + '  DH  ' + DH + '  WH  ' + WH + '  diff  ' + diff);
        //alert('diff  ' + diff);

        //debugger;
        var WscrollTop = $(window).scrollTop();
        var DH = $(document).height();
        //alert('DH' + DH);
        var WH = $(window).height();
        //alert('WH' + WH);
        var diff = (DH - WH);
        //alert('diff' + diff);

        var DivHeight = 0;
        $('.ClsFooter').each(function () { DivHeight += this.scrollHeight; });
        //console.log('DivHeight  ' + DivHeight);
        //console.log('diff  ' + diff);
        if (diff == 0)
        {
            DivHeight = DivHeight + 100;
            //console.log('DivHeight  ' + DivHeight);

        } else if (DivHeight == 0) {
            DivHeight = diff + 100;
            //console.log('diff  ' + diff);
        } else {
            if (DivHeight < diff)
                DivHeight = diff;


        }
        
        $('html, body').animate({ scrollTop: DivHeight }, 700);

       
    }
})(jQuery);