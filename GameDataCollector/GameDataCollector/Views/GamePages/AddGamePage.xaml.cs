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
    public partial class AddGamePage : ContentPage
    {
        private IGameViewModel viewModel;
        private List<Genre> selectedGenres = new List<Genre>();
        private List<Descriptor> selectedDescriptors = new List<Descriptor>();

        public AddGamePage()
        {
            viewModel = App.ServiceProvider.GetService<IGameViewModel>();
            InitializeComponent();
            Title = "Spiel hinzufügen";
            konsolePicker.ItemsSource = viewModel.GetKonsolen();
            storagePicker.ItemsSource = viewModel.GetStorages();

            genrePicker.ItemsSource = App.AllGenres;
            genrePicker.SelectedIndexChanged += GenrePicker_SelectedIndexChanged;
            descriptorPicker.ItemsSource = App.AllDescriptors;
            descriptorPicker.SelectedIndexChanged += DescriptorPicker_SelectedIndexChanged;
            konsolePicker.SelectedIndexChanged += KonsolePicker_SelectedIndexChanged;
            saveButton.Clicked += SaveButton_Clicked;
        }

        private void DescriptorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (Descriptor)descriptorPicker.SelectedItem;
            if (!selectedDescriptors.Contains(selectedItem))
            {
                Label label = new Label();
                label.Text = selectedItem.ToString();
                label.VerticalOptions = LayoutOptions.Center;
                label.HorizontalOptions = LayoutOptions.Center;
                selectedDescriptors.Add(selectedItem);
                SelectedDescriptors.Children.Add(label);
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(gameName.Text != null && storagePicker.SelectedItem != null && speicherGB.Text != null)
            {
                var storage = (Storage)storagePicker.SelectedItem;
                var name = gameName.Text;
                float.TryParse(speicherGB.Text, out float space);
                var game = viewModel.CreateGame(storage.Id, name, selectedGenres, space);
                viewModel.AddDescriptors(game.Id, selectedDescriptors);
                await DisplayAlert("Spiel wurde hinzugefügt",$"{name} wurde zur Speicherkarte {storage.Name} hinzugefügt","OK");
                gameName.Text = string.Empty;
                speicherGB.Text = string.Empty;
                selectedGenres.Clear();
                SelectedGenres.Children.Clear();
                selectedDescriptors.Clear();
                SelectedDescriptors.Children.Clear();
                await Navigation.PopToRootAsync();
            }
        }

        private void KonsolePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.SelectedKonsole = (Konsole)konsolePicker.SelectedItem;
            storagePicker.ItemsSource = viewModel.GetStorages();
        }

        private void GenrePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (Genre)genrePicker.SelectedItem;
            if (!selectedGenres.Contains(selectedItem))
            {
                Label label = new Label();
                label.Text = selectedItem.ToString();
                label.VerticalOptions = LayoutOptions.Center;
                label.HorizontalOptions = LayoutOptions.Center;
                selectedGenres.Add(selectedItem);
                SelectedGenres.Children.Add(label);
            }
        }
    }
}