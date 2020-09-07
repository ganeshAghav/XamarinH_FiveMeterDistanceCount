using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Support.V4.App;
using Android.Util;
using Android.Webkit;
using BlackFramework.Droid.Controller;
using ModelViewUI;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;

namespace BlackFramework.Droid
{
    [Activity(Label = "DistanceInMeter", MainLauncher = true, NoHistory = false, Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.AdjustPan)]
    public class MainActivity : Activity
    {

        string AppName = "Count Distance In Meter";
        const int REQUEST_WRITE_PERMISSION = 1;
        const int REQUEST_READ_PERMISSION = 2;
        const int REQUEST_LOCATION_PERMISSION = 3;
        const int REQUEST_CAMERA = 4;
        const int REQUEST_RECORD_AUDIO_PERMISSION = 5;
        const int REQUEST_SMSSENT = 6;
        const int REQUEST_PHONESTATE = 7;

        static string[] PERMISSIONS_PHONESTATE = {
        Manifest.Permission.ReadPhoneState
        };

        static string[] PERMISSIONS_SMS = {
        Manifest.Permission.SendSms
        };

        static string[] PERMISSIONS_LOCATION = {
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessCoarseLocation
        };

        static string[] PERMISSIONS_CAMARA = {
            Manifest.Permission.Camera
        };

        static string[] PERMISSIONS_AUDIO = {
            Manifest.Permission.RecordAudio
        };

        static string[] PERMISSIONS_Read = {

            Manifest.Permission.ReadExternalStorage
        };
        static string[] PERMISSIONS_WRIGHT = {

            Manifest.Permission.WriteExternalStorage,

        };

        EditText edtLat, edtLong;
        TextView txtResult;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            string PhoneModel = Android.OS.Build.Model;

            SetContentView(Resource.Layout.Main);

            edtLat = FindViewById<EditText>(Resource.Id.edtlati);
            edtLong = FindViewById<EditText>(Resource.Id.edtlongi);
            txtResult = FindViewById<TextView>(Resource.Id.textResult);

            Button button = FindViewById<Button>(Resource.Id.btnfindlocation);

            button.Click += delegate {
                ShowMapActivity();
            };

            ResourceManager.EnsureResources(
                     typeof(IDataAccess).Assembly,
                     String.Format("/data/data/{0}/files", Application.Context.PackageName));

            RequestSMSPermission();
        }

        void RequestSMSPermission()
        {
            Log.Info(AppName, "SMS permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) == (int)Android.Content.PM.Permission.Granted)
            {
                RequestWrightPermission();
            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_SMS, REQUEST_SMSSENT);
            }
        }

        void RequestWrightPermission()
        {
            Log.Info(AppName, "CAMERA permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Android.Content.PM.Permission.Granted)
            {
                RequestReadPermission();

            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_WRIGHT, REQUEST_WRITE_PERMISSION);
            }
        }

        void RequestReadPermission()
        {
            Log.Info(AppName, "Read permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Android.Content.PM.Permission.Granted)
            {
                RequestLocationPermission();

            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_Read, REQUEST_READ_PERMISSION);
            }
        }

        void RequestLocationPermission()
        {
            Log.Info(AppName, "Location permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == (int)Android.Content.PM.Permission.Granted)
            {
                RequestCameraPermission();

            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_LOCATION, REQUEST_LOCATION_PERMISSION);
            }
        }

        void RequestCameraPermission()
        {
            Log.Info(AppName, "CAMERA permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Android.Content.PM.Permission.Granted)
            {
                RequestAudioPermission();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS_CAMARA, REQUEST_CAMERA);

            }
        }

