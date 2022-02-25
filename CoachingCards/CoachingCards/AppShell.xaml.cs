using CoachingCards.Models;
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
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.LeftHemisphere.ToString().ToLower()))
            {
                StaticHelper.Mode = GameMode.LeftHemisphere;
            }
            else if (args.Target.Location.OriginalString.ToLower().Contains(GameMode.RightHemisphere.ToString().ToLower()))
            {
                StaticHelper.Mode = GameMode.RightHemisphere;
            }
        }
    }
}
