$(function() {
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
            url: "http://dentaldiary.gearhostpreview.com/pricelist/bycity/" + cityId,
            method: "GET"
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
                url: "http://dentaldiary.gearhostpreview.com/person/all",
                method: "GET"
            }).done(function(data) {
                var str = "";
                for(var i=0; i<data.length; i++) {
                     str+= '<option value="'+ data[i].id +'">' + data[i].fullName + "</option>"
                }
                $("#users").html(str);
            });
        });
        
        $("#withUser").click(function() {
            var cityId = localStorage.getItem("city");
            var date = $("#dateWU");
            var time = $("#timeWU");
            var dateTime = date.val() + "T" + time.val() + ":00.764";
            var recivier = $("#recivierWU");
            var price = $("#namePriceWU").val();
            var person = $("#users").val();
            var obj = {
                date: dateTime,
                recivier: recivier.val(),
                personId: person,
                cityId: cityId,
                priceId: price
            };
            $.ajax({
                url: "http://dentaldiary.gearhostpreview.com/receptions/create/withuser",
                method: "POST",
                data: obj
            }).done(function(data) {
                recivier.val("");
                date.val("");
                time.val("");
                alert("Запис добавлен");
            })
        });
        
        $("#sub").click(function() {
            var cityId = localStorage.getItem("city");
            var date = $("#date");
            var time = $("#time");
            var dateTime = date.val() + "T" + time.val() + ":00.764";
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
                    recivier: recivier.val()
                }
            };
            $.ajax({
                url: "http://dentaldiary.gearhostpreview.com/receptions/create",
                method: "POST",
                data: obj
            }).done(function(data) {
                name.val("");
                tel.val("");
                address.val("");
                recivier.val("");
                date.val("");
                time.val("");
                alert("Запис добавлено. Тепер перейдіть на сторінку 'Перегляд пацієнта' і заповніть дані про пацієнта");
            })
        });
        
    });
})