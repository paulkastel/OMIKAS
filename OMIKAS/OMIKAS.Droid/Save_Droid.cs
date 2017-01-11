using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Xml.Linq;

[assembly: Dependency(typeof(OMIKAS.Droid.Save_Droid))]
namespace OMIKAS.Droid
{
	/// <summary>
	/// Klasa obsluguj¹ca generowanie raportów w systemie Android
	/// </summary>
	public class Save_Droid : ISave
	{
		/// <summary>
		/// Sciezka do pliku z ostatnim raportem
		/// </summary>
		string path;

		/// <summary>
		/// Obsluga wysylania maili. Stworz mail i uruchom odpowiedni program pocztowy
		/// </summary>
		public void sendEmail()
		{
			try
			{
				var email = new Intent(Android.Content.Intent.ActionSend);
				var file = new Java.IO.File(path);
				var uri = Android.Net.Uri.FromFile(file);

				file.SetReadable(true, true);
				//do kogo, tytul tresc i zalacznik
				email.PutExtra(Android.Content.Intent.ExtraEmail, new string[] { App.DAUtil.GetUser().ElementAt(0).emailadd });
				email.PutExtra(Android.Content.Intent.ExtraSubject, "Raport OMIKAS");
				email.PutExtra(Android.Content.Intent.ExtraText, "Raport programu OMIKAS");
				email.PutExtra(Intent.ExtraStream, uri);
				email.SetType("message/rfc822");

				//uruchom program pocztowy
				Forms.Context.StartActivity(email);
			}
			catch(Exception ex)
			{

			}
		}

		/// <summary>
		/// Zapisuje raport do pliku PDF i wyswietla o tym komunikat
		/// </summary>
		/// <param name="alloys">Lista skladnikow bioraca udzial w procesie</param>
		/// <param name="smelt">Jednoelementowa lista wytopu który chcemy zrealizowac</param>
		/// <param name="solut">Lista rozwi¹zañ</param>
		public void saveFile(List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut)
		{
			try
			{
				//stworz folder
				var directory = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory, "OMIKAS_Raports").ToString();
				if(!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				//sciezka do raportu pdf
				path = Path.Combine(directory, DateTime.Now.ToString("yyMMddHHmm") + "OMIKAS_Raport.pdf");

				if(File.Exists(path))
				{
					File.Delete(path);
				}

				//=========================================================================================
				//================ Tworzenie PDF za polacza iTextSharp ====================================
				#region PDFCreation

				var fs = new FileStream(path, FileMode.Create);
				//Stworz pdf o rozmiarze i marginesach
				Document document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);
				document.Open();

				//font w pdfie
				BaseFont bf = BaseFont.CreateFont(
						BaseFont.HELVETICA,
						BaseFont.CP1252,
						BaseFont.EMBEDDED);
				iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10);

				//metadane pliku
				document.AddAuthor("OMIKAS");
				document.AddCreator(App.DAUtil.GetUser().ElementAt(0).user_name + " " + App.DAUtil.GetUser().ElementAt(0).user_surname);
				document.AddKeywords("PDF alloys metallurgy education");
				document.AddSubject("Raport");
				document.AddTitle("OMIKAS Raport");

				//naglowek raportu
				document.Add(new Paragraph("Raport OMIKAS - Karta wytopu w prózniowym piecu indukcyjnym.", font));
				if(ProcessResults.isWeightType)
				{
					document.Add(new Paragraph("Optymalizacja masowa.", font));
				}
				else
				{
					document.Add(new Paragraph("Optymalizacja kosztowa.", font));
				}
				document.Add(new Paragraph("Autor: " + App.DAUtil.GetUser().ElementAt(0).user_name + " " + App.DAUtil.GetUser().ElementAt(0).user_surname, font));
				document.Add(new Paragraph("Data " + DateTime.Now.ToString(), font));

				//Cala glowna tabela - tabela wyswietla zawsze tylko kompletne rzêdy
				PdfPTable table = new PdfPTable(5 + alloys.Count);
				table.SpacingBefore = 10;
				table.WidthPercentage = 100;

				double sum = 0;
				int i = 0;

				#region row1
				//pierwszy rz¹d komórek w tabeli
				table.AddCell(" ");
				PdfPCell cwytop = new PdfPCell(new Phrase("Wytop '" + smelt.ElementAt(0).name + "'", font));
				cwytop.Colspan = 3;
				cwytop.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
				table.AddCell(cwytop);

