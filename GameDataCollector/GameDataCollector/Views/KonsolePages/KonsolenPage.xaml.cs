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
    public partial class KonsolenPage : ContentPage
    {
        private IKonsoleViewModel viewModel;
        private List<DataClasses.Element> elements;
        public ObservableCollection<DataClasses.Element> Items { get; set; }
        public Command<DataClasses.Element> ItemTapped { get; set; }

        private Konsole selectedKonsole;

        public KonsolenPage()
        {
            InitializeComponent();
            BindingContext = viewModel = App.ServiceProvider.GetService<IKonsoleViewModel>();
            Title = "Konsolen Info";
            Items = new ObservableCollection<DataClasses.Element>(GetElements());
            ItemTapped = new Command<DataClasses.Element>(OpenDeails);
            deleteButton.Clicked += DeleteButton_Clicked;
            MyListView.ItemsSource = Items;
        }
        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            viewModel.DeleteKonsole(selectedKonsole.Id);
            Items.Remove(Items.Where(x => x.ID == selectedKonsole.Id).First());
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            viewModel.SetKonsole(((DataClasses.Element)e.Item).ID);

            var konsole = viewModel.Konsoles.Where(x => x.Id == ((DataClasses.Element)e.Item).ID).First();
            var info = viewModel.GetInfo(konsole);

            var edit = await DisplayAlert($"Info Konsole {konsole.Name}",
                $"Name: {konsole.Name}" + Environment.NewLine +
                $"Typ: {konsole.ConsoleName}" + Environment.NewLine +
                $"Anzahl Speicher: {konsole.Storages.Count}" + Environment.NewLine +
                $"Anzahl Spiele: {info}", "Bearbeiten", "OK");

            selectedKonsole = konsole;

            if (edit)
            {
                await Navigation.PushAsync(new EditKonsolePage(selectedKonsole));
            }
        }
        protected override void OnAppearing()
        {
            Items.Clear();
            foreach (var element in GetElements())
            {
                Items.Add(element);
            }
        }
        private IEnumerable<DataClasses.Element> GetElements()
        {
            elements = new List<DataClasses.Element>();
            foreach (var konsole in viewModel.Konsoles)
            {
                var element = new DataClasses.Element()
                {
                    ID = konsole.Id,
                    Name = konsole.Name
                };
                elements.Add(element);
            }
            return elements;
        }
        async void OpenDeails(DataClasses.Element element)
        {
            if (element == null)
                return;

            await DisplayAlert($"Item {element.Name} Tapped", $"The item {element.Name} was registrated.", "OK");

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(KonsoleDetailPage)}?{nameof(IDetailViewModel<Konsole>.ItemId)}={element.ID}");
        }
    }
}
