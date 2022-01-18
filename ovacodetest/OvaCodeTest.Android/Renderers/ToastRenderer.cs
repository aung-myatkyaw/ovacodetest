using Android.Widget;
using OvaCodeTest.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastRenderer))]
namespace OvaCodeTest.Droid
{
    public class ToastRenderer : Toast
    {      
        public void Show(string Message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, Message, ToastLength.Short).Show();
        }
    } 
}