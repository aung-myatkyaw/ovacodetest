using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using OvaCodeTest.Views;
using OvaCodeTest.Views.MessagePopup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OvaCodeTest
{
    public partial class App : Application
    {
        public static List<RES_MENU> Menu { get; set; }
        public static REQ_AUTHORIZATION AuthorizeObject { get; internal set; }

        public App()
        {
            InitializeComponent();
            Current.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static async Task ExecuteIfConnected(Func<Task> actionToExecuteIfConnected)
        {
            var current = Connectivity.NetworkAccess;
            //Check If Connected
            if (current == NetworkAccess.Internet)
            {
                await actionToExecuteIfConnected();
            }
            else
            {
                await ShowNetworkConnectionAlert();
            }

        }

        static async Task ShowNetworkConnectionAlert()
        {
            //await UserDialogs.Instance.AlertAsync("No Internet Connection", "Error", "OK");
            await PopupNavigation.Instance.PushAsync(new MessagePopupPage("Error", "No Internet Connection."));
        }
    }
}
