namespace StudyReporter.Models.Domain
{
    public class ValidationResult
    {
        public static ValidationResult SuccessfulResult { get; } = new ValidationResult
        {
            IsValid = true,
            Error = string.Empty,
        };

        public bool IsValid { get; set; }

        public string Error { get; set; }
    }
}
