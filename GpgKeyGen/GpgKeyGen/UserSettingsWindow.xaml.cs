using System;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GpgKeyGen
{
    /// <summary>
    /// Interaction logic for UserSettingsWindow.xaml
    /// </summary>
    public partial class UserSettingsWindow : Window
    {
        private UserSettingsViewModel model { get; set; }
        public UserSettingsWindow()
        {
            model = new UserSettingsViewModel();
            model.LoadSettings();
            model.CloseView = new Action(this.Close);
            DataContext = model;
            InitializeComponent();

        }

        private void GetPath(object sender, RoutedEventArgs e)
        {
            using (var dlg = new CommonOpenFileDialog())
            {

                dlg.Title = "Gdzie zapisać klucze? ";
                dlg.IsFolderPicker = true;
                dlg.InitialDirectory = model.Path;

                dlg.AddToMostRecentlyUsedList = false;
                dlg.AllowNonFileSystemItems = false;
                dlg.DefaultDirectory = model.Path;
                dlg.EnsureFileExists = true;
                dlg.EnsurePathExists = true;
                dlg.EnsureReadOnly = false;
                dlg.EnsureValidNames = true;
                dlg.Multiselect = false;
                dlg.ShowPlacesList = true;

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    model.Path = dlg.FileName;
                }
            }
        }

        private void SavePath(object sender, RoutedEventArgs e)
        {
            model.SaveSettings();
            DialogResult = true;
        }
    }
}
