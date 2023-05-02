using filmio.ViewModel;

namespace filmio {

    public partial class App : Application {

        private readonly IHost _host;

        private FirebaseAuthConfig? config;

        public App() {

            _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {

                config = new FirebaseAuthConfig() {

                    ApiKey = context.Configuration.GetValue<string>("FIREBASE_KEY"),
                    AuthDomain = context.Configuration.GetValue<string>("FIREBASE_DOMAIN"),
                    Providers = new FirebaseAuthProvider[] {
                          new EmailProvider()
                    }
                };


                services.AddSingleton(services => new LoginViewModel(config));
                services.AddSingleton<LoginWindow>();

            })
            .Build();
        }

        protected override void OnStartup(StartupEventArgs e) {

            // Show the login window
            MainWindow = _host.Services.GetRequiredService<LoginWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}