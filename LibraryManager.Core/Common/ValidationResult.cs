﻿namespace LibraryManager.Core.Common
{
    public sealed class ValidationResult
        : Result, IValidationResult
    {
        public ValidationResult(Error[] errors)
            : base(false, IValidationResult.ValidationError)
        {
            Errors = errors;
        }

        public Error[] Errors { get; }

        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }
}
