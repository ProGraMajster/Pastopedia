using Java.Util.Logging;
using Pastopedia.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pastopedia
{
    public static class ContentManager
    {
        static string PathMain = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
            "//Pastopedia//";
        static string PathContent = PathMain +"Content//";
        static string PathSettings = PathMain + "Settings//";

        static string PathPasta = PathContent + "Pasta//";

        static string PathTemp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
            "//Temp//";

        public static event EventHandler InitProgress;
        public static event EventHandler InitEnd;

        static int currentContentVersoion = 0;

        private static bool up = false;

        public static List<Pasta> AllPast = new List<Pasta>();

        public static void Init()
        {
            try
            {

                ProgressInfo("Sprawdzanie dostępności aktualizacji treści...");
                if (CheckUpdate())
                {
                    up = true;
                    if (Directory.Exists(PathContent))
                    {
                        Directory.Delete(PathContent, true);
                    }
                    if (Directory.Exists(PathTemp))
                    {
                        Directory.Delete(PathTemp, true);
                    }
                    ProgressInfo("Sprawdzanie struktury folderów...");
                    CheckAllDir();
                    ProgressInfo("Pobieranie aktualizacji...");
                    DownloadContent();
                    ProgressInfo("Wypakowywanie...");
                    Unpack();
                    ProgressInfo("Przenoszenie...");
                    ContentMove();
                }
                ProgressInfo("Sprawdzanie struktury folderów...");
                CheckAllDir();
                ProgressInfo("Wczytywanie treści...");
                AllPast = GetallPastas();
                ProgressInfo("Kończenie...");
                if(InitEnd != null)
                {
                    if(InitEnd.Target != null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            InitEnd.Invoke(AllPast, EventArgs.Empty);
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }
        }

        static void ProgressInfo(string message)
        {
            if(InitProgress  != null)
            {
                if(InitProgress.Target != null)
                {
                    InitProgress.Invoke(message, EventArgs.Empty);
                }
            }
        }
        public static string LinkToContent = @"https://github.com/ProGraMajster/Pastopedia.Content/archive/refs/heads/master.zip";
        public async static void DownloadContent()
        {
            try
            {
                System.Net.WebClient wb = new();
                wb.DownloadProgressChanged += (sender, args) => {
                    ProgressInfo("Pobieranie.. " + args.BytesReceived + "/" + args.TotalBytesToReceive);
                };

                //await wb.DownloadFileTaskAsync(new Uri(LinkToContent),MobilePaths.Temp + "content.zip");
                wb.DownloadFile(LinkToContent, PathTemp + "content.zip");
                wb.Dispose();
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Unpack()
        {
            try
            {
                if (!File.Exists(PathTemp + "content.zip"))
                {
                    _ = new FileNotFoundException();
                }
                if (Directory.Exists(PathTemp))
                {
                    //Directory.Delete(AppFolders.UpdatedContentFolder, true);
                    Directory.CreateDirectory(PathTemp);
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(PathTemp + "content.zip", PathTemp);
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        public static void ContentMove()
        {
            try
            {
                string rp = "Pastopedia.Content-master//Pastopedia.Content//";
                DirFilesMoveToDir(PathTemp + rp + "Pasty", PathPasta);
                File.Copy(PathTemp + rp + "version.txt",PathMain+ "version.txt",true);
            }
            catch(Exception ex ) 
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }
        }


        #region Versions content repo
        public static string GetCurrentContentVersion()
        {
            if (!File.Exists(PathContent + "version.txt"))
            {
                return "?";
            }
            string currentversion_string = File.ReadAllLines(PathMain + "version.txt")[0];
            return currentversion_string;
        }
        public static bool CheckUpdate()
        {
            try
            {
                if (!File.Exists(PathMain + "version.txt"))
                {
                    return true;
                }
                string currentversion_string = File.ReadAllLines(PathMain + "version.txt")[0];
                int currentversion = int.Parse(currentversion_string);
                WebClient webClient = new();
                string onlineversionstring = webClient.DownloadString("https://raw.githubusercontent.com/ProGraMajster/Pastopedia.Content/master/Pastopedia.Content/version.txt");
                int onlineversion = int.Parse(onlineversionstring);
                if (onlineversion > currentversion)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        #endregion


        private static void DirFilesMoveToDir(string pathSource, string pathnewloocation)
        {
            try
            {
                Console.WriteLine("pathSource:" + pathSource + " => pathnewloocation:" + pathnewloocation);
                if (Directory.Exists(pathnewloocation))
                {
                    Directory.Delete(pathnewloocation, true);
                    Directory.CreateDirectory(pathnewloocation);
                }
                else
                {
                    Directory.CreateDirectory(pathnewloocation);
                }
                DirectoryInfo directoryInfo = new(pathSource);
                DirectoryInfo directoryInfo2 = new(pathnewloocation);
                if (!directoryInfo.Exists)
                {
                    return;
                }
                if (!directoryInfo2.Exists)
                {
                    return;
                }
                foreach (var item in directoryInfo.GetFiles())
                {
                    item.MoveTo(pathnewloocation + item.Name, true);
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }
        }


        static void CheckAllDir()
        {
            CheckDir(PathMain);
            CheckDir(PathContent);
            CheckDir(PathSettings);
            CheckDir(PathTemp);
            CheckDir(PathPasta);
        }


        static void CheckDir(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public static List<Pasta> GetallPastas()
        {
            List<Pasta> list = new();
            try
            {
                DirectoryInfo directoryInfo = new(PathPasta);
                foreach (var item in directoryInfo.GetFiles())
                {
                    if (item.FullName.EndsWith(".json"))
                    {
                        Pasta p = (Pasta)DeserializationJsonEx(item.FullName, typeof(Pasta));
                        list.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }
            return list;
        }

        public static Pasta GetPastaFromPath(string path)
        {
            try
            {
                Pasta p = (Pasta)DeserializationJsonEx(path, typeof(Pasta));
                return p;
            }
            catch(Exception ex)
            {
                LogManager.Log(ex);
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public static string SerializationJsonGetString(object obj, Type type)
        {
            return JsonSerializer.Serialize(obj, type);
        }

        public static string SerializationJsonEx(object obj, Type type)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = true;
            return JsonSerializer.Serialize(obj, type, jsonSerializerOptions);
        }

        public static object DeserializationJsonEx(string path, Type type)
        {
            try
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.WriteIndented = true;
                return JsonSerializer.Deserialize(File.ReadAllText(path), type, jsonSerializerOptions);
            }
            catch (Exception)
            {
            }

            return null;
        }

    }
}
