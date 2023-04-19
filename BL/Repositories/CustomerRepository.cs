using BL.Core;
using BL.Hashing;
using BL.Repositories.Interfaceis;
using BL.Repository;
using FCBankBasicHelper.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly Encryption encryption;
        public CustomerRepository(FcbankBasicContext context, Encryption encryption) : base(context)
        {
            this.encryption = encryption;
        }
        public Customer GetCustomerByPassport(string passport)
        {
            return context?.Customers.AsNoTracking().FirstOrDefault(u => u.Passport == passport);
        }
        public Customer GetCustomerById(int id)
        {
            return context?.Customers.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }
        public void RemoveCustomer(string passport)
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    var customer = GetCustomerByPassport(passport);
                    if (customer != null)
                    {
                        context.Customers.Remove(customer);
                        context.SaveChanges();
                    }                    
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.StackTrace);
                }
            }
        }
        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            var encryptedName = encryption.Encrypt(name);

            // Build expression tree for dynamic filter
            var parameter = Expression.Parameter(typeof(Customer), "c");
            var property = Expression.Property(parameter, nameof(Customer.FirstName));
            var value = Expression.Constant(encryptedName, typeof(string));
            var contains = Expression.Call(property, "Contains", Type.EmptyTypes, value);
            var lambda = Expression.Lambda<Func<Customer, bool>>(contains, parameter);
            var customers = context.Customers.Where(lambda);

            return customers;
        }

        public IEnumerable<Customer> GetCustomersBySurname(string surname)
        {
            var encryptedSurname = encryption.Encrypt(surname);

            // Build expression tree for dynamic filter
            var parameter = Expression.Parameter(typeof(Customer), "c");
            var property = Expression.Property(parameter, nameof(Customer.LastName));
            var value = Expression.Constant(encryptedSurname, typeof(string));
            var contains = Expression.Call(property, "Contains", Type.EmptyTypes, value);
            var lambda = Expression.Lambda<Func<Customer, bool>>(contains, parameter);
            var customers = context.Customers.Where(lambda);

            return customers;
        }
        public IEnumerable<Customer> GetCustomersByBirthday(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                DateTime datatime;
                if (startDate != DateTime.MinValue)
                    datatime = startDate;
                else
                    datatime = endDate;

                var parameter = Expression.Parameter(typeof(Customer), "c");
                var property = Expression.Property(parameter, nameof(Customer.Birthday));
                var value = Expression.Constant(datatime);
                var condition = Expression.Equal(property, value);
                var lambda = Expression.Lambda<Func<Customer, bool>>(condition, parameter);

                return context.Customers.Where(lambda);
            }
            else
            {
                if (startDate > endDate)
                {
                    throw new ArgumentException("Start date cannot be later than end date.", nameof(startDate));
                }
                var parameter = Expression.Parameter(typeof(Customer), "c");
                var property = Expression.Property(parameter, nameof(Customer.Birthday));
                var startValue = Expression.Constant(startDate);
                var endValue = Expression.Constant(endDate);
                var startCondition = Expression.GreaterThanOrEqual(property, startValue);
                var endCondition = Expression.LessThanOrEqual(property, endValue);
                var combinedCondition = Expression.AndAlso(startCondition, endCondition);
                var lambda = Expression.Lambda<Func<Customer, bool>>(combinedCondition, parameter);

                return context.Customers.Where(lambda);
            }
        }
        public IEnumerable<Customer> GetCustomersByAddress(string address)
        {
            var encryptedAddress = encryption.Encrypt(address);

            
            var parameter = Expression.Parameter(typeof(Customer), "c");
            var property = Expression.Property(parameter, nameof(Customer.Address));
            var value = Expression.Constant(encryptedAddress, typeof(string));
            var contains = Expression.Call(property, "Contains", Type.EmptyTypes, value);
            var lambda = Expression.Lambda<Func<Customer, bool>>(contains, parameter);
            var customers = context.Customers.Where(lambda);

          

            return customers;

        }
    }
}
