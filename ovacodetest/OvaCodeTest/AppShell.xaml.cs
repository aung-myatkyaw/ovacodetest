using OvaCodeTest.ViewModels;
using OvaCodeTest.Views;
using OvaCodeTest.Views.ShoppingCart;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace OvaCodeTest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ShoppingCartPage), typeof(ShoppingCartPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            App.AuthorizeObject = null;
            await Current.GoToAsync("//LoginPage");
        }

        long lastPress;
        protected override bool OnBackButtonPressed()
        {
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (currentTime - lastPress > 5000)
            {
                DependencyService.Get<Toast>().Show("Press back again to exit.");
                lastPress = currentTime;
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }

        }
    }
}
