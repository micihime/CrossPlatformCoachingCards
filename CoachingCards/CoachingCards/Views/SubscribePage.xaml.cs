using CoachingCards.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoachingCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubscribePage : ContentPage
    {
        SubscribeViewModel vm;

        public SubscribePage()
        {
            InitializeComponent();
            BindingContext = vm = new SubscribeViewModel();
        }
    }
}