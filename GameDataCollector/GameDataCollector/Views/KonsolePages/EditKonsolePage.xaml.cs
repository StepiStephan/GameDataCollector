using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataClasses;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditKonsolePage : ContentPage
    {
        private IKonsoleViewModel viewModel;
        private Konsole konsole;
        public EditKonsolePage(Konsole konsole)
        {
            this.konsole = konsole;
            viewModel = App.ServiceProvider.GetService<IKonsoleViewModel>();

            InitializeComponent();
            Title = $"Konsole {konsole.Name} bearbeiten";
            konsoleName.Text = konsole.ConsoleName;
            konsoleType.Text = konsole.Name;
            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (konsoleName.Text != null && konsoleType.Text != null && konsoleType.Text != null)
            {
                viewModel.EditKonsole(konsole.Id, konsoleName.Text, konsoleType.Text);
                await DisplayAlert("Konsole geändert",
                    $"Konsole wurde auf {konsoleName.Text} mit dem Konsolentyp {konsoleType.Text} geändert",
                     "OK");
                await Navigation.PopToRootAsync();
            }
        }
    }
}