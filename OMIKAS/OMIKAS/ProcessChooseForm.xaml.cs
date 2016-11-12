using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;

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
			SimplexSolver solver = new SimplexSolver();
			try
			{
				int savid, vzvid;
				solver.AddVariable("Saudi Arabia", out savid);
				solver.SetBounds(savid, 0, 9000);
				solver.AddVariable("Venezuela", out vzvid);
				solver.SetBounds(vzvid, 0, 6000);

				int gasoline, jetfuel, machinelubricant, cost;
				solver.AddRow("gasoline", out gasoline);
				solver.AddRow("jetfuel", out jetfuel);
				solver.AddRow("machinelubricant", out machinelubricant);
				solver.AddRow("cost", out cost);

				solver.SetCoefficient(gasoline, savid, 0.3);
				solver.SetCoefficient(gasoline, vzvid, 0.4);
				solver.SetBounds(gasoline, 2000, Rational.PositiveInfinity);
				solver.SetCoefficient(jetfuel, savid, 0.4);
				solver.SetCoefficient(jetfuel, vzvid, 0.2);
				solver.SetBounds(jetfuel, 1500, Rational.PositiveInfinity);
				solver.SetCoefficient(machinelubricant, savid, 0.2);
				solver.SetCoefficient(machinelubricant, vzvid, 0.3);
				solver.SetBounds(machinelubricant, 500, Rational.PositiveInfinity);

				solver.SetCoefficient(cost, savid, 20);
				solver.SetCoefficient(cost, vzvid, 15);
				solver.AddGoal(cost, 1, true);

				solver.Solve(new SimplexSolverParams());
				await DisplayAlert("Wynik solvera", solver.GetValue(savid).ToDouble().ToString(), "ok");
			}
			catch
			{

			}
		}

		private async void btntool_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
