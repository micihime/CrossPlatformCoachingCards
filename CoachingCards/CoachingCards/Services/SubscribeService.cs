using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CoachingCards.Services
{
    public static class SubscribeService
    {
        #region CONSTANTS

        private const string api = "https://api.mailerlite.com";
        private const string apiMediaType = "application/json";
        private const string apiTokenKey = "x-mailerlite-apikey";
        private const string apiToken = "1a2bf25bdfc8ea00654ec4e9c7ef5fd5";

        private const string apiEndpointSubscribe = "/api/v2/subscribers"; //https://developers.mailerlite.com/reference/create-a-subscriber
        private const string apiEndpointSubscribeToGroup = "/api/v2/groups/group_name/subscribers"; //https://developers.mailerlite.com/reference/groupsby_group_namesubscribers
        private const string apiEndpointUnsubscribe = "/api/v2/subscribers/"; //https://developers.mailerlite.com/reference/update-subscriber
        //UNSUBSCRIBE - PUT request with email param

        private const string nameKey = "name";
        private const string emailKey = "email";
        private const string groupNameKey = "group_name";
        private const string resubscribeKey = "resubscribe";
        private const string typeKey = "type";
        private const string autorespondersKey = "autoresponders";
        private const string resendAutorespondersKey = "resend_autoresponders";

        private const string unsubscribedVal = "unsubscribed";
        private const string falseVal = "false";
        private const string trueVal = "true";

        private const string appGroup = "app_cesky";
        private const string cardGroupKey = "Koučovací_karta_";
        #endregion

        public static HttpResponseMessage Subscribe(string name, string email)
        {
            var client = new HttpClient { BaseAddress = new Uri(api) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiMediaType));
            client.DefaultRequestHeaders.Add(apiTokenKey, apiToken);

            var values = new Dictionary<string, string> { { nameKey, name }, { emailKey, email }, { resubscribeKey, trueVal } };
            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json.ToString(), Encoding.UTF8, apiMediaType);

            var result = client.PostAsync(apiEndpointSubscribe, content).Result;
            return result;
        }

        public static HttpResponseMessage SubscribeToAppGroup(string name, string email)
        {
            var client = new HttpClient { BaseAddress = new Uri(api) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiMediaType));
            client.DefaultRequestHeaders.Add(apiTokenKey, apiToken);

            var values = new Dictionary<string, string> { { nameKey, name }, { emailKey, email }, { groupNameKey, appGroup },
                { resubscribeKey, trueVal }, { autorespondersKey, trueVal } };
            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json.ToString(), Encoding.UTF8, apiMediaType);

            var result = client.PostAsync(apiEndpointSubscribeToGroup, content).Result;
            return result;
        }

        public static HttpResponseMessage SubscribeToCardGroup(string name, string email, int cardNumber)
        {
            var client = new HttpClient { BaseAddress = new Uri(api) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiMediaType));
            client.DefaultRequestHeaders.Add(apiTokenKey, apiToken);

            var values = new Dictionary<string, string> { { nameKey, name }, { emailKey, email }, {groupNameKey, $"{cardGroupKey}{cardNumber}" },
                { resubscribeKey, trueVal }, { autorespondersKey, trueVal } };
            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json.ToString(), Encoding.UTF8, apiMediaType);

            var result = client.PostAsync(apiEndpointSubscribeToGroup, content).Result;
            return result;
        }

        //UNSUBSCRIBE - PUT request with email param
        public static HttpResponseMessage Unsubscribe(string email)
        {
            var client = new HttpClient { BaseAddress = new Uri(api) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiMediaType));
            client.DefaultRequestHeaders.Add(apiTokenKey, apiToken);

            var values = new Dictionary<string, string> { { typeKey, unsubscribedVal }, { resendAutorespondersKey, falseVal } };
            var json = JsonConvert.SerializeObject(values);
            var content = new StringContent(json.ToString(), Encoding.UTF8, apiMediaType);

            string apiEndpoint = $"{apiEndpointUnsubscribe}{email.Replace("@", $"%40")}";
            var result = client.PutAsync(apiEndpoint, content).Result;
            return result;
        }

        public static HttpResponseMessage UnsubscribeFromAll(string email)
        {
            //foreach
            //  UNSUBSCRIBE - PUT request with email param

            throw new NotImplementedException();
        }
    }
}