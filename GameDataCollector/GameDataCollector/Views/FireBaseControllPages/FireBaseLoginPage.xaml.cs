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
            viewModel.LoggedInStateChanged += ViewModel_LoggedInStateChanged;
            SetLogInState(true);
        }

        private void ViewModel_LoggedInStateChanged(object sender, EventArgs e)
        {
            SetLogInState(!viewModel.LoggedIn);
        }

        private void SetLogInState(bool state)
        {
            logOut.IsEnabled = !state;
            logIn.IsEnabled = state;
            register.IsEnabled = state;
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await  Navigation.PushAsync(new FireBaseRegisterPage());
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            viewModel.LogOut();
        }

        private async void LogIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                viewModel.LogIn(email.Text, passwort.Text);
            }
            catch
            {
                await DisplayAlert("Login Fehler", "Emailadresse und Passwort stimmen nicht überein", "OK");
            }
        }
    }
}