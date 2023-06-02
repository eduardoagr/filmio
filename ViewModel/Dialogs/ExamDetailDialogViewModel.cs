namespace filmio.ViewModel.Dialogs {
    class ExamDetailDialogViewModel : ObservableObject {

        public ExamDetail examDetail { get; set; }

        public Command ScheduleExamCommand { get; set; }

        public ObservableCollection<string> Subjects { get; set; }

        public ExamDetailDialogViewModel() {

            examDetail = new ExamDetail();
            ScheduleExamCommand = new Command(ScheduleExamActuion, canSchedule);
            examDetail.PropertyChanged += (s, a) => ScheduleExamCommand.RaiseCanExecuteChanged();
            GrabData();
        }

        private void GrabData() {
            Subjects = new ObservableCollection<string>() {
                "Mathematics",
                "Programming"
            };

        }

        private bool canSchedule(object arg) {
            return !string.IsNullOrEmpty(examDetail.Date)
                && !string.IsNullOrEmpty(examDetail.Subject)
                && !string.IsNullOrEmpty(examDetail.Title);
        }

        private void ScheduleExamActuion(object obj) {

            examDetail.Title = examDetail.Title;
            examDetail.Date = examDetail.Date?.Split(" ")[0];
            examDetail.Subject = examDetail.Subject;
        }


    }
}
