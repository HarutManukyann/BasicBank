using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Models.DTO;
using CoreWCFService;


namespace FcBankClient.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string email;
        private string password;
        Service serviceWCF;

        public ICommand LoginCommand { get; set; }
        public ICommand RegistrationWindowCommand { get; set; }
        

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegistrationWindowCommand = new RelayCommand(RegistrationWindow);
            
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private void Login()
        {
            LoginModel loginModel = new LoginModel
            {
                Email = Email, Password = Password
            };

            
        }

        private void RegistrationWindow()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            RegistrationView registrationWindow = new RegistrationView();
            currentWindow.Close();
            registrationWindow.Show();
        }
    }
}
