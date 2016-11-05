using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace OMIKAS
{
    public partial class MainForm : ContentPage
    {
        public MainForm(string usr)
        {
			InitializeComponent();
			lbl_welcome.Text = "Witaj "+usr;
		}
	}
}
