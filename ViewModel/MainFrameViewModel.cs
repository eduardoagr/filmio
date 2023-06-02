using filmio.ViewModel.Pages;

namespace filmio.ViewModel {
    public class MainFrameViewModel : ObservableObject {

        public Frame ContentFrame { get; set; }

        public MainFrameViewModel(Frame frame, string id) {
            ContentFrame = frame;

            ContentFrame.Content = new MainPage() {
                DataContext = new MainPageViewModel(ContentFrame, id)
            };

        }
    }
}
