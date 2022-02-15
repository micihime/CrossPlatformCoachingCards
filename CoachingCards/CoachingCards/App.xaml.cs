using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoachingCards
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            VersionTracking.Track();
            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
