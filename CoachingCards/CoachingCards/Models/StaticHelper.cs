using System;
using Xamarin.Essentials;

namespace CoachingCards.Models
{
    public static class StaticHelper
    {
        #region App

        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

        public static int CurrentDeckId
        {
            get => Preferences.Get(nameof(CurrentDeckId), 0);
            set => Preferences.Set(nameof(CurrentDeckId), value);
        }
        #endregion

        #region User

        public static string User
        {
            get => Preferences.Get(nameof(User), "");
            set => Preferences.Set(nameof(User), value);
        }

        public static string Email
        {
            get => Preferences.Get(nameof(Email), "");
            set => Preferences.Set(nameof(Email), value);
        }
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