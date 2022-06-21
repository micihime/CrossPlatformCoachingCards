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
                //Switch_Toggled();
            }
        }

        //void Switch_Toggled()
        //{
        //    if (!NotificationsON)
        //        SelectedTime = DateTime.Now.TimeOfDay;
        //}

        TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get
            {
                _selectedTime = StaticHelper.NotificationTime.TimeOfDay;
                return _selectedTime;
            }
            set
            {
                //set global in RescheduleNotif() method
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
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Notification Settings", $"Notification time set to {Convert.ToDateTime(SelectedTime.ToString()).ToString("HH:mm")}", "Ok");
            }
            else
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Notification Settings", "Please switch on notification", "OK");
        }
    }
}