				PdfPCell cwsad = new PdfPCell(new Phrase("Skladniki stopowe [g]", font));
				cwsad.Colspan = alloys.Count;
				cwsad.HorizontalAlignment = 1;
				table.AddCell(cwsad);
				table.AddCell(new Phrase("Masa [g]", font));
				#endregion

				#region row2
				table.AddCell(new Phrase("Pierwiastek", font));
				table.AddCell(new Phrase("min [%]", font));
				table.AddCell(new Phrase("max [%]", font));
				table.AddCell(new Phrase("wsp. par. [%]", font));
				//wszystkie nazwy skladnikow biorace udzial
				foreach(Alloy x in alloys)
				{
					table.AddCell(new Phrase(x.name, font));
				}
				table.AddCell(new Phrase("TOTAL", font)); //suma
				#endregion

				#region row3
				//rozwiazania stopow
				table.AddCell(new Phrase("TOTAL", font));
				table.AddCell(new Phrase(smelt.ElementAt(0).min_Norm.Sum().ToString(), font));
				table.AddCell(new Phrase("100", font));
				table.AddCell(new Phrase(smelt.ElementAt(0).evoporation.Sum().ToString(), font));
				sum = 0;
				foreach(ProcessResults.Solution x in solut)
				{
					table.AddCell(new Phrase(x.solTxt, font));
					sum += x.solNum; //oraz ich suma
				}
				table.AddCell(new Phrase(sum.ToString("0.000"), font));
				#endregion

				//reszta tabeli
				for(int j = 0; j < 29; j++)
				{
					//wszystkie nazwy pierwiastkow
					switch(j)
					{
						#region nameRows
						case 0:
							table.AddCell(new Phrase("Fe", font));
							break;
						case 1:
							table.AddCell(new Phrase("C", font));
							break;
						case 2:
							table.AddCell(new Phrase("Si", font));
							break;
						case 3:
							table.AddCell(new Phrase("Mn", font));
							break;
						case 4:
							table.AddCell(new Phrase("P", font));
							break;
						case 5:
							table.AddCell(new Phrase("S", font));
							break;
						case 6:
							table.AddCell(new Phrase("Cr", font));
							break;
						case 7:
							table.AddCell(new Phrase("Mo", font));
							break;
						case 8:
							table.AddCell(new Phrase("Ni", font));
							break;
						case 9:
							table.AddCell(new Phrase("Al", font));
							break;
						case 10:
							table.AddCell(new Phrase("Co", font));
							break;
						case 11:
							table.AddCell(new Phrase("Cu", font));
							break;
						case 12:
							table.AddCell(new Phrase("Nb", font));
							break;
						case 13:
							table.AddCell(new Phrase("Ti", font));
							break;
						case 14:
							table.AddCell(new Phrase("V", font));
							break;
						case 15:
							table.AddCell(new Phrase("W", font));
							break;
						case 16:
							table.AddCell(new Phrase("Pb", font));
							break;
						case 17:
							table.AddCell(new Phrase("Sn", font));
							break;
						case 18:
							table.AddCell(new Phrase("B", font));
							break;
						case 19:
							table.AddCell(new Phrase("Ca", font));
							break;
						case 20:
							table.AddCell(new Phrase("Zr", font));
							break;
						case 21:
							table.AddCell(new Phrase("As", font));
							break;
						case 22:
							table.AddCell(new Phrase("Bi", font));
							break;
						case 23:
							table.AddCell(new Phrase("Sb", font));
							break;
						case 24:
							table.AddCell(new Phrase("Zn", font));
							break;
						case 25:
							table.AddCell(new Phrase("Mg", font));
							break;
						case 26:
							table.AddCell(new Phrase("N", font));
							break;
						case 27:
							table.AddCell(new Phrase("H", font));
							break;
						case 28:
							table.AddCell(new Phrase("O", font));
							break;
						default:
							table.AddCell(new Phrase("ERROR", font));
							break;
							#endregion
					}
					//parametry wytopu
					table.AddCell(new Phrase(smelt.ElementAt(0).min_Norm[j].ToString(), font));
					table.AddCell(new Phrase(smelt.ElementAt(0).max_Norm[j].ToString(), font));
					table.AddCell(new Phrase(smelt.ElementAt(0).evoporation[j].ToString(), font));

					sum = 0;
					i = 0;

					//procentowa zawartosc razy masa skladnika to masa konkretnego piewiastka
					foreach(ProcessResults.Solution x in solut)
					{
						if(alloys.ElementAt(i).tabOfElements[j] / 100 * x.solNum == 0)
						{
							table.AddCell(new Phrase(" ", font));
						}
						else
						{
							table.AddCell(new Phrase((alloys.ElementAt(i).tabOfElements[j] / 100 * x.solNum).ToString("0.0000"), font));
						}
						sum += x.solNum * alloys.ElementAt(i).tabOfElements[j] / 100;
						i++;
					}
					table.AddCell(new Phrase(sum.ToString("0.000"), font)); //suma
				}

