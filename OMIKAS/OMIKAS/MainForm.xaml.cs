using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace OMIKAS
{
    public partial class MainForm : ContentPage
    {
		/// <summary>
		/// Konstruktor klasy, tworzy okno z użytkownikiem oraz iloscia jego danych
		/// </summary>
		/// <param name="usr">Nazwa uzytkownika uruchamiajacego okno</param>
        public MainForm(string usr)
        {
			InitializeComponent();
			//Uzupelnij tekst etykiet
			lbl_welcome.Text = "Witaj " + App.DAUtil.GetUser().ElementAt(0).user_name;
			lbl_all_stat.Text = "Masz "+App.alloymetals.Count.ToString()+" stworzonych składników stopowych.";
			lbl_smel_stat.Text = "Masz "+App.smeltals.Count.ToString()+" stworzonych wytopów.";
		}
		/// <summary>
		/// Odswieza dane po pokazaniu okna
		/// </summary>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			//Uzupelnij tekst etykiet
			lbl_welcome.Text = "Witaj " + App.DAUtil.GetUser().ElementAt(0).user_name;
			lbl_all_stat.Text = "Masz " + App.alloymetals.Count.ToString() + " stworzonych składników stopowych.";
			lbl_smel_stat.Text = "Masz " + App.smeltals.Count.ToString() + " stworzonych wytopów.";
		}
	}
}
