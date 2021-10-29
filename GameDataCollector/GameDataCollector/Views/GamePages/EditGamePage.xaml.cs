using DataClasses;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditGamePage : ContentPage
    {
        Game game;
        IGameViewModel viewModel;
        ObservableCollection<Genre> Genres;

        public EditGamePage(Game game)
        {
            Genres = new ObservableCollection<Genre>(game.GameGenre);
            this.game = game;
            viewModel = App.ServiceProvider.GetService<IGameViewModel>();
            InitializeComponent();

            Title = $"Bearbeiten von {game.Name}";
            gameName.Text = game.Name;
            gameSize.Text = game.SpaceOnSorage.ToString();
            genrePicker.ItemsSource = App.AllGenres;
            genres.ItemsSource = Genres;
            saveButton.Clicked += SaveButton_Clicked;
            genrePicker.SelectedIndexChanged += GenrePicker_SelectedIndexChanged;
            genres.ItemTapped += Genres_ItemTapped;

        }

        private async void Genres_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Genre genre = (Genre)e.Item;
            var deleteGenre = await DisplayAlert("Genre löschen?", $"Das Genre {genre} wurde ausgewählt.", "Löschen", "Abbrechen");

            if (deleteGenre)
            {
                viewModel.DeleteGenre(game.Id, genre);
                Genres.Remove(genre);
                
            }
        }

        private void GenrePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Genre genre = (Genre)genrePicker.SelectedItem;
            if(!game.GameGenre.Contains(genre))
            {
                viewModel.AddGenre(game.Id, new List<Genre>() { genre });
                Genres.Add(genre);
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var success = float.TryParse(gameSize.Text, out float size);
            if (success)
            {
                viewModel.EditGame(game.Id, gameName.Text, size);
                await DisplayAlert("Spiel geändert",
                                    $"Spiel wurde auf {gameName.Text} mit der Speichergröße {gameSize.Text}GB geändert",
                                     "OK");
            }
        }
    }
}