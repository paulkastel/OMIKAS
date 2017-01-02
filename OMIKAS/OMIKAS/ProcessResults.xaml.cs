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
		public class Solution
		{
			public string name { get; set; }
			public double solNum { get; set; }
			public string solTxt { get; set; }
			public Solution(string n, double num, string txt)
			{
				this.name = n;
				this.solNum = num;
				this.solTxt = txt;
			}
		}

		private List<Alloy> alloys;
		private List<Smelt> smelt;
		private SimplexSolver solver;
		public static bool isWeightType;

		public List<Solution> solut;

		private double total;

		public ProcessResults(SimplexSolver solver, List<Alloy> alloys, List<Smelt> smelt, bool isWeightFun)
		{
			InitializeComponent();
			solver.Solve(new SimplexSolverParams());
			this.solver = solver;
			this.alloys = alloys;
			this.smelt = smelt;
			isWeightType = isWeightFun;
			total = 0;
			string x;
			if(isWeightFun)
			{
				lblintro.Text = "Minimalna ilość składników potrzebnych do wytopienia " + smelt.ElementAt(0).Weight.ToString() + " [g] wytopu " + smelt.ElementAt(0).name.ToString() + " spełniających normy:";
				this.solutionView.ItemsSource = solut = createSolution(alloys, solver, isWeightFun);
				x = solver.GetValue(29).ToDouble().ToString();
			}
			else
			{
				lblintro.Text = "Mimilana ilość składników potrzebnych do wytopienia " + smelt.ElementAt(0).Weight.ToString() + " [g] wytopu " + smelt.ElementAt(0).name.ToString() + " przy możliwie najniższych kosztach:";
				this.solutionView.ItemsSource = solut = createSolution(alloys, solver, isWeightFun);
				x = solver.GetValue(30).ToDouble().ToString();
			}
			lblTotal.Text = total.ToString("0.0000") + " " + x;
		}

		private List<Solution> createSolution(List<Alloy> all, SimplexSolver solv, bool op)
		{
			List<Solution> sol = new List<Solution>();
			if(op)
			{
				for(int i = 0; i < all.Count(); i++)
				{
					double tmp = solver.GetValue(i).ToDouble();
					//tmp = tmp / 100;
					sol.Add(new Solution(all.ElementAt(i).name, tmp, tmp.ToString("0.0000")));
					total += tmp;
				}
			}
			else
			{
				for(int i = 0; i < all.Count(); i++)
				{
					double tmp = solver.GetValue(i).ToDouble();
					sol.Add(new Solution(all.ElementAt(i).name, tmp, tmp.ToString("0.0000")));
					total += tmp;
				}
			}
			return sol;

		}

		private async void btn_raport_Clicked(object sender, EventArgs e)
		{
			var filesc = DependencyService.Get<ISave>();

			var action = await DisplayActionSheet("Co chcesz zrobić?", "Eksport do pliku", "Eksport do pliku i wyślij mail");
			if(action == "Eksport do pliku")
			{
				filesc.saveFileAndNotice(this, alloys, smelt, solut);
			}
			else if(action == "Eksport do pliku i wyślij mail")
			{
				filesc.saveFile(alloys, smelt, solut);
				filesc.sendEmail();
			}
		}
	}
}
