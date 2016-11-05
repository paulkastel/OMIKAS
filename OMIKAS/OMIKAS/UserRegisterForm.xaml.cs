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
				MasterDetailPage x = new MasterDetailPage();
				x.MasterBehavior = MasterBehavior.Popover;
				x.Master = new MainMenuSliderForm();
				x.Detail = new NavigationPage(new MainForm("User ktory zarejestrowal"));
				App.Current.MainPage = x;

				await Navigation.PopToRootAsync();
			}
		}

        private void btn_logGuest_Clicked(object sender, EventArgs e)
        {
			MasterDetailPage x = new MasterDetailPage();
			x.MasterBehavior = MasterBehavior.Popover;
			x.Master = new MainMenuSliderForm();
			x.Detail = new NavigationPage(new MainForm("Gosciu z rejest"));
			App.Current.MainPage = x;
		}
    }
}
