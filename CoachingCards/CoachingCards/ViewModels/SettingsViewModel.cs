using CoachingCards.Models;
using CoachingCards.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region CONSTANTS

        private const string failureHeading = "UPOZORNENI";
        private const string failureText = "Nepodařilo se vás odhlásit z odběru novinek. Prosíme zkuste to později. V pripade problemov, kontaktujte nas na nasledujucej emailovej adrese: info@iost.cz";
        private const string buttonText = "OK";
        #endregion

        AsyncCommand _saveCommand;
        public AsyncCommand Deregister { get; }

        public AsyncCommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
            set
            {
                SetProperty(ref _saveCommand, value);
            }
        }

        bool _notificationsON;

        public bool NotificationsON
        {
            get
            {
                _notificationsON = StaticHelper.NotificationsON;
                return _notificationsON;
            }
            set
            {
                StaticHelper.NotificationsON = value;
                SetProperty(ref _notificationsON, value);
            }
        }

        public string Name { get => CardService.GetUsername(); }

        public string Email { get => CardService.GetEmail(); }

        TimeSpan _selectedTime;

        public TimeSpan SelectedTime
        {
            get
            {
                _selectedTime = StaticHelper.SelectedNotifTime.TimeOfDay;
                return _selectedTime;
            }
            set
            {
                StaticHelper.SelectedNotifTime = Convert.ToDateTime(value.ToString());
                SetProperty(ref _selectedTime, value);
            }
        }

        public SettingsViewModel()
        {
            SaveCommand = new AsyncCommand(() => SaveLocalNotificationAsync());
            Deregister = new AsyncCommand(OnDeregister);
        }

        async Task SaveLocalNotificationAsync()
        {
            if (NotificationsON)
            {
                var time = Convert.ToDateTime(SelectedTime.ToString());
                await NotificationService.ScheduleNotif(time);
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Notification Settings", $"Notification time set to {Convert.ToDateTime(SelectedTime.ToString()):HH:mm}", "Ok");
            }
            else
            {
                NotificationService.CancelNotif();
            }
        }

        public async Task OnDeregister()
        {
            try
            {
                var response = SubscribeService.Unsubscribe(CardService.GetEmail());

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
