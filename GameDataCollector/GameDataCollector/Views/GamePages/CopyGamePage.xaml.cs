using DataClasses;
using Enums;
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

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CopyGamePage : ContentPage
    {
        private IGameViewModel viewModel;
        private ObservableCollection<Storage> Storages { get; set; }
        private ObservableCollection<Storage> NewStorages { get; set; }

        private ObservableCollection<Game> Games { get; set; }
        public CopyGamePage()
        {
            viewModel = App.ServiceProvider.GetService<IGameViewModel>();
            InitializeComponent();
            this.Title = "Spiel Kopieren";
            konsoleAlt.ItemsSource = viewModel.GetKonsolen();
            konsoleNeu.ItemsSource = viewModel.GetKonsolen();

            Storages = new ObservableCollection<Storage>(viewModel.GetStorages());
            Games = new ObservableCollection<Game>(viewModel.GetGames());
            NewStorages = new ObservableCollection<Storage>(viewModel.GetStorages());

            storageAlt.ItemsSource = Storages;
            storageNeu.ItemsSource = NewStorages;
            gamePicker.ItemsSource = Games;

            if (viewModel.SelectedKonsole != null)
                konsoleAlt.SelectedItem = viewModel.SelectedKonsole;

            if (viewModel.SelectedStorage != null)
                storageAlt.SelectedItem = viewModel.SelectedStorage;

            konsoleAlt.SelectedIndexChanged += KonsoleAlt_SelectedIndexChanged;
            storageAlt.SelectedIndexChanged += StorageAlt_SelectedIndexChanged;
            konsoleNeu.SelectedIndexChanged += KonsoleNeu_SelectedIndexChanged;
            storageNeu.SelectedIndexChanged -= StorageNeu_SelectedIndexChanged;

            saveButton.Clicked += SaveButton_Clicked;
        }

        private void StorageNeu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (storageNeu.SelectedItem != Picker.SelectedItemProperty.DefaultValue)
            {
                viewModel.SelectedStorage = (Storage)storageNeu.SelectedItem;
            }
        }

        private void KonsoleAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.SelectedKonsole = (Konsole)konsoleAlt.SelectedItem;

            var newStorages = viewModel.GetStorages();
            Storages.Clear();
            foreach (var storage in newStorages)
            {
                Storages.Add(storage);
            }

            StorageAlt_SelectedIndexChanged(sender, e);
        }

        private void StorageAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (storageAlt.SelectedItem != Picker.SelectedItemProperty.DefaultValue)
            {
                viewModel.SelectedStorage = (Storage)storageAlt.SelectedItem;
            }
            var newGames = viewModel.GetGames();
            Games.Clear();
            foreach (var game in newGames)
            {
                Games.Add(game);
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (storageAlt.SelectedItem != null &&
                gamePicker.SelectedItem != null &&
                storageNeu.SelectedItem != null)
            {
                var oldStorage = (Storage)storageAlt.SelectedItem;
                var game = (Game)gamePicker.SelectedItem;
                var newStorage = (Storage)storageNeu.SelectedItem;
                viewModel.CopyGame(oldStorage.Id, game.Id, newStorage.Id);
                await Navigation.PopToRootAsync();

            }
        }

        private void KonsoleNeu_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.SelectedKonsole = (Konsole)konsoleNeu.SelectedItem;

            var newStorages = viewModel.GetStorages();
            NewStorages.Clear();
            foreach (var storage in newStorages)
            {
                NewStorages.Add(storage);
            }

            StorageNeu_SelectedIndexChanged(sender, e);
        }
    }
}