using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class UserLoginForm : ContentPage
	{
		public UserLoginForm()
		{
			InitializeComponent();
		}

		private void btn_login_Clicked(object sender, EventArgs e)
		{
			App.IsUserLoggedIn = true;
			App.userapp.isUser = true;
			App.userapp.emailadd = ent_mail.Text;
			App.userapp.password = ent_pass.Text;
			App.setHomePageApp(App.userapp.user_name + App.userapp.user_surname);
		}

		private void btn_logGuest_Clicked(object sender, EventArgs e)
		{
			App.userapp.isUser = false;
			App.setHomePageApp("Gościu");
		}

		private async void regi_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new UserRegisterForm());
		}
	}
}
