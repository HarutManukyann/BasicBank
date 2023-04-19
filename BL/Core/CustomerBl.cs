using BL.Hashing;
using BL.Repositories;
using FCBankBasicHelper.Models;
using Models.BaseType;
using Models.DTO;
using Models.Mappers;


namespace BL.Core
{
    public class CustomerBl
    {
        private readonly Validation validation;
        private readonly Encryption encryption;
        private readonly CustomerRepository customerRepository;
        public CustomerBl(Encryption encryption, Validation validation , CustomerRepository customerRepository)
        { 
            this.customerRepository = customerRepository;
            this.validation = validation;
            this.encryption = encryption;
        }
        public ResponseBase<CustomerModel> InsertCustomer(CustomerModel model)
        {
            try
            {
                Customer customer = Mapper<CustomerModel, Customer>.Map(model);
                if (customer == null || !validation.CustomerValidation(customer)) throw new Exception("Invalid customer");
                encryption.EncryptData(customer);                
                customerRepository.Add(customer);
                return new ResponseBase<CustomerModel>(true, "Customer added Successfully", model);
            }
            catch (Exception ex)
            {
                return new ResponseBase<CustomerModel>(false, ex.Message, model);
            }
        }
        public ResponseBase<List<CustomerModel>> GetAll()
        {
            try
            {
                List<CustomerModel> result = new List<CustomerModel>();
                IEnumerable<Customer> data = customerRepository.GetAll();
                foreach (var item in data)
                {
                    encryption.DecryptData(item);
                    result.Add(Mapper<Customer, CustomerModel>.Map(item));
                }
                return new ResponseBase<List<CustomerModel>>(true, "Customers getted Successfully", result);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CustomerModel>>(false, ex.Message);
            }
        }
        public ResponseBase<CustomerModel> GetCustomerById(int id)
        {
            try
            {
                Customer customer = customerRepository.GetCustomerById(id);
                if (customer is null) throw new Exception("Customer not found");
                encryption.DecryptData(customer);
                var customermodel = Mapper<Customer, CustomerModel>.Map(customer);
                return new ResponseBase<CustomerModel>(true, "Customer getted Successfully", customermodel);
            }
            catch (Exception ex)
            {
                return new ResponseBase<CustomerModel>(false, ex.Message);
            }
        }
        public ResponseBase<CustomerModel> GetCustomerByPassport(string passport)
        {
            try
            {
                encryption.Encrypt(passport);
                Customer customer = customerRepository.GetCustomerByPassport(passport);
                if (customer is null) throw new Exception("Customer is not found");
                encryption.DecryptData(customer);
                var customermodel = Mapper<Customer, CustomerModel>.Map(customer);
                return new ResponseBase<CustomerModel>(true, "Customer getted Successfully", customermodel);
            }
            catch (Exception ex)
            {
                return new ResponseBase<CustomerModel>(false, ex.Message);
            }
        }
        public ResponseBase<CustomerModel> UpdateCustomer(CustomerModel model)
        {
            try
            {
                Customer customer = Mapper<CustomerModel, Customer>.Map(model);
                if (customer == null && !validation.CustomerValidation(customer)) throw new Exception("Invalid customer");
                encryption.EncryptData(customer);
                customerRepository.Update(customer);
                return new ResponseBase<CustomerModel>(true, "Customer updated Successfully", model);
            }
            catch (Exception ex)
            {
                return new ResponseBase<CustomerModel>(false, ex.Message);
            }
        }
        public ResponseBase<CustomerModel> RemoveCustomer(string passport)
        {
            try
            {
                customerRepository.RemoveCustomer(passport);
                return new ResponseBase<CustomerModel>(true, "Customer removed Successfully");
            }
            catch (Exception ex)
            {
                return new ResponseBase<CustomerModel>(false, ex.Message);
            }
        }

        public ResponseBase<IEnumerable<CustomerModel>> GetFilteredCustomers(FiltrationModel filtrationModel)
        {
            try
            {
                List<CustomerModel> filteredCustomers = new List<CustomerModel>();

                if (!string.IsNullOrEmpty(filtrationModel.Name))
                {
                    var customersByName = customerRepository.GetCustomersByName(filtrationModel.Name);
                    foreach (var customer in customersByName)
                    {
                        encryption.DecryptData(customer);
                        var customerModel = Mapper<Customer, CustomerModel>.Map(customer);
                        filteredCustomers.Add(customerModel);
                    }
                }

                if (!string.IsNullOrEmpty(filtrationModel.Surname))
                {
                    var customersBySurname = customerRepository.GetCustomersBySurname(filtrationModel.Surname);
                    foreach (var customer in customersBySurname)
                    {
                        encryption.DecryptData(customer);
                        var customerModel = Mapper<Customer, CustomerModel>.Map(customer);
                        filteredCustomers.Add(customerModel);
                    }
                }

                if (filtrationModel.StartDate != DateTime.MinValue && filtrationModel.EndDate != DateTime.MinValue)
                {
                    var customersByBirthday = customerRepository.GetCustomersByBirthday(filtrationModel.StartDate, filtrationModel.EndDate);
                    foreach (var customer in customersByBirthday)
                    {
                        encryption.DecryptData(customer);
                        var customerModel = Mapper<Customer, CustomerModel>.Map(customer);
                        filteredCustomers.Add(customerModel);
                    }
                }

                if (!string.IsNullOrEmpty(filtrationModel.Address))
                {
                    var customersByAddress = customerRepository.GetCustomersByAddress(filtrationModel.Address);
                    foreach (var customer in customersByAddress)
                    {
                        encryption.DecryptData(customer);
                        var customerModel = Mapper<Customer, CustomerModel>.Map(customer);
                        filteredCustomers.Add(customerModel);
                    }
                }               
                return new ResponseBase<IEnumerable<CustomerModel>>(true, "Customer filtered Successfully", filteredCustomers.Distinct());
            }
            catch (Exception ex)
            {
                return new ResponseBase<IEnumerable<CustomerModel>>(false, ex.Message);
            }
        }
    }
}

