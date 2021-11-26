using DataClasses;
using Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using ViewModels.Contract.DataClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetRandomGamePage : ContentPage
    {
        ObservableCollection<Genre> Genres;
        ObservableCollection<Descriptor> Descriptors;
        ObservableCollection<InfoClass> Games;
        IRandomGameViewModel viewModel;
        public GetRandomGamePage()
        {
            Genres = new ObservableCollection<Genre>();
            Descriptors = new ObservableCollection<Descriptor>();
            Games = new ObservableCollection<InfoClass>();
            viewModel = App.ServiceProvider.GetService<IRandomGameViewModel>();

            InitializeComponent();

            descriptorPicker.ItemsSource = App.AllDescriptors;
            genrePicker.ItemsSource = App.AllGenres;
            descriptors.ItemsSource = Descriptors;
            genres.ItemsSource = Genres;
            descriptorPicker.SelectedIndexChanged += DescriptorPicker_SelectedIndexChanged;
            descriptors.ItemTapped += Descriptors_ItemTapped;
            genres.ItemTapped += Genres_ItemTapped;
            genrePicker.SelectedIndexChanged += GenrePicker_SelectedIndexChanged;
            getRandomGames.Clicked += GetRandomGames_Clicked;
        }

        private void Genres_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Genre descriptor = (Genre)e.Item;
            Genres.Remove(descriptor);
        }

        private void Descriptors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Descriptor descriptor = (Descriptor)e.Item;
            Descriptors.Remove(descriptor);
        }

        private async void GetRandomGames_Clicked(object sender, EventArgs e)
        {
            var success = int.TryParse(countGames.Text, out int count);
            if(success)
            {
                Games.Clear();
                var games = viewModel.GetGames(Genres, Descriptors);
                if(games.Count < count)
                {
                    count = games.Count;
                }
                string gamesText = string.Empty;
                for (int i = 0; i < count; i++)
                {
                    Random random = new Random();
                    var value = random.Next(0, games.Count-1);
                    bool maxVal = value == games.Count - 1;
                    while(Games.Contains(games[value]))
                    {
                        if(maxVal)
                        {
                            if(value == 0)
                            {
                                i = count;
                                break;
                            }
                            else
                            {
                                value--;
                            }

                        }
                        else
                        {
                            value++;
                            if(value == games.Count - 1)
                            {
                                maxVal = true;
                            }
                        }
                    }
                    Games.Add(games[value]);
                    gamesText += games[i] + Environment.NewLine;
                }

                await DisplayAlert("Spiele", gamesText, "OK");
            }
        }

        private void GenrePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var genre = (Genre)genrePicker.SelectedItem;
            Genres.Add(genre);
        }

        private void DescriptorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var description = (Descriptor)descriptorPicker.SelectedItem;
            Descriptors.Add(description);
        }

        private async void TextCell_Tapped(object sender, EventArgs e)
        {

            await DisplayAlert("Gameinfo", "Spiel Getippt", "OK");
        }
    }
}