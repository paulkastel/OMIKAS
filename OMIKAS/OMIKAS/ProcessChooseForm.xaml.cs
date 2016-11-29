using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class ProcessChooseForm : ContentPage
	{
		/// <summary>
		/// Konstruktor okna
		/// </summary>
		public ProcessChooseForm()
		{
			InitializeComponent();
			selectedAlloys = new List<Alloy>();
			selectedSmelts = new List<Alloy>();
		}

		/// <summary>
		/// strona do wyboru stopow
		/// </summary>
		private SelectMultipleBasePage<Alloy> multiPageAlloys;
		/// <summary>
		/// strona do wyboru wytopow
		/// </summary>
		private SelectMultipleBasePage<Alloy> multiPageSmelts;

		private List<Alloy> selectedAlloys;
		private List<Alloy> selectedSmelts;

		private async void btn_chooseAlloy_Clicked(object sender, EventArgs e)
		{
			if(multiPageAlloys == null)
				multiPageAlloys = new SelectMultipleBasePage<Alloy>(App.alloymetals) { Title = "Metale do obliczeń" };

			await Navigation.PushAsync(multiPageAlloys);
		}

		private async void btn_chooseSmelt_Clicked(object sender, EventArgs e)
		{
			if(multiPageSmelts == null)
				multiPageSmelts = new SelectMultipleBasePage<Alloy>(App.alloysmelts) { Title = "Wytopy do obliczeń" };

			await Navigation.PushAsync(multiPageSmelts);
		}

		private List<Alloy> WhatisSelected(SelectMultipleBasePage<Alloy> page, Label lbl)
		{
			if(page != null)
			{
				lbl.Text = "Wybrano: ";
				List<Alloy> selected = page.GetSelection();
				foreach(Alloy item in selected)
				{
					lbl.Text += item.nameAlloy + ", ";
				}
				return selected;
			}
			else
			{
				lbl.Text = "Wybrano: ";
				List<Alloy> tmp = new List<Alloy>();
				return tmp;
			}

		}
		/// <summary>
		/// Za każdym razem gdy pokaze się okno wczytaj wybrane stopy i wytopy aby przejsc dalej muszą być wybrane conajmniej 1 stop i conajmniej 1 wytop
		/// </summary>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			selectedAlloys = WhatisSelected(multiPageAlloys, lbl_selectedAlloys);
			selectedSmelts = WhatisSelected(multiPageSmelts, lbl_selectedSmelts);
			if(!selectedAlloys.Any() || !selectedSmelts.Any())
			{
				btn_count.IsEnabled = false;
			}
			else
				btn_count.IsEnabled = true;
		}

		private async void btn_count_Clicked(object sender, EventArgs e)
		{
			if(!(selectedSmelts.Count > 1))
			{
				await Navigation.PushAsync(new ProcessCalcForm(selectedAlloys, selectedSmelts, SwitchMinMax.IsToggled, SwitchPriceWeight.IsToggled));
			}
			else
				await DisplayAlert("Error", "Ilość wybranych wytopów musi wynosić 1", "OK");
		}

		private async void btntool_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
