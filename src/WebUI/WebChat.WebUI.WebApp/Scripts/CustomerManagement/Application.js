var appManagement = appManagement || {};

var customerAppsDropdownId = '#customerApps';

appManagement.getAppId = function () {
    return $(customerAppsDropdownId).val();
}

appManagement.getApplicationUsersAndChatsInfo = function (appId) {
    $.ajax({
        url: '/CustomerAppManagement/ApplicationUsersAndChatsInfo/',
        type: "get",
        data: {appId : appId},
        success: function (result) {
            if (result.IsSuccess) {
                $('#usersAndChatsInfo').html(result.Data);
            }
            else {
                NotifyError(result.Message)
            }
        }
    });
};

appManagement.getUsersOnMap = function (appId) {
    $.ajax({
        url: '/CustomerAppManagement/UsersOnMap/',
        type: "get",
        data: { appId: appId },
        success: function (result) {
            if (result.IsSuccess) {
                $("#usersOnMap").html(result.Data);
                $('#usersOnMap #world-map').vectorMap({ map: 'world_mill' });
            }
            else {
                NotifyError(result.Message)
            }
        }
    });
};


$(document).ready(function () {
    var appId = appManagement.getAppId();
    appManagement.getApplicationUsersAndChatsInfo(appId);
    appManagement.getUsersOnMap(appId);
});

