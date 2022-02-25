using CoachingCards.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    [QueryProperty(nameof(Parameter), nameof(Parameter))]
    public class DeckViewModel : BaseViewModel
    {
        public string Parameter { get; set; }

        #region DECK RESOURCES

        private const string emptyDeckBackgroundImage = "empty.png";
        private const string backgroundImage = "back.png";
        private const string backgroundImageLeft = "backgroundL.png";
        private const string backgroundImageRight = "backgroundR.png";
        private const string separatorImage = "separator.png";
        #endregion

        #region COMMANDS

        public ICommand ToggleCard { get; }
        public AsyncCommand FirstRunCommand { get; }

        #endregion

        #region PRIVATE FIELDS

        private List<Card> deck;
        bool IsEmpty
        {
            get => deck == null || deck.Count == 0;
        }

        private bool showBack;
        private string heading;
        private string text;
        private string action;
        private string background;
        private string separator;
        #endregion

        #region TEXT PROPERTIES

        public string Heading
        {
            get => heading;
            set => SetProperty(ref heading, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Action
        {
            get => action;
            set => SetProperty(ref action, value);
        }
        #endregion

        #region IMAGE PROPERTIES

        public string Background
        {
            get => background;//isLeft ? backgroundImageLeft : backgroundImageRight;
            set => SetProperty(ref background, value);
        }

        public string Separator
        {
            get => separator;
            set => SetProperty(ref separator, value);
        }
        #endregion

        #region CONSTRUCTORS

        public DeckViewModel()
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;

            showBack = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;

            deck = CardList.GetNewDeck(StaticHelper.Mode);

            ToggleCard = new MvvmHelpers.Commands.Command(OnToggleCard);
            FirstRunCommand = new AsyncCommand(FirstRun);
        }

        async Task FirstRun()
        {
            if (StaticHelper.FirstRun)
            {
                StaticHelper.FirstRun = false;
                //await Application.Current.MainPage.DisplayAlert("Welcome!", "Welcome to Coaching Cards. Let's get started with tutorial.", "OK");
                await Shell.Current.GoToAsync("///IntroductionPage");
            }
        }

        public DeckViewModel(GameMode gameMode)
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;

            showBack = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;

            deck = CardList.GetNewDeck(gameMode);
            ToggleCard = new MvvmHelpers.Commands.Command(OnToggleCard);
        }
        #endregion

        void OnToggleCard()
        {
            if (IsEmpty)
            {
                showBack = true; //!showBack;
                Heading = Text = Action = string.Empty;
                Background = emptyDeckBackgroundImage;
                Separator = string.Empty;
            }
            else
            {
                if (showBack) //show top card
                {
                    showBack = false; //!showBack;
                    Heading = deck[0].Heading;
                    Text = deck[0].Text;
                    Action = deck[0].Action;
                    Background = deck[0].IsLeft ? backgroundImageLeft : backgroundImageRight;
                    Separator = separatorImage;
                }
                else //toss top card
                {
                    deck.RemoveAt(0); //toss

                    showBack = true; //!showBack;
                    Heading = Text = Action = string.Empty;
                    Background = backgroundImage;
                    Separator = string.Empty;
                }
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            OnToggleCard();
        }

        //private static void ResetGame()
        //{
        //    cards = CardList.GetNewDeck();

        //    //animateCard(animShuffleDeck);

        //    showBack = true;

        //    cardDeckBackground.Source = backgroundImage; //cardDeck.BackgroundColor = Color.Orange;
        //    //separator1.Source = null;
        //    //separator2.Source = null;

        //    cardHeading.Text = empty;
        //    cardText.Text = empty;
        //    cardAction.Text = empty;
        //}
    }
}