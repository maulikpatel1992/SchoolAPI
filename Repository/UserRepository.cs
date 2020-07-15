using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        
        public IEnumerable<User> GetAllUsers(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .ToList();

        public User GetUser(Guid userId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(userId), trackChanges)
            .SingleOrDefault();

        public void CreateUser(User user) => Create(user);

        public IEnumerable<User> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => 
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

        public void DeleteUser(User user) 
        { 
            Delete(user); 
        }
    }
}
