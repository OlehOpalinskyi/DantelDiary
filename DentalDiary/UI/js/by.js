$(function () {
    CheckToken();
 //   var baseUrl = "http://stomat.pp.ua/";
    var cityId = localStorage.getItem("city");
    loadCity();
    GetAll();
    
     $(document).on("click", ".btn-danger.btn-xs", function() {
        var id = $(this).data("id");
        var that = $(this);
        $.ajax({
            url: baseUrl + "receptions/delete/" + id,
            method: "DELETE",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            }
        }).done(function(data) {
            that.closest("tr").remove();
        });
    });
    
    var id;
    $(document).on("click", ".btn-primary.btn-xs", function() {
        id = $(this).data("id");
        $.ajax({
            url: baseUrl + "receptions/get-reception/" + id,
            method: 'get',
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            success: function (data) {
                $('#recivier').val(data.recivier);
                $('#return').val(data.return);
                if (data.isReturn === true) {
                    $('#isReturn').attr("checked", "checked");
                }
            }
        });
    });
    
    $("#editRecivier").click(function() {
        var recivier = $("#recivier").val();
        var obj = {
            name: recivier,
            returnPay: $('#return').val(),
            isReturn: $('#isReturn').prop("checked")
        };
        $.ajax({
            url: baseUrl + "receptions/edit-recivier/" + id,
            method: "PUT",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            data: obj,
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            success: function (data) {
                GetAll();
                $("#close").click();
            }
        });
    });
    
    $("#myInput").keypress(function(e) {
        if(e.which == 13) {
            $.ajax({
                url: baseUrl + "receptions/search-by-recivier/" + cityId,
                method: "GET",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                },
                data:{customer: $(this).val()},
                success: function(data) {
                    BuildTable(data);
                    DeleteNulls();
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
    
    function GetAll() {
        $.ajax({
            url: baseUrl + "receptions/reciviers/bycity/" + cityId,
            method: "post",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            beforeSend: function () {
                $("#table").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
            }
    }).done(function(data) {
        BuildTable(data);
        DeleteNulls();
        Returned();
    });
    }
    
    function BuildTable(data) {
        var str = "";
        for(var i=0; i<data.length; i++) {
            var dateTime = new Date(data[i].date);
            var date = dateTime.toLocaleDateString();
            str += "<tr data-return='" + data[i].isReturn + "'><td>" + data[i].recivier + "</td><td>" + data[i].customer + "</td><td>" + data[i].priceName + "</td><td>" + data[i].kindOfWork + "</td><td>" + data[i].priceCount + "</td><td>" + data[i].payment + "</td><td>" + data[i].return + "</td><td>" + date + "</td>" +
                '<td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-primary btn-xs" data-toggle="modal" data-target="#edit" data-id="'+data[i].id + '"><span class="glyphicon glyphicon-pencil"></span></button></p></td>' + '<td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-danger btn-xs" data-id="'+data[i].id + '"><span class="glyphicon glyphicon-trash"></span></button></p></td></tr>'
            }
        $("#table").html(str);
    }

    function Returned() {
        $('tr[data-return="true"]').map(function (i, el) {
            $(el).addClass('green');
        });
    }
})