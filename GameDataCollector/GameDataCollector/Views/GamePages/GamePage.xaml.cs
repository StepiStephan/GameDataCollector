using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameDataCollector;
using DataClasses;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private IGameViewModel viewModel;
        private List<DataClasses.Element> elements = new List<DataClasses.Element>();
        private Game selectedGame;

        public ObservableCollection<DataClasses.Element> Items { get; set; }

        public GamePage()
        {
            InitializeComponent();
            Title = "Spiele Info";
            BindingContext = viewModel = App.ServiceProvider.GetService<IGameViewModel>();
            Items = new ObservableCollection<DataClasses.Element>(GetElements());
            deleteButton.Clicked += DeleteButton_Clicked;
            
            MyListView.ItemsSource = Items;
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            viewModel.DeleteGame(selectedGame.Id);
            Items.Remove(Items.Where(x => x.ID == selectedGame.Id).First());

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            var game = viewModel.GetGame(((DataClasses.Element)e.Item).ID);
            string genres = string.Empty;
            var info = viewModel.GetInfos(game);
            foreach(var genre in game.GameGenre)
            {
                genres += genre.ToString() + Environment.NewLine;
            }
            if (genres != string.Empty)
            {
                genres = genres.Remove(genres.Length - Environment.NewLine.Length);
            }
            var edit = await DisplayAlert($"Info Game {game.Name}", 
                $"Name: {game.Name}" + Environment.NewLine +
                $"Space: {game.SpaceOnSorage} GB" + Environment.NewLine + 
                $"Genres: {genres}" + Environment.NewLine +
                $"Konsole: {info[0]}" + Environment.NewLine +
                $"Speicher: {info[1]}","Bearbeiten", "OK");

            selectedGame = game;

            if(edit)
            {
                await Navigation.PushAsync(new EditGamePage(selectedGame));
            }
        }
        protected override void OnAppearing()
        {
            ClearList();
        }

        private void ClearList()
        {
            Items.Clear();
            foreach (var storage in GetElements())
            {
                Items.Add(storage);
            }
        }

        private IEnumerable<DataClasses.Element> GetElements()
        {
            elements = new List<DataClasses.Element>();
            foreach (var game in viewModel.GetGames())
            {
                var element = new DataClasses.Element()
                {
                    ID = game.Id,
                    Name = game.Name
                };
                elements.Add(element);
            }
            return elements;
        }
    }
}
