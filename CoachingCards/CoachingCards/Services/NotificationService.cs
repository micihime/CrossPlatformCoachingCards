using CoachingCards.Models;
using Plugin.LocalNotification;
using System;
using System.Threading.Tasks;

namespace CoachingCards.Services
{
    public static class NotificationService
    {
        public static async Task ScheduleNotif(DateTime scheduledTime)
        {
            NotificationCenter.Current.CancelAll();

            StaticHelper.NotificationTime = scheduledTime;

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
                    NotifyTime = StaticHelper.NotificationTime
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
