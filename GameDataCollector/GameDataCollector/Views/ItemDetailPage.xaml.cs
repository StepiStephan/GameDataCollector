using GameDataCollector.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GameDataCollector.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}