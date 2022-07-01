using CoachingCards.Models;
using MvvmHelpers;
using System;
using System.Threading.Tasks;

namespace CoachingCards.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        MvvmHelpers.Commands.AsyncCommand _saveCommand;

        public MvvmHelpers.Commands.AsyncCommand SaveCommand
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
            SaveCommand = new MvvmHelpers.Commands.AsyncCommand(() => SaveLocalNotificationAsync());
        }

        async Task SaveLocalNotificationAsync()
        {
            if (NotificationsON)
            {
                var time = Convert.ToDateTime(SelectedTime.ToString());
                await StaticHelper.RescheduleNotif(time);
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Notification Settings", $"Notification time set to {Convert.ToDateTime(SelectedTime.ToString()):HH:mm}", "Ok");
            }
            else
            {
                StaticHelper.CancelNotif();
            }
        }
    }
}
