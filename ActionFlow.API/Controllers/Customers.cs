using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActionFlow.API.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace ActionFlow.API.Controllers
{
    public static class Customers
    {
        private static readonly string _apiUrl = "https://actionflowapimanagement.azure-api.net/v1/api/";
        private static RestClient _client = (RestClient)new RestClient(_apiUrl).UseNewtonsoftJson();
        private static string _resource = "customers";

        #region GET
        public static async Task<List<Customer>> GetCustomersAsync()
        {
            var request = new RestRequest(_resource, DataFormat.Json);
            List<Customer> customers = await _client.GetAsync<List<Customer>>(request);

            return customers;
        }
        public static List<Customer> GetCustomers()
        {
            var request = new RestRequest(_resource, DataFormat.Json);
            var response = _client.Get<List<Customer>>(request);

            return response.Data;
        }
        #endregion

        #region POST
        public static async Task<Customer> AddCustomerAsync(Customer customer)
        {
            var request = new RestRequest(_resource, Method.POST);
            request.AddJsonBody(customer);

            Customer t = await _client.PostAsync<Customer>(request);

            return t;
        }
        public static Customer AddCustomer(Customer customer)
        {
            var request = new RestRequest(_resource, Method.POST);
            request.AddJsonBody(customer);

            var response = _client.Post<Customer>(request);

            return response.Data;
        }
        #endregion

        #region DELETE

        public static async Task<Customer> DeleteCustomerAsync(Guid guid)
        {
            var request = new RestRequest(_resource + "/{id}", DataFormat.Json)
                .AddParameter("id", guid.ToString(), ParameterType.UrlSegment);

            Customer customer = await _client.DeleteAsync<Customer>(request);

            return customer;
        }
        public static Customer DeleteCustomer(Guid guid)
        {
            var request = new RestRequest(_resource + "/{id}", DataFormat.Json)
                .AddParameter("id", guid.ToString(), ParameterType.UrlSegment);

            var response = _client.Delete<Customer>(request);

            return response.Data;
        }
        #endregion

        #region PUT

        public static async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var request = new RestRequest(_resource + "/{id}", Method.PUT)
                .AddParameter("id", customer.Guid.ToString(), ParameterType.UrlSegment)
                .AddJsonBody(customer);


            Customer c = await _client.PutAsync<Customer>(request);

            return c;
        }
        public static Customer UpdateCustomer(Customer customer)
        {
            var request = new RestRequest(_resource + "/{id}", Method.PUT)
                .AddParameter("id", customer.Guid.ToString(), ParameterType.UrlSegment)
                .AddJsonBody(customer);


            var response = _client.Put<Customer>(request);

            return response.Data;
        }

        #endregion

    }
}
