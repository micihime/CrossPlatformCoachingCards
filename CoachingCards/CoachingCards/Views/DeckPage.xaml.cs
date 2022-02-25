using CoachingCards.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoachingCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckPage : ContentPage
    {
        DeckViewModel vm;

        public DeckPage()
        {
            InitializeComponent();
            BindingContext = vm = new DeckViewModel();
        }

        protected override async void OnAppearing()
        {
            //Accelerometer.ShakeDetected += this.OnShaked;
            Accelerometer.Start(SensorSpeed.Default);
            base.OnAppearing();
            await vm.FirstRunCommand.ExecuteAsync();
        }

        protected override void OnDisappearing()
        {
            Accelerometer.Stop();
            //Accelerometer.ShakeDetected -= this.OnShaked;
            base.OnDisappearing();
        }
    }
}