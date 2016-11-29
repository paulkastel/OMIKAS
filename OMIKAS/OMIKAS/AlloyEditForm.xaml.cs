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
		//if true then alloys, else smelts
		private bool itAlloyList;
		//indeks stopu na liscie stopow/wytopow
		private int indeksOfAlloy;

		public AlloyEditForm(string nameOperation, bool whichList)
		{
			InitializeComponent();
			isEdited = false;
			itAlloyList = whichList;

			if(itAlloyList)
			{
				lbl_name.Text = "NAZWA STOPU:";
				this.btn_action.Text = this.Title = "Dodaj stop";
			}
			else
			{
				lbl_name.Text = "NAZWA WYTOPU: ";
				this.btn_action.Text = this.Title = "Dodaj wytop";

				/*this.entFe.IsEnabled = false;
				this.entC.IsEnabled = false;
				this.entSi.IsEnabled = false;
				this.entMn.IsEnabled = false;
				this.entP.IsEnabled = false;
				this.entS.IsEnabled = false;
				this.entCr.IsEnabled = false;
				this.entMo.IsEnabled = false;
				this.entNi.IsEnabled = false;
				this.entAl.IsEnabled = false;
				this.entCo.IsEnabled = false;
				this.entCu.IsEnabled = false;
				this.entNb.IsEnabled = false;
				this.entTi.IsEnabled = false;
				this.entV.IsEnabled = false;
				this.entW.IsEnabled = false;
				this.entPb.IsEnabled = false;*/
			}
		}

		public AlloyEditForm(Alloy metal, int indeks, bool whichlist)
		{
			InitializeComponent();
			itAlloyList = whichlist;
			isEdited = true;

			this.indeksOfAlloy = indeks;
			if(itAlloyList)
			{
				this.btn_action.Text = this.Title = "Edytuj stop";
				lbl_name.Text = "NAZWA STOPU:";
			}
			else
			{
				this.btn_action.Text = this.Title = "Edytuj wytop";
				lbl_name.Text = "NAZWA WYTOPU: ";
			}

			entName.Text = metal.nameAlloy;
			entPrice.Text = metal.Price.ToString();
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
			if(isEdited && itAlloyList)
			{
				App.alloymetals.RemoveAt(indeksOfAlloy); //to usun z listy stopow
			}
			//jezeli to ekran edycji i operujemy na liscie wytopow
			if(isEdited && !itAlloyList)
			{
				App.alloysmelts.RemoveAt(indeksOfAlloy); //to usun z listy wytopow
			}

			//i edytowane zawartosci z pol zapisz do odpowiedniej listy
			Alloy met = new Alloy();
			if(!string.IsNullOrWhiteSpace(entName.Text))
			{
				//przekazanie wartosci z pol do nowego metalu który
				met = Alloy.addNewAlloy(this, entName.Text, entPrice.Text, entWeight.Text, entFe.Text, entC.Text, entSi.Text, entMn.Text, entP.Text, entS.Text, entCr.Text, entMo.Text, entNi.Text, entAl.Text, entCo.Text, entCu.Text, entNb.Text, entTi.Text, entV.Text, entW.Text, entPb.Text);

				if(itAlloyList)
				{
					App.alloymetals.Add(met); //dodamy do listy skladnikow
				}
				else
				{
					App.alloysmelts.Add(met); //lub listy listy wytopow
				}
			}
			await Navigation.PopAsync();
		}
	}
}
