using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views.FireBaseControllPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FireBaseSaveLoadPage : ContentPage
    {
        private IFireBaseWorkFlow workFlow;
        public FireBaseSaveLoadPage()
        {
            workFlow = App.ServiceProvider.GetService<IFireBaseWorkFlow>();
            InitializeComponent();
            result.IsVisible = false;
            save.Clicked += Save_Clicked;
            load.Clicked += Load_Clicked;
            workFlow.DatabaseLoaded += WorkFlow_DatabaseLoaded;
            workFlow.DatabaseSaved += WorkFlow_DatabaseSaved;
            Appearing += FireBaseSaveLoadPage_Appearing;
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
            await workFlow.LoadDataOnFireBase();
            SetButtons(true);
            SetSpinner(false);
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            SetSpinner(true);
            SetButtons(false);
            await workFlow.SaveDataOnFireBase();
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