function initMap(dotNetHelper, latitude, longitude) {
    console.log("initMap called");
    ymaps.ready(function () {
        var map = new ymaps.Map('map', {
            center: [latitude, longitude],
            zoom: 10
        });

        var placemark = new ymaps.Placemark([latitude, longitude], {});
        map.geoObjects.add(placemark);

        // If you need to call the SetMapCenter method from C#:
        //dotNetHelper.invokeMethodAsync('SetMapCenter', latitude, longitude);
    });
}
