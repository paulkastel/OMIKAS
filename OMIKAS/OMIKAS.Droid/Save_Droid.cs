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
				page.DisplayAlert("Dziwny error dot. Raportu", "Coœ niespotykaniego "+ex.ToString(), "OK");
			}
		}
	}
}