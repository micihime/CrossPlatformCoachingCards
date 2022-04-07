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

        //public static int CurrentDeckId
        //{
        //    get => Preferences.Get(nameof(CurrentDeckId), 0);
        //    set => Preferences.Set(nameof(CurrentDeckId), value);
        //}

        //public static int MinDeckId
        //{
        //    get => Preferences.Get(nameof(MinDeckId), 0);
        //    set => Preferences.Set(nameof(MinDeckId), value);
        //}

        //public static int MaxDeckId
        //{
        //    get => Preferences.Get(nameof(MaxDeckId), 0);
        //    set => Preferences.Set(nameof(MaxDeckId), value);
        //}
    }
}
