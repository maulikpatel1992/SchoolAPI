using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class SecEnrollmentMgtConfiguration : IEntityTypeConfiguration<SecEnrollmentMgt>
    {
        public void Configure(EntityTypeBuilder<SecEnrollmentMgt> builder)
        {
            builder.HasData
            (
                new SecEnrollmentMgt
                {
                    Id = new Guid("1d490a70-94ce-4d15-9494-5248280c2ce1"),
                    StartDate = new DateTime(2020, 05,15 ),
                    EndDate = new DateTime(2020, 08, 15),
                    CreatedDate = new DateTime(2020, 06, 22),
                    UpdatedDate = new DateTime(2020, 06, 25),
                    UserId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce1"),
                    SecCourseId = new Guid("1d490a70-90ce-4d15-9494-5248280c2ce1")
                },
                new SecEnrollmentMgt
                {
                    Id = new Guid("2d490a70-94ce-4d15-9494-5248280c2ce1"),
                    StartDate = new DateTime(2020, 06, 18),
                    EndDate = new DateTime(2020, 08, 22),
                    CreatedDate = new DateTime(2020, 06, 2),
                    UpdatedDate = new DateTime(2020, 06, 5),
                    UserId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce6"),
                    SecCourseId = new Guid("0d490a70-94ce-4d15-9494-5248280c2ce1")
                },
                 new SecEnrollmentMgt
                 {
                     Id = new Guid("4d490a70-94ce-4d15-9494-5248280c2ce1"),
                     StartDate = new DateTime(2020, 06, 23),
                     EndDate = new DateTime(2020, 08, 29),
                     CreatedDate = new DateTime(2020, 06, 2),
                     UpdatedDate = new DateTime(2020, 06, 2),
                     UserId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce7"),
                     SecCourseId = new Guid("1d490a70-94ce-4d15-9494-5248280c2ce1")
                 }

            );
        }
    }
}
