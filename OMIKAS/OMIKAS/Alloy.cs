using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIKAS
{
	public class Alloy
	{
		/// <summary>
		/// Nazwa produktu (stopu lub wytopu
		/// </summary>
		public string nameAlloy { get; set; }
		public double Fe { get; private set; }
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
		/// <summary>
		/// Cena stopu/wytopu
		/// </summary>
		public double Price { get; set; }
		public double Weight { get; set; }

		public double[] tabOfElements;

		private void createTabOfElements()
		{
			tabOfElements[0] = this.Fe;
			tabOfElements[1] = this.C;
			tabOfElements[2] = this.Si;
			tabOfElements[3] = this.Mn;
			tabOfElements[4] = this.P;
			tabOfElements[5] = this.S;
			tabOfElements[6] = this.Cr;
			tabOfElements[7] = this.Mo;
			tabOfElements[8] = this.Ni;
			tabOfElements[9] = this.Al;
			tabOfElements[10] = this.Co;
			tabOfElements[11] = this.Cu;
			tabOfElements[12] = this.Nb;
			tabOfElements[13] = this.Ti;
			tabOfElements[14] = this.V;
			tabOfElements[15] = this.W;
			tabOfElements[16] = this.Pb;
		}

		public Alloy()
		{
			tabOfElements = new double[17];
		}

		/// <summary>
		/// Funkcja tworzacy produkt metal z wypelnionymi danymi ktore pobiera z pol tekstowych
		/// </summary>
		/// <param name="page">Strona na ktorej moze sie pojawic error</param>
		/// <param name="name">nazwa produktu</param>
		/// <param name="price">cena produktu</param>
		/// <param name="weight">masa produktu</param>
		/// <param name="fe">zawartosc zelaza w produkcie</param>
		/// <param name="c">zawartosc wegla w produkcie</param>
		/// <param name="si">zawartosc krzemu w produkcie</param>
		/// <param name="mn"></param>
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
		public static Alloy addNewAlloy(Xamarin.Forms.Page page, string name, string price, string weight, string fe, string c, string si, string mn, string p, string s, string cr, string mo, string ni, string al, string co, string cu, string nb, string ti, string v, string w, string pb)
		{
			Alloy metal = new Alloy();
			//parsuj najwazniejsze dane
			metal.nameAlloy = name;
			metal.Price = metal.parseThatValue(page, price);
			metal.Weight = metal.parseThatValue(page, weight);

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

			metal.createTabOfElements();

			return metal;
		}

		/// <summary>
		/// Funkcja parsujaca dane i wyswietlajaca error na ekranie jezeli cos jest nie tak
		/// </summary>
		/// <param name="page">Strona na ktorej wyswietli sie error</param>
		/// <param name="element">liczba ktora chcemy przekonwertowac na double</param>
		/// <returns></returns>
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
				//Jezeli jest ok to zwroc do produktu poprawna liczbe z zamienionymi przecinkami na kropki
				//dzieki temu zabiegowi mozna dawac kropki i przecinki i jest fajnie
				return double.Parse(element.Replace(",", "."));
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