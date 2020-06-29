using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolAPI.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseMgt",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    CourseTitle = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMgt", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    Password = table.Column<string>(maxLength: 10, nullable: false),
                    Status = table.Column<string>(nullable: true),
                    SystemRoleId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(nullable: false),
                    AssignmentTitle = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignment_CourseMgt_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseMgt",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseSectionMgt",
                columns: table => new
                {
                    SecCourseId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSectionMgt", x => x.SecCourseId);
                    table.ForeignKey(
                        name: "FK_CourseSectionMgt_CourseMgt_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseMgt",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecAssignmentMgt",
                columns: table => new
                {
                    SectionAId = table.Column<Guid>(nullable: false),
                    SubmissionText = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    AssignmentId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecAssignmentMgt", x => x.SectionAId);
                    table.ForeignKey(
                        name: "FK_SecAssignmentMgt_Assignment_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignment",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecAssignmentMgt_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecEnrollmentMgt",
                columns: table => new
                {
                    SecEnrolId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    SecCourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecEnrollmentMgt", x => x.SecEnrolId);
                    table.ForeignKey(
                        name: "FK_SecEnrollmentMgt_CourseSectionMgt_SecCourseId",
                        column: x => x.SecCourseId,
                        principalTable: "CourseSectionMgt",
                        principalColumn: "SecCourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecEnrollmentMgt_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseMgt",
                columns: new[] { "CourseId", "CourseTitle", "CreatedDate", "Description", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-1d54a9991870"), "Web mining", new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "learn more about laravel, HTML, CSS", new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Data Analytics", new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Data analysis using R and Python", new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9d4c053-49b6-410c-bc78-3d54a9991870"), "Java Programming", new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Servlet, JSP, HTML, CSS", new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedDate", "Email", "Password", "Status", "SystemRoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce1"), new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "mku46@njit.edu", "mkuqwer", "Active", "Teacher", new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce6"), new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "kru64@njit.edu", "asfsa", "hold", "Admin", new DateTime(2020, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce7"), new DateTime(2020, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "adfr89@njit.edu", "mgffrr", "Active", "Student", new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Assignment",
                columns: new[] { "AssignmentId", "AssignmentTitle", "CourseId", "Description" },
                values: new object[,]
                {
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb51a"), "Web mining-Generate web page", new Guid("c9d4c053-49b6-410c-bc78-1d54a9991870"), "Print hello world on web page" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Find Prime number from give data sets", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Double prime: two continuos prime" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb53a"), "Pettern", new Guid("c9d4c053-49b6-410c-bc78-3d54a9991870"), "Print start shape using for loop" }
                });

            migrationBuilder.InsertData(
                table: "CourseSectionMgt",
                columns: new[] { "SecCourseId", "CourseId", "CreatedDate", "EndDate", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1d490a70-90ce-4d15-9494-5248280c2ce1"), new Guid("c9d4c053-49b6-410c-bc78-1d54a9991870"), new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("0d490a70-94ce-4d15-9494-5248280c2ce1"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1d490a70-94ce-4d15-9494-5248280c2ce1"), new Guid("c9d4c053-49b6-410c-bc78-3d54a9991870"), new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "SecAssignmentMgt",
                columns: new[] { "SectionAId", "AssignmentId", "CreatedDate", "Score", "SubmissionText", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5240280c2ca1"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb51a"), new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, "Created home page", new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce1") },
                    { new Guid("3d490a70-94ce-4d15-9494-5241280c2cb6"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 95, "Found 25 prime from given dataset.", new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce6") },
                    { new Guid("3d490a70-94ce-4d15-9494-5242280c2cc1"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb53a"), new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, "Created pattern", new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce7") }
                });

            migrationBuilder.InsertData(
                table: "SecEnrollmentMgt",
                columns: new[] { "SecEnrolId", "CreatedDate", "EndDate", "SecCourseId", "StartDate", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("1d490a70-94ce-4d15-9494-5248280c2ce1"), new DateTime(2020, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1d490a70-90ce-4d15-9494-5248280c2ce1"), new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce1") },
                    { new Guid("2d490a70-94ce-4d15-9494-5248280c2ce1"), new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0d490a70-94ce-4d15-9494-5248280c2ce1"), new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce6") },
                    { new Guid("4d490a70-94ce-4d15-9494-5248280c2ce1"), new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1d490a70-94ce-4d15-9494-5248280c2ce1"), new DateTime(2020, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce7") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CourseId",
                table: "Assignment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSectionMgt_CourseId",
                table: "CourseSectionMgt",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SecAssignmentMgt_AssignmentId",
                table: "SecAssignmentMgt",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SecAssignmentMgt_UserId",
                table: "SecAssignmentMgt",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecEnrollmentMgt_SecCourseId",
                table: "SecEnrollmentMgt",
                column: "SecCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SecEnrollmentMgt_UserId",
                table: "SecEnrollmentMgt",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecAssignmentMgt");

            migrationBuilder.DropTable(
                name: "SecEnrollmentMgt");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "CourseSectionMgt");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CourseMgt");
        }
    }
}
