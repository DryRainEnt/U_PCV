using System;
using System.Collections.Generic;
using System.Linq;

namespace PCV_Fundamentals
{
	public static class DatabaseManager
	{
		public static Dictionary<ulong, PCV_Object> objectCache = new Dictionary<ulong, PCV_Object>();
		private static Random _random = new Random();
		
		public static ulong[] GetSubjectQuery()
		{
			var result = objectCache.Where(x => x.Value is PCV_Subject).Select(x => x.Key);

			return result.ToArray();
		}
		
		public static ulong[] GetEventQuery()
		{
			var result = objectCache.Where(x => x.Value is PCV_Event).Select(x => x.Key);

			return result.ToArray();
		}

		public static (ulong, ulong) GetMinMaxTime()
		{
			var events = GetEventQuery();
			
			var min = ulong.MaxValue;
			var max = ulong.MinValue;

			foreach (var ev in events)
			{
				min = Math.Min(min, ((PCV_Event)objectCache[ev]).startTime);
				max = Math.Max(max, ((PCV_Event)objectCache[ev]).endTime);
			}

			return (min, max);
		}
		
		public static ulong[] GetEventQuery(ulong subjectId)
		{
			var result = objectCache.Where(x
					=> (x.Value is PCV_Event @event) && @event.iSubject == subjectId)
				.Select(x => x.Key);

			return result.ToArray();
		}
		
		public static ulong[] GetInfoNodeQuery()
		{
			var result = objectCache.Where(x => x.Value is PCV_InfoNode).Select(x => x.Key);

			return result.ToArray();
		}
		
		public static void RegisterObject(PCV_Object obj)
		{
			objectCache.Add(obj.oId, obj);
			Program.Instance.OnDatabaseModified();
		}
		
		public static void UnregisterObject(ulong id)
		{
			objectCache.Remove(id);
		}

		public static ulong GenerateUniqueId()
		{
			byte[] buffer = new byte[8];
			_random.NextBytes(buffer);
			ulong id = BitConverter.ToUInt64(buffer, 0);

			while (objectCache.ContainsKey(id) || id == 0)
			{
				id++;
			}

			return id;
		}
	}
}