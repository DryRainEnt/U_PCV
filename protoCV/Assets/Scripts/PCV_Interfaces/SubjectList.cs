using System.Collections.Generic;
using UnityEngine;
using PCV_Fundamentals;

namespace PCV_Interfaces
{
	public class SubjectList : MonoBehaviour
	{
		public List<Subject> Subjects = new List<Subject>();

		public float widthMult = 1f;
		
		public int SubjectCount => Subjects.Count;
		
		
	}
}