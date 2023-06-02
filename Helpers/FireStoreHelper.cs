using Newtonsoft.Json;

namespace filmio.Helpers {
    public class FireStoreHelper {

        private readonly FirestoreDb _db;

        public FireStoreHelper(string projectId = "filmio-4fa42") {

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                "serviceAccountKey.json");

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

        public async Task<ObservableCollection<User>> GetAllUsersExceptCurrentUserAsync(string currentUserId) {
            CollectionReference usersRef = _db.Collection("Users");

            // Query all users except the current user
            QuerySnapshot snapshot = await usersRef.WhereNotEqualTo("Id", currentUserId).GetSnapshotAsync();

            ObservableCollection<User> users = new();

            foreach (DocumentSnapshot document in snapshot.Documents) {
                User user = new() {
                    Id = document.GetValue<string>("Id"),
                    Name = document.GetValue<string>("Name"),
                    Email = document.GetValue<string>("Email")
                };

                Dictionary<string, object> addressData = document.GetValue<Dictionary<string, object>>("Address");
                Address address = new() {
                    building = addressData["Building"]?.ToString(),
                    road = addressData["Road"]?.ToString(),
                    neighbourhood = addressData["Neighbourhood"]?.ToString(),
                    quarter = addressData["Quarter"]?.ToString(),
                    suburb = addressData["Suburb"]?.ToString(),
                    city = addressData["City"]?.ToString(),
                    county = addressData["County"]?.ToString(),
                    state = addressData["State"]?.ToString(),
                    postcode = addressData["Postcode"]?.ToString(),
                    country = addressData["Country"]?.ToString(),
                    countryCode = addressData["CountryCode"]?.ToString()
                };

                user.Address = address;

                users.Add(user);
            }

            return users;
        }

        public async Task<(string, string)> CreateRoomInFirestoreAsync(ObservableCollection<User> usersInvited) {
            // Generate the room in Daily.co or retrieve the response from Daily.co API
            var dailyCoResponse = new DailyCoServices();
            var room = await dailyCoResponse.CreateRoomAsync();

            // Create a new Firestore document reference
            DocumentReference roomRef = _db.Collection("rooms").Document(room!.name);

            room.id = roomRef.Id;
            string roomJson = JsonConvert.SerializeObject(room);

            // Deserialize the JSON to a dictionary
            var roomData = JsonConvert.DeserializeObject<Dictionary<string, object>>(roomJson);

            // Add usersInvited data to roomData
            var usersInvitedData = usersInvited.Select(user => new { email = user.Email, name = user.Name }).ToList();
            roomData["usersInvited"] = usersInvitedData;

            // Insert the room document into Firestore
            await roomRef.SetAsync(roomData);

            return (room.url, room.id);
        }

        public async Task<ObservableCollection<User>> GetUsersInvitedFromFirestoreAsync(string roomId) {
            DocumentReference roomRef = _db.Collection("rooms").Document(roomId);

            DocumentSnapshot snapshot = await roomRef.GetSnapshotAsync();

            if (snapshot.Exists) {
                Dictionary<string, object> roomData = snapshot.ToDictionary();

                if (roomData.TryGetValue("usersInvited", out object usersInvitedObj) && usersInvitedObj is List<object> usersInvitedData) {
                    var usersInvited = new ObservableCollection<User>();

                    foreach (var userData in usersInvitedData) {
                        if (userData is Dictionary<string, object> userDataDict) {
                            string email = userDataDict.TryGetValue("email", out object emailObj) ? emailObj.ToString() : "";
                            string name = userDataDict.TryGetValue("name", out object nameObj) ? nameObj.ToString() : "";
                            usersInvited.Add(new User { Email = email, Name = name });
                        }
                    }

                    return usersInvited;
                }
            }

            return new ObservableCollection<User>();
        }
    }
}