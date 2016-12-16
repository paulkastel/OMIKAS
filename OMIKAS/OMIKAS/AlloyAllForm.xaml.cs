using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class AlloyAllForm : ContentPage
	{

		public AlloyAllForm()
		{
			InitializeComponent();
			//alloymetalView.ItemsSource = App.alloymetals;
			alloymetalView.ItemsSource = App.DAUtil.GetAllAlloys();
		}

		private async void btn_info_Clicked(object sender, EventArgs e)
		{
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;
			await Navigation.PushAsync(new AlloyDetailForm(metal));
			//await Navigation.PushAsync(new AlloyDetailForm(App.DAUtil.ge));
		}

		private async void btn_delAlloy_Clicked(object sender, EventArgs e)
		{
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;
			var answer = await DisplayAlert("Usun", "Na pewno usunac " + metal.name + "?", "Tak", "Nie");
			if(answer)
			{
				//App.alloymetals.RemoveAt(App.alloymetals.IndexOf(metal));
				App.DAUtil.DeleteAlloy(metal);
			}
			OnAppearing();
		}

		private async void btn_addAlloy_Clicked(object sender, EventArgs e)
		{
			//ustawic argumenty w AlloyEditForm tak aby rozorznial czy wywolala go edycja czy add
			await Navigation.PushAsync(new AlloyEditForm());
		}

		private async void btn_editAlloy_Clicked(object sender, EventArgs e)
		{
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;
			await Navigation.PushAsync(new AlloyEditForm(metal, App.alloymetals.IndexOf(metal)));
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
		/// <summary>
		/// Jak sie pojawia ekran z lista stopow/wytopow to wczytaj odpowiednia liste
		/// </summary>
		protected override void OnAppearing()
		{
			//Dla poprawnego dzialania zeruje widok listy i
			alloymetalView.ItemsSource = null;
			alloymetalView.ItemsSource = App.DAUtil.GetAllAlloys();
		}
	}
}
