using Android.Gms.Ads;
using Pastopedia.Data;
using Plugin.MauiMTAdmob.Extra;
using Plugin.MauiMTAdmob;
namespace Pastopedia
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            ContentManager.InitProgress += ContentManager_InitProgress;
            ContentManager.InitEnd += ContentManager_InitEnd;
            try
            {
                CrossMauiMTAdmob.Current.TagForChildDirectedTreatment = MTTagForChildDirectedTreatment.TagForChildDirectedTreatmentUnspecified;
                CrossMauiMTAdmob.Current.TagForUnderAgeOfConsent = MTTagForUnderAgeOfConsent.TagForUnderAgeOfConsentUnspecified;
                CrossMauiMTAdmob.Current.MaxAdContentRating = MTMaxAdContentRating.MaxAdContentRatingG;
                Plugin.MauiMTAdmob.Controls.MTAdView ad = new();
                ad.AdsId = "ca-app-pub-3088807533847490/2341291875";
                ad.AdSize = Plugin.MauiMTAdmob.Extra.BannerSize.Banner;
                ad.HorizontalOptions = LayoutOptions.Center;
                slAds.Children.Add(ad);
            }
            catch(Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }

        }

        private void ContentManager_InitProgress(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    lblProgressInfo.IsVisible = true;
                    lblProgressInfo.Text = (string)sender;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });
        }

        private async void btnTestOpenPastaInfoPage_Clicked(object sender, EventArgs e)
        {
            try
            {
                Pasta p = new Pasta();
                p.Title = "Biedronki";
                p.Content = "Biedronki mają, ze wszystkich owadów, najbardziej #!$%@? software, jaki istnieje. Serio #!$%@?.\r\nBo taka na ten przykład pszczoła - #!$%@?, szuka kwiatów, nawiguje na słońce, zbiera nektar, znosi do gniazda - no w #!$%@? skomplikowane. Zresztą, #!$%@? - mucha. Mucha też musi wyczuwać woń gówna i ścierwa, nawigować, szukać pożywienia, złożyć jaja w jakimś dobrym ścierwie. Wcale nie takie proste.\r\nA jak działa biedronka?\r\nOtóż pierwszy punkt algorytmu biedronki brzmi:\r\n\r\n1. #!$%@? w górę.\r\nNie wiem, czy bawiliście się kiedyś biedronkami. Jak się to weźmie na rękę, to zawsze idzie w góre. Zawsze #!$%@?. Zawsze. Jak się jej w połowie drogi odwróci rękę, to się zatrzyma i zmieni kierunek, żeby isć w górę. Można se tak ruszać pińćset razy i zawsze #!$%@? w górę. Biedronka to taki #!$%@? owadzi odpowiednik gradientu. Zawsze się kieruje w górę zbocza.\r\nMa to w #!$%@? zabawne konsekwencje, jak się biedronkę postawi na płaskiej powierzchni. Zaczyna iść w przypadkowym kierunku, bo #!$%@? nie ma jak iść pod górę. Najlepiej jak dojdzie do krawędzi. Wtedy ten jej #!$%@? mósk cannot into logika, tylko zaczyna iść wzdłuż krawędzi. W dół nie zejdzie, bo #!$%@? algorytm, a na płaskie też nie wróci, #!$%@? wie czemu.\r\nKiedyś z kolegą ze studiów znaleźliśmy na wykładzie biedronkę. Ławki w auli były w #!$%@? długie, na jakieś 30 m. Mówię mu: pacz #!$%@? i położyłem biedronkę na ławce. Doszła do krawędzi i idzie wzdłuż. Idzie #!$%@? i idzie, doszła do końca, skręciła i dalej wzdłuż kolejnej krawędzi, a potem jeszcze raz i szła znowu w naszą stronę. 45 minut #!$%@? szła przez te 30 metrów i ni #!$%@? #!$%@? nie ogarnęła, że sytuacja jest #!$%@? i może by gdzieś polecieć, bo gówno. Nie, #!$%@?.\r\nJakby ławka miała kilometr, to by #!$%@? zdechła po drodze.\r\nCiąg dalszy algorytmu brzmi:\r\n\r\n2. Jak jesteś na szczycie to poleć byle #!$%@? gdzie.\r\nJak już pozwolicie wejść biedronce na szczyt palca, czy #!$%@?, czy czegoś, to obróci się dookoła, upewni, że gradient się wyzerował i fruuuu #!$%@? w losowym kierunku. Czasem potrafi wylądować znowy na tej samej ręce xD\r\n\r\n3. Jak już leziesz i coś znajdziesz do żarcia, to #!$%@?.\r\nNie no, akurat całkiem spoko punkt. Tylko #!$%@?, metoda szukania po #!$%@?.\r\n\r\n4. Jak cię coś przestraszy - to się zesraj.\r\nWiem, że ta żółtobrązowa maź to nie sraka, ale #!$%@?, jakie lepsze słowo może to opisać. Ewentualnie rzygi? #!$%@? tam semantyka. Ważne, że jak biedronkę spotka zagrożenie, to zwija nóżki i sra tym smrodem. #!$%@?. #!$%@?. Przecież ma skrzydła, mogłaby #!$%@?ć. Ale nie, #!$%@?. Zesraj się. Najlepsze jak się #!$%@? biedronkę do pajęczyny. Jak byłem mały to wrzucałem tam różne owady. Jak wrzucisz muchę, to do ostatniej chwili walczy, żeby odlecieć i nawet czasem zdąży, zanim ją pająk dorwie. Rzadko, ale jednak. Pszczoła to już w ogóle siła, stronk i mało która pajęczyna utrzyma.\r\nA biedronka co?\r\nZESRA SIĘ #!$%@?. Przychodzi pająk, paczy, #!$%@?.jpg.\r\nCo robi? XDDDDD owija #!$%@?ę pajęczyną i #!$%@? z pajęczyny. Nie dość, że #!$%@? nie zje, bo smrut, to jeszcze #!$%@? zdechnie z głodu, bo ją opędzlował jak #!$%@? pajęczyną XDDDDD\r\nAcha - jeszcze jedno - przyglądaliście się, jak wyglądają oczy biedronki? To nie są te #!$%@? wielkie białe plamy. Nie. Oczy biedronki to małe kropeczki na tej durnej mordzie. Co ona tym widzi? Pewno #!$%@? widzi. Mucha ma #!$%@? w kosmos, dizajnerskie oczy, bo musi być czujna, patrzeć, analizować. To samo pszczoła.\r\nALe nie #!$%@? biedronka. Po #!$%@? jej oczy, jak tylko lezie w górę, lata pisiont centymetrów i sra? xD\r\nW tym roku #!$%@?ło biedronek jak #!$%@?. Możecie się pobawić, sprawdzić. Tylko uwaga - bo SIĘ ZESRAJĄ!";
                p.Guid = Guid.NewGuid();
                p.Author = "N/A";
                p.Tags = new List<string>();
                p.Tags.Add("biedronki");
                await Navigation.PushAsync(new Pages.PastaInfoPage(p));
            }
            catch(Exception ex)
            {
                await DisplayAlert("Błąd", ex.ToString(), "Ok");
                Console.WriteLine(ex.ToString());
            }
        }

        private async void btnOpenPast(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                if(button.CommandParameter == null)
                {
                    return;
                }
                Pasta pasta = (Pasta)button.CommandParameter;
                if(pasta == null)
                {
                    return;
                }
                await Navigation.PushAsync(new Pages.PastaInfoPage(pasta));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", ex.ToString(), "Ok");
                Console.WriteLine(ex.ToString());
            }
        }
        private async void btnOpenPast2(object sender, EventArgs e)
        {
            try
            {
                Material.Components.Maui.Button button = (Material.Components.Maui.Button)sender;
                if(button.CommandParameter == null)
                {
                    await DisplayAlert("", "Wystąpił błąd", "Ok");
                    return;
                }
                string path = (string)button.CommandParameter;
                if (string.IsNullOrEmpty(path))
                {
                    await DisplayAlert("", "Wystąpił błąd", "Ok");
                    return;
                }
                if(!File.Exists(path))
                {
                    await DisplayAlert("", "Wystąpił błąd", "Ok");
                    return;
                }
/*
                Pasta pasta = ContentManager

                await Navigation.PushAsync(new Pages.PastaInfoPage());*/
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", ex.ToString(), "Ok");
                Console.WriteLine(ex.ToString());
            }
        }
        bool flc = false;
        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            if(flc == false)
            {
                
                Thread th = new(() =>
                {
                    try
                    {
                        Thread.Sleep(1000);
                        ContentManager.Init();
                    }
                    catch(Exception ex)
                    {
                        LogManager.Log(ex);
                        Console.WriteLine(ex.ToString());
                        try
                        {
                            Thread th2 = new(() =>
                            {
                                try
                                {
                                    Thread.Sleep(2500);
                                    ContentManager.Init();
                                }
                                catch(Exception ex3)
                                {
                                    LogManager.Log(ex3);
                                    Console.WriteLine(ex3.ToString());
                                }
                            });
                            th2.Name = "R2_ContentManager.Init";
                            th2.Start();
                        }
                        catch (Exception ex2)
                        {
                            LogManager.Log(ex2);
                            Console.WriteLine(ex2.ToString());
                        }
                    }
                });
                th.Name = "ContentManager.Init";
                th.Start();
                flc = true;
            }
        }

        private void ContentManager_InitEnd(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in ContentManager.AllPast)
                {
                    Button button = new();
                    button.Text = item.Title;
                    button.CommandParameter = item;
                    button.Clicked += btnOpenPast;
                    button.HorizontalOptions = LayoutOptions.FillAndExpand;
                    button.Margin = new Thickness(5);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        slC.Children.Add(button);
                    });
                }
                lblProgressInfo.IsVisible = false;
                TheSpinner.IsVisible = false;
            }
            catch(Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }
        }
    }

}
