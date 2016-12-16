using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class SmeltAllForm : ContentPage
	{
		public SmeltAllForm()
		{
			InitializeComponent();
			//smeltsmetalView.ItemsSource = App.smeltals;
			smeltsmetalView.ItemsSource = App.DAUtil.GetAllSmelts();
		}
		private async void btn_info_Clicked(object sender, EventArgs e)
		{
			var signal = sender as Button;
			var smelt = signal.BindingContext as Smelt;
			await Navigation.PushAsync(new SmeltDetailForm(smelt));
		}
		private async void btn_delSmelt_Clicked(object sender, EventArgs e)
		{
			var signal = sender as Button;
			var smelt = signal.BindingContext as Smelt;
			var answer = await DisplayAlert("Usun", "Na pewno usunac " + smelt.name + "?", "Tak", "Nie");
			if(answer)
			{
				//App.smeltals.RemoveAt(App.smeltals.IndexOf(smelt));
				App.DAUtil.DeleteSmelt(smelt);
			}
			OnAppearing();
		}
		private async void btn_addSmelt_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SmeltEditForm());
		}


		private async void btn_editSmelt_Clicked(object sender, EventArgs e)
		{
			var signal = sender as Button;
			var metal = signal.BindingContext as Smelt;
			await Navigation.PushAsync(new SmeltEditForm(metal, App.smeltals.IndexOf(metal)));
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		protected override void OnAppearing()
		{
			//Dla poprawnego dzialania zeruje widok listy i
			smeltsmetalView.ItemsSource = null;
			//smeltsmetalView.ItemsSource = App.smeltals;
			smeltsmetalView.ItemsSource = App.DAUtil.GetAllSmelts();
		}
	}
}
