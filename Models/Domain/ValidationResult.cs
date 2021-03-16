using System;

namespace StudyReporter.Models.Domain
{
    public class ValidationResult
    {
        private string _errorMessage;

        public bool IsValid { get; private set; }

        public string Error 
        {
            get => _errorMessage;

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _errorMessage = value;
            }
        }

        public static ValidationResult SuccessfulResult()
        {
            return new ValidationResult
            {
                IsValid = true,
                Error = string.Empty,
            };
        }

        public static ValidationResult FailedResult(string errorMessage)
        {
            return new ValidationResult
            {
                IsValid = false,
                Error = errorMessage,
            };
        }
    }
}
