using CoachingCards.Models;
using CoachingCards.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Plugin.LocalNotification;
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
            ToggleCard = new MvvmHelpers.Commands.Command(OnToggleCard);
            FirstRunCommand = new AsyncCommand(FirstRun);
        }
        #endregion

        #region COMMANDS

        async Task FirstRun()
        {
            if (StaticHelper.FirstRun)
            {
                var notification = new NotificationRequest
                {
                    BadgeNumber = 1,
                    Title = "Pripomienka",
                    Description = "Pripominam hranie!",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(30), // Used for Scheduling local notification, if not specified notification will show immediately.
                        NotifyRepeatInterval = new TimeSpan(1, 0, 0, 0, 0)
                    }
                };
                NotificationCenter.Current.Show(notification);

                StaticHelper.FirstRun = false;

                await Shell.Current.GoToAsync("///IntroductionPage");
            }
        }

        void OnToggleCard()
        {
            if (IsEmpty())
                ShowEmptyDeck();
            else
            {
                if (showBack) //show top card
                {
                    StaticHelper.CurrentDeckId++;
                    //CardService.SetCurrentDeckId(CardService.GetCurrentDeckId() + 1);
                    ShowCard();
                }
                else //toss top card
                {
                    ShowCardBack();
                }
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            OnToggleCard();
        }
        #endregion

        public void ResetGame()
        {
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            ShowCardBack();
            CardService.CreateNewDeck();
        }

        private void ShowCardBack()
        {
            showBack = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;
        }

        public void ShowCard()
        {
            showBack = false; //!showBack;
            //card = CardService.GetCardByDeckId(CardService.GetCurrentDeckId());
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
            //return CardService.GetCurrentDeckId() == CardService.GetMaxDeckId();
            return StaticHelper.CurrentDeckId == CardService.GetMaxDeckId();
        }
    }
}