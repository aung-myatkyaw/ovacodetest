using System;
using System.Diagnostics;
using CS.ERP.PL.POS.DAT;
using OvaCodeTest.Utils;
using OvaCodeTest.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvaCodeTest.Views.ShoppingCart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingPopupPage : PopupPage
    {
        private readonly ShoppingCartViewModel viewModel;
        public ShoppingPopupPage(RES_SHOPPING shopping, RES_SHOPPING_DETAIL detail)
        {
            InitializeComponent();
            BindingContext = viewModel = new ShoppingCartViewModel();

            viewModel.SelectedShopping = shopping;
            viewModel.SelectedDetail = detail;

            if (double.TryParse(viewModel.SelectedDetail.Price, out double price))
            {
                viewModel.SelectedPrice = price;
            }

            if (decimal.TryParse(viewModel.SelectedDetail.QTY, out decimal qty))
            {
                viewModel.SelectedQTY = (int)qty;
            }

            if (double.TryParse(viewModel.SelectedDetail.TotalAmount, out double total))
            {
                viewModel.SelectedTotal = total;
            }

            CloseWhenBackgroundIsClicked = false;
            closeButton.Opacity = 1;
        }
        private async void Update_Clicked(object sender, EventArgs e)
        {
            try
            {
                updateButton.IsEnabled = false;

                Debug.WriteLine("Updated");
                await viewModel.UpdateDataAsync();
                await PopupNavigation.Instance.PopAsync();

                updateButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                updateButton.IsEnabled = true;
            }
        }

        bool disableClose;
        private async void Close_Tapped(object sender, EventArgs e)
        {

            try
            {
                if (disableClose)
                    return;

                disableClose = true;
                await PopupNavigation.Instance.PopAsync();
                disableClose = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                disableClose = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true; // Disable back button
        }

        private void DownTapped(object sender, EventArgs e)
        {
            try
            {
                // add qty
                if (viewModel.SelectedQTY > 0)
                {
                    viewModel.SelectedQTY -= 1;
                    viewModel.SelectedDetail.QTY = viewModel.SelectedQTY.ToString();

                    //modify total
                    double total = viewModel.SelectedPrice * viewModel.SelectedQTY;
                    viewModel.SelectedDetail.TotalAmount = total.ToString();
                    viewModel.SelectedTotal = total;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void UpTapped(object sender, EventArgs e)
        {
            try
            {
                // add qty
                viewModel.SelectedQTY += 1;
                viewModel.SelectedDetail.QTY = viewModel.SelectedQTY.ToString();

                //modify total
                double total = viewModel.SelectedPrice * viewModel.SelectedQTY;
                viewModel.SelectedDetail.TotalAmount = total.ToString();
                viewModel.SelectedTotal = total;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}