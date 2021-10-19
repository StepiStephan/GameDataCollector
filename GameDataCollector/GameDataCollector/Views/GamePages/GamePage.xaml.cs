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

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private readonly IGameViewModel viewModel;
        private List<DataClasses.Element> elements = new List<DataClasses.Element>();

        public ObservableCollection<DataClasses.Element> Items { get; set; }

        public GamePage()
        {
            InitializeComponent();
            BindingContext = viewModel = App.ServiceProvider.GetService<IGameViewModel>();
            Items = new ObservableCollection<DataClasses.Element>(GetElements());

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert($"Item {((DataClasses.Element)e.Item).Name} Tapped", $"The item {((DataClasses.Element)e.Item).Name} was registrated.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            Items = new ObservableCollection<DataClasses.Element>(GetElements());
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
