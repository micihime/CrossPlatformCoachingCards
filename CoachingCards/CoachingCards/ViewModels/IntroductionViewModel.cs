using CoachingCards.Models;
using CoachingCards.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class IntroductionViewModel : BaseViewModel
    {
        #region PRIVATE FIELDS

        private readonly List<Intro> introPages;
        private int pageNr;

        private string heading;
        private string paragraph1;
        private string paragraph2;
        private string paragraph3;
        private string paragraph4;
        private string paragraph5;
        private bool canGoNext;
        private bool canGoPrevious;
        #endregion

        #region PROPERTIES

        public string Heading
        {
            get => heading;
            set => SetProperty(ref heading, value);
        }

        public string Paragraph1
        {
            get => paragraph1;
            set => SetProperty(ref paragraph1, value);
        }

        public string Paragraph2
        {
            get => paragraph2;
            set => SetProperty(ref paragraph2, value);
        }

        public string Paragraph3
        {
            get => paragraph3;
            set => SetProperty(ref paragraph3, value);
        }

        public string Paragraph4
        {
            get => paragraph4;
            set => SetProperty(ref paragraph4, value);
        }

        public string Paragraph5
        {
            get => paragraph5;
            set => SetProperty(ref paragraph5, value);
        }

        public bool NextVisible
        {
            get => canGoNext;
            set => SetProperty(ref canGoNext, value);
        }

        public bool PreviousVisible
        {
            get => canGoPrevious;
            set => SetProperty(ref canGoPrevious, value);
        }
        #endregion

        #region COMMANDS

        public ICommand Next { get; }
        public ICommand Previous { get; }
        public AsyncCommand Register { get; }
        #endregion

        #region CONSTRUCTOR

        public IntroductionViewModel()
        {
            introPages = CardService.GetIntro().ToList();

            pageNr = 0;
            LoadPageContent();

            Next = new MvvmHelpers.Commands.Command(OnNext, CanGoNext);
            Previous = new MvvmHelpers.Commands.Command(OnPrevious, CanGoPrevious);
            Register = new AsyncCommand(OnRegister);
        }
        #endregion

        #region COMMANDS IMPLEMENTATION

        bool CanGoNext() { return pageNr != (introPages.Count - 1); }

        bool CanGoPrevious() { return pageNr != 0; }

        void OnNext()
        {
            if (CanGoNext())
            {
                pageNr++;
                LoadPageContent();
            }
        }

        void OnPrevious()
        {
            if (CanGoPrevious())
            {
                pageNr--;
                LoadPageContent();
            }
        }

        void LoadPageContent()
        {
            SetIntroTexts();
            SetButtonVisibility();
        }

        private void SetIntroTexts()
        {
            Heading = introPages[pageNr].Heading;
            Paragraph1 = introPages[pageNr].Paragraph1;
            Paragraph2 = introPages[pageNr].Paragraph2;
            Paragraph3 = introPages[pageNr].Paragraph3;
            Paragraph4 = introPages[pageNr].Paragraph4;
            Paragraph5 = introPages[pageNr].Paragraph5;
        }

        private void SetButtonVisibility()
        {
            NextVisible = CanGoNext();
            PreviousVisible = CanGoPrevious();
        }

        async Task OnRegister() { await Shell.Current.GoToAsync("/RegisterPage"); }
        #endregion
    }
}