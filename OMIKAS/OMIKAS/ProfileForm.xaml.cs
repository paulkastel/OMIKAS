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
		public ProfileForm()
		{
			InitializeComponent();
			lbl_email.Text = App.DAUtil.GetUser().ElementAt(0).emailadd;
			lbl_name.Text = App.DAUtil.GetUser().ElementAt(0).user_name;
			lbl_surname.Text = App.DAUtil.GetUser().ElementAt(0).user_surname;
		}

		private async void btn_editprofile_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileEditForm(App.DAUtil.GetUser().ElementAt(0)));
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			lbl_email.Text = App.DAUtil.GetUser().ElementAt(0).emailadd;
			lbl_name.Text = App.DAUtil.GetUser().ElementAt(0).user_name;
			lbl_surname.Text = App.DAUtil.GetUser().ElementAt(0).user_surname;
		}
	}
}
