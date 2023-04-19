using Models.BaseType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using Models.DTO;
using CoreWCFService;
using WebAPI;
using FCBankBasicHelper.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using Azure;
using BL.Core;
using System.Windows.Data;
using System.Windows.Documents;

namespace FcBankClient.ViewModels
{
    public class FiltrationViewModel : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private DateTime startDate=DateTime.Now;
        private DateTime endDate = DateTime.Now;
        private string email;
        private string passport;
        private string address;
        private string phone;
        private ObservableCollection<CustomerModel> retrievedItems;

        private ObservableCollection<CustomerModel> _myData;
        public ObservableCollection<CustomerModel> MyData
        {
            get { return _myData; }
            set
            {
                _myData = value;
                OnPropertyChanged(nameof(MyData));
            }
        }

        public FiltrationViewModel() 
        {
        }

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

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
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
        private RelayCommand creatNew;
        public ICommand CreatNew => creatNew ??= new RelayCommand(PerformCreatNew);

        private void PerformCreatNew()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            CustomerView customerView = new CustomerView();
            currentWindow.Close();
            customerView.Show();
        }

        private RelayCommand searchCommand;
        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);

        private async void Search()
        {
            var newCustomer = new FiltrationModel()
            {
                Name = firstName,
                Surname = lastName,
                StartDate = startDate,
                EndDate = endDate,
                Passport = passport,
                Address = address,
            };

            Service service = new Service();
            List<CustomerModel> list = new List<CustomerModel>
            {
              new CustomerModel{ Id=5, FirstName = "Syuzanna",LastName = "Loretsyan", Birthday = DateTime.Now, Passport = "AK6845752", Address ="ErevanArmenia", Phone="+37465895225" , Email="SyuzannaLoretsyan" },
              new CustomerModel{  FirstName = "asfce",LastName = "awfd",Email="aeft" },
              new CustomerModel{  FirstName = "asfce",LastName = "awfd",Email="aeft" },
            };
            MyData = new ObservableCollection<CustomerModel>(list);

            //try
            //{
            //    var response = await service.FilterCustomers(newCustomer);

            //    if (response.Success)
            //    {
            //        MyData = new ObservableCollection<CustomerModel>((IEnumerable<CustomerModel>)response.Data);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Customer not found " + response.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occurred while finding the customer" + ex.Message);
            //}
        }
        private RelayCommand<CustomerModel> editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand<CustomerModel>(Edit);

        private void Edit(CustomerModel obj)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            EditCustomerView editCustomer = new EditCustomerView(obj);
            currentWindow.Close();
            editCustomer.Show();

        }

        private RelayCommand<string> deleteCommand;
        public ICommand DeleteCommand => deleteCommand ??= new RelayCommand<string>(Delete);


        private async void Delete(string passport)
        {

            Passport = passport;
           
            Service service = new Service();

            try
            {
                var response = await service.DeleteCustomer(Passport);

                if (response.Success)
                {
                    MyData = new ObservableCollection<CustomerModel>((IEnumerable<CustomerModel>)response.Data);
                    MessageBox.Show("Customer deleted ");

                }
                else
                {
                    MessageBox.Show("Customer not found " + response.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while finding the customer" + ex.Message);
            }
        }
    }
}