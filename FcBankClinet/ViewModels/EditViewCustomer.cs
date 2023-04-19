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
using System.Windows.Input;

namespace FcBankClient.ViewModels
{
    public class EditViewCustomer : ViewModelBase
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

        private RelayCommand editCommand;

        public EditViewCustomer(CustomerModel obj)
        {
            firstName = obj.FirstName;
            lastName = obj.LastName;
            birthday = obj.Birthday;
            email = obj.Email;
            passport = obj.Passport;
            address = obj.Address;
            phone = obj.Phone;
        }

        public ICommand EditCommand => editCommand ??= new RelayCommand(Edit);

        private async void Edit()
        {
            var editCustomer = new CustomerModel()
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

            try
            {
                var response = await service.UpdateCustomer(editCustomer);

                if (response.Success)
                {
                    MessageBox.Show("Customer's data edited");

                    var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    FiltrationView filtrationView = new FiltrationView();
                    currentWindow.Close();
                    filtrationView.Show();
                }
                else
                {
                    MessageBox.Show("Data edit failed, please try again");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while editing the Customer." + ex.Message);
            }
        }
    }
}