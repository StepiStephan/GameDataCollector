﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoragePage : ContentPage
    {
        private IStorageViewModel viewModel;
        public ObservableCollection<string> Items { get; set; }

        public StoragePage()
        {
            InitializeComponent();
            BindingContext = viewModel = App.ServiceProvider.GetService<IStorageViewModel>();
            Items = new ObservableCollection<string>(viewModel.Storages.Select(x => x.Name));

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
