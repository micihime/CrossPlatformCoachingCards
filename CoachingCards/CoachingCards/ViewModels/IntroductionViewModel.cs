using CoachingCards.Models;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class IntroductionViewModel : BindableObject
    {
        #region PRIVATE FIELDS

        private List<Intro> introPages;
        private int pageNr;

        private string heading;
        private string paragraph1;
        private string paragraph2;
        private string paragraph3;
        private bool canGoNext;
        private bool canGoPrevious;
        #endregion

        #region PROPERTIES

        public string Heading
        {
            get => heading;
            set
            {
                if (value == heading)
                    return;

                heading = value;
                OnPropertyChanged();
            }
        }

        public string Paragraph1
        {
            get => paragraph1;
            set
            {
                if (value == paragraph1)
                    return;

                paragraph1 = value;
                OnPropertyChanged();
            }
        }

        public string Paragraph2
        {
            get => paragraph2;
            set
            {
                if (value == paragraph2)
                    return;

                paragraph2 = value;
                OnPropertyChanged();
            }
        }

        public string Paragraph3
        {
            get => paragraph3;
            set
            {
                if (value == paragraph3)
                    return;

                paragraph3 = value;
                OnPropertyChanged();
            }
        }

        public bool NextVisible
        {
            get => canGoNext;
            set
            {
                if (value == canGoNext)
                    return;

                canGoNext = value;
                OnPropertyChanged();
            }
        }

        public bool PreviousVisible
        {
            get => canGoPrevious;
            set
            {
                if (value == canGoPrevious)
                    return;

                canGoPrevious = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region COMMANDS

        public ICommand Next { get; }

        public ICommand Previous { get; }
        #endregion

        #region CONSTRUCTOR

        public IntroductionViewModel()
        {
            introPages = IntroList.GetIntro();

            pageNr = 0;
            Heading = introPages[pageNr].Heading;
            Paragraph1 = introPages[pageNr].Paragraph1;
            Paragraph2 = introPages[pageNr].Paragraph2;
            Paragraph3 = introPages[pageNr].Paragraph3;

            NextVisible = CanGoNext();
            PreviousVisible = CanGoPrevious();

            Next = new Command(OnNext, CanGoNext);
            Previous = new Command(OnPrevious, CanGoPrevious);
        }
        #endregion

        #region COMMANDS IMPLEMENTATION

        bool CanGoNext()
        {
            return (pageNr == (introPages.Count - 1)) ? false : true;
        }

        bool CanGoPrevious()
        {
            return (pageNr == 0) ? false : true;
        }

        void OnNext()
        {
            if (CanGoNext())
            {
                pageNr++;
                Heading = introPages[pageNr].Heading;
                Paragraph1 = introPages[pageNr].Paragraph1;
                Paragraph2 = introPages[pageNr].Paragraph2;
                Paragraph3 = introPages[pageNr].Paragraph3;

                NextVisible = CanGoNext();
                PreviousVisible = CanGoPrevious();
            }
        }

        void OnPrevious()
        {
            if (CanGoPrevious())
            {
                pageNr--;
                Heading = introPages[pageNr].Heading;
                Paragraph1 = introPages[pageNr].Paragraph1;
                Paragraph2 = introPages[pageNr].Paragraph2;
                Paragraph3 = introPages[pageNr].Paragraph3;

                NextVisible = CanGoNext();
                PreviousVisible = CanGoPrevious();
            }
        }
        #endregion
    }
}
