
namespace filmio.ViewModel {
    public class MainPageViewModel : ObservableObject {

        private readonly Frame _frame;

        public Command CreateMeetingCommand { get; set; }

        public Command JoinMeetingCommand { get; set; }

        public MainPageViewModel(Frame frame) {

            _frame = frame;

            CreateMeetingCommand = new Command(CreateMeetingAction);

            JoinMeetingCommand = new Command(JoinCommand);

        }

        private void JoinCommand(object obj) {

        }

        private void CreateMeetingAction(object obj) {

            CreateMeetingPage createMeeting = new() {
                DataContext = new CreateMeetingPageViewModel(_frame)
            };

            _frame.Navigate(createMeeting);
        }
    }
}
