
using filmio.ViewModel;

namespace filmio {
    public partial class LoginWindow : Window {

        private readonly LoginViewModel _loginViewModel;

        public LoginWindow(LoginViewModel loginViewModel) {
            InitializeComponent();
            _loginViewModel = loginViewModel;
            DataContext = _loginViewModel;
        }
    }
}
