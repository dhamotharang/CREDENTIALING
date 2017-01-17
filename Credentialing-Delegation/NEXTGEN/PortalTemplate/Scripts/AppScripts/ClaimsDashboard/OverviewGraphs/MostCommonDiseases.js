$(function () {
    function initMap() {
        var mapDiv = document.getElementById('ICDReport');
        var map = new google.maps.Map(mapDiv, {
            center: { lat: 27.6648, lng: -81.5158 },
            zoom: 6
        });

        var marker1 = new google.maps.Marker({
            position: { lat: 26.6648, lng: -81.5158 },
            map: map,
            title: '327.0 - Organic insomnia'
        });
        var marker2 = new google.maps.Marker({
            position: { lat: 25.6648, lng: -80.98956 },
            map: map,
            title: '743.45 - Aniridia'
        });
        var marker3 = new google.maps.Marker({
            position: { lat: 25.6648, lng: -80.5158 },
            map: map,
            title: '813 - Fracture of radius and ulna'
        });
        var marker4 = new google.maps.Marker({
            position: { lat: 27.2223, lng: -80.5158 },
            map: map,
            title: '398 - Other rheumatic heart disease'
        });
        var marker5 = new google.maps.Marker({
            position: { lat: 27.2422, lng: -80.5158 },
            map: map,
            title: '398 - Other rheumatic heart disease'
        });
    }

    initMap();
})