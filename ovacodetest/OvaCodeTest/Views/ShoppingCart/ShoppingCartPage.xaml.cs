using System;
using System.Diagnostics;
using System.Linq;
using CS.ERP.PL.POS.DAT;
using OvaCodeTest.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvaCodeTest.Views.ShoppingCart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCartPage : ContentPage
    {
        private readonly ShoppingCartViewModel shoppingCartViewModel;

        public ShoppingCartPage()
        {
            InitializeComponent();
            BindingContext = shoppingCartViewModel = new ShoppingCartViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await shoppingCartViewModel.LoadDataAsync();
            });
        }

        private async void ShoppingListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ShoppingListView.IsEnabled = false;
            RES_SHOPPING_DETAIL selected = e.Item as RES_SHOPPING_DETAIL;
            if (selected != null)
            {
                RES_SHOPPING shopping = shoppingCartViewModel.ShoppingList.Where(x => x.ShoppingCode == selected.ShoppingCode).FirstOrDefault();
                ShoppingPopupPage popup = new ShoppingPopupPage(shopping, selected);
                popup.Disappearing += (sende, est) => { OnAppearing(); };
                await PopupNavigation.Instance.PushAsync(popup);
            }
            ShoppingListView.IsEnabled = true;
        }

        private void ShoppingListView_Refreshing(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ShoppingListView.IsEnabled = false;
                    ShoppingListView.IsPullToRefreshEnabled = false;

                    await shoppingCartViewModel.LoadDataAsync();

                    ShoppingListView.IsEnabled = true;
                    ShoppingListView.IsPullToRefreshEnabled = true;
                    ShoppingListView.EndRefresh();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ShoppingListView.IsEnabled = true;
                ShoppingListView.IsPullToRefreshEnabled = true;
                ShoppingListView.EndRefresh();
            }
        }
    }
}
