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

			MasterDetailPage x = new MasterDetailPage();
			x.MasterBehavior = MasterBehavior.Popover;
			x.Master = new MainMenuSliderForm();
			x.Detail = new NavigationPage(new MainForm("User ktory zalogowal"));
			App.Current.MainPage = x;
		}

		private void btn_logGuest_Clicked(object sender, EventArgs e)
        {
			MasterDetailPage x = new MasterDetailPage();
			x.MasterBehavior = MasterBehavior.Popover;
			x.Master = new MainMenuSliderForm();
			x.Detail = new NavigationPage(new MainForm("Gosciu z log"));
			App.Current.MainPage = x;
		}

		private async void regi_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new UserRegisterForm());
		}
	}
}
