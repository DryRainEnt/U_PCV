using System;
using System.Collections.Generic;

namespace PCV_Fundamentals
{
	[Serializable]
	// ReSharper disable once InconsistentNaming
	public class PCV_Event : PCV_Object
	{
		public ulong iInfoNode;
		public ulong iSubject;
		
		public ulong startTime;
		public ulong endTime;
		public ulong duration => endTime - startTime;

		public Actant actant;
		
		public ulong iParentEvent;
		public ulong iChildEvent;
		public ulong iPrecedingEvent;
		public ulong iSucceedingEvent;
		
		public List<ulong> iStakeholders;

		public PCV_Event(string n) : base(n)
		{
			iInfoNode = new PCV_InfoNode(n + " InfoNode").oId;
			
			startTime = 0;
			endTime = 0;

			actant = new Actant(){iSubjectId = iSubject};
			
			iParentEvent = 0;
			iChildEvent = 0;
			iPrecedingEvent = 0;
			iSucceedingEvent = 0;
			
			iStakeholders = new List<ulong>();
		}
	}
}