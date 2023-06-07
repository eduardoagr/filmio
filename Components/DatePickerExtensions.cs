namespace filmio.Components {
    public static class DatePickerExtensions {
        public static readonly DependencyProperty DisallowPastDatesProperty =
            DependencyProperty.RegisterAttached("DisallowPastDates", typeof(bool), typeof(DatePickerExtensions), new PropertyMetadata(false, OnDisallowPastDatesChanged));

        public static bool GetDisallowPastDates(DatePicker datePicker) {
            return (bool)datePicker.GetValue(DisallowPastDatesProperty);
        }

        public static void SetDisallowPastDates(DatePicker datePicker, bool value) {
            datePicker.SetValue(DisallowPastDatesProperty, value);
        }

        private static void OnDisallowPastDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is DatePicker datePicker) {
                if ((bool)e.NewValue) {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
                }
            }
        }
    }
}
