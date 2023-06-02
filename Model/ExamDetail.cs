namespace filmio.Model {
    class ExamDetail : ObservableObject {

        private string? title;
        private string? subject;
        private string? date;


        public string? Title {
            get => title!;
            set {
                title = value;
                OnPropertyChanged();
            }
        }

        public string? Subject {
            get => subject!;
            set {
                subject = value;
                OnPropertyChanged();
            }
        }

        public string? Date {
            get => date!;
            set {
                date = value;
                OnPropertyChanged();
            }
        }
    }
}
