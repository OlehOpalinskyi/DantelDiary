$(function () {
    $("#login").click(function () {
        var login = $("input[name='user']");
        var password = $("input[name='pass']");

        $.ajax({
            url: "http://localhost:50612/token",
            method: "post",
            data: {
                grant_type: "password",
                username: login.val(),
                password: password.val()
            },
            error: function (jqXHR) {
                alert(jqXHR.responseJSON.error_description);
            },
            success: function (data) {
                console.log(data);
                var tokenInfo = {
                    'token': "Bearer " + data.access_token,
                    'expire': new Date().valueOf() + (data.expires_in * 1000)
                }
                localStorage.setItem('token', JSON.stringify(tokenInfo));
                window.location.replace("index.html");
            }
        });
    });
});