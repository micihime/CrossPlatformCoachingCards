using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class AboutCardsViewModel : BaseViewModel
    {
        public AboutCardsViewModel()
        {
            Title = "About Cards";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.mindartconcept.cz"));
        }

        public ICommand OpenWebCommand { get; }
    }
}