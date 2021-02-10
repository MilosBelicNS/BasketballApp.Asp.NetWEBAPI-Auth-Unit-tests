$(document).ready(function () {
    var host = "https://" + window.location.host;
    var token = null;
    var headers = {};
    var playersEndpoint = "/api/players";
    var clubsEndpoint = "/api/clubs";



    //pripremanje dogadjaja
    $("body").on("click", "#btnEdit", editPlayers);
    $("body").on("click", "#btnDelete", deletePlayers);





    loadPlayers();

    //funkcija ucitavanja igraca
    function loadPlayers() {
        var requestUrl = host + playersEndpoint;
        $.getJSON(requestUrl, setPlayers);
    }

    //popunjavanje tabele igraca
    function setPlayers(data, status) {

        var $container = $("#data");
        $container.empty();

        if (status === "success") {
            // ispis naslova
            var div = $("<div></div>");
            var h1 = $("<h1>Players</h1>");
            div.append(h1);
            // ispis tabele
            var table = $("<table class='table table-bordered'></table>");
            var header;
            if (token) {
                header = $("<thead><tr style='background-color:yellow'><td>Name</td><td>Born</td><td>Club</td><td>Matches</td><td>Points avg</td><td>Option</td><td>Option</td></tr></thead>");
            } else {
                header = $("<thead><tr style='background-color:yellow'><td>Name</td><td>Born</td><td>Club</td><td>Matches</td><td>Points avg</td></tr></thead>");
            }

            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].Name + "</td><td>" + data[i].Born + "</td><td>" + data[i].Club.Name + "</td><td>" + data[i].Matches + "</td><td>" + data[i].PointsAverage + "</td>";

                // prikaz dugmadi za izmenu i brisanje
                var stringId = data[i].Id.toString();
                var displayEdit = "<td><button class='btn btn-warning' id=btnEdit name=" + stringId + ">Edit</button></td>";
                var displayDelete = "<td><button class='btn btn-danger' id=btnDelete name=" + stringId + ">Delete</button></td>";

                // prikaz samo ako je korisnik prijavljen
                if (token) {
                    row += displayData + displayEdit + displayDelete + "</tr>";//prikazujemo display sa tokenom jer je tu puna tabela
                } else {
                    row += displayData + "</tr>";//displej sa osnovnim podacima koji nema token
                }
                // dodati red
                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);

            $container.append(div);
        }
    }
    //kad se pritisne dugme registracija i prijava
    $("#regLogButt").click(function () {
        console.log(data);
        $("#info").empty().append("Registration and login");
        $("#regForm").css("display", "block");
        $("#regLogButt").css("display", "none");
        $("#beginButt").css("display", "block");



    });
    


    // registracija korisnika
    $("#regForm").submit(function (e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var pass1 = $("#regPass").val();
        var pass2 = $("#regPass1").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": pass1,
            "ConfirmPassword": pass2
        };

        $.ajax({
            type: "POST",
            url: host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#regForm").css("display", "none");
            $("#logForm").css("display", "block");
            $("#regEmail").val("");
            $("#regPass").val("");
            $("#regPass1").val("");

        }).fail(function (data) {
            alert("Error!");
        });

    });
    //na klik dugmeta log u okviru forme registracije
    $("#logButt").click(function (e) {
        e.preventDefault();
        $("#info").empty().append("Registration and login");
        $("#regForm").css("display", "none");
        $("#regLogButt").css("display", "none");
        $("#beginButt").css("display", "block");
        $("#logForm").css("display", "block");

        
    });

    // prijava korisnika
    $("#logForm").submit(function (e) {
        e.preventDefault();

        var email = $("#logEmail").val();
        var pass = $("#logPass").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": pass
        };

        $.ajax({
            "type": "POST",
            "url": host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
         
            $("#info").empty().append("User: " + data.userName);
            token = data.access_token;
            $("#searchPlayer").css("display", "block");
            $("#data").attr("class", "col-sm-8");
            $("#beginButt").css("display", "none");
            $("#logForm").css("display", "none");
            $("#logOutButt").css("display", "block");
            $("#refresh").css("display", "block");
            $("#logEmail").val("");
            $("#logPass").val("");
           
            
            loadPlayers();

        }).fail(function (data) {
            alert("Error!");
        });
    });
    //na klik dugmeta registration u okviru forme prijave
    $("#regButt").click(function (e) {
        e.preventDefault();
        $("#info").empty().append("Registration and login");
        $("#regForm").css("display", "block");
        $("#regLogButt").css("display", "none");
        $("#beginButt").css("display", "block");
        $("#logForm").css("display", "none");
        
    });

    // odjava korisnika sa sistema
    $("#logOutButt").click(function () {
        token = null;
        headers = {};

        location.reload();
    });

    //reload tabele
    $("#refresh").click(function (e) {
        e.preventDefault();
        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        loadPlayers();


    });
    //na klik dugmeta begining
    $("#beginButt").click(function (e) {
        e.preventDefault();
        location.reload();

    });

    //brisanje 
    function deletePlayers() {

        var deleteID = this.name;

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        // saljemo zahtev 
        $.ajax({
            url: host + playersEndpoint + "/" + deleteID.toString(),
            type: "DELETE",
            headers: headers
        })
            .done(function (data, status) {
                loadPlayers();
            })
            .fail(function (data, status) {
                alert("Error!");
            });
    }


    //popunjavanje selekta za edit
    $.ajax({
        "type": "GET",
        "url": host + clubsEndpoint
    }).done(function (data, status) {
        var select = $("#clubEdit");
        for (var i = 0; i < data.length; i++) {
            var option = "<option value='" + data[i].Id + "'>" + data[i].Name + "</option>";
            select.append(option);
        }
    });

    //edit 
    function editPlayers() {
        $("#editForm").css("display", "block");
        editId = this.name;

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        $.ajax({
            "type": "GET",
            "url": host + playersEndpoint + "/" + editId,
            "headers": headers
        }).done(function (data, status) {

            //console.log(data);

            $("#nameEdit").val(data.Name);
            $("#bornEdit").val(data.Born);
            $("#clubEdit").val(data.ClubId);
            $("#matchesEdit").val(data.Matches);
            $("#pointEdit").val(data.PointsAverage);


        });
    }

    //izmena 
    $("#editForm").submit(function (e) {
        e.preventDefault();

        var name = $("#nameEdit").val();
        var born = $("#bornEdit").val();
        var club = $("#clubEdit").val();
        var matches = $("#matchesEdit").val();
        var points = $("#pointEdit").val();


        if (token) {
            headers.Authorization = "Bearer " + token;
        }


        var sendData = {
            "Id": editId,
            "Name": name,
            "Born": born,
            "ClubId": club,
            "Matches": matches,
            "PointsAverage": points

        };

        $.ajax({
            "type": "PUT",
            "url": host + playersEndpoint + "/" + editId,
            "data": sendData,
            "headers": headers
        }).done(function () {
            loadPlayers();
            $("#editForm").css("display", "none");
        }).fail(function () { alert("Error!"); });



    });

    //pretraga
    $("#searchPlayer").submit(function (e) {
        e.preventDefault();


        var from = $("#from").val();
        var to = $("#to").val();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var sendData = {
            "Min": from,
            "Max": to
        };

        $.ajax({

            url: host + "/api/search",
            type: "POST",
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            $("#from").val("");
            $("#to").val("");

            setPlayers(data, status);
        }).fail(function (data, status) { alert("Error"); });
    });

    //na klik dugmeta back u okviru edita
    $("#back").click(function (e) {
        e.preventDefault();
        $("#editForm").css("display", "none");

    });

    



});