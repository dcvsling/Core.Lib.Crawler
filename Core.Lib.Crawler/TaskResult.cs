using Core.Lib.Crawler;
using System;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public class TaskResult<TResult>
    {
        private readonly Task<TResult> _task;
        private Result<TResult> _result;
        public static TaskResult<TResult> Wait(Task<TResult> task)
            => new TaskResult<TResult>(task);
        
        private TaskResult(Task<TResult> task)
            => _task = task;

        public void ForNext(Action<Result<TResult>> next)
        {
            try
            {
                Task.WaitAny(_task.ContinueWith(t =>
                    next(t.IsFaulted ? new Result<TResult>(t.Exception) : new Result<TResult>(t.Result)))
                    );
            }
            catch(Exception ex)
            {
                next(new Result<TResult>(ex));
            }
        }

        public Result<TResult> ForResult()
        {
            ForNext(r => _result = r);
            return _result;
        }
        public TResult Value => ForResult().Value;
    }
}
