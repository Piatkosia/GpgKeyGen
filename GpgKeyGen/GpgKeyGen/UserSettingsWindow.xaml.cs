using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
            string path;
            if (GetFilePath("Gdzie zapisać klucze? ", out path))
            {
                model.Path = path;
            }
        }

        private bool GetFilePath(string title, out string path)
        {
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.Title = title;
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
                   path = dlg.FileName;
                   return true;
                }
            }

            path = "";
            return false;
        }

        private void SavePath(object sender, RoutedEventArgs e)
        {
            model.SaveSettings();
            DialogResult = true;
        }

        private void GetDocumentPath(object sender, RoutedEventArgs e)
        {
            string path;
            if (GetFilePath("Gdzie zapisać dokumenty? ", out path))
            {
                model.DocumentPath = path;
            }
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