				//dodaj tabele do dokumentu
				document.Add(table);

				document.Close();
				writer.Close();
				fs.Close();
				#endregion
				//=========================================================================================
			}
			catch(Exception ex)
			{

			}
		}

		/// <summary>
		/// Zapisuje raport do pliku PDF i wyswietla o tym komunikat
		/// </summary>
		/// <param name="page">na stronie z wynikami ma sie pojawic komunikat</param>
		/// <param name="alloys">Lista skladnikow bioraca udzial w procesie</param>
		/// <param name="smelt">Jednoelementowa lista wytopu który chcemy zrealizowac</param>
		/// <param name="solut">Lista rozwi¹zañ</param>
		public void saveFileAndNotice(ProcessResults page, List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut)
		{
			try
			{
				//stworz folder
				var directory = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory, "OMIKAS_Raports").ToString();
				if(!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				//plik to data + nazwa
				path = Path.Combine(directory, DateTime.Now.ToString("yyMMddHHmm") + "OMIKAS_Raport.pdf");

				//jak istnieje to nadpisz plik
				if(File.Exists(path))
				{
					File.Delete(path);
				}

				//=========================================================================================
				//================ Tworzenie PDF za polacza iTextSharp ====================================
				#region PDFCreation

				var fs = new FileStream(path, FileMode.Create);
				//Stworz pdf o rozmiarze i marginesach
				Document document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
				PdfWriter writer = PdfWriter.GetInstance(document, fs);
				document.Open();

				//font w pdfie
				BaseFont bf = BaseFont.CreateFont(
						BaseFont.HELVETICA,
						BaseFont.CP1252,
						BaseFont.EMBEDDED);
				iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10);

				//metadane pliku
				document.AddAuthor("OMIKAS");
				document.AddCreator(App.DAUtil.GetUser().ElementAt(0).user_name + " " + App.DAUtil.GetUser().ElementAt(0).user_surname);
				document.AddKeywords("PDF alloys metallurgy education");
				document.AddSubject("Raport");
				document.AddTitle("OMIKAS Raport");

				//naglowek raportu
				document.Add(new Paragraph("Raport OMIKAS - Karta wytopu w prózniowym piecu indukcyjnym.", font));
				if(ProcessResults.isWeightType)
				{
					document.Add(new Paragraph("Optymalizacja masowa.", font));
				}
				else
				{
					document.Add(new Paragraph("Optymalizacja kosztowa.", font));
				}
				document.Add(new Paragraph("Autor: " + App.DAUtil.GetUser().ElementAt(0).user_name + " " + App.DAUtil.GetUser().ElementAt(0).user_surname, font));
				document.Add(new Paragraph("Data " + DateTime.Now.ToString(), font));

				//Cala glowna tabela - tabela wyswietla zawsze tylko kompletne rzêdy
				PdfPTable table = new PdfPTable(5 + alloys.Count);
				table.SpacingBefore = 10;
				table.WidthPercentage = 100;

				double sum = 0;
				int i = 0;

				#region row1
				//pierwszy rz¹d komórek w tabeli
				table.AddCell(" ");
				PdfPCell cwytop = new PdfPCell(new Phrase("Wytop '" + smelt.ElementAt(0).name + "'", font));
				cwytop.Colspan = 3;
				cwytop.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
				table.AddCell(cwytop);

				PdfPCell cwsad = new PdfPCell(new Phrase("Wsad [g]", font));
				cwsad.Colspan = alloys.Count;
				cwsad.HorizontalAlignment = 1;
				table.AddCell(cwsad);
				table.AddCell(new Phrase(" ", font));
				#endregion

				#region row2
				table.AddCell(new Phrase("Pierwiastek", font));
				table.AddCell(new Phrase("min [%]", font));
				table.AddCell(new Phrase("max [%]", font));
				table.AddCell(new Phrase("wsp. par. [%]", font));
				//wszystkie nazwy skladnikow biorace udzial
				foreach(Alloy x in alloys)
				{
					table.AddCell(new Phrase(x.name, font));
				}
				table.AddCell(new Phrase("TOTAL", font)); //suma
				#endregion

				#region row3
				//rozwiazania stopow
				table.AddCell(new Phrase("TOTAL", font));
				table.AddCell(new Phrase(smelt.ElementAt(0).min_Norm.Sum().ToString(), font));
				table.AddCell(new Phrase("100", font));
				table.AddCell(new Phrase(smelt.ElementAt(0).evoporation.Sum().ToString(), font));
				sum = 0;
				foreach(ProcessResults.Solution x in solut)
				{
					table.AddCell(new Phrase(x.solTxt, font));
					sum += x.solNum; //oraz ich suma
				}
				table.AddCell(new Phrase(sum.ToString("0.000"), font));
				#endregion

				//reszta tabeli
				for(int j = 0; j < 29; j++)
				{
					//wszystkie nazwy pierwiastkow
					switch(j)
					{
						#region nameRows
						case 0:
							table.AddCell(new Phrase("Fe", font));
							break;
						case 1:
							table.AddCell(new Phrase("C", font));
							break;
						case 2:
							table.AddCell(new Phrase("Si", font));
							break;
						case 3:
							table.AddCell(new Phrase("Mn", font));
							break;
						case 4:
							table.AddCell(new Phrase("P", font));
							break;
						case 5:
							table.AddCell(new Phrase("S", font));
							break;
						case 6:
							table.AddCell(new Phrase("Cr", font));
							break;
						case 7:
							table.AddCell(new Phrase("Mo", font));
							break;
						case 8:
							table.AddCell(new Phrase("Ni", font));
							break;
						case 9:
							table.AddCell(new Phrase("Al", font));
							break;
						case 10:
							table.AddCell(new Phrase("Co", font));
							break;
						case 11:
							table.AddCell(new Phrase("Cu", font));
							break;
						case 12:
							table.AddCell(new Phrase("Nb", font));
							break;
						case 13:
							table.AddCell(new Phrase("Ti", font));
							break;
						case 14:
							table.AddCell(new Phrase("V", font));
							break;
						case 15:
							table.AddCell(new Phrase("W", font));
							break;
						case 16:
							table.AddCell(new Phrase("Pb", font));
							break;
						case 17:
							table.AddCell(new Phrase("Sn", font));
							break;
						case 18:
							table.AddCell(new Phrase("B", font));
							break;
						case 19:
							table.AddCell(new Phrase("Ca", font));
							break;
						case 20:
							table.AddCell(new Phrase("Zr", font));
							break;
						case 21:
							table.AddCell(new Phrase("As", font));
							break;
						case 22:
							table.AddCell(new Phrase("Bi", font));
							break;
						case 23:
							table.AddCell(new Phrase("Sb", font));
							break;
						case 24:
							table.AddCell(new Phrase("Zn", font));
							break;
						case 25:
							table.AddCell(new Phrase("Mg", font));
							break;
						case 26:
							table.AddCell(new Phrase("N", font));
							break;
						case 27:
							table.AddCell(new Phrase("H", font));
							break;
						case 28:
							table.AddCell(new Phrase("O", font));
							break;
						default:
							table.AddCell(new Phrase("ERROR", font));
							break;
							#endregion
					}
					//parametry wytopu
					table.AddCell(new Phrase(smelt.ElementAt(0).min_Norm[j].ToString(), font));
					table.AddCell(new Phrase(smelt.ElementAt(0).max_Norm[j].ToString(), font));
					table.AddCell(new Phrase(smelt.ElementAt(0).evoporation[j].ToString(), font));

					sum = 0;
					i = 0;

					//procentowa zawartosc razy masa skladnika to masa konkretnego piewiastka
					foreach(ProcessResults.Solution x in solut)
					{
						if(alloys.ElementAt(i).tabOfElements[j] / 100 * x.solNum == 0)
						{
							table.AddCell(new Phrase(" ", font));
						}
						else
						{
							table.AddCell(new Phrase((alloys.ElementAt(i).tabOfElements[j] / 100 * x.solNum).ToString("0.0000"), font));
						}
						sum += x.solNum * alloys.ElementAt(i).tabOfElements[j] / 100;
						i++;
					}
					table.AddCell(new Phrase(sum.ToString("0.000"), font)); //suma
				}

				//dodaj tabele do dokumentu
				document.Add(table);

				document.Close();
				writer.Close();
				fs.Close();
				#endregion
				//=========================================================================================

				//Poka¿ komunikat i uruchom aplikacje do przegl¹dania PDF
				Java.IO.File file = new Java.IO.File(path);
				Intent intent = new Intent(Intent.ActionView);
				intent.SetDataAndType(Android.Net.Uri.FromFile(file), "application/pdf");
				page.DisplayAlert("Raport", "Raport wyeksportowano do " + path.ToString(), "OK");
				Forms.Context.StartActivity(intent);
			}
			catch(Exception ex)
			{
				page.DisplayAlert("Dziwny error dot. Raportu", "Coœ niespotykaniego " + ex.ToString(), "OK");
			}
		}

		public void exportXMLData(Xamarin.Forms.Page page)
		{
			var directory = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory, "OMIKAS_Raports").ToString();
			try
			{
				if(App.DAUtil.GetAllAlloys().Any())
				{
					var xml = new XElement("Alloys", App.DAUtil.GetAllAlloys().Select(x => new XElement("Alloy",
						new XAttribute("name", x.name),
						new XAttribute("Fe", x.Fe),
						new XAttribute("C", x.C),
						new XAttribute("Si", x.Si),
						new XAttribute("Mn", x.Mn),
						new XAttribute("P", x.P),
						new XAttribute("S", x.S),
						new XAttribute("Cr", x.Cr),
						new XAttribute("Mo", x.Mo),
						new XAttribute("Ni", x.Ni),
						new XAttribute("Al", x.Al),
						new XAttribute("Co", x.Co),
						new XAttribute("Cu", x.Cu),
						new XAttribute("Nb", x.Nb),
						new XAttribute("Ti", x.Ti),
						new XAttribute("V", x.V),
						new XAttribute("W", x.W),
						new XAttribute("Pb", x.Pb),
						new XAttribute("Sn", x.Sn),
						new XAttribute("B", x.B),
						new XAttribute("Ca", x.Ca),
						new XAttribute("Zr", x.Zr),
						new XAttribute("As", x.As),
						new XAttribute("Bi", x.Bi),
						new XAttribute("Sb", x.Sb),
						new XAttribute("Zn", x.Zn),
						new XAttribute("Mg", x.Mg),
						new XAttribute("N", x.N),
						new XAttribute("H", x.H),
						new XAttribute("O", x.O))));

					if(!Directory.Exists(directory))
					{
						Directory.CreateDirectory(directory);
					}

					//sciezka do exportu xml
					var path = Path.Combine(directory, "OMIKAS_AlloysExport.xml");
					if(File.Exists(path))
					{
						File.Delete(path);
					}
					xml.Save(path);
					page.DisplayAlert("Export", "Skladniki stopowe wyeksportowano do pliku " + path.ToString(), "OK");
				}
				else
				{
					page.DisplayAlert("Export", "Brak stopów do eksportu", "OK");
				}
			}
			catch(Exception ex)
			{
				page.DisplayAlert("Error", ex.ToString(), "OK");
			}
			try
			{
				if(App.DAUtil.GetAllSmelts().Any())
				{
					var xml = new XElement("Smelts", App.DAUtil.GetAllSmelts().Select(x => new XElement("Smelt",
						new XAttribute("name", x.name),
						new XAttribute("minFe", x.Fe_min),
						new XAttribute("minC", x.C_min),
						new XAttribute("minSi", x.Si_min),
						new XAttribute("minMn", x.Mn_min),
						new XAttribute("minP", x.P_min),
						new XAttribute("minS", x.S_min),
						new XAttribute("minCr", x.Cr_min),
						new XAttribute("minMo", x.Mo_min),
						new XAttribute("minNi", x.Ni_min),
						new XAttribute("minAl", x.Al_min),
						new XAttribute("minCo", x.Co_min),
						new XAttribute("minCu", x.Cu_min),
						new XAttribute("minNb", x.Nb_min),
						new XAttribute("minTi", x.Ti_min),
						new XAttribute("minV", x.V_min),
						new XAttribute("minW", x.W_min),
						new XAttribute("minPb", x.Pb_min),
						new XAttribute("minSn", x.Sn_min),
						new XAttribute("minB", x.B_min),
						new XAttribute("minCa", x.Ca_min),
						new XAttribute("minZr", x.Zr_min),
						new XAttribute("minAs", x.As_min),
						new XAttribute("minBi", x.Bi_min),
						new XAttribute("minSb", x.Sb_min),
						new XAttribute("minZn", x.Zn_min),
						new XAttribute("minMg", x.Mg_min),
						new XAttribute("minN", x.N_min),
						new XAttribute("minH", x.H_min),
						new XAttribute("minO", x.O_min),

						new XAttribute("maxFe", x.Fe_max),
						new XAttribute("maxC", x.C_max),
						new XAttribute("maxSi", x.Si_max),
						new XAttribute("maxMn", x.Mn_max),
						new XAttribute("maxP", x.P_max),
						new XAttribute("maxS", x.S_max),
						new XAttribute("maxCr", x.Cr_max),
						new XAttribute("maxMo", x.Mo_max),
						new XAttribute("maxNi", x.Ni_max),
						new XAttribute("maxAl", x.Al_max),
						new XAttribute("maxCo", x.Co_max),
						new XAttribute("maxCu", x.Cu_max),
						new XAttribute("maxNb", x.Nb_max),
						new XAttribute("maxTi", x.Ti_max),
						new XAttribute("maxV", x.V_max),
						new XAttribute("maxW", x.W_max),
						new XAttribute("maxPb", x.Pb_max),
						new XAttribute("maxSn", x.Sn_max),
						new XAttribute("maxB", x.B_max),
						new XAttribute("maxCa", x.Ca_max),
						new XAttribute("maxZr", x.Zr_max),
						new XAttribute("maxAs", x.As_max),
						new XAttribute("maxBi", x.Bi_max),
						new XAttribute("maxSb", x.Sb_max),
						new XAttribute("maxZn", x.Zn_max),
						new XAttribute("maxMg", x.Mg_max),
						new XAttribute("maxN", x.N_max),
						new XAttribute("maxH", x.H_max),
						new XAttribute("maxO", x.O_max),

						new XAttribute("evoFe", x.Fe_evo),
						new XAttribute("evoC", x.C_evo),
						new XAttribute("evoSi", x.Si_evo),
						new XAttribute("evoMn", x.Mn_evo),
						new XAttribute("evoP", x.P_evo),
						new XAttribute("evoS", x.S_evo),
						new XAttribute("evoCr", x.Cr_evo),
						new XAttribute("evoMo", x.Mo_evo),
						new XAttribute("evoNi", x.Ni_evo),
						new XAttribute("evoAl", x.Al_evo),
						new XAttribute("evoCo", x.Co_evo),
						new XAttribute("evoCu", x.Cu_evo),
						new XAttribute("evoNb", x.Nb_evo),
						new XAttribute("evoTi", x.Ti_evo),
						new XAttribute("evoV", x.V_evo),
						new XAttribute("evoW", x.W_evo),
						new XAttribute("evoPb", x.Pb_evo),
						new XAttribute("evoSn", x.Sn_evo),
						new XAttribute("evoB", x.B_evo),
						new XAttribute("evoCa", x.Ca_evo),
						new XAttribute("evoZr", x.Zr_evo),
						new XAttribute("evoAs", x.As_evo),
						new XAttribute("evoBi", x.Bi_evo),
						new XAttribute("evoSb", x.Sb_evo),
						new XAttribute("evoZn", x.Zn_evo),
						new XAttribute("evoMg", x.Mg_evo),
						new XAttribute("evoN", x.N_evo),
						new XAttribute("evoH", x.H_evo),
						new XAttribute("evoO", x.O_evo))));

					if(!Directory.Exists(directory))
					{
						Directory.CreateDirectory(directory);
					}
					var path = Path.Combine(directory, "OMIKAS_SmeltsExport.xml");
					if(File.Exists(path))
					{
						File.Delete(path);
					}
					xml.Save(path);
					page.DisplayAlert("Export", "Wytopy wyeksportowano do pliku " + path.ToString(), "OK");
				}
				else
				{
					page.DisplayAlert("Export", "Brak wytopów do eksportu", "OK");
				}
			}
			catch(Exception ex)
			{
				page.DisplayAlert("Error", ex.ToString(), "OK");
			}
		}
	}
}