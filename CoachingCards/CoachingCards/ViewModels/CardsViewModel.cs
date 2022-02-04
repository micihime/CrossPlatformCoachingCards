using CoachingCards.Models;
using CoachingCards.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoachingCards.ViewModels
{
    public class CardsViewModel : BaseViewModel
    {
        private Card _selectedCard;

        public ObservableCollection<Card> Cards { get; }
        public Command LoadCardsCommand { get; }
        public Command AddCardCommand { get; }
        public Command<Card> CardTapped { get; }

        public CardsViewModel()
        {
            Title = "Browse";
            Cards = new ObservableCollection<Card>();
            LoadCardsCommand = new Command(async () => await ExecuteLoadCardsCommand());

            CardTapped = new Command<Card>(OnCardSelected);

            AddCardCommand = new Command(OnAddCard);
        }

        async Task ExecuteLoadCardsCommand()
        {
            IsBusy = true;

            try
            {
                Cards.Clear();
                var cards = await DataStore.GetAllAsync(true);
                foreach (var card in cards)
                {
                    Cards.Add(card);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedCard = null;
        }

        public Card SelectedCard
        {
            get => _selectedCard;
            set
            {
                SetProperty(ref _selectedCard, value);
                OnCardSelected(value);
            }
        }

        private async void OnAddCard(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewCardPage));
        }

        async void OnCardSelected(Card Card)
        {
            if (Card == null)
                return;

            // This will push the CardDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CardDetailPage)}?{nameof(CardDetailViewModel.CardId)}={Card.ID}");
        }
    }
}