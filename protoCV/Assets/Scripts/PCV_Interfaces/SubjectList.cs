using System.Collections.Generic;
using UnityEngine;
using PCV_Fundamentals;

namespace PCV_Interfaces
{
	public class SubjectList : Singleton<SubjectList>
	{
        public List<PCV_Subject> Subjects = new List<PCV_Subject>();
        public GameObject subjectColumnPrefab;

		public float widthMult = 1f;
		
		public int SubjectCount => Subjects.Count;
		
		public async void OnAddSubjectButton()
		{
			PCV_Subject newSubject = await ObjectGenerator.Instance.GenerateObject(GenerateType.Subject) as PCV_Subject;
			
			if (newSubject == null)
			{
				Debug.LogWarning("Subject generation failed!");
				return;
			}
			
			var subject = Instantiate(subjectColumnPrefab, transform).GetComponent<SubjectColumn>();
			Subjects.Add(newSubject);
			subject.Initiate(newSubject.oId);
		}
	}
}