using System;

namespace MosaicoSolutions.Functional
{
    public struct Try<T>
    {
        internal T Success { get; }
        
        internal Exception Exception { get; }

        public bool IsSuccess { get; }

        public bool IsFailure =>
            !IsSuccess;

        public Option<T> OptionSuccess =>
            Success;

        public Option<Exception> OptionException =>
            Exception;

        internal Try(T value)
        {
            Success = value;
            IsSuccess = true;
            Exception = null;
        }

        internal Try(Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
            Success = default(T);
        }

        public static implicit operator Try<T>(T value) =>
            new Try<T>(value);

        public static implicit operator Try<T>(Exception exception) =>
            new Try<T>(exception);

        public void IfSuccess(Action<T> success)
        {
            if (IsSuccess)
                success(Success);
        }
        
        public void IfFailure(Action<Exception> failure)
        {
            if (IsFailure)
                failure(Exception);
        }
        
        public void Match(Action<T> success, Action<Exception> failure)
        {
            if (IsSuccess)
                success(Success);
            else
                failure(Exception);
        }

        public void Match(Action<T> success, Action failure)
        {
            if (IsSuccess)
                success(Success);
            else
                failure();
        }
    }
}