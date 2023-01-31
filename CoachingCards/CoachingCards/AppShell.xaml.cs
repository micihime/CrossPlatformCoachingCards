using CoachingCards.Models;
using CoachingCards.Services;
using CoachingCards.Views;
using Xamarin.Forms;

namespace CoachingCards
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("IntroductionPage", typeof(IntroductionPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            //Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
            if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.Full.ToString().ToLower()))
            {
                CardService.SetCurrentGameMode((int)GameMode.Full);
                CardService.SetCurrentDeckId(0);
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.LeftHemisphere.ToString().ToLower()))
            {
                CardService.SetCurrentGameMode((int)GameMode.LeftHemisphere);
                CardService.SetCurrentDeckId(0);
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.RightHemisphere.ToString().ToLower()))
            {
                CardService.SetCurrentGameMode((int)GameMode.RightHemisphere);
                CardService.SetCurrentDeckId(0);
            }
        }
    }
}