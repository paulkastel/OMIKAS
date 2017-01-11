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
		/// <summary>
		/// if true then add, else edit. Używane w momencie wciśnięcia przycisku "Dodaj"
		/// </summary>
		private bool isEdited;

		/// <summary>
		/// indeks stopu na liscie stopow
		/// </summary>
		private int indeksOfAlloy;

		/// <summary>
		/// Pomocniczy stop ktory jest edytowany
		/// </summary>
		private Alloy al;

		/// <summary>
		/// Konstruktor okna z formularzem dodającym
		/// </summary>
		public AlloyEditForm()
		{
			InitializeComponent();
			isEdited = false;
			this.btn_action.Text = this.Title = "Dodaj składnik stopowy";
		}

		/// <summary>
		/// Konstruktor okna z formularzem edytujacym stop
		/// </summary>
		/// <param name="metal">Konkretny stop ktory jest edytowany</param>
		/// <param name="indeks">Jego indeks na liscie</param>
		public AlloyEditForm(Alloy metal, int indeks)
		{
			InitializeComponent();
			isEdited = true;

			al = metal;
			this.indeksOfAlloy = indeks;
			this.btn_action.Text = "Edytuj stop";
			this.Title = "Edytuj \"" + metal.name + "\"";

			//W inputy wstaw nazwę i cenę
			entName.Text = metal.name;
			entPrice.Text = metal.Price.ToString();

			//W inputy wstaw zawartości chemiczne stopu
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
			entSn.Text = metal.Sn.ToString();
			entB.Text = metal.B.ToString();
			entCa.Text = metal.Ca.ToString();
			entZr.Text = metal.Zr.ToString();
			entAs.Text = metal.As.ToString();
			entBi.Text = metal.Bi.ToString();
			entSb.Text = metal.Sb.ToString();
			entZn.Text = metal.Zn.ToString();
			entMg.Text = metal.Mg.ToString();
			entN.Text = metal.N.ToString();
			entH.Text = metal.H.ToString();
			entO.Text = metal.O.ToString();
		}

		/// <summary>
		/// Funkcja edytujaca lub dodajacy nowy produkt (po wcisnieciu btn) w zaleznosci od tego jaki jest aktualnie ekran wyswietlany
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_action_Clicked(object sender, EventArgs e)
		{
			//Czy nazwa jest pusta
			if(!string.IsNullOrWhiteSpace(entName.Text))
			{
				if(isEdited)
				{
					App.DAUtil.DeleteAlloy(al); //to usun z listy stopow
				}

				Alloy met = new Alloy();
				//przekazanie wartosci z pol do nowego metalu który
				met = Alloy.addNewAlloy(this, entName.Text, entPrice.Text, entFe.Text, entC.Text, entSi.Text, entMn.Text, entP.Text, entS.Text, entCr.Text, entMo.Text, entNi.Text, entAl.Text, entCo.Text, entCu.Text, entNb.Text, entTi.Text, entV.Text, entW.Text, entPb.Text, entSn.Text, entB.Text, entCa.Text, entZr.Text, entAs.Text, entBi.Text, entSb.Text, entZn.Text, entMg.Text, entN.Text, entH.Text, entO.Text);
				//Dodaj do bazy
				App.DAUtil.SaveAlloy(met);
				await Navigation.PopAsync();
			}
			else
				await DisplayAlert("Error", "Nie można stworzyć stopu bez nazwy!", "OK");
		}
	}
}
