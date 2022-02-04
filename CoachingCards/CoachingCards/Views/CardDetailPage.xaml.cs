using CoachingCards.ViewModels;
using Xamarin.Forms;

namespace CoachingCards.Views
{
    public partial class CardDetailPage : ContentPage
    {
        public CardDetailPage()
        {
            InitializeComponent();
            BindingContext = new CardDetailViewModel();
        }
    }
}