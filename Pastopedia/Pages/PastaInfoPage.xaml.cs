using Android.Graphics;
using Pastopedia.Data;
using System.Diagnostics;

namespace Pastopedia.Pages
{
    public partial class PastaInfoPage : ContentPage
    {
        public Pastopedia.Data.Pasta pasta;
        public PastaInfoPage(Pastopedia.Data.Pasta pastaa)
        {
            InitializeComponent();
            pasta = pastaa;
            try
            {
                Plugin.MauiMTAdmob.Controls.MTAdView ad = new();
                ad.AdsId = "ca-app-pub-3088807533847490/9156862104";
                ad.AdSize = Plugin.MauiMTAdmob.Extra.BannerSize.Banner;
                ad.HorizontalOptions = LayoutOptions.Center;
                slAds.Children.Add(ad);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

        }

        bool flc = false;
        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            if(flc == false)
            {
                if(pasta == null)
                {
                    await DisplayAlert("Wyst¹pi³ b³¹d!", "B³¹d", "Ok");
                    await Navigation.PopAsync();
                    return;
                }

                lblTitle.Text = pasta.Title;
                lblGuid.Text = "GUID:   "+pasta.Guid.ToString();
                if (!string.IsNullOrEmpty(pasta.Author))
                {
                    if(pasta.Author != "N/A")
                    {
                        lblAuthor.Text = pasta.Author;
                        lblAuthor.IsVisible = true;
                    }
                    
                }
                string p = pasta.Content;
                lblPreviewContent.Text = p.Substring(0, 100)+"...";

                flc = true;
            }
        }

        private async void btnOpenReadPlayer_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Pages.ReadPlayerPasta(pasta));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

