using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace OMIKAS
{
	/// <summary>
	/// Info o uzytkowniku
	/// </summary>
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		/// <summary>
		/// Nazwisko uzytkownika
		/// </summary>
		public string user_surname { get; set; }

		/// <summary>
		/// Imie uzytkownika
		/// </summary>
		public string user_name { get; set; }

		/// <summary>
		/// Adres e-mail uzytkownika
		/// </summary>
		public string emailadd { get; set; }
	}
}
