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
    }
}
