namespace filmio.ViewModel {
    public class DailyCoViewModel : ObservableObject {

        public Visibility isTeacher { get; set; }

        public string MeetingUrl { get; set; }

        public ObservableCollection<User>? PeopleInvited { get; set; }

        public DailyCoViewModel(string roomID, string role, string url) {
            MeetingUrl = url ?? string.Empty;

            FireStoreHelper fireStore = new();

            Task.Run(async () => await GetUserAsync(roomID, fireStore));

            if (role.Equals("Teacher")) {
                isTeacher = Visibility.Visible;
            }
        }

        private async Task GetUserAsync(string roomID, FireStoreHelper fireStore) {
            PeopleInvited = await fireStore.GetUsersInvitedFromFirestoreAsync(roomID);
        }
    }
}
