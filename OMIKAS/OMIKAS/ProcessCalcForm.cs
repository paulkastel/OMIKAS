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
		/// if true then optymalizacja masy if true optymalizacja kosztów
		/// </summary>
		private bool isCost;
		/// <summary>
		/// Zmienne równań do solvera
		/// </summary>
		private int[] tabEq;
		/// <summary>
		/// Zmienne do solvera
		/// </summary>
		private int[] tabVar;

		private Grid gridLayout;

		/// <summary>
		/// Konstruktor okna z obliczeniami
		/// </summary>
		/// <param name="selectedAlloys">Lista z składnikami stopowymi</param>
		/// <param name="selectedSmelts">Lista z zawsze tylko jednym wytopem</param>
		/// <param name="isMin">Zmienna dzięki której wiadomo czy mimimalizujemy czy maksymalizujemy</param>
		/// <param name="isCost">Zmienna dzięki której wiadomo z jakim typem optymalizacji mamy do czynienia, false if cost</param>
		public ProcessCalcForm(List<Alloy> selectedAlloys, List<Alloy> selectedSmelts, Boolean isMin, Boolean isCost)
		{
			//definiuj solver i przygotuj dane
			solver = new SimplexSolver();
			this.selectedSmelts = selectedSmelts;
			this.selectedAlloys = selectedAlloys;
			this.isCost = !isCost;
			this.isMin = isMin;

			BackgroundColor = Color.White;
			gridLayout = new Grid();
			gridLayout.HorizontalOptions = LayoutOptions.Center;
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(45) });
			gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

			Button btn = new Button { Text = "OBLICZ" };
			btn.Clicked += calculate;

			//Inicjalizacja wygladu stronki
			if(this.isCost)
			{
				prepareCostLayout();
				gridLayout.Children.Add(btn, 0, 19);
			}
			else
			{
				prepareWeightLayout();
				gridLayout.Children.Add(btn, 0, 18);
			}

			Grid.SetColumnSpan(btn, 3);

			//grid ktory da sie scrollowac
			var sc = new ScrollView { Content = gridLayout };
			Content = sc;
		}

		/// <summary>
		/// Funkcja tworząca layout równań pod optymalizację kosztów
		/// </summary>
		private void prepareCostLayout()
		{
			Title = "Optymalizacja kosztów";

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

			gridLayout.Children.Add(minimal, 0, 18);
			Grid.SetColumnSpan(minimal, 3);
		}

		/// <summary>
		/// Funkcja tworząca layout równań pod optymalizację masy
		/// </summary>
		private void prepareWeightLayout()
		{
			Title = "Optymalizacja masy";

			picklist = new List<Picker>();
			//18: 17 pierwiastkow + 1 rownanie minimalizujace
			for(int i = 0; i < 17; i++)
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
			#endregion

			Label minimal;
			gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
			if(isMin)
			{
				minimal = new Label { Text = "Minimalizacja <-- Ex_j - M" };
			}
			else
			{
				minimal = new Label { Text = "Maksymalizacja <-- Ex_j - M" };
			}

			gridLayout.Children.Add(minimal, 0, 17);
			Grid.SetColumnSpan(minimal, 3);
		}

		/// <summary>
		/// Wciskając przycisk użytkownik rozwiązuje układ równań
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void calculate(object sender, EventArgs e)
		{
			tabVar = new int[selectedAlloys.Count];

			if(isCost)
				tabEq = new int[19]; //z równaniem masowym 
			else
				tabEq = new int[18]; //bez rownania masowego

			//===================W OBU MODELACH TA CZĘŚĆ JEST WSPÓLNA=====================================
			//warunek nieujemności zmiennych
			for(int i = 0; i < tabVar.Count(); i++)
			{
				//tworzymy wszystkie zmienne w ukladzie i dla kazdej xi>=0
				solver.AddVariable("x" + i.ToString(), out tabVar[i]);
				solver.SetBounds(tabVar[i], 0, Rational.PositiveInfinity);
			}

			//w obu funkcjach jest E(Aij*Xij > bi) i=1, 2, ..., n
			for(int i = 0; i < tabEq.Count() - 2; i++)
			{
				//stworz rownanie, wypelnij wspolczynnikami (zmiennymi)
				solver.AddRow("r_p" + i.ToString(), out tabEq[i]);
				for(int j = 0; j < selectedAlloys.Count; j++)
				{
					solver.SetCoefficient(tabEq[i], tabVar[j], selectedAlloys.ElementAt(j).tabOfElements[i]);
				}

				//wybierz operator warunkowy <=, =, >=
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
			//===============================================================================================

			if(isCost) //jezeli wybrano funkcje kosztow=========================================
			{
				solver.AddRow("weight_condition", out tabEq[17]);//warunek masowy
				solver.AddRow("cost", out tabEq[18]);//funkcja celu
				for(int i = 0; i < selectedAlloys.Count; i++)
				{
					solver.SetCoefficient(tabEq[17], tabVar[i], 1); //wypełnienie równania masowego 1x1, 1x2, 1x3, 1x4...
					solver.SetCoefficient(tabEq[18], tabVar[i], selectedAlloys.ElementAt(i).Price);
				}

				if(picklist.ElementAt(picklist.Count - 1).SelectedIndex == 0) //<= do war masowego
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

				solver.AddGoal(tabEq[18], 1, isMin);
			}
			else //jezeli wybrano funkcje masowa=================================
			{
				int M; //M = masa wytopu
				solver.AddVariable("M", out M);
				solver.SetBounds(M, selectedSmelts.ElementAt(0).Weight, selectedSmelts.ElementAt(0).Weight);

				//funkcja celu min/max -> x1, x2, x3, - M
				solver.AddRow("weight", out tabEq[17]);
				for(int i = 0; i < selectedAlloys.Count; i++)
				{
					solver.SetCoefficient(tabEq[17], tabVar[i], 1);
				} //Exj - 1M
				solver.SetCoefficient(tabEq[17], M, -1);

				solver.AddGoal(tabEq[17], 1, isMin);
			}

			//rozwiaz rownanie
			solver.Solve(new SimplexSolverParams());

			//pokaż wyniki w okienku
			for(int i = 0; i < tabVar.Count(); i++)
			{
				DisplayAlert("Wynik solvera", solver.GetValue(tabVar[i]).ToDouble().ToString(), "ok");
			}
		}
	}
}
