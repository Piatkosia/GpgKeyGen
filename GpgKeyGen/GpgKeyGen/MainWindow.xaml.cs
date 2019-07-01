
using System.Windows;

namespace GpgKeyGen
{
    public partial class MainWindow : Window
    {
        private GeneratorParamsModel CurrentParams { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CurrentParams = new GeneratorParamsModel();
            CurrentParams.ClearPasswordFunction = ClearPasswordAfterSend;
            DataContext = CurrentParams;
        }

        private void ChangeSettings(object sender, RoutedEventArgs e)
        {
            UserSettingsWindow window = new UserSettingsWindow();
            window.ShowDialog();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            CurrentParams.Password = KeyPwd.Password;
        }

        public void ClearPasswordAfterSend()
        {
            KeyPwd.Password = string.Empty;
        }
    }
}
