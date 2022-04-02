﻿using CoachingCards.Models;
using CoachingCards.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
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

        #region DECLARING COMMANDS

        public ICommand ToggleCard { get; }
        public AsyncCommand FirstRunCommand { get; }

        #endregion

        #region PRIVATE FIELDS

        private Card card;

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

            if (StaticHelper.CurrentDeckId == 0)
                ResetGame();
            else
                ShowCard();

            ToggleCard = new MvvmHelpers.Commands.Command(OnToggleCard);
            FirstRunCommand = new AsyncCommand(FirstRun);
        }

        public DeckViewModel(GameMode gameMode)
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;

            if (StaticHelper.CurrentDeckId == 0)
                ResetGame();
            else
                ShowCard();

            ToggleCard = new MvvmHelpers.Commands.Command(OnToggleCard);
            FirstRunCommand = new AsyncCommand(FirstRun);
        }
        #endregion

        #region COMMANDS

        async Task FirstRun()
        {
            if (StaticHelper.FirstRun)
            {
                StaticHelper.FirstRun = false;
                //await Application.Current.MainPage.DisplayAlert("Welcome!", "Welcome to Coaching Cards. Let's get started with tutorial.", "OK");
                await Shell.Current.GoToAsync("///IntroductionPage");
            }
        }

        void OnToggleCard()
        {
            if (IsEmpty())
            {
                StaticHelper.CurrentDeckId = 0; //reset
                ShowEmptyDeck();
            }
            else
            {
                if (showBack) //show top card
                    ShowCard();
                else //toss top card
                {
                    StaticHelper.CurrentDeckId++; //toss
                    ShowCardBack();
                }
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            OnToggleCard();
        }
        #endregion

        void ResetGame()
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            ShowCardBack();
            CardService.CreateNewDeck(StaticHelper.Mode);
        }

        private void ShowCardBack()
        {
            showBack = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;
        }

        private void ShowCard()
        {
            showBack = false; //!showBack;
            card = CardService.GetCardByDeckId(StaticHelper.CurrentDeckId);
            Heading = card.Heading;
            Text = card.Text;
            Action = card.Action;
            Background = card.IsLeft ? backgroundImageLeft : backgroundImageRight;
            Separator = separatorImage;
        }

        private void ShowEmptyDeck()
        {
            showBack = true; //!showBack;
            Heading = Text = Action = string.Empty;
            Background = emptyDeckBackgroundImage;
            Separator = string.Empty;
        }

        private bool IsEmpty()
        {
            return (StaticHelper.CurrentDeckId == StaticHelper.MaxDeckId) ? true : false;
        }
    }
}