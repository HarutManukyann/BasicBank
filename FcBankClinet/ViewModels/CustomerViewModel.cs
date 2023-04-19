using CoreWCFService;
using FCBankBasicHelper.Models;
using GalaSoft.MvvmLight.CommandWpf;
using Models.BaseType;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebAPI;

namespace FcBankClient.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private DateTime birthday;
        private string email;
        private string passport;
        private string address;
        private string phone;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public DateTime Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Passport
        {
            get => passport;
            set
            {
                passport = value;
                OnPropertyChanged(nameof(Passport));
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        private RelayCommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(Add);

        private async void Add()
        {
            var newCustomer = new CustomerModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Birthday = birthday,
                 Email = email,
                Passport = passport,
                Address = address,
                Phone = phone,
            };

            Service service = new Service();
            var responseTask = service.InsertCustomer(newCustomer);

            try
            {
                ResponseBase<CustomerModel> response = await responseTask;

                if (response.Success)
                {
                    MessageBox.Show("Customer created");
                }
                else
                {
                    MessageBox.Show("Creation failed, please try again");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while creating the Customer. " + ex.Message);
            }
        }

        private RelayCommand backCommand;
        public ICommand BackCommand => backCommand ??= new RelayCommand(Back);

        private void Back()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            FiltrationView filtrationView = new FiltrationView();
            currentWindow.Close();
            filtrationView.Show();
        }
    }
}