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
		
		private PCV_ObjectTypeEnum _typeEnum;

		public Image image;
		public TMP_Text nameLabel;
		public TMP_Text descriptionLabel;
		
		public TMP_InputField nameInput;
		public TMP_InputField descriptionInput;
		
		public RectTransform Transform;
		public Vector2 Position => Transform.position;

		[SerializeField] private Button handle;
		[SerializeField] private Button closeButton;
		[SerializeField] private TMP_InputField startField;
		[SerializeField] private TMP_InputField endField;
		
		private PCV_Object _targetObject;
		private PCV_InfoNode _targetInfoNode;

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
			
			DatabaseManager.objectCache.TryGetValue(io, out _targetObject);
			DatabaseManager.objectCache.TryGetValue(ii, out var targetInfoNode);
			
			if (targetInfoNode is PCV_InfoNode infoNode)
			{
				_targetInfoNode = infoNode;
				descriptionInput.text = infoNode.content;
				descriptionInput.onValueChanged.AddListener((s) => OnContentUpdated());
			}
			if (_targetObject != null)
			{
				nameInput.text = _targetObject.oName;
				nameInput.onValueChanged.AddListener((s) => OnContentUpdated());
			
				switch (_targetObject)
				{
					case PCV_Subject subject:
						_typeEnum = PCV_ObjectTypeEnum.Subject;
						break;
					case PCV_Event ev:
						_typeEnum = PCV_ObjectTypeEnum.Event;
						
						_startToken = TimeLine.Instance.time.GetTimeToken(ev.startTime);
						_endToken = TimeLine.Instance.time.GetTimeToken(ev.endTime);
						startField.text = _startToken.token.ToString();
						endField.text = _endToken.token.ToString();
						startField.onValueChanged.AddListener((s) => OnContentUpdated());
						endField.onValueChanged.AddListener((s) => OnContentUpdated());
						break;
					case PCV_InfoNode info:
						_typeEnum = PCV_ObjectTypeEnum.InfoNode;
						break;
					default:
						_typeEnum = PCV_ObjectTypeEnum.Object;
						break;
				}
				
				_typeEnum = _targetObject switch
				{
					PCV_Subject _ => PCV_ObjectTypeEnum.Subject,
					PCV_Event _ => PCV_ObjectTypeEnum.Event,
					PCV_InfoNode _ => PCV_ObjectTypeEnum.InfoNode,
					_ => PCV_ObjectTypeEnum.Object
				};
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

		private void OnContentUpdated()
		{
			_targetInfoNode.content = descriptionInput.text;
			_targetObject.oName = nameInput.text;

			switch (_typeEnum)
			{
				case PCV_ObjectTypeEnum.Subject:
					break;
				case PCV_ObjectTypeEnum.Event:
					if (_targetObject is PCV_Event ev)
					{
						ev.startTime = StartToken.token;
						ev.endTime = EndToken.token;
					}
					break;
				case PCV_ObjectTypeEnum.InfoNode:
					break;
				default:
					break;
			}
			
			var (min, max) = DatabaseManager.GetMinMaxTime();
			TimeLine.Instance.UpdateTimeRange(min, max);
			
			Program.Instance.OnDatabaseModified();
		}

		private void Dispose()
		{
			OnContentUpdated();
			Destroy(gameObject);
		}

		private void LockToCursor()
		{
			_isLocked = true;
			_offset = Position - Cursor.Instance.Position;
		}

		private void UnlockFromCursor()
		{
			_isLocked = false;
			_offset = Vector2.zero;
		}
	}
}