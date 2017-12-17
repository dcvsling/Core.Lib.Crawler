namespace Core.Lib.Crawler
{

    public interface ISpec<T>
    {
        T Build();
    }
}
