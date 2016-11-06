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
		private User user_profile;
		public ProfileForm()
		{
			InitializeComponent();
			user_profile = App.userapp;
			if(user_profile.isUser)
			{
				lbl_surname.Text = user_profile.user_surname;
				lbl_name.Text = user_profile.user_name;
				lbl_email.Text = user_profile.emailadd;
			}
			else
			{
				lbl_name.Text = "Gość";
				lbl_surname.Text = "Gość";
				lbl_email.Text = "";
				btn_editprofile.IsEnabled = user_profile.isUser;
			}
		}

		private async void btn_editprofile_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileEditForm(user_profile));
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
