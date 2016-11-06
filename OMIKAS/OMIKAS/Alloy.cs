using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMIKAS
{
	public class Alloy
	{
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

		private bool isadd { get; set; }

		public static Alloy addNewAlloy(Xamarin.Forms.Page page, string name, string fe, string c, string si, string mn, string p, string s, string cr, string mo, string ni, string al, string co, string cu, string nb, string ti, string v, string w, string pb)
		{
			Alloy metal = new Alloy();
			metal.nameAlloy = name;

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

			return metal;
		}

		private Double parseThatValue(Xamarin.Forms.Page page, string element)
		{
			double num = 0;
			if(string.IsNullOrWhiteSpace(element))
			{
				return 0;
			}
			else if(Double.TryParse(element, out num))
			{
				return double.Parse(element.Replace(",", "."));
			}
			else
			{
				page.DisplayAlert("Error", "Nie udało się przetworzyć zawartości: " + element, "OK");
				return double.NaN;
			}

		}
	}
}
