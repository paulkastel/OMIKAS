using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class MainMenuSliderForm : ContentPage
	{
		/// <summary>
		/// Konstruktor okna z menu glownym aplikacji
		/// </summary>
		public MainMenuSliderForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Wciskając przycisk, push modal okno z profilem
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_profile_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new ProfileForm()));
		}
		/// <summary>
		/// Wciskajac przycisk push modal okno z skladnikami stopowymi
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_alloy_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new AlloyAllForm()));
		}

		/// <summary>
		/// Wciskajac przycisk push modal okno z wytopami
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_smelts_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new SmeltAllForm()));
		}

		/// <summary>
		/// Wciskajac przycisk push modal okno z ustawieniami
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_settings_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new SetTabbForm()));
		}

		/// <summary>
		/// Wciskajac przycisk push modal okno do obliczen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_calc_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new ProcessChooseForm()));
		}
		
		/// <summary>
		/// Za kazdym razem kiedy pojawia się ekran sprawdź czy listy stopow i wytopow nie sa puste i dopiero wtedy zezwól na obliczenia
		/// </summary>
		protected override void OnAppearing()
		{
			//pobieram z bazy sqlite wszystkie stopy i wytopy do list
			App.alloymetals = App.DAUtil.GetAllAlloys();
			App.smeltals = App.DAUtil.GetAllSmelts();

			//jesli ktoras pusta to zabron obliczen
			if(!App.alloymetals.Any() || !App.smeltals.Any())
			{
				btn_calc.IsEnabled = false;
			}
			else
				btn_calc.IsEnabled = true;

			base.OnAppearing();
		}
	}
}
