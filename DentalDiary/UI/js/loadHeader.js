function loadCity() {
        $.ajax({
            url: "http://localhost:50612/city/all",
            method: "GET",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            }
        }).done(function(data) {
            var cities = "";
            for(var i=0; i<data.length; i++) {
                cities+= "<li><a href='#' data-id='" + data[i].id + "'>" + data[i].name + "</a></li>";
            }
            $('#cities').append(cities);
            var id = localStorage.getItem("city");
            if(id == null) {
                 $('#cities li').eq(0).addClass('active');
                localStorage.setItem("city", data[0].id);
            }
           else {
               $("[data-id='"+ id +"']").parent().addClass('active');
           }
        });
}

function CheckToken() {
    var currDate = new Date().valueOf();
    if (!window.localStorage.token || JSON.parse(localStorage.token).expire < currDate) {
        localStorage.removeItem("token");
        window.location.replace("auth.html");
    }
}

function DeleteNulls() {
    $("#table tr td").map(function (index, item) {
        if ($(item).text() == "null")
            $(item).text("");
    });
}

function Unauthorized(err) {
    if (err == "Unauthorized") {
        localStorage.removeItem("token");
        location.reload();
    }
}