using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Attributes;

namespace OMIKAS
{
	/// <summary>
	/// Wytop
	/// </summary>
	public class Smelt
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		/// <summary>
		/// nazwa wytopu
		/// </summary>
		public string name { get; set; }
		public double Weight { get; set; }

		#region Norma_min
		public double Fe_min { get; private set; }
		public double C_min { get; set; }
		public double Si_min { get; set; }
		public double Mn_min { get; set; }
		public double P_min { get; set; }
		public double S_min { get; set; }
		public double Cr_min { get; set; }
		public double Mo_min { get; set; }
		public double Ni_min { get; set; }
		public double Al_min { get; set; }
		public double Co_min { get; set; }
		public double Cu_min { get; set; }
		public double Nb_min { get; set; }
		public double Ti_min { get; set; }
		public double V_min { get; set; }
		public double W_min { get; set; }
		public double Pb_min { get; set; }

		public double Sn_min { get; set; }
		public double B_min { get; set; }
		public double Ca_min { get; set; }
		public double Zr_min { get; set; }
		public double As_min { get; set; }
		public double Bi_min { get; set; }
		public double Sb_min { get; set; }
		public double Zn_min { get; set; }
		public double Mg_min { get; set; }
		public double N_min { get; set; }
		public double H_min { get; set; }
		public double O_min { get; set; }

		#endregion

		#region Norma_nax
		public double Fe_max { get; private set; }
		public double C_max { get; set; }
		public double Si_max { get; set; }
		public double Mn_max { get; set; }
		public double P_max { get; set; }
		public double S_max { get; set; }
		public double Cr_max { get; set; }
		public double Mo_max { get; set; }
		public double Ni_max { get; set; }
		public double Al_max { get; set; }
		public double Co_max { get; set; }
		public double Cu_max { get; set; }
		public double Nb_max { get; set; }
		public double Ti_max { get; set; }
		public double V_max { get; set; }
		public double W_max { get; set; }
		public double Pb_max { get; set; }

		public double Sn_max { get; set; }
		public double B_max { get; set; }
		public double Ca_max { get; set; }
		public double Zr_max { get; set; }
		public double As_max { get; set; }
		public double Bi_max { get; set; }
		public double Sb_max { get; set; }
		public double Zn_max { get; set; }
		public double Mg_max { get; set; }
		public double N_max { get; set; }
		public double H_max { get; set; }
		public double O_max { get; set; }
		#endregion

		#region wsp_Parowania
		public double Fe_evo { get; private set; }
		public double C_evo { get; set; }
		public double Si_evo { get; set; }
		public double Mn_evo { get; set; }
		public double P_evo { get; set; }
		public double S_evo { get; set; }
		public double Cr_evo { get; set; }
		public double Mo_evo { get; set; }
		public double Ni_evo { get; set; }
		public double Al_evo { get; set; }
		public double Co_evo { get; set; }
		public double Cu_evo { get; set; }
		public double Nb_evo { get; set; }
		public double Ti_evo { get; set; }
		public double V_evo { get; set; }
		public double W_evo { get; set; }
		public double Pb_evo { get; set; }

		public double Sn_evo { get; set; }
		public double B_evo { get; set; }
		public double Ca_evo { get; set; }
		public double Zr_evo { get; set; }
		public double As_evo { get; set; }
		public double Bi_evo { get; set; }
		public double Sb_evo { get; set; }
		public double Zn_evo { get; set; }
		public double Mg_evo { get; set; }
		public double N_evo { get; set; }
		public double H_evo { get; set; }
		public double O_evo { get; set; }
		#endregion

		/// <summary>
		/// Tablica z minimalnymi normami
		/// </summary>
		public double[] min_Norm;

		/// <summary>
		/// Tablica z maksymalnymi normami
		/// </summary>
		public double[] max_Norm;

		/// <summary>
		/// Tablica zawierajaca wsp parowania
		/// </summary>
		public double[] evoporation;

