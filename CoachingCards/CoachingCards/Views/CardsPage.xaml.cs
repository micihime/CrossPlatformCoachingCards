using CoachingCards.Models;
using CoachingCards.ViewModels;
using CoachingCards.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoachingCards.Views
{
    public partial class CardsPage : ContentPage
    {
        CardsViewModel _viewModel;

        public CardsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CardsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}