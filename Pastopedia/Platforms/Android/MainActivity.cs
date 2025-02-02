﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Gms.Ads;
using Android.Views;
using System.Diagnostics;

namespace Pastopedia
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(
    new string[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "https",
    DataHost = "sites.google.com",
    DataPath = "/view/pastopedia",
    AutoVerify = true)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            MobileAds.Initialize(this);
            SetGDPR();
            //HandleIntent(Intent);
            //CreateNotificationChannelIfNeeded();
            //OnNewIntent(Intent);
        }

        protected override async void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            var data = intent.DataString;

            if (intent.Action != Intent.ActionView) return;
            if (string.IsNullOrWhiteSpace(data)) return;

            if (data.Contains("/test"))
            {

            }
        }

        /* private static void HandleIntent(Intent intent)
         {
             FirebaseCloudMessagingImplementation.OnNewIntent(intent);
         }

         private void CreateNotificationChannelIfNeeded()
         {
             if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
             {
                 CreateNotificationChannel();
             }
         }

         private void CreateNotificationChannel()
         {
             var channelId = $"{PackageName}.general";
             var notificationManager = (NotificationManager)GetSystemService(NotificationService);
             var channel = new NotificationChannel(channelId, "General", NotificationImportance.Default);
             notificationManager.CreateNotificationChannel(channel);
             FirebaseCloudMessagingImplementation.ChannelId = channelId;
             //FirebaseCloudMessagingImplementation.SmallIconRef = Resource.Drawable.ic;
         }*/

        private void SetGDPR()
        {
            Console.WriteLine(@"DEBUG: MainActivity.OnCreate: Starting consent management flow, via UserMessagingPlatform.");
            try
            {
#if DEBUG
                var debugSettings = new Xamarin.Google.UserMesssagingPlatform.ConsentDebugSettings.
        Builder(this)
                .SetDebugGeography(Xamarin.Google.UserMesssagingPlatform.ConsentDebugSettings
                        .DebugGeography
                        .DebugGeographyEea)
                .AddTestDeviceHashedId(Android.Provider.Settings.Secure.GetString(ContentResolver,
        Android.Provider.Settings.Secure.AndroidId))
                .Build();
#endif

                var requestParameters = new Xamarin.Google.UserMesssagingPlatform.
        ConsentRequestParameters
                    .Builder()
                    .SetTagForUnderAgeOfConsent(false)
#if DEBUG
                    .SetConsentDebugSettings(debugSettings)
#endif
                    .Build();

                var consentInformation = Xamarin.Google.UserMesssagingPlatform.
        UserMessagingPlatform.GetConsentInformation(this);

                consentInformation.RequestConsentInfoUpdate(
                    this,
                    requestParameters,
                    new GoogleUMPConsentUpdateSuccessListener(
                        () =>
                        {
                            // The consent information state was updated.
                            // You are now ready to check if a form is available.
                            if (consentInformation.IsConsentFormAvailable)
                            {
                                Xamarin.Google.UserMesssagingPlatform.UserMessagingPlatform.LoadConsentForm(
                                    this,
                                    new GoogleUMPFormLoadSuccessListener((Xamarin.Google.
        UserMesssagingPlatform.IConsentForm f) => {
            googleUMPConsentForm = f;
            googleUMPConsentInformation = consentInformation;
            Console.WriteLine(@"DEBUG: MainActivity.OnCreate: Consent management flow: LoadConsentForm has loaded a form, which will be shown if necessary, once the ViewModel is ready.");
            DisplayAdvertisingConsentFormIfNecessary();
        }),
                                    new GoogleUMPFormLoadFailureListener((Xamarin.Google.
        UserMesssagingPlatform.FormError e) => {
            // Handle the error.
            Console.WriteLine("ERROR: MainActivity.OnCreate: Consent management flow: failed in LoadConsentForm with error " + e.Message);

        }));
                            }
                            else
                            {
                                Console.WriteLine(@"DEBUG: MainActivity.OnCreate: Consent management flow: RequestConsentInfoUpdate succeeded but no consent form was available.");
                            }
                        }),
                    new GoogleUMPConsentUpdateFailureListener(
                        (Xamarin.Google.UserMesssagingPlatform.FormError e) =>
                        {
                            // Handle the error.
                            Console.WriteLine(@"ERROR: MainActivity.OnCreate: Consent management flow: failed in RequestConsentInfoUpdate with error " + e.Message);
                        })
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR: MainActivity.OnCreate: Exception thrown during consent management flow: ", ex);
            }
        }

        private Xamarin.Google.UserMesssagingPlatform.IConsentForm googleUMPConsentForm = null;
        private Xamarin.Google.UserMesssagingPlatform.IConsentInformation
        googleUMPConsentInformation = null;
        public void DisplayAdvertisingConsentFormIfNecessary()
        {
            try
            {
                if (googleUMPConsentForm != null && googleUMPConsentInformation != null)
                {
                    /* ConsentStatus:
                        Unknown = 0,
                        NotRequired = 1,
                        Required = 2,
                        Obtained = 3
                    */
                    if (googleUMPConsentInformation.ConsentStatus == 2)
                    {
                        Console.WriteLine(@"DEBUG: MainActivity. DisplayAdvertisingConsentFormIfNecessary: Consent form is being displayed.");
                        DisplayAdvertisingConsentForm();
                    }
                    else
                    {
                        Console.WriteLine(@"DEBUG: MainActivity. DisplayAdvertisingConsentFormIfNecessary: Consent form is not being displayed because consent status is " + googleUMPConsentInformation.ConsentStatus.ToString());
                    }
                }
                else
                {
                    Console.WriteLine(@"ERROR: MainActivity.DisplayAdvertisingConsentFormIfNecessary: consent form or consent information missing.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR: MainActivity.DisplayAdvertisingConsentFormIfNecessary: Exception thrown: ", ex);
            }
        }

        public void DisplayAdvertisingConsentForm()
        {
            try
            {
                if (googleUMPConsentForm != null && googleUMPConsentInformation != null)
                {
                    Console.WriteLine(@"DEBUG: MainActivity.DisplayAdvertisingConsentForm: Consent form is being displayed.");

                    googleUMPConsentForm.Show(this, new GoogleUMPConsentFormDismissedListener(
                            (Xamarin.Google.UserMesssagingPlatform.FormError f) =>
                            {
                                if (googleUMPConsentInformation.ConsentStatus == 2) // required
                                {
                                    Console.WriteLine(@"ERROR: MainActivity.DisplayAdvertisingConsentForm: Consent was dismissed; showing it again because consent is still required.");
                                    DisplayAdvertisingConsentForm();
                                }
                            }));
                }
                else
                {
                    Console.WriteLine(@"ERROR: MainActivity.DisplayAdvertisingConsentForm: consent form or consent information missing.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR: MainActivity.DisplayAdvertisingConsentForm: Exception thrown: ", ex);
            }
        }

        public class GoogleUMPConsentFormDismissedListener : Java.Lang.Object,
        Xamarin.Google.UserMesssagingPlatform.IConsentFormOnConsentFormDismissedListener
        {
            public GoogleUMPConsentFormDismissedListener(
        Action<Xamarin.Google.UserMesssagingPlatform.FormError> failureAction)
            {
                a = failureAction;
            }
            public void OnConsentFormDismissed(Xamarin.Google.UserMesssagingPlatform.FormError f)
            {
                a(f);
            }

            private Action<Xamarin.Google.UserMesssagingPlatform.FormError> a = null;
        }

        public class GoogleUMPConsentUpdateFailureListener : Java.Lang.Object,
        Xamarin.Google.UserMesssagingPlatform.IConsentInformationOnConsentInfoUpdateFailureListener
        {
            public GoogleUMPConsentUpdateFailureListener(
        Action<Xamarin.Google.UserMesssagingPlatform.FormError> failureAction)
            {
                a = failureAction;
            }
            public void OnConsentInfoUpdateFailure(Xamarin.Google.UserMesssagingPlatform.FormError f)
            {
                a(f);
            }

            private Action<Xamarin.Google.UserMesssagingPlatform.FormError> a = null;
        }

        public class GoogleUMPConsentUpdateSuccessListener : Java.Lang.Object,
        Xamarin.Google.UserMesssagingPlatform.IConsentInformationOnConsentInfoUpdateSuccessListener
        {
            public GoogleUMPConsentUpdateSuccessListener(Action successAction)
            {
                a = successAction;
            }

            public void OnConsentInfoUpdateSuccess()
            {
                a();
            }

            private Action a = null;
        }

        public class GoogleUMPFormLoadFailureListener : Java.Lang.Object,
        Xamarin.Google.UserMesssagingPlatform.UserMessagingPlatform
        .IOnConsentFormLoadFailureListener
        {
            public GoogleUMPFormLoadFailureListener(
        Action<Xamarin.Google.UserMesssagingPlatform.FormError> failureAction)
            {
                a = failureAction;
            }
            public void OnConsentFormLoadFailure(Xamarin.Google.UserMesssagingPlatform.FormError e)
            {
                a(e);
            }

            private Action<Xamarin.Google.UserMesssagingPlatform.FormError> a = null;
        }

        public class GoogleUMPFormLoadSuccessListener : Java.Lang.Object,
        Xamarin.Google.UserMesssagingPlatform.UserMessagingPlatform
        .IOnConsentFormLoadSuccessListener
        {
            public GoogleUMPFormLoadSuccessListener(
        Action<Xamarin.Google.UserMesssagingPlatform.IConsentForm> successAction)
            {
                a = successAction;
            }
            public void OnConsentFormLoadSuccess(
        Xamarin.Google.UserMesssagingPlatform.IConsentForm f)
            {
                a(f);
            }

            private Action<Xamarin.Google.UserMesssagingPlatform.IConsentForm> a = null;
        }
    }
}
