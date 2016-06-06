; (function ($) {
    var baseDomain = __chat.targetUrl;

    function listener(event) {

        var data = event.data;
        switch (data.key) {
            case "hide_chat_window": hide_chat_window(); break;
            case "open_chat_window": open_chat_window(); break;
            case "getCurrentUrl": {
                var currectUrl = window.location.href;
                var iframeWin = document.getElementById("webchat-full").contentWindow;
                iframeWin.postMessage({ key: "currentUrl", value: currectUrl }, baseDomain);
            } break;
            case "setUrl": {
                window.location.href = data.value;
            } break;
        }
    }

    if (window.addEventListener) {
        window.addEventListener("message", listener);
    } else {
        // IE8
        window.attachEvent("onmessage", listener);
    }


    jQuery(function () {
        create_webchat_compact_container();
        create_webchat_compact_window();

        var chatWasOpened = localStorage.getItem("chatWasOpened");
        if (chatWasOpened === "false" || chatWasOpened === null)
            $('#webchat-compact-container').show(null);
        else
            open_chat_window();
    });

    function create_webchat_compact_container() {
        $('<div id="webchat-compact-container"></div>').appendTo('body');
        $('#webchat-compact-container').attr('style', 'position: fixed; bottom: 0px; right: 15px; width: 250px; height: 35px; z-index: 2147483639; opacity: 1; background: transparent;');
        $('#webchat-compact-container').hide(null);
    }

    function create_webchat_compact_window() {
        $('<div id="webchat-compact"/></div>').appendTo('#webchat-compact-container');
        $('#webchat-compact').attr('style', 'left: 0; width: 100%; border: 0; padding: 0; margin: 0; float: none;')

        $.ajax({
            url: baseDomain + '/Chat/CompactView',
            type: "GET",
            jsonp: 'jsonp',
            success: function (data) {
                $('#webchat-compact').html(data);
                $('#webchat-compact-container').show(null);
                open_chat_window();
            }
        });
    };

    function open_chat_window() {
        localStorage.setItem("chatWasOpened", "true");
        $('#webchat-compact-container').hide(null);

        var chat_window = $('#framewrap');
        if (chat_window.length) {
            chat_window.show(null);
        }
        else {
            $('<div id="framewrap"></div>').appendTo('body');
            $('#framewrap').attr('style', 'position: fixed; padding-top:0px; background-color:#f0ad4e; bottom: 0px; right: 15px; width: 300px; height: 385px; border: 1px black solid;');

            $('<iframe id="webchat-full" name="chat"/>').appendTo('#framewrap');
            $('#webchat-full').attr('style', 'dicplay="inline"; width:100%; height:100%; background-color:#fff;');
            $('#webchat-full').attr('scrolling', 'no');
            $('#webchat-full').attr('frameborder', '0');
            $('#webchat-full').attr('allowtransparency', 'true');
            $('#webchat-full').attr('src', baseDomain + '/chat');

            var targetUrl = localStorage.getItem('webChatTargetUrl');
            var targetAppId = localStorage.getItem('webChatAppId');
            var iframeWin = document.getElementById("webchat-full").contentWindow;
            window.setTimeout(function () {
                iframeWin.postMessage({ key: "targetUrl", value: targetUrl }, baseDomain);
                iframeWin.postMessage({ key: "targetAppId", value: targetAppId }, baseDomain);
            }, 1000)();

        }


    }

    function hide_chat_window() {
        localStorage.setItem("chatWasOpened", "false");
        $('#framewrap').hide(null);
        $('#webchat-compact-container').show(null);
    }

})(jQuery);









