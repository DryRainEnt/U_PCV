using UnityEngine;
using PCV_Fundamentals;
using UnityEngine.EventSystems;

namespace PCV_Interfaces
{
	public class EventStream : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
	{
		public int indent;
		
		public EventHeader header;
		
		public RectTransform rectTransform;
		
		public void LoadEvent(ulong iEvent)
		{
			header.Initiate(iEvent);
		}
		
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log($"Click on {header.iEvent}");
		}

		public void OnPointerMove(PointerEventData eventData)
		{
			Debug.Log($"Moving on {header.iEvent}");
		}
	}
}