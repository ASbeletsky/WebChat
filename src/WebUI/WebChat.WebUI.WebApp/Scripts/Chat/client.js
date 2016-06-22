function listener(event) {
    if (event.data.key == "targetUrl") {       
            localStorage.setItem('webChatTargetUrl', event.data.value);
    }
    if (event.data.key == "targetAppId") {
            localStorage.setItem('webChatAppId', event.data.value);
    }
    if (event.data.key == "currentUrl") {
        _parentUrl = event.data.value;
    }
    
}
if (window.addEventListener) {
    window.addEventListener("message", listener);
} else {
    // IE8
    window.attachEvent("onmessage", listener);
}

    function ExecuteParentFunction(funcName) {
        var targetUrl = localStorage.getItem("webChatTargetUrl");
        parent.postMessage({ key: funcName }, targetUrl);
    }

    function HideChat() {        
        ExecuteParentFunction('hide_chat_window');
    };

    var _parentUrl = "";

    function authExternalProvider(provider) {
        var baseUrl = jQuery("base").first().attr("href");
        var returnUrl = getParentUrl() + '/Account/ExternalLoginCallback'
        var externalProviderUrl = baseUrl + "Account/ExternalLogin?provider=" + provider
                                                                    + "&returnUrl=" + returnUrl;
        var oauthWindow = popupWindow(externalProviderUrl, 700, 600);
    };

    function getParentUrl() {
        _parentUrl = localStorage.getItem('webChatTargetUrl');
        ExecuteParentFunction('getCurrentUrl');
        return _parentUrl;
    }

    function popupWindow(url, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    }
