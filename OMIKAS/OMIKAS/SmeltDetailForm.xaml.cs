using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class SmeltDetailForm : ContentPage
	{
		public SmeltDetailForm(Smelt smelt)
		{
			InitializeComponent();
			//TODO: WYŚWIETLANIE WSZYSTKICH DANYCH
			this.Title = "Szczegóły " + smelt.name;
			Fe_min.Text = smelt.Fe_min.ToString();
			C_min.Text = smelt.C_min.ToString();
			Si_min.Text = smelt.Si_min.ToString();
			Mn_min.Text = smelt.Mn_min.ToString();
			P_min.Text = smelt.P_min.ToString();

			Fe_max.Text = smelt.Fe_max.ToString();
			C_max.Text = smelt.C_max.ToString();
			Si_max.Text = smelt.Si_max.ToString();
			Mn_max.Text = smelt.Mn_max.ToString();
			P_max.Text = smelt.P_max.ToString();

			Fe_evo.Text = smelt.Fe_evo.ToString();
			C_evo.Text = smelt.C_evo.ToString();
			Si_evo.Text = smelt.Si_evo.ToString();
			Mn_evo.Text = smelt.Mn_evo.ToString();
			P_evo.Text = smelt.P_evo.ToString();
		}
	}
}
