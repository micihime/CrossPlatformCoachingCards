using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    [QueryProperty(nameof(CardId), nameof(CardId))]
    public class CardDetailViewModel : BaseViewModel
    {
        private int cardId;
        private string heading;
        private string text;
        private string action;
        private bool isLeft;

        public int Id { get; set; }

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

        public bool IsLeft { get; set; }

        public int CardId
        {
            get
            {
                return cardId;
            }
            set
            {
                cardId = value;
                LoadCardId(value);
            }
        }

        public async void LoadCardId(int cardId)
        {
            try
            {
                var Card = await DataStore.GetAsync(cardId);
                Id = Card.ID;
                Heading = Card.Heading;
                Text = Card.Text;
                Action = Card.Action;
                IsLeft = Card.IsLeft;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Card");
            }
        }
    }
}