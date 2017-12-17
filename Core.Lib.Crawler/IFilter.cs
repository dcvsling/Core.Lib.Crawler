using System;
namespace Core.Lib.Crawler
{
    public interface IFilter<T>
    {
        bool IsEnable(T t);
    }

    public class DefaultFilter<T> : IFilter<T>
    {
        private readonly Func<T, bool> _predicate;

        public DefaultFilter(Func<T,bool> predicate)
        {
            _predicate = predicate;
        }

        public bool IsEnable(T t)
            => _predicate(t);
    }
}
