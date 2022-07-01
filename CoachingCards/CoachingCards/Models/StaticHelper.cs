using Plugin.LocalNotification;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CoachingCards.Models
{
    public static class StaticHelper
    {
        public static int CurrentDeckId
        {
            get => Preferences.Get(nameof(CurrentDeckId), 0);
            set => Preferences.Set(nameof(CurrentDeckId), value);
        }

        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

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

        public static string GameModeToString(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Full:
                    return "Všechny karty";
                case GameMode.LeftHemisphere:
                    return "Levá hemisféra";
                case GameMode.RightHemisphere:
                    return "Pravá hemisféra";
                default:
                    return null;
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

        public static async Task ScheduleNotif()
        {
            NotificationTime = DateTime.Now.AddSeconds(30);

            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Title = "Koučovací karty",
                Description = "Jaká bude tvá dnešní karta?",
                NotificationId = 1,
                Schedule = new NotificationRequestSchedule
                {
                    RepeatType = NotificationRepeat.TimeInterval,
                    NotifyRepeatInterval = new TimeSpan(24, 0, 0),
                    NotifyTime = NotificationTime
                }
            };
            await NotificationCenter.Current.Show(notification);
        }

        public static async Task RescheduleNotif(DateTime scheduledTime)
        {
            NotificationCenter.Current.CancelAll();

            NotificationTime = scheduledTime;

            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Title = "Koučovací karty",
                Description = "Jaká bude tvá dnešní karta?",
                NotificationId = 1,
                Schedule = new NotificationRequestSchedule
                {
                    RepeatType = NotificationRepeat.TimeInterval,
                    NotifyRepeatInterval = new TimeSpan(24, 0, 0),
                    NotifyTime = NotificationTime
                }
            };

            await NotificationCenter.Current.Show(notification);
        }

        public static void CancelNotif()
        {
            NotificationCenter.Current.CancelAll();
        }
    }
}