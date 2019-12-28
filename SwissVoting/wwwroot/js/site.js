var CH = (function (o) {
    o.BaseURL = "http://localhost:20001/geoserver/ows";
    o.SharedFragment = "?service=WFS&version=1.1.0&request=GetFeature&outputFormat=json&srsName=EPSG:4326";
    o.CantonsURL = o.BaseURL + o.SharedFragment + "&typeName=ch-cantons";
    o.PlacesURL = o.BaseURL + o.SharedFragment + "&typeName=ch-places";
    o.Map = null;

    o.Init = function () {
        o.Map = L.map("map")
            .setView([46.6, 8.2], 8);

        $.get(o.CantonsURL, function (data) {
            var geoJsonLayer = new L.GeoJSON(data.features, {
                onEachFeature: function (feature, layer) {
                    if (feature.properties && feature.properties.name_1)
                        layer.bindTooltip("<div class=\"canton-name\">" + feature.properties.name_1 + "</div>", { closeButton: false });
                }
            })
                .addTo(o.Map);
        });

        /*$.get(o.PlacesURL, function (data) {
            var geoJsonLayer = new L.GeoJSON(data.features)
                .addTo(o.Map);
        });*/
    };

    return o;
})(CH || {});