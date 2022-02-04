using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Návod";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.mindartconcept.cz"));
        }

        public ICommand OpenWebCommand { get; }
    }
}