using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoachingCards.Services
{
    public static class SubscribeService
    {
        #region CONSTANTS

        private const string api = "https://api.mailerlite.com";
        private const string apiEndpoint = "/api/v2/subscribers";
        private const string apiMediaType = "application/json";
        private const string apiTokenKey = "x-mailerlite-apikey";
        private const string apiToken = "1a2bf25bdfc8ea00654ec4e9c7ef5fd5";
        private const string nameKey = "name";
        private const string emailKey = "email";
        private const string groupNameKey = "group_name";
        private const string groupNameValue = "Aplikace Koučovací karty česky";
        private const string cardGroupKey = "Koučovací_karta_";
        #endregion

        public static Task<HttpResponseMessage> Subscribe(string name, string email)
        {
            var client = new HttpClient { BaseAddress = new Uri(api) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiMediaType));
            client.DefaultRequestHeaders.Add(apiTokenKey, apiToken);

            var values = new Dictionary<string, string> { { nameKey, name }, { emailKey, email }, { groupNameKey, groupNameValue } };
            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json.ToString(), Encoding.UTF8, apiMediaType);

            return client.PostAsync(apiEndpoint, content);
        }

        public static Task<HttpResponseMessage> CardSubscribe(string name, string email, int cardNumber)
        {
            var client = new HttpClient { BaseAddress = new Uri(api) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiMediaType));
            client.DefaultRequestHeaders.Add(apiTokenKey, apiToken);

            var values = new Dictionary<string, string> { { nameKey, name }, { emailKey, email }, { groupNameKey, $"{cardGroupKey}{cardNumber}" } };
            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json.ToString(), Encoding.UTF8, apiMediaType);

            return client.PostAsync(apiEndpoint, content);
        }
    }
}