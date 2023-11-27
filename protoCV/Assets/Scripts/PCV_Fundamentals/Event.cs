namespace PCV_Fundamentals
{
	public class Event : Object
	{
		public ulong iInfoNode;
		
		public ulong startTime;
		public ulong endTime;

		public Actant actant;
		
		public ulong iParentEvent;
		public ulong iChildEvent;
		public ulong iPrecedingEvent;
		public ulong iSucceedingEvent;
		
		public ulong[] iStakeholders;
	}
}