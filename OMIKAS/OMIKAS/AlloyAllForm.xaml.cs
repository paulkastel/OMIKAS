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
			alloymetalView.ItemsSource = App.alloymetals;
        }

        private async void btn_info_Clicked(object sender, EventArgs e)
        {
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;

			await Navigation.PushAsync(new AlloyDetailForm(metal));
        }

        private async void btn_delAlloy_Clicked(object sender, EventArgs e)
        {
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;

			var answer = await DisplayAlert("Usun", "Czy chcesz usunac stop?", "Tak", "Nie");
            if(answer)
			{
				App.alloymetals.RemoveAt(App.alloymetals.IndexOf(metal));
			}
			OnAppearing();
        }

        private async void btn_addAlloy_Clicked(object sender, EventArgs e)
        {
			//ustawic argumenty w AlloyEditForm tak aby rozorznial czy wywolala go edycja czy add
			await Navigation.PushAsync(new AlloyEditForm("Add"));
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

		protected override void OnAppearing()
		{
			alloymetalView.ItemsSource = null;
			alloymetalView.ItemsSource = App.alloymetals;
			base.OnAppearing();
		}
	}
}
