using StudyReporter.Models.Domain;

namespace StudyReporter.Models.Interfaces
{
    public interface IValidator<in T>
    {
        ValidationResult Validate(T item); 
    }
}
