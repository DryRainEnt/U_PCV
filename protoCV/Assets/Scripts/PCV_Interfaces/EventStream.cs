using UnityEngine;
using PCV_Fundamentals;

namespace PCV_Interfaces
{
	public class EventStream : MonoBehaviour
	{
		public int indent;
		
		public ulong iEvent;
		public PCV_Fundamentals.Event targetEvent;
		public EventHeader header;
		
		public RectTransform rectTransform;
	}
}