using CS.ERP.PL.POS.RES;
using OvaCodeTest.Services.HttpClientProvider;
using OvaCodeTest.Services.ShoppingCart;
using OvaCodeTest.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingCartService))]
namespace OvaCodeTest.Services.ShoppingCart
{
    class ShoppingCartService : IShoppingCartService
    {
        readonly IHttpClientProvider httpClientProvider;
        Dictionary<string, string> headers = new Dictionary<string, string>();

        public ShoppingCartService()
        {
            httpClientProvider = DependencyService.Get<IHttpClientProvider>();
        }

        public async Task<JSN_SHOPPING> GetDataAsync(dynamic obj)
        {

            JSN_SHOPPING LoginResObj = new JSN_SHOPPING();
            try
            {
                string baseuri = GlobalSettings.POSAuthenticationEndpoint;
                string jsonString = await httpClientProvider.PostAsync(baseuri, "Service.svc/getShopping", obj);
                LoginResObj = GlobalFunction.ModelConverter<JSN_SHOPPING>(jsonString);
            }
            catch (Exception ex)
            {
                LoginResObj.Message.Code = "0";
                LoginResObj.Message.Message = "Something went wrong";
                Debug.WriteLine(ex.Message);
            }
            return LoginResObj;
        }

        public async Task<JSN_SHOPPING> SaveDataAsync(dynamic obj)
        {

            JSN_SHOPPING LoginResObj = new JSN_SHOPPING();
            try
            {
                string baseuri = GlobalSettings.POSAuthenticationEndpoint;
                string jsonString = await httpClientProvider.PostAsync(baseuri, "Service.svc/saveShopping", obj);
                LoginResObj = GlobalFunction.ModelConverter<JSN_SHOPPING>(jsonString);
            }
            catch (Exception ex)
            {
                LoginResObj.Message.Code = "0";
                LoginResObj.Message.Message = "Something went wrong";
                Debug.WriteLine(ex.Message);
            }
            return LoginResObj;
        }
    }
}
