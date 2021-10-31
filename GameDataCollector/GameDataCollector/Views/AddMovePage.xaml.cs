using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevExpress.XamarinForms.Charts;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMovePage : ContentPage
    {
        public AddMovePage()
        {
            InitializeComponent();
            Title = "Hinzufügen und Verschieben";
            PieChartView pieChartView = new PieChartView();
            stackLayout.Children.Add(pieChartView);
            
            addKonsoleButton.Clicked += AddKonsoleButton_Clicked;
            addStorageButton.Clicked += AddStorageButton_Clicked;
            addGameButton.Clicked += AddGameButton_Clicked;
            moveGameButton.Clicked += MoveGameButton_Clicked;
            moveStorageButton.Clicked += MoveStorageButton_Clicked;
        }

        private async void MoveStorageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoveStoragePage());
        }

        private async void MoveGameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoveGamePage());
        }

        private async void AddGameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddGamePage());
        }

        private async void AddStorageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddStoragePage());
        }

        private async void AddKonsoleButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddKonsolePage());
        }
    }
}