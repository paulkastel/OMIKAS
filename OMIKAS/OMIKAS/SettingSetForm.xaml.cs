using System;

using Xamarin.Forms;

namespace OMIKAS
{
    public partial class SettingSetForm : ContentPage
    {
		/// <summary>
		/// Konstruktor okna
		/// </summary>
        public SettingSetForm()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Importowanie stopow i wytopow z pliku XML
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_importData_Clicked(object sender, EventArgs e)
		{
			var filesc = DependencyService.Get<ISave>();
		}
		
		/// <summary>
		/// Eksport stopow i wytopów do pliku XML
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_exportData_Clicked(object sender, EventArgs e)
		{
			var filesc = DependencyService.Get<ISave>();
			filesc.exportXMLData(this);
		}
	}
}
