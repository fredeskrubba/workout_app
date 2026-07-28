using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApplication.Shared.Results
{
    public record Result<T>(
    bool IsSuccess,
    T? Value,
    string? Error)
    {
        public static Result<T> Success(T value) =>
            new(true, value, null);

        public static Result<T> Failure(string error) =>
            new(false, default, error);
    }
}
