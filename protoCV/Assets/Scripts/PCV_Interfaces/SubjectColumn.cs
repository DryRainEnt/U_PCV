using System.Collections.Generic;
using UnityEngine;

namespace PCV_Interfaces
{
	public class SubjectColumn : MonoBehaviour
	{
		public SubjectHeader header;
		
		public List<EventStream> eventStreams = new List<EventStream>();
	}
}