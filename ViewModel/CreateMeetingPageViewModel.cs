namespace filmio.ViewModel {
    public class CreateMeetingPageViewModel : ObservableObject {

        public Frame Frame { get; set; }

        public Visibility canGoBack { get; set; }

        public Command GoBackCommand { get; set; }

        public CreateMeetingPageViewModel(Frame frame) {

            Frame = frame;

            GoBackCommand = new Command(GoBackAction);

            if (frame.CanGoBack) {
                canGoBack = Visibility.Visible;
            }
        }

        private void GoBackAction(object obj) {
            Frame.GoBack();
        }
    }
}
