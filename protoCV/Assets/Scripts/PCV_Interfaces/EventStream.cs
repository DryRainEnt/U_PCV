using UnityEngine;
using PCV_Fundamentals;
using UnityEngine.EventSystems;

namespace PCV_Interfaces
{
	public class EventStream : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public ulong targetId;
		public int indent;
		
		public EventHeader header;
		
		public RectTransform rectTransform;
		
		private SelectMenu _selectMenu;
		
		private TimeMarker _startMarker;
		private TimeMarker _endMarker;

		private bool _isSelected;

		public void Initiate(ulong iEvent)
		{
			targetId = iEvent;
			LoadEvent();
			
			
			Program.Instance.ListenForModification(LoadEvent);
		}
		
		public void LoadEvent()
		{
			header.Initiate(targetId);
			SetPosition(header.targetEvent.startTime);
			SetLength(header.targetEvent.duration);
		}

		private void OnDestroy()
		{
			Program.Instance.StopListeningForModification(LoadEvent);
		}
		
		public void SetLength(ulong period)
		{
			var length = period * TimeLine.Instance.secondsPerUnit;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, length);
		}
		
		public void SetPosition(ulong start)
		{
			var point = start * TimeLine.Instance.secondsPerUnit;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, point);
		}
		

		private void Deselect(GameObject obj)
		{
			if (_selectMenu == null || !_selectMenu.isExclusive) return;
			
			Debug.Log($"Deselecting {header.iEvent} header");

			Program.Instance.StopListeningForDeselect(Deselect);
			
			Destroy(_selectMenu.gameObject);
			_selectMenu = null;
		}
		
		private void Deactivate(GameObject obj)
		{
			_isSelected = false;
			Program.Instance.StopListeningForDeselect(Deactivate);
			
			Destroy(_startMarker.gameObject);
			Destroy(_endMarker.gameObject);
			_startMarker = null;
			_endMarker = null;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				Program.Instance.OnDeselect(gameObject);
				
				(_startMarker, _endMarker) = TimeLine.Instance.CreateTimeMarkerSet(this);
				_isSelected = true;
				Program.Instance.ListenForDeselect(Deactivate);
			}
			else if (eventData.button == PointerEventData.InputButton.Right)
			{
				Debug.Log($"Right click on {header.iEvent} column");
				_selectMenu = Program.Instance.CreateSelectMenu(Input.mousePosition, new SelectMenuCall("View Event Details", 
						null, null,() =>
						{
							Program.Instance.GenerateInfoBox(header.targetEvent.oId, header.targetEvent.iInfoNode);
							Deselect(_selectMenu.gameObject);
						})
				);
				Program.Instance.ListenForDeselect(Deselect);
			}
			// if click point is out of bounds, destroy cached menu
			else if (_selectMenu)
			{
				Program.Instance.OnDeselect(_selectMenu.gameObject);
			}
		}

		public void OnPointerMove(PointerEventData eventData)
		{
			// Debug.Log($"Moving on {header.iEvent}");
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log($"Enter on {header.iEvent}");
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Debug.Log($"Exit on {header.iEvent}");
		}
	}
}