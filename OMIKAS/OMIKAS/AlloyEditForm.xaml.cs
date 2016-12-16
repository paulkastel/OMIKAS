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
		private bool isEdited;
		//indeks stopu na liscie stopow/wytopow
		private int indeksOfAlloy;
		private Alloy al;
		public AlloyEditForm()
		{
			InitializeComponent();
			isEdited = false;
			this.btn_action.Text = this.Title = "Dodaj stop";
		}

		public AlloyEditForm(Alloy metal, int indeks)
		{
			InitializeComponent();
			isEdited = true;

			al = metal;
			this.indeksOfAlloy = indeks;
			this.btn_action.Text = this.Title = "Edytuj stop";

			entName.Text = metal.name;
			entPrice.Text = metal.Price.ToString();

			//TODO: W stopach masa jest nie potrzebna, albo usunac albo zrobic jakas funkcje zliczajaca ilosc stopow w magazynie.
			entWeight.Text = metal.Weight.ToString();


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
		/// Przycisk edytujacy lub dodajacy nowy produkt w zaleznosci od tego jaki jest aktualnie ekran wyswietlany
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_action_Clicked(object sender, EventArgs e)
		{
			//TODO: PRZYCISK KTORY DODAJAC NIE POWRACA DO POPRZEDNIEGO EKRANU TYLKO ODPALA SZYBKIE DOAWANIE KOLEJNEGO STOPU/WYTOPU
			//TODO: SORTOWANIE ALFABETYCZNE CALEJ LISTY PO NAZWACH

			//Jezeli to ekran edycji i operujemy na liscie skladnikow
			if(isEdited)
			{
				//App.alloymetals.RemoveAt(indeksOfAlloy); //to usun z listy stopow
				App.DAUtil.DeleteAlloy(al);
			}

			//i edytowane zawartosci z pol zapisz do odpowiedniej listy
			Alloy met = new Alloy();
			if(!string.IsNullOrWhiteSpace(entName.Text))
			{
				//przekazanie wartosci z pol do nowego metalu który
				met = Alloy.addNewAlloy(this, entName.Text, entPrice.Text, entWeight.Text, entFe.Text, entC.Text, entSi.Text, entMn.Text, entP.Text, entS.Text, entCr.Text, entMo.Text, entNi.Text, entAl.Text, entCo.Text, entCu.Text, entNb.Text, entTi.Text, entV.Text, entW.Text, entPb.Text, entSn.Text, entB.Text, entCa.Text, entZr.Text, entAs.Text, entBi.Text, entSb.Text, entZn.Text, entMg.Text, entN.Text, entH.Text, entO.Text);
				App.DAUtil.SaveAlloy(met);
				//App.alloymetals.Add(met); //dodamy do listy skladnikow
			}
			await Navigation.PopAsync();
		}
	}
}
