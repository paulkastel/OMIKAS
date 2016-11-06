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
        }

        private async void btn_confirmEdit_Clicked(object sender, EventArgs e)
        {
           await this.Navigation.PopAsync();
        }
    }
}
