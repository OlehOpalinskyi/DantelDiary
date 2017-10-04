$(function () {
    CheckToken();
    var baseUrl = "http://stomat.pp.ua/";
    loadCity();
    $( "#tabs" ).tabs();
    $(document).on("click", '#cities li', function() {
        var id = $(this).find('a').data("id");
        localStorage.setItem("city", id);
        $(".active").removeClass("active");
        $(this).addClass("active");
    });
    
    $("#reception").click(function() {
        var cityId = localStorage.getItem("city");
        $.ajax({
            url: baseUrl + "pricelist/bycity/" + cityId,
            method: "GET",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
        }).done(function(data) {
            var str = "";
            for(var i=0; i<data.length; i++) {
                str+= '<option value="'+ data[i].id +'">' + data[i].name + "</option>";
            }
            $("#namePrice").html(str);
            $("#namePriceWU").html(str);
        });
        
        $("#tab2").click(function() {
            $.ajax({
                url: baseUrl + "person/all",
                method: "GET",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                }
            }).done(function(data) {
                var str = "";
                for(var i=0; i<data.length; i++) {
                     str+= '<option value="'+ data[i].id +'">' + data[i].fullName + "</option>"
                }
                $("#users").html(str);
                console.log(data);
            });
        });
        
        $("#withUser").click(function () {
            var that = $(this);
            var cityId = localStorage.getItem("city");
            var date = $("#dateWU");
            var time = $("#timeWU");
            var dateTime;
            if (date.val() !== "" && time.val() !== "")
                dateTime = date.val() + "T" + time.val() + ":00.764";
            else
                dateTime = "";
            var recivier = $("#recivierWU");
            var price = $("#namePriceWU").val();
            var person = $("#users").val();
            var obj = {
                date: dateTime,
                recivier: recivier.val(),
                personId: person,
                cityId: cityId,
                priceId: price,
                priority: $("#priorityW").val()
            };
            $.ajax({
                url: baseUrl + "receptions/create/withuser",
                method: "POST",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                },
                data: obj,
                beforeSend: function (xhr, opts) {
                    if (dateTime == "") {
                        xhr.abort();
                        alert("Введіть дату");
                    }
                    else
                        that.html('<i class="fa fa-spinner fa-spin fa-2x fa-fw"></i><span class="sr-only">Loading...</span>');
                }
            }).done(function(data) {
                recivier.val("");
                date.val("");
                time.val("");
                that.html("Add");
                $(".btn.btn-default").click();
            })
        });
        
        $("#sub").click(function () {
            var that = $(this);
            var cityId = localStorage.getItem("city");
            var date = $("#date");
            var time = $("#time");
            var dateTime;
            if (date.val() !== "" && time.val() !== "")
                dateTime = date.val() + "T" + time.val() + ":00.764";
            else
                dateTime = "";
            var name = $("#name");
            var tel = $("#tel");
            var address = $("#adress");
            var recivier = $("#recivier");
            var price = $("#namePrice").val();
            var obj = {
                person: {
                    fullName: name.val(),
                    address: address.val(),
                    phoneNumber: tel.val(),
                    dateOfBirth: new Date()
                },
                recInfo: {
                    date: dateTime,
                    cityId: cityId,
                    priceId: price,
                    recivier: recivier.val(),
                    priority: $("#priority").val()
                }
            };
            $.ajax({
                url: baseUrl + "receptions/create",
                method: "POST",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                },
                data: obj,
                beforeSend: function (xhr, prop) {
                    if (dateTime == "") {
                        xhr.abort();
                        alert("Введіть дату");
                    }
                    else
                        that.html('<i class="fa fa-spinner fa-spin fa-2x fa-fw"></i><span class="sr-only">Loading...</span>');
                }
            }).done(function(data) {
                name.val("");
                tel.val("");
                address.val("");
                recivier.val("");
                date.val("");
                time.val("");
                that.html("Add");
                $(".btn.btn-default").click();
                alert("Запис добавлено. Тепер перейдіть на сторінку 'Перегляд пацієнта' і заповніть дані про пацієнта");
            })
        });
        
    });
})