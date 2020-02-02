var Download_URL = "";
var GFlag = "";
var chatHub = $.connection.chatHub;
var Group_Users = [];
//funtion to get connnted user list
function Append_User_List(desc) {
    $("#group-1").empty();
    $.ajax
        ({
            url: "/Chat/GetUserList?DESC=" + desc,
            method: "Get",
            dataType: "json",
            success: function (data) {
                $.each(data, function (index, value) {
                    AddUser(value.UserId, value.UserName, value.ISACTIVE, value.Message)
                    Connected_users.push({ UserId: value.UserId, UserName: value.UserName })
                })
            }
        });
}


$(function () {
    // Declare a proxy to reference the hub. 
    registerClientMethods(chatHub);

    // Start Hub
    $.connection.hub.start().done(function () {
        var name = $("#chatdisplayname").val();
        var userid = $('#chatuserid').val();

        if (name.length > 0) {
            chatHub.server.connect(name, userid);
        }

    });

});



function registerClientMethods(chatHub) {
    // Calls when user successfully logged in
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {
        $('#hdId').val(id);
        $('#chatdisplayname').val(userName);
        $('#spanUser').html(userName);
    }


    // On New User Connected
    chatHub.client.onNewUserConnected = function (id, name) {
        $('#' + id).find('.user-status').addClass('fa fa-circle green');
        Append_User_List("");
    }

    //Funtion to get online userlist for anroid
    chatHub.client.GetUserList = function (UserList) {
    }

    //funtion to get chat history for anroid 
    chatHub.client.GetChatHistory = function (History) {

    }

    //funtion to get msgid for anroid 
    chatHub.client.GetMsgId = function (MsgId) {

    }

    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName) {
        $('#' + id).find('.user-status').removeClass('green');
    }

    // On private message  send --- used for getting only current msg -- not in use
    //chatHub.client.sendPrivateMessage = function (toUserId, fromUserName, fromuserid, GroupUsers, message, msgid, msgdate) {
    // AddPrivateMsg(msgid, fromUserName, fromuserid, message, GroupUsers, msgdate, toUserId) 
    // }

    // On private message  send - for anroid-- used for getting only current msg -- not in use
    chatHub.client.sendPrivateMessageForMo = function (toUserId, fromuserid, message, msgdate) {
    
    }

    // On private message  send - added  for test
    chatHub.client.sendPrivateMessageForMo1 = function (toUserId, fromuserid, message, msgdate) {

    }

    chatHub.client.TouserchatHistory = function (fromuserid, toUserId, History) {
        if (History != null && History != '') {
            $("div[data-chatboxid=" + fromuserid + "]").find('.conversation-body').html('');
            $.each(History, function (i, History) {
                Group_Users.push({ UserId: History.ToUserId, UserName: History.TOUSER });
                Group_Users.push({ UserId: History.UserId, UserName: History.FROMUSER });
                
                if ($("div[data-chatboxid=" + fromuserid + "]").length) {
                    //$('.chatdv .chatbox').attr("data-currentmsgid", toUserId);
                    //$("div[data-currentmsgid=" + toUserId + "]").find('.conversation-body').html('');
                    
                    ServerUser.pushMessage(toUserId, History.Message, History.FROMUSER, History.UserId, History.CREATEDON, Group_Users, true, true, "1", 1, fromuserid)
                }
                else {
                    ServerUser.pushMessage(toUserId, History.Message, History.FROMUSER, History.UserId, History.CREATEDON, Group_Users, true, true, "0", 1, fromuserid)
                }
                Group_Users = [];
            });

            var $DivHight = 0;
            $('.conversation-body').each(function () {
                $DivHight += this.scrollHeight;
                console.log($DivHight);
            });
            $('div').animate({ scrollTop: $DivHight }, 600);

        }
    }
}


//on getting private message to push  for anroid-- not in use
//function AddPrivateMsg(msgid, fromUserName, fromuserid, message, GroupUsers, msgdate, toUserId) {
//    ServerUser.pushMessage(msgid, message, fromUserName, fromuserid, new Date(), GroupUsers, true, true,"1","0");
//}

//funtion to add user in userlist
function AddUser(id, name, status, Msg) {
    if (parseInt(Login_user_id) != parseInt(id)) {
        code = $('<a id=' + id + ' href="#" style="display:block;padding:10px 0;" data-msgid="0" data-usermsgid="0" class="media-box no-margin-top active user-list"><span class="width-100"> <span class="user-status pull-right" style="border:none; margin-top:3px;"></span> <span class="media-box-body"><span class="media-box-heading"><strong title="' + name + '" style="width: 160px;float: left;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;">' + name + '</strong><div class="badge badge-warning hidden chat-badge-count" style="margin-left: 5px; float: right;">0</div><br><small class="text-muted">' + Msg + '</small> </span> </span></a>');
        if (status == true) { code.find('.user-status').addClass('fa fa-circle green'); }
        else { code.find('.user-status').removeClass('fa fa-circle '); }
   
        $("#group-1").append(code);
    }
    else {

    }
}

//function to show tooltip on username mouseover
$(document).on("mouseover", ".username", function (e) {
    if (!$(this).data('tooltip')) {
        $(this).tooltip({
            content: function () {
                return $(this).attr('title');
            },
            position: { my: "left+15 center", at: "right center" }
        }).triggerHandler('mouseover');
    }
});

