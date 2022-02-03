using System.Windows.Input;

namespace CoachingCards.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
        }

        public ICommand OpenWebCommand { get; }
    }
}