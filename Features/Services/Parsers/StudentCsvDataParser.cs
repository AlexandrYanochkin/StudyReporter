using System;
using System.Linq;
using System.Text.RegularExpressions;
using StudyReporter.Models.Dto;
using StudyReporter.Models.Constants;
using StudyReporter.Models.Interfaces;

namespace StudyReporter.Features.Services.Parsers
{
    internal class StudentCsvDataParser : IParser<StudentDto>
    {
        private const string _subjectCsvPattern = @"[A-Za-z]+(;\d{1}){2,}";

        private const string _studentCsvPattern = @"[A-Za-z]+;[A-Za-z]+;\d{1}";

        private const char _csvDivider = ';';

        private static readonly string _commonCsvPattern = $@"^{_studentCsvPattern}(;{_subjectCsvPattern})+$";

        public StudentDto Parse(string inputData)
        {
            if (!Regex.IsMatch(inputData, _commonCsvPattern))
            {
                throw new ArgumentException(ErrorMessages.InvalidCsvPattern);
            }

            var studentMatch = Regex.Match(inputData, _studentCsvPattern);

            var subjectMatches = Regex.Matches(inputData, _subjectCsvPattern);

            var student = CreateStudent(studentMatch.Value);

            student.Subjects = subjectMatches.Cast<Match>().Select(t => CreateSubject(t.Value)).ToList();

            return student;
        }

        private StudentDto CreateStudent(string studentPattern)
        {
            var studentData = studentPattern.Split(_csvDivider);

            var student = new StudentDto
            {
                FirstName = studentData[0],
                LastName = studentData[1],
                Course = int.Parse(studentData[2]),
            };

            return student;
        }

        private SubjectDto CreateSubject(string subjectPattern)
        {
            var subjectData = subjectPattern.Split(_csvDivider);

            var subject = new SubjectDto
            {
                Name = subjectData[0],
                Marks = subjectData.Skip(1).Select(t => int.Parse(t)).ToList(),
            };

            return subject;
        }
    }
}
