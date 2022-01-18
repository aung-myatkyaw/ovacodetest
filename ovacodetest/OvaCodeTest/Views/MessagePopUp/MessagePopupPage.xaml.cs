using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OvaCodeTest.Views.MessagePopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePopupPage : PopupPage
    {
        int flag = 0;
        public MessagePopupPage(string title, string message, int f = 1)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            MessageLabel.Text = message;
            flag = f;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //if (flag == 2)
            //{
            //    UserDialogs.Instance.ShowLoading("Loading...", MaskType.Black);
            //    MessagingCenter.Send(this, "ContactFlag", true);
            //    UserDialogs.Instance.HideLoading();
            //}
            await PopupNavigation.Instance.PopAsync();

        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void BackIcon_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
