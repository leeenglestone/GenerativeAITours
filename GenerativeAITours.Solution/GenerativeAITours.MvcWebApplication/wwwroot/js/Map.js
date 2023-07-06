﻿
const Map = {

    PlotLocations: function (json) {
        
        //let tileServer1 = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
        let tileServer2 = 'https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png';

        var tiles = L.tileLayer(tileServer2, {
            maxZoom: 18,
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        });

        var location = this.GetLocationByLocationName(json.location);

        console.log(location);

        var latlng = new L.LatLng(location.latitude, location.longitude);

        if (map != undefined) { map.remove(); }

        var map = new L.Map('map', { center: latlng, zoom: 13, layers: [tiles] });

        // Add markers for each activity
        json.days.forEach(function (day) {
            // Iterate through each activity
            day.activities.forEach(function (activity) {

                var divIcon = L.divIcon({
                    className: 'custom-marker',
                    html: '<div class="marker-circle">' + activity.number + '</div>',
                    iconSize: [30, 30]
                });

                // Create a marker with the div icon for each activity
                var marker = L.marker([activity.latitude, activity.longitude], { icon: divIcon }).addTo(map);

                // Create a marker for each activity
                //var marker = L.marker([activity.latitude, activity.longitude]).addTo(map);

                // Add a popup with activity details to the marker
                marker.bindPopup("<b>" + activity.name + "</b><br>" + activity.description);
            });
        });


    },
    GetLocationByLocationName: function (locationName) {

        const locations = allLocations();

        for (let i = 0; i < locations.length; i++) {
            if (locations[i].name.toLowerCase() === locationName.toLowerCase()) {
                return {
                    latitude: locations[i].latitude,
                    longitude: locations[i].longitude
                };
            }
        }

        return null; // City not found

    }

};