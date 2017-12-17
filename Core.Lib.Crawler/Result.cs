using System.Threading.Tasks;
using System;

namespace Core.Lib.Crawler
{
    public class Result<TResult> : IResult<TResult>
    {
        private readonly Exception _error;
        private readonly Func<Exception> _errorFunc;
        private readonly Func<TResult> _resultFunc;
        private readonly TResult _result;

        public Result(Exception error) : this(error, default) { }
        public Result(TResult result) : this(default, result) { }
        private Result(Exception error, TResult result)
        {
            this._error = error;
            this._result = result;
            this._errorFunc = () => _error;
            this._resultFunc = () => _result;
        }

        protected Result(Func<Exception> error, Func<TResult> result)
        {
            _resultFunc = result;
            _errorFunc = error;
        }
        protected Result(Result<TResult> other)
        {
            this._error = other._error;
            this._errorFunc = other._errorFunc;
            this._result = other._result;
            this._resultFunc = other._resultFunc;
        }
        public virtual Result<TResult> OnError<TException>(Action<TException> error) where TException : Exception
        {
            new Result<TResult>(AppendError(error), _resultFunc);
            return this;
        }

        private Func<Exception> AppendError<TException>(Action<TException> error) where TException : Exception
            => () =>
            {
                var ex = _errorFunc();
                (ex is TException e ? (Action)(() => error(e)) : () => { })();
                return ex;
            };

        public virtual Result<TNewResult> OnSuccess<TNewResult>(Func<TResult, TNewResult> result)
            => new Result<TNewResult>(_errorFunc, () => result(_resultFunc()));
        public virtual TResult Value => (_errorFunc() == null ? _resultFunc : () => default)();
    }
}
