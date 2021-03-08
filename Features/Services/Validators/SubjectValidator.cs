using System.Linq;
using StudyReporter.Models.Constants;
using StudyReporter.Models.Domain;
using StudyReporter.Models.Dto;
using StudyReporter.Models.Interfaces;

namespace StudyReporter.Features.Services.Validators
{
    internal class SubjectValidator : IValidator<SubjectDto>
    {
        public ValidationResult Validate(SubjectDto item)
        {
            var areValidFields = (item != null)
                && !string.IsNullOrEmpty(item.Name) 
                && (item.Marks != null)
                && item.Marks.All(t => MarkInRange(t));

            if (!areValidFields)
            {
                return ValidationResult.Failure(ErrorMessages.SubjectInvalidFields);
            }

            return ValidationResult.SuccessfulResult;
        }

        private bool MarkInRange(int mark)
        {
            return (mark >= StudyValues.MinMark) && (mark <= StudyValues.MaxMark);
        }
    }
}
