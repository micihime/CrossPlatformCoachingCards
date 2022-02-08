using CoachingCards.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoachingCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckPage : ContentPage
    {
        #region DECK

        private const string empty = "";
        private const string emptyDeckBackgroundImage = "empty.png";
        private const string backgroundImage = "back.png";
        private const string backgroundImageLeft = "backgroundL.png";
        private const string backgroundImageRight = "backgroundR.png";
        private const string separatorImage = "separator.png";

        private static Image cardDeckBackground;
        private static Image separator1;
        private static Image separator2;

        private static Label cardHeading;
        private static Label cardText;
        private static Label cardAction;

        private static List<Card> cards;
        private static bool showBack;

        private static bool IsEmpty
        {
            get => cards == null || cards.Count == 0;
        }
        #endregion

        public DeckPage()
        {
            InitializeComponent();

            cardDeckBackground = CardDeckBackground;
            separator1 = Separator1;
            separator2 = Separator2;

            cardHeading = CardHeading;
            cardText = CardText;
            cardAction = CardAction;

            ResetGame();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            CardManipulation();
        }

        private static void ResetGame()
        {
            cards = CardList.GetNewDeck();

            //animateCard(animShuffleDeck);

            showBack = true;

            cardDeckBackground.Source = backgroundImage; //cardDeck.BackgroundColor = Color.Orange;
            separator1.Source = null;
            separator2.Source = null;

            cardHeading.Text = empty;
            cardText.Text = empty;
            cardAction.Text = empty;
        }

        #region PRIVATE METHODS

        public static void CardManipulation()
        {
            if (IsEmpty)
                EmptyDeck();
            else
                ToggleCard();
        }

        //private static void animateCard(Animation anim)
        //{
        //    cardLayout.ClearAnimation();
        //    cardLayout.Animation = anim;
        //    cardLayout.StartAnimation(anim);
        //}

        private static void ToggleCard()
        {
            if (showBack)
                ShowTopCard();
            else
                TossTopCard();
        }

        private static void ShowTopCard()
        {
            //animateCard(animTurnCard);

            showBack = false;

            cardDeckBackground.Source = cards[0].IsLeft ? backgroundImageLeft : backgroundImageRight; //cardDeck.BackgroundColor = Color.Lavender;
            separator1.Source = separatorImage;
            separator2.Source = separatorImage;

            cardHeading.Text = cards[0].Heading;
            cardText.Text = cards[0].Text;
            cardAction.Text = cards[0].Action;
        }

        private static void TossTopCard() //show card back
        {
            //animateCard(animTossCard);

            cards.RemoveAt(0); //toss
            showBack = true;

            cardDeckBackground.Source = backgroundImage; //cardDeck.BackgroundColor = Color.Orange;
            separator1.Source = null;
            separator2.Source = null;

            cardHeading.Text = empty;
            cardText.Text = empty;
            cardAction.Text = empty;
        }

        private static void EmptyDeck()
        {
            cardDeckBackground.Source = emptyDeckBackgroundImage;  //cardDeck.BackgroundColor = Color.White;
            separator1.Source = null;
            separator2.Source = null;

            cardHeading.Text = empty;
            cardText.Text = empty;
            cardAction.Text = empty;
        }
        #endregion
    }
}