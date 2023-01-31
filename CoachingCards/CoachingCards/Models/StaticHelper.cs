using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoachingCards.Models
{
    public static class StaticHelper
    {
        #region CONSTANTS

        private const string failureHeading = "UPOZORNENI";
        private const string noInternetText = "Nemáte pripojeni na internet. Pro pokracovani je pripojeni k internetu nevyhnutne.";
        private const string buttonText = "OK";
        #endregion

        #region App

        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

        //public static int CurrentDeckId
        //{
        //    get => Preferences.Get(nameof(CurrentDeckId), 0);
        //    set => Preferences.Set(nameof(CurrentDeckId), value);
        //}
        #endregion

        #region Settings

        public static bool NotificationsON
        {
            get => Preferences.Get(nameof(NotificationsON), true);
            set => Preferences.Set(nameof(NotificationsON), value);
        }

        public static DateTime SelectedNotifTime
        {
            get => Preferences.Get(nameof(SelectedNotifTime), new DateTime());
            set => Preferences.Set(nameof(SelectedNotifTime), value);
        }

        public static DateTime NotificationTime
        {
            get => Preferences.Get(nameof(NotificationTime), new DateTime());
            set => Preferences.Set(nameof(NotificationTime), value);
        }
        #endregion

        public static async Task CheckInternetConnection()
        {
            //check for internet access
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert(failureHeading, noInternetText, buttonText);
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
        }

        public static string GameModeToString(int mode)
        {
            switch (mode)
            {
                case (int)GameMode.Full:
                    return "Všechny karty";
                case (int)GameMode.LeftHemisphere:
                    return "Levá hemisféra";
                case (int)GameMode.RightHemisphere:
                    return "Pravá hemisféra";
                default:
                    return null;
            }
        }
    }
}