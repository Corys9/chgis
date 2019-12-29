var CH = (function (o) {
    o.GeoServerURL = "http://localhost:20001/geoserver/ows";
    o.SharedFragment = "?service=WFS&version=1.1.0&request=GetFeature&outputFormat=json&srsName=EPSG:4326";
    o.CantonsURL = o.GeoServerURL + o.SharedFragment + "&typeName=ch-cantons";
    o.PlacesURL = o.GeoServerURL + o.SharedFragment + "&typeName=ch-places";
    o.ApiURL = "https://localhost:44388/";
    o.GetCantonVotesURL = o.ApiURL + "votes/get-canton-votes";
    o.Map = null;
    o.CurrentLawID = 1;
    o.Votes = null;
    o.Cantons = [];

    o.Init = function () {
        o.Map = L.map("map")
            .setView([46.6, 8.2], 8);

        var getVotes = $.ajax({
            url: o.GetCantonVotesURL,
            data: o.CurrentLawID,
            success: function (data) {
                o.Votes = data;
            },
            error: function (err) {
                console.error(err);
            }
        });

        var getCantons = $.ajax({
            url: o.CantonsURL,
            success: function (data) {
                o.Cantons = data;
            },
            error: function (err) {
                console.error(err);
            }
        });

        $.when(getVotes, getCantons).done(function () {
            var geoJsonLayer = new L.GeoJSON(o.Cantons.features, {
                onEachFeature: function (feature, layer) {
                    if (feature.properties && feature.properties.name_1)
                        layer.bindTooltip(o.getTooltipHtml(feature), { closeButton: false });
                },
                style: function (feature) {
                    var color = "black";

                    if (o.Votes === null)
                        return { color: color };

                    var votes = o.Votes[feature.properties.id_1];
                    if (!votes)
                        return { color: color };

                    
                    if (votes.for > votes.against)
                        color = "rgb(0, 50, 0)";
                    else if (votes.against > votes.for)
                        color = "rgb(50, 0, 0)";

                    return { color: color };
                }
            })
                .addTo(o.Map);
        });

        /*$.get(o.PlacesURL, function (data) {
            var geoJsonLayer = new L.GeoJSON(data.features)
                .addTo(o.Map);
        });*/
    };

    // PRIVATE

    o.getTooltipHtml = function (feature) {
        var html = "<div class=\"canton-name\">" + feature.properties.name_1 + "</div>";

        var votes = o.Votes[feature.properties.id_1];
        if (votes) {
            html += "<div class=\"votes-for\">For: " + votes.for + "</div>";
            html += "<div class=\"votes-against\">Against: " + votes.against + "</div>";
            html += "<div>" + (votes.for / (votes.for + votes.against) * 100).toFixed(2) + "% in favor.";
        }

        return html;
    };

    return o;
})(CH || {});