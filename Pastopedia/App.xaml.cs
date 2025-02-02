using MaterialColorUtilities.Maui;
using Plugin.MauiMTAdmob;

namespace Pastopedia
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CrossMauiMTAdmob.Current.UserPersonalizedAds = true;
            CrossMauiMTAdmob.Current.ComplyWithFamilyPolicies = true;
            CrossMauiMTAdmob.Current.UseRestrictedDataProcessing = true;
            CrossMauiMTAdmob.Current.AdsId = DeviceInfo.Platform == DevicePlatform.Android ? "ca-app-pub-3088807533847490/2341291875" : "ca-app-pub-3088807533847490/2341291875";
            CrossMauiMTAdmob.Current.TestDevices = new List<string>() { };

            IMaterialColorService.Current.Initialize(this.Resources);
            MainPage = new AppShell();
        }
    }
}
