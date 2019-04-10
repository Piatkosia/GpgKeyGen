using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GpgKeyGen.Validators;
using Prism.Commands;
using Prism.Mvvm;

namespace GpgKeyGen
{
    public class UserSettingsViewModel : BindableBase
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }
        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        private string _documentPath;

        public string DocumentPath
        {
            get { return _documentPath; }
            set { SetProperty(ref _documentPath, value); }
        }

        public DelegateCommand SaveCommand { get; private set; }
        public Action CloseView { get; set; }

        public UserSettingsViewModel()
        {
            SaveCommand = new DelegateCommand(SaveSettings, CanSaveSettings);
        }

        private bool CanSaveSettings()
        {
            NetAddressCorrect netAddressValidator = new NetAddressCorrect();
            netAddressValidator.AllowEmpty = true;
            return (Directory.Exists(Path) && netAddressValidator.Validate(Address, CultureInfo.CurrentCulture) == ValidationResult.ValidResult);

        }

        internal void LoadSettings()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LocalKeyPath))
                Properties.Settings.Default.LocalKeyPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Path = Properties.Settings.Default.LocalKeyPath;
            Address = Properties.Settings.Default.KeyServer;
            DocumentPath = Properties.Settings.Default.DocumentOutputPath;
        }

       

        internal void SaveSettings()
        {
            Properties.Settings.Default.LocalKeyPath = Path;
            Properties.Settings.Default.KeyServer = Address;
            Properties.Settings.Default.DocumentOutputPath = DocumentPath;
            Properties.Settings.Default.Save();
            CloseView();
        }
    }
}
