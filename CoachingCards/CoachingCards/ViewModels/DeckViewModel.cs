using CoachingCards.Models;
using System.Collections.Generic;
using System.Windows.Input;
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
            showBack = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;

            deck = CardList.GetNewDeck();
            ToggleCard = new Command(OnToggleCard);
        }

        public DeckViewModel(GameMode gameMode)
        {
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

        //private void ShowTopCard()
        //{
        //    showBack = false;

        //    cardLayout.SetBackgroundResource(cards[0].Background);
        //    separator.SetBackgroundColor(Android.Graphics.Color.Black);

        //    cardTextView.SetText(HtmlCompat.FromHtml(cards[0].RenderText(), 0), TextView.BufferType.Normal);
        //    cardActionTextView.SetText(HtmlCompat.FromHtml(cards[0].Action, 0), TextView.BufferType.Normal);

        //}

        //private void TossTopCard()
        //{
        //    cards.RemoveAt(0); //toss
        //    showBack = true;

        //    cardLayout.SetBackgroundResource(cardBack);
        //    separator.SetBackgroundColor(Android.Graphics.Color.Transparent);

        //    cardTextView.SetText(textEmpty);
        //    cardActionTextView.SetText(textEmpty);

        //}

        //private void EmptyDeck()
        //{
        //    cardLayout.SetBackgroundResource(cardEmpty);
        //    separator.SetBackgroundColor(Android.Graphics.Color.Transparent);

        //    cardTextView.SetText(textEmpty);
        //    cardActionTextView.SetText(textEmpty);
        //}

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
