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

var myMap;

function setPoint(dotNetHelper) {
    ymaps.ready(function () {
        if (!myMap) {
            myMap = new ymaps.Map('map', {
                center: [55.751574, 37.573856],
                zoom: 10
            });
        }

        myMap.events.add('click', function (e) {
            var coords = e.get('coords');

            if (myMap.geoObjects.getLength() > 0) {
                myMap.geoObjects.removeAll();
            }

            var placemark = new ymaps.Placemark(coords, {}, {
                hintContent: 'Координаты',
                balloonContent: 'Широта: ' + coords[0] + ', Долгота: ' + coords[1]
            });

            myMap.geoObjects.add(placemark);

            var coordinates = [placemark.geometry.getCoordinates()[0], placemark.geometry.getCoordinates()[1]];
            dotNetHelper.invokeMethodAsync('SetCoordinates', placemark.geometry.getCoordinates()[0], placemark.geometry.getCoordinates()[1]);
        });
    });
}











