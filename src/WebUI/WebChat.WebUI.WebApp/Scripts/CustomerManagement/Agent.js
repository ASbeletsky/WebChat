var agentManagement = agentManagement || {};

var pageContainer = '#AjaxContainer';


agentManagement.OpenLink = function (actionName){
    $.ajax({
        url: '/CustomerAgentManagement/' + actionName,
        type: "get",
        success: function (result){
            if(result.IsSuccess){
                $(pageContainer).html(result.Data);
            }
            else{
                NotifyError(result.Message)
            }
        }       
    });
};

$(document).ready(function () {
    agentManagement.OpenLink('AgentsList');
});