using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasData
            (
                new Assignment
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb51a"),
                    AssignmentTitle = "Web mining-Generate web page",
                    Description = "Print hello world on web page",
                    CourseId = new Guid("c9d4c053-49b6-410c-bc78-1d54a9991870")
                },
                new Assignment
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                    AssignmentTitle = "Find Prime number from give data sets",
                    Description = "Double prime: two continuos prime",
                    CourseId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                 new Assignment
                 {
                     Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb53a"),
                     AssignmentTitle = "Pettern",
                     Description = "Print start shape using for loop",
                     CourseId = new Guid("c9d4c053-49b6-410c-bc78-3d54a9991870")
                 }
            );
        }
    }
}
