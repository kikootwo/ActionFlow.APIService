using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActionFlow.API.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace ActionFlow.API.Controllers
{
    public static class Jobs
    {
        private static readonly string _apiUrl = "https://actionflowapimanagement.azure-api.net/v1/api/";
        private static RestClient _client = (RestClient)new RestClient(_apiUrl).UseNewtonsoftJson();
        private static string _resource = "jobs";

        #region GET
        public static async Task<List<Job>> GetJobsAsync()
        {
            var request = new RestRequest(_resource, DataFormat.Json);
            List<Job> jobs = await _client.GetAsync<List<Job>>(request);

            return jobs;
        }
        public static List<Job> GetJobs()
        {
            var request = new RestRequest(_resource, DataFormat.Json);
            var response = _client.Get<List<Job>>(request);

            return response.Data;
        }
        #endregion

        #region POST
        public static async Task<Job> AddJobAsync(Job job)
        {
            var request = new RestRequest(_resource, Method.POST);
            request.AddJsonBody(job);

            Job t = await _client.PostAsync<Job>(request);

            return t;
        }
        public static Job AddJob(Job job)
        {
            var request = new RestRequest(_resource, Method.POST);
            request.AddJsonBody(job);

            var response = _client.Post<Job>(request);

            return response.Data;
        }
        #endregion

        #region DELETE

        public static async Task<Job> DeleteJobAsync(Guid guid)
        {
            var request = new RestRequest(_resource + "/{id}", DataFormat.Json)
                .AddParameter("id", guid.ToString(), ParameterType.UrlSegment);

            Job job = await _client.DeleteAsync<Job>(request);

            return job;
        }
        public static Job DeleteJob(Guid guid)
        {
            var request = new RestRequest(_resource + "/{id}", DataFormat.Json)
                .AddParameter("id", guid.ToString(), ParameterType.UrlSegment);

            var response = _client.Delete<Job>(request);

            return response.Data;
        }
        #endregion

        #region PUT

        public static async Task<Job> UpdateJobAsync(Job job)
        {
            var request = new RestRequest(_resource + "/{id}", Method.PUT)
                .AddParameter("id", job.Guid.ToString(), ParameterType.UrlSegment)
                .AddJsonBody(job);


            Job c = await _client.PutAsync<Job>(request);

            return c;
        }
        public static Job UpdateJob(Job job)
        {
            var request = new RestRequest(_resource + "/{id}", Method.PUT)
                .AddParameter("id", job.Guid.ToString(), ParameterType.UrlSegment)
                .AddJsonBody(job);


            var response = _client.Put<Job>(request);

            return response.Data;
        }

        #endregion

    }
}
