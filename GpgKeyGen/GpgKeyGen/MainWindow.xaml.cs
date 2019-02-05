using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileSystemUtils;
using GpgKeyGenWrapper;

namespace GpgKeyGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GeneratorParamsModel CurrentParams { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CurrentParams = new GeneratorParamsModel();
            DataContext = CurrentParams;
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            if (CurrentParams.IsValid())
            {
                GpgBatchGenerator.GenerateKey(CurrentParams.ToGpgKeygenParams());
                string path = FilenameUtils.GetTempFilePathWithExtension(".txt");
                File.WriteAllText(path, GpgBatchGenerator.GetScript(CurrentParams.ToGpgKeygenParams()));


            }
            else MessageBox.Show("Znaleziono następujące błędy:" + System.Environment.NewLine + CurrentParams.ErrorLog);
        }

        private void ChangeSettings(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            CurrentParams.Password = KeyPwd.Password;
        }
    }
}
