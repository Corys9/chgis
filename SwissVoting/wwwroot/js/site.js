var CH = (function (o) {
    o.GeoServerURL = "http://localhost:20001/geoserver/ows";
    o.SharedFragment = "?service=WFS&version=1.1.0&request=GetFeature&outputFormat=json&srsName=EPSG:4326";
    o.CantonsURL = o.GeoServerURL + o.SharedFragment + "&typeName=ch-cantons";
    o.PlacesURL = o.GeoServerURL + o.SharedFragment + "&typeName=ch-places";
    o.ApiURL = "https://localhost:44388/";
    o.GetAllVotesURL = o.ApiURL + "votes/get-all-votes";
    o.Map = null;
    o.CurrentLawID = 1;
    o.Votes = [];
    o.Cantons = [];

    o.Init = function () {
        o.Map = L.map("map")
            .setView([46.6, 8.2], 8);

        var getVotes = $.ajax({
            url: o.GetAllVotesURL,
            data: o.CurrentLawID,
            success: function (data) {
                o.Votes = data;
            }
        });

        var getCantons = $.ajax({
            url: o.CantonsURL,
            success: function (data) {
                o.Cantons = data;
            }
        });

        $.when(getVotes, getCantons).done(function () {
            var geoJsonLayer = new L.GeoJSON(o.Cantons.features, {
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