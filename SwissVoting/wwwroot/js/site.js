var CH = (function (o) {
    o.GeoServerURL = "http://localhost:20001/geoserver/ows";
    o.SharedFragment = "?service=WFS&version=1.1.0&request=GetFeature&outputFormat=json&srsName=EPSG:4326";
    o.CantonsURL = o.GeoServerURL + o.SharedFragment + "&typeName=ch-cantons";
    o.PlacesURL = o.GeoServerURL + o.SharedFragment + "&typeName=ch-places";
    o.ApiURL = "https://localhost:44388/";
    o.GetCantonVotesURL = o.ApiURL + "votes/by-canton";
    o.GetCustomVotesURL = o.ApiURL + "votes/custom";
    o.Map = null;
    o.CurrentLawID = 0;
    o.Votes = null;
    o.Cantons = [];
    o.CantonLayer = null;

    o.Init = function () {
        // Leaflet config
        o.Map = L.map("map", {
            center: new L.LatLng(46.6, 8.2),
            zoom: 8,
            loadingControl: true
        });

        // Leaflet.Draw config
        var drawLayers = new L.FeatureGroup();
        o.Map.addLayer(drawLayers);
        var drawOptions = {
            position: "topright",
            draw: {
                polygon: {
                    allowIntersection: false
                }
            },
            edit: {
                featureGroup: drawLayers,
                remove: true
            }
        };

        var drawControl = new L.Control.Draw(drawOptions);
        o.Map.addControl(drawControl);

        o.Map.on(L.Draw.Event.CREATED, function (e) {
            drawLayers.addLayer(e.layer);
        });

        o.Map.on('draw:created', function (e) {
            if (e.layerType !== "polygon")
                return;

            o.colorPolygon(e.layer);
        });

        o.Map.on('draw:edited', function (e) {
            var layers = e.layers;
            layers.eachLayer(function (layer) {
                if (!(layer instanceof L.Polygon))
                    return;

                o.colorPolygon(layer);
            });
        });

        // select first law in the list
        var firstLawID = $(".law")[0].id.substring(4);
        o.SelectLaw(firstLawID);
    };

    o.SelectLaw = function (lawID) {
        if (o.CurrentLawID && o.CurrentLawID !== lawID)
            $("#law-" + o.CurrentLawID).removeClass("active");

        o.CurrentLawID = lawID;
        $("#law-" + lawID).addClass("active");

        o.refreshMap();
    };

    // PRIVATE

    o.refreshMap = function () {
        var getVotes = $.ajax({
            url: o.GetCantonVotesURL + "/" + o.CurrentLawID,
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
            var newLayer = new L.GeoJSON(o.Cantons.features, {
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

                    var percentage = votes.for / (votes.for + votes.against) * 100;

                    if (votes.for > votes.against)
                        color = "hsl(100, " + (100 - percentage) + "%, " + (100 - percentage) + "%)";
                    else if (votes.against > votes.for)
                        color = "hsl(0, " + percentage + "%, " + percentage + "%)";

                    return { color: color };
                }
            });

            if (o.CantonLayer)
                o.Map.removeLayer(o.CantonLayer);

            newLayer.addTo(o.Map);
            o.CantonLayer = newLayer;
        });
    };

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

    o.colorPolygon = function (layer) {
        var polystring = "";
        for (var i = 0; i < layer._latlngs[0].length; ++i)
            polystring += layer._latlngs[0][i].lng + " " + layer._latlngs[0][i].lat + ",";
        polystring += layer._latlngs[0][0].lng + " " + layer._latlngs[0][0].lat;

        console.log(layer);

        $.ajax({
            url: o.GetCustomVotesURL + "/" + o.CurrentLawID + "?polystring=" + polystring,
            contentType: "application/json",
            dataType: "json",
            success: function (votes) {
                var color = "yellow";
                var percentage = votes.for / (votes.for + votes.against) * 100;

                if (votes.for > votes.against)
                    color = "hsl(100, " + (100 - percentage) + "%, " + (100 - percentage) + "%)";
                else if (votes.against > votes.for)
                    color = "hsl(0, " + percentage + "%, " + percentage + "%)";

                layer.setStyle({ color: color, opacity: 0.75 });
            },
            error: function (err) {
                console.error(err);
            }
        });
    };

    return o;
})(CH || {});