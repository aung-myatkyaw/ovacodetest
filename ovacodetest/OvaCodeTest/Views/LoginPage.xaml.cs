using OvaCodeTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvaCodeTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        private void ShowHidePassword_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (loginPasswordBox.IsPassword)
                {
                    imgButton.Source = "show_pass.png";
                    imgButton.HeightRequest = 27;
                    loginPasswordBox.IsPassword = false;
                }
                else
                {
                    imgButton.Source = "hide_pass.png";
                    imgButton.HeightRequest = 27;
                    loginPasswordBox.IsPassword = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}