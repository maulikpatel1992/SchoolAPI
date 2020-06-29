using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class SecAssignmentMgtConfiguration : IEntityTypeConfiguration<SecAssignmentMgt>
    {
        public void Configure(EntityTypeBuilder<SecAssignmentMgt> builder)
        {
            builder.HasData
            (
                new SecAssignmentMgt
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5240280c2ca1"),
                    Score = 90,
                    SubmissionText = "Created home page",
                    CreatedDate = new DateTime(2020, 06, 22),
                    UpdatedDate = new DateTime(2020, 06, 25),
                    AssignmentId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb51a"),
                    UserId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce1")
                },
                new SecAssignmentMgt
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5241280c2cb6"),
                    Score = 95,
                    SubmissionText = "Found 25 prime from given dataset.",
                    CreatedDate = new DateTime(2020, 06, 22),
                    UpdatedDate = new DateTime(2020, 06, 25),
                    AssignmentId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                    UserId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce6")
                },
                 new SecAssignmentMgt
                 {
                     Id = new Guid("3d490a70-94ce-4d15-9494-5242280c2cc1"),
                     Score = 100,
                     SubmissionText = "Created pattern",
                     CreatedDate = new DateTime(2020, 06, 22),
                     UpdatedDate = new DateTime(2020, 06, 25),
                     AssignmentId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb53a"),
                     UserId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce7")
                 }
            );
        }
    }
}
