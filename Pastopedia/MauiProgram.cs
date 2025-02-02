using CommunityToolkit.Maui;
using Material.Components.Maui.Extensions;
using MaterialColorUtilities.Maui;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;


namespace Pastopedia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMaterialComponents()
                .UseMaterialColors()
                .UseMauiMTAdmob()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //.RegisterFirebaseServices()
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        /*

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events => {
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                {
                    CrossFirebase.Initialize(activity, CreateCrossFirebaseSettings());
                    Firebase.FirebaseApp.InitializeApp(activity);
                    Plugin.Firebase.Analytics.FirebaseAnalyticsImplementation.Initialize(activity);
                }));
            });


            builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);

            return builder;
        }


        private static CrossFirebaseSettings CreateCrossFirebaseSettings()
        {
            // bool isDynamicLinksEnabled = false, bool isFirestoreEnabled = false, bool isFunctionsEnabled = false, bool isRemoteConfigEnabled = false, bool isStorageEnabled = false, )
            return new CrossFirebaseSettings(
                isAnalyticsEnabled: true,
                isCrashlyticsEnabled: true,
                isAuthEnabled: true,
                isCloudMessagingEnabled: true);
        }*/
    }
}
