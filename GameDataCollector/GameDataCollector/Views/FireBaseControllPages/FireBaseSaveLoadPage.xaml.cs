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
            save.Clicked += Save_Clicked;
            load.Clicked += Load_Clicked;
        }

        private async void Load_Clicked(object sender, EventArgs e)
        {
            await workFlow.LoadDataOnFireBase();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            await workFlow.SaveDataOnFireBase();
        }
    }
}