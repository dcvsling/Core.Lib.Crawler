using System;

namespace Core.Lib.Crawler
{
    public interface IResult<TResult>
    {
        TResult Value { get; }

        Result<TResult> OnError<TException>(Action<TException> error) where TException : Exception;
        Result<TNewResult> OnSuccess<TNewResult>(Func<TResult, TNewResult> result);
    }
}