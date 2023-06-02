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
                HandleAuthException(ex);
                return null;
            }
        }

        public async Task<UserCredential?> SignInUser(string email, string password) {
            try {
                return await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
            } catch (FirebaseAuthException ex) {
                HandleAuthException(ex);
                return null;
            }
        }

        private static void ShowError(string Message) {

            var dlg = new GenericDialog();
            var vm = new GenericDialogViewModel(Message, 0, dlg);
            dlg.DataContext = vm;

            dlg.ShowAsync();
        }

        private static void HandleAuthException(FirebaseAuthException ex) {
            switch (ex.Reason) {
                case AuthErrorReason.InvalidEmailAddress:
                    ShowError("Invalid email");
                    break;
                case AuthErrorReason.WeakPassword:
                    ShowError("Weak password: it must be at least 6 characters");
                    break;
                case AuthErrorReason.EmailExists:
                    ShowError("Email exist");
                    break;
                case AuthErrorReason.WrongPassword:
                    ShowError("Wrong Password");
                    break;
                default:
                    break;
            }
        }
    }
}
