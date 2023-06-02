namespace filmio.ViewModel.Pages {
    public class CreateMeetingPageViewModel : ObservableObject {

        private string? CurrentUserId;

        private FireStoreHelper storeHelper;

        public Frame Frame { get; set; }

        public ObservableCollection<User> Users { get; set; }

        public string? SelectedItem { get; set; }

        public User? SelectedUser { get; set; }

        public ObservableCollection<User> PeopleInMeeting { get; set; }

        public ObservableCollection<string>? Roles { get; set; }

        public Command? GoBackCommand { get; set; }

        public Command? RemoveItemCommend { get; set; }

        public Command? InviteCommand { get; set; }

        public Command? RemoveCommand { get; set; }

        public AsyncCommand? StartMeetingCommand { get; set; }

        public CreateMeetingPageViewModel(Frame frame, string id) {

            storeHelper = new();

            Roles = new ObservableCollection<string>();

            Users = new ObservableCollection<User>();

            PeopleInMeeting = new ObservableCollection<User>();

            CurrentUserId = id;

            Frame = frame;

            GoBackCommand = new Command(GoBackAction);

            StartMeetingCommand = new AsyncCommand(StartMeetingAtionAsync);

            InviteCommand = new Command(InviteAction);

            RemoveCommand = new Command<User>(RemoveAction);

            FillRoles();

            GetUsers(CurrentUserId);
        }

        private void RemoveAction(object obj) {

            if (SelectedUser != null) {
                var message = $"Do you want to remove: {SelectedUser!.Email}";
                GenericDialog dialog = new();
                var dialogViewModel = new GenericDialogViewModel(message, 1,
                    dialog, this);
                dialogViewModel.SelectedUser = SelectedUser;
                dialog.DataContext = dialogViewModel;
                dialog.ShowAsync();
            }

        }

        private void InviteAction(object obj) {
            var user = obj as User;
            if (!PeopleInMeeting.Contains(user!)) {
                PeopleInMeeting.Add(user!);
            }
        }

        private async Task StartMeetingAtionAsync() {

            if (string.IsNullOrEmpty(SelectedItem)) {
                string? message = "You have to select a role";
                GenericDialog dlg = new();
                dlg.DataContext = new GenericDialogViewModel(message, 0, dlg);
                await dlg.ShowAsync();
            } else {

                FireStoreHelper fireStore = new();

                var obj = await fireStore.CreateRoomInFirestoreAsync(PeopleInMeeting);

                DailyCoWindow coWindow = new() {
                    DataContext = new DailyCoViewModel(obj.Item2, SelectedItem!, obj.Item1!)
                };

                coWindow.Show();

                WindowsHelper.CloseWindow(Application.Current.Windows[0]);
            }
        }

        private void FillRoles() {
            Roles?.Add("Teacher");
            Roles?.Add("Student");
        }

        private async void GetUsers(string currentUserId) {
            Users = await storeHelper.GetAllUsersExceptCurrentUserAsync(currentUserId);
        }

        private void GoBackAction(object obj) {
            Frame.GoBack();
        }
    }
}
