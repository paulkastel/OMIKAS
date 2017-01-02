using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace OMIKAS
{
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public User()
		{
		}
		public string user_surname { get; set; }
		public string user_name { get; set; }
		public string emailadd { get; set; }
	}
}
