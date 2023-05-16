


namespace filmio.ViewModel {

    public class LoginViewModel : ObservableObject {

        private readonly FirebaseAuthConfig _config;

        public User User { get; set; }

        public Visibility isFormVis { get; set; }

        public Visibility isWorkiing { get; set; }

        public AsyncCommand LoginCommand { get; set; }

        public Command CreateAccountCommand { get; set; }

        public LoginViewModel(FirebaseAuthConfig config) {
            _config = config;
            User = new User();
            isFormVis = Visibility.Visible;
            isWorkiing = Visibility.Collapsed;
            LoginCommand = new AsyncCommand(LoginAction, CanLogin);
            User.PropertyChanged += (sender, args) => LoginCommand.RaiseCanExecuteChanged();
            CreateAccountCommand = new Command(CreateAccountAction);
        }

        private void CreateAccountAction(object obj) {
            RegisterDialog dialog = new() {
                DataContext = new RegsterDialogViewModel(_config)
            };

            var registerViewModel = (RegsterDialogViewModel)dialog.DataContext;

            registerViewModel.CloseRequested += (sender, args) => {
                dialog.Close();
            };

            dialog.ShowDialog();
        }

        private async Task LoginAction() {

            isFormVis = Visibility.Collapsed;
            isWorkiing = Visibility.Visible;

            FirebaseAuthHelper authHelper = new(_config);

            var newUser = await authHelper.SignInUser(User.Email,
                User.Password);

            if (newUser != null) {

                FireStoreHelper fireStore = new();
                var location = await GeoData.GetGeoData();
                await fireStore.UpdateUserAsync(newUser.User.Uid, location!);

                MainFrame mainFrame = new();
                MainFrameViewModel frameViewModel = new(mainFrame.ContentFrame);
                mainFrame.DataContext = frameViewModel;
                mainFrame.Show();

                WindowsHelper.CloseWindow(Application.Current.MainWindow);


            } else {
                isFormVis = Visibility.Visible;
                isWorkiing = Visibility.Collapsed;
            }
        }

        private bool CanLogin(object arg) {
            return !string.IsNullOrEmpty(User.Email)
                && !string.IsNullOrEmpty(User.Password);
        }
    }
}
