namespace filmio.ViewModel {
    public class ErrorDialogViewModel : ObservableObject {

        public ContentDialog? Dialog { get; set; }

        public string _FirebaseError { get; set; }

        public Command CloseCommand { get; set; }

        public ErrorDialogViewModel(string firebaseError, ContentDialog dialog) {

            _FirebaseError = firebaseError;
            Dialog = dialog;
            CloseCommand = new Command(CloseAction);
        }

        private void CloseAction(object obj) {
            Dialog?.Hide();

        }
    }
}
