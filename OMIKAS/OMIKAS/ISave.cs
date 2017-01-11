using System.Collections.Generic;

namespace OMIKAS
{

	/// <summary>
	/// Interfejs zapisu pliku z ktorego potem dziedzicza Android i iOS
	/// </summary>
	public interface ISave
	{
		/// <summary>
		/// Zapisz raport do pliku PDF
		/// </summary>
		/// <param name="alloys">Lista stopów biorąca udział w procesie wytopu</param>
		/// <param name="smelt">Jednoelementowa lista wytopów zawierająca dane o wytopie do umieszczenia na raporcie</param>
		/// <param name="solut">Lista rozwiązań dla każdego z stopów</param>
		void saveFile(List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut);

		/// <summary>
		/// Uruchamia aplikacje do wysyłania maili i wysyła raport
		/// </summary>
		void sendEmail();

		/// <summary>
		/// Zapisuje raport do pliku PDF i wyświetla komunikat
		/// </summary>
		/// <param name="processResults">Strona na której wyświetli się komunikat</param>
		/// <param name="alloys">Lista stopów biorąca udział w procesie wytopu</param>
		/// <param name="smelt">Jednoelementowa lista wytopów zawierająca dane o wytopie do umieszczenia na raporcie</param>
		/// <param name="solut">Lista rozwiązań dla każdego z stopów</param>
		void saveFileAndNotice(ProcessResults processResults, List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut);

		void exportXMLData(Xamarin.Forms.Page page);
	}
}