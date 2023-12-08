using PCV_Fundamentals;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PCV_Interfaces
{
    public class TimeMarker : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private EventStream eventStream;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_InputField timeField;
        
        public TimeTokenUnit unit;
        public TimeTokenSet timeToken;
        
        public void SyncTime(EventStream stream, bool isStart)
        {
            SetTime(isStart ? stream.header.targetEvent.startTime : stream.header.targetEvent.endTime);
        }
        
        public void SetPosition(ulong time)
        {
            var point = time / TimeLine.Instance.secondsPerUnit;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, point);
        }
        
        public void SetTime(ulong time)
        {
            timeToken = TimeLine.Instance.time.GetTimeToken(time);
            
            var txt = TimeLine.Instance.GetTimeString(timeToken.token, unit);
            timeText.text = txt;
            timeField.text = txt;
            SetPosition(timeToken.token);
        }
        
        public void SetUnit(TimeTokenUnit newUnit)
        {
            unit = newUnit;
            SetTime(timeToken.token);
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}
