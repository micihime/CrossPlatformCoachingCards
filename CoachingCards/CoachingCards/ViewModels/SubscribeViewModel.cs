﻿using MvvmHelpers;
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
        private const string apiToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI0IiwianRpIjoiYzg2NDM3Y2Q4MDYwOTU0NGY1YjVmMjBlMDI3NjMwNTc5ZTExZTYyZmU1ZDA1MDU2ZmJjYzIxZGNmZjc0NDkyZGFlODNiOTFiZWI1N2ZmMjgiLCJpYXQiOjE2NTI2OTE2MDEuNDkwMTI1LCJuYmYiOjE2NTI2OTE2MDEuNDkwMTMsImV4cCI6NDgwODM2NTIwMS40ODQyMzMsInN1YiI6IjU2ODM4Iiwic2NvcGVzIjpbXX0.tyRAyXul_26ps_dwHAkWQYaf5DBHQ6OH9KiSbIgrZTo5DiByU1uSiG70jrbJbd206LAHcf4gQkyNtteiTUtvGUqNqnq_luX5F5IWyuNLYWBewKOw2J-u1-qpKuS-STZXp22yOXGhFr5nPcfM7gu2JAUNGIrKmKUG8qIY9TivtrCv1wCadAPGprnp4jCU-kgNg2s4a33AISKNNCGhPFQ3B_nrtjDm4JtAV3UyxeZjU4vVaObXx3nBKF_--T8EE-rf2PIW2bgRtV0Vj8NioP66YohY9RZmaLWFGaKpkGILp2OghvJfP2WzySJ3myj-rjXjabcDrTAW7A9_3lY_XDHbvoLBbgfLVT290KH4nv0gkLLTmfL4D4V40ST_Gh2GHUQRj5TuOhzdoHnQnOMg8WDptfGFIWhTL6B2SkqdmXZMgSIoBndnT7PyetG3LSdImxsO4v9Ivn2gwDLu_Q6r7z2UvX73Z5meTXYNMGKyC0RG_iOdisYL5jKcOu-QEF8WqvHq0Dn5YBkfUpseeYoypes-FI2LM4gXrgLEAn_D-pilJhDXJHWh61NFu1kmpaEYJ5QG9XqZ4AystIHxxrL6kmQIHKzlg_9MBvSaL3R7X9aI4WxXYUUpXhU9eax24ug8tINbBG60lbRB4O-4mH04nvQATM-qsRghYCh_c38zR-vE6Xs";

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

                if (response.IsSuccessStatusCode)
                    await App.Current.MainPage.DisplayAlert(successHeading, successText, buttonText);
                else
                    await App.Current.MainPage.DisplayAlert(failureHeading, failureText, buttonText);
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert(failureHeading, failureText, buttonText);
            }
        }
    }
}