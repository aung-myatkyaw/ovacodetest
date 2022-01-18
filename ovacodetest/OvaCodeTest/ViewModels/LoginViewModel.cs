using Acr.UserDialogs;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using Newtonsoft.Json.Linq;
using OvaCodeTest.Services.Login;
using OvaCodeTest.Utils;
using OvaCodeTest.Views;
using OvaCodeTest.Views.MessagePopup;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OvaCodeTest.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        string loginUsername = string.Empty;
        public string LoginUsername
        {
            get { return loginUsername; }
            set { SetProperty(ref loginUsername, value); }
        }

        string loginPassword = string.Empty;
        public string LoginPassword
        {
            get { return loginPassword; }
            set { SetProperty(ref loginPassword, value); }
        }

        bool isChecked = false;
        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }
        }
        public Command LoginCommand { get; }
        private readonly ILoginService loginService;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            loginService = DependencyService.Get<ILoginService>();

            // Remember Check
            string remember = GlobalFunction.ReadAppSetting("Checked");
            if (remember.Equals("true"))
            {
                LoginUsername = GlobalFunction.ReadAppSetting("Username");
                LoginPassword = GlobalFunction.ReadAppSetting("Password");
                IsChecked = true;
            }
        }

        private async void OnLoginClicked()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await App.ExecuteIfConnected(async () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(LoginUsername))
                    {
                        await PopupNavigation.Instance.PushAsync(new MessagePopupPage("Alert", "Please fill username."));
                    }
                    else if (string.IsNullOrEmpty(LoginPassword))
                    {
                        await PopupNavigation.Instance.PushAsync(new MessagePopupPage("Alert", "Please fill password."));
                    }
                    else
                    {
                        UserDialogs.Instance.ShowLoading("Loading...", MaskType.Black);
                        REQ_AUTHORIZATION obj = new REQ_AUTHORIZATION
                        {
                            ControlAsk = "700",
                            DeviceName = DeviceInfo.Model,
                            Location = "",
                            MenuAsk = "700",
                            ProductAsk = "2",
                            TranBrowserType = "",
                            TranDateTime = "",
                            TranIPAddres = "",
                            TransactionName = "",
                            UserID = LoginUsername,
                            UserPassword = LoginPassword
                        };
                        JObject jObject = JObject.FromObject(obj);
                        
                        ////testing
                        //App.AuthorizeObject = obj;
                        //if (IsChecked)
                        //{
                        //    GlobalFunction.WriteAppSetting("Checked", "true");
                        //    GlobalFunction.WriteAppSetting("Username", LoginUsername.Trim());
                        //    GlobalFunction.WriteAppSetting("Password", LoginPassword.Trim());
                        //}
                        //else
                        //{
                        //    GlobalFunction.WriteAppSetting("Checked", "false");
                        //}
                        //Application.Current.MainPage = new AppShell();

                        // call login api
                        JSN_RES_MOBILE_LOGIN resobj = await loginService.LoginAsync(jObject);

                        if (resobj.Message.Code == "7")
                        {
                            // save for later api calls
                            App.AuthorizeObject = obj;

                            // save menu
                            App.Menu = resobj.menu;

                            // Remember check
                            if (IsChecked)
                            {
                                GlobalFunction.WriteAppSetting("Checked", "true");
                                GlobalFunction.WriteAppSetting("Username", LoginUsername.Trim());
                                GlobalFunction.WriteAppSetting("Password", LoginPassword.Trim());
                            }
                            else
                            {
                                GlobalFunction.WriteAppSetting("Checked", "false");
                            }

                            Application.Current.MainPage = new AppShell();
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new MessagePopupPage("Alert", resobj.Message.Message));
                        }
                        UserDialogs.Instance.HideLoading();
                    }
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
