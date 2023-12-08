using PCV_Fundamentals;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PCV_Interfaces
{
	public class SubjectHeader : MonoBehaviour, IPointerClickHandler
	{
		public ulong targetId;

		public PCV_Subject target;

		[SerializeField]
		private TMP_Text label;
		
		private SelectMenu _selectMenu;
		
		public void Initiate(ulong id)
		{
			targetId = id;
			target = DatabaseManager.objectCache[targetId] as PCV_Subject;
			if (target != null) label.text = target.oName;
		}

		private void Deselect(GameObject obj)
		{
			if (_selectMenu == null || !_selectMenu.isExclusive) return;
			
			Debug.Log($"Deselecting {targetId} header");

			Program.Instance.StopListeningForDeselect(Deselect);
			
			Destroy(_selectMenu.gameObject);
			_selectMenu = null;
		}
		
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				Debug.Log($"Right click on {targetId} header");
				_selectMenu = Program.Instance.CreateSelectMenu(Input.mousePosition, new SelectMenuCall("View subject details", 
					null, null,() =>
					{
						Program.Instance.GenerateInfoBox(target.oId, target.iInfoNode);
						Deselect(_selectMenu.gameObject);
					}));
				Program.Instance.ListenForDeselect(Deselect);
			}
			// if click point is out of bounds, destroy cached menu
			else if (_selectMenu)
			{
				Program.Instance.OnDeselect(_selectMenu.gameObject);
			}
		}
	}
}