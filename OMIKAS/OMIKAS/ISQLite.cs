using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace OMIKAS
{
	/// <summary>
	/// Interfejs do tworzenia połaczeń z bazą SQLite
	/// </summary>
	public interface ISQLite
	{
		/// <summary>
		/// Tworzy bazę SQLite na telefonie
		/// </summary>
		/// <returns></returns>
		SQLiteConnection GetConnection();
	}
}
