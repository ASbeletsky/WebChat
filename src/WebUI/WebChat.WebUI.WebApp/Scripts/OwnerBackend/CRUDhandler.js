$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

function editAgent(agentId, btn) {
    if ($(btn).hasClass("btn-primary")) {
        $('#agent' + agentId + ' input[type="text"]').prop("disabled", "");
        $(btn).removeClass("btn-primary").addClass("btn-success").html("Save");
    } else {
        $('#agent' + agentId).submit();
    }
};

$(".agentsSites > a > span").click(function(e) {
    e.preventDefault();
    var elem = $(this).parent();
    var agentId = $(this).parent().data("agentid");
    var appId = $(this).parent().data("appid");

    //console.log(agentId + " - " + appId);
    
    $.get("/Owner/ChangeAgentApp/?agentId=" + agentId + "&appId=" + appId, function (data) {
        if (data == "True") {
            elem.toggleClass("active");
        } else {
            alert("Data Loaded: " + data);
        }
        //
    });

});
