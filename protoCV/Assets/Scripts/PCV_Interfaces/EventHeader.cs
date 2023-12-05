using PCV_Fundamentals;
using UnityEngine;

namespace PCV_Interfaces
{
	public class EventHeader : MonoBehaviour
	{
		public ulong iEvent;
		public PCV_Fundamentals.PCV_Event targetEvent;

		public void Initiate(ulong iEvent)
		{
			this.iEvent = iEvent;
			targetEvent = DatabaseManager.objectCache[iEvent] as PCV_Fundamentals.PCV_Event;
		}
	}
}