﻿using CoachingCards.Models;
using CoachingCards.Services;
using MvvmHelpers;
using System;
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

        #region PRIVATE FIELDS

        private CardExtended card;
        private string pageTitle;
        #endregion

        #region PROPERTIES

        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }

        #region CARD PROPERTIES

        public string Heading
        {
            get => card.Heading;
            set { card.Heading = value; OnPropertyChanged(); }
        }

        public string Text
        {
            get => card.Text;
            set { card.Text = value; OnPropertyChanged(); }
        }

        public string Action
        {
            get => card.Action;
            set { card.Action = value; OnPropertyChanged(); }
        }

        public string Background
        {
            get => card.Background;
            set { card.Background = value; OnPropertyChanged(); }
        }

        public string Separator
        {
            get => card.Separator;
            set { card.Separator = value; OnPropertyChanged(); }
        }
        #endregion

        #endregion

        #region DECLARING COMMANDS

        public ICommand ToggleCardCommand { get; }
        //public AsyncCommand FirstRunCommand { get; }
        #endregion

        #region CONSTRUCTORS

        public DeckViewModel()
        {
            pageTitle = StaticHelper.GameModeToString(CardService.GetCurrentGameMode());
            card = new CardExtended();
            ToggleCardCommand = new MvvmHelpers.Commands.Command(OnToggleCard);
            //FirstRunCommand = new AsyncCommand(FirstRun);
        }
        #endregion

        #region COMMANDS

        public async Task OnAppearing()
        {
            Accelerometer.Start(SensorSpeed.Default);

            if (StaticHelper.FirstRun)
                await FirstRun(); //FirstRunCommand.ExecuteAsync();

            LoadGame();
        }

        public void OnDisappearing() { Accelerometer.Stop(); }

        public void OnToggleCard() { ToggleCard(); } //called on tap + on shake

        public void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            OnToggleCard();
        }
        #endregion

        #region HELPERS

        private async Task FirstRun()
        {
            //await StaticHelper.ScheduleNotif();
            StaticHelper.FirstRun = false;
            await Shell.Current.GoToAsync("///IntroductionPage");
        }

        private void LoadGame()
        {
            if (StaticHelper.CurrentDeckId == 0)
                ResetGame();
            else if (IsEmpty())
                ShowEmptyDeck();
            else if (card.IsBacksideUp) //show current card
                ShowCurrentCard();
            else //toss top card
                ShowCardBack();
        }

        private void ResetGame()
        {
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            ShowCardBack();
            CardService.CreateNewDeck();
        }

        private void ToggleCard()
        {
            if (IsEmpty())
                ShowEmptyDeck();
            else
                TurnCard();
        }

        private void TurnCard()
        {
            if (card.IsBacksideUp) //show new card - incrementing current deck id
                ShowNewCard();
            else //toss top card
                ShowCardBack();
        }

        private void ShowCardBack()
        {
            card.IsBacksideUp = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;
        }

        public void ShowCurrentCard()
        {
            card = CardService.GetCardExtendedByDeckId(StaticHelper.CurrentDeckId);
            card.IsBacksideUp = false;
            Heading = card.Heading;
            Text = card.Text;
            Action = card.Action;
            Background = card.IsLeft ? backgroundImageLeft : backgroundImageRight;
            Separator = separatorImage;
        }

        public void ShowNewCard()
        {
            StaticHelper.CurrentDeckId++;
            ShowCurrentCard();
        }

        private void ShowEmptyDeck()
        {
            card.IsBacksideUp = true;
            Heading = Text = Action = string.Empty;
            Background = emptyDeckBackgroundImage;
            Separator = string.Empty;
        }

        private bool IsEmpty()
        {
            return StaticHelper.CurrentDeckId == CardService.GetMaxDeckId();
        }
        #endregion
    }
}