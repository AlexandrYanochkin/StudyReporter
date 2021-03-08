using System.Collections.Generic;

namespace StudyReporter.Models.Dto
{
    public class SubjectDto
    {
        public string Name { get; set; }

        public IEnumerable<int> Marks { get; set; }
    }
}