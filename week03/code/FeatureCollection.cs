// Classes to deserialize USGS GeoJSON earthquake feed.
//
// Top-level JSON structure:
// {
//   "type": "FeatureCollection",
//   "features": [
//     {
//       "type": "Feature",
//       "properties": {
//         "mag": 2.36,
//         "place": "1km NE of Pahala, Hawaii",
//         ...
//       },
//       ...
//     },
//     ...
//   ]
// }

public class FeatureCollection
{
    public string Type { get; set; } = "";
    public List<Feature> Features { get; set; } = [];
}

public class Feature
{
    public string Type { get; set; } = "";
    public EarthquakeProperties Properties { get; set; } = new();
}

public class EarthquakeProperties
{
    public double? Mag { get; set; }
    public string Place { get; set; } = "";
}
