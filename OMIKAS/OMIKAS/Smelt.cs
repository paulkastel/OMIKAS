﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Attributes;

namespace OMIKAS
{
	public class Smelt
	{
		[PrimaryKey, AutoIncrement]
		public int ID {get; set; }

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

		public double[] min_Norm;
		public double[] max_Norm;
		public double[] evoporation;

		public Smelt()
		{
			min_Norm = new double[29];
			max_Norm = new double[29];
			evoporation = new double[29];
		}

		private void createTabofMaxNorm()
		{
			max_Norm[0] = this.Fe_max;
			max_Norm[1] = this.C_max;
			max_Norm[2] = this.Si_max;
			max_Norm[3] = this.Mn_max;
			max_Norm[4] = this.P_max;
			max_Norm[5] = this.S_max;
			max_Norm[6] = this.Cr_max;
			max_Norm[7] = this.Mo_max;
			max_Norm[8] = this.Ni_max;
			max_Norm[9] = this.Al_max;
			max_Norm[10] = this.Co_max;
			max_Norm[11] = this.Cu_max;
			max_Norm[12] = this.Nb_max;
			max_Norm[13] = this.Ti_max;
			max_Norm[14] = this.V_max;
			max_Norm[15] = this.W_max;
			max_Norm[16] = this.Pb_max;

			max_Norm[17] = this.Sn_max;
			max_Norm[18] = this.B_max;
			max_Norm[19] = this.Ca_max;
			max_Norm[20] = this.Zr_max;
			max_Norm[21] = this.As_max;
			max_Norm[22] = this.Bi_max;
			max_Norm[23] = this.Sb_max;
			max_Norm[24] = this.Zn_max;
			max_Norm[25] = this.Mg_max;
			max_Norm[26] = this.N_max;
			max_Norm[27] = this.H_max;
			max_Norm[28] = this.O_max;
		}

		private void createTabofMinNorm()
		{
			min_Norm[0] = this.Fe_min;
			min_Norm[1] = this.C_min;
			min_Norm[2] = this.Si_min;
			min_Norm[3] = this.Mn_min;
			min_Norm[4] = this.P_min;
			min_Norm[5] = this.S_min;
			min_Norm[6] = this.Cr_min;
			min_Norm[7] = this.Mo_min;
			min_Norm[8] = this.Ni_min;
			min_Norm[9] = this.Al_min;
			min_Norm[10] = this.Co_min;
			min_Norm[11] = this.Cu_min;
			min_Norm[12] = this.Nb_min;
			min_Norm[13] = this.Ti_min;
			min_Norm[14] = this.V_min;
			min_Norm[15] = this.W_min;
			min_Norm[16] = this.Pb_min;

			min_Norm[17] = this.Sn_min;
			min_Norm[18] = this.B_min;
			min_Norm[19] = this.Ca_min;
			min_Norm[20] = this.Zr_min;
			min_Norm[21] = this.As_min;
			min_Norm[22] = this.Bi_min;
			min_Norm[23] = this.Sb_min;
			min_Norm[24] = this.Zn_min;
			min_Norm[25] = this.Mg_min;
			min_Norm[26] = this.N_min;
			min_Norm[27] = this.H_min;
			min_Norm[28] = this.O_min;
		}

		private void createTabofEvoporation()
		{
			evoporation[0] = this.Fe_evo;
			evoporation[1] = this.C_evo;
			evoporation[2] = this.Si_evo;
			evoporation[3] = this.Mn_evo;
			evoporation[4] = this.P_evo;
			evoporation[5] = this.S_evo;
			evoporation[6] = this.Cr_evo;
			evoporation[7] = this.Mo_evo;
			evoporation[8] = this.Ni_evo;
			evoporation[9] = this.Al_evo;
			evoporation[10] = this.Co_evo;
			evoporation[11] = this.Cu_evo;
			evoporation[12] = this.Nb_evo;
			evoporation[13] = this.Ti_evo;
			evoporation[14] = this.V_evo;
			evoporation[15] = this.W_evo;
			evoporation[16] = this.Pb_evo;

			evoporation[17] = this.Sn_evo;
			evoporation[18] = this.B_evo;
			evoporation[19] = this.Ca_evo;
			evoporation[20] = this.Zr_evo;
			evoporation[21] = this.As_evo;
			evoporation[22] = this.Bi_evo;
			evoporation[23] = this.Sb_evo;
			evoporation[24] = this.Zn_evo;
			evoporation[25] = this.Mg_evo;
			evoporation[26] = this.N_evo;
			evoporation[27] = this.H_evo;
			evoporation[28] = this.O_evo;
		}

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
				sm.name = name;
				sm.Fe_min = sm.parseThatValue(page, femin);
				sm.Fe_max = sm.parseThatValue(page, femax);
				sm.Fe_evo = sm.parseThatValue(page, feevo);

				sm.C_min = sm.parseThatValue(page, cmin);
				sm.C_max = sm.parseThatValue(page, cmax);
				sm.C_evo = sm.parseThatValue(page, cevo);

				sm.Si_min = sm.parseThatValue(page, simin);
				sm.Si_max = sm.parseThatValue(page, simax);
				sm.Si_evo = sm.parseThatValue(page, sievo);

				sm.Mn_min = sm.parseThatValue(page, mnmin);
				sm.Mn_max = sm.parseThatValue(page, mnmax);
				sm.Mn_evo = sm.parseThatValue(page, mnevo);

				sm.P_min = sm.parseThatValue(page, pmin);
				sm.P_max = sm.parseThatValue(page, pmax);
				sm.P_evo = sm.parseThatValue(page, pevo);

				sm.S_min = sm.parseThatValue(page, smin);
				sm.S_max = sm.parseThatValue(page, smax);
				sm.S_evo = sm.parseThatValue(page, sevo);

				sm.Cr_min = sm.parseThatValue(page, crmin);
				sm.Cr_max = sm.parseThatValue(page, crmax);
				sm.Cr_evo = sm.parseThatValue(page, crevo);

				sm.Mo_min = sm.parseThatValue(page, momin);
				sm.Mo_max = sm.parseThatValue(page, momax);
				sm.Mo_evo = sm.parseThatValue(page, moevo);

				sm.Ni_min = sm.parseThatValue(page, nimin);
				sm.Ni_max = sm.parseThatValue(page, nimax);
				sm.Ni_evo = sm.parseThatValue(page, nievo);

				sm.Al_min = sm.parseThatValue(page, almin);
				sm.Al_max = sm.parseThatValue(page, almax);
				sm.Al_evo = sm.parseThatValue(page, alevo);

				sm.Co_min = sm.parseThatValue(page, comin);
				sm.Co_max = sm.parseThatValue(page, comax);
				sm.Co_evo = sm.parseThatValue(page, coevo);

				sm.Cu_min = sm.parseThatValue(page, cumin);
				sm.Cu_max = sm.parseThatValue(page, cumax);
				sm.Cu_evo = sm.parseThatValue(page, cuevo);

				sm.Nb_min = sm.parseThatValue(page, nbmin);
				sm.Nb_max = sm.parseThatValue(page, nbmax);
				sm.Nb_evo = sm.parseThatValue(page, nbevo);

				sm.Ti_min = sm.parseThatValue(page, timin);
				sm.Ti_max = sm.parseThatValue(page, timax);
				sm.Ti_evo = sm.parseThatValue(page, tievo);

				sm.V_min = sm.parseThatValue(page, vmin);
				sm.V_max = sm.parseThatValue(page, vmax);
				sm.V_evo = sm.parseThatValue(page, vevo);

				sm.W_min = sm.parseThatValue(page, wmin);
				sm.W_max = sm.parseThatValue(page, wmax);
				sm.W_evo = sm.parseThatValue(page, wevo);

				sm.Pb_min = sm.parseThatValue(page, pbmin);
				sm.Pb_max = sm.parseThatValue(page, pbmax);
				sm.Pb_evo = sm.parseThatValue(page, pbevo);

				sm.Sn_min = sm.parseThatValue(page, snmin);
				sm.Sn_max = sm.parseThatValue(page, snmax);
				sm.Sn_evo = sm.parseThatValue(page, snevo);

				sm.B_min = sm.parseThatValue(page, bmin);
				sm.B_max = sm.parseThatValue(page, bmax);
				sm.B_evo = sm.parseThatValue(page, bevo);

				sm.Ca_min = sm.parseThatValue(page, camin);
				sm.Ca_max = sm.parseThatValue(page, camax);
				sm.Ca_evo = sm.parseThatValue(page, caevo);

				sm.Zr_min = sm.parseThatValue(page, zrmin);
				sm.Zr_max = sm.parseThatValue(page, zrmax);
				sm.Zr_evo = sm.parseThatValue(page, zrevo);

				sm.As_min = sm.parseThatValue(page, asmin);
				sm.As_max = sm.parseThatValue(page, asmax);
				sm.As_evo = sm.parseThatValue(page, asevo);

				sm.Bi_min = sm.parseThatValue(page, bimin);
				sm.Bi_max = sm.parseThatValue(page, bimax);
				sm.Bi_evo = sm.parseThatValue(page, bievo);

				sm.Sb_min = sm.parseThatValue(page, sbmin);
				sm.Sb_max = sm.parseThatValue(page, sbmax);
				sm.Sb_evo = sm.parseThatValue(page, sbevo);

				sm.Zn_min = sm.parseThatValue(page, znmin);
				sm.Zn_max = sm.parseThatValue(page, znmax);
				sm.Zn_evo = sm.parseThatValue(page, znevo);

				sm.Mg_min = sm.parseThatValue(page, mgmin);
				sm.Mg_max = sm.parseThatValue(page, mgmax);
				sm.Mg_evo = sm.parseThatValue(page, mgevo);

				sm.N_min = sm.parseThatValue(page, nmin);
				sm.N_max = sm.parseThatValue(page, nmax);
				sm.N_evo = sm.parseThatValue(page, nevo);

				sm.H_min = sm.parseThatValue(page, hmin);
				sm.H_max = sm.parseThatValue(page, hmax);
				sm.H_evo = sm.parseThatValue(page, hevo);

				sm.O_min = sm.parseThatValue(page, omin);
				sm.O_max = sm.parseThatValue(page, omax);
				sm.O_evo = sm.parseThatValue(page, oevo);


				sm.createTabofMinNorm();
				sm.createTabofMaxNorm();
				sm.createTabofEvoporation();
			}
			catch(Exception ex)
			{
				page.DisplayAlert("Dziwny error", ex.ToString(), "OK");
			}
			return sm;
		}

		private Double parseThatValue(Xamarin.Forms.Page page, string element)
		{
			double num = 0;
			//Jezeli string nic nie zawiera to zwroc po prostu zero
			if(string.IsNullOrWhiteSpace(element))
			{
				return 0;
			}
			//W innym razie sproboj przeparsowac liczbe
			else if(Double.TryParse(element, out num))
			{
				//Jezeli jest ok to zwroc do produktu poprawna liczbe
				//TODO: Sprobowac znalezc sposb na poprawienie . i ,
				//TODO: Wartosci nieujemne i mniejsze od 100
				return double.Parse(element, NumberStyles.AllowDecimalPoint);
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
