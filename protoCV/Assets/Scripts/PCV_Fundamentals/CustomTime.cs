using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace PCV_Fundamentals
{
	[Flags]
	public enum TimeTokenUnit
	{
		None = 0x00,
		Era = 0x01,
		Year = 0x02,
		Month = 0x04,
		Day = 0x08,
		Hour = 0x10,
		Minute = 0x20,
		Second = 0x40,
	}

	public struct TimePiece
	{
		public TimeTokenUnit currentUnit;
		public TimeTokenUnit subUnit;

		// For every...
		public TimeTokenUnit frequencyUnit;
		// n-th ...of this unit...
		public ulong frequency;
		
		// follows this table.
		public ulong[] irregularTable;
	}
	
	[Serializable]
	public struct TimeTokenSet
	{
		public ulong token;

		public ulong second;
		public ulong minute;
		public ulong hour;
		public ulong day;
		public ulong month;
		public ulong year;
		public ulong era;
		
		public static TimeTokenSet operator ++(TimeTokenSet t)
		{
			t.token++;
			return t;
		}
		
	}
	
	[Serializable]
	public class CustomTime
	{
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

		public ulong GetTimeValue(TimeTokenSet token)
		{
			ulong totalDays = 0;

			// 연도별 날짜 계산
			for (ulong y = 1; y < token.year; y++)
			{
				totalDays += IsLeapYear(y) ? (ulong)366 : (ulong)365;
			}

			// 월별 날짜 계산
			ulong[] daysInMonth = { 31, IsLeapYear(token.year) ? (ulong)29 : (ulong)28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
			for (ulong m = 1; m < token.month; m++)
			{
				totalDays += daysInMonth[m - 1];
			}

			// 현재 월의 날짜 추가
			totalDays += token.day - 1;

			// 총 초 계산
			ulong totalSeconds = totalDays * 24 * 60 * 60; // 일 -> 초
			totalSeconds += token.hour * 60 * 60; // 시 -> 초
			totalSeconds += token.minute * 60; // 분 -> 초
			totalSeconds += token.second; // 초

			return totalSeconds;
		}

		private static bool IsLeapYear(ulong year)
		{
			return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
		}
		
		public TimeTokenSet GetTimeToken(ulong token)
		{
			TimeTokenSet timeToken = new TimeTokenSet();
			var totalSeconds = token;
			timeToken.token = token;

			// 초, 분, 시간 계산
			timeToken.second = totalSeconds % 60;
			ulong totalMinutes = totalSeconds / 60;
			timeToken.minute = totalMinutes % 60;
			ulong totalHours = totalMinutes / 60;
			timeToken.hour = totalHours % 24;

			// 일 계산
			ulong totalDays = totalHours / 24;

			// 연도 계산
			ulong year = 1;
			while (totalDays >= (ulong)(IsLeapYear((ulong)year) ? 366 : 365))
			{
				totalDays -= (ulong)(IsLeapYear((ulong)year) ? 366 : 365);
				year++;
			}
			timeToken.year = year;

			// 월 계산
			ulong month = 1;
			ulong[] daysInMonth = { (ulong)31, IsLeapYear((ulong)year) ? (ulong)29 : (ulong)28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
			while (totalDays >= (ulong)daysInMonth[month - 1])
			{
				totalDays -= (ulong)daysInMonth[month - 1];
				month++;
			}
			timeToken.month = month;

			// 남은 일 계산
			timeToken.day = totalDays + 1; // 일은 1부터 시작

			return timeToken;
		}
	}
}