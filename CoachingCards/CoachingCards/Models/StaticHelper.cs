using Xamarin.Essentials;

namespace CoachingCards.Models
{
    public static class StaticHelper
    {
        public static GameMode Mode;

        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

        public static int CurrentCard
        {
            get => Preferences.Get(nameof(CurrentCard), 0);
            set => Preferences.Set(nameof(CurrentCard), value);
        }
    }
}
