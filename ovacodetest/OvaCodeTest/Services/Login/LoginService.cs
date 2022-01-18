using CS.ERP.PL.SYS.RES;
using OvaCodeTest.Services.Login;
using OvaCodeTest.Services.HttpClientProvider;
using OvaCodeTest.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginService))]
namespace OvaCodeTest.Services.Login
{
    class LoginService : ILoginService
    {
        readonly IHttpClientProvider httpClientProvider;
        Dictionary<string, string> headers = new Dictionary<string, string>();

        public LoginService()
        {
            httpClientProvider = DependencyService.Get<IHttpClientProvider>();
        }

        public async Task<JSN_RES_MOBILE_LOGIN> LoginAsync(dynamic obj)
        {

            JSN_RES_MOBILE_LOGIN LoginResObj = new JSN_RES_MOBILE_LOGIN();
            try
            {
                string baseuri = GlobalSettings.SystemAuthenticationEndpoint;
                string jsonString = await httpClientProvider.PostAsync(baseuri, "Service.svc/Login", obj);
                LoginResObj = GlobalFunction.ModelConverter<JSN_RES_MOBILE_LOGIN>(jsonString);
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
