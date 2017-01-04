using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class ProfileForm : ContentPage
	{
		/// <summary>
		/// Konstruktor okna profilu
		/// </summary>
		public ProfileForm()
		{
			InitializeComponent();
			//Wszystkie dane wyswietl na etykiecie
			lbl_email.Text = App.DAUtil.GetUser().ElementAt(0).emailadd;
			lbl_name.Text = App.DAUtil.GetUser().ElementAt(0).user_name;
			lbl_surname.Text = App.DAUtil.GetUser().ElementAt(0).user_surname;
		}

		/// <summary>
		/// Uruchomienie okna z edycja danych profilu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_editprofile_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileEditForm(App.DAUtil.GetUser().ElementAt(0)));
		}

		/// <summary>
		/// Powrót do poprzedniej strony
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Odswieżanie strony z profilem
		/// </summary>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			lbl_email.Text = App.DAUtil.GetUser().ElementAt(0).emailadd;
			lbl_name.Text = App.DAUtil.GetUser().ElementAt(0).user_name;
			lbl_surname.Text = App.DAUtil.GetUser().ElementAt(0).user_surname;
		}
	}
}
