using System.Collections.Generic;
using UnityEngine;

namespace PCV_Fundamentals
{
	public class DatabaseManager
	{
		public Dictionary<ulong, Object> objectCache = new Dictionary<ulong, Object>();
	}
}