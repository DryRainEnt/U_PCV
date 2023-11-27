using System;

namespace PCV_Fundamentals
{
	[Serializable]
	public class CustomTimeUnit
	{
		public ulong regularSecondsPerMinute;
		public ulong regularMinutesPerHour;
		public ulong regularHoursPerDay;
		public ulong regularDaysPerMonth;
		public ulong regularMonthsPerYear;
		public ulong regularYearsPerEra;
		public ulong regularZeroPoint;
	}
	
	[Serializable]
	public class CustomTime
	{
		public CustomTimeUnit unit;
		
		public ulong[] irregularSecondsPerMinute;
		public ulong[] irregularMinutesPerHour;
		public ulong[] irregularHoursPerDay;
		public ulong[] irregularDaysPerMonth;
		public ulong[] irregularMonthsPerYear;
		public ulong[] irregularYearsPerEra;

		public ulong secondUnitInfo;
		public ulong minuteUnitInfo;
		public ulong hourUnitInfo;
		public ulong dayUnitInfo;
		public ulong monthUnitInfo;
		public ulong yearUnitInfo;
		public ulong eraUnitInfo;

		public string secondUnitName;
		public string minuteUnitName;
		public string hourUnitName;
		public string dayUnitName;
		public string monthUnitName;
		public string yearUnitName;
		public string eraUnitName;
	}
}