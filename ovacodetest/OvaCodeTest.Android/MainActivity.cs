using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using AndroidX.AppCompat.App;
using Xamarin.Forms.Platform.Android;
using Android.Views;

namespace OvaCodeTest.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : FormsAppCompatActivity
    {
        private int uiOptions;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            base.OnCreate(savedInstanceState);

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

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStart()
        {
            try
            {
                base.OnStart();
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
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnPause()
        {
            try
            {
                base.OnPause();
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
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
            catch (Exception)
            {
            }
        }
        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            try
            {
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
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
            catch (Exception)
            {
            }
            return base.OnKeyUp(keyCode, e);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            try
            {
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
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
            catch (Exception)
            {
            }
            return base.OnTouchEvent(e);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            try
            {
                //----------------- Hide Bottom Navigation -----------------//
                base.OnWindowFocusChanged(hasFocus);
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
#pragma warning disable CS0618 // Type or member is obsolete
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnResume()
        {
            try
            {
                base.OnResume();
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
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
            catch (Exception)
            {
            }
        }
        protected override void OnDestroy()
        {
            if (SplashActivity.running == true)
            {
                SplashActivity.fa.MoveTaskToBack(true);
                SplashActivity.fa.Finish();
                Finish();
                SplashActivity.running = false;
            }
            try
            {
                base.OnDestroy();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // hardware back button is pressed popup is closed
                //you don't need to do anything inside the if/else statement
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}