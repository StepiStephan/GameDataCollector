using System.ComponentModel;
using test.ViewModels;
using Xamarin.Forms;

namespace test.Views
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