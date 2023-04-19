using Models.BaseType;
using Models.DTO;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http.Headers;
using WebAPI;

namespace CoreWCFService
{
    public class Service : IService
    {
        private HttpClient client;
        public Service()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7263/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<ResponseBase<CustomerModel>> InsertCustomer(CustomerModel model)
        {
            var response = await client.PostAsJsonAsync($"Customer/Post", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<CustomerModel>>();
            return result;
        }
        public async Task<ResponseBase<CustomerModel>> GetCustomerById(int id)
        {
            var response = await client.GetAsync($"Customer/GetById?id={id}");
            var result = await response.Content.ReadAsAsync<ResponseBase<CustomerModel>>();
            return result;
        }
        public async Task<ResponseBase<CustomerModel>> DeleteCustomer(string passport)
        {
            var response = await client.GetAsync($"Customer/Remove?id={passport}");
            var result = await response.Content.ReadAsAsync<ResponseBase<CustomerModel>>();
            return result;
        }
        public async Task<ResponseBase<CustomerModel>> UpdateCustomer(CustomerModel model)
        {
            var response = await client.PutAsJsonAsync($"Customer/Update", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<CustomerModel>>();
            return result;
        }
        public async Task<ResponseBase<List<CustomerModel>>> GetAllCustomers()
        {
            var response = await client.GetAsync("Customer/GetAll");
            var result = await response.Content.ReadAsAsync<ResponseBase<List<CustomerModel>>>();
            return result;
        }
        public async Task<ResponseBase<CustomerModel>> GetCustomerByPassport(string passport)
        {
            var response = await client.GetAsync($"Customer/GetbyPassport?passport={passport}");
            var result = await response.Content.ReadAsAsync<ResponseBase<CustomerModel>>();
            return result;
        }

        public async Task<ResponseBase<AuthenticatedResponse>> LoginUser(LoginModel model)
        {
            var response = await client.PutAsJsonAsync($"UserAuth/Login", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<AuthenticatedResponse>>();
            return result;
        }
        public async Task<ResponseBase<UserModel>> DeleteUser(UserModel model)
        {
            var response = await client.PutAsJsonAsync($"User/Remove", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<UserModel>>();
            return result;
        }
        public async Task<ResponseBase<AuthenticatedResponse>> RegisterUser(UserModel model)
        {
            var response = await client.PutAsJsonAsync($"UserAuth/Registration", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<AuthenticatedResponse>>();
            return result;
        }
        public async Task<ResponseBase<UserModel>> UpdateUser(UserModel model)
        {
            var response = await client.PutAsJsonAsync($"User/Update", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<UserModel>>();
            return result;
        }      
        public async Task<ResponseBase<IEnumerable<CustomerModel>>> FilterCustomers(FiltrationModel filtrationModel)
        {
            var response = await client.PostAsJsonAsync($"Customer/CustomerFiltration", filtrationModel);
            var result = await response.Content.ReadAsAsync<ResponseBase<IEnumerable<CustomerModel>>>();
            return result;
        }

        public async Task<ResponseBase<PhoneModel>> AddPhone(PhoneModel model)
        {
            var response = await client.PutAsJsonAsync($"Phone/Post", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<PhoneModel>>();
            return result;
        }

        public async Task<ResponseBase<PhoneModel>> UpdatePhone(PhoneModel model)
        {
            var response = await client.PutAsJsonAsync($"Phone/Update", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<PhoneModel>>();
            return result;
        }

        public async Task<ResponseBase<PhoneModel>> DeletePhone(PhoneModel model)
        {
            var response = await client.PutAsJsonAsync($"Phone/Remove", model);
            var result = await response.Content.ReadAsAsync<ResponseBase<PhoneModel>>();
            return result;
        }

        public async Task<ResponseBase<PhoneModel>> GetPhoneByUserId(int id)
        {
            var response = await client.GetAsync($"Phone/GetByUserId?userid={id}");
            var result = await response.Content.ReadAsAsync<ResponseBase<PhoneModel>>();
            return result;
        }
    }
}