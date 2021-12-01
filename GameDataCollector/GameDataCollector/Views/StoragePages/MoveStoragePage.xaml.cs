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
    public partial class MoveStoragePage : ContentPage
    {
        private IStorageViewModel viewModel;
        private ObservableCollection<Storage> Storages { get; set; }
        public MoveStoragePage()
        {
            viewModel = App.ServiceProvider.GetService<IStorageViewModel>();
            InitializeComponent();
            Title = "Speicher verschieben";
            konsoleAlt.ItemsSource = konsoleNeu.ItemsSource = viewModel.Konsolen;
            Storages = new ObservableCollection<Storage>(viewModel.GetStorages());

            storage.ItemsSource = Storages;

            if (viewModel.SelectedKonsole != null)
                konsoleAlt.SelectedItem = viewModel.SelectedKonsole;

            konsoleAlt.SelectedIndexChanged += KonsoleAlt_SelectedIndexChanged;
            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (konsoleAlt.SelectedItem != null &&
                storage.SelectedItem != null &&
                konsoleNeu.SelectedItem != null)
            {
                var oldKonsole = (Konsole)konsoleAlt.SelectedItem;
                var selectedStorage = (Storage)storage.SelectedItem;
                var newKonsole = (Konsole)konsoleNeu.SelectedItem;
                viewModel.MoveStorage(oldKonsole.Id, selectedStorage.Id, newKonsole.Id);
                await Navigation.PopToRootAsync();
            }
        }

        private void KonsoleAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.SelectedKonsole = (Konsole)((Picker)sender).SelectedItem;
            var newStorages = viewModel.GetStorages();
            Storages.Clear();
            foreach (var storage in newStorages)
            {
                Storages.Add(storage);
            }
        }
    }
}