using filmio.View.Dialogs;
using filmio.ViewModel;

namespace filmio.Helpers {
    public class FirebaseAuthHelper {

        private readonly IFirebaseAuthClient _firebaseAuthClient;

        public FirebaseAuthHelper(FirebaseAuthConfig firebaseAuthConfig) {
            _firebaseAuthClient = new FirebaseAuthClient(firebaseAuthConfig);
        }

        public async Task<UserCredential?> SignUpNewUser(string name, string email, string password) {
            try {
                return await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, name);
            } catch (FirebaseAuthException ex) {
                await HandleAuthExceptionAsync(ex);
                return null;
            }
        }

        public async Task<UserCredential?> SignInUser(string email, string password) {
            try {
                return await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
            } catch (FirebaseAuthException ex) {
                await HandleAuthExceptionAsync(ex);
                return null;
            }
        }

        private static async Task ShowErrorAsync(string Message) {

            var dlg = new ErrorDialog();
            var vm = new ErrorDialogViewModel(Message, dlg);
            dlg.DataContext = vm;

            await dlg.ShowAsync();
        }

        private static async Task HandleAuthExceptionAsync(FirebaseAuthException ex) {
            switch (ex.Reason) {
                case AuthErrorReason.InvalidEmailAddress:
                    await ShowErrorAsync("Invalid email");
                    break;
                case AuthErrorReason.WeakPassword:
                    await ShowErrorAsync("Weak password");
                    break;
                case AuthErrorReason.EmailExists:
                    await ShowErrorAsync("Email exist");
                    break;
                case AuthErrorReason.WrongPassword:
                    await ShowErrorAsync("Wrong Password");
                    break;
                default:
                    break;
            }
        }
    }
}
