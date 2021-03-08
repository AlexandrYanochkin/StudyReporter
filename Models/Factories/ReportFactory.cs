using System.Linq;
using System.Collections.Generic;
using StudyReporter.Models.Dto;

namespace StudyReporter.Models.Factories
{
    public static class ReportFactory
    {
        public static IEnumerable<StudentReportDto> CreateStudentReport(this IEnumerable<StudentDto> students)
        {
            var studentReport = students
               .Select(student => new StudentReportDto
               {
                   FirstName = student.FirstName,
                   LastName = student.LastName,
                   Course = student.Course,
                   Subjects = student.Subjects.Select(subject => new SubjectReportDto
                   {
                       SubjectName = subject.Name,
                       AverageMark = subject.Marks.Average(),
                       MaxMark = subject.Marks.Max(),
                       MinMark = subject.Marks.Min(),
                   }).ToList(),
               });

            return studentReport;
        }
    }
}
