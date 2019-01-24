var years = {};
function mapOnClick(lat, lng) {
    $("#loading_placeholder").show();
    return api.get("RepairInfo/Get", {latitude: lat, longitude: lng})
        .then(function (data) {
            $("#loading_placeholder").hide();
            if (!data) {
                $("#sidebarContainer").hide();
                return;
            }
            $("#sidebarContainer").show();
            $(".left_child").hide();
            $("#repairProperties").show();
            var address = data.address;
            $("#title").text(address["Street"] + ", " + address["House"]);
            $("#subtitle").text(address["City"] + ", " + address["State"]);
            $("#postcode").text(address["Postcode"]);
            
            if (data.repair){
                $("#info_placeholder").hide();
                $("#info").show();
                var house = data.repair.House;
                var houseKeys = Object.keys(house);
                for (var i = 0; i < houseKeys.length; i++){
                    $("#" + houseKeys[i]).text(house[houseKeys[i]]);
                }
            } else{
                $("#info").hide();
                $("#info_placeholder").show();
            }
        });
}

function getRepairInfo(map, lat, lng) {
    markers.clearLayers();
    targetMarker && targetMarker.removeFrom(map);
    map.panTo(new L.LatLng(lat, lng));
    mapOnClick(lat,  lng).then(function() {
        if (!targetMarker) {
            targetMarker = new L.Marker([lat, lng]);
            targetMarker.addTo(map);
        } else {
            targetMarker.setLatLng([lat, lng]);
            targetMarker.addTo(map);
        }
    });
}

function selectLayer(number) {
    var keys = Object.keys(years);
    var layer = years[keys[number]];

    new Promise(function (resolve) {
        if (!layer) {
            $("#loading_placeholder").show();
            api.get("Layers/GetLayer", {years: keys[number]})
                .then(function (data) {
                    $("#loading_placeholder").hide();
                    years[keys[number]] = data;
                    resolve(data);
                })
        } else {
            resolve(layer);
        }
    })
        .then(function (coord) {
            markers.clearLayers();
            for (var k = 0; k < coord.length; k++) {
                var latLng = new L.LatLng(coord[k].Latitude, coord[k].Longitude);
                var m = new L.Marker(latLng);
                markersList.push(m);
                markers.addLayer(m);
            }
            return false;
        });
}

$( function() {
    var cache = {};
    $("#addressInput").autocomplete({
        minLength: 5,
        source: function( request, response ) {
            var term = request.term;
            if (term in cache) {
                response(cache[term]);
                return;
            }

            $.getJSON("Address/Autocomplete", request, function (data, status, xhr) {
                cache[term] = data;
                response(data);
            })
        },
        select: function( event, ui ) {
            $("#loading_placeholder").show();
            api.get("House/Search", { address: ui.item.value })
                .then(function (data) {
                    getRepairInfo(map, data.Latitude, data.Longitude);
                });
        }
    });
    
    api.get("Layers/GetYears", {})
        .then(function (data) {
            var $years = $("#years");
            for (var i = 0; i < data.length; i++){
                years[data[i]] = null;
                $years.append($("<li class='list_item active' onclick='selectLayer(" + i + ")'>" + data[i] + "</li>"));
            }
            // if (data.length > 0)
            //     selectLayer(0);
        });
    
    $("#layers_btn").on('click', function () {
        $("#sidebarContainer").show();
        $(".left_child").hide();
        $("#yearsInfo").show();
    });
} );