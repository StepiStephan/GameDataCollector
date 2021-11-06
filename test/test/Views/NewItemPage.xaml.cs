using System;
using System.Collections.Generic;
using System.ComponentModel;
using test.Models;
using test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}