using System.Collections.Generic;
using UnityEngine;

namespace PCV_Fundamentals
{
	public class SubjectList : MonoBehaviour
	{
		public List<Subject> Subjects = new List<Subject>();

		public float widthMult = 1f;
		
		public int SubjectCount => Subjects.Count;
		
		
	}
}