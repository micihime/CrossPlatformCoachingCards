using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoachingCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckPage : ContentPage
    {
        public DeckPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //Accelerometer.ShakeDetected += this.OnShaked;
            Accelerometer.Start(SensorSpeed.Default);
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Accelerometer.Stop();
            //Accelerometer.ShakeDetected -= this.OnShaked;
            base.OnDisappearing();
        }

        //private void OnShaked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // your actions on shake go here, eg open a page 
        //        onshakeDetect.Text = "SHAKED!";
        //    }
        //    catch (Exception ex)
        //    {
        //        // your exception handler here
        //    }
        //}
    }
}