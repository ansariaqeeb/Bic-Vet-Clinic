
var $current_user_id = null,
 Connected_users = [],
 Group_Users = [],
 chat_window_count = 2,
 currentmsgid = 0,
 Fromuserid = null;
Fromusername = "";
var userName = $('#chatdisplayname').val();
var Login_user_id = $('#chatuserid').val();

var ServerUser = ServerUser || {
    $current_user: null,
    isOpen: false,
    chat_history: [],
    statuses: {
        online: { class: 'fa fa-circle green user-status is-online', order: 1, label: 'Online' },
        offline: { class: 'is-offline', order: 4, label: 'Offline' },
        idle: { class: 'fa fa-circle orange user-status is-idle', order: 3, label: 'Idle' },
        busy: { class: 'fa fa-circle red user-status is-busy', order: 2, label: 'Busy' }
    }
};

(function ($, window, undefined) {
    "use strict";
    var $chat = $("#app-chat"),
		$chat_inner = $chat.find('.chat-inner'),
		$chat_badge = $chat.find('.chat-badge-count').add($('.chat-notifications-badge')),
		$conversation_window = $(".chat-conversation"),
		$conversation_header = $conversation_window.find(".panel-title"),
		$conversation_body = $('.conversation-body'),
        $User_List = $('.user-list'),
		textarea = $('.chat-text-area textarea'),
		sidebar_default_is_open = !$(".page-container").hasClass('sidebar-collapsed');


    $.extend(ServerUser, {

        init: function () {
            // Implement Unique ID in case it doesn't exists
            if ($.isFunction($.fn.uniqueId) == false) {
                jQuery.fn.extend({
                    uniqueId: (function () {
                        var uuid = 0;

                        return function () {
                            return this.each(function () {
                                if (!this.id) {
                                    this.id = "ui-id-" + (++uuid);
                                }
                            });
                        };
                    })(),
                });
            }

            //function called on user name click from user list
            $chat.on('click', '.user-list', function (ev) {//chat group                
                ev.preventDefault();
                var $chat_user = $(this);
                var usermsgid = $(this).attr('data-usermsgid');
                $current_user_id = $(this).attr('id');
                var grpuser = [];
                grpuser.push({ UserId: $current_user_id, UserName: $chat_user.find('span').find('strong').text() });
                var user_status;
                if ($('#' + $current_user_id).children('span').children('span').hasClass('green')) { user_status = "green"; } else { user_status = ""; }

                //Render chat history 
                chatHub.server.chatHistory(Login_user_id, $current_user_id).done(function (History) {
                    if (History != null && History != '') {
                        $(this).find('.chat-badge-count').addClass('hidden'); // for unread msg
                        $('.hat-badge-count').html(0); // for unread msg count
                        ServerUser.refreshUserIds($current_user_id);
                        $.each(History, function (i, History) {
                            Group_Users.push({ UserId: History.ToUserId, UserName: History.TOUSER });
                            Group_Users.push({ UserId: History.UserId, UserName: History.FROMUSER });
                            ServerUser.open($current_user_id, History.MESSAGEID, Group_Users, "green");
                            $('div[data-chatboxid=' + $current_user_id + ']').attr('data-currentmsgid', $current_user_id);
                            
                            ServerUser.pushMessage($current_user_id, History.Message, History.FROMUSER, History.UserId, History.CREATEDON, Group_Users, true, true, "0", 0, 0)
                            Group_Users = [];
                        });

                    }
                    else {
                        ServerUser.open($current_user_id, $current_user_id, grpuser, user_status);
                    }

                    var $DivHight = 0;
                    $('.conversation-body').each(function () {
                        $DivHight += this.scrollHeight;
                        console.log($DivHight);
                    });
                    $('div').animate({ scrollTop: $DivHight }, 600);

                });
            });


            //funtion on msg textarea keydown 
            $(document).delegate('.chat-text-area textarea', 'keydown click', function (e) {
                if (e.keyCode == 13 && !e.shiftKey) {
                    var msg = $(this).val();

                    if (msg == "") {
                        $.c9Message("Please Enter text to send", 'Warning', "W");
                        return false;
                    }
                    else {
                        var $e = $(this)
                        var selecteduser = $(this).parents('.chat-conversation').attr('data-chatboxid');
                        var CurrentmsgId = $(this).parents('.chat-conversation').attr('data-currentmsgid');
                        var selectedname = $(this).parents('.chat-conversation').find('.username').html();
                        
                        var Current_Group_Users = [];

                        Group_Users.push({ UserId: selecteduser, UserName: selectedname });
                        Group_Users.push({ UserId: Login_user_id, UserName: userName });
                        Current_Group_Users = Group_Users;

                        var myObject = new Object();
                        myObject.MESSAGE = msg;
                        myObject.LOGINUSERID = Login_user_id;
                        myObject.CURRENTMSGID = CurrentmsgId;

                        //myObject.pets.push(selecteduser);
                        //myObject.pets.push(Login_user_id);

                        myObject.USERGRP = [selecteduser, Login_user_id];

                        var USERDETAILS = JSON.stringify(myObject);
               
                        var CHATTINGMODE = 1;
                        
                        //funtion to send msg to selected user 
                        chatHub.server.sendPrivateMessage(USERDETAILS, CHATTINGMODE).done(function (msgid) { //msgid
                            $e.parents('.chat-conversation').attr('data-currentmsgid', msgid);
                            
                            ServerUser.pushMessage(msgid, msg, userName, Login_user_id, new Date(), Current_Group_Users, true, true, "0", 0, 0)
                            Group_Users = [];
                            $("div[data-currentmsgid=" + msgid + "]").find('.conversation-body').each(function () {
                                $(this).animate({ scrollTop: this.scrollHeight }, 600);
                            });
                        });
                        
                        $(this).val('');

                        e.preventDefault();

                        return false;
                    }
                }
                else
                    if (e.keyCode == 27) {
                        ServerUser.close();
                    }
            });


        },

        //funtion to minimize chat window
        minimize: function (e) {
            $(e).parents('.chat-conversation').find('.minbody').toggleClass('minimize');
            $(e).parents('.chat-conversation').find('.mintext').toggleClass('minimize');
        },

        //funtion to open chat window
        open: function (fromuserid, msgid, grpusers, user_status) {
        
            chat_window_count = 2;
            var $Chat_box = $('<div data-chatboxID="' + fromuserid + '"  data-currentmsgid="' + $('.chatdv .chat-conversation').length + '"  style="width: 240px; right:17%;box-shadow: 0px 1px 1px #333; position: fixed; bottom: 0px;z-index: 999; background-color: rgb(255, 255, 255);" class="chatbox chat-conversation ' + msgid + '">' +
                   '<div class="panel-heading bg-primary" style="cursor:pointer"><small class="pull-right"><i> </i></small><div class="panel-title chatbox-name"></div>' +
                   '</div><div class="list-group conversation-body scroll no-margin-bottom minbody"  data-height="180" style="width: 100%; height: 200px;overflow:auto;"><span class="offline-msg"></span></div><div class="panel-footer clearfix mintext">' +
                       '<div class="input-group col-sm-12 chat-text-area no-padding"><textarea class="form-control" placeholder="type your message..." style="resize:none;overflow-y:hidden;height:52px;" cols="27"></textarea>' +
                         '</div></div></div>');
         

            var group_users_name = "";

            if (grpusers.length == 2 || grpusers.length == 1) {
                $.each(grpusers, function (i, el) {

                    if (parseInt(el.UserId) != parseInt(Login_user_id)) {
                        group_users_name = el.UserName;
                    }
                })
                $Chat_box.find('.chatbox-name').html('<span style="border:none;margin-top: -8px;background-color: transparent;" class="fa fa-circle no-margin-left  user-status ' + user_status + '"></span><a class="conversation-close" onclick="ServerUser.close(this)" href="#"><i class="fa fa-close pull-right"></i></a><span data-value="" onclick="ServerUser.minimize(this)" class="username"style="padding-left:10px;" >' + group_users_name + '</span>')
                if (!user_status) { $Chat_box.find('.offline-msg').html(group_users_name + ' is currently offline.').show(); }
            }

            function check_exist() {
                var Flag = false;
                if ($('.chatdv .chat-conversation').length > 0) {
                    $('.chatdv .chat-conversation').each(function () {
                        var a = $(this).attr('data-chatboxID');
                        if (parseInt(a) == parseInt(fromuserid)) {

                            Flag = false;
                            return false;

                        }
                        else {

                            Flag = true;
                        }
                    })
                }
                else {

                    Flag = true;
                }
                return Flag;
            }

            function check_exist1() {
                var Flag = false;
                if ($('.chatdv .chat-conversation').length > 0) {
                    $('.chatdv .chat-conversation').each(function () {

                        var a = $(this).attr('data-currentmsgid');
                        if (parseInt(a) == parseInt(msgid)) {

                            Flag = false;
                            return false;

                        }
                        else {

                            Flag = true;
                        }
                    })
                }
                else {

                    Flag = true;
                }
                return Flag;
            }


            if ($('.chatdv .chat-conversation').length < 4 && check_exist() && check_exist1()) {
                if ($('.chatdv .chat-conversation').length > 0) {
                    $('.chatdv .chat-conversation').each(function () {
                        var right = (16.9 * chat_window_count)  //script to openchat window acc to count of chat
                        $(this).css('right', right + "%");
                        chat_window_count++
                    })
                }
                if ($('.chatdv .chat-conversation').length == 0) {
                    $Chat_box.find('.conversation-body').parents('.chat-conversation').attr('data-currentmsgid', currentmsgid);
                }

                else {
                    var arr = [];
                    $('.chatdv .chat-conversation').each(function () {
                        arr.push(parseInt($(this).attr('data-currentmsgid')));
                    });
                    if (($.inArray(currentmsgid, arr) == -1)) {

                        $Chat_box.find('.conversation-body').parents('.chat-conversation').attr('data-currentmsgid', currentmsgid);
                    }
                }
                $('.chatdv').append($Chat_box)
            }
        },

        //funtion to close chat conversation
        close: function (e) {
            $(e).parents('.chat-conversation').remove();
            currentmsgid = 0;
            var count = 1;
            $('.chatdv .chat-conversation').each(function () {
                var right = (16.9 * count);
                $(this).css('right', right + "%");
                count++
            })
            return false;
        },

        //used in render msg funtion
        updateScrollbars: function (e) {
            e.get(0).scrollTop = e.get(0).scrollHeight;
            //scrollToBottom(e);
        },

        //used in push msg funtion
        refreshUserIds: function (id) {
             if (typeof ServerUser.chat_history[id] == 'undefined') {
                ServerUser.chat_history[id] = {
                    messages: [],
                    group_users: [],
                    unreads: 0,
                    status: status
                };
            }            
        },

        // function to push messages to render 
        pushMessage: function (msgid, msg, from, fromuserid, time, GroupUsers, fromOpponent, unread, IsBoxOpen, IsReceive, fromuseridchatbox) {
            Fromuserid = fromuserid;
            Fromusername = from;
            currentmsgid = msgid;
            if (fromuseridchatbox != '' && IsBoxOpen == "0") {
                ServerUser.open(fromuseridchatbox, msgid, GroupUsers, "green");
                if (GroupUsers.length == 2) {
                    this.puffUnreadsAll(fromuseridchatbox, msgid);
                }
            }
            
            if (msgid) {
                this.refreshUserIds(msgid);
                this.chat_history[msgid].group_users = GroupUsers
                this.chat_history[msgid].messages = [];
                this.chat_history[msgid].messages.push({
                    message: msg,
                    from: from,
                    time: time,
                    attachment: "",
                    filename: "",
                    fromOpponent: fromOpponent,
                    unread: unread
                });
                ServerUser.renderMessages(msgid, fromuserid, IsReceive, fromuseridchatbox)

            } 
        },

        //funtio to render message on both from n To user chat window
        renderMessages: function (id, fromuserid, IsReceive, fromuseridchatbox) {
            if (IsReceive == 1) {
                if (typeof this.chat_history[id] != 'undefined') {
                    //$("div[data-currentmsgid=" + id + "]").find('.conversation-body').html('');
                    for (var i in this.chat_history[id].messages) {
                        var entry = this.chat_history[id].messages[i];
                        var $entry = $('<a class="list-group-item"  href="#"><div class="media-box"><div class="media-box-body clearfix" style="font-size:13px;"><small class="pull-right"></small> <strong class="media-box-heading text-primary user"></strong> <p class="mb-sm"></p></div></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 65%; left: 19%; font-size: 13px;z-index:9;white-space: nowrap;" class="text-center"><i class="time"></i></div></a>')
                        var date = entry.time,
                         date_formated = date;

                        if (typeof date == 'object') {

                            var hour = date.getHours(),
                                hour = (hour < 10 ? "0" : "") + hour,

                                min = date.getMinutes(),
                                min = (min < 10 ? "0" : "") + min,

                                sec = date.getSeconds();
                            sec = (sec < 10 ? "0" : "") + sec;

                            date_formated = hour + ':' + min;
                        }

                        $entry.find('.user').html(entry.from);
                        $entry.find('p').html(entry.message.replace(/\n/g, '<br>'));
                        $entry.find('.time').html(date_formated);
                        if (entry.fromOpponent) {
                            $entry.addClass('odd');
                        }

                        if (entry.unread && typeof slient == 'undefined') {
                            $entry.addClass('unread');
                            entry.unread = false;
                        }
                        $("div[data-chatboxid=" + fromuseridchatbox + "]").find('.conversation-body').append($entry);
                    }
                    //   this.updateScrollbars($("div[data-currentmsgid=" + id + "]").find('.conversation-body'));
                }
            }
            else {
                if (typeof this.chat_history[id] != 'undefined') {
                    //$("div[data-currentmsgid=" + id + "]").find('.conversation-body').html('');
                    for (var i in this.chat_history[id].messages) {
                        var entry = this.chat_history[id].messages[i];
                        var $entry = $('<a class="list-group-item"  href="#"><div class="media-box"><div class="media-box-body clearfix" style="font-size:13px;"><small class="pull-right"></small> <strong class="media-box-heading text-primary user"></strong> <p class="mb-sm"></p></div></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 65%; left: 19%; font-size: 13px;z-index:9;white-space: nowrap;" class="text-center"><i class="time"></i></div></a>')
                        var date = entry.time,
                         date_formated = date;

                        if (typeof date == 'object') {

                            var hour = date.getHours(),
                                hour = (hour < 10 ? "0" : "") + hour,

                                min = date.getMinutes(),
                                min = (min < 10 ? "0" : "") + min,

                                sec = date.getSeconds();
                            sec = (sec < 10 ? "0" : "") + sec;

                            date_formated = hour + ':' + min;
                        }

                        $entry.find('.user').html(entry.from);
                        $entry.find('p').html(entry.message.replace(/\n/g, '<br>'));
                        $entry.find('.time').html(date_formated);
                        if (entry.fromOpponent) {
                            $entry.addClass('odd');
                        }

                        if (entry.unread && typeof slient == 'undefined') {
                            $entry.addClass('unread');
                            entry.unread = false;
                        }
                        $("div[data-currentmsgid=" + id + "]").find('.conversation-body').append($entry);
                    }
                    //   this.updateScrollbars($("div[data-currentmsgid=" + id + "]").find('.conversation-body'));
                }
            }
        },

        //funtion to show unread badge count 
        puffUnreadsAll: function (fromuserid, msgid) {
            $('.chat-icon').removeClass('flash');
            var arr = [];
            $('.chatdv .chat-conversation').each(function () {
                arr.push(parseInt($(this).attr('data-chatboxid')));
            });
            if (($.inArray(parseInt(fromuserid), arr) == -1)) {
                $('#' + fromuserid).attr('data-usermsgid', msgid)
                var value = $('#' + fromuserid).find('.chat-badge-count').html();
                value = (parseInt(value)) + 1;
                $('#' + fromuserid).find('.chat-badge-count').removeClass('hidden').html(value);
                $('.chat-icon').addClass('flash')
                $('.chat-badge-count').html(value);
            }
        },
    });


    // Refresh Ids
    ServerUser.init();


})(jQuery, window);

