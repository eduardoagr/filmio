

using System.Security;

namespace filmio.Model {
    public class User : ObservableObject {
        
        private string? email;
        private string? name;
        private string? password;

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
    }
}
