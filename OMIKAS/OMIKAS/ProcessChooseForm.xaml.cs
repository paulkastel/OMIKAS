using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class ProcessChooseForm : ContentPage
	{
		public ProcessChooseForm()
		{
			InitializeComponent();
			InitializeComponent();
			listMetalView.ItemsSource = App.alloymetals;
		}

		private void btn_addAlloy_Clicked(object sender, EventArgs e)
		{

		}
	}
}
