using System;
using System.IO;
using Xamarin.Forms;
using OMIKAS.iOS;

[assembly: Dependency(typeof(SQLiteService))]
namespace OMIKAS.iOS
{
	public class SQLiteService : ISQLite
	{
		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var filename = "OMIKAS.db3";
			string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); //Documents folder
			string libPath = Path.Combine(docPath, "..", "Library"); //Library folder
			var path = Path.Combine(libPath, filename);

			if(!File.Exists(path))
			{
				File.Create(path);
			}
			var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);

			// Return the database connection 
			return conn;
		}
	}
}
