using System;
using OMIKAS.Droid;
using Xamarin.Forms;
using System.IO;
using SQLite;

[assembly: Dependency(typeof(SQLiteService))]
namespace OMIKAS.Droid
{
	/// <summary>
	/// Tworzy baze SQLite w systemie Android
	/// </summary>
	public class SQLiteService : ISQLite
	{
		/// <summary>
		/// Tworzy plik SQLite na urz¹dzeniu
		/// </summary>
		/// <returns>Polaczenie z tym plikiem</returns>
		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var filename = "OMIKAS.db3";
			var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentspath, filename);
			if(!File.Exists(path))
				File.Create(path);

			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var conn = new SQLite.Net.SQLiteConnection(platform, path);
			return conn;
		}
	}
}