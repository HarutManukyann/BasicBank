using Azure;
using CoreWCFService;
using GalaSoft.MvvmLight.CommandWpf;
using Models.BaseType;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebAPI;

namespace FcBankClient.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string username;
        private string email;
        private string firstName;
        private string lastName;
        private DateTime birthday = DateTime.Now;
        private string passportNumber;
        private string address;
        private string password1;
        private string password2;
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(username);
                }
            }
        }
        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(email);
                }
            }
        }
        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(firstName);
                }
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(lastName);
                }
            }
        }

        public DateTime Birthday
        {
            get => birthday;
            set
            {
                if (birthday != value)
                {
                    birthday = value;
                    OnPropertyChanged(nameof(birthday));
                }
            }
        }

        public string PassportNumber
        {
            get => passportNumber;
            set
            {
                if (passportNumber != value)
                {
                    passportNumber = value;
                    OnPropertyChanged(passportNumber);
                }
            }
        }

        public string Address
        {
            get => address;
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged(address);
                }
            }
        }

        public string Password1
        {
            get => password1;
            set
            {
                if (password1 != value)
                {
                    password1 = value;
                    OnPropertyChanged(nameof(Password1));
                }
            }
        }

        public string Password2
        {
            get => password2;
            set
            {
                if (password2 != value)
                {
                    password2 = value;
                    OnPropertyChanged(nameof(Password2));
                }
            }
        }


        public ICommand LoginCommand => new RelayCommand(Login);

        private void Login()
        {
            var CurrentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            LoginView loginView = new LoginView();
            CurrentWindow.Close();
            loginView.Show();
        }

        private RelayCommand registerCommand;
        public ICommand RegisterCommand => registerCommand ??= new RelayCommand(Register);

        private async void Register()
        {
            var newUser = new UserModel()
            {
                Username = username,
                Email = email,
                PassportNumber = passportNumber,
                FirstName = firstName,
                LastName = lastName,
                Birthday = birthday,
                Address = address,
                IsActive = true,
                Password = password1,
            };


            Service service = new Service();

            var responseTask = service.RegisterUser(newUser);

            try
            {
                var response = await responseTask;

                if (response.Success)
                {
                    Console.WriteLine("User registered successfully!");
                    var CurrentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    CustomerView authWindow = new CustomerView();
                    CurrentWindow.Close();
                    authWindow.Show();
                }
                else
                {
                    MessageBox.Show("User registration failed: " + response.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while registering the user: " + ex.Message);
            }
        }
    }
}
