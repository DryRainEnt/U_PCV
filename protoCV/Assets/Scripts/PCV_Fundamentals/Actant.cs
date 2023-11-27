using System;

namespace PCV_Fundamentals
{
	[Serializable]
	public class Actant
	{
		public ulong iSubjectId;
		public ulong iSenderId;
		public ulong iReceiverId;
		public ulong iObjectiveId;
		public ulong iSupporterId;
		public ulong iOpponentId;
	}
}