using System.Collections.Generic;

namespace StudyReporter.Models.Dto
{
    public class StudentDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Course { get; set; }

        public IEnumerable<SubjectDto> Subjects { get; set; }
    }
}
