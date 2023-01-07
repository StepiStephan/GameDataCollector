using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views.WishListPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWishListItemPage : ContentPage
    {
        IWishListViewModel viewmodel;
        private int counter = 0;
        private IList<View> additionalEditors= new List<View>();
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
            var releaseDate = datePicker.Date;
            var storeName = store.Text;
            float.TryParse(price.Text, out float ammountPrice);

            if(!(gameName == string.Empty || consoleName == string.Empty|| storeName == string.Empty))
            {
                List<(string, float)> angebote = new List<(string, float)> 
                { 
                    (storeName, ammountPrice)
                };
                for(int i = 0; i < additionalEditors.Count; i+=2)
                {
                    angebote.Add((((Editor)additionalEditors[i]).Text, float.Parse(((Editor)additionalEditors[i]).Text)));
                }
                viewmodel.AddGame(gameName, consoleName, angebote, releaseDate);
            }

            name.Text = string.Empty;
            konsole.Text = string.Empty;
            datePicker.Date = DateTime.Now;
            store.Text = string.Empty;
            price.Text = string.Empty;

            foreach(var editor in additionalEditors)
            {
                DataFields.Children.Remove(editor);
            }

            await Navigation.PopToRootAsync();
        }

        private void addAnbieter_Clicked(object sender, EventArgs e)
        {
            if(counter < 2)
            {
                var editorName = new Editor();
                DataFields.Children.Add(new Label() { Text = $"Zusätzlicher Anbieter" });
                DataFields.Children.Add(editorName);
                additionalEditors.Add(editorName);

                var editorKosten = new Editor() { Keyboard = Keyboard.Numeric};
                DataFields.Children.Add(new Label() { Text = $"Kosten" });
                DataFields.Children.Add(editorKosten);
                additionalEditors.Add(editorKosten);
                counter ++;
            }
        }

        private void addReleaseDate_Clicked(object sender, EventArgs e)
        {

        }
    }
}