$(function() {
    loadCity();
    var cityId = localStorage.getItem("city");
    BuildTable();
    
    $('#createPrice').click(function() {
        var nameP = $("#name");
        var priceP = $("#price");
        var groupP = $("#group");
        var price = {
            name: nameP.val(),
            price: priceP.val(),
            kindOfWork: groupP.val(),
            cityId: cityId
        };
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/pricelist/create",
            method: "POST",
            data: price
        }).done(function(data) {
            nameP.val("");
            priceP.val("");
            groupP.val("");
            var record = "<tr><td>" + data.id + "</td><td>" + data.name + "</td><td>" + data.price + "</td><td>" + data.kindOfWork + '</td><td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-primary btn-xs editB" data-id="'+ data.id + '" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button></p></td><td><p data-placement="top" data-toggle="tooltip"><button data-id="' + data.id + '" class="btn btn-danger btn-xs delD"><span class="glyphicon glyphicon-trash"></span></button></p></td></tr>';
            $('#priceList').append(record);
        });
    });
    
    $(document).on("click", ".delD", function() {
        var id = $(this).data("id");
        var that = $(this);
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/pricelist/delete/" + id,
            method: "DELETE"
        }).done(function(data) {
           that.closest("tr").remove();
        });
    });
    
    var upId;
    $("#upPrice").click(function() {
        var price = {
            name: $("#editName").val(),
            price: $("#editPrice").val(),
            kindOfWork: $("#editGroup").val(),
            cityId: cityId
        };
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/pricelist/edit/" + upId,
            method: "PUT",
            data: price
        }).done(function(data) {
            BuildTable();
           // $('#edit').hide();
        });
    });
    
    $(document).on("click", ".editB", function() {
        var id = $(this).data("id");
        upId = id;        
    });
    
    function BuildTable(data) {
       $.ajax({
           url: "http://dentaldiary.gearhostpreview.com/pricelist/bycity/" + cityId,
       method: "GET"
   }).done(function(data) {
       var str = "";
       for(var i=0; i< data.length; i++) {
           str+= "<tr><td>" + data[i].id + "</td><td>" + data[i].name + "</td><td>" + data[i].price + "</td><td>" + data[i].kindOfWork + '</td><td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-primary btn-xs editB" data-id="'+ data[i].id + '" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button></p></td><td><p data-placement="top" data-toggle="tooltip"><button data-id="' + data[i].id + '" class="btn btn-danger btn-xs delD"><span class="glyphicon glyphicon-trash"></span></button></p></td></tr>';
       }
       $('#priceList').html(str);
   });
    }
});