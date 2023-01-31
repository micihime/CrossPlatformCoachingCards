using CoachingCards.Models;
using CoachingCards.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region CONSTANTS

        private const string successHeading = "Děkujeme!";
        private const string successText = "Přihlášení k odběru novinek bylo úspěšné.";
        private const string failureHeading = "UPOZORNENI";
        private string failureText = $"Name: {0}\nEmail: {1}\nStatus code: {2}\nReason phrase: {3}\nContent: {4}\n";
        //private const string failureText = "Nepodařilo se vás přihlásit k odběru novinek. Prosíme zkuste to později.";
        private const string noInternetText = "Nemáte pripojeni na internet. Pro pokracovani je pripojeni k internetu nevyhnutne.";
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

        public AsyncCommand Register { get; }
        #endregion

        public RegisterViewModel()
        {
            StaticHelper.CheckInternetConnection();
            Register = new AsyncCommand(OnRegister);
        }

        public async Task OnRegister()
        {
            try
            {
                StaticHelper.CheckInternetConnection();

                //save name and email to DB
                CardService.SetUsername(name);
                CardService.SetEmail(email);

                //subscribe to newsletter
                var response = SubscribeService.SubscribeToAppGroup(name, email);
                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync("///Full");
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(failureHeading, String.Format(failureText, name, email, response.StatusCode, response.ReasonPhrase, response.Content),
                        buttonText); //TODO: replace with original text
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert(failureHeading, e.Message + e.InnerException, buttonText);
            }
        }
    }
}