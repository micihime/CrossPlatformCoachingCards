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
        private static StackLayout cardDeck;
        private static Image cardDeckBackground;
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

            cardDeck = CardDeck;
            cardHeading = CardHeading;
            cardText = CardText;
            cardAction = CardAction;
            cardDeckBackground = CardDeckBackground;

            cards = CardList.GetNewDeck();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            CardManipulation();
        }
        private void testButt_Clicked(object sender, System.EventArgs e)
        {
            CardManipulation();
        }

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    switch (item.ItemId)
        //    {
        //        case Resource.Id.mode_all_cards:
        //            Deck.NewGame(GameMode.Full);
        //            return true;
        //        case Resource.Id.mode_left_hemisphere:
        //            Deck.NewGame(GameMode.LeftHemisphere);
        //            return true;
        //        case Resource.Id.mode_right_hemisphere:
        //            Deck.NewGame(GameMode.RightHemisphere);
        //            return true;
        //            //case Resource.Id.mode_guide:
        //            //        Deck.NewGame(GameMode.Full);
        //            //        return true;
        //    }

        //    return base.OnOptionsItemSelected(item);
        //}

        #region PUBLIC METHODS

        public static void CardManipulation()
        {
            if (IsEmpty)
                EmptyDeck();
            else
                ToggleCard();
        }
        #endregion

        #region PRIVATE METHODS

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

            //cardDeck.BackgroundColor = Color.Lavender;
            cardDeckBackground.Source = cards[0].IsLeft ? "backgroundL.png" : "backgroundR.png";
            //separator.SetBackgroundColor(Android.Graphics.Color.Black);

            cardHeading.Text = cards[0].Heading;
            cardText.Text = cards[0].Text;
            cardAction.Text = cards[0].Action;
        }

        private static void TossTopCard() //show card back
        {
            //animateCard(animTossCard);

            cards.RemoveAt(0); //toss
            showBack = true;

            //cardDeck.BackgroundColor = Color.Orange;
            cardDeckBackground.Source = "back.png";

            //separator.SetBackgroundColor(Android.Graphics.Color.Transparent);

            cardHeading.Text = empty;
            cardText.Text = empty;
            cardAction.Text = empty;
        }

        private static void EmptyDeck()
        {
            //cardDeck.BackgroundColor = Color.White;
            cardDeckBackground.Source = "empty.png";

            //separator.SetBackgroundColor(Android.Graphics.Color.Transparent);

            cardHeading.Text = empty;
            cardText.Text = empty;
            cardAction.Text = empty;
        }

        private static void ResetGame()
        {
            //animateCard(animShuffleDeck);

            showBack = true;

            //cardDeck.BackgroundColor = Color.Orange;
            cardDeckBackground.Source = "back.png";

            //separator.SetBackgroundColor(Android.Graphics.Color.Transparent);

            cardHeading.Text = empty;
            cardText.Text = empty;
            cardAction.Text = empty;
        }
        #endregion
    }
}