namespace filmio.ViewModel.Pages {
    public class MainPageViewModel : ObservableObject {

        private readonly Frame _frame;

        private readonly string _currentUserId;

        public ObservableCollection<CustomTile> customTiles { get; set; }

        public Command CreateMeetingCommand { get; set; }

        public Command JoinMeetingCommand { get; set; }

        public Command? ManageUsersCommand { get; set; }

        public Command? scheduleCommand { get; set; }


        public MainPageViewModel(Frame frame, string id) {

            _frame = frame;

            _currentUserId = id;

            CreateMeetingCommand = new Command(CreateMeetingAction);

            JoinMeetingCommand = new Command(JoinCommand);

            ManageUsersCommand = new Command(ManageUsersAction);

            scheduleCommand = new Command(OpenExamDetalDialogAction);

            CreateUI();
        }

        private void OpenExamDetalDialogAction(object obj) {

            ExamDetaiDialog detaiDialog = new() {
                DataContext = new ExamDetailDialogViewModel()
            };

            detaiDialog.ShowDialog();
        }

        private void ManageUsersAction(object obj) {
            ManageUsersPage usersPage = new() {
                DataContext = new ManageUsersPageViewModel(_currentUserId, _frame)
            };

            _frame.Navigate(usersPage);
        }

        private void JoinCommand(object obj) {

            DailyCoServices dailyCo = new();

        }

        private void CreateMeetingAction(object obj) {

            CreateMeetingPage createMeeting = new() {
                DataContext = new CreateMeetingPageViewModel(_frame, _currentUserId)
            };

            _frame.Navigate(createMeeting);
        }

        private void CreateUI() {
            const string imagesLocation = "pack://application:,,,/img/";
            customTiles = new ObservableCollection<CustomTile>() {
                new CustomTile {
                    Title = "Manage classroom",
                    Command = ManageUsersCommand!,
                    BackgroundColor = new SolidColorBrush(Colors.RoyalBlue),
                    Image = $"{imagesLocation}icons8-google-classroom-48.png"
                },
                new CustomTile {
                    Title = "Schedule exam",
                    Command = scheduleCommand!,
                    BackgroundColor = new SolidColorBrush(Colors.DarkGreen),
                    Image = $"{imagesLocation}icons8-schedule-100.png"
                },
                new CustomTile {
                    Title = "Upload documents" ,
                    Command = ManageUsersCommand!,
                    BackgroundColor = new SolidColorBrush (Colors.DarkMagenta),
                    Image = $"{imagesLocation}icons8-upload-to-the-cloud-100.png"
                },
                new CustomTile {
                    Title = "Report card",
                    Command = JoinMeetingCommand!,
                    BackgroundColor = new SolidColorBrush(Colors.DarkRed),
                    Image = $"{imagesLocation}icons8-report-card-100.png"
                },
            };
        }
    }
}
