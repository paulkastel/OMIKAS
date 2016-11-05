using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
    public partial class AlloyAllForm : ContentPage
    {
        public AlloyAllForm()
        {
            InitializeComponent();
            AlloyList.ItemsSource = new List<Alloy> {
                new Alloy { Name = "stop1" },
                new Alloy { Name = "stop2" },
                new Alloy { Name = "stop3" },
            };
        }

        private async void btn_info_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlloyDetailForm());
        }

        private async void btn_delAlloy_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Usun", "Czy chcesz usunac stop?", "Tak", "Nie");
            Debug.WriteLine("Odpowiedz: " + answer);
        }

        private async void btn_addAlloy_Clicked(object sender, EventArgs e)
        {
            //ustawic argumenty w AlloyEditForm tak aby rozorznial czy wywolala go edycja czy add
            await Navigation.PushAsync(new AlloyEditForm("Add"));
        }

        private async void btn_editAlloy_Clicked(object sender, EventArgs e)
        {
            //ustawic argumenty w AlloyEditForm tak aby rozorznial czy wywolala go edycja czy add
            await Navigation.PushAsync(new AlloyEditForm("Edit"));
        }

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
