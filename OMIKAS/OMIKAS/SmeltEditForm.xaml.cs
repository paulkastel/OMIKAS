using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class SmeltEditForm : ContentPage
	{
		/// <summary>
		/// if true then add, else edit Używane w momencie wciśnięcia przycisku "Dodaj"
		/// </summary>
		private bool isEdited;

		/// <summary>
		/// indeks wytopu na liscie wytopow
		/// </summary>
		private int indeksOfSmelt;

		/// <summary>
		/// Pomocniczy wytop ktory jest edytowany
		/// </summary>
		private Smelt sml;

		/// <summary>
		/// Konstruktor okna z formularzem dodającym
		/// </summary>
		public SmeltEditForm()
		{
			InitializeComponent();
			isEdited = false;
			this.btn_action.Text = this.Title = "Dodaj wytop";
		}

		/// <summary>
		/// Konstruktor okna z formularzem edytujacym wytop
		/// </summary>
		/// <param name="metal">wytop ktory jest edytowany</param>
		/// <param name="indeks">Jego indeks na liscie</param>
		public SmeltEditForm(Smelt metal, int indeks)
		{
			InitializeComponent();
			isEdited = true;

			sml = metal;

			this.indeksOfSmelt = indeks;
			this.btn_action.Text = "Edytuj wytop";
			this.Title = "Edytuj \"" + metal.name + "\"";

			this.entName.Text = metal.name;

			//w inputach normy minimalne
			#region minWypelnij
			Fe_min.Text = metal.Fe_min.ToString();
			C_min.Text = metal.C_min.ToString();
			Si_min.Text = metal.Si_min.ToString();
			Mn_min.Text = metal.Mn_min.ToString();
			P_min.Text = metal.P_min.ToString();
			S_min.Text = metal.S_min.ToString();
			Cr_min.Text = metal.Cr_min.ToString();
			Mo_min.Text = metal.Mo_min.ToString();
			Ni_min.Text = metal.Ni_min.ToString();
			Al_min.Text = metal.Al_min.ToString();
			Co_min.Text = metal.Co_min.ToString();
			Cu_min.Text = metal.Cu_min.ToString();
			Nb_min.Text = metal.Nb_min.ToString();
			Ti_min.Text = metal.Ti_min.ToString();
			V_min.Text = metal.V_min.ToString();
			W_min.Text = metal.W_min.ToString();
			Pb_min.Text = metal.Pb_min.ToString();
			Sn_min.Text = metal.Sn_min.ToString();
			B_min.Text = metal.B_min.ToString();
			Ca_min.Text = metal.Ca_min.ToString();
			Zr_min.Text = metal.Zr_min.ToString();
			As_min.Text = metal.As_min.ToString();
			Bi_min.Text = metal.Bi_min.ToString();
			Sb_min.Text = metal.Sb_min.ToString();
			Zn_min.Text = metal.Zn_min.ToString();
			Mg_min.Text = metal.Mg_min.ToString();
			N_min.Text = metal.N_min.ToString();
			H_min.Text = metal.H_min.ToString();
			O_min.Text = metal.O_min.ToString();
			#endregion

			//w inputach normy max
			#region maxWypelnij
			Fe_max.Text = metal.Fe_max.ToString();
			C_max.Text = metal.C_max.ToString();
			Si_max.Text = metal.Si_max.ToString();
			Mn_max.Text = metal.Mn_max.ToString();
			P_max.Text = metal.P_max.ToString();
			S_max.Text = metal.S_max.ToString();
			Cr_max.Text = metal.Cr_max.ToString();
			Mo_max.Text = metal.Mo_max.ToString();
			Ni_max.Text = metal.Ni_max.ToString();
			Al_max.Text = metal.Al_max.ToString();
			Co_max.Text = metal.Co_max.ToString();
			Cu_max.Text = metal.Cu_max.ToString();
			Nb_max.Text = metal.Nb_max.ToString();
			Ti_max.Text = metal.Ti_max.ToString();
			V_max.Text = metal.V_max.ToString();
			W_max.Text = metal.W_max.ToString();
			Pb_max.Text = metal.Pb_max.ToString();
			Sn_max.Text = metal.Sn_max.ToString();
			B_max.Text = metal.B_max.ToString();
			Ca_max.Text = metal.Ca_max.ToString();
			Zr_max.Text = metal.Zr_max.ToString();
			As_max.Text = metal.As_max.ToString();
			Bi_max.Text = metal.Bi_max.ToString();
			Sb_max.Text = metal.Sb_max.ToString();
			Zn_max.Text = metal.Zn_max.ToString();
			Mg_max.Text = metal.Mg_max.ToString();
			N_max.Text = metal.N_max.ToString();
			H_max.Text = metal.H_max.ToString();
			O_max.Text = metal.O_max.ToString();
			#endregion

			//w inputach wspolcznniki parowania
			#region evoWypelnij
			Fe_evo.Text = metal.Fe_evo.ToString();
			C_evo.Text = metal.C_evo.ToString();
			Si_evo.Text = metal.Si_evo.ToString();
			Mn_evo.Text = metal.Mn_evo.ToString();
			P_evo.Text = metal.P_evo.ToString();
			S_evo.Text = metal.S_evo.ToString();
			Cr_evo.Text = metal.Cr_evo.ToString();
			Mo_evo.Text = metal.Mo_evo.ToString();
			Ni_evo.Text = metal.Ni_evo.ToString();
			Al_evo.Text = metal.Al_evo.ToString();
			Co_evo.Text = metal.Co_evo.ToString();
			Cu_evo.Text = metal.Cu_evo.ToString();
			Nb_evo.Text = metal.Nb_evo.ToString();
			Ti_evo.Text = metal.Ti_evo.ToString();
			V_evo.Text = metal.V_evo.ToString();
			W_evo.Text = metal.W_evo.ToString();
			Pb_evo.Text = metal.Pb_evo.ToString();
			Sn_evo.Text = metal.Sn_evo.ToString();
			B_evo.Text = metal.B_evo.ToString();
			Ca_evo.Text = metal.Ca_evo.ToString();
			Zr_evo.Text = metal.Zr_evo.ToString();
			As_evo.Text = metal.As_evo.ToString();
			Bi_evo.Text = metal.Bi_evo.ToString();
			Sb_evo.Text = metal.Sb_evo.ToString();
			Zn_evo.Text = metal.Zn_evo.ToString();
			Mg_evo.Text = metal.Mg_evo.ToString();
			N_evo.Text = metal.N_evo.ToString();
			H_evo.Text = metal.H_evo.ToString();
			O_evo.Text = metal.O_evo.ToString();
			#endregion

		}

		/// <summary>
		/// Funkcja edytujaca lub dodajaca nowy wytop po wcisnieciu przycisku
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_action_Clicked(object sender, EventArgs e)
		{
			//nie mozna zapisac wytopu bez nazwy
			if(!string.IsNullOrWhiteSpace(entName.Text))
			{
				if(isEdited)
				{
					//jak edytuje to
					App.DAUtil.DeleteSmelt(sml);    //usuwam
				}
				Smelt smel = new Smelt();       //tworze nowy

				smel = Smelt.addNewSmelt(this, entName.Text,
				Fe_min.Text, Fe_max.Text, Fe_evo.Text,
				C_min.Text, C_max.Text, C_evo.Text,
				Si_min.Text, Si_max.Text, Si_evo.Text,
				Mn_min.Text, Mn_max.Text, Mn_evo.Text,
				P_min.Text, P_max.Text, P_evo.Text,
				S_min.Text, S_max.Text, S_evo.Text,
				Cr_min.Text, Cr_max.Text, Cr_evo.Text,
				Mo_min.Text, Mo_max.Text, Mo_evo.Text,
				Ni_min.Text, Ni_max.Text, Ni_evo.Text,
				Al_min.Text, Al_max.Text, Al_evo.Text,
				Co_min.Text, Co_max.Text, Co_evo.Text,
				Cu_min.Text, Cu_max.Text, Cu_evo.Text,
				Nb_min.Text, Nb_max.Text, Nb_evo.Text,
				Ti_min.Text, Ti_max.Text, Ti_evo.Text,
				V_min.Text, V_max.Text, V_evo.Text,
				W_min.Text, W_max.Text, W_evo.Text,
				Pb_min.Text, Pb_max.Text, Pb_evo.Text,
				Sn_min.Text, Sn_max.Text, Sn_evo.Text,
				B_min.Text, B_max.Text, B_evo.Text,
				Ca_min.Text, Ca_max.Text, Ca_evo.Text,
				Zr_min.Text, Zr_max.Text, Zr_evo.Text,
				As_min.Text, As_max.Text, As_evo.Text,
				Bi_min.Text, Bi_max.Text, Bi_evo.Text,
				Sb_min.Text, Sb_max.Text, Sb_evo.Text,
				Zn_min.Text, Zn_max.Text, Zn_evo.Text,
				Mg_min.Text, Mg_max.Text, Mg_evo.Text,
				N_min.Text, N_max.Text, N_evo.Text,
				H_min.Text, H_max.Text, H_evo.Text,
				O_min.Text, O_max.Text, O_evo.Text);

				App.DAUtil.SaveSmelt(smel); //zapisuje do bazy
				await Navigation.PopAsync();
			}
			else
				await DisplayAlert("Error", "Nie można stworzyć wytopu bez nazwy!", "OK");
		}
	}
}
