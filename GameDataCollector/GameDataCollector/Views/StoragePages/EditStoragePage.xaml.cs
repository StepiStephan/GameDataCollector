using DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStoragePage : ContentPage
    {
        private Storage storage;
        private IStorageViewModel viewModel;
        public EditStoragePage(Storage storage )
        {
            this.storage = storage;
            viewModel = App.ServiceProvider.GetService<IStorageViewModel>();

            InitializeComponent();
            Title = $"Speicher {storage.Name} bearbeiten";
            storageName.Text = storage.Name;
            storageSize.Text = storage.Space.ToString();

            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (storageName.Text != null && storageSize.Text != null)
            {
                var size = float.Parse(storageSize.Text);
                viewModel.EditStorage(storage.Id, storageName.Text, size);
                await DisplayAlert("Speicher geändert",
                    $"Speicher wurde auf {storageName.Text} mit der Speichergröße {storageSize.Text}GB geändert",
                     "OK");
            }
        }
    }
}