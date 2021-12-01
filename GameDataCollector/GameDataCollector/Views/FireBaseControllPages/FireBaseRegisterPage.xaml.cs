using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views.FireBaseControllPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FireBaseRegisterPage : ContentPage
    {
        IFireBaseViewModel viewModel;
        public FireBaseRegisterPage()
        {
            viewModel = App.ServiceProvider.GetService<IFireBaseViewModel>();
            InitializeComponent();
            register.Clicked += Register_Clicked;
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if(passwort.Text == passwortSecond.Text)
            {
                viewModel.Register(email.Text, passwort.Text);
                email.IsEnabled = false;
                passwort.IsEnabled = false;
                passwortSecond.IsEnabled = false;
                await DisplayAlert("Erfolgreich", "Konto wurde erfolgreich hinzugefügt", "OK");
                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("Passwort Fehler", "Passwörter stimman nicht überein", "OK");
            }
        }
    }
}