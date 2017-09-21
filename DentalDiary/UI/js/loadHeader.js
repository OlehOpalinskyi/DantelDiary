function loadCity() {
        $.ajax({
            url: "http://dentaldiary.gearhostpreview.com/city/all",
            method: "GET"
        }).done(function(data) {
            var cities = "";
            for(var i=0; i<data.length; i++) {
                cities+= "<li><a href='#' data-id='" + data[i].id + "'>" + data[i].name + "</a></li>";
            }
            $('#cities').append(cities);
            var id = localStorage.getItem("city");
            if(id == null) {
                 $('#cities li').eq(0).addClass('active');
                localStorage.setItem("city", data[0].id);
            }
           else {
               $("[data-id='"+ id +"']").parent().addClass('active');
           }
        });
    }