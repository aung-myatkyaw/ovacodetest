using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;

namespace OvaCodeTest.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        public static Activity fa;
        public static bool running = false;
        private int uiOptions;

        protected override void OnCreate(Bundle bundle)
        {
            //----------------- Prevent Notch UI -----------------//
            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var statusBarHeightInfo = typeof(FormsAppCompatActivity).GetField("statusBarHeight", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                statusBarHeightInfo?.SetValue(this, 0);
            }


            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                IWindowInsetsController insetsController = Window.InsetsController;
                if (insetsController != null)
                {
                    insetsController.Hide(WindowInsets.Type.NavigationBars());
                    insetsController.Hide(WindowInsets.Type.StatusBars());
                }
            }
            else
            {
                //----------------- Hide Bottom Navigation -----------------//
#pragma warning disable CS0618 // Type or member is obsolete
                uiOptions = (int)Window.DecorView.SystemUiVisibility;
#pragma warning restore CS0618 // Type or member is obsolete
                uiOptions |= (int)SystemUiFlags.Immersive;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.LayoutHideNavigation;

#pragma warning disable CS0618 // Type or member is obsolete
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
#pragma warning restore CS0618 // Type or member is obsolete
                Window.AddFlags(WindowManagerFlags.Fullscreen);
                Window.AddFlags(WindowManagerFlags.LayoutInOverscan);
                Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
                Window.ClearFlags(WindowManagerFlags.TranslucentNavigation);
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            }

            SetContentView(Resource.Layout.SplashLayout);
            base.OnCreate(bundle);
            fa = this;
            running = true;
            RunOnUiThread(async () =>
            {
                if (Build.VERSION.SdkInt != BuildVersionCodes.O)
                {
                    await Task.Delay(2000);
                }

                Intent intent = new Intent(this, typeof(MainActivity));
                if (Intent.Extras != null)
                {
                    intent.PutExtras(Intent.Extras);
                }
                StartActivity(intent);
            });
        }
    }
}