		/// <summary>
		/// Konstruktor wytopu. Inicjalizacja 3 tablic
		/// </summary>
		public Smelt()
		{
			min_Norm = new double[29];
			max_Norm = new double[29];
			evoporation = new double[29];
		}

		/// <summary>
		/// Wypelnia tablice maksimow z danego wytopu
		/// </summary>
		/// <param name="s">wytop z ktorego brane sa dane</param>
		public void createTabofMaxNorm(Smelt s)
		{
			max_Norm[0] = s.Fe_max;
			max_Norm[1] = s.C_max;
			max_Norm[2] = s.Si_max;
			max_Norm[3] = s.Mn_max;
			max_Norm[4] = s.P_max;
			max_Norm[5] = s.S_max;
			max_Norm[6] = s.Cr_max;
			max_Norm[7] = s.Mo_max;
			max_Norm[8] = s.Ni_max;
			max_Norm[9] = s.Al_max;
			max_Norm[10] = s.Co_max;
			max_Norm[11] = s.Cu_max;
			max_Norm[12] = s.Nb_max;
			max_Norm[13] = s.Ti_max;
			max_Norm[14] = s.V_max;
			max_Norm[15] = s.W_max;
			max_Norm[16] = s.Pb_max;

			max_Norm[17] = s.Sn_max;
			max_Norm[18] = s.B_max;
			max_Norm[19] = s.Ca_max;
			max_Norm[20] = s.Zr_max;
			max_Norm[21] = s.As_max;
			max_Norm[22] = s.Bi_max;
			max_Norm[23] = s.Sb_max;
			max_Norm[24] = s.Zn_max;
			max_Norm[25] = s.Mg_max;
			max_Norm[26] = s.N_max;
			max_Norm[27] = s.H_max;
			max_Norm[28] = s.O_max;
		}

		/// <summary>
		/// Wypelnia tablice minimow z danego wytopu
		/// </summary>
		/// <param name="s">Wytop z ktorego brane sa dane</param>
		public void createTabofMinNorm(Smelt s)
		{
			min_Norm[0] = s.Fe_min;
			min_Norm[1] = s.C_min;
			min_Norm[2] = s.Si_min;
			min_Norm[3] = s.Mn_min;
			min_Norm[4] = s.P_min;
			min_Norm[5] = s.S_min;
			min_Norm[6] = s.Cr_min;
			min_Norm[7] = s.Mo_min;
			min_Norm[8] = s.Ni_min;
			min_Norm[9] = s.Al_min;
			min_Norm[10] = s.Co_min;
			min_Norm[11] = s.Cu_min;
			min_Norm[12] = s.Nb_min;
			min_Norm[13] = s.Ti_min;
			min_Norm[14] = s.V_min;
			min_Norm[15] = s.W_min;
			min_Norm[16] = s.Pb_min;

			min_Norm[17] = s.Sn_min;
			min_Norm[18] = s.B_min;
			min_Norm[19] = s.Ca_min;
			min_Norm[20] = s.Zr_min;
			min_Norm[21] = s.As_min;
			min_Norm[22] = s.Bi_min;
			min_Norm[23] = s.Sb_min;
			min_Norm[24] = s.Zn_min;
			min_Norm[25] = s.Mg_min;
			min_Norm[26] = s.N_min;
			min_Norm[27] = s.H_min;
			min_Norm[28] = s.O_min;
		}

		/// <summary>
		/// Wypelnia tablice wsp parowania z danego wytopu
		/// </summary>
		/// <param name="s">Wytop z ktorego brane sa dane</param>
		public void createTabofEvoporation(Smelt s)
		{
			evoporation[0] = s.Fe_evo;
			evoporation[1] = s.C_evo;
			evoporation[2] = s.Si_evo;
			evoporation[3] = s.Mn_evo;
			evoporation[4] = s.P_evo;
			evoporation[5] = s.S_evo;
			evoporation[6] = s.Cr_evo;
			evoporation[7] = s.Mo_evo;
			evoporation[8] = s.Ni_evo;
			evoporation[9] = s.Al_evo;
			evoporation[10] = s.Co_evo;
			evoporation[11] = s.Cu_evo;
			evoporation[12] = s.Nb_evo;
			evoporation[13] = s.Ti_evo;
			evoporation[14] = s.V_evo;
			evoporation[15] = s.W_evo;
			evoporation[16] = s.Pb_evo;

			evoporation[17] = s.Sn_evo;
			evoporation[18] = s.B_evo;
			evoporation[19] = s.Ca_evo;
			evoporation[20] = s.Zr_evo;
			evoporation[21] = s.As_evo;
			evoporation[22] = s.Bi_evo;
			evoporation[23] = s.Sb_evo;
			evoporation[24] = s.Zn_evo;
			evoporation[25] = s.Mg_evo;
			evoporation[26] = s.N_evo;
			evoporation[27] = s.H_evo;
			evoporation[28] = s.O_evo;
		}

		/// <summary>
		/// Funkcja tworzaca nowy wytop z danymi
		/// </summary>
		/// <param name="page">Strona na ktorej jest wyswietlany ewentualny error</param>
		/// <param name="name">Nazwa wytopu</param>
		/// <param name="femin">Minimalna zawartosc zelaza w wytopie</param>
		/// <param name="femax">Maks. zaw. zelaza w wytopie</param>
		/// <param name="feevo">Wsp. parowania zelaza</param>
		/// <param name="cmin"></param>
		/// <param name="cmax"></param>
		/// <param name="cevo">Wsp. parowania wegla</param>
		/// <param name="simin"></param>
		/// <param name="simax">Max. zaw. krzemu</param>
		/// <param name="sievo"></param>
		/// <param name="mnmin"></param>
		/// <param name="mnmax"></param>
		/// <param name="mnevo">Wsp. parowania manganu</param>
		/// <param name="pmin"></param>
		/// <param name="pmax">Max. zaw.fosforu</param>
		/// <param name="pevo"></param>
		/// <param name="smin"></param>
		/// <param name="smax"></param>
		/// <param name="sevo">Wsp. parowania krzemu</param>
		/// <param name="crmin"></param>
		/// <param name="crmax">Max. zaw. chromu</param>
		/// <param name="crevo"></param>
		/// <param name="momin">Min. zaw. molibdenu</param>
		/// <param name="momax">Max. zaw. molibdenu</param>
		/// <param name="moevo"></param>
		/// <param name="nimin">Min. zaw. niklu</param>
		/// <param name="nimax">Max. zaw. niklu</param>
		/// <param name="nievo"></param>
		/// <param name="almin">Min. zaw. aluminium</param>
		/// <param name="almax">Max. zaw. alum</param>
		/// <param name="alevo">Wsp. parowania aluminium</param>
		/// <param name="comin"></param>
		/// <param name="comax"></param>
		/// <param name="coevo"></param>
		/// <param name="cumin"></param>
		/// <param name="cumax"></param>
		/// <param name="cuevo">Wsp. parowania miedzi</param>
		/// <param name="nbmin"></param>
		/// <param name="nbmax"></param>
		/// <param name="nbevo"></param>
		/// <param name="timin">Min. zaw. tytanu</param>
		/// <param name="timax"></param>
		/// <param name="tievo"></param>
		/// <param name="vmin"></param>
		/// <param name="vmax"></param>
		/// <param name="vevo"></param>
		/// <param name="wmin"></param>
		/// <param name="wmax"></param>
		/// <param name="wevo"></param>
		/// <param name="pbmin"></param>
		/// <param name="pbmax"></param>
		/// <param name="pbevo">Wsp. parowania olowiu</param>
		/// <param name="snmin"></param>
		/// <param name="snmax"></param>
		/// <param name="snevo"></param>
		/// <param name="bmin"></param>
		/// <param name="bmax"></param>
		/// <param name="bevo"></param>
		/// <param name="camin">Min. zaw. wapnia</param>
		/// <param name="camax"></param>
		/// <param name="caevo"></param>
		/// <param name="zrmin"></param>
		/// <param name="zrmax"></param>
		/// <param name="zrevo"></param>
		/// <param name="asmin"></param>
		/// <param name="asmax"></param>
		/// <param name="asevo"></param>
		/// <param name="bimin"></param>
		/// <param name="bimax"></param>
		/// <param name="bievo"></param>
		/// <param name="sbmin"></param>
		/// <param name="sbmax"></param>
		/// <param name="sbevo"></param>
		/// <param name="znmin"></param>
		/// <param name="znmax"></param>
		/// <param name="znevo"></param>
		/// <param name="mgmin"></param>
		/// <param name="mgmax"></param>
		/// <param name="mgevo">Wsp. parowania magnezu</param>
		/// <param name="nmin">Min. zaw. azotu</param>
		/// <param name="nmax"></param>
		/// <param name="nevo"></param>
		/// <param name="hmin">Min. zaw. wodoru</param>
		/// <param name="hmax"></param>
		/// <param name="hevo"></param>
		/// <param name="omin"></param>
		/// <param name="omax"></param>
		/// <param name="oevo"></param>
		/// <returns>Stop z danymi</returns>
		public static Smelt addNewSmelt(Xamarin.Forms.Page page, string name,
			string femin, string femax, string feevo,
			string cmin, string cmax, string cevo,
			string simin, string simax, string sievo,
			string mnmin, string mnmax, string mnevo,
			string pmin, string pmax, string pevo,
			string smin, string smax, string sevo,
			string crmin, string crmax, string crevo,
			string momin, string momax, string moevo,
			string nimin, string nimax, string nievo,
			string almin, string almax, string alevo,
			string comin, string comax, string coevo,
			string cumin, string cumax, string cuevo,
			string nbmin, string nbmax, string nbevo,
			string timin, string timax, string tievo,
			string vmin, string vmax, string vevo,
			string wmin, string wmax, string wevo,
			string pbmin, string pbmax, string pbevo,
			string snmin, string snmax, string snevo,
			string bmin, string bmax, string bevo,
			string camin, string camax, string caevo,
			string zrmin, string zrmax, string zrevo,
			string asmin, string asmax, string asevo,
			string bimin, string bimax, string bievo,
			string sbmin, string sbmax, string sbevo,
			string znmin, string znmax, string znevo,
			string mgmin, string mgmax, string mgevo,
			string nmin, string nmax, string nevo,
			string hmin, string hmax, string hevo,
			string omin, string omax, string oevo)
		{
			Smelt sm = new Smelt();
			try
			{
				//nazwa z pola jest nazwa wytopu
				sm.name = name;
				sm.Fe_min = sm.parseThatValue(page, femin, false);
				sm.Fe_max = sm.parseThatValue(page, femax, true);
				sm.Fe_evo = sm.parseThatValue(page, feevo, false);

				sm.C_min = sm.parseThatValue(page, cmin, false);
				sm.C_max = sm.parseThatValue(page, cmax, true);
				sm.C_evo = sm.parseThatValue(page, cevo, false);

				sm.Si_min = sm.parseThatValue(page, simin, false);
				sm.Si_max = sm.parseThatValue(page, simax, true);
				sm.Si_evo = sm.parseThatValue(page, sievo, false);

				sm.Mn_min = sm.parseThatValue(page, mnmin, false);
				sm.Mn_max = sm.parseThatValue(page, mnmax, true);
				sm.Mn_evo = sm.parseThatValue(page, mnevo, false);

				sm.P_min = sm.parseThatValue(page, pmin, false);
				sm.P_max = sm.parseThatValue(page, pmax, true);
				sm.P_evo = sm.parseThatValue(page, pevo, false);

				sm.S_min = sm.parseThatValue(page, smin, false);
				sm.S_max = sm.parseThatValue(page, smax, true);
				sm.S_evo = sm.parseThatValue(page, sevo, false);

				sm.Cr_min = sm.parseThatValue(page, crmin, false);
				sm.Cr_max = sm.parseThatValue(page, crmax, true);
				sm.Cr_evo = sm.parseThatValue(page, crevo, false);

				sm.Mo_min = sm.parseThatValue(page, momin, false);
				sm.Mo_max = sm.parseThatValue(page, momax, true);
				sm.Mo_evo = sm.parseThatValue(page, moevo, false);

				sm.Ni_min = sm.parseThatValue(page, nimin, false);
				sm.Ni_max = sm.parseThatValue(page, nimax, true);
				sm.Ni_evo = sm.parseThatValue(page, nievo, false);

				sm.Al_min = sm.parseThatValue(page, almin, false);
				sm.Al_max = sm.parseThatValue(page, almax, true);
				sm.Al_evo = sm.parseThatValue(page, alevo, false);

				sm.Co_min = sm.parseThatValue(page, comin, false);
				sm.Co_max = sm.parseThatValue(page, comax, true);
				sm.Co_evo = sm.parseThatValue(page, coevo, false);

				sm.Cu_min = sm.parseThatValue(page, cumin, false);
				sm.Cu_max = sm.parseThatValue(page, cumax, true);
				sm.Cu_evo = sm.parseThatValue(page, cuevo, false);

				sm.Nb_min = sm.parseThatValue(page, nbmin, false);
				sm.Nb_max = sm.parseThatValue(page, nbmax, true);
				sm.Nb_evo = sm.parseThatValue(page, nbevo, false);

				sm.Ti_min = sm.parseThatValue(page, timin, false);
				sm.Ti_max = sm.parseThatValue(page, timax, true);
				sm.Ti_evo = sm.parseThatValue(page, tievo, false);

				sm.V_min = sm.parseThatValue(page, vmin, false);
				sm.V_max = sm.parseThatValue(page, vmax, true);
				sm.V_evo = sm.parseThatValue(page, vevo, false);

				sm.W_min = sm.parseThatValue(page, wmin, false);
				sm.W_max = sm.parseThatValue(page, wmax, true);
				sm.W_evo = sm.parseThatValue(page, wevo, false);

				sm.Pb_min = sm.parseThatValue(page, pbmin, false);
				sm.Pb_max = sm.parseThatValue(page, pbmax, true);
				sm.Pb_evo = sm.parseThatValue(page, pbevo, false);

				sm.Sn_min = sm.parseThatValue(page, snmin, false);
				sm.Sn_max = sm.parseThatValue(page, snmax, true);
				sm.Sn_evo = sm.parseThatValue(page, snevo, false);

				sm.B_min = sm.parseThatValue(page, bmin, false);
				sm.B_max = sm.parseThatValue(page, bmax, true);
				sm.B_evo = sm.parseThatValue(page, bevo, false);

				sm.Ca_min = sm.parseThatValue(page, camin, false);
				sm.Ca_max = sm.parseThatValue(page, camax, true);
				sm.Ca_evo = sm.parseThatValue(page, caevo, false);

				sm.Zr_min = sm.parseThatValue(page, zrmin, false);
				sm.Zr_max = sm.parseThatValue(page, zrmax, true);
				sm.Zr_evo = sm.parseThatValue(page, zrevo, false);

				sm.As_min = sm.parseThatValue(page, asmin, false);
				sm.As_max = sm.parseThatValue(page, asmax, true);
				sm.As_evo = sm.parseThatValue(page, asevo, false);

				sm.Bi_min = sm.parseThatValue(page, bimin, false);
				sm.Bi_max = sm.parseThatValue(page, bimax, true);
				sm.Bi_evo = sm.parseThatValue(page, bievo, false);

				sm.Sb_min = sm.parseThatValue(page, sbmin, false);
				sm.Sb_max = sm.parseThatValue(page, sbmax, true);
				sm.Sb_evo = sm.parseThatValue(page, sbevo, false);

				sm.Zn_min = sm.parseThatValue(page, znmin, false);
				sm.Zn_max = sm.parseThatValue(page, znmax, true);
				sm.Zn_evo = sm.parseThatValue(page, znevo, false);

				sm.Mg_min = sm.parseThatValue(page, mgmin, false);
				sm.Mg_max = sm.parseThatValue(page, mgmax, true);
				sm.Mg_evo = sm.parseThatValue(page, mgevo, false);

				sm.N_min = sm.parseThatValue(page, nmin, false);
				sm.N_max = sm.parseThatValue(page, nmax, true);
				sm.N_evo = sm.parseThatValue(page, nevo, false);

				sm.H_min = sm.parseThatValue(page, hmin, false);
				sm.H_max = sm.parseThatValue(page, hmax, true);
				sm.H_evo = sm.parseThatValue(page, hevo, false);

				sm.O_min = sm.parseThatValue(page, omin, false);
				sm.O_max = sm.parseThatValue(page, omax, true);
				sm.O_evo = sm.parseThatValue(page, oevo, false);

				//wypelnij tablice wszystkich danych
				sm.createTabofMinNorm(sm);
				sm.createTabofMaxNorm(sm);
				sm.createTabofEvoporation(sm);

				//Spprawdzenie czy zawartosci w wytopie sa poprawne [min < max]
				sm.checkifNormOK(page);
			}
			catch(Exception ex)
			{
				//Jezeli cokolwiek pojdzie zle to wyswietl blad
				page.DisplayAlert("Dziwny error", ex.ToString(), "OK");
			}
			//zwroc wytop
			return sm;
		}

		/// <summary>
		/// Sprawdza czy dane sa w porzadku
		/// </summary>
		/// <param name="page">Strona na ktorej jest wyswietlany komunikat o bledzie</param>
		private void checkifNormOK(Xamarin.Forms.Page page)
		{
			for(int i = 0; i < min_Norm.Count(); i++)
			{
				//Dla kazdego elementu sprawdz czy wartosc minalna jest mniejsza niz maksymalna
				if(min_Norm[i] > max_Norm[i])
				{
					//Jezeli jest zle pokaz komunikat
					page.DisplayAlert("Warning!", "W " + (i + 1).ToString() + " pierwiastku norma minimalna jest większa od maksymalnej.", "Zrozumiałem");
				}
			}
		}

		/// <summary>
		/// Funkcja zamieniajaca stringi na double, jezeli to mozliwe
		/// </summary>
		/// <param name="page">Strona na ktorej wyswietlany bedzie komunikat</param>
		/// <param name="element">tekst ktory ma byc zmieniony na liczbe</param>
		/// <param name="isMax">Jezeli true to zwraca 100% (maks), a nie 0</param>
		/// <returns>Zmieniona liczba</returns>
		private Double parseThatValue(Xamarin.Forms.Page page, string element, bool isMax)
		{
			double num = 0;
			//Jezeli string nic nie zawiera to zwroc po prostu zero
			if(string.IsNullOrWhiteSpace(element))
			{
				if(isMax)
					return 100;
				else
					return 0;
			}
			//W innym razie sproboj przeparsowac liczbe
			else if(Double.TryParse(element, out num))
			{
				//Jezeli jest ok to zwroc do produktu poprawna liczbe
				//TODO: Sprobowac znalezc sposb na poprawienie . i ,
				//TODO: Wartosci nieujemne i mniejsze od 100
				double tmp = double.Parse(element, NumberStyles.AllowDecimalPoint);
				if(tmp > 100)
					page.DisplayAlert("Warning!", "Wartość " + tmp + "przekracza 100%!", "Zrozumiałem");
				return tmp;
			}
			else
			{
				//Jezeli liczba to glupoty to wywal error w ktorym pokazesz jaka zawartosc jest zle, a do metalu zwroc NaN
				page.DisplayAlert("Error", "Nie udało się przetworzyć zawartości: " + element, "OK");
				return 0;
			}
		}
	}
}
