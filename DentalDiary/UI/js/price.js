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
    $("#upPrice").click(function () {
        var that = $(this);
        var price = {
            name: $("#editName").val(),
            price: $("#editPrice").val(),
            kindOfWork: $("#editGroup").val(),
            cityId: cityId
        };
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/pricelist/edit/" + upId,
            method: "PUT",
            data: price,
            beforeSend: function () {
                that.html('<i class="fa fa-spinner fa-spin fa-2x fa-fw"></i><span class="sr-only">Loading...</span>')
            }
        }).done(function(data) {
            BuildTable();
            that.html('<span class="glyphicon glyphicon-ok-sign"></span> Update');
            $(".close").click();
        });
    });
    
    $(document).on("click", ".editB", function() {
        var id = $(this).data("id");
        upId = id;
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/pricelist/get-price/" + id,
            method: "get",
            success: function (data) {
                $("#editName").val(data.name);
                $("#editPrice").val(data.price);
                $("#editGroup").val(data.kindOfWork);
            }
        });
    });
    
    function BuildTable(data) {
       $.ajax({
           url: "http://dentaldiary.gearhostpreview.com/pricelist/bycity/" + cityId,
           method: "GET",
           beforeSend: function () {
               $("#priceList").html('<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>');
           }
   }).done(function(data) {
       var str = "";
       for(var i=0; i< data.length; i++) {
           str+= "<tr><td>" + data[i].id + "</td><td>" + data[i].name + "</td><td>" + data[i].price + "</td><td>" + data[i].kindOfWork + '</td><td><p data-placement="top" data-toggle="tooltip"><button class="btn btn-primary btn-xs editB" data-id="'+ data[i].id + '" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button></p></td><td><p data-placement="top" data-toggle="tooltip"><button data-id="' + data[i].id + '" class="btn btn-danger btn-xs delD"><span class="glyphicon glyphicon-trash"></span></button></p></td></tr>';
       }
       $('#priceList').html(str);
   });
    }
});