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
		public AlloyAllForm(bool whatOperation)
        {
            InitializeComponent();
			
			whichList = whatOperation;
			if(whichList)
			{
				Title = "Stopy metali";
				alloymetalView.ItemsSource = App.alloymetals;
			}
			else
			{
				Title = "Wytopy";
				alloymetalView.ItemsSource = App.alloysmelts;
			}
        }

		private bool whichList;
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

			var answer = await DisplayAlert("Usun", "Na pewno usunac "+metal.nameAlloy+"?", "Tak", "Nie");
            if(answer)
			{
				if(whichList)
				{
					App.alloymetals.RemoveAt(App.alloymetals.IndexOf(metal));
				}
				else
				{
					App.alloysmelts.RemoveAt(App.alloysmelts.IndexOf(metal));
				}
			}
			OnAppearing();
        }

        private async void btn_addAlloy_Clicked(object sender, EventArgs e)
        {
			//ustawic argumenty w AlloyEditForm tak aby rozorznial czy wywolala go edycja czy add
			await Navigation.PushAsync(new AlloyEditForm("Add", whichList));
        }

        private async void btn_editAlloy_Clicked(object sender, EventArgs e)
        {
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;

			if(whichList)
			{
				await Navigation.PushAsync(new AlloyEditForm(metal, App.alloymetals.IndexOf(metal), whichList));
			}
			else
			{
				await Navigation.PushAsync(new AlloyEditForm(metal, App.alloysmelts.IndexOf(metal), whichList));
			}
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		protected override void OnAppearing()
		{
			alloymetalView.ItemsSource = null;
			if(whichList)
				alloymetalView.ItemsSource = App.alloymetals;
			else
				alloymetalView.ItemsSource = App.alloysmelts;
		}
	}
}
