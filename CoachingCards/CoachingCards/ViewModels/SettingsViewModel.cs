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

        bool _subscribeNotifON;
        public bool SubscribeNotifON
        {
            get
            {
                _subscribeNotifON = StaticHelper.SubscribeNotifON;
                return _subscribeNotifON;
            }
            set
            {
                StaticHelper.SubscribeNotifON = value;
                SetProperty(ref _subscribeNotifON, value);
                //Switch_Toggled();
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

            if (SubscribeNotifON)
            {
                //TODO:
            }
        }

        //void Switch_Toggled()
        //{
        //    if (!NotificationsON)
        //        SelectedTime = DateTime.Now.TimeOfDay;
        //}
    }
}
