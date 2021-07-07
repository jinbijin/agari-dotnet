namespace BinarySearchable
{
    public interface IBinarySearcher<T>
    {
        T Current { get; }
        bool MoveBefore();
        bool MoveAfter();
    }
}
