using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        ISecEnrollmentMgtRepository SecEnrollmentMgt { get; }
        IAssignmentRepository Assignment { get; }
        ICourseMgtRepository CourseMgt { get; }
        ICourseSectionMgtRepository CourseSectionMgt { get; }
        ISecAssignmentMgtRepository SecAssignmentMgt { get; }
        Task SaveAsync();
        
    }
}
