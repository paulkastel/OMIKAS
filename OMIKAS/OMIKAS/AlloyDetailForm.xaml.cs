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
		/// <summary>
		/// Pomocniczy stop o ktorym jest wyswietlana informacja
		/// </summary>
		private Alloy metal;

		/// <summary>
		/// Konstruktor okna pokazujacy szczegoly stopu
		/// </summary>
		/// <param name="metal">Konkretny stop ktory wyswietlamy</param>
		public AlloyDetailForm(Alloy metal)
		{
			InitializeComponent();

			this.metal = metal;

			this.Title = "Szczegóły " + metal.name;
			lblPrice.Text = metal.Price.ToString();

			lblFe.Text = metal.Fe.ToString();
			lblC.Text = metal.C.ToString();
			lblSi.Text = metal.Si.ToString();
			lblMn.Text = metal.Mn.ToString();
			lblP.Text = metal.P.ToString();
			lblS.Text = metal.S.ToString();
			lblCr.Text = metal.Cr.ToString();
			lblMo.Text = metal.Mo.ToString();
			lblNi.Text = metal.Ni.ToString();
			lblAl.Text = metal.Al.ToString();
			lblCo.Text = metal.Co.ToString();
			lblCu.Text = metal.Cu.ToString();
			lblNb.Text = metal.Nb.ToString();
			lblTi.Text = metal.Ti.ToString();
			lblV.Text = metal.V.ToString();
			lblW.Text = metal.W.ToString();
			lblPb.Text = metal.Pb.ToString();
			lblSn.Text = metal.Sn.ToString();
			lblB.Text = metal.B.ToString();
			lblCa.Text = metal.Ca.ToString();
			lblZr.Text = metal.Zr.ToString();
			lblAs.Text = metal.As.ToString();
			lblBi.Text = metal.Bi.ToString();
			lblSb.Text = metal.Sb.ToString();
			lblZn.Text = metal.Zn.ToString();
			lblMg.Text = metal.Mg.ToString();
			lblN.Text = metal.N.ToString();
			lblH.Text = metal.H.ToString();
			lblO.Text = metal.O.ToString();
		}

		/// <summary>
		/// Wywolanie okna edycji dla stopu który jest aktualnie wyświetlany
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AlloyEditForm(metal, App.alloymetals.IndexOf(metal)));
			//Usuniecie strony detail ze stosu a po wcisnieciu przycisku "edycja" nastepuje odrazu powrot do listy
			this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
		}
	}
}
