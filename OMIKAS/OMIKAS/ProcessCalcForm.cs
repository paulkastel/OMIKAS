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
		public ProcessCalcForm(List<Alloy> selectedAlloys, List<Alloy> selectedSmelts, Boolean isCost, Boolean isMin)
		{
			//definiuj solver
			solver = new SimplexSolver();

			//Inicjalizacja wygladu stronki
			Title = "Równania";
			BackgroundColor = Color.White;
			var gridLayout = new Grid();
			gridLayout.HorizontalOptions = LayoutOptions.Center;

			//Trzy kolumny na rownania lewa strona, operator, prawa strona
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

			int[] tabEq = new int[19];
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

				//Ostatnie równannie to warunek masowy
				if(i == 17)
					solver.AddRow("war masowy" + i.ToString(), out tabEq[i]);
				else
					solver.AddRow("eq" + i.ToString(), out tabEq[i]);

				//w kazdym rzedzie na srodku dodaj picker
				gridLayout.Children.Add(picklist.ElementAt(i), 1, i);
			}
			solver.AddRow("koszt", out tabEq[18]);

			int[] tabVar = new int[selectedAlloys.Count];
			int counter = 0; //licznik

			#region definicja stringow rownan
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
			#endregion

			//Lewa strona
			foreach(Alloy met in selectedAlloys)
			{
				#region Utworz stringi rownan
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
				#endregion

				//Każdy z stopow to zmienna w rownaniu ktora jest nieujemna
				solver.AddVariable(met.nameAlloy, out tabVar[counter]);
				solver.SetBounds(tabVar[counter], 0, Rational.PositiveInfinity);
				counter++;
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

			//ostatni rzad na button
			gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
			Label minimal = new Label { Text = "Minimalizacja <-- " + eqPrice };
			Button btn = new Button { Text = "DO IT" };
			btn.Clicked += calculate;

			gridLayout.Children.Add(minimal, 0, 18);
			gridLayout.Children.Add(btn, 0, 19);
			Grid.SetColumnSpan(minimal, 3);
			Grid.SetColumnSpan(btn, 3);

			//grid ktory da sie scrollowac
			var sc = new ScrollView { Content = gridLayout };
			Content = sc;


			//TODO here
			for(int i = 0; i < tabEq.Count(); i++)
			{
				for(int j = 0; i < tabVar.Count(); j++)
				{
					//solver.SetCoefficient(tabEq[i], tabVar[j], selectedAlloys.ElementAt
				}
			}


		}

		private void calculate(object sender, EventArgs e)
		{

		}
	}
}
