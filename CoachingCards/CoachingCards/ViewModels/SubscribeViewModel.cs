using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoachingCards.ViewModels
{
    public class SubscribeViewModel : BaseViewModel
    {
        #region CONSTANTS

        private const string api = "https://api.mailerlite.com";
        private const string apiEndpoint = "/api/v2/subscribers";
        private const string apiMediaType = "application/json";
        private const string apiTokenKey = "x-mailerlite-apikey";
        private const string apiToken = "1a2bf25bdfc8ea00654ec4e9c7ef5fd5";
        private const string successHeading = "Děkujeme!";
        private const string successText = "Přihlášení k odběru novinek bylo úspěšné.";
        private const string failureHeading = "UPOZORNENI";
        private const string failureText = "Nepodařilo se vás přihlásit k odběru novinek. Prosíme zkuste to později.";
        private const string buttonText = "OK";
        #endregion

        #region PRIVATE FIELDS

        private string name;
        private string email;
        #endregion

        #region PROPERTIES

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        #endregion

        #region DECLARING COMMANDS

        public AsyncCommand Subscribe { get; }
        #endregion

        public SubscribeViewModel()
        {
            Subscribe = new AsyncCommand(OnSubscribe);
        }

        public async Task OnSubscribe()
        {
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(api) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiMediaType));
                client.DefaultRequestHeaders.Add(apiTokenKey, apiToken);

                var values = new Dictionary<string, string> { { "name", name }, { "email", email } };
                var json = JsonConvert.SerializeObject(values);
                var content = new StringContent(json.ToString(), Encoding.UTF8, apiMediaType);

                var response = client.PostAsync(apiEndpoint, content).Result;
                string message = $"Status code: {response.StatusCode}\nReason phrase: {response.ReasonPhrase}\nContent: {response.Content}\n";

                if (response.IsSuccessStatusCode)
                    await App.Current.MainPage.DisplayAlert(successHeading, successText, buttonText);
                else
                    await App.Current.MainPage.DisplayAlert(failureHeading, message, buttonText);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert(failureHeading, e.Message + e.InnerException, buttonText);
            }
        }
    }
}