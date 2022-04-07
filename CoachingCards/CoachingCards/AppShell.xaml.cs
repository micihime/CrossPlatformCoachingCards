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
                StaticHelper.Mode = GameMode.Full;
                CardService.SetCurrentDeckId(0);
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.LeftHemisphere.ToString().ToLower()))
            {
                StaticHelper.Mode = GameMode.LeftHemisphere;
                CardService.SetCurrentDeckId(0);
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.RightHemisphere.ToString().ToLower()))
            {
                StaticHelper.Mode = GameMode.RightHemisphere;
                CardService.SetCurrentDeckId(0);
            }
        }
    }
}
