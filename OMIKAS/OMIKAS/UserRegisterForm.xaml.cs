using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
    public partial class UserRegisterForm : ContentPage
    {
        public UserRegisterForm()
        {
            InitializeComponent();
        }

        private async void btn_register_Clicked(object sender, EventArgs e)
        {
			var rootPage = Navigation.NavigationStack.FirstOrDefault();
			if(rootPage != null)
			{
				App.IsUserLoggedIn = true;
				App.userapp.isUser = true;
				App.userapp.emailadd = ent_email.Text;
				App.userapp.password = ent_pswd1.Text;
				App.userapp.user_surname = ent_surname.Text;
				App.setHomePageApp(App.userapp.user_name + App.userapp.user_surname);
				await Navigation.PopToRootAsync();
			}
		}

        private void btn_logGuest_Clicked(object sender, EventArgs e)
        {
			App.userapp.isUser = false;
			App.setHomePageApp("Gościu");
		}
    }
}
