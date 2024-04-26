function initMap(dotNetHelper, latitude, longitude) {
    console.log("initMap called");
    ymaps.ready(function () {
        var map = new ymaps.Map('map', {
            center: [latitude, longitude],
            zoom: 15
        });

        var placemark = new ymaps.Placemark([latitude, longitude], {
            hintContent: 'Координаты',
            balloonContent: 'Широта: ' + latitude + ', Долгота: ' + longitude
        });
        map.geoObjects.add(placemark);

        ymaps.geocode([latitude, longitude]).then(function (res) {
            var address = res.geoObjects.get(0).getAddressLine();
            var city = address.split(',')[0].trim();

            placemark.properties.set({
                hintContent: 'Координаты',
                balloonContent: 'Город: ' + city + '<br>Адрес: ' + address + '<br>Широта: ' + latitude + ', Долгота: ' + longitude
            });
        });

        var routeButtonControl = new ymaps.control.RouteButton({
            options: {
                size: "large",
                text: "Проложить маршрут"
            }
        });

        map.controls.add(routeButtonControl);

        var backToPointButton = new ymaps.control.Button({
            data: {
                content: "Вернуться к точке"
            },
            options: {
                selectOnClick: true
            }
        });

        backToPointButton.events.add('click', function () {
            map.setCenter([latitude, longitude]);
        });

        map.controls.add(backToPointButton);

        routeButtonControl.events.add('click', function () {
            routeButtonControl.routePanel.state.set('to', [latitude, longitude]);
        });
    });
}
