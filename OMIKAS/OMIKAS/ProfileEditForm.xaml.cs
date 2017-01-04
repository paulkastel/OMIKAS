using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class ProfileEditForm : ContentPage
	{
		/// <summary>
		/// Pomocnicza zmienna usera poddawana edycji
		/// </summary>
		private User profile_edit;

		/// <summary>
		/// Konstruktor okna
		/// </summary>
		/// <param name="user_profile">User ktory jest edytowany</param>
		public ProfileEditForm(User user_profile)
		{
			InitializeComponent();
			//przekopiowanie wszystkich danych
			profile_edit = user_profile;
			txtname.Text = profile_edit.user_name;
			txtsurname.Text = profile_edit.user_surname;
			txtmail.Text = profile_edit.emailadd;
		}

		/// <summary>
		/// Wykonanie edycji po profilu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_confirmEdit_Clicked(object sender, EventArgs e)
		{
			//wklep dane do usera pomocniczego
			profile_edit.user_name = txtname.Text;
			profile_edit.user_surname = txtsurname.Text;
			profile_edit.emailadd = txtmail.Text;

			if(string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtsurname.Text))
			{
				//Jezeli imie i nazwisko jest nieuzupelnione to pokaz error
				await DisplayAlert("Error!", "Uzupełnij dane!", "OK");
			}
			else
			{
				//Uakutalnij dane usera w bazie i schowaj strone
				App.DAUtil.UpdateUser(profile_edit);
				await this.Navigation.PopAsync();
			}
		}
	}
}
