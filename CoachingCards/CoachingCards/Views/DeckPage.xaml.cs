using CoachingCards.ViewModels;
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
            base.OnAppearing();
            await vm.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnDisappearing();
        }
    }
}