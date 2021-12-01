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
    public partial class FireBaseSaveLoadPage : ContentPage
    {
        private IFireBaseViewModel viewmodel;
        public FireBaseSaveLoadPage()
        {
            viewmodel = App.ServiceProvider.GetService<IFireBaseViewModel>();
            InitializeComponent();
            result.IsVisible = false;
            save.Clicked += Save_Clicked;
            load.Clicked += Load_Clicked;
            viewmodel.DatabaseLoaded += WorkFlow_DatabaseLoaded;
            viewmodel.DatabaseSaved += WorkFlow_DatabaseSaved;
            viewmodel.LoggedInStateChanged += Viewmodel_LoggedInStateChanged;
            Appearing += FireBaseSaveLoadPage_Appearing;
            SetButtons(viewmodel.LoggedIn);
        }

        private void Viewmodel_LoggedInStateChanged(object sender, EventArgs e)
        {
            SetButtons(viewmodel.LoggedIn);
        }

        private void FireBaseSaveLoadPage_Appearing(object sender, EventArgs e)
        {
            result.IsVisible = false;
        }

        private void WorkFlow_DatabaseSaved(object sender, EventArgs e)
        {
            result.Text = "Erfolgreich Gespeichert";
            result.IsVisible = true;
        }

        private void WorkFlow_DatabaseLoaded(object sender, EventArgs e)
        {
            result.Text = "Erfolgreich Geladen";
            result.IsVisible = true;
        }

        private async void Load_Clicked(object sender, EventArgs e)
        {
            SetSpinner(true);
            SetButtons(false);
            await viewmodel.LoadData();
            SetButtons(true);
            SetSpinner(false);
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            SetSpinner(true);
            SetButtons(false);
            await viewmodel.SaveData();
            SetButtons(true);
            SetSpinner(false);
        }

        private void SetButtons(bool active)
        {
            save.IsEnabled = active;
            load.IsEnabled = active;
        }

        private void SetSpinner(bool active)
        {
            result.IsVisible = !active;
            spinner.IsRunning = active;
            spinner.IsVisible = active;
        }
    }
}