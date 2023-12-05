using System;

namespace PCV_Fundamentals
{
	[Serializable]
	
	// ReSharper disable once InconsistentNaming
	public class PCV_InfoNode : PCV_Object
	{
		// Gonna be a string for now, but will be a more complex text visualization later, such as md or html
		public string content;
		
		public PCV_InfoNode(string n) : base(n)
		{
			content = n + " New Description";
		}
	}
}