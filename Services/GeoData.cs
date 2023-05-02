
using filmio.Model;

namespace filmio.Services {
    public class GeoData {

        public static async Task<Address?> GetGeoData() {

            GeoServices _geoServices = new();

            var accessStatus = await Geolocator.RequestAccessAsync();

            if (accessStatus == GeolocationAccessStatus.Allowed) {
                var locator = new Geolocator();

                var pos = await locator.GetGeopositionAsync();

                var userCurrentLocation = await _geoServices.GetLocation(pos.Coordinate.Latitude, pos.Coordinate.Longitude);

                return userCurrentLocation!.address;
            }

            return null;
        }
    }
}
