using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace Entities.Configuration
{
    class CourseSectionMgtConfiguration : IEntityTypeConfiguration<CourseSectionMgt>
    {
        public void Configure(EntityTypeBuilder<CourseSectionMgt> builder)
        {
            builder.HasData
            (
                new CourseSectionMgt
                {
                    Id = new Guid("1d490a70-90ce-4d15-9494-5248280c2ce1"),
                    StartDate = new DateTime(2020, 05, 15),
                    EndDate = new DateTime(2020, 08, 15),
                    CreatedDate = new DateTime(2020, 06, 22),
                    UpdatedDate = new DateTime(2020, 06, 25),
                    CourseId = new Guid("c9d4c053-49b6-410c-bc78-1d54a9991870")
                },
                new CourseSectionMgt
                {
                    Id = new Guid("0d490a70-94ce-4d15-9494-5248280c2ce1"),
                    StartDate = new DateTime(2020, 06, 18),
                    EndDate = new DateTime(2020, 08, 22),
                    CreatedDate = new DateTime(2020, 06, 2),
                    UpdatedDate = new DateTime(2020, 06, 5),
                    CourseId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                 new CourseSectionMgt
                 {
                     Id = new Guid("1d490a70-94ce-4d15-9494-5248280c2ce1"),
                     StartDate = new DateTime(2020, 06, 23),
                     EndDate = new DateTime(2020, 08, 29),
                     CreatedDate = new DateTime(2020, 06, 2),
                     UpdatedDate = new DateTime(2020, 06, 2),
                     CourseId = new Guid("c9d4c053-49b6-410c-bc78-3d54a9991870")
                 }

            );
        }
    }
}
