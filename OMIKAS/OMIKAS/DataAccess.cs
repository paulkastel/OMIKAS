using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using Xamarin.Forms;
namespace OMIKAS
{
	public class DataAccess
	{
		private SQLiteConnection dbConn;

		public DataAccess()
		{
			dbConn = DependencyService.Get<ISQLite>().GetConnection();
			dbConn.CreateTable<Alloy>();
			dbConn.CreateTable<Smelt>();
			dbConn.CreateTable<User>();
		}

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
	}
}
