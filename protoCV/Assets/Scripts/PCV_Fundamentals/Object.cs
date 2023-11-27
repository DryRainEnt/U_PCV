using System;
using System.Collections;
using System.Collections.Generic;

namespace PCV_Fundamentals
{
	[Serializable]
	public class Object
	{
		public ulong oId;
		public string oName;

		public ulong[] iRelations;
	}

}