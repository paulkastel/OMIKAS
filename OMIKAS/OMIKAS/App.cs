using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace OMIKAS
{
	public class App : Application
	{
		public static bool IsUserLoggedIn { get; set; }
		public static void setHomePageApp(string whoEntered)
		{
			var x = new MasterDetailPage();
			x.MasterBehavior = MasterBehavior.Popover;
			x.Detail = new NavigationPage(new MainForm(whoEntered));
			x.Master = new MainMenuSliderForm();
			App.Current.MainPage = x;
		}
		public App()
		{
			//Uruchom strone zawierajaca MenuSlider czyli MainForm
			//MainPage = new NavigationPage(new MainMenuSliderForm(""));
			if(!IsUserLoggedIn)
			{
				MainPage = new NavigationPage(new UserLoginForm());
			}
			else
			{
				MasterDetailPage u = new MasterDetailPage();
				u.MasterBehavior = MasterBehavior.Popover;
				u.Title = "nowy master";
				u.Master = new MainMenuSliderForm();
				u.Detail = new NavigationPage(new MainForm("xdwa"));
				MainPage = u;
			}

			//------------------Ustawienie koloru naglowka na zielony---------------
			Current.Resources = new ResourceDictionary();
			var navigationStyle = new Style(typeof(NavigationPage));
			var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.FromHex("#00693c") };
			navigationStyle.Setters.Add(barBackgroundColorSetter);
			Current.Resources.Add(navigationStyle);

			//-----------------------------------------------------------------------
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
