using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views.WishListPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWishListItemPage : ContentPage
    {
        IWishListViewModel viewmodel;
        public AddWishListItemPage()
        {
            viewmodel = App.ServiceProvider.GetService<IWishListViewModel>();
            InitializeComponent();
            save.Clicked += Save_Clicked;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var gameName = name.Text;
            var consoleName = konsole.Text;
            var storeName = store.Text;
            float.TryParse(price.Text, out float ammountPrice);

            if(!(gameName == string.Empty || consoleName == string.Empty|| storeName == string.Empty))
            {
                viewmodel.AddGame(gameName, consoleName, storeName, ammountPrice);
            }

            name.Text = string.Empty;
            konsole.Text = string.Empty;
            store.Text = string.Empty;
            price.Text = string.Empty;

            await Navigation.PopToRootAsync();
        }
    }
}