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
        public ObservableCollection<string> Items { get; set; }

        public StoragePage()
        {
            InitializeComponent();
            BindingContext = viewModel = App.ServiceProvider.GetService<IStorageViewModel>();
            List<Element> elements = new List<Element>();
            foreach(var item in viewModel.GetStorages())
            {
                elements.Add(new Element()
                {
                    ID = item.Id,
                    Name = item.Name
                });
            }
            Items = new ObservableCollection<string>(elements.Select(x => x.Name));
            
            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
