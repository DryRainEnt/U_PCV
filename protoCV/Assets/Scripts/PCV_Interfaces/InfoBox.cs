using System;
using PCV_Fundamentals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCV_Interfaces
{
	public class InfoBox : MonoBehaviour
	{
		public ulong iObject;
		public ulong iInfo;

		public Image Image;
		public TMP_Text NameLabel;
		public TMP_Text DescriptionLabel;
		public RectTransform Transform;
		public Vector2 Position => Transform.position;

		[SerializeField] private Button handle;
		[SerializeField] private Button closeButton;
		[SerializeField] private TMP_InputField startField;
		[SerializeField] private TMP_InputField endField;

		private TimeTokenSet _startToken;
		public TimeTokenSet StartToken
		{
			get
			{
				var isValid = ulong.TryParse(startField.text, out var value);
				if (isValid)
					return _startToken = TimeLine.Instance.time.GetTimeToken(value);
				return _startToken;
			}
		}

		private TimeTokenSet _endToken;
		public TimeTokenSet EndToken
		{
			get
			{
				var isValid = ulong.TryParse(endField.text, out var value) && value > StartToken.token;
				if (isValid)
					return _endToken = TimeLine.Instance.time.GetTimeToken(value);
				return _endToken;
			}
		}
		
		private bool _isLocked;
		private Vector2 _offset;
		
		public void LoadInfo(ulong io, ulong ii)
		{
			iObject = io;
			iInfo = ii;
			
			DatabaseManager.objectCache.TryGetValue(io, out var targetObject);
			DatabaseManager.objectCache.TryGetValue(ii, out var targetInfo);
			
			if (targetInfo is PCV_InfoNode infoNode)
			{
				DescriptionLabel.text = infoNode.content;
			}
			if (targetObject != null)
			{
				NameLabel.text = targetObject.oName;
			}
			
			handle = transform.Find("Handle").GetComponent<Button>();
			closeButton = transform.Find("Close").GetComponent<Button>();
			
			handle.onClick.AddListener(() =>
			{
				if (_isLocked)
					UnlockFromCursor();
				else
					LockToCursor();
			});
			closeButton.onClick.AddListener(Dispose);
		}

		private void Update()
		{
			if (_isLocked)
				transform.position = Cursor.Instance.Position + _offset;
		}

		private void Dispose()
		{
			Destroy(gameObject);
		}

		private void LockToCursor()
		{
			_isLocked = true;
			_offset = Cursor.Instance.Position - Position;
		}

		private void UnlockFromCursor()
		{
			_isLocked = false;
			_offset = Vector2.zero;
		}
	}
}