//resizable

var public_vars = public_vars || {};

jQuery.extend(public_vars, {

    breakpoints: {
        largescreen: [991, -1],
        tabletscreen: [768, 990],
        devicescreen: [420, 767],
        sdevicescreen: [0, 419]
    },

    lastBreakpoint: null
});



// Get current breakpoint
function get_current_breakpoint() {
    var width = jQuery(window).width(),
		breakpoints = public_vars.breakpoints;

    for (var breakpont_label in breakpoints) {
        var bp_arr = breakpoints[breakpont_label],
			min = bp_arr[0],
			max = bp_arr[1];

        if (max == -1)
            max = width;

        if (min <= width && max >= width) {
            return breakpont_label;
        }
    }

    return null;
}


// Check current screen breakpoint
function is(screen_label) {
    return get_current_breakpoint() == screen_label;
}


// Is xs device
function isxs() {
    return is('devicescreen') || is('sdevicescreen');
}

// Is md or xl
function ismdxl() {
    return is('tabletscreen') || is('largescreen');
}


//created by Rajnikant Tiwari
var public_vars = public_vars || {};

; (function ($, window, undefined) {

    "use strict";

    $(document).ready(function () {
        // Sidebar Menu var
        public_vars.$body = $("body");
        public_vars.$pageContainer = public_vars.$body.find(".page-container");
        public_vars.$chat = public_vars.$pageContainer.find('#app-chat');
    });
    var wid = 0;

})(jQuery, window);
function scrollToBottom($el) {

    $el.get(0).scrollTop = $el.get(0).scrollHeight;
}
function disableXOverflow() {
    public_vars.$body.addClass('overflow-x-disabled');
}

function enableXOverflow() {
    public_vars.$body.removeClass('overflow-x-disabled');
}
(function (k) { k.transit = { version: "0.9.9", propertyMap: { marginLeft: "margin", marginRight: "margin", marginBottom: "margin", marginTop: "margin", paddingLeft: "padding", paddingRight: "padding", paddingBottom: "padding", paddingTop: "padding" }, enabled: true, useTransitionEnd: false }; var d = document.createElement("div"); var q = {}; function b(v) { if (v in d.style) { return v } var u = ["Moz", "Webkit", "O", "ms"]; var r = v.charAt(0).toUpperCase() + v.substr(1); if (v in d.style) { return v } for (var t = 0; t < u.length; ++t) { var s = u[t] + r; if (s in d.style) { return s } } } function e() { d.style[q.transform] = ""; d.style[q.transform] = "rotateY(90deg)"; return d.style[q.transform] !== "" } var a = navigator.userAgent.toLowerCase().indexOf("chrome") > -1; q.transition = b("transition"); q.transitionDelay = b("transitionDelay"); q.transform = b("transform"); q.transformOrigin = b("transformOrigin"); q.transform3d = e(); var i = { transition: "transitionEnd", MozTransition: "transitionend", OTransition: "oTransitionEnd", WebkitTransition: "webkitTransitionEnd", msTransition: "MSTransitionEnd" }; var f = q.transitionEnd = i[q.transition] || null; for (var p in q) { if (q.hasOwnProperty(p) && typeof k.support[p] === "undefined") { k.support[p] = q[p] } } d = null; k.cssEase = { _default: "ease", "in": "ease-in", out: "ease-out", "in-out": "ease-in-out", snap: "cubic-bezier(0,1,.5,1)", easeOutCubic: "cubic-bezier(.215,.61,.355,1)", easeInOutCubic: "cubic-bezier(.645,.045,.355,1)", easeInCirc: "cubic-bezier(.6,.04,.98,.335)", easeOutCirc: "cubic-bezier(.075,.82,.165,1)", easeInOutCirc: "cubic-bezier(.785,.135,.15,.86)", easeInExpo: "cubic-bezier(.95,.05,.795,.035)", easeOutExpo: "cubic-bezier(.19,1,.22,1)", easeInOutExpo: "cubic-bezier(1,0,0,1)", easeInQuad: "cubic-bezier(.55,.085,.68,.53)", easeOutQuad: "cubic-bezier(.25,.46,.45,.94)", easeInOutQuad: "cubic-bezier(.455,.03,.515,.955)", easeInQuart: "cubic-bezier(.895,.03,.685,.22)", easeOutQuart: "cubic-bezier(.165,.84,.44,1)", easeInOutQuart: "cubic-bezier(.77,0,.175,1)", easeInQuint: "cubic-bezier(.755,.05,.855,.06)", easeOutQuint: "cubic-bezier(.23,1,.32,1)", easeInOutQuint: "cubic-bezier(.86,0,.07,1)", easeInSine: "cubic-bezier(.47,0,.745,.715)", easeOutSine: "cubic-bezier(.39,.575,.565,1)", easeInOutSine: "cubic-bezier(.445,.05,.55,.95)", easeInBack: "cubic-bezier(.6,-.28,.735,.045)", easeOutBack: "cubic-bezier(.175, .885,.32,1.275)", easeInOutBack: "cubic-bezier(.68,-.55,.265,1.55)" }; k.cssHooks["transit:transform"] = { get: function (r) { return k(r).data("transform") || new j() }, set: function (s, r) { var t = r; if (!(t instanceof j)) { t = new j(t) } if (q.transform === "WebkitTransform" && !a) { s.style[q.transform] = t.toString(true) } else { s.style[q.transform] = t.toString() } k(s).data("transform", t) } }; k.cssHooks.transform = { set: k.cssHooks["transit:transform"].set }; if (k.fn.jquery < "1.8") { k.cssHooks.transformOrigin = { get: function (r) { return r.style[q.transformOrigin] }, set: function (r, s) { r.style[q.transformOrigin] = s } }; k.cssHooks.transition = { get: function (r) { return r.style[q.transition] }, set: function (r, s) { r.style[q.transition] = s } } } n("scale"); n("translate"); n("rotate"); n("rotateX"); n("rotateY"); n("rotate3d"); n("perspective"); n("skewX"); n("skewY"); n("x", true); n("y", true); function j(r) { if (typeof r === "string") { this.parse(r) } return this } j.prototype = { setFromString: function (t, s) { var r = (typeof s === "string") ? s.split(",") : (s.constructor === Array) ? s : [s]; r.unshift(t); j.prototype.set.apply(this, r) }, set: function (s) { var r = Array.prototype.slice.apply(arguments, [1]); if (this.setter[s]) { this.setter[s].apply(this, r) } else { this[s] = r.join(",") } }, get: function (r) { if (this.getter[r]) { return this.getter[r].apply(this) } else { return this[r] || 0 } }, setter: { rotate: function (r) { this.rotate = o(r, "deg") }, rotateX: function (r) { this.rotateX = o(r, "deg") }, rotateY: function (r) { this.rotateY = o(r, "deg") }, scale: function (r, s) { if (s === undefined) { s = r } this.scale = r + "," + s }, skewX: function (r) { this.skewX = o(r, "deg") }, skewY: function (r) { this.skewY = o(r, "deg") }, perspective: function (r) { this.perspective = o(r, "px") }, x: function (r) { this.set("translate", r, null) }, y: function (r) { this.set("translate", null, r) }, translate: function (r, s) { if (this._translateX === undefined) { this._translateX = 0 } if (this._translateY === undefined) { this._translateY = 0 } if (r !== null && r !== undefined) { this._translateX = o(r, "px") } if (s !== null && s !== undefined) { this._translateY = o(s, "px") } this.translate = this._translateX + "," + this._translateY } }, getter: { x: function () { return this._translateX || 0 }, y: function () { return this._translateY || 0 }, scale: function () { var r = (this.scale || "1,1").split(","); if (r[0]) { r[0] = parseFloat(r[0]) } if (r[1]) { r[1] = parseFloat(r[1]) } return (r[0] === r[1]) ? r[0] : r }, rotate3d: function () { var t = (this.rotate3d || "0,0,0,0deg").split(","); for (var r = 0; r <= 3; ++r) { if (t[r]) { t[r] = parseFloat(t[r]) } } if (t[3]) { t[3] = o(t[3], "deg") } return t } }, parse: function (s) { var r = this; s.replace(/([a-zA-Z0-9]+)\((.*?)\)/g, function (t, v, u) { r.setFromString(v, u) }) }, toString: function (t) { var s = []; for (var r in this) { if (this.hasOwnProperty(r)) { if ((!q.transform3d) && ((r === "rotateX") || (r === "rotateY") || (r === "perspective") || (r === "transformOrigin"))) { continue } if (r[0] !== "_") { if (t && (r === "scale")) { s.push(r + "3d(" + this[r] + ",1)") } else { if (t && (r === "translate")) { s.push(r + "3d(" + this[r] + ",0)") } else { s.push(r + "(" + this[r] + ")") } } } } } return s.join(" ") } }; function m(s, r, t) { if (r === true) { s.queue(t) } else { if (r) { s.queue(r, t) } else { t() } } } function h(s) { var r = []; k.each(s, function (t) { t = k.camelCase(t); t = k.transit.propertyMap[t] || k.cssProps[t] || t; t = c(t); if (k.inArray(t, r) === -1) { r.push(t) } }); return r } function g(s, v, x, r) { var t = h(s); if (k.cssEase[x]) { x = k.cssEase[x] } var w = "" + l(v) + " " + x; if (parseInt(r, 10) > 0) { w += " " + l(r) } var u = []; k.each(t, function (z, y) { u.push(y + " " + w) }); return u.join(", ") } k.fn.transition = k.fn.transit = function (z, s, y, C) { var D = this; var u = 0; var w = true; if (typeof s === "function") { C = s; s = undefined } if (typeof y === "function") { C = y; y = undefined } if (typeof z.easing !== "undefined") { y = z.easing; delete z.easing } if (typeof z.duration !== "undefined") { s = z.duration; delete z.duration } if (typeof z.complete !== "undefined") { C = z.complete; delete z.complete } if (typeof z.queue !== "undefined") { w = z.queue; delete z.queue } if (typeof z.delay !== "undefined") { u = z.delay; delete z.delay } if (typeof s === "undefined") { s = k.fx.speeds._default } if (typeof y === "undefined") { y = k.cssEase._default } s = l(s); var E = g(z, s, y, u); var B = k.transit.enabled && q.transition; var t = B ? (parseInt(s, 10) + parseInt(u, 10)) : 0; if (t === 0) { var A = function (F) { D.css(z); if (C) { C.apply(D) } if (F) { F() } }; m(D, w, A); return D } var x = {}; var r = function (H) { var G = false; var F = function () { if (G) { D.unbind(f, F) } if (t > 0) { D.each(function () { this.style[q.transition] = (x[this] || null) }) } if (typeof C === "function") { C.apply(D) } if (typeof H === "function") { H() } }; if ((t > 0) && (f) && (k.transit.useTransitionEnd)) { G = true; D.bind(f, F) } else { window.setTimeout(F, t) } D.each(function () { if (t > 0) { this.style[q.transition] = E } k(this).css(z) }) }; var v = function (F) { this.offsetWidth; r(F) }; m(D, w, v); return this }; function n(s, r) { if (!r) { k.cssNumber[s] = true } k.transit.propertyMap[s] = q.transform; k.cssHooks[s] = { get: function (v) { var u = k(v).css("transit:transform"); return u.get(s) }, set: function (v, w) { var u = k(v).css("transit:transform"); u.setFromString(s, w); k(v).css({ "transit:transform": u }) } } } function c(r) { return r.replace(/([A-Z])/g, function (s) { return "-" + s.toLowerCase() }) } function o(s, r) { if ((typeof s === "string") && (!s.match(/^[\-0-9\.]+$/))) { return s } else { return "" + s + r } } function l(s) { var r = s; if (k.fx.speeds[r]) { r = k.fx.speeds[r] } return o(r, "ms") } k.transit.getTransitionValue = g })(jQuery);

