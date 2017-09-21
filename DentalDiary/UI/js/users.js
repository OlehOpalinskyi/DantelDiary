$(function() {
    loadCity();
    BuildTable();
    
    $(document).on("click", ".btn-danger.btn-xs", function() {
        var id = $(this).data("id");
        var that = $(this);
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/person/delete/" + id,
            method: "DELETE"
        }).done(function(data) {
            that.closest("tr").remove();
        });
    });
    var idPerson;
    $(document).on("click", ".btn-info", function() {
            var id = $(this).data("id");
            $("#id").val(id);
            idPerson = id;
            $.ajax({
                url: "http://dentaldiary.gearhostpreview.com/person/" + id,
                method: "get"
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
            url: "http://dentaldiary.gearhostpreview.com/person/get-receptions/" + id,
            method: "get"
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
            url: "http://dentaldiary.gearhostpreview.com/person/edit/" + idPerson,
            method: "PUT",
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
            url: 'http://dentaldiary.gearhostpreview.com/addimages',
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function(data){
                $("#galary").attr("href", data);
                alert("Зображення завантажено");
            }
        });
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
    
    function BuildTable() {
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/person/all",
            method: "GET"
        }).done(function(data) {
            var count = data.length;
            $("#count").html(count);
            var str = "";
            for(var i=0; i<count; i++) {
                str += "<tr><td>" + data[i].fullName + "</td><td>" + data[i].address + "</td><td>" + data[i].email + "</td><td>" + data[i].phoneNumber + '</td><td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-danger btn-xs" data-id="'+ data[i].id + '"><span class="glyphicon glyphicon-trash"></span></button></p></td>' + '<td><button data-toggle="modal" data-target="#pacient" type="button" class="btn-info" data-id="'+ data[i].id + '">Карточка</button></td></tr>';
                $("#table").html(str);
            }
        });
    }
})