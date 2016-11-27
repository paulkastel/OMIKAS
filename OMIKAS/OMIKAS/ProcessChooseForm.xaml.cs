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
		public ProcessChooseForm()
		{
			InitializeComponent();
			selectedAlloys = new List<Alloy>();
			selectedSmelts = new List<Alloy>();
		}

		private SelectMultipleBasePage<Alloy> multiPageAlloys;
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
				return null;
			}

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			selectedAlloys = WhatisSelected(multiPageAlloys, lbl_selectedAlloys);
			selectedSmelts = WhatisSelected(multiPageSmelts, lbl_selectedSmelts);
		}

		private async void btn_count_Clicked(object sender, EventArgs e)
		{
			if(!(selectedSmelts.Count > 1))
			{
				await Navigation.PushAsync(new ProcessCalcForm(selectedAlloys, selectedSmelts, SwitchMinMax.IsToggled, SwitchPriceWeight.IsToggled));
			}
		}

		private async void btntool_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
