using FCBankBasicHelper.Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories.Interfaceis
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Customer GetCustomerByPassport(string passport);
        Customer GetCustomerById(int id);       
        void RemoveCustomer(string passport);
        IEnumerable<Customer> GetCustomersByName(string name);
        IEnumerable<Customer> GetCustomersBySurname(string surname);
        IEnumerable<Customer> GetCustomersByBirthday(DateTime startDate, DateTime endDate);
        IEnumerable<Customer> GetCustomersByAddress(string address);
    }
}
