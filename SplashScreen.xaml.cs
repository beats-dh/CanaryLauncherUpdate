using System;
using System.Windows;
using System.IO;
using System.Net;
using System.Windows.Threading;

namespace CanaryLauncherUpdate
{
	/// <summary>
	/// Lógica interna para SplashScreen.xaml
	/// </summary>
	public partial class SplashScreen : Window
	{
		WebClient webClient = new WebClient();

		string urlClient = "https://github.com/lucasgiovannibr/clientlauncherupdate/archive/refs/heads/main.zip";
		string urlVersion = "https://raw.githubusercontent.com/lucasgiovannibr/clientlauncherupdate/main/version.txt";
		string currentVersion = "";
		DispatcherTimer timer = new DispatcherTimer();

		public SplashScreen()
		{
			InitializeComponent();
			timer.Tick += new EventHandler(timer_SplashScreen);
			timer.Interval = new TimeSpan(0, 0, 5); // 5 seconds
			timer.Start();
		}

		public void timer_SplashScreen(object Source, EventArgs e)
		{
			// Check current version
			currentVersion = webClient.DownloadString(urlVersion);
			if (currentVersion == null)
			{
				this.Close();
			}

			// Check client download
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(urlClient);
			HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				this.Close();
			}

			if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/CanaryClient"))
			{
				Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/CanaryClient");
			}
			MainWindow mainWindow = new MainWindow();
			this.Close();
			mainWindow.Show();
			timer.Stop();
		}
	}
}
