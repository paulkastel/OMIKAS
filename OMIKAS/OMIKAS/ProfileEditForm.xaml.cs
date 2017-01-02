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
		private User profile_edit;

		public ProfileEditForm(User user_profile)
		{
			InitializeComponent();
			profile_edit = user_profile;
			txtname.Text = profile_edit.user_name;
			txtsurname.Text = profile_edit.user_surname;
			txtmail.Text = profile_edit.emailadd;
		}

		private async void btn_confirmEdit_Clicked(object sender, EventArgs e)
		{
			profile_edit.user_name = txtname.Text;
			profile_edit.user_surname = txtsurname.Text;
			profile_edit.emailadd = txtmail.Text;

			if(string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtsurname.Text))
			{
				await DisplayAlert("Error!", "Uzupełnij dane!", "OK");
			}
			else
			{
				App.DAUtil.UpdateUser(profile_edit);
				await this.Navigation.PopAsync();
			}
		}
	}
}
