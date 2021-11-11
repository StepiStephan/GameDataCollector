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
    public partial class FirebasePage : ContentPage
    {
        IStartPageViewModel viewModel;

        public FirebasePage()
        {
            viewModel = App.ServiceProvider.GetService<IStartPageViewModel>();
            InitializeComponent();
        }
    }
}