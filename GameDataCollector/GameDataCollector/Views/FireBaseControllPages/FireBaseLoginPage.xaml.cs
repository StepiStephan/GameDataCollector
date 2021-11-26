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
    public partial class FireBaseLoginPage : ContentPage
    {
        IFireBaseViewModel viewModel;
        public FireBaseLoginPage()
        {
            viewModel = App.ServiceProvider.GetService<IFireBaseViewModel>();
            InitializeComponent();
            logIn.Clicked += LogIn_Clicked;
            logOut.Clicked += LogOut_Clicked;
            register.Clicked += Register_Clicked;
            logOut.IsEnabled = false;
            logIn.IsEnabled = true;
            register.IsEnabled = true;
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            logOut.IsEnabled = true;
            logIn.IsEnabled = false;
            register.IsEnabled = false;
            await  Navigation.PushAsync(new FireBaseRegisterPage());
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            viewModel.LogOut();
            logOut.IsEnabled = false;
            logIn.IsEnabled = true;
            register.IsEnabled = true;
        }

        private async void LogIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                viewModel.LogIn(email.Text, passwort.Text);
                logOut.IsEnabled = true;
                logIn.IsEnabled = false;
                register.IsEnabled = false;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Login Fehler", "Emailadresse und PAsswort stimmen nicht überein", "OK");
            }
        }
    }
}