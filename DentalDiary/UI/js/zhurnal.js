$(function() {
   loadCity();
   var cityId = localStorage.getItem("city");
    var orderId;
    GetAll();
    $.ajax({
        url: "http://dentaldiary.gearhostpreview.com/pricelist/bycity/" + cityId,
            method: "GET"
        }).done(function(data) {
            var str = "";
            for(var i=0; i<data.length; i++) {
                str+= '<option value="'+ data[i].id +'">' + data[i].name + "</option>";
            }
            $("#work").html(str);
            $("#editWork").html(str);
        });
    
    $.ajax({
        url: "http://dentaldiary.gearhostpreview.com/person/all",
                method: "GET"
            }).done(function(data) {
                var str = "";
                for(var i=0; i<data.length; i++) {
                     str+= '<option value="'+ data[i].id +'">' + data[i].fullName + "</option>"
                }
                $("#editUsers").html(str);
            });
    
    $(document).on("click", ".btn-success.btn-xs", function() {
        var id = $(this).data("id");
        orderId = id;
        that = $(this);
    });
    var that;
    $("#pay").click(function() {
        var priceOne = $("#pay1").val() * 1;
        var priceTwo = $("#pay2").val() * 1;
        if(priceTwo == "") {
            $.ajax({
                url: "http://dentaldiary.gearhostpreview.com/receptions/pay/" + orderId + "/" + priceOne,
                method: "GET"
            }).done(function(data) {
                $("#pay1").val("");
                $(".close").eq(1).click();
                that.closest("tr").remove();
            });
        }
        else {
            var priceId = $("#work").val() * 1;
            var obj = {
                idOrder: orderId,
                priceOne: priceOne,
                priceId: priceId,
                priceTwo: priceTwo
            };
            
            $.ajax({
                url: "http://dentaldiary.gearhostpreview.com/receptions/pay/withorder",
                method: "POST",
                data: obj
            }).done(function(data) {
                $("#pay1").val("");
                $("#pay2").val("");
                $(".close").eq(1).click();
                that.closest("tr").remove();
            });
        }
    });
    
    $("#myInput").keypress(function(e) {
        if(e.which == 13) {
            $.ajax({
                url: "http://dentaldiary.gearhostpreview.com/receptions/search-by-customer/" + cityId,
                method: "GET",
                data: { customer: $(this).val() },
                beforeSend: function () {
                    $("#table").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
                },
                success: function(data) {
                    BuildTable(data);
                }
            });
        } 
    });
    
    $("#myInput").keyup(function(e) {
        if(e.keyCode == 27) {
            $(this).val("");
            GetAll();
        }
    });
    
    $(document).on("click", ".btn-primary.btn-xs", function() {
        var id = $(this).data("id");
        orderId = id;
    });
    
    $("#editDiary").click(function() {
        var date = $("#date");
        var time = $("#time");
        var dateTime = date.val() + "T" + time.val() + ":00.764";
        var obj = {
            date: dateTime,
            userId: $("#editUsers").val(),
            priceId: $("#editWork").val()
        };
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/receptions/edit-diary/" + orderId,
            method: "PUT",
            data: obj
        }).done(function(data) {
            console.log(data);
            date.val("");
            time.val("");
            BuildTable();
            $("#close").click();
        })
    })
    
    $(document).on("click", ".btn-danger.btn-xs", function() {
        var id = $(this).data("id");
        var that = $(this);
        console.log(id);
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/receptions/delete/" + id,
            method: "DELETE"
        }).done(function(data) {
            that.closest("tr").remove();
        });
    });
    
     $('.datetimepicker-days').on('click', 'td.day', function (e) {
         var day = $(this).text();
         var date = $(".date.form_date").data("datetimepicker").getDate();
         var year = date.getFullYear();
         var mounth = (date.getMonth() + 1);
         if(date.getMonth() + 1 < 10)
             mounth = "0" + (date.getMonth() + 1);
         if(day < 10)
             day = "0" + day;
         var dateStr = year + "-" + mounth + "-" + day;
         $.ajax({
             url: "http://dentaldiary.gearhostpreview.com/receptions/sort-by-date?date=" + dateStr + "&cityId=" + cityId,
             method: "post",
             data: {
                 date: dateStr,
                 cityId: cityId
             },
             beforeSend: function () {
                 $("#table").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
             },
             success: function(data) {
                 BuildTable(data);
             }
         })
    });
    
    function GetAll() {
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/receptions/diary/" + cityId,
            method: "GET",
            beforeSend: function () {
                $("#table").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
            }
    }).done(function(data) {
        BuildTable(data);
        Priority();
    });
    }
    
    function BuildTable(data) {
        var str = "";
        for(var i=0; i<data.length; i++) {
            var dateTime = new Date(data[i].date);
            var date = dateTime.toLocaleDateString();
            var time = dateTime.toLocaleTimeString();
            str += "<tr data-priority='"+ data[i].priority +"'><td>" + data[i].customer + "</td><td>" + data[i].priceName + "</td><td>" + data[i].kindOfWork + 
                "</td><td>" + data[i].priceCount + "</td><td>" + date + "</td><td>" + time +"</td>" + 
                '<td class="text-center"><p data-placement="top" data-toggle="tooltip"><button class="btn btn-primary btn-xs" data-id="'+ data[i].id+ '" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button></p></td>' + 
                '<td class="text-center"><p data-placement="top" data-toggle="tooltip"><button class="btn btn-danger btn-xs" data-id="'+
                data[i].id+'"><span class="glyphicon glyphicon-trash"></span></button></p></td>' + '<td class="text-center"><p data-placement="top" data-toggle="tooltip"><button class="btn btn-success btn-xs" data-toggle="modal" data-target="#finish" data-id="'+ data[i].id + '"><span class="glyphicon glyphicon-plus"></span></button></p></td></tr>';
            }
        $("#table").html(str);
    }

    function Priority() {
        $("tr[data-priority='5']").css("background-color", "red");
    }
});