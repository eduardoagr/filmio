namespace filmio.ViewModel.Pages
{
    public class ManageUsersPageViewModel : ObservableObject {

        private readonly string? currentId;

        private readonly Frame frame;

        private readonly FireStoreHelper storeHelper;

        public User? SelectedUser { get; set; }

        public ObservableCollection<User>? Users { get; set; }

        public Command GoBackCommand { get; set; }


        public ManageUsersPageViewModel(string CurrentUserID, Frame frame) {

            storeHelper = new FireStoreHelper();
            currentId = CurrentUserID;
            this.frame = frame;
            GetUsersFromFirebase(currentId);

            GoBackCommand = new Command(GoBackAction);

        }

        private void GoBackAction(object obj) {
            if (frame != null && frame.CanGoBack) {
                frame.GoBack();
            }

        }

        private async void GetUsersFromFirebase(string currentUserId) {

            Users = new ObservableCollection<User>();
            Users = await storeHelper.GetAllUsersExceptCurrentUserAsync(currentUserId);
        }
    }
}