        void RequestAudioPermission()
        {
            Log.Info(AppName, "Read Phone State permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.RecordAudio) == (int)Android.Content.PM.Permission.Granted)
            {
                RequestReadPhoneStatePermission();

            }
            else
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS_AUDIO, REQUEST_RECORD_AUDIO_PERMISSION);
            }
        }

        void RequestReadPhoneStatePermission()
        {
            Log.Info(AppName, "SMS permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadPhoneState) == (int)Android.Content.PM.Permission.Granted)
            {
                ShowMapActivity();
                //RegisterControllersToPortableRazor();
            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_PHONESTATE, REQUEST_PHONESTATE);
            }
        }

        void RegisterControllersToPortableRazor()
        {
           
            var webView = FindViewById<WebView>(Resource.Id.webView);
            PortableRazor.RouteHandler.Controllers.Clear();
            var LoginController = new LoginController(new HybridWebView(webView),this);
            PortableRazor.RouteHandler.RegisterController("LoginController", LoginController);
            LoginController.OnCreate();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case REQUEST_READ_PERMISSION:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            //Permission granted
                            RequestLocationPermission();

                        }
                        else
                        {
                            Toast.MakeText(this, "Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_WRITE_PERMISSION:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            //Permission granted
                            RequestReadPermission();

                        }
                        else
                        {
                            Toast.MakeText(this, "Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_LOCATION_PERMISSION:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            RequestCameraPermission();
                        }
                        else
                        {
                            Toast.MakeText(this, " LOCATION Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_RECORD_AUDIO_PERMISSION:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {

                            //RegisterControllersToPortableRazor();
                        }
                        else
                        {
                            Toast.MakeText(this, " LOCATION Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_CAMERA:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            //Permission granted
                            RequestAudioPermission();
                        }
                        else
                        {
                            Toast.MakeText(this, " LOCATION Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_SMSSENT:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            RequestWrightPermission();
                        }
                        else
                        {
                            Toast.MakeText(this, " SMS Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_PHONESTATE:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            //RegisterControllersToPortableRazor();
                            ShowMapActivity();
                        }
                        else
                        {
                            Toast.MakeText(this, " PHONESTATE Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;

            }



        }


        public async void ShowMapActivity()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync();

                edtLat.Text=position.Latitude.ToString();
                edtLong.Text = position.Longitude.ToString();

                double lat1 = 19.1746884;
                double long1 = 72.8464181;

                double result= GetDistanceBetweenPoints(position.Latitude, position.Longitude, lat1, long1);
                //Console.WriteLine(distance(32.9697, -96.80322, 29.46786, -98.53506, "M"));
                //Console.WriteLine(distance(32.9697, -96.80322, 29.46786, -98.53506, "K"));
                //Console.WriteLine(distance(32.9697, -96.80322, 29.46786, -98.53506, "N"));

                int disResult = Convert.ToInt32(result);

                if (disResult < 6) //5 is meters distance
                {
                    txtResult.Text = "You are in Branch";
                }
                else
                {
                    txtResult.Text = "You are not in Branch";
                }
               

            }
            catch (Exception ex)
            {
                txtResult.Text = ex.ToString();
            }

            

        }


        public double GetDistanceBetweenPoints(double lat1, double long1, double lat2, double long2)
        {
            double distance = 0;

            double dLat = (lat2 - lat1) / 180 * Math.PI;
            double dLong = (long2 - long1) / 180 * Math.PI;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                        + Math.Cos(lat2) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //Calculate radius of earth
            // For this you can assume any of the two points.
            double radiusE = 6378135; // Equatorial radius, in metres
            double radiusP = 6356750; // Polar Radius

            //Numerator part of function
            double nr = Math.Pow(radiusE * radiusP * Math.Cos(lat1 / 180 * Math.PI), 2);
            //Denominator part of the function
            double dr = Math.Pow(radiusE * Math.Cos(lat1 / 180 * Math.PI), 2)
                            + Math.Pow(radiusP * Math.Sin(lat1 / 180 * Math.PI), 2);
            double radius = Math.Sqrt(nr / dr);

            //Calaculate distance in metres.
            distance = radius * c;
            return distance;
        }


    }
}

