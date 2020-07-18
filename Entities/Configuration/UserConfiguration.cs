using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                new User
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce1"),
                    Email = "mku46@njit.edu",
                    Password = "mkuqwer",
                    Status = "Active",
                    SystemRoleId="Teacher",
                    CreatedDate = new DateTime(2020,06,22),
                    UpdatedDate = new DateTime(2020, 06, 25)
                },
                new User
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce6"),
                    Email = "kru64@njit.edu",
                    Password = "asfsa",
                    Status = "hold",
                    SystemRoleId = "Admin",
                    CreatedDate = new DateTime(2020, 06, 15),
                    UpdatedDate = new DateTime(2020, 06, 27)
                },
                 new User
                 {
                     Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce7"),
                     Email = "adfr89@njit.edu",
                     Password = "mgffrr",
                     Status = "Active",
                     SystemRoleId = "Student",
                     CreatedDate = new DateTime(2020, 06, 07),
                     UpdatedDate = new DateTime(2020, 06, 28)
                 }
            );
        }
    }
}
