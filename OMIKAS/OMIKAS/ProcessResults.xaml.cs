using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OMIKAS
{
	public partial class ProcessResults : ContentPage
	{
		private List<Alloy> alloys;
		private List<Smelt> smelt;

		public ProcessResults(List<Alloy> alloys, List<Smelt> smelt)
		{
			InitializeComponent();
			this.alloys = alloys;
			this.smelt = smelt;

			lbl1.Text = alloys.ElementAt(0).tabOfElements[0].ToString();
			lbl2.Text = alloys.ElementAt(0).Fe.ToString();
			lbl3.Text = smelt.ElementAt(0).max_Norm[0].ToString();
			lbl4.Text = smelt.ElementAt(0).min_Norm[0].ToString();
			lbl5.Text = smelt.ElementAt(0).evoporation[1].ToString();

		}
	}
}
