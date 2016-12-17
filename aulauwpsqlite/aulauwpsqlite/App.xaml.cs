using SQLite.Net;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace aulauwpsqlite
{
    sealed partial class App : Application
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string DB_PATH = Path.Combine(Path.Combine
            (ApplicationData.Current.LocalFolder.Path, "PessoaManager.sqlite"));   
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            if (!CheckFileExists("PessoaManager.sqlite").Result)
            {
                using (var db = new SQLiteConnection
                    (new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    db.CreateTable<Pessoa>();
                }
            }
            Debug.WriteLine(DB_PATH);
        }
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = 
await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync
(fileName);
                return true;
            }
            catch
            {
            }
            return false;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
