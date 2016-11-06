using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class AlloyEditForm : ContentPage
	{
		//if true then add, else edit
		private bool whataction;
		private int indeksOfAlloy;

		public AlloyEditForm(string operation)
		{
			InitializeComponent();
			whataction = true;
			this.btn_action.Text = this.Title = "Dodaj stop";
		}

		public AlloyEditForm(Alloy metal, int indeks)
		{
			InitializeComponent();
			whataction = false;
			this.btn_action.Text = this.Title = "Edytuj stop";
			this.indeksOfAlloy = indeks;

			entName.Text = metal.nameAlloy;
			entFe.Text = metal.Fe.ToString();
			entC.Text = metal.C.ToString();
			entSi.Text = metal.Si.ToString();
			entMn.Text = metal.Mn.ToString();
			entP.Text = metal.P.ToString();
			entS.Text = metal.S.ToString();
			entCr.Text = metal.Cr.ToString();
			entMo.Text = metal.Mo.ToString();
			entNi.Text = metal.Ni.ToString();
			entAl.Text = metal.Al.ToString();
			entCo.Text = metal.Co.ToString();
			entCu.Text = metal.Cu.ToString();
			entNb.Text = metal.Nb.ToString();
			entTi.Text = metal.Ti.ToString();
			entV.Text = metal.V.ToString();
			entW.Text = metal.W.ToString();
			entPb.Text = metal.Pb.ToString();
		}

		private async void btn_action_Clicked(object sender, EventArgs e)
		{
			if(!whataction)
			{
				App.alloymetals.RemoveAt(indeksOfAlloy);
			}

			Alloy met = new Alloy();
			if(!string.IsNullOrWhiteSpace(entName.Text))
			{
				met = Alloy.addNewAlloy(this, entName.Text, entFe.Text, entC.Text, entSi.Text, entMn.Text, entP.Text, entS.Text, entCr.Text, entMo.Text, entNi.Text, entAl.Text, entCo.Text, entCu.Text, entNb.Text, entTi.Text, entV.Text, entW.Text, entPb.Text);
				App.alloymetals.Add(met);
			}
			await Navigation.PopAsync();
		}
	}
}
