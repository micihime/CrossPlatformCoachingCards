using CoachingCards.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class DeregisterViewModel : BaseViewModel
    {
        #region CONSTANTS

        private const string failureHeading = "UPOZORNENI";
        private const string failureText = "Nepodařilo se vás odhlásit z odběru novinek. Prosíme zkuste to později. V pripade problemov, kontaktujte nas na nasledujucej emailovej adrese: info@iost.cz";
        private const string buttonText = "OK";
        #endregion

        #region DECLARING COMMANDS

        public AsyncCommand Deregister { get; }
        #endregion

        public DeregisterViewModel()
        {
            Deregister = new AsyncCommand(OnDeregister);
        }

        public async Task OnDeregister()
        {
            try
            {
                var response = await SubscribeService.UnsubscribeFromAll();

                if (response.IsSuccessStatusCode)
                    await Shell.Current.GoToAsync("///IntroductionPage");
                else
                    await App.Current.MainPage.DisplayAlert(failureHeading, $"Status code: {response.StatusCode}\nReason phrase: {response.ReasonPhrase}\nContent: {response.Content.ReadAsStreamAsync()}\n", buttonText);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert(failureHeading, e.Message + e.InnerException, buttonText);
            }
        }
    }
}