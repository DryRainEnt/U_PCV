using System;
using System.Collections;
using System.Collections.Generic;

namespace PCV_Fundamentals
{
	[Serializable]
	// ReSharper disable once InconsistentNaming
	public class PCV_Object
	{
		public ulong oId;
		public string oName;

		public List<ulong> iRelations;

		public PCV_Object(string n)
		{
			oId = DatabaseManager.GenerateUniqueId();
			oName = n;
			iRelations = new List<ulong>();
			DatabaseManager.RegisterObject(this);
		}
		
		public void AddRelation(ulong i)
		{
			if (iRelations.Contains(i))
				return;
			
			iRelations.Add(i);
			DatabaseManager.objectCache[i].AddRelation(oId);
		}
		
		public void RemoveRelation(ulong i)
		{
			if (!iRelations.Contains(i))
				return;
			
			iRelations.Remove(i);
			DatabaseManager.objectCache[i].RemoveRelation(oId);
		}
	}

}