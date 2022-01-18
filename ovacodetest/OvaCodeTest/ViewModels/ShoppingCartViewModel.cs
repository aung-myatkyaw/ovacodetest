using Acr.UserDialogs;
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using Newtonsoft.Json.Linq;
using OvaCodeTest.Services.ShoppingCart;
using OvaCodeTest.Utils;
using OvaCodeTest.Views.MessagePopup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OvaCodeTest.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        RES_SHOPPING selectedShopping;
        public RES_SHOPPING SelectedShopping
        {
            get { return selectedShopping; }
            set { SetProperty(ref selectedShopping, value); }
        }

        RES_SHOPPING_DETAIL selectedDetail;
        public RES_SHOPPING_DETAIL SelectedDetail
        {
            get { return selectedDetail; }
            set { SetProperty(ref selectedDetail, value); }
        }

        double selectedPrice = 0;
        public double SelectedPrice
        {
            get { return selectedPrice; }
            set { SetProperty(ref selectedPrice, value); }
        }

        int selectedQTY = 0;
        public int SelectedQTY
        {
            get { return selectedQTY; }
            set { SetProperty(ref selectedQTY, value); }
        }

        double selectedTotal = 0;
        public double SelectedTotal
        {
            get { return selectedTotal; }
            set { SetProperty(ref selectedTotal, value); }
        }

        bool refreshEnabled = true;
        public bool RefreshEnabled
        {
            get { return refreshEnabled; }
            set { SetProperty(ref refreshEnabled, value); }
        }

        private readonly IShoppingCartService cartService;

        public Command CartRefreshCommand { get; }
        public ObservableCollection<RES_SHOPPING> ShoppingList { get; set; }
        public ObservableCollection<RES_SHOPPING_DETAIL> ShoppingDetailsList { get; set; }

        public ShoppingCartViewModel()
        {
            cartService = DependencyService.Get<IShoppingCartService>();

            CartRefreshCommand = new Command(RefreshCartAsync);

            ShoppingList = new ObservableCollection<RES_SHOPPING>();
            ShoppingDetailsList = new ObservableCollection<RES_SHOPPING_DETAIL>();
        }

        private async void RefreshCartAsync()
        {
            try
            {
                RefreshEnabled = false;

                await LoadDataAsync();

                RefreshEnabled = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                RefreshEnabled = true;
            }
        }

        public async Task LoadDataAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await App.ExecuteIfConnected(async () =>
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Loading...", MaskType.Black);

                    JSN_REQ_SHOPPING obj = new JSN_REQ_SHOPPING
                    {
                        REQ_AUTHORIZATION = App.AuthorizeObject
                    };
                    JObject jObject = JObject.FromObject(obj);

                    // call get cart api
                    JSN_SHOPPING resobj = await cartService.GetDataAsync(jObject);

                    if (resobj.Message.Code == "7")
                    {
                        ShoppingList.Clear();
                        ShoppingDetailsList.Clear();

                        resobj.RES_SHOPPING.ForEach(item => ShoppingList.Add(item));
                        resobj.RES_SHOPPING_DETAIL.ForEach(item => ShoppingDetailsList.Add(item));
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new MessagePopupPage("Alert", resobj.Message.Message));
                    }
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;
                }
            });
            IsBusy = false;
        }

        public async Task UpdateDataAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await App.ExecuteIfConnected(async () =>
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Loading...", MaskType.Black);

                    var tmpList = new List<RES_SHOPPING_DETAIL>();
                    tmpList.Add(SelectedDetail);

                    JSN_REQ_SHOPPING obj = new JSN_REQ_SHOPPING
                    {
                        REQ_AUTHORIZATION = App.AuthorizeObject,
                        RES_SHOPPING = SelectedShopping,
                        RES_SHOPPING_DETAIL = tmpList
                    };
                    JObject jObject = JObject.FromObject(obj);

                    // call login api
                    JSN_SHOPPING resobj = await cartService.SaveDataAsync(jObject);

                    if (resobj.Message.Code == "7")
                    {
                        DependencyService.Get<Toast>().Show("Updated");
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new MessagePopupPage("Alert", resobj.Message.Message));
                    }
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;
                }
            });
            IsBusy = false;
        }
    }
}
