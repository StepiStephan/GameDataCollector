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

        public StoragePage()
        {
            InitializeComponent();
            BindingContext = viewModel = App.ServiceProvider.GetService<IStorageViewModel>();
            
            Items = new ObservableCollection<DataClasses.Element>(GetElements());
            
            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert($"Item {((DataClasses.Element)e.Item).Name} Tapped", $"The item {((DataClasses.Element)e.Item).Name} was registrated.", "OK");
            viewModel.SetStorage(((DataClasses.Element)e.Item).ID);

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
