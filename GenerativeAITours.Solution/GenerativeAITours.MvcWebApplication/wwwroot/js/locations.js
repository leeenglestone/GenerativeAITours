function allLocations() {
    return [

        // Americas
        new Location("Atlanta", 33.7490, -84.3880),
        new Location("Boston", 42.3601, -71.0589),
        new Location("Branson", 36.6437, -93.2185),
        new Location("Columbus", 39.96, -82.99),
        new Location("Charlotte", 35.2271, -80.8431),
        new Location("Chicago", 41.8781, -87.6298),
        new Location("Dallas", 32.77, -96.79),
        new Location("Detroit", 42.3314, -83.0458),
        new Location("Kansas City", 39.09, -94.57),
        new Location("Las Vegas", 36.1716, -115.1391),
        new Location("Los Angeles", 34.05, -118.24),
        //new City("",0, 0),		
        new Location("Minneapolis", 44.97, -93.26),
        new Location("New York", 40.7128, -74.0060),
        new Location("Raleigh", 35.7796, -78.6382),

        new Location("San Francisco", 37.7749, -122.4194),
        new Location("Seattle", 47.6062, -122.33),
        new Location("Vancouver", 49.2827, -123.1207),

        //new City("",0, 0),
        //new City("",0, 0),

        // Europe
        new Location("Amsterdam", 52.3676, 4.9041),
        new Location("Baden", 48.76, 8.22),
        new Location("Barcelona", 41.3874, 2.1686),
        new Location("Belfast", 54.5973, -5.9301),
        new Location("Berlin", 52.5200, 13.4050),
        new Location("Birmingham", 52.4862, -1.8904),
        new Location("Brighton", 50.8229, -0.1363),
        new Location("Bristol", 51.4545, -2.5879),
        new Location("Bucharest", 44.4268, 26.1025),
        new Location("Budapest", 47.4979, 19.0402),
        new Location("Cologne", 50.93, 6.9603),
        new Location("Copenhagen", 55.6761, 12.5683),
        new Location("Dublin", 53.3498, -6.2603),
        new Location("Geneva", 46.2044, 6.1432),
        new Location("Glasgow", 55.8642, -4.2518),
        new Location("Gothenburg", 57.7089, 11.9746),
        new Location("Helsinki", 60.1699, 24.9384),
        new Location("Istanbul", 41.0082, 28.9784),
        new Location("Kongsberg", 59.6721, 9.6460),
        new Location("Krakow", 50.06, 19.94),
        new Location("Kyiv", 50.4501, 30.5234),
        new Location("Lingen", 52.5403, 7.3293),
        new Location("Lisbon", 38.7223, -9.1393),
        new Location("Liverpool", 53.4106, -2.9779),
        new Location("Ljubljana", 46.0569, 14.5058),
        new Location("London", 51.5072, -0.1276),
        new Location("Madrid", 40.4168, -3.7038),
        new Location("Mallorca", 39.69, 3.01),
        new Location("Manchester", 53.4808, -2.2426),
        new Location("Mechelen", 51.0259, 4.4776),
        new Location("Nice", 43.7102, 7.2620),
        new Location("Nurnberg", 49.4521, 0),
        new Location("Odense", 55.4038, 10.4024),
        new Location("Oslo", 59.9139, 10.7522),
        new Location("Paris", 48.8566, 2.3522),
        new Location("Porto", 41.1496, -8.6110),
        new Location("Prague", 50.0755, 14.4378),
        new Location("Reading", 51.4551, -0.9787),
        new Location("Rekyavik", 64.1466, -21.9426),
        new Location("Riga", 56.9496, 24.1052),
        new Location("Rome", 41.9028, 12.4964),
        new Location("Russia", 55.7558, 37.6173),
        new Location("Sheffield", 53.38, -1.47),
        new Location("Southampton", 50.9105, -1.4049),
        new Location("Stockholm", 59.3293, 18.0686),
        new Location("Tallinn", 59.4370, 24.7536),
        new Location("Tartu", 58.3780, 26.7290),
        new Location("Utrecht", 52.0907, 5.1214),
        new Location("Venice", 45.4408, 12.3155),
        new Location("Vianen", 51.9903, 5.1030),
        new Location("Vienna", 48.2082, 16.3738),
        new Location("Vilnius", 54.6872, 25.2797),
        new Location("Warsaw", 52.2297, 21.0122),
        new Location("Zurich", 47.3769, 8.5417),
        //new City("",0, 0),				
        //new City("",0, 0),
        //new City("",0, 0),

        // Asia

        new Location("Bengaluru", 12.9716, 77.5946),
        new Location("Jakarta", -6.20, 106.84),
        new Location("Sydney", -33.8688, 151.2093),
        new Location("Tokyo", 35.6762, 139.6503),
        //new City("",0, 0),
        //new City("",0, 0),

        // Africa
        new Location("Agadir", 30.4278, -9.5981),
        new Location("Lagos", 6.5244, 3.3792),
        //new City("",0, 0),
        //new City("",0, 0),
    ];
}

function Location(name, latitude, longitude) {
    this.name = name;
    this.latitude = latitude;
    this.longitude = longitude;
}