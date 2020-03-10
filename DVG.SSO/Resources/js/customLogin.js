var requestUrl;
var timeout;

$(document).ready(function () {
    $('button[type=submit]').click(function () {
        var form = $(this).parents('form:first');
        if (form.valid()) {
            form.submit(function (e) {
                var data = form.serialize();
                if (this.beenSubmitted == data)
                    return false;
                else
                    var img = document.createElement("IMG");
                    img.src = "/Resources/images/loaders/loading13.gif";
                    document.getElementById('lblMessage').appendChild(img);

                    this.beenSubmitted = data;
                    $.post(form.attr('action'), data, function (response) {
                    if (response.Error) {
                        CreateMessageError(response.Title);
                        //$("#imgCaptcha").attr("src", "/Account/Captcha/?prefix=Captcha&time=" + new Date().getTime());
                    }
                    else {
                    	if (form.attr('action') == '/Account/RecoverPassword/' || form.attr('action') == '/Account/Activate/') {
                    		CreateMessageSucces(response.Title);
                    	}
                    	else {
                    		CreateMessageSucces('Đang chuyển hướng..' + response.Title);
                    		$(function () {
                    			function redirect() {
                    				document.location.href = response.Title;
                    			};
                    			window.setTimeout(redirect, 3); // 1 seconds
                    		});
                    	}
                    }
                });
                return false;
            });
        }
    });
});
function CreateMessageSucces(strMessage) {
    var message = $("#lblMessage");
    message.html("<div class=\"alert alert-success\"><button data-dismiss=\"alert alert-success\" class=\"close\" type=\"button\">×</button>" + strMessage + "</div>");
}
function CreateMessageError(strMessage) {
    var message = $("#lblMessage");
    message.html("<div class=\"alert\"><button data-dismiss=\"alert alert-error\" class=\"close\" type=\"button\">×</button>" + strMessage + "</div>");
}
