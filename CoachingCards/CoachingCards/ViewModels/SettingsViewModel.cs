using CoachingCards.Services;
using MvvmHelpers;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        MvvmHelpers.Commands.Command _saveCommand;
        public MvvmHelpers.Commands.Command SaveCommand
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

        bool _notificationONOFF;
        public bool NotificationONOFF
        {
            get
            {
                return _notificationONOFF;
            }
            set
            {
                SetProperty(ref _notificationONOFF, value);
                Switch_Toggled();
            }
        }
        void Switch_Toggled()
        {
            if (NotificationONOFF == false)
            {
                MessageText = string.Empty;
                SelectedTime = DateTime.Now.TimeOfDay;
                SelectedDate = DateTime.Today;
                //DependencyService.Get<ILocalNotificationService>().Cancel(0);
            }
        }

        DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        TimeSpan _selectedTime = DateTime.Now.TimeOfDay;
        public TimeSpan SelectedTime
        {
            get
            {
                return _selectedTime;
            }
            set
            {
                SetProperty(ref _selectedTime, value);
            }
        }

        string _messageText;
        public string MessageText
        {
            get
            {
                return _messageText;
            }
            set
            {
                SetProperty(ref _messageText, value);
            }
        }
        public SettingsViewModel()
        {
            SaveCommand = new MvvmHelpers.Commands.Command(() => SaveLocalNotification());
        }
        void SaveLocalNotification()
        {
            if (NotificationONOFF == true)
            {
                var date = (SelectedDate.Date.Month.ToString("00") + "-" + SelectedDate.Date.Day.ToString("00") + "-" + SelectedDate.Date.Year.ToString());
                var time = Convert.ToDateTime(SelectedTime.ToString()).ToString("HH:mm");
                var dateTime = date + " " + time;
                var selectedDateTime = DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(MessageText))
                {
                    //DependencyService.Get<ILocalNotificationService>().Cancel(0);
                    //DependencyService.Get<ILocalNotificationService>().LocalNotification("Local Notification", MessageText, 0, selectedDateTime);
                    App.Current.MainPage.DisplayAlert("LocalNotificationDemo", "Notification details saved successfully ", "Ok");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("LocalNotificationDemo", "Please enter meassage", "OK");
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("LocalNotificationDemo", "Please switch on notification", "OK");
            }
        }
    }
}
