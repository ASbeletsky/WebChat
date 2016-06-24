var app = function () {
    var self = self || {};
    var customerAppsDropdownId = '#customerApps';
    
    $('#customerApps').on('change', function () {
        var appId = $(this).val();
        window.location.href = '/CustomerAppManagement/Index/' + appId;
    });

    var loadPage = function (data) {
        $.ajax({
            url: data.url,
            type: "get",
            data: data.params,
            success: function (result) {
                if (result.IsSuccess) {
                    $(data.container).html(result.Data);
                    if (typeof (data.onSuccess) != "undefined") {
                        data.onSuccess();
                    }
                }
                else {
                    NotifyError(result.Message)
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (thrownError == "")
                    thrownError = "Не удалось получить ответ от сервера";
                NotifyError(thrownError)
            }
        });
    };

    self.getAppId = function () {
        return $(customerAppsDropdownId).val();
    };

    self.getCreateAppPage = function () {
        loadPage({
            url: '/CustomerAppManagement/CreateApplication/',
            container: '#modal-container',
            onSuccess: function () {
                $('#add-app-modal-dialog').modal('show');
            }
        });
    };

    self.onApplicationCreateSuccess = function (result) {
        if (result.IsSuccess) {
            $('#app-selector').html(result.Data);
            $('#add-app-modal-dialog').modal('hide').data('bs.modal', null);
            NotifySuccess(result.Мessage);
        }
        else {
            NotifyError(result.Мessage)
        }
    };

    self.getEditAppPage = function () {
        loadPage({
            url: '/CustomerAppManagement/EditApplication/',
            params: {appId : self.getAppId()},
            container: '#modal-container',
            onSuccess: function () {
                $('#edit-app-modal-dialog').modal('show');
            }
        });
    };

    self.onApplicationEditSuccess = function (result) {
        if (result.IsSuccess) {
            $('#appInfo').html(result.Data);
            $('#edit-app-modal-dialog').modal('hide').data('bs.modal', null);
            NotifySuccess(result.Мessage);
        }
        else {
            NotifyError(result.Мessage)
        }
    };

    self.getAppScript = function () {
        loadPage({
            url: '/CustomerAppManagement/ApplicationScript/',
            params: { appId: self.getAppId() },
            container: '#modal-container',
            onSuccess: function () {
                $('#app-script-modal-dialog').modal('show');
                var clipboard = new Clipboard('#copy-btn');

                clipboard.on('success', function (e) {
                    $('#copy-btn').tooltip('show');
                });

                $('#app-script-modal-dialog').on('hide.bs.modal', function () {
                    clipboard.destroy();
                })
            }
        });
    };
   
    self.getApplicationUsersAndChatsInfo = function (appId) {
        loadPage({
            url:'/AppStatistic/ApplicationUsersAndChatsInfo/',
            params: { appId: appId },
            container : '#usersAndChatsInfo'
        });        
    };

    self.onApplicationDelete = function (result) {
        if (result.IsSuccess) {
            window.location.href = '/CustomerAppManagement/Index/'
        }
        else{
            NotifyError(result.Message);
        }
    }

    self.getUsersOnMap = function (appId) {
        loadPage({
            url: '/AppStatistic/UsersOnMap/',
            params: { appId: appId },
            container: '#usersOnMap',
            onSuccess: function () {
                window.map.showWorldMap('#usersOnMap #world-map');
            }
        });        
    };

    self.getUsersStatistic = function (appId) {
        loadPage({
            url: '/AppStatistic/UsersStatistic/',
            params: { appId: appId },
            container: '#usersStatistic',
            onSuccess: function () {
                statistic.drowNewReturnedClientChart();
                statistic.drowAuthorizeTypeChart();
            }
        });      
    };

    self.getDialogsPerDay = function (appId) {
        loadPage({
            url: '/AppStatistic/DialogsPerDay/',
            params: { appId: appId},
            container: '#dialogs-per-day-container',
            onSuccess: function () {
                $('#dialogs-per-day-form #StartDate').datepicker({format: 'dd/mm/yyyy'});
                $('#dialogs-per-day-form #EndDate').datepicker({ format: 'dd/mm/yyyy' });
                $('#dialogs-per-day-form').submit();
            }
        });      
    };

    self.getDialogsDurationPerDay = function (appId) {
        loadPage({
            url: '/AppStatistic/DialogsDurationPerDay/',
            params: { appId: appId },
            container: '#dialogs-duration-per-day-container',
            onSuccess: function () {
                $('#dialogs-duration-per-day-container #StartDate').datepicker({ format: 'dd/mm/yyyy' });
                $('#dialogs-duration-per-day-container #EndDate').datepicker({ format: 'dd/mm/yyyy' });
                $('#dialogs-duration-per-day-form').submit();
            }
        });
    };

    self.getMessageCountDifference = function (appId) {
        loadPage({
            url: '/AppStatistic/MessageCountDifferencePage/',
            params: { appId: appId },
            container: '#message-count-difference-container',
            onSuccess: function () {
                $('#message-count-difference-form').submit();
            }
        });
    };


    
    return self;
}();

$(document).ready(function () {   
    var appId = app.getAppId();
    app.getApplicationUsersAndChatsInfo(appId);
    //app.getUsersOnMap(appId);
    //app.getUsersStatistic(appId);
    app.getDialogsPerDay(appId);
    //app.getMessageCountDifference(appId);
    app.getDialogsDurationPerDay(appId);  
});



