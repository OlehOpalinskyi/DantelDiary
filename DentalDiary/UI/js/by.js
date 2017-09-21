$(function() {
    var cityId = localStorage.getItem("city");
    loadCity();
    GetAll();
    
     $(document).on("click", ".btn-danger.btn-xs", function() {
        var id = $(this).data("id");
        var that = $(this);
        $.ajax({
            url: "http://localhost:50612/receptions/delete/" + id,
            method: "DELETE"
        }).done(function(data) {
            that.closest("tr").remove();
        });
    });
    
    var id;
    $(document).on("click", ".btn-primary.btn-xs", function() {
        id = $(this).data("id");
    });
    
    $("#editRecivier").click(function() {
        var recivier = $("#recivier").val();
        $.ajax({
            url: "http://localhost:50612/receptions/edit-recivier/" + id + "?recivier=" + recivier,
            method: "PUT"
        }).done(function(data) {
            console.log(data);
            BuildTable();
            $("#close").click();
        });
    });
    
    $("#myInput").keypress(function(e) {
        if(e.which == 13) {
            $.ajax({
                url: "http://localhost:50612/receptions/search-by-recivier/" + cityId,
                method: "GET",
                data:{customer: $(this).val()},
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
    
    function GetAll() {
        $.ajax({
        url: "http://localhost:50612/receptions/bycity/" + cityId,
        method: "GET"
    }).done(function(data) {
        BuildTable(data)
    });
    }
    
    function BuildTable(data) {
        var str = "";
        for(var i=0; i<data.length; i++) {
            var dateTime = new Date(data[i].date);
            var date = dateTime.toLocaleDateString();
            str += "<tr><td>" + data[i].recivier + "</td><td>" + data[i].customer +"</td><td>" + data[i].priceName + "</td><td>" + data[i].kindOfWork + "</td><td>" + data[i].priceCount + "</td><td>" + data[i].payment + "</td><td>" + date +"</td>" + 
                '<td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-primary btn-xs" data-toggle="modal" data-target="#edit" data-id="'+data[i].id + '"><span class="glyphicon glyphicon-pencil"></span></button></p></td>' + '<td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-danger btn-xs" data-id="'+data[i].id + '"><span class="glyphicon glyphicon-trash"></span></button></p></td></tr>'
            }
        $("#table").html(str);
    }
})