using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ISecEnrollmentMgtRepository _secEnrollmentMgtRepository;
        private IUserRepository _userRepository;
        private IAssignmentRepository _assignmentRepository;
        private ICourseMgtRepository _coursemgtRepository;
        private ICourseSectionMgtRepository _coursesectionmgtRepository;
        private ISecAssignmentMgtRepository _secassignmentRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);

                return _userRepository;
            }
        }

        public ISecEnrollmentMgtRepository SecEnrollmentMgt
        {
            get
            {
                if (_secEnrollmentMgtRepository == null)
                    _secEnrollmentMgtRepository = new SecEnrollmentMgtRepository(_repositoryContext);

                return _secEnrollmentMgtRepository;
            }
        }

        public IAssignmentRepository Assignment
        {
            get
            {
                if (_assignmentRepository == null)
                    _assignmentRepository = new AssignmentRepository(_repositoryContext);

                return _assignmentRepository;
            }
        }

        public ICourseMgtRepository CourseMgt
        {
            get
            {
                if (_coursemgtRepository == null)
                    _coursemgtRepository = new CourseMgtRepository(_repositoryContext);

                return _coursemgtRepository;
            }
        }

        public ICourseSectionMgtRepository CourseSectionMgt
        {
            get
            {
                if (_coursesectionmgtRepository == null)
                    _coursesectionmgtRepository = new CourseSectionMgtRepository(_repositoryContext);

                return _coursesectionmgtRepository;
            }
        }

        public ISecAssignmentMgtRepository SecAssignmentMgt
        {
            get
            {
                if (_secassignmentRepository == null)
                    _secassignmentRepository = new SecAssignmentMgtRepository(_repositoryContext);

                return _secassignmentRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}