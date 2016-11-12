using System;

using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace OMIKAS
{
	/* 
	* based on
	* https://gist.github.com/glennstephens/76e7e347ca6c19d4ef15
	*/

	public class SelectMultipleBasePage<T> : ContentPage
	{
		public class WrappedSelection<T> : INotifyPropertyChanged
		{
			public T Item { get; set; }
			bool isSelected = false;
			public bool IsSelected
			{
				get
				{
					return isSelected;
				}
				set
				{
					if(isSelected != value)
					{
						isSelected = value;
						PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
						//						PropertyChanged (this, new PropertyChangedEventArgs (nameof (IsSelected))); // C# 6
					}
				}
			}
			public event PropertyChangedEventHandler PropertyChanged = delegate { };
		}
		public class WrappedItemSelectionTemplate : ViewCell
		{
			public WrappedItemSelectionTemplate() : base()
			{
				Label name = new Label() { VerticalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#1E1E1E"), FontSize = 15 };
				name.SetBinding(Label.TextProperty, new Binding("Item.nameAlloy"));
				Switch mainSwitch = new Switch() { HorizontalOptions = LayoutOptions.EndAndExpand };
				mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));

				Grid layout = new Grid();
				layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
				layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				layout.Children.Add(mainSwitch, 0, 0);
				layout.Children.Add(name, 1, 0);

				View = layout;
			}
		}
		public List<WrappedSelection<T>> WrappedItems = new List<WrappedSelection<T>>();

		public SelectMultipleBasePage(List<T> items)
		{
			all = false;

			WrappedItems = items.Select(item => new WrappedSelection<T>() { Item = item, IsSelected = false }).ToList();
			ListView mainList = new ListView()
			{
				ItemsSource = WrappedItems,
				ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
			};

			mainList.ItemSelected += (sender, e) =>
			{
				if(e.SelectedItem == null)
					return;
				var o = (WrappedSelection<T>)e.SelectedItem;
				o.IsSelected = !o.IsSelected;
				((ListView)sender).SelectedItem = null; //de-select
			};
			Content = mainList;
			ToolbarItems.Add(new ToolbarItem("Zaznacz wszystkie", null, SelectAll, ToolbarItemOrder.Primary));
		}

		private bool all;
		void SelectAll()
		{
			all = !all;
			foreach(var wi in WrappedItems)
			{
				wi.IsSelected = all;
			}
		}
		public List<T> GetSelection()
		{
			return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
		}
	}
}


