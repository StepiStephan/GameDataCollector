using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddKonsolePage : ContentPage
    {
        private IKonsoleViewModel viewModel;
        public AddKonsolePage()
        {
            viewModel = App.ServiceProvider.GetService<IKonsoleViewModel>();
            InitializeComponent();
            Title = "Konsole hizufügen";
            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (konsoleName.Text != null && konsoleTyp.Text != null && speicherGB.Text != null)
            {
                var konsoleNameText = konsoleName.Text;
                var konsoleTypText = konsoleTyp.Text;
                float.TryParse(speicherGB.Text, out float space);
                viewModel.CreateKonsole(konsoleTypText, konsoleNameText, space);
                await DisplayAlert("Konsole wurde hinzugefügt", $"{konsoleTypText} wurde mit dem Namen {konsoleNameText} hinzugefügt", "OK");
                konsoleName.Text = string.Empty;
                konsoleTyp.Text = string.Empty;
                speicherGB.Text = string.Empty;
                await Navigation.PopToRootAsync();
            }
        }
    }
}