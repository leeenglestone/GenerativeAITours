
const Map = {

    PlotLocations: function (location) {
        
        //let tileServer1 = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
        let tileServer2 = 'https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}.png';

        var tiles = L.tileLayer(tileServer2, {
            maxZoom: 18,
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        });

        var location = this.GetLocationByLocationName(_location);
        var latlng = new L.LatLng(location.latitude, location.longitude);

        if (map != undefined) { map.remove(); }

        var map = new L.Map('map', { center: latlng, zoom: 12, layers: [tiles] });

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