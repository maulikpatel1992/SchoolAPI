using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class CourseMgtConfiguration : IEntityTypeConfiguration<CourseMgt>
    {
        public void Configure(EntityTypeBuilder<CourseMgt> builder)
        {
            builder.HasData
            (
                new CourseMgt
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-1d54a9991870"),
                    CourseTitle = "Web mining",
                    Description = "learn more about laravel, HTML, CSS",
                    CreatedDate = new DateTime(2020, 06, 22),
                    UpdatedDate = new DateTime(2020, 06, 25)
                },
                new CourseMgt
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    CourseTitle = "Data Analytics",
                    Description = "Data analysis using R and Python",
                    CreatedDate = new DateTime(2020, 06, 22),
                    UpdatedDate = new DateTime(2020, 06, 25)
                },
                 new CourseMgt
                 {
                     Id = new Guid("c9d4c053-49b6-410c-bc78-3d54a9991870"),
                     CourseTitle = "Java Programming",
                     Description = "Servlet, JSP, HTML, CSS",
                     CreatedDate = new DateTime(2020, 06, 22),
                     UpdatedDate = new DateTime(2020, 06, 25)
                 }
            );
        }
    }
}
