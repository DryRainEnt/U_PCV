using System;
using System.Collections.Generic;

namespace PCV_Fundamentals
{
	[Serializable]
	// ReSharper disable once InconsistentNaming
	public class PCV_Subject : PCV_Object
	{
		public ulong iInfoNode;
		public List<string> tags;
		public List<ulong> events;
		
		public PCV_Subject(string n) : base(n)
		{
			iInfoNode = new PCV_InfoNode(n + " InfoNode").oId;
			tags = new List<string>();
			events = new List<ulong>();
		}
	}
}