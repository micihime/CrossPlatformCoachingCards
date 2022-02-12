using CoachingCards.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class DeckViewModel : BindableObject
    {
        #region DECK RESOURCES

        private const string emptyDeckBackgroundImage = "empty.png";
        private const string backgroundImage = "back.png";
        private const string backgroundImageLeft = "backgroundL.png";
        private const string backgroundImageRight = "backgroundR.png";
        private const string separatorImage = "separator.png";
        #endregion

        #region COMMANDS

        public ICommand ToggleCard { get; }
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
            set
            {
                if (value == heading)
                    return;

                heading = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => text;
            set
            {
                if (value == text)
                    return;

                text = value;
                OnPropertyChanged();
            }
        }

        public string Action
        {
            get => action;
            set
            {
                if (value == action)
                    return;

                action = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IMAGE PROPERTIES

        public string Background
        {
            get
            {
                return background;//isLeft ? backgroundImageLeft : backgroundImageRight;
            }
            set
            {
                if (value == background)
                    return;

                background = value;
                OnPropertyChanged();
            }
        }

        public string Separator
        {
            get
            {
                return separator;
            }
            set
            {
                if (value == separator)
                    return;

                separator = value;
                OnPropertyChanged();
            }
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

            deck = CardList.GetNewDeck();
            ToggleCard = new Command(OnToggleCard);
        }

        public DeckViewModel(GameMode gameMode)
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;

            showBack = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;

            deck = CardList.GetNewDeck(gameMode);
            ToggleCard = new Command(OnToggleCard);
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