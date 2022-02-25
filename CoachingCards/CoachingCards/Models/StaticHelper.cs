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
    }
}
