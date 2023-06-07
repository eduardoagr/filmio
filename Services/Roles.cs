namespace filmio.Services {
    public class Roles {

        public static ObservableCollection<string> GetRoles() {

            return new ObservableCollection<string>() {
                "Teacher",
                "Student"
            };
        }
    }
}
