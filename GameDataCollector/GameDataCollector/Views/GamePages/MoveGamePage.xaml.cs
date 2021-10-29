using DataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoveGamePage : ContentPage
    {
        private IGameViewModel viewModel;
        private ObservableCollection<Storage> Storages { get; set; }
        private ObservableCollection<Game> Games { get; set; }
        public MoveGamePage()
        {
            viewModel = App.ServiceProvider.GetService<IGameViewModel>();
            InitializeComponent();
            this.Title = "Spiel verschieben";
            konsoleAlt.ItemsSource = viewModel.GetKonsolen();
            Storages = new ObservableCollection<Storage>(viewModel.GetStorages());
            Games = new ObservableCollection<Game>(viewModel.GetGames());

            storageAlt.ItemsSource = storageNeu.ItemsSource = Storages;
            gamePicker.ItemsSource = Games;

            if (viewModel.SelectedKonsole != null)
                konsoleAlt.SelectedItem = viewModel.SelectedKonsole;

            if (viewModel.SelectedStorage != null)
                storageAlt.SelectedItem = viewModel.SelectedStorage;

            konsoleAlt.SelectedIndexChanged += KonsoleAlt_SelectedIndexChanged;
            storageAlt.SelectedIndexChanged += StorageAlt_SelectedIndexChanged;
            saveButton.Clicked += SaveButton_Clicked;
        }

        private void StorageAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newGames = viewModel.GetGames();
            Games.Clear();
            foreach (var game in newGames)
            {
                Games.Add(game);
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if( storageAlt.SelectedItem != null &&
                gamePicker.SelectedItem != null &&
                storageNeu.SelectedItem != null)
            {
                var oldStorage = (Storage)storageAlt.SelectedItem;
                var game = (Game)gamePicker.SelectedItem;
                var newStorage = (Storage)storageNeu.SelectedItem;
                viewModel.MoveGame(oldStorage.Id, game.Id, newStorage.Id);
            }
        }

        private void KonsoleAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newStorages = viewModel.GetStorages();
            Storages.Clear();
            foreach(var storage in newStorages)
            {
                Storages.Add(storage);
            }

            StorageAlt_SelectedIndexChanged(sender, e);
        }
    }
}