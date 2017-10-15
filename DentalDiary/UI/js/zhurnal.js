$(function () {
    CheckToken();
  //  var baseUrl = "http://stomat.pp.ua/";
    loadCity();
    $(this).prop("checked", false);
    $("#datepicker").datepicker();
    $("#datepicker").datepicker("option", "dateFormat", "yy-mm-dd");
$('#datepicker').hide = function () {};
    $('#datepicker').datepicker('show');
   var cityId = localStorage.getItem("city");
    var orderId;
    GetAll();
    $.ajax({
        url: baseUrl + "pricelist/bycity/" + cityId,
        method: "GET",
        headers: {
            Authorization: JSON.parse(localStorage.token).token
        },
        error: function (jqXHR, textStatus, errorThrown) {
            Unauthorized(errorThrown);
        }
        }).done(function(data) {
            var str = "";
            for (var i = 0; i < data.length; i++) {
                if (data[i].id == 2015)
                    continue;
                str+= '<option value="'+ data[i].id +'">' + data[i].name + "</option>";
            }
            $("#work").html(str);
            $("#editWork").html(str);

            $.ajax({
                url: baseUrl + "pricelist/price/" + data[1].id,
                method: "get",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                },
                success: function (data) {
                    $('#pay2').val(data);
                }
            })
        });

    $("#work").change(function () {
        var id = $(this).val();
        $.ajax({
            url: baseUrl + "pricelist/price/" + id,
            method: "get",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            success: function (data) {
                $('#pay2').val(data);
            }
        });
    })
    
        $.ajax({
            url: baseUrl + "person/all",
            method: "GET",
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            }
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
            var price = that.closest('tr').find('td').eq(3).text();
            $('#pay1').val(price);
            $('#finish').addClass('in');
            $('#finish').css('display', 'block');
        });

        $('.close').click(function () {
            var edit = $('#edit');
            var finish = $('#finish');
            if (edit.hasClass('in')) {
                edit.removeClass('in');
                edit.css('display', 'none');
            }
            else {
                finish.removeClass('in');
                finish.css('display', 'none');
            }
        });

        var that;
        $("#pay").click(function() {
            var priceOne = $("#pay1").val() * 1;
            var priceTwo = $("#pay2").val() * 1;
            var value = $('#check').prop("checked");
            if (value == false) {
                var obj = {
                    orderId: orderId,
                    price: priceOne,
                    comment: $('#comment').val()
                };
                $.ajax({
                    url: baseUrl + "receptions/pay",
                    method: "post",
                    headers: {
                        Authorization: JSON.parse(localStorage.token).token
                    },
                    data: obj,
                    error: function (jqXHR, textStatus, errorThrown) {
                        Unauthorized(errorThrown);
                    }
                }).done(function(data) {
                    $("#pay1").val("");
                    $(".close").eq(1).click();
                    $('#check').prop("checked", false);
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
                    url: baseUrl + "receptions/pay/withorder",
                    method: "POST",
                    headers: {
                        Authorization: JSON.parse(localStorage.token).token
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        Unauthorized(errorThrown);
                    },
                    data: obj
                }).done(function(data) {
                    $("#pay1").val("");
                    $("#pay2").val("");
                    $(".close").eq(1).click();
                    that.closest("tr").remove();
                });
            }
        });

        $('#check').change(function () {
            var value = $(this).prop("checked");
            console.log(value);
            if(value)
                $('#addedPrice').show();
            else
                $('#addedPrice').hide();
        });

        $("#myInput").keypress(function(e) {
            if(e.which == 13) {
                $.ajax({
                    url: baseUrl + "receptions/search-by-customer/" + cityId,
                    method: "GET",
                    headers: {
                        Authorization: JSON.parse(localStorage.token).token
                    },
                    data: { customer: $(this).val() },
                    error: function (jqXHR, textStatus, errorThrown) {
                        Unauthorized(errorThrown);
                    },
                    beforeSend: function () {
                        $("#table").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
                    },
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
    
        $(document).on("click", ".btn-primary.btn-xs", function() {
            var id = $(this).data("id");
            orderId = id;
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
                    console.log(data);
                    $('#editUsers').val(data.personId);
                    $('#editWork').val(data.priceId);
                }
            });
            $('#edit').addClass('in');
            $('#edit').css('display', 'block');
        });
    
        $("#editDiary").click(function() {
            var date = $("#date");
            var time = $("#time");
            var dateTime;
            if (date.val() !== "" && time.val() !== "")
                dateTime = date.val() + "T" + time.val() + ":00.764";
            else
                dateTime = "";
            var obj = {
                date: dateTime,
                userId: $("#editUsers").val(),
                priceId: $("#editWork").val()
            };
            $.ajax({
                url: baseUrl + "receptions/edit-diary/" + orderId,
                method: "PUT",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                },
                beforeSend: function(xhr, prop) {
                    if (dateTime == "") {
                        xhr.abort();
                        alert("������ ����");
                    }
                },
                data: obj
            }).done(function(data) {
                console.log(data);
                date.val("");
                time.val("");
                GetAll();
                $(".close").click();
            })
        })
    
        $(document).on("click", ".btn-danger.btn-xs", function() {
            var id = $(this).data("id");
            var that = $(this);
            console.log(id);
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
        
        $('#datepicker').on('change', function () {
            var date = $('#datepicker').datepicker().val();
            console.log(date);
            $.ajax({
                url: baseUrl + "receptions/sort-by-date?date=" + date + "&cityId=" + cityId,
                method: "post",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                data: {
                    date: date,
                    cityId: cityId
                },
                beforeSend: function () {
                    $("#table").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                },
                success: function(data) {
                    BuildTable(data);
                    DeleteNulls();
                }
            })
        });
    
        function GetAll() {
            $.ajax({
                url: baseUrl + "receptions/diary/" + cityId,
                method: "GET",
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
            });
        }
    
        function BuildTable(data) {
            var str = "";
            var important = "";
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var item = "";
                    var dateTime = new Date(data[i].date);
                    var date = dateTime.toLocaleDateString();
                    var time = dateTime.toLocaleTimeString();
                    if (data[i].recomended == true) {
                        item += "<tr class='recomend'><td>";
                    }
                    else
                        item += "<tr><td>";
                    item += data[i].customer + "</td><td>" + data[i].priceName + "</td><td>" + data[i].kindOfWork +
                        "</td><td>" + data[i].priceCount + "</td><td>" + date + "</td><td>" + time + "</td><td>" + data[i].approximateTime + "</td>" + '<td class="text-center"><p data-placement="top" data-toggle="tooltip"><button class="btn btn-primary btn-xs" data-id="' + data[i].id + '" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button></p></td>' +
                        '<td class="text-center"><p data-placement="top" data-toggle="tooltip"><button class="btn btn-danger btn-xs" data-id="' +
                        data[i].id + '"><span class="glyphicon glyphicon-trash"></span></button></p></td>' + '<td class="text-center"><p data-placement="top" data-toggle="tooltip"><button class="btn btn-success btn-xs" data-toggle="modal" data-target="#finish" data-id="' + data[i].id + '"><span class="glyphicon glyphicon-plus"></span></button></p></td></tr>';

                    if (data[i].priority > 1)
                        important += item;
                    else
                        str += item;
                }
            }
            $("#table").html(str);
            $("#priority").html(important);
        }

        function Priority() {
            $("tr[data-priority='5']").css("background-color", "red");
        }
    });
