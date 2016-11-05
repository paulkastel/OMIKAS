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
		}

        private async void btn_editprofile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfileEditForm());
        }

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
