using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Solvers;
using Xamarin.Forms;


namespace OMIKAS
{
	public partial class ProcessResults : ContentPage
	{
		/// <summary>
		/// Klasa rozwiązań zawierająca wyniki z solvera
		/// </summary>
		public class Solution
		{
			/// <summary>
			/// Nazwa stopu którego dotyczy rozwiązanie
			/// </summary>
			public string name { get; set; }

			/// <summary>
			/// rozwiązanie w postaci liczby
			/// </summary>
			public double solNum { get; set; }

			/// <summary>
			/// rozwiązani w postaci tekstu
			/// </summary>
			public string solTxt { get; set; }

			/// <summary>
			/// Konstruktor
			/// </summary>
			/// <param name="n">nazwa stopu</param>
			/// <param name="num">rozwiązanie w postaci liczby</param>
			/// <param name="txt">rozwiązani w postaci tekstu</param>
			public Solution(string n, double num, string txt)
			{
				this.name = n;
				this.solNum = num;
				this.solTxt = txt;
			}
		}

		/// <summary>
		/// Lista stopów biorąca udział w procesie
		/// </summary>
		private List<Alloy> alloys;

		/// <summary>
		/// Jednoelementowa lista z wytopem
		/// </summary>
		private List<Smelt> smelt;

		/// <summary>
		/// Solver do rozwiązania układu równań
		/// </summary>
		private SimplexSolver solver;

		/// <summary>
		/// Rozpoznanie typu optymalizacji. if true - optymalizacja masowa
		/// </summary>
		public static bool isWeightType;

		/// <summary>
		/// Lista z rozwiązaniami układu równań
		/// </summary>
		public List<Solution> solut;

		/// <summary>
		/// Zmienna do sumowania rozwiązań
		/// </summary>
		private double total;

		/// <summary>
		/// Konstruktor okna z wynikami
		/// </summary>
		/// <param name="solver">Solver do rozwiązania</param>
		/// <param name="alloys">Lista stopów w procesie</param>
		/// <param name="smelt">Jednoelementowa lista wytopów</param>
		/// <param name="isWeightFun">typ optymalizacji</param>
		public ProcessResults(SimplexSolver solver, List<Alloy> alloys, List<Smelt> smelt, bool isWeightFun)
		{
			InitializeComponent();

			//rozwiązanie układu
			solver.Solve(new SimplexSolverParams());

			//inicjalizacja danych
			this.solver = solver;
			this.alloys = alloys;
			this.smelt = smelt;
			isWeightType = isWeightFun;
			total = 0;

			//jezeli typ optymalizacji masowy, odpowiedni komunikat i stworzenie listy z rozwiązaniami
			if(isWeightFun)
			{
				lblintro.Text = "Minimalna ilość składników potrzebnych do wytopienia " + smelt.ElementAt(0).Weight.ToString() + " [g] wytopu " + smelt.ElementAt(0).name.ToString() + " spełniających normy:";
				this.solutionView.ItemsSource= solut = createSolution(alloys, solver);
				foreach(Solution s in solut)
				{
					if(double.IsNaN(s.solNum))
					{
						btn_raport.IsEnabled = false;
						lblTotal.Text = "Brak rozwiązań - niepoprawne dane wejściowe";
					}
					else
					{
						btn_raport.IsEnabled = true;
						//suma wszystkich rozwiązań
						lblTotal.Text = total.ToString("0.00");
					}
				}
			}
			else
			{
				lblintro.Text = "Minimalna ilość składników potrzebnych do wytopienia " + smelt.ElementAt(0).Weight.ToString() + " [g] wytopu " + smelt.ElementAt(0).name.ToString() + " przy możliwie najniższych kosztach:";
				this.solutionView.ItemsSource = solut = createSolution(alloys, solver);
				foreach(Solution s in solut)
				{
					if(double.IsNaN(s.solNum))
					{
						btn_raport.IsEnabled = false;
						lblTotal.Text = "Brak rozwiązań - niepoprawne dane wejściowe";
					}
					else
					{
						btn_raport.IsEnabled = true;
						//suma wszystkich rozwiązań
						lblTotal.Text = total.ToString("0.00");
					}
				}
			}
		}

		/// <summary>
		/// Tworzy listę rozwiązań wraz z nazwami stopów
		/// </summary>
		/// <param name="all">Lista stopów biorąca udział w procesie</param>
		/// <param name="solv">Solver zawierający wyniki</param>
		/// <returns>Lista rozwiązań</returns>
		private List<Solution> createSolution(List<Alloy> all, SimplexSolver solv)
		{
			List<Solution> sol = new List<Solution>();
			for(int i = 0; i < all.Count(); i++)
			{
				//Dla kazdego składnika weź wynik i stworz obiekt rozwiązanie
				double tmp = solver.GetValue(i).ToDouble();
				sol.Add(new Solution(all.ElementAt(i).name, tmp, tmp.ToString("0.00000")));
				total += tmp;
			}
			return sol;
		}

		/// <summary>
		/// Co się dzieje po wciśnięciu rozwiązania
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_raport_Clicked(object sender, EventArgs e)
		{
			//dla każdego systemu rozwiąż sprawę inaczej
			var filesc = DependencyService.Get<ISave>();

			//zadaj pytanie co zrobić a potem to zrób
			var action = await DisplayActionSheet("Co chcesz zrobić?", "Eksport do pliku", "Eksport do pliku i wyślij mail");
			if(action == "Eksport do pliku")
			{
				//tylko zapisz plik
				filesc.saveFileAndNotice(this, alloys, smelt, solut);
			}
			else if(action == "Eksport do pliku i wyślij mail")
			{
				//wyslij maila i zapisz plik
				filesc.saveFile(alloys, smelt, solut);
				filesc.sendEmail();
			}
		}
	}
}
