$(function () {  // wait for when document is loaded. Use $(function(){});

    var mapOptions = {
        center: new google.maps.LatLng(55.756414, 12.446271),
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(document.getElementById('map'), mapOptions);

    var markerOptions = {
        position: new google.maps.LatLng(55.756414, 12.446271),
        map: map
    };
    var marker = new google.maps.Marker(markerOptions);
    marker.setMap(map);

    var infoWindowOptions = {
        content: 'The Company is placed Here!'
    };

    var infoWindow = new google.maps.InfoWindow(infoWindowOptions);
    google.maps.event.addListener(marker, 'click', function (e) {

        infoWindow.open(map, marker);

    });

});