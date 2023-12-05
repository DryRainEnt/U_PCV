namespace PCV_Fundamentals
{
	public delegate PCV_Subject OnSubjectCreatedDelegate(PCV_Subject subject);
	public delegate PCV_Subject OnSubjectDeletedDelegate(PCV_Subject subject);
	public delegate PCV_Subject OnSubjectListAddedDelegate(PCV_Subject subject);
	public delegate PCV_Subject OnSubjectListRemovedDelegate(PCV_Subject subject);
	public delegate int OnSubjectMovedDelegate(PCV_Subject subject, int oldIndex, int newIndex);
	
	public static class SubjectExtensions
	{
		
	}
}