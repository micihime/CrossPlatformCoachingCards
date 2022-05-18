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

        public ICommand ToggleCard { get; }
        public AsyncCommand FirstRunCommand { get; }
        #endregion

        #region CONSTRUCTORS

        public DeckViewModel()
        {
            pageTitle = StaticHelper.GameModeToString(CardService.GetCurrentGameMode());
            card = new CardExtended();
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
                    Title = "Koučovací karty",
                    Description = "Jaká bude tvá dnešní karta?",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(30), // Used for Scheduling local notification, if not specified notification will show immediately.
                        NotifyRepeatInterval = new TimeSpan(1, 0, 0, 0, 0)
                    }
                };
                await NotificationCenter.Current.Show(notification);

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
                if (card.ShowBack) //show top card
                {
                    StaticHelper.CurrentDeckId++;
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

        #region HELPERS

        public void ResetGame()
        {
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            ShowCardBack();
            CardService.CreateNewDeck();
        }

        private void ShowCardBack()
        {
            card.ShowBack = true;
            Heading = Text = Action = string.Empty;
            Background = backgroundImage;
            Separator = string.Empty;
        }

        public void ShowCard()
        {
            card = CardService.GetCardExtendedByDeckId(StaticHelper.CurrentDeckId);
            card.ShowBack = false;
            Heading = card.Heading;
            Text = card.Text;
            Action = card.Action;
            Background = card.IsLeft ? backgroundImageLeft : backgroundImageRight;
            Separator = separatorImage;
        }

        private void ShowEmptyDeck()
        {
            card.ShowBack = true;
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