var map, service, infoWindow;

function initAutocomplete() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: {lat: 40.20564, lng: -8.4195},
        zoom: 16,
        mapTypeId: 'roadmap'
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
                position: place.geometry.location
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
            map.setCenter(geolocation);
        });
    }
}

function getChargingStations(lat, lng){
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            var chargingStationsJSON = JSON.parse(this.responseText);
            var geolocation = {
                lat: chargingStationsJSON[0].AddressInfo.Latitude,
                lng: chargingStationsJSON[0].AddressInfo.Longitude
            };
            map.setCenter(geolocation);
            map.setZoom(16);
            chargingStationsJSON.forEach(function(chargingStation) {
                for (var i = 0; i < 100; i++){
                    createMarker(chargingStation.AddressInfo.Latitude, chargingStation.AddressInfo.Longitude);
                }
            });
        }
    };
    var url = "https://api.openchargemap.io/v3/poi/?output=json&countrycode=PT&maxresults=100";
    url += "&latitude=" + lat + "&longitude=" + lng;
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

function createMarker(lat, lng) {
    var myLatLng = {lat: lat, lng: lng};
    
    var marker = new google.maps.Marker({
        map: map,
        position: myLatLng
    });
}