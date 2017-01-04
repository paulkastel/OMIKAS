using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class SmeltAllForm : ContentPage
	{
		/// <summary>
		/// Konstruktor okna z lista wytopow
		/// </summary>
		public SmeltAllForm()
		{
			InitializeComponent();
			//Wypelnij liste na ekranie wszystkimi wytopami jakie sa w bazie
			smeltsmetalView.ItemsSource = App.DAUtil.GetAllSmelts();
		}

		/// <summary>
		/// Pokazuje okna z informacja o konkretnym wytopie
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_info_Clicked(object sender, EventArgs e)
		{
			//Klikniety przycisk jest sygnalem ktory powiazuje kontekts z wytopem z listy
			var signal = sender as Button;
			var smelt = signal.BindingContext as Smelt;

			//odpal strone z szczeglowami wytopu
			await Navigation.PushAsync(new SmeltDetailForm(smelt));
		}

		/// <summary>
		/// Wyswietla komunikat z zapytaniem czy usunac wytop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_delSmelt_Clicked(object sender, EventArgs e)
		{
			//Klikniety przycisk jest sygnalem ktory powiazuje kontekst z wtyopem z listy
			var signal = sender as Button;
			var smelt = signal.BindingContext as Smelt;

			//Na podstawie odpowiedzi usun wytop z bazy
			var answer = await DisplayAlert("Usun", "Na pewno usunac " + smelt.name + "?", "Tak", "Nie");
			if(answer)
			{
				App.DAUtil.DeleteSmelt(smelt);
			}
			OnAppearing(); //A nastepnie uruchom funkcje odswiezajaca ekran
		}

		/// <summary>
		/// Pokazuje okno dodania nowego wytopu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_addSmelt_Clicked(object sender, EventArgs e)
		{
			//Konstruktor okna bez argumentow = dodaj nowy wytop
			await Navigation.PushAsync(new SmeltEditForm());
		}

		/// <summary>
		/// Pokazuje okno edycji wytopu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_editSmelt_Clicked(object sender, EventArgs e)
		{
			//Klikniety przycisk jest sygnalem ktory powiazuje kontekst z wtyopem z listy
			var signal = sender as Button;
			var metal = signal.BindingContext as Smelt;

			//Konstruktor okna z dwoma argumentami = edycja
			await Navigation.PushAsync(new SmeltEditForm(metal, App.smeltals.IndexOf(metal)));
		}

		/// <summary>
		/// Powrót do poprzedniego okna
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			//Schowaj ten ekran
			await Navigation.PopModalAsync();
		}

		/// <summary>
		///  Po pojawieniu się ekranu, odświeża listę i sortuje ją alfabetycznie
		/// </summary>
		protected override void OnAppearing()
		{
			//Dla poprawnego dzialania zeruje widok listy i cala liste laduje z bazy
			smeltsmetalView.ItemsSource = null;

			var currentList = App.DAUtil.GetAllSmelts();
			this.smeltsmetalView.ItemsSource = currentList;
			var newList = currentList.OrderBy(x => x.name).ToList();
			this.smeltsmetalView.ItemsSource = newList;
		}
	}
}