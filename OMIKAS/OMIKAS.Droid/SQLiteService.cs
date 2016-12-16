using System;
using OMIKAS.Droid;
using Xamarin.Forms;
using System.IO;
using SQLite;

[assembly: Dependency(typeof(SQLiteService))]
namespace OMIKAS.Droid
{
	public class SQLiteService : ISQLite
	{
		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var filename = "OMIKAS.db3";
			var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentspath, filename);
			if(!File.Exists(path)) File.Create(path);

			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var conn = new SQLite.Net.SQLiteConnection(platform, path);
			return conn;
		}
	}
}