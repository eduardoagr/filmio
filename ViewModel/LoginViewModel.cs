using filmio.Dialogs;
using filmio.Services;

using User = filmio.Model.User;

namespace filmio.ViewModel {

    public class LoginViewModel : ObservableObject {

        private readonly FirebaseAuthConfig _config;

        public User User { get; set; }

        public AsyncCommand LoginCommand { get; set; }

        public Command CreateAccountCommand { get; set; }

        public LoginViewModel(FirebaseAuthConfig config) {
            _config = config;
            User = new User();

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

            FirebaseAuthHelper authHelper = new(_config);

            var newUser = await authHelper.SignInUser(User.Email,
                User.Password);

            if (newUser != null) {

                FireStoreHelper fireStore = new();
                var location = await GeoData.GetGeoData();
                await fireStore.UpdateUserAsync(newUser.User.Uid, location!);

            }
        }

        private bool CanLogin(object arg) {
            return !string.IsNullOrEmpty(User.Email)
                && !string.IsNullOrEmpty(User.Password);
        }
    }
}
