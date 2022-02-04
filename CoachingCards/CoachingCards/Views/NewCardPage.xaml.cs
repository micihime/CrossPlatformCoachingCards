using CoachingCards.Models;
using CoachingCards.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoachingCards.Views
{
    public partial class NewCardPage : ContentPage
    {
        public Card Card { get; set; }

        public NewCardPage()
        {
            InitializeComponent();
            BindingContext = new NewCardViewModel();
        }
    }
}