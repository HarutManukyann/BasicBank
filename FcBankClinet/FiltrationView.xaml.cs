using FcBankClient.ViewModels;
using Models.DTO;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FcBankClient
{
    /// <summary>
    /// Interaction logic for FiltrationView.xaml
    /// </summary>
    public partial class FiltrationView : Window
    {
        public FiltrationView()
        {
            InitializeComponent();
            this.DataContext = new FiltrationViewModel();

            //List<CustomerModel> list = new List<CustomerModel>
            //{
            //  new CustomerModel{  FirstName = "asfce",LastName = "awfd",Email="aeft" },
            //  new CustomerModel{  FirstName = "asfce",LastName = "awfd",Email="aeft" },
            //  new CustomerModel{  FirstName = "asfce",LastName = "awfd",Email="aeft" },
            //};
            //Customers_data.ItemsSource = list;
        }
    }
}
