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
		public MainMenuSliderForm()
		{
			InitializeComponent();
		}
		private async void btn_profile_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new ProfileForm()));
		}

		private async void btn_alloy_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new AlloyAllForm()));
		}

		private async void btn_smelts_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new SmeltAllForm()));
		}

		private async void btn_settings_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new SetTabbForm()));
		}

		private async void btn_calc_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new ProcessChooseForm()));
		}
		
		/// <summary>
		/// Za kazdym razem kiedy pojawia się ekran sprawdź czy listy stopow i wytopow i dopiero wtedy zezwól na obliczenia
		/// </summary>
		protected override void OnAppearing()
		{
			App.alloymetals = App.DAUtil.GetAllAlloys();
			App.smeltals = App.DAUtil.GetAllSmelts();
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
