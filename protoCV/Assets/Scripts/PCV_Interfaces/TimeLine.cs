using System;
using System.Collections.Generic;
using PCV_Fundamentals;
using UnityEngine;

namespace PCV_Interfaces
{
	public enum TimeLineType
	{
		Regular,
		Irregular,
	}
	
	public class TimeLine : Singleton<TimeLine>
	{
		public float secondsPerUnit = 10f;
		
		// 세계관에 따라 유동적으로 변할 수 있는 시간의 개념을 담아낼 수 있는 기준이 필요함
		public CustomTime time;

		[SerializeField] private RectTransform timeLineArea;
		[SerializeField] private GameObject timeMarkerPrefab;

		public ulong minTime = ulong.MaxValue;
		public ulong maxTime = ulong.MinValue;
		
		public float TimeLineHeight => timeLineArea.sizeDelta.y;
		public float GetTimelinePosition(ulong t)
		{
			return (t - minTime) / secondsPerUnit;
		}

		public void Initiate()
		{
			time = new CustomTime();
		}

		public TimeMarker CreateTimeMarker(EventStream stream, bool isStart)
		{
			var go = Instantiate(timeMarkerPrefab, timeLineArea);
			var marker = go.GetComponent<TimeMarker>();
			
			marker.SyncTime(stream, isStart);

			return marker;
		}
		
		public (TimeMarker, TimeMarker) CreateTimeMarkerSet(EventStream stream)
		{
			return (CreateTimeMarker(stream, true), CreateTimeMarker(stream, false));
		}
		
		public void UpdateTimeRange(ulong t)
		{
			if (t < minTime)
			{
				minTime = t;
			}
			if (t > maxTime)
			{
				maxTime = t;
			}
		}
		
		public void UpdateTimeRange(ulong min, ulong max)
		{
			minTime = min;
			maxTime = max;
		}

		public void UpdateTimeRange()
		{
			timeLineArea.sizeDelta = new Vector2(timeLineArea.sizeDelta.x, (maxTime - minTime) / secondsPerUnit);
		}

		private string GetTimeUnitString(TimeTokenSet t, TimeTokenUnit unit)
		{
			string result = "";
			
			// 특정 시간 토큰을 파싱하여 지정 단위의 문자열로 반환하는 함수
			switch (unit)
			{
				case TimeTokenUnit.Era:
					result = $"{t.era}{time.eraUnitName} ";
					break;
				case TimeTokenUnit.Year:
					result = $"{t.year}{time.yearUnitName} ";
					break;
				case TimeTokenUnit.Month:
					result = $"{t.month}{time.monthUnitName} ";
					break;
				case TimeTokenUnit.Day:
					result = $"{t.day}{time.dayUnitName} ";
					break;
				case TimeTokenUnit.Hour:
					result = $"{t.hour}{time.hourUnitName} ";
					break;
				case TimeTokenUnit.Minute:
					result = $"{t.minute}{time.minuteUnitName} ";
					break;
				case TimeTokenUnit.Second:
					result = $"{t.second}{time.secondUnitName} ";
					break;
			}
			
			return result;
		}

		public string GetTimeString(ulong t, TimeTokenUnit unitUse)
		{
			string result = "";
			TimeTokenSet timeToken = time.GetTimeToken(t);

			// 특정 시간 토큰을 파싱하여 플래그로 포함된 단위 조합의 문자열로 반환하는 함수
			// unitUse에 포함된 단위만 반환함
			if (unitUse.HasFlag(TimeTokenUnit.Era))
			{
				result += GetTimeUnitString(timeToken, TimeTokenUnit.Era);
			}
			if (unitUse.HasFlag(TimeTokenUnit.Year))
			{
				result += GetTimeUnitString(timeToken, TimeTokenUnit.Year);
			}
			if (unitUse.HasFlag(TimeTokenUnit.Month))
			{
				result += GetTimeUnitString(timeToken, TimeTokenUnit.Month);
			}
			if (unitUse.HasFlag(TimeTokenUnit.Day))
			{
				result += GetTimeUnitString(timeToken, TimeTokenUnit.Day);
			}
			if (unitUse.HasFlag(TimeTokenUnit.Hour))
			{
				result += GetTimeUnitString(timeToken, TimeTokenUnit.Hour);
			}
			if (unitUse.HasFlag(TimeTokenUnit.Minute))
			{
				result += GetTimeUnitString(timeToken, TimeTokenUnit.Minute);
			}
			if (unitUse.HasFlag(TimeTokenUnit.Second))
			{
				result += GetTimeUnitString(timeToken, TimeTokenUnit.Second);
			}
			
			return result;
		}
	}
}