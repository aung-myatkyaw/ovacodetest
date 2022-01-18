using Android.Content;
using OvaCodeTest.Controls;
using OvaCodeTest.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace OvaCodeTest.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control.Background = new Android.Graphics.Drawables.ColorDrawable(Android.Graphics.Color.Transparent);
            //Control.SetBackgroundColor(global::Android.Graphics.Color.White);

            // placeholder text color
            Control.SetHintTextColor(global::Android.Graphics.Color.Rgb(182, 182, 182));
        }

    }
}