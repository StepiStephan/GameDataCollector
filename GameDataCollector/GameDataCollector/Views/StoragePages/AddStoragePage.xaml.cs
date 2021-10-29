using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses;
using GameDataCollector;
using ViewModels.Contract;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStoragePage : ContentPage
    {
        private IStorageViewModel viewModel;
        public AddStoragePage()
        {
            viewModel = App.ServiceProvider.GetService<IStorageViewModel>();
            InitializeComponent();
            Title = "Speicher hinzufügen";
            konsolePicker.ItemsSource = viewModel.Konsolen;
            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(speicherName.Text != null && konsolePicker.SelectedItem != null && speicherGB.Text != null)
            {
                var konsole = (Konsole)konsolePicker.SelectedItem;
                var name = speicherName.Text;
                float.TryParse(speicherGB.Text, out float space);
                viewModel.CreateStorage(konsole.Id, name, space);
                await DisplayAlert("Speicherkarte wurde hinzugefügt", $"{name} wurde zur Konsole {konsole.Name} hinzugefügt", "OK");
                speicherName.Text = string.Empty;
                speicherGB.Text = string.Empty;
            }
        }
    }
}