using PCV_Fundamentals;
using UnityEngine;

namespace PCV_Interfaces
{
	public class TimeLine : MonoBehaviour
	{
		public float heightMult = 1f;
		
		// 세계관에 따라 유동적으로 변할 수 있는 시간의 개념을 담아낼 수 있는 기준이 필요함
		public CustomTime time;

		public void Initiate()
		{
			// Test Code
			time = new CustomTime()
			{
				unit = new CustomTimeUnit()
				{
					regularSecondsPerMinute = 60,
					regularMinutesPerHour = 60,
					regularHoursPerDay = 24,
					regularDaysPerMonth = 30,
					regularMonthsPerYear = 12,
					regularYearsPerEra = 100,
					regularZeroPoint = 31526042400,
				},
				irregularDaysPerMonth = new ulong[]
				{
					31,
					28,
					31,
					30,
					31,
					30,
					31,
					31,
					30,
					31,
					30,
					31,
				},
				secondUnitName = "s",
				minuteUnitName = "m",
				hourUnitName = "h",
				dayUnitName = "D",
				monthUnitName = "M",
				yearUnitName = "Y",
				eraUnitName = "c",
			};
		}
	}
}