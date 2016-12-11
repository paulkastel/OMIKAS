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
		/// Okno pokazujace szczegoly produktu
		/// </summary>
		/// <param name="metal">Konkretny stop/wytop ktory wyswietlamy</param>
		public AlloyDetailForm(Alloy metal)
		{
			InitializeComponent();
			//TODO: WYŚWIETLANIE WSZYSTKICH DANYCH
			this.Title = "Szczegóły " + metal.name;
			lblFe.Text = metal.Fe.ToString();
			lblC.Text = metal.C.ToString();
			lblSi.Text = metal.Si.ToString();
			lblMn.Text = metal.Mn.ToString();
			lblP.Text = metal.P.ToString();
		}
	}
}
