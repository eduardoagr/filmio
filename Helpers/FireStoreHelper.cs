using filmio.Model;

using Google.Cloud.Firestore;

namespace filmio.Helpers {
    public class FireStoreHelper {

        private readonly FirestoreDb _db;

        public FireStoreHelper(string projectId = "filmio-4fa42") {

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "serviceAccountKey.json");

            _db = FirestoreDb.Create(projectId);
        }

        public async Task AddUserAsync(string userId, string name, string email, Address address) {
            CollectionReference usersRef = _db.Collection("Users");
            DocumentReference newUserRef = usersRef.Document(userId);

            var user = new Dictionary<string, object> {
                { "Id", userId },
                { "Name", name },
                { "Email", email },
                    { "Address", new Dictionary<string, object> {
                    { "Building", address.building! },
                    { "Road", address.road! },
                    { "Neighbourhood", address.neighbourhood! },
                    { "Quarter", address.quarter! },
                    { "Suburb", address.suburb! },
                    { "City", address.city! },
                    { "County", address.county! },
                    { "State", address.state! },
                    { "Postcode", address.postcode! },
                    { "Country", address.country! },
                    { "CountryCode", address.countryCode! }
            }}
        };

            await newUserRef.SetAsync(user);
        }

        public async Task UpdateUserAsync(string userId, Address address) {
            var userRef = _db.Collection("Users").Document(userId);

            var updateData = new Dictionary<string, object>() {
                { "Address", new Dictionary<string, object> {
                    { "Building", address.building! },
                    { "Road", address.road! },
                    { "Neighbourhood", address.neighbourhood! },
                    { "Quarter", address.quarter! },
                    { "Suburb", address.suburb! },
                    { "City", address.city! },
                    { "County", address.county! },
                    { "State", address.state! },
                    { "Postcode", address.postcode! },
                    { "Country", address.country! },
                    { "CountryCode", address.countryCode! }
            }}
        };
            await userRef.UpdateAsync(updateData);
        }

    }
}
