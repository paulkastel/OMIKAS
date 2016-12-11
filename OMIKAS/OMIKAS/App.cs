using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace OMIKAS
{
	public class App : Application
	{
		/// <summary>
		/// Zmienna sprawdzająca czy użytkownik jest zalogowany. True jeżeli jest, False jeżeli nie zalogowany
		/// </summary>
		public static bool IsUserLoggedIn { get; set; }

		/// <summary>
		/// Ustawia glowna strone aplikacji z menu
		/// </summary>
		/// <param name="whoEntered">Nazwa uzytkownika który uruchomil strone glowna z menu aplikacji</param>
		public static void setHomePageApp(string whoEntered)
		{
			MasterDetailPage x = new MasterDetailPage();
			x.MasterBehavior = MasterBehavior.Popover;		//Zachowanie bocznego panelu
			x.Detail = new NavigationPage(new MainForm(whoEntered)); //Strona glowna do ktorej jest przypiete menu boczne
			x.Master = new MainMenuSliderForm();	//MainMenuSliderForm jako boczny panel
			App.Current.MainPage = x; //Ustawia się strone jako strone glowna aplikacji
		}
		/// <summary>
		/// Lista z skladnikami stopowymi przechowujaca wszystkie stopy metali ich dane
		/// </summary>
		public static List<Alloy> alloymetals;
		/// <summary>
		/// Lista z wytopami i ich danymi
		/// </summary>
		public static List<Smelt> smeltals;

		/// <summary>
		/// Uzytkownik aplikacji przechowujacy dane o uzytkowniku
		/// </summary>
		public static User userapp;
		
		/// <summary>
		/// Kazdorazowe uruchomienie aplikacji
		/// </summary>
		public App()
		{
			if(!IsUserLoggedIn)
			{
				//Jezeli apka nie zna uzytkownika (nowy user to zainicjalizuj nowe dane i uruchom ekran logowania
				userapp = new User();
				alloymetals = new List<Alloy>();
				smeltals = new List<Smelt>();
				MainPage = new NavigationPage(new UserLoginForm());

			}
			else
			{
				//Apka zna uzytownika
				setHomePageApp("Ktos kto juz tu byl");
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
