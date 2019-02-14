using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CmdWrapper;
using FileSystemUtils;
using GeneralUtils;
using GpgKeyGenWrapper;
using Prism.Commands;
using Prism.Mvvm;

namespace GpgKeyGen
{
    public class GeneratorParamsModel : BindableBase
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
            }
        }
        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set
            {
                SetProperty(ref _comment, value);
                GenerateCommand.RaiseCanExecuteChanged();
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                GenerateCommand.RaiseCanExecuteChanged();
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                GenerateCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _oneDay;

        public bool OneDay
        {
            get { return _oneDay; }
            set
            {
                SetProperty(ref _oneDay, value);
                GenerateCommand.RaiseCanExecuteChanged();
            }
        }

        private string _cmdOutputString;

        public string CmdOutputString
        {
            get { return _cmdOutputString; }
            set
            {
                SetProperty(ref _cmdOutputString, value);
            }
        }


        public string ErrorLog { get; set; }
        public DelegateCommand GenerateCommand { get; private set; }
        public GeneratorParamsModel()
        {
                GenerateCommand = new DelegateCommand(async()=> { await Generate(); }, CanGenerate);
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LocalKeyPath) ||!Directory.Exists(Properties.Settings.Default.LocalKeyPath)) Properties.Settings.Default.LocalKeyPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Properties.Settings.Default.Save();
        }

        private bool CanGenerate()
        {
            return IsValid() && !generatingInProgress;
        }

        private bool generatingInProgress = false;
        private async Task Generate()
        {
            generatingInProgress = true;
            CmdOutputString = String.Empty;
            string path = FilenameUtils.GetTempFilePathWithExtension(".txt");
            File.WriteAllText(path, GpgBatchGenerator.GetScript(ToGpgKeygenParams()));
            Wrapper cmdWrapper = new Wrapper();
            cmdWrapper.Exited += CmdWrapper_Exited;
            cmdWrapper.OnIncommingText += CmdWrapper_OnIncommingText;
            CmdOutputString += "Tworzenie klucza: " + System.Environment.NewLine;
            await RunGpgCommand(cmdWrapper, " --batch --gen-key -v " + path);
           
            string keyId =
                CmdOutputString.Split(Environment.NewLine.ToCharArray()).Last(w => String.IsNullOrEmpty(w) == false)
                    .Split(' ')[5].Remove(0, 2).Trim();
            CmdOutputString += "Import klucza do bazy lokalnej: " + System.Environment.NewLine;
            await RunGpgCommand(cmdWrapper,
                $"--import {GpgKeygenParams.DefaultPublicKeyFilename} {GpgKeygenParams.DefaultPrivateKeyFilename}");
           

            if (MessageBox.Show("Czy wysłać nowo wygenerowany klucz publiczny na serwer?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                await RunGpgCommand(cmdWrapper,
                    " --keyserver {Properties.Settings.Default.KeyServer}  --send-key {keyId}");     
            }

        }

        private async Task RunGpgCommand(Wrapper cmdWrapper, string command)
        {
            await Task.Run(async () => await cmdWrapper.RunCmdProcess("gpg",command,
                Properties.Settings.Default.LocalKeyPath, Encoding.GetEncoding(852)));
        }

        private void CmdWrapper_OnIncommingText(object sender, IncommingTextEventArgs e)
        {
            CmdOutputString += TextUtils.ConsoleToWPF(e.IncommingText) + System.Environment.NewLine;
        }

        private void CmdWrapper_Exited(object sender, EventArgs e)
        {
            generatingInProgress = false;
        }

        public GpgKeygenParams ToGpgKeygenParams()
        {
            return new GpgKeygenParams()
            {
                Comment = this.Comment,
                Email = this.Email,
                ExpiredInDays = OneDay ? 1 : 0,
                Password = this.Password,
                Username = this.Username,
            };
        }

        public bool IsValid()
        {
            var result = !String.IsNullOrWhiteSpace(Username);
            result &= !String.IsNullOrWhiteSpace(Comment);
            result &= EmailUtils.IsValidAddress(Email);
            result &= !string.IsNullOrWhiteSpace(Password);
            return result;
        }

        public string GetError()
        {
            ErrorLog = String.Empty;
            if (String.IsNullOrWhiteSpace(Username))
            {
                ErrorLog += "Dane osobowe nie zostały ustawione.\n";
            }
            if (String.IsNullOrWhiteSpace(Comment))
            {
                ErrorLog += "Komentarz nie został ustawiony" + System.Environment.NewLine;
            }
            if (!EmailUtils.IsValidAddress(Email))
            {
                ErrorLog += "E-mail jest nieustawiony lub jest niepoprawny." + System.Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorLog += "Hasło jest nieustawione." + System.Environment.NewLine;
            }
            return ErrorLog;
        }
    }
}
