namespace BinarySearchable
{
    public interface IBinarySearchable<T>
    {
        IBinarySearcher<T> GetBinarySearcher();
    }
}
