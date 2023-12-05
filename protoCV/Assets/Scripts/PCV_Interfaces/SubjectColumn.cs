using System.Collections.Generic;
using PCV_Fundamentals;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PCV_Interfaces
{
	public class SubjectColumn : MonoBehaviour, IPointerClickHandler
	{
		public SubjectHeader header;
		public Transform eventStreamParent;
		public GameObject eventStreamPrefab;
		
		public List<EventStream> eventStreams = new List<EventStream>();
		
		private SelectMenu _selectMenu;
		
		public void Initiate(ulong id)
		{
			header.Initiate(id);
			foreach (var ev in header.target.events)
			{
				var newEventStream = Instantiate(eventStreamPrefab, eventStreamParent).GetComponent<EventStream>();
				newEventStream.LoadEvent(ev);
				AddEventStream(newEventStream);
			}
		}
		
		public void AddEventStream(EventStream eventStream)
		{
			eventStream.transform.SetParent(eventStreamParent);
			eventStreams.Add(eventStream);
		}


		private void Deselect(GameObject obj)
		{
			if (_selectMenu == null || !_selectMenu.isExclusive) return;
			
			Debug.Log($"Deselecting {header.targetId} column");

			Program.Instance.StopListeningForDeselect(Deselect);
			
			Destroy(_selectMenu.gameObject);
			_selectMenu = null;
		}
		
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				Debug.Log($"Right click on {header.targetId} column");
				_selectMenu = Program.Instance.CreateSelectMenu(Input.mousePosition, new SelectMenuCall("Add Event", 
					null, null,() =>
					{
						var newEvent = new PCV_Event($"E{header.targetId}-{eventStreams.Count}");
						DatabaseManager.RegisterObject(newEvent);
						var eStream = Instantiate(eventStreamPrefab, eventStreamParent).GetComponent<EventStream>();
						eStream.LoadEvent(newEvent.oId);
						
						Deselect(_selectMenu.gameObject);
					}),
					new SelectMenuCall("Empty", 
						null, null, null)
					);
				Program.Instance.ListenForDeselect(Deselect);
			}
			// if click point is out of bounds, destroy cached menu
			else
			{
				Program.Instance.OnDeselect(_selectMenu.gameObject);
			}
		}
	}
}