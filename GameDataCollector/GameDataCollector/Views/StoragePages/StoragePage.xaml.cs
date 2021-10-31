using DataClasses;
using System;
using System.Collections.Generic;
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
        List<DataClasses.Element> elements = new List<DataClasses.Element>();

        public ObservableCollection<DataClasses.Element> Items { get; set; }
        private Storage selectedStorage;

        public StoragePage()
        {
            InitializeComponent();
            Title = "Speicher Info";
            BindingContext = viewModel = App.ServiceProvider.GetService<IStorageViewModel>();
            Items = new ObservableCollection<DataClasses.Element>(GetElements());
            deleteButton.Clicked += DeleteButton_Clicked;

            MyListView.ItemsSource = Items;
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var allGames = await DisplayAlert("Lösche Speicher", "Willst du alle Spiele auf dem Speicher löschen", "Ja", "Nein");
            if (allGames)
            {
                viewModel.DeleteStorageWithGames(selectedStorage.Id);
            }
            else
            {
                viewModel.DeleteStorage(selectedStorage.Id);
            }
            Items.Remove(Items.Where(x => x.ID == selectedStorage.Id).First());

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            viewModel.SetStorage(((DataClasses.Element)e.Item).ID);

            //Deselect Item
            var storage = viewModel.GetStorages().Where(x => x.Id == ((DataClasses.Element)e.Item).ID).First();
            var info = viewModel.GetInfos(storage);

            var edit = await DisplayAlert($"Info Storage {storage.Name}",
                $"Name: {storage.Name}" + Environment.NewLine +
                $"Space: {storage.Space} GB" + Environment.NewLine +
                $"Anzahl Spiele: {storage.Games.Count}" + Environment.NewLine +
                $"Konsole: {info}", "Bearbeiten", "OK");

            selectedStorage = storage;

            if (edit)
            {
                await Navigation.PushAsync(new EditStoragePage(selectedStorage));
            }
        }
        protected override void OnAppearing()
        {
            Items.Clear();
            foreach(var storage in GetElements())
            {
                Items.Add(storage);
            }
        }
        private IEnumerable<DataClasses.Element> GetElements()
        {
            elements = new List<DataClasses.Element>();
            foreach (var storage in viewModel.GetStorages())
            {
                var element = new DataClasses.Element()
                {
                    ID = storage.Id,
                    Name = storage.Name
                };
                elements.Add(element);
            }
            return elements;
        }
    }
}
