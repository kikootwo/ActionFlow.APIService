using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActionFlow.API.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Action = ActionFlow.API.Models.Action;

namespace ActionFlow.API.Controllers
{
    public static class Actions
    {
        private static readonly string _apiUrl = "https://actionflowapimanagement.azure-api.net/v1/api/";
        private static RestClient _client = (RestClient)new RestClient(_apiUrl).UseNewtonsoftJson();
        private static string _resource = "actions";

        #region GET
        public static async Task<List<Action>> GetActionsAsync()
        {
            var request = new RestRequest(_resource, DataFormat.Json);
            List<Action> actions = await _client.GetAsync<List<Action>>(request);

            return actions;
        }
        public static List<Action> GetActions()
        {
            var request = new RestRequest(_resource, DataFormat.Json);
            var response = _client.Get<List<Action>>(request);

            return response.Data;
        }
        #endregion

        #region POST
        public static async Task<Action> AddActionAsync(Action action)
        {
            var request = new RestRequest(_resource, Method.POST);
            request.AddJsonBody(action);

            Action t = await _client.PostAsync<Action>(request);

            return t;
        }
        public static Action AddAction(Action action)
        {
            var request = new RestRequest(_resource, Method.POST);
            request.AddJsonBody(action);

            var response = _client.Post<Action>(request);

            return response.Data;
        }
        #endregion

        #region DELETE

        public static async Task<Action> DeleteActionAsync(Guid guid)
        {
            var request = new RestRequest(_resource + "/{id}", DataFormat.Json)
                .AddParameter("id", guid.ToString(), ParameterType.UrlSegment);

            Action action = await _client.DeleteAsync<Action>(request);

            return action;
        }
        public static Action DeleteAction(Guid guid)
        {
            var request = new RestRequest(_resource + "/{id}", DataFormat.Json)
                .AddParameter("id", guid.ToString(), ParameterType.UrlSegment);

            var response = _client.Delete<Action>(request);

            return response.Data;
        }
        #endregion

        #region PUT

        public static async Task<Action> UpdateActionAsync(Action action)
        {
            var request = new RestRequest(_resource + "/{id}", Method.PUT)
                .AddParameter("id", action.Guid.ToString(), ParameterType.UrlSegment)
                .AddJsonBody(action);


            Action c = await _client.PutAsync<Action>(request);

            return c;
        }
        public static Action UpdateAction(Action action)
        {
            var request = new RestRequest(_resource + "/{id}", Method.PUT)
                .AddParameter("id", action.Guid.ToString(), ParameterType.UrlSegment)
                .AddJsonBody(action);


            var response = _client.Put<Action>(request);

            return response.Data;
        }

        #endregion

    }
}
