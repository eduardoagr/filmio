namespace filmio.ViewModel.Dialogs {
    public class RegsterDialogViewModel : ObservableObject {
        private const string msg = "Registration successful";
        private readonly FirebaseAuthConfig _config;

        public User User { get; set; }

        public AsyncCommand RegisterCommand { get; set; }

        public Command CancelCommand { get; set; }


        public event EventHandler? CloseRequested;

        public Visibility IsFieldsVisible { get; set; }

        public Visibility isWorking { get; set; }

        public RegsterDialogViewModel(FirebaseAuthConfig config) {

            _config = config;

            User = new User();
            IsFieldsVisible = Visibility.Visible;
            isWorking = Visibility.Hidden;

            User.PropertyChanged += (s, a) =>
            RegisterCommand?.RaiseCanExecuteChanged();

            RegisterCommand = new AsyncCommand(RegisterAction, CanRegister);
            CancelCommand = new Command(CancelAction);
        }

        private void OnCloseRequested() {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void CancelAction() {
            OnCloseRequested();
        }


        private async Task RegisterAction() {

            IsFieldsVisible = Visibility.Collapsed;
            isWorking = Visibility.Visible;

            FirebaseAuthHelper authHelper = new(_config);

            var newUser = await authHelper.SignUpNewUser(User.Name!,
                User.Email, User.Password);

            if (newUser != null) {
                FireStoreHelper fireStore = new();
                var location = await GeoData.GetGeoData();
                await fireStore.AddUserAsync(newUser.User.Uid, User.Name!,
                    User.Email, location!);
                GenericDialog dialog = new();
                dialog.DataContext = new GenericDialogViewModel(msg, 2, dialog);
                await dialog.ShowAsync();

            } else {
                IsFieldsVisible = Visibility.Visible;
                isWorking = Visibility.Collapsed;
            }
        }

        private bool CanRegister(object arg) {
            return !string.IsNullOrEmpty(User.Name)
               && !string.IsNullOrEmpty(User.Email)
               && !string.IsNullOrEmpty(User.Password);
        }
    }
}
