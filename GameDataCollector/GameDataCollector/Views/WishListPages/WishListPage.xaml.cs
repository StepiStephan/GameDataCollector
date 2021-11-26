﻿using DataClasses;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevExpress.XamarinForms.DataGrid;

namespace GameDataCollector.Views.WishListPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishListPage : ContentPage
    {
        public ObservableCollection<WishListItem> WishList { get; }
        public ObservableCollection<string> Konsolen { get; }

        private IWishListViewModel viewmodel;
        public WishListPage()
        {
            InitializeComponent();
            viewmodel = App.ServiceProvider.GetService<IWishListViewModel>();
            Konsolen = new ObservableCollection<string>(viewmodel.GetKonsolen());
            WishList = new ObservableCollection<WishListItem>();
            //MyListView.ItemsSource = WishList;
            grid.ItemsSource = WishList;
            grid.Columns[0].IsVisible = false;
            grid.Columns[2].IsVisible = false;
            konsolePicker.ItemsSource = Konsolen;
            konsolePicker.SelectedIndexChanged += KonsolePicker_SelectedIndexChanged;
            addEntryButton.Clicked += AddEntryButton_Clicked;
            removeEntryButton.Clicked += RemoveEntryButton_Clicked;
            viewmodel.ItemAdded += Viewmodel_ItemAdded;
        }

        private void Viewmodel_ItemAdded(object sender, EventArgs e)
        {
            var konsoleAuswahl = (string)konsolePicker.SelectedItem;
            Konsolen.Clear();
            foreach (var konsole in viewmodel.GetKonsolen())
            {
                Konsolen.Add(konsole);
            }
            WishList.Clear();
            konsolePicker.SelectedItem = Konsolen.Where(x => x == konsoleAuswahl).SingleOrDefault();
        }

        private void RemoveEntryButton_Clicked(object sender, EventArgs e)
        {
            if (grid.SelectedItem != null)
            {
                var game = (WishListItem)grid.SelectedItem;
                viewmodel.RemoveGame(game.ID);
                var konsoleAuswahl = game.KonsoleType;
                Konsolen.Clear();
                foreach (var konsole in viewmodel.GetKonsolen())
                {
                    Konsolen.Add(konsole);
                }

                konsolePicker.SelectedItem = Konsolen.Where(x => x == konsoleAuswahl).SingleOrDefault();
            }
        }

        private async void AddEntryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddWishListItemPage());

        }

        private void KonsolePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<WishListItem> konsoleWishList = viewmodel.GetItems((string)konsolePicker.SelectedItem);
            WishList.Clear();
            foreach(var item in konsoleWishList)
            {
                WishList.Add(item);
            }
        }

        private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (WishListItem)e.Item;
            await DisplayAlert($"{item.Name}", 
                $"Name : {item.Name}" + Environment.NewLine +
                $"Günstigster Händler: {item.Store}" + Environment.NewLine +
                $"Günstigster Preis: {item.Ammount}", "OK");
        }
    }
}