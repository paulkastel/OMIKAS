using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class AlloyDetailForm : ContentPage
	{
		public AlloyDetailForm(Alloy metal)
		{
			InitializeComponent();

			this.Title = "Szczegóły " + metal.nameAlloy;
			lblFe.Text = metal.Fe.ToString();
			lblC.Text = metal.Fe.ToString();
			lblSi.Text = metal.Fe.ToString();
			lblMn.Text = metal.Fe.ToString();
			lblP.Text = metal.Fe.ToString();
		}
	}
}
