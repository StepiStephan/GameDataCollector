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

        public KonsolenPage()
        {
            InitializeComponent();
            BindingContext = viewModel = App.ServiceProvider.GetService<IKonsoleViewModel>();

            Items = new ObservableCollection<DataClasses.Element>(GetElements());
            ItemTapped = new Command<DataClasses.Element>(OpenDeails);
            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await Task.Delay(1);
            //await DisplayAlert($"Item {((DataClasses.Element)e.Item).Name} Tapped", $"The item {((DataClasses.Element)e.Item).Name} was registrated.", "OK");
            //viewModel.SetKonsole(((DataClasses.Element)e.Item).ID);

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
