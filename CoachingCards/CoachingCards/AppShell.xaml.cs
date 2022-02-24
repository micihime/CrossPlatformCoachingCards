using CoachingCards.Models;
using System;
using Xamarin.Forms;

namespace CoachingCards
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(DeckPage), typeof(DeckPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
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
