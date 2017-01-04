using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using Xamarin.Forms;
namespace OMIKAS
{
	/// <summary>
	/// Operacje na bazie danych
	/// </summary>
	public class DataAccess
	{
		/// <summary>
		/// Połączenie z bazą danych
		/// </summary>
		private SQLiteConnection dbConn;

		/// <summary>
		/// Konstruktor - inicjalizuje wszystkie dane
		/// </summary>
		public DataAccess()
		{
			//W zależności od platformy inaczej wygląda połączenie z bazą SQLite
			dbConn = DependencyService.Get<ISQLite>().GetConnection();

			//Utwórz tabele w bazie
			dbConn.CreateTable<Alloy>();
			dbConn.CreateTable<Smelt>();
			dbConn.CreateTable<User>();
		}

		/// <summary>
		/// Pobiera liste stopow z SQLite
		/// </summary>
		/// <returns>Lista wszystkich stopow</returns>
		public List<Alloy> GetAllAlloys()
		{
			return dbConn.Query<Alloy>("Select * From [Alloy]");
		}

		public int SaveAlloy(Alloy alloy)
		{
			return dbConn.Insert(alloy);
		}

		public int DeleteAlloy(Alloy alloy)
		{
			return dbConn.Delete(alloy);
		}

		/// <summary>
		/// Pobiera liste wytopow z bazy
		/// </summary>
		/// <returns>Lista wszystkich wytopów</returns>
		public List<Smelt> GetAllSmelts()
		{
			return dbConn.Query<Smelt>("Select * From [Smelt]");
		}

		public int SaveSmelt(Smelt smelt)
		{
			return dbConn.Insert(smelt);
		}

		public int DeleteSmelt(Smelt smelt)
		{
			return dbConn.Delete(smelt);
		}

		/// <summary>
		/// Ile jest wytopów w bazie
		/// </summary>
		/// <returns>Ilość wytopów</returns>
		public int SmeltsCount()
		{
			var tb = dbConn.GetTableInfo("Smelt");
			return tb.Count;
		}

		/// <summary>
		/// Ile jest stopow w bazie?
		/// </summary>
		/// <returns>Ilosc stopów</returns>
		public int AlloysCount()
		{
			var tb = dbConn.GetTableInfo("Alloy");
			return tb.Count;
		}

		/// <summary>
		/// Ile jest użytkowników
		/// </summary>
		/// <returns>Lista użytkownikó</returns>
		public List<User> GetUser()
		{
			return dbConn.Query<User>("Select * From [User]");
		}

		public int SaveUser(User usr)
		{
			return dbConn.Insert(usr);
		}

		public int UpdateUser(User usr)
		{
			return dbConn.Update(usr);
		}
	}
}
