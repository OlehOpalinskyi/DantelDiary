$(function () {
    CheckToken();
    var baseUrl = "http://localhost:50612/";
    loadCity();
    GetAll();
    
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
    }).done(function (data) {
        var str = "";
        for (var i = 0; i < data.length; i++) {
            str += '<option value="' + data[i].id + '">' + data[i].name + "</option>";
        }
        $("#priceList").html(str);
    });

    $(document).on("click", ".btn-danger.btn-xs", function() {
        var id = $(this).data("id");
        var that = $(this);
        $.ajax({
            url: baseUrl + "person/delete/" + id,
            method: "DELETE",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
        }).done(function(data) {
            that.closest("tr").remove();
        });
    });
    var idPerson;
    $(document).on("click", ".btn-info", function() {
        var id = $(this).data("id");
        $("#order").data("id", id);
            $("#id").val(id);
            idPerson = id;
            $.ajax({
                url: baseUrl + "person/" + id,
                method: "get",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                }
            }).done(function(data) {
                var dob = ToDateString(data.dateOfBirth);
                var fVisit = ToDateString(data.firstVisit);
                var lVisit = ToDateString(data.lastVisit);
                $("#name").val(data.fullName);
                $("#phone").val(data.phoneNumber);
                $("#email").val(data.email);
                $("#address").val(data.address);
                $("#dob").val(dob);
                $("#fVisit").val(fVisit);
                $("#lVisit").val(lVisit);
                $("#debt").val(data.debt);
                $("#complaints").val(data.complaints);
                $("#lastTreatment").val(data.lastTreatment);
                $("#lastDiagnosis").val(data.lastDiagnosis);
                $("#finishDiagnosis").val(data.finalDiagnosis);
                $("#advice").val(data.anotherOpinion);
                $("#treatment").val(data.treatmentPlan);
                $("#galary").attr("href", data.linkToImages);
            });
        
        $.ajax({
            url: baseUrl + "person/get-receptions/" + id,
            method: "get",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            }
        }).done(function(data) {
            var str = "";
            for(var i=0; i<data.length; i++) {
                var date = new Date(data[i].date).toLocaleDateString();
                str += "<tr><td>" + data[i].priceName + "</td><td>" + data[i].kindOfWork + "</td><td>" + data[i].priceCount + "</td><td>" + 
                    data[i].payment + "</td><td>" + date + "</td></tr>";
            }
            $("#tableUser").html(str);
        });
        
        });
    
    $("#createRec").click(function () {
        var id = $("#order").data("id");
        var that = $(this);
        var cityId = localStorage.getItem("city");
        var date = $("#dateWU");
        var time = $("#timeWU");
        var dateTime = date.val() + "T" + time.val() + ":00.764";
        var recivier = $("#recivierWU");
        var price = $("#priceList").val();
        var obj = {
            date: dateTime,
            recivier: recivier.val(),
            personId: id,
            cityId: cityId,
            priceId: price,
            priority: $("#priorityW").val()
        };
        console.log(obj);
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
            beforeSend: function () {
                that.html('<i class="fa fa-spinner fa-spin fa-2x fa-fw"></i><span class="sr-only">Loading...</span>');
            }
        }).done(function (data) {
            recivier.val("");
            date.val("");
            time.val("");
            that.html("Add");
            $(".close").click();
        });
    });

    $("#myInput").keypress(function (e) {
        if (e.which == 13) {
            $.ajax({
                url: baseUrl + "person/search",
                method: "GET",
                headers: {
                    Authorization: JSON.parse(localStorage.token).token
                },
                data: { person: $(this).val() },
                error: function (jqXHR, textStatus, errorThrown) {
                    Unauthorized(errorThrown);
                },
                beforeSend: function () {
                    $("#table").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
                },
                success: function (data) {
                    BuildTable(data);
                    DeleteNulls();
                }
            });
        }
    });

    $("#myInput").keyup(function (e) {
        if (e.keyCode == 27) {
            $(this).val("");
            GetAll();
        }
    });

    $("#save").click(function() {
        var dob = $("#dob").val();
        var fv = $("#fVisit").val();
        var lv = $("#lVisit").val();
        var obj = {
            fullName: $("#name").val(),
            address: $("#address").val(),
            email: $("#email").val(),
            phoneNumber: $("#phone").val(),
            firstVisit: fv,
            lastVisit: lv,
            dateOfBirth: dob,
            debt: $("#debt").val(),
            complaints: $("#complaints").val(),
            lastTreatment: $("#lastTreatment").val(),
            lastDiagnosis: $("#lastDiagnosis").val(),
            finalDiagnosis: $("#finishDiagnosis").val(),
            anotherOpinion: $("#advice").val(),
            treatmentPlan: $("#treatment").val()
        };
        $.ajax({
            url: baseUrl + "person/edit/" + idPerson,
            method: "PUT",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            data: obj
        }).done(function(data) {
            alert("Зміни збережено");
        });
    });
    
    $("#sendImg").click(function() {
        var data = new FormData();
        jQuery.each(jQuery('#img')[0].files, function(i, file) {
            data.append('file-'+i, file);
        });
        data.append("id", $("#id").val());
        
        jQuery.ajax({
            url: baseUrl + 'addimages',
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            success: function(data){
                $("#galary").attr("href", data);
                alert("Зображення завантажено");
            }
        });
    });
    
    $("#addPacient").click(function () {
        var that = $(this);
        var name = $("#add #name");
        var address = $("#add #address");
        var email = $("#add #email");
        var phone = $("#add #phone");
        var dob = $("#add #dob");
        var dfv = $("#add #dfv");
        var dlv = $("#add #dlv");
        var obj = {
            fullName: name.val(),
            email: email.val(),
            phoneNumber: phone.val(),
            firstVisit: dfv.val() + "T",
            lastVisit: dlv.val() + "T",
            dateOfBirth: dob.val() + "T"
        };
        $.ajax({
            url: baseUrl + "person/create",
            method: "POST",
            headers: {
                Authorization: JSON.parse(localStorage.token).token
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Unauthorized(errorThrown);
            },
            data: obj,
            beforeSend: function () {
                that.html('<i class="fa fa-spinner fa-spin fa-2x fa-fw"></i><span class="sr-only">Loading...</span>');
            }
        }).done(function (data) {
            var str = "<tr><td>" + data.fullName + "</td><td>" + data.address + "</td><td>" + data.email + "</td><td>" + data.phoneNumber + '</td><td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-danger btn-xs" data-id="' + data.id + '"><span class="glyphicon glyphicon-trash"></span></button></p></td>' + '<td><button data-toggle="modal" data-target="#pacient" type="button" class="btn-info" data-id="' + data.id + '">Карточка</button></td></tr>';
            $("#table").append(str);
            name.val("");
            address.val("");
            email.val("");
            dob.val("");
            phone.val("");
            dfv.val("");
            dlv.val("");
            that.html("Add");
            $(".close").click();
        })
    });

    function ToDateString(value) {
        var date = new Date(value);
        var dateString = date.getFullYear() + "-";
        if(date.getMonth() < 10) {
            dateString += "0"+ date.getMonth() + "-";
        }
        else
            dateString += date.getMonth() + "-";
        if(date.getDate() < 10)
            dateString += "0" + date.getDate();
        else
            dateString += date.getDate();
        return dateString;
    }
    
    function GetAll() {
        $.ajax({
            url: baseUrl + "person/all",
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
        });
    }

    function BuildTable(data) {
        var count = data.length;
        $("#count").html(count);
        var str = "";
        for (var i = 0; i < count; i++) {
            str += "<tr><td>" + data[i].fullName + "</td><td>" + data[i].address + "</td><td>" + data[i].email + "</td><td>" + data[i].phoneNumber + '</td><td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-danger btn-xs" data-id="' + data[i].id + '"><span class="glyphicon glyphicon-trash"></span></button></p></td>' + '<td><button data-toggle="modal" data-target="#pacient" type="button" class="btn-info" data-id="' + data[i].id + '">Карточка</button></td></tr>';
            $("#table").html(str);
            DeleteNulls();
        }
    }
})