using PCV_Fundamentals;
using TMPro;
using UnityEngine;

namespace PCV_Interfaces
{
	public class EventHeader : MonoBehaviour
	{
		public ulong iEvent;
		public PCV_Event targetEvent;

		[SerializeField] private RectTransform rectTransform;
		[SerializeField] private TMP_Text label ;
		
		public void Initiate(ulong eventID)
		{
			iEvent = eventID;
			targetEvent = DatabaseManager.objectCache[iEvent] as PCV_Event;

			if (targetEvent != null)
			{
				label.text = targetEvent.oName;
			}
		}
	}
}