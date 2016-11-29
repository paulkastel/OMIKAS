using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace OMIKAS
{
	public class ProcessCalcForm : ContentPage
	{
		private List<Picker> picklist;
		private SimplexSolver solver;

		private List<Alloy> selectedAlloys;
		private List<Alloy> selectedSmelts;
		/// <summary>
		/// if tue then mimimalizuj równanie
		/// </summary>
		private bool isMin;
		/// <summary>
		/// if true then optymalizacja masy if false optymalizacja kosztów
		/// </summary>
		private bool isCost;
		private int[] tabEq;
		private int[] tabVar;

		private void prepareCostLayout()
		{
			Title = "Optymalizacja kosztów";
			BackgroundColor = Color.White;
			var gridLayout = new Grid();
			gridLayout.HorizontalOptions = LayoutOptions.Center;

			//Trzy kolumny na rownania lewa strona, operator, prawa strona
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

			picklist = new List<Picker>();
			//19: 17 pierwiastkow + 1 warunek masowy + 1 rownanie minimalizujace
			for(int i = 0; i < 18; i++)
			{
				//na kazde rownanie przypada jeden rzad w layoucie
				gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });

				Picker pick = new Picker();
				pick.HeightRequest = 30;
				pick.Items.Add("<=");
				pick.Items.Add("=");
				pick.Items.Add(">=");
				pick.SelectedIndex = 1;
				picklist.Add(pick);

				//w kazdym rzedzie na srodku dodaj picker
				gridLayout.Children.Add(picklist.ElementAt(i), 1, i);		
			}

			#region definicja stringow rownan
			string eqFe = "Żelazo: ";
			string eqC = "Węgiel: ";
			string eqSi = "Krzem: ";
			string eqMn = "Mangan: ";
			string eqP = "Fosfor: ";
			string eqS = "Siarka: ";
			string eqCr = "Chrom: ";
			string eqMo = "Molibden: ";
			string eqNi = "Nikiel: ";
			string eqAl = "Aluminium: ";
			string eqCo = "Kobalt: ";
			string eqCu = "Miedź: ";
			string eqNb = "Niob: ";
			string eqTi = "Tytan: ";
			string eqV = "Wanad: ";
			string eqW = "Wolfram: ";
			string eqPb = "Ołów: ";
			string eqPrice = null;
			#endregion

			//Lewa strona
			foreach(Alloy met in selectedAlloys)
			{
				#region Utworz stringi rownan
				eqFe += met.Fe.ToString() + " ";
				eqC += met.C.ToString() + " ";
				eqSi += met.Si.ToString() + " ";
				eqMn += met.Mn.ToString() + " ";
				eqP += met.P.ToString() + " ";
				eqS += met.S.ToString() + " ";
				eqCr += met.Cr.ToString() + " ";
				eqMo += met.Mo.ToString() + " ";
				eqNi += met.Ni.ToString() + " ";
				eqAl += met.Al.ToString() + " ";
				eqCo += met.Co.ToString() + " ";
				eqCu += met.Cu.ToString() + " ";
				eqNb += met.Nb.ToString() + " ";
				eqTi += met.Ti.ToString() + " ";
				eqV += met.V.ToString() + " ";
				eqW += met.W.ToString() + " ";
				eqPb += met.Pb.ToString() + " ";
				eqPrice += met.Price.ToString() + " ";
				#endregion
			}

			#region Dodaj z lewej strony
			gridLayout.Children.Add(new Label { Text = eqFe }, 0, 0);
			gridLayout.Children.Add(new Label { Text = eqC }, 0, 1);
			gridLayout.Children.Add(new Label { Text = eqSi }, 0, 2);
			gridLayout.Children.Add(new Label { Text = eqMn }, 0, 3);
			gridLayout.Children.Add(new Label { Text = eqP }, 0, 4);
			gridLayout.Children.Add(new Label { Text = eqS }, 0, 5);
			gridLayout.Children.Add(new Label { Text = eqCr }, 0, 6);
			gridLayout.Children.Add(new Label { Text = eqMo }, 0, 7);
			gridLayout.Children.Add(new Label { Text = eqNi }, 0, 8);
			gridLayout.Children.Add(new Label { Text = eqAl }, 0, 9);
			gridLayout.Children.Add(new Label { Text = eqCo }, 0, 10);
			gridLayout.Children.Add(new Label { Text = eqCu }, 0, 11);
			gridLayout.Children.Add(new Label { Text = eqNb }, 0, 12);
			gridLayout.Children.Add(new Label { Text = eqTi }, 0, 13);
			gridLayout.Children.Add(new Label { Text = eqV }, 0, 14);
			gridLayout.Children.Add(new Label { Text = eqW }, 0, 15);
			gridLayout.Children.Add(new Label { Text = eqPb }, 0, 16);
			gridLayout.Children.Add(new Label { Text = "War masowy" }, 0, 17);
			#endregion

			#region Prawa strona
			eqFe = selectedSmelts.ElementAt(0).Fe.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqC = selectedSmelts.ElementAt(0).C.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqSi = selectedSmelts.ElementAt(0).Si.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqMn = selectedSmelts.ElementAt(0).Mn.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqP = selectedSmelts.ElementAt(0).P.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqS = selectedSmelts.ElementAt(0).S.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqCr = selectedSmelts.ElementAt(0).Cr.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqMo = selectedSmelts.ElementAt(0).Mo.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqNi = selectedSmelts.ElementAt(0).Ni.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqAl = selectedSmelts.ElementAt(0).Al.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqCo = selectedSmelts.ElementAt(0).Co.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqCu = selectedSmelts.ElementAt(0).Cu.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqNb = selectedSmelts.ElementAt(0).Nb.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqTi = selectedSmelts.ElementAt(0).Ti.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqV = selectedSmelts.ElementAt(0).V.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqW = selectedSmelts.ElementAt(0).W.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			eqPb = selectedSmelts.ElementAt(0).Pb.ToString() + "*" + selectedSmelts.ElementAt(0).Weight;
			#endregion

			#region  Dodaj z prawej strony
			gridLayout.Children.Add(new Label { Text = eqFe }, 2, 0);
			gridLayout.Children.Add(new Label { Text = eqC }, 2, 1);
			gridLayout.Children.Add(new Label { Text = eqSi }, 2, 2);
			gridLayout.Children.Add(new Label { Text = eqMn }, 2, 3);
			gridLayout.Children.Add(new Label { Text = eqP }, 2, 4);
			gridLayout.Children.Add(new Label { Text = eqS }, 2, 5);
			gridLayout.Children.Add(new Label { Text = eqCr }, 2, 6);
			gridLayout.Children.Add(new Label { Text = eqMo }, 2, 7);
			gridLayout.Children.Add(new Label { Text = eqNi }, 2, 8);
			gridLayout.Children.Add(new Label { Text = eqAl }, 2, 9);
			gridLayout.Children.Add(new Label { Text = eqCo }, 2, 10);
			gridLayout.Children.Add(new Label { Text = eqCu }, 2, 11);
			gridLayout.Children.Add(new Label { Text = eqNb }, 2, 12);
			gridLayout.Children.Add(new Label { Text = eqTi }, 2, 13);
			gridLayout.Children.Add(new Label { Text = eqV }, 2, 14);
			gridLayout.Children.Add(new Label { Text = eqW }, 2, 15);
			gridLayout.Children.Add(new Label { Text = eqPb }, 2, 16);
			gridLayout.Children.Add(new Label { Text = selectedSmelts.ElementAt(0).Weight.ToString() }, 2, 17);
			#endregion

			Label minimal;
			gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
			if(isMin)
			{
				minimal = new Label { Text = "Minimalizacja <-- " + eqPrice };
			}
			else
			{
				minimal = new Label { Text = "Maksymalizacja <-- " + eqPrice };
			}

			//ostatni rzad na button

			Button btn = new Button { Text = "OBLICZ" };
			btn.Clicked += calculate;

			gridLayout.Children.Add(minimal, 0, 18);
			gridLayout.Children.Add(btn, 0, 19);
			Grid.SetColumnSpan(minimal, 3);
			Grid.SetColumnSpan(btn, 3);

			//grid ktory da sie scrollowac
			var sc = new ScrollView { Content = gridLayout };
			Content = sc;
		}


		private void prepareWeightLayout()
		{
			Title = "Optymalizacja masy";
			BackgroundColor = Color.White;
			var gridLayout = new Grid();
			gridLayout.HorizontalOptions = LayoutOptions.Center;
		}

		public ProcessCalcForm(List<Alloy> selectedAlloys, List<Alloy> selectedSmelts, Boolean isCost, Boolean isMin)
		{
			//definiuj solver i przygotuj dane
			solver = new SimplexSolver();
			this.selectedSmelts = selectedSmelts;
			this.selectedAlloys = selectedAlloys;
			this.isCost = !isCost;
			this.isMin = isMin;

			//Inicjalizacja wygladu stronki
			if(isCost)
				prepareCostLayout();
			else
				prepareWeightLayout();
		}

		private void calculate(object sender, EventArgs e)
		{
			tabVar = new int[selectedAlloys.Count];

			for(int i = 0; i < tabVar.Count(); i++)
			{
				solver.AddVariable("v" + i.ToString(), out tabVar[i]);
				solver.SetBounds(tabVar[i], 0, Rational.PositiveInfinity);
			}

			tabEq = new int[19];

			for(int i = 0; i < tabEq.Count() - 2; i++)
			{
				solver.AddRow("p" + i.ToString(), out tabEq[i]);
				for(int j = 0; j < selectedAlloys.Count; j++)
				{
					solver.SetCoefficient(tabEq[i], tabVar[j], selectedAlloys.ElementAt(j).tabOfElements[i]);
				}
				if(picklist.ElementAt(i).SelectedIndex == 0) //<=
				{
					solver.SetBounds(tabEq[i], 0, selectedSmelts.ElementAt(0).Weight * selectedSmelts.ElementAt(0).tabOfElements[i]);
				}
				else if(picklist.ElementAt(i).SelectedIndex == 1)//=
				{
					solver.SetBounds(tabEq[i], selectedSmelts.ElementAt(0).Weight * selectedSmelts.ElementAt(0).tabOfElements[i], selectedSmelts.ElementAt(0).Weight * selectedSmelts.ElementAt(0).tabOfElements[i]);
				}
				else if(picklist.ElementAt(i).SelectedIndex == 2)//>=
				{
					solver.SetBounds(tabEq[i], selectedSmelts.ElementAt(0).Weight * selectedSmelts.ElementAt(0).tabOfElements[i], Rational.PositiveInfinity);
				}
				else
				{
					DisplayAlert("Error", "zle na: " + i.ToString(), "k");
				}
			}

			//warunek masowy
			solver.AddRow("masowy", out tabEq[17]);
			solver.AddRow("cost", out tabEq[18]);
			for(int i = 0; i < selectedAlloys.Count; i++)
			{
				solver.SetCoefficient(tabEq[17], tabVar[i], 1);
				solver.SetCoefficient(tabEq[18], tabVar[i], selectedAlloys.ElementAt(i).Price);
			}

			if(picklist.ElementAt(picklist.Count - 1).SelectedIndex == 0) //<=
			{
				solver.SetBounds(tabEq[17], 0, selectedSmelts.ElementAt(0).Weight);
			}
			else if(picklist.ElementAt(picklist.Count - 1).SelectedIndex == 1)//=
			{
				solver.SetBounds(tabEq[17], selectedSmelts.ElementAt(0).Weight, selectedSmelts.ElementAt(0).Weight);
			}
			else if(picklist.ElementAt(picklist.Count - 1).SelectedIndex == 2)//>=
			{
				solver.SetBounds(tabEq[17], selectedSmelts.ElementAt(0).Weight, Rational.PositiveInfinity);
			}
			else
			{
				DisplayAlert("Error", "zle na masa", "k");
			}

			solver.AddGoal(tabEq[18], 1, true);
			solver.Solve(new SimplexSolverParams());

			for(int i = 0; i < tabVar.Count(); i++)
			{
				DisplayAlert("Wynik solvera", solver.GetValue(tabVar[i]).ToDouble().ToString(), "ok");
			}
		}
	}
}
