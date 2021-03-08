using System.Collections.Generic;

namespace StudyReporter.Models.Dto
{
    public class StudentReportDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Course { get; set; }

        public IEnumerable<SubjectReportDto> Subjects { get; set; }
    }
}
