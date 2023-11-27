namespace PCV_Fundamentals
{
	public delegate Subject OnSubjectCreatedDelegate(Subject subject);
	public delegate Subject OnSubjectDeletedDelegate(Subject subject);
	public delegate Subject OnSubjectListAddedDelegate(Subject subject);
	public delegate Subject OnSubjectListRemovedDelegate(Subject subject);
	public delegate int OnSubjectMovedDelegate(Subject subject, int oldIndex, int newIndex);
	
	public static class SubjectExtensions
	{
		
	}
}