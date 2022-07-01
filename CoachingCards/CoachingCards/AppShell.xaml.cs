using CoachingCards.Models;
using CoachingCards.Services;
using Xamarin.Forms;

namespace CoachingCards
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
            if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.Full.ToString().ToLower()))
            {
                CardService.SetCurrentGameMode((int)GameMode.Full);
                StaticHelper.CurrentDeckId = 0;
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.LeftHemisphere.ToString().ToLower()))
            {
                CardService.SetCurrentGameMode((int)GameMode.LeftHemisphere);
                StaticHelper.CurrentDeckId = 0;
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.RightHemisphere.ToString().ToLower()))
            {
                CardService.SetCurrentGameMode((int)GameMode.RightHemisphere);
                StaticHelper.CurrentDeckId = 0;
            }
        }
    }
}