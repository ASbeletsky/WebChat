var map = function () {
    var self = self || {};
    var mapContainer;

    self.createMap = function (settings) {
        $(settings.container).vectorMap(
                {
                    map: settings.map,
                    zoomButtons: !0,
                    markers: settings.markers,
                    normalizeFunction: "polynomial",
                    regionsSelectable: !0,
                    markersSelectable: !0,
                    backgroundColor: "#ffffff",
                    zoomOnScroll: !1,
                    regionStyle: {
                        initial: {
                            fill: "#428bca", "fill-opacity": 1, stroke: "none", "stroke-width": 0, "stroke-opacity": 1
                        },
                        hover: {
                            fill: "green"
                        }
                    },
                    onRegionClick: settings.onRegionClick,
                    series: {
                        regions: {
                            scale: ["#C8EEFF", "#0071A4"], normalizeFunction: "polynomial"
                        },
                        markers: {
                            attribute: "r",
                            scale: [5, 15],
                            values: [187.7, 255.16, 310.69, 605.17, 248.31, 107.35, 217.22]
                        }
                    },
                    markerStyle: {
                        initial: {
                            fill: "#E9422E",
                            stroke: "#E9422E",
                            "fill-opacity": 1,
                            "stroke-width": 8,
                            "stroke-opacity": .3
                        },
                        hover: {
                            stroke: "black",
                            "stroke-width": 2
                        }
                    }
                });
    };

    self.getCurrentMap = function () {
        return $(mapContainer).vectorMap('get', 'mapObject');
    }

    self.showWorldMap = function (container) {
        mapContainer = container;
        self.createMap({
            map: "world_mill",
            container: mapContainer,
            markers: [{ latLng: [41.9, 12.45], name: "Vatican City" }, { latLng: [55.75, 37.61], name: "Moscow" }, { latLng: [52.52, 13.4], name: "Berlin" }, { latLng: [37.77, -122.41], name: "San Francisco" }, { latLng: [3.2, 73.22], name: "Maldives" }, { latLng: [32.71, -117.16], name: "San Diego" }, { latLng: [40.71, -74], name: "New York" }, { latLng: [47.6, -122.33], name: "Seattle" }, { latLng: [1.3, 103.8], name: "Singapore" }, { latLng: [41.87, -87.62], name: "Chicago" }, { latLng: [26.02, 50.55], name: "Bahrain" }],
            onRegionClick: function (event, code) {
                if (code == "UA") { self.showUkrainianMap(); }
            }
        });
    };

    self.showUkrainianMap = function () {
        var currentMap = self.getCurrentMap();
        currentMap.remove();

        self.createMap({
            map: "ukraine_merc_en",
            container: mapContainer
        });

        self.getCurrentMap().updateSize();
    };

    return self;
}();


