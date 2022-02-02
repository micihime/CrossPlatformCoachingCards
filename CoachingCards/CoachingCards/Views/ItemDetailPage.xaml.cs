using CoachingCards.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CoachingCards.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}