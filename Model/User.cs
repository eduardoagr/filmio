namespace filmio.Model {
    public class User : ObservableObject {

        private string? id;
        private string? email;
        private string? name;
        private string? password;
        private Address? address;
        private SolidColorBrush? isInvited;

        public Address? Address {
            get => address!;
            set {
                address = value;
                OnPropertyChanged();
            }
        }
        public string? Id {
            get => id!;
            set {
                id = value;
                OnPropertyChanged();
            }
        }
        public string? Name {
            get => name!;
            set {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Email {
            get => email!;
            set {
                email = value;
                OnPropertyChanged();
            }
        }

        public string Password {
            get => password!;
            set {
                password = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush IsInvited {
            get => isInvited!;
            set {
                isInvited = value;
                OnPropertyChanged();
            }
        }
    }
}
