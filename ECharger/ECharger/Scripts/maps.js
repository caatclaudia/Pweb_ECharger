var map, service, infoWindow;
var chargingStationsMarkers = [];

function initAutocomplete() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: {lat: 40.20564, lng: -8.4195},
        zoom: 16,
        mapTypeId: 'roadmap'
    });

    infowindow = new google.maps.InfoWindow({
        content: " "
    });

    geolocate();

    // Create the search box and link it to the UI element.
    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function() {
        searchBox.setBounds(map.getBounds());
    });

    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function() {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        clearChargingStationsMarkers();
        markers.forEach(function(marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function(place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location,
                animation: google.maps.Animation.DROP
            }));

            getChargingStations(place.geometry.location.lat(), place.geometry.location.lng());

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
    });
}

function geolocate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var geolocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            getChargingStations(position.coords.latitude, position.coords.longitude);
            map.setCenter(geolocation);
        });
    }
}

function getChargingStations(lat, lng) {
    var url = "/api/chargingStations/";
    url += parseInt(lat) + "/" + parseInt(lng);
    $.getJSON(url,
        function (chargingStations) {

            var geolocation = {
                lat: chargingStations[0].latitude,
                lng: chargingStations[0].longitude
            };
            map.setCenter(geolocation);
            map.setZoom(16);

            var i = 0;
            $.each(chargingStations, function (key, chargingStation) {
                createCharginStationMarker(chargingStation, i * 200);
                i++;
            })
        });
}

function createCharginStationMarker(chargingStation, timeout) {

    var myLatLng = {
        lat: chargingStation.latitude,
        lng: chargingStation.longitude
    };

    window.setTimeout(function() {
        var marker = new google.maps.Marker({
            map: map,
            position: myLatLng,
            animation: google.maps.Animation.DROP,
            icon: 'http://maps.google.com/mapfiles/ms/icons/green-dot.png'
        });

        google.maps.event.addListener(marker, 'click', function () {
            var content = '<p><b>Station: </b>' + chargingStation.name + '</p>';
            content += '<p><b>Street: </b>' + chargingStation.streetName + '</p>';
            content += '<p><b>City: </b>' + chargingStation.city + '</p>';
            content += '<p><b>Operator: </b>' + chargingStation.operator + '</p>';
            content += '<p><b>Price per minute: </b>' + roundToTwo(chargingStation.pricePerMinute) + '</p>';
            content += '<a onclick="reserveChargingStation(' + chargingStation.id + ')" class="btn btn-primary js-reservation" > Reserve Now</a> ';
            infowindow.setContent(content);

            infowindow.open(map, this);
        });

        chargingStationsMarkers.push(marker);
    }, timeout);
}

function reserveChargingStation(chargingStationID) {
    window.location.href = '/Reservations/MapCreate/' + chargingStationID;
}

function clearChargingStationsMarkers() {
    for (var i = 0; i < chargingStationsMarkers.length; i++) {
        chargingStationsMarkers[i].setMap(null);
    }
    chargingStationsMarkers = [];
}

function roundToTwo(num) {
    return +(Math.round(num + "e+2") + "e-2");
}