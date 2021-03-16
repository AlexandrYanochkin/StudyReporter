using System.Linq;
using System.Collections.Generic;
using StudyReporter.Models.Dto;
using StudyReporter.Models.Domain;
using StudyReporter.Models.Interfaces;
using StudyReporter.Models.Constants;

namespace StudyReporter.Features.Services.Validators
{
    internal class StudentValidator : IValidator<StudentDto>
    {
        private readonly IValidator<SubjectDto> _subjectValidator;

        public StudentValidator(IValidator<SubjectDto> subjectValidator)
        {
            _subjectValidator = subjectValidator;
        }

        public ValidationResult Validate(StudentDto item)
        {
            if (!ValidateFiels(item))
            {
                return ValidationResult.FailedResult(ErrorMessages.StudentInvalidFields);
            }

            if (!ValidateSubjects(item.Subjects))
            {
                return ValidationResult.FailedResult(ErrorMessages.SubjectError);
            }

            var subjectValidationResults = item.Subjects.Select(t => _subjectValidator.Validate(t));

            if (subjectValidationResults.Any(t => !t.IsValid))
            {
                return subjectValidationResults.First(t => !t.IsValid);
            }

            return ValidationResult.SuccessfulResult();
        }

        public static bool ValidateFiels(StudentDto studentDto)
        {
            var areValidFields = studentDto != null
                && !string.IsNullOrEmpty(studentDto.FirstName)
                && !string.IsNullOrEmpty(studentDto.LastName)
                && CourseInRange(studentDto.Course)
                && studentDto.Subjects != null;

            return areValidFields;
        }

        public static bool ValidateSubjects(IEnumerable<SubjectDto> subjects)
        {
            var countOfSubjects = subjects.Count();
            var countOfUniqueNamedSubjects = subjects
                .Select(t => t.Name)
                .Distinct()
                .Count();

            return (countOfSubjects == countOfUniqueNamedSubjects);
        }

        private static bool CourseInRange(int course)
        {
            return (course >= StudyValues.MinCourse) && (course <= StudyValues.MaxCourse);
        }
    }
}