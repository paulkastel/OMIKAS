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
            AlloyList.ItemsSource = new List<Alloy> {
                new Alloy { Name = "stop1" },
                new Alloy { Name = "stop2" },
                new Alloy { Name = "stop3" },
            };
        }
    }
}
