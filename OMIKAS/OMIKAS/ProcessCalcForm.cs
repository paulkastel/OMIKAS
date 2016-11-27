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

		public ProcessCalcForm(List<Alloy> selectedAlloys, List<Alloy> selectedSmelts, Boolean isCost, Boolean isMin)
		{
			//Inicjalizacja wygladu stronki
			Title = "Równania";
			BackgroundColor = Color.White;
			Grid gridLayout = new Grid();
			gridLayout.HorizontalOptions = LayoutOptions.Center;

			//Trzy kolumny na rownania lewa strona, operator, prawa strona
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

			picklist = new List<Picker>();
			//18 - 17 pierwiastkow + 1 warunek masowy
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

				//w kazdym rzedzia na srodku dodaj picker
				gridLayout.Children.Add(picklist.ElementAt(i), 1, i);

			}

			string eqFe = null;
			string eqC = null;
			string eqSi = null;
			string eqMn = null;
			string eqP = null;
			string eqS = null;
			string eqCr = null;
			string eqMo = null;
			string eqNi = null;
			string eqAl = null;
			string eqCo = null;
			string eqCu = null;
			string eqNb = null;
			string eqTi = null;
			string eqV = null;
			string eqW = null;
			string eqPb = null;
			string eqPrice = null;

			//Lewa strona
			foreach(Alloy met in selectedAlloys)
			{
				eqFe += met.Fe.ToString() + met.nameAlloy + " ";
				eqC += met.C.ToString() + met.nameAlloy + " ";
				eqSi += met.Si.ToString() + met.nameAlloy + " ";
				eqMn += met.Mn.ToString() + met.nameAlloy + " ";
				eqP += met.P.ToString() + met.nameAlloy + " ";
				eqS += met.S.ToString() + met.nameAlloy + " ";
				eqCr += met.Cr.ToString() + met.nameAlloy + " ";
				eqMo += met.Mo.ToString() + met.nameAlloy + " ";
				eqNi += met.Ni.ToString() + met.nameAlloy + " ";
				eqAl += met.Al.ToString() + met.nameAlloy + " ";
				eqCo += met.Co.ToString() + met.nameAlloy + " ";
				eqCu += met.Cu.ToString() + met.nameAlloy + " ";
				eqNb += met.Nb.ToString() + met.nameAlloy + " ";
				eqTi += met.Ti.ToString() + met.nameAlloy + " ";
				eqV += met.V.ToString() + met.nameAlloy + " ";
				eqW += met.W.ToString() + met.nameAlloy + " ";
				eqPb += met.Pb.ToString() + met.nameAlloy + " ";
				eqPrice += met.Price.ToString() + " ";
			}

			//Dodaj z lewej strony
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

			//Prawa strona
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

			//Dodaj z prawej strony
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

			//ostatni rzad na button
			gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
			Label minimal = new Label { Text = "Minimalizacja: " + eqPrice };
			Button btn = new Button { Text = "DO IT" };
			btn.Clicked += calculate;

			gridLayout.Children.Add(minimal, 0, 17);
			gridLayout.Children.Add(btn, 0, 18);
			Grid.SetColumnSpan(minimal, 3);
			Grid.SetColumnSpan(btn, 3);

			Content = gridLayout;

			/*
						//===================================================================================================
						int a, b, c;
						SimplexSolver solver = new SimplexSolver();
						solver.AddVariable(A.nameAlloy, out a);
						solver.SetBounds(a, 0, Rational.PositiveInfinity);
						solver.AddVariable(B.nameAlloy, out b);
						solver.SetBounds(b, 0, Rational.PositiveInfinity);
						solver.AddVariable(C.nameAlloy, out c);
						solver.SetBounds(c, 0, Rational.PositiveInfinity);


						int r1, r2, r3, r4, cost, masa;
						solver.AddRow("r1", out r1);
						solver.AddRow("r2", out r2);
						solver.AddRow("r3", out r3);
						solver.AddRow("r4", out r4);
						solver.AddRow("cost", out cost);
						solver.AddRow("masa", out masa);

						solver.SetCoefficient(r1, a, A.P);
						solver.SetCoefficient(r1, b, B.P);
						solver.SetCoefficient(r1, c, C.P);
						solver.SetBounds(r1, Wytop.P * Wytop.Weight, Rational.PositiveInfinity);

						solver.SetCoefficient(r2, a, A.C);
						solver.SetCoefficient(r2, b, B.C);
						solver.SetCoefficient(r2, c, C.C);
						solver.SetBounds(r2, 0, Wytop.C * Wytop.Weight);

						solver.SetCoefficient(r3, a, A.Si);
						solver.SetCoefficient(r3, b, B.Si);
						solver.SetCoefficient(r3, c, C.Si);
						solver.SetBounds(r3, 0, Wytop.Si * Wytop.Weight);

						solver.SetCoefficient(r4, a, A.Mn);
						solver.SetCoefficient(r4, b, B.Mn);
						solver.SetCoefficient(r4, c, C.Mn);
						solver.SetBounds(r4, Wytop.Mn * Wytop.Weight, Rational.PositiveInfinity);

						solver.SetCoefficient(masa, a, 1);
						solver.SetCoefficient(masa, b, 1);
						solver.SetCoefficient(masa, c, 1);
						solver.SetBounds(masa, Wytop.Weight, Rational.PositiveInfinity);

						solver.SetCoefficient(cost, a, A.Price);
						solver.SetCoefficient(cost, b, B.Price);
						solver.SetCoefficient(cost, c, C.Price);
						solver.AddGoal(cost, 1, true);

						solver.Solve(new SimplexSolverParams());
						DisplayAlert("Wynik solvera", solver.GetValue(a).ToDouble().ToString(), "ok");
						DisplayAlert("Wynik solvera", solver.GetValue(b).ToDouble().ToString(), "ok");
						DisplayAlert("Wynik solvera", solver.GetValue(c).ToDouble().ToString(), "ok");

				*/
		}

		private void calculate(object sender, EventArgs e)
		{

		}
	}
}
