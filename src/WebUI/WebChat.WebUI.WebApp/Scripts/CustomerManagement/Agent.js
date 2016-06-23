var agents = function () {

    var self = self || {};

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

    self.getAgentList = function () {
        loadPage({
            url: '/CustomerAgentManagement/AgentsList/',
            container: '#agent-list-container'
        });
    }

    self.AddAgentPage = function () {
        loadPage({
            url: '/CustomerAgentManagement/AddAgent/',
            container: '#agent-container',
            onSuccess: function () {
                $('#SelectedApps').chosen();
            }
        });
    }

    self.OnAddAgentSuccess = function (result) {
        if (result.IsSuccess) {
            NotifySuccess(result.Message)
            $('#agent-container').html(result.Data);
        }
        else {
            NotifyError(result.Message);
        }
    }

    return self;
}();

$('#add-agent-btn').on('click', function () {
    agents.AddAgentPage();
});

$(document).ready(function () {
    agents.getAgentList();   
});