using CoachingCards.Models;
using CoachingCards.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region CONSTANTS

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

        public AsyncCommand Register { get; }
        #endregion

        public RegisterViewModel()
        {
            Register = new AsyncCommand(OnRegister);
        }

        public async Task OnRegister()
        {
            try
            {
                StaticHelper.User = name;
                StaticHelper.Email = email;

                var response = await SubscribeService.SubscribeToGroup(name, email);

                if (response.IsSuccessStatusCode)
                    await Shell.Current.GoToAsync("///Full");
                else
                    await App.Current.MainPage.DisplayAlert(failureHeading, $"Name: {name}\nEmail: {email}\nStatus code: {response.StatusCode}\nReason phrase: {response.ReasonPhrase}\nContent: {response.Content.ReadAsStreamAsync()}\n", buttonText);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert(failureHeading, e.Message + e.InnerException, buttonText);
            }
        }
    }
}