using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
    public partial class AlloyEditForm : ContentPage
    {
        public AlloyEditForm(string operation)
        {
            InitializeComponent();
			if(operation == "Add")
			{
				this.Title="Dodaj stop";
			}
			else if(operation == "Edit")
			{
				this.Title="Edytuj stop";
			}
			else
			{

			}
        }
    }
}
