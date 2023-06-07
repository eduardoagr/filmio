namespace filmio.ViewModel.Dialogs {
    class ExamDetailDialogViewModel : ObservableObject {

        public ExamDetail ExamDetail { get; set; }

        public Command ScheduleExamCommand { get; set; }

        public ObservableCollection<string>? Subjects { get; set; }

        public ExamDetailDialogViewModel() {

            ExamDetail = new ExamDetail();
            ScheduleExamCommand = new Command(ScheduleExamActuion, canSchedule);
            ExamDetail.PropertyChanged += (s, a) => ScheduleExamCommand.RaiseCanExecuteChanged();
            GrabData();
        }

        private void GrabData() {
            Subjects = new ObservableCollection<string>() {
                "Mathematics",
                "Programming"
            };

        }

        private bool canSchedule(object arg) {
            return !string.IsNullOrEmpty(ExamDetail.Date)
                && !string.IsNullOrEmpty(ExamDetail.Subject)
                && !string.IsNullOrEmpty(ExamDetail.Title);
        }

        private void ScheduleExamActuion(object obj) {

            ExamDetail.Title = ExamDetail.Title;
            ExamDetail.Date = ExamDetail.Date?.Split(" ")[0];
            ExamDetail.Subject = ExamDetail.Subject;
        }


    }
}
