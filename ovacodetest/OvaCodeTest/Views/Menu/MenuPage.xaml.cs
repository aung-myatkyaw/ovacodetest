using CS.ERP.PL.SYS.DAT;
using OvaCodeTest.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvaCodeTest.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        private readonly MenuViewModel viewModel;

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MenuViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.LoadMenu();
                viewModel.GenerateTemplate();
            });
        }

        bool disableSelect;
        private void MenuListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if (disableSelect)
                {
                    return;
                }
                disableSelect = true;
                MenuListView.IsEnabled = false;
                RES_MENU menuItem = e.Item as RES_MENU;
                if (menuItem == null)
                    return;

                viewModel.SelectedMenu = menuItem;

                viewModel.GenerateTemplate();
                MenuListView.IsEnabled = true;
                disableSelect = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MenuListView.IsEnabled = true;
                disableSelect = false;
            }
        }
    }
}