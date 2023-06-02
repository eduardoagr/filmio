using filmio.ViewModel.Pages;

namespace filmio.ViewModel.Dialogs
{

    public class GenericDialogViewModel : ObservableObject {

        private GenericDialog? dialog;

        private readonly ObservableCollection<User>? ListOfUsers;

        private int Id;

        private readonly CreateMeetingPageViewModel? CreateMeetingPageViewModel;

        public string? Title { get; set; }

        public string? Msg { get; set; }

        public User? SelectedUser { get; set; }

        public Command CancelButtonCommand { get; set; }

        public Command OkButtonCommand { get; set; }

        public Command DeleteButtonCommand { get; set; }

        public Visibility isCancelVisible { get; set; } = Visibility.Collapsed;

        public Visibility isOkVisible { get; set; } = Visibility.Collapsed;

        public Visibility isDeleteVisible { get; set; } = Visibility.Collapsed;

        public GenericDialogViewModel(string msg, int identifier, GenericDialog dlg, CreateMeetingPageViewModel? createMeetingPageViewModel = null) {

            Id = identifier;
            Msg = msg;
            dialog = dlg;
            CreateMeetingPageViewModel = createMeetingPageViewModel;


            if (identifier == 0) {
                Title = "Error";
                isOkVisible = Visibility.Visible;
            } else if (identifier == 1) {
                Title = "Remove item";
                isDeleteVisible = Visibility.Visible;
                isCancelVisible = Visibility.Visible;
            } else if (identifier == 2) {
                Title = "Success";
                isOkVisible = Visibility.Visible;
            }

            OkButtonCommand = new Command(OkButtonAction);

            CancelButtonCommand = new Command(CancelButtonAction);

            DeleteButtonCommand = new Command(DeleteButtonAction);

        }

        private void DeleteButtonAction(object obj) {
            CreateMeetingPageViewModel?.PeopleInMeeting.Remove(SelectedUser!);
            dialog!.Hide();
        }

        private void CancelButtonAction(object obj) {

            CreateMeetingPageViewModel!.SelectedUser = null;
            dialog?.Hide();
        }

        private void OkButtonAction(object obj) {
            dialog?.Hide();
            if (Id == 2) {
                WindowsHelper.CloseWindow(Application.Current.Windows[2]);
            }
        }
    }
}
