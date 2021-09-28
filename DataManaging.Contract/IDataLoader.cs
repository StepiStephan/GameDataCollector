namespace DataManaging.Contract
{
    public interface IDataLoader<T>
    {
        T LoadObject();
    }
}