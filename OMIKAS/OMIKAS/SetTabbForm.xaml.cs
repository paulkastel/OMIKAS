using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
    public partial class SetTabbForm : TabbedPage
    {
		/// <summary>
		/// Konstruktor
		/// </summary>
        public SetTabbForm()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Powrót do poprzedniej strony
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
