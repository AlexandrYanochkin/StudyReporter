namespace StudyReporter.Models.Domain
{
    public class ValidationResult
    {
        public static ValidationResult SuccessfulResult { get; } = new ValidationResult
        {
            IsValid = true,
            Error = string.Empty,
        };

        public static ValidationResult Failure(string errorMessage)
        {
            return new ValidationResult
            {
                IsValid = false,
                Error = errorMessage,
            };
        }

        public bool IsValid { get; set; }

        public string Error { get; set; }
    }
}
