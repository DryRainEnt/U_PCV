using PCV_Fundamentals;
using UnityEngine;

namespace PCV_Interfaces
{
	public class SubjectHeader : MonoBehaviour
	{
		public ulong targetId;

		public Subject target;
		
		public void Initiate(ulong id)
		{
			targetId = id;
			target = Program.DatabaseManager.objectCache[targetId] as Subject;
		}
	}
}