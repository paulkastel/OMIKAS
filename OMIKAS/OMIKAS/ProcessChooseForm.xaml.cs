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
		/// <summary>
		/// Solver rozwiązujący układ równań
		/// </summary>
		private SimplexSolver solver;

		/// <summary>
		/// Konstruktor okna
		/// </summary>
		public ProcessChooseForm()
		{
			InitializeComponent();
			selectedAlloys = new List<Alloy>();
			selectedSmelts = new List<Smelt>();
		}

		/// <summary>
		/// strona do wyboru stopow
		/// </summary>
		private SelectMultipleBasePage<Alloy> multiPageAlloys;

		/// <summary>
		/// strona do wyboru wytopow
		/// </summary>
		private SelectMultipleBasePage<Smelt> multiPageSmelts;

		/// <summary>
		/// Lista wybranych stopów do procesu
		/// </summary>
		private List<Alloy> selectedAlloys;

		/// <summary>
		/// Jednoelementowa lista z wybranym wytopem
		/// </summary>
		private List<Smelt> selectedSmelts;

		/// <summary>
		/// Funkcja ładuje stronę z wszystkimi stopami
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_chooseAlloy_Clicked(object sender, EventArgs e)
		{
			//Wczytaj do strony liste stopów i ją wyświetl
			if(multiPageAlloys == null)
				multiPageAlloys = new SelectMultipleBasePage<Alloy>(App.DAUtil.GetAllAlloys()) { Title = "Wybór stopów" };

			await Navigation.PushAsync(multiPageAlloys);
		}

		/// <summary>
		/// Funkcja ładuje stronę z wszystkimi wytopami
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_chooseSmelt_Clicked(object sender, EventArgs e)
		{
			//Wczytaj do strony liste wytopów i ją wyświetl
			if(multiPageSmelts == null)
				multiPageSmelts = new SelectMultipleBasePage<Smelt>(App.DAUtil.GetAllSmelts()) { Title = "Wybór wytopu" };

			await Navigation.PushAsync(multiPageSmelts);
		}

		/// <summary>
		/// Wyświetla nazwy wszystkich wybranych stopów na etykiecie
		/// </summary>
		/// <param name="page">Strona z której zostały wybrane stopy</param>
		/// <param name="lbl">Etykieta na której ma się pojawić tekst</param>
		/// <returns>lista stopów</returns>
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

		/// <summary>
		/// Wyświetla nazwy wszystkich wybranych wytopów na etykiecie
		/// </summary>
		/// <param name="page">Strona z której zostały wybrane wytopy</param>
		/// <param name="lbl">Etykieta na której ma się pojawić tekst</param>
		/// <returns>lista wytopów</returns>
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
			//Jezeli cos jest wybrane w obu listach odblokuj przycisk obliczeń
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

		/// <summary>
		/// Akcja wykonywana po wcisnieciu przycisku. Sprawdzenie poprawnosci danych i uruchomienie solvera
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_count_Clicked(object sender, EventArgs e)
		{
			if(!(selectedSmelts.Count > 1))
			{
				double num = 0;
				//Spróbuj przeparsować dane
				if(Double.TryParse(entWeight.Text, out num))
				{
					//Inicjalizuj solver i do wytopu wprowadź masę
					this.solver = new SimplexSolver();
					selectedSmelts.ElementAt(0).Weight = double.Parse(entWeight.Text);

					//Do uruchomienia solera potrzebnne są tablice z pierwiastkami (zmniejszenie ilości kodu i poprawienie czytelności)
					foreach(Alloy xd in selectedAlloys)
					{
						xd.createTabOfElements(xd);
					}

					foreach(Smelt xd in selectedSmelts)
					{
						xd.createTabofEvoporation(xd);
						xd.createTabofMaxNorm(xd);
						xd.createTabofMinNorm(xd);
					}

					//Wybór otymalizacji kosztowej lub masowej
					if(SwitchPriceWeight.IsToggled)
					{
						//optymalizuj mase
						calculate_weight(selectedAlloys, selectedSmelts);
					}
					else
					{
						//optymalizuj koszt
						calculate_price(selectedAlloys, selectedSmelts);
					}
				}
				//Pokaż errory jak coś źle
				else
					await DisplayAlert("Error", "Popraw masę wytopu", "OK");
			}
			else
				await DisplayAlert("Error", "Ilość wybranych wytopów musi wynosić 1", "OK");
		}

		/// <summary>
		/// Powrót do poprzedniego ekranu (menu główne)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btntool_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Funkcja z pomocą której następuje uruchomienie solvera i wykonanie obliczeń optymalizujących cenę
		/// </summary>
		/// <param name="alloys">Lista stopów biorących udział w procesie wytapiania</param>
		/// <param name="smelt">Wytop który chcemy uzyskać</param>
		private async void calculate_price(List<Alloy> alloys, List<Smelt> smelt)
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
						solver.SetCoefficient(tabEq[i], tabVar[j], selectedAlloys.ElementAt(j).tabOfElements[i] * (1 - smelt.ElementAt(0).evoporation[i] / 100));
					}
					solver.SetBounds(tabEq[i], smelt.ElementAt(0).min_Norm[i] * smelt.ElementAt(0).Weight, smelt.ElementAt(0).max_Norm[i] * smelt.ElementAt(0).Weight);
				}

				solver.AddRow("weightCond", out tabEq[29]); //war masowy
				solver.AddRow("cost", out tabEq[30]); //f celu

				for(int i = 0; i < alloys.Count; i++)
				{
					solver.SetCoefficient(tabEq[29], tabVar[i], 1);
					if(alloys.ElementAt(i).Price == 0)
					{
						solver.SetCoefficient(tabEq[30], tabVar[i], 1);
					}
					else
						solver.SetCoefficient(tabEq[30], tabVar[i], alloys.ElementAt(i).Price);
				}
				solver.SetBounds(tabEq[29], smelt.ElementAt(0).Weight, smelt.ElementAt(0).Weight);
				solver.AddGoal(tabEq[30], 1, true);

				//po obliczeniach pokaż ekran z wynikami
				await Navigation.PushAsync(new ProcessResults(solver, alloys, smelt, SwitchPriceWeight.IsToggled));
			}
			catch(Exception ex)
			{
				await DisplayAlert("Error", "Cos w solverze nie tak:\n" + ex.ToString(), "OK");
			}
		}

		/// <summary>
		/// Funkcja z pomocą której następuje uruchomienie solvera i wykonanie obliczeń optymalizujących mase
		/// </summary>
		/// <param name="alloys">Lista stopów biorących udział w procesie wytapiania</param>
		/// <param name="smelt">Wytop który chcemy uzyskać</param>
		private async void calculate_weight(List<Alloy> alloys, List<Smelt> smelt)
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
						solver.SetCoefficient(tabEq[i], tabVar[j], selectedAlloys.ElementAt(j).tabOfElements[i] * (1 - smelt.ElementAt(0).evoporation[i] / 100));
					}
					solver.SetBounds(tabEq[i], smelt.ElementAt(0).min_Norm[i] * smelt.ElementAt(0).Weight, smelt.ElementAt(0).max_Norm[i] * smelt.ElementAt(0).Weight);
				}

				solver.AddRow("weightCond", out tabEq[29]); //war masowy
				solver.AddRow("Weight", out tabEq[30]); //f celu

				for(int i = 0; i < alloys.Count; i++)
				{
					solver.SetCoefficient(tabEq[29], tabVar[i], 1);
					solver.SetCoefficient(tabEq[30], tabVar[i], 1);
				}

				double tmpW = -1 * smelt.ElementAt(0).Weight;
				int m;
				
				solver.AddVariable("m", out m);
				solver.SetBounds(m, smelt.ElementAt(0).Weight, smelt.ElementAt(0).Weight);
				solver.SetCoefficient(tabEq[30], m , tmpW);
				solver.SetBounds(tabEq[29], smelt.ElementAt(0).Weight, smelt.ElementAt(0).Weight);
				solver.AddGoal(tabEq[30], 1, true);

				//po obliczeniach pokaż ekran z wynikami
				await Navigation.PushAsync(new ProcessResults(solver, alloys, smelt, SwitchPriceWeight.IsToggled));
			}
			catch(Exception ex)
			{
				await DisplayAlert("ERROR", ex.ToString(), "Ok");
			}
		}
	}
}
