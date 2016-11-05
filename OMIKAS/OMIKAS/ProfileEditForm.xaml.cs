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
        public ProfileEditForm()
        {
            InitializeComponent();
        }

        private async void btn_confirmEdit_Clicked(object sender, EventArgs e)
        {
           await this.Navigation.PopAsync();
        }
    }
}
