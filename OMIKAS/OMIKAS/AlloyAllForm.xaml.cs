using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OMIKAS
{
	public partial class AlloyAllForm : ContentPage
	{
		/// <summary>
		/// Konstruktor okna z lista skladnikow stopowych
		/// </summary>
		public AlloyAllForm()
		{
			InitializeComponent();
			//Wypelnij liste na ekranie wszystkimi stopami jakie sa w bazie
			alloymetalView.ItemsSource = App.DAUtil.GetAllAlloys();
		}

		/// <summary>
		/// Pokazuje okno z informacja o konkretnym skladniku stopowym
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_info_Clicked(object sender, EventArgs e)
		{
			//Klikniety przycisk jest sygnalem ktory powiazuje kontekts z skladnikiem stopowym z listy
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;

			//odpal ta strone z informacja o skladniku
			await Navigation.PushAsync(new AlloyDetailForm(metal));
		}

		/// <summary>
		/// Wyswietla komunikat z zapytaniem czy usunac skladnik
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_delAlloy_Clicked(object sender, EventArgs e)
		{
			//Klikniety przycisk jest sygnalem ktory powiazuje kontekts z skladnikiem stopowym z listy
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;

			//Na podstawie odpowiedzi usun skladnik z bazy
			var answer = await DisplayAlert("Usun", "Na pewno usunac " + metal.name + "?", "Tak", "Nie");
			if(answer)
			{
				App.DAUtil.DeleteAlloy(metal);
			}
			OnAppearing(); //A nastepnie uruchom funkcje odswiezajaca ekran
		}

		/// <summary>
		/// Pokazuje okno dodania nowego elementu do skladnikow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_addAlloy_Clicked(object sender, EventArgs e)
		{
			//Konstruktor okna bez argumentow = dodaj nowy element
			await Navigation.PushAsync(new AlloyEditForm());
		}

		/// <summary>
		/// Pokazuje okno edycji skladnika stopowego
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btn_editAlloy_Clicked(object sender, EventArgs e)
		{
			//Klikniety przycisk jest sygnalem ktory powiazuje kontekts z skladnikiem stopowym z listy
			var signal = sender as Button;
			var metal = signal.BindingContext as Alloy;

			//Konstruktor okna z dwoma argumentami = edycja istniejacego elementu
			await Navigation.PushAsync(new AlloyEditForm(metal, App.alloymetals.IndexOf(metal)));
		}

		/// <summary>
		/// Powrot do poprzedniego okna
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			//Schowaj ten ekran
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Po pojawieniu się ekranu, odświeża listę i sortuje ją alfabetycznie
		/// </summary>
		protected override void OnAppearing()
		{
			//Dla poprawnego dzialania zeruje widok listy i cala liste laduje z bazy
			alloymetalView.ItemsSource = null;

			var currentList = App.DAUtil.GetAllAlloys();
			this.alloymetalView.ItemsSource = currentList;
			var newList = currentList.OrderBy(x => x.name).ToList();
			this.alloymetalView.ItemsSource = newList;
		}
	}
}