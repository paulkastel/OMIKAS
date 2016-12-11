using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;

using Xamarin.Forms;
using Microsoft.SolverFoundation.Solvers;

namespace OMIKAS
{
	public partial class ProcessChooseForm : ContentPage
	{
		private SimplexSolver solver;
		/// <summary>
		/// Konstruktor okna
		/// </summary>
		public ProcessChooseForm()
		{
			InitializeComponent();
			selectedAlloys = new List<Alloy>();
			selectedSmelts = new List<Smelt>();
			solver = new SimplexSolver();
		}

		/// <summary>
		/// strona do wyboru stopow
		/// </summary>
		private SelectMultipleBasePage<Alloy> multiPageAlloys;
		/// <summary>
		/// strona do wyboru wytopow
		/// </summary>
		private SelectMultipleBasePage<Smelt> multiPageSmelts;

		private List<Alloy> selectedAlloys;
		private List<Smelt> selectedSmelts;

		private async void btn_chooseAlloy_Clicked(object sender, EventArgs e)
		{
			if(multiPageAlloys == null)
				multiPageAlloys = new SelectMultipleBasePage<Alloy>(App.alloymetals) { Title = "Metale do obliczeń" };

			await Navigation.PushAsync(multiPageAlloys);
		}

		private async void btn_chooseSmelt_Clicked(object sender, EventArgs e)
		{
			if(multiPageSmelts == null)
				multiPageSmelts = new SelectMultipleBasePage<Smelt>(App.smeltals) { Title = "Wytopy do obliczeń" };

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
					lbl.Text += item.name + ", ";
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

		private List<Smelt> WhatisSelected(SelectMultipleBasePage<Smelt> page, Label lbl)
		{
			if(page != null)
			{
				lbl.Text = "Wybrano: ";
				List<Smelt> selected = page.GetSelection();
				foreach(Smelt item in selected)
				{
					lbl.Text += item.name + ", ";
				}
				return selected;
			}
			else
			{
				lbl.Text = "Wybrano: ";
				List<Smelt> tmp = new List<Smelt>();
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
				double num = 0;
				//await Navigation.PushAsync(new ProcessCalcForm(selectedAlloys, selectedSmelts, SwitchPriceWeight.IsToggled));
				if(Double.TryParse(entWeight.Text, out num))
				{
					selectedSmelts.ElementAt(0).Weight = double.Parse(entWeight.Text);

					if(SwitchPriceWeight.IsToggled == false)
						calculate_price(selectedAlloys, selectedSmelts);
					else
						calculate_weight(selectedAlloys, selectedSmelts);
				}
				else
					await DisplayAlert("Error", "Popraw masę wytopu", "OK");
			}
			else
				await DisplayAlert("Error", "Ilość wybranych wytopów musi wynosić 1", "OK");
		}

		private async void btntool_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		private void calculate_price(List<Alloy> alloys, List<Smelt> smelt)
		{
			int[] tabVar = new int[alloys.Count]; //il stopow w wytopie
			int[] tabEq = new int[31]; //il pierwiastkow + war + f. celu

			try
			{
				//warunek nieujemnosci zmiennych
				for(int i = 0; i < tabVar.Count(); i++)
				{
					solver.AddVariable(alloys.ElementAt(i).name, out tabVar[i]);
					solver.SetBounds(tabVar[i], 0, Rational.PositiveInfinity);
				}

				//E(Aij*Xij>bi) i=1,2,..,n
				for(int i = 0; i < tabEq.Count() - 2; i++)
				{
					solver.AddRow("r" + i.ToString(), out tabEq[i]);
					for(int j = 0; j < selectedAlloys.Count; j++)
					{
						//zmienna, kazda zawartosc pierwiastka x jego wsp. parowania
						solver.SetCoefficient(tabEq[i], tabVar[j], selectedAlloys.ElementAt(j).tabOfElements[i] * (1 - smelt.ElementAt(0).evoporation[i]/100));
					}
					solver.SetBounds(tabEq[i], smelt.ElementAt(0).min_Norm[i] * smelt.ElementAt(0).Weight, smelt.ElementAt(0).max_Norm[i] * smelt.ElementAt(0).Weight);
				}

				solver.AddRow("weightCond", out tabEq[29]); //war masowy
				solver.AddRow("cost", out tabEq[30]); //f celu

				for(int i = 0; i < alloys.Count; i++)
				{
					solver.SetCoefficient(tabEq[29], tabVar[i], 1);
					solver.SetCoefficient(tabEq[30], tabVar[i], alloys.ElementAt(i).Price);
				}
				solver.SetBounds(tabEq[29], smelt.ElementAt(0).Weight, smelt.ElementAt(0).Weight);
				solver.AddGoal(tabEq[30], 1, true);

				solver.Solve(new SimplexSolverParams());

				DisplayAlert("Optymalizacja kosztów", "Koszt wynosi: " + solver.GetValue(tabEq[30]).ToDouble(), "OK");

				for(int i = 0; i < tabVar.Count(); i++)
				{
					DisplayAlert("Wynik zmiennych", "x" + i.ToString() + ": " + solver.GetValue(tabVar[i]).ToDouble().ToString(), "OK");
				}

			}
			catch(Exception ex)
			{
				DisplayAlert("Error", "Cos w solverze nie tak:\n" + ex.ToString(), "OK");
			}
		}

		private void calculate_weight(List<Alloy> alloys, List<Smelt> smelt)
		{
			int[] tabVar = new int[alloys.Count]; //il stopow w wytopie
			int[] tabEq = new int[30]; //il pierwiastkow + war + f. celu

			try
			{
				//warunek nieujemnosci zmiennych
				for(int i = 0; i < tabVar.Count(); i++)
				{
					solver.AddVariable(alloys.ElementAt(i).name, out tabVar[i]);
					solver.SetBounds(tabVar[i], 0, Rational.PositiveInfinity);
				}

				//E(Aij*Xij>bi) i=1,2,..,n
				for(int i = 0; i < tabEq.Count() - 1; i++)
				{
					solver.AddRow("r" + i.ToString(), out tabEq[i]);
					for(int j = 0; j < selectedAlloys.Count; j++)
					{
						//zmienna, kazda zawartosc pierwiastka x jego wsp. parowania
						solver.SetCoefficient(tabEq[i], tabVar[j], selectedAlloys.ElementAt(j).tabOfElements[i] * (100 - smelt.ElementAt(0).evoporation[i]));
					}
					solver.SetBounds(tabEq[i], smelt.ElementAt(0).min_Norm[i] * smelt.ElementAt(0).Weight, smelt.ElementAt(0).max_Norm[i] * smelt.ElementAt(0).Weight);
				}

				int M = 0;
				solver.AddVariable("M", out M);
				solver.SetBounds(M, smelt.ElementAt(0).Weight, selectedSmelts.ElementAt(0).Weight);

				solver.AddRow("weight", out tabEq[29]);
				for(int i=0; i<alloys.Count; i++)
				{
					solver.SetCoefficient(tabEq[29], tabVar[i], 1);
				}

				//Exj - 1M
				solver.SetCoefficient(tabEq[29], M, -1);
				solver.AddGoal(tabEq[29], 1, true);

				solver.Solve(new SimplexSolverParams());

				DisplayAlert("Optymalizacja masy", "Masowa funkcja celu: " + solver.GetValue(tabEq[29]).ToDouble(), "OK");

				for(int i = 0; i < tabVar.Count(); i++)
				{
					DisplayAlert("Wynik zmiennych", "x" + i.ToString() + ": " + solver.GetValue(tabVar[i]).ToDouble().ToString(), "OK");
				}


			}
			catch(Exception ex)
			{
				DisplayAlert("Error", "Cos w solverze nie tak:\n" + ex.ToString(), "OK");
			}

		}
	}
}
