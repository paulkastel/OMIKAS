using MessageUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OMIKAS.iOS
{
	/// <summary>
	/// Generowanie raporów i wyslanie maili w systemie iOS - nie dziala
	/// </summary>
	public class Save_iOS : ISave
	{
		/// <summary>
		/// Sciezka do pliku z PDF
		/// </summary>
		string path;

		public void saveFile(List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut)
		{
			throw new NotImplementedException();
		}

		public void saveFileAndNotice(ProcessResults processResults, List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut)
		{
			throw new NotImplementedException();
		}

		public void sendEmail()
		{
			MFMailComposeViewController mailController;
			if(MFMailComposeViewController.CanSendMail)
			{
				// do mail operations here

				//Set the recipients, subject and message body
				mailController = new MFMailComposeViewController();
				mailController.SetToRecipients(new string[] { App.DAUtil.GetUser().ElementAt(0).emailadd });
				mailController.SetSubject("Raport OMIKAS");
				mailController.SetMessageBody("Raport programu OMIKAS", false);

				//Handle the Finished event.
				mailController.Finished += (object s, MFComposeResultEventArgs args) =>
				{
					Console.WriteLine(args.Result.ToString());
					args.Controller.DismissViewController(true, null);
				};
				//zalaczyc zalacznik i poprawic bledy

				//Present the MFMailComposeViewController. - linijka nizej powinna dzialac coz.
				//this.PresentViewController(mailController, true, null);
			}
		}
	}
}