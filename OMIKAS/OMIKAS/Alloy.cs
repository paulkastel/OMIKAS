using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace OMIKAS
{
	/// <summary>
	/// Skladnik stopowy (stop metalu)
	/// </summary>
	public class Alloy
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		/// <summary>
		/// Nazwa stopu
		/// </summary>
		public string name { get; set; }

		public double Fe { get; set; }
		public double C { get; set; }
		public double Si { get; set; }
		public double Mn { get; set; }
		public double P { get; set; }
		public double S { get; set; }
		public double Cr { get; set; }
		public double Mo { get; set; }
		public double Ni { get; set; }
		public double Al { get; set; }
		public double Co { get; set; }
		public double Cu { get; set; }
		public double Nb { get; set; }
		public double Ti { get; set; }
		public double V { get; set; }
		public double W { get; set; }
		public double Pb { get; set; }
		public double Sn { get; set; }
		public double B { get; set; }
		public double Ca { get; set; }
		public double Zr { get; set; }
		public double As { get; set; }
		public double Bi { get; set; }
		public double Sb { get; set; }
		public double Zn { get; set; }
		public double Mg { get; set; }
		public double N { get; set; }
		public double H { get; set; }
		public double O { get; set; }

		/// <summary>
		/// Cena stopu/wytopu
		/// </summary>
		public double Price { get; set; }

		/// <summary>
		/// Lista wszystkich pierwiastkow w kolejnosci
		/// </summary>
		public double[] tabOfElements;

		/// <summary>
		/// Wypelnia tabOFElements wszystkimi pierwiastkami
		/// </summary>
		/// <param name="m">Konkretny stop z ktorego pobierane sa dane do tab</param>
		public void createTabOfElements(Alloy m)
		{
			tabOfElements[0] = m.Fe;
			tabOfElements[1] = m.C;
			tabOfElements[2] = m.Si;
			tabOfElements[3] = m.Mn;
			tabOfElements[4] = m.P;
			tabOfElements[5] = m.S;
			tabOfElements[6] = m.Cr;
			tabOfElements[7] = m.Mo;
			tabOfElements[8] = m.Ni;
			tabOfElements[9] = m.Al;
			tabOfElements[10] = m.Co;
			tabOfElements[11] = m.Cu;
			tabOfElements[12] = m.Nb;
			tabOfElements[13] = m.Ti;
			tabOfElements[14] = m.V;
			tabOfElements[15] = m.W;
			tabOfElements[16] = m.Pb;

			tabOfElements[17] = m.Sn;
			tabOfElements[18] = m.B;
			tabOfElements[19] = m.Ca;
			tabOfElements[20] = m.Zr;
			tabOfElements[21] = m.As;
			tabOfElements[22] = m.Bi;
			tabOfElements[23] = m.Sb;
			tabOfElements[24] = m.Zn;
			tabOfElements[25] = m.Mg;
			tabOfElements[26] = m.N;
			tabOfElements[27] = m.H;
			tabOfElements[28] = m.O;
		}

		/// <summary>
		/// Konstruktor stopu. Inicjalizuje tabOfElements
		/// </summary>
		public Alloy()
		{
			tabOfElements = new double[29];
		}

		/// <summary>
		/// Funkcja tworzacy produkt metal z wypelnionymi danymi ktore pobiera z pol tekstowych
		/// </summary>
		/// <param name="page">Strona na ktorej moze sie pojawic error</param>
		/// <param name="name">nazwa produktu</param>
		/// <param name="price">cena produktu</param>
		/// <param name="fe">zawartosc zelaza w produkcie</param>
		/// <param name="c">zawartosc wegla w produkcie</param>
		/// <param name="si">zawartosc krzemu w produkcie</param>
		/// <param name="mn">zawartosc manganu</param>
		/// <param name="p">zawartosc fosforu w produkcie</param>
		/// <param name="s"></param>
		/// <param name="cr"></param>
		/// <param name="mo"></param>
		/// <param name="ni"></param>
		/// <param name="al">zawartosc aluminium w produkcie</param>
		/// <param name="co"></param>
		/// <param name="cu">zawartosc miedzi w produkcie</param>
		/// <param name="nb"></param>
		/// <param name="ti"></param>
		/// <param name="v"></param>
		/// <param name="w"></param>
		/// <param name="pb">zawartosc olowiu w produkcie</param>
		/// <returns>Gotowy metal z wypelnionymi danymi</returns>
		public static Alloy addNewAlloy(Xamarin.Forms.Page page, string name, string price, string fe, string c, string si, string mn, string p, string s, string cr, string mo, string ni, string al, string co, string cu, string nb, string ti, string v, string w, string pb, string sn, string b, string ca, string zr, string aas, string bi, string sb, string zn, string mg, string n, string h, string o)
		{
			Alloy metal = new Alloy();
			//parsuj najwazniejsze dane
			try
			{
				metal.name = name;
				metal.Price = metal.parseThatValue(page, price);

				//parsuj dane skladnikow
				metal.Fe = metal.parseThatValue(page, fe);
				metal.C = metal.parseThatValue(page, c);
				metal.Si = metal.parseThatValue(page, si);
				metal.Mn = metal.parseThatValue(page, mn);
				metal.P = metal.parseThatValue(page, p);
				metal.S = metal.parseThatValue(page, s);
				metal.Cr = metal.parseThatValue(page, cr);
				metal.Mo = metal.parseThatValue(page, mo);
				metal.Ni = metal.parseThatValue(page, ni);
				metal.Al = metal.parseThatValue(page, al);
				metal.Co = metal.parseThatValue(page, co);
				metal.Cu = metal.parseThatValue(page, cu);
				metal.Nb = metal.parseThatValue(page, nb);
				metal.Ti = metal.parseThatValue(page, ti);
				metal.V = metal.parseThatValue(page, v);
				metal.W = metal.parseThatValue(page, w);
				metal.Pb = metal.parseThatValue(page, pb);
				metal.Sn = metal.parseThatValue(page, sn);
				metal.B = metal.parseThatValue(page, b);
				metal.Ca = metal.parseThatValue(page, ca);
				metal.Zr = metal.parseThatValue(page, zr);
				metal.As = metal.parseThatValue(page, aas);
				metal.Bi = metal.parseThatValue(page, bi);
				metal.Sb = metal.parseThatValue(page, sb);
				metal.Zn = metal.parseThatValue(page, zn);
				metal.Mg = metal.parseThatValue(page, mg);
				metal.N = metal.parseThatValue(page, n);
				metal.H = metal.parseThatValue(page, h);
				metal.O = metal.parseThatValue(page, o);

				metal.createTabOfElements(metal); //Wszystkie pierwiastki leca do tablicy
				metal.checkIfOK(page); //Sprawdzam czy suma wszystkich stopow nie przekaracza 100%
			}
			catch(Exception ex)
			{
				page.DisplayAlert("Dziwny error", ex.ToString(), "OK");
			}

			return metal;
		}
		
		/// <summary>
		/// Sprawdzenie czy suma wszystkich pierwiastkow nie jest większa niż 100%
		/// </summary>
		/// <param name="page">Strona na ktorej pojawi się ewentualny komunikat o błędzie</param>
		private void checkIfOK(Xamarin.Forms.Page page)
		{
			double suma=0;
			foreach(double x in tabOfElements)
			{
				suma = suma + x;
			}
			if(suma > 100)
				page.DisplayAlert("Warning!", "Suma procentowa wszystkich elementów przekracza 100%", "Zrozumialem");
		}

		/// <summary>
		/// Funkcja parsujaca dane i wyswietlajaca error na ekranie jezeli cos jest nie tak
		/// </summary>
		/// <param name="page">Strona na ktorej wyswietli sie error</param>
		/// <param name="element">liczba ktora chcemy przekonwertowac na double</param>
		/// <returns>Wartosc zalezna od stringu</returns>
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