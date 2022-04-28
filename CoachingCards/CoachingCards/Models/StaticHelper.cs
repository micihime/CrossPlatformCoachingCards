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
    }
}
