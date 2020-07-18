using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
     
        IEnumerable<User> GetAllUsers(bool trackChanges);

        User GetUser(Guid userId, bool trackChanges);
        void CreateUser(User user);
        IEnumerable<User> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteUser(User user);
    }
}
