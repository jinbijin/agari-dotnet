namespace Traversable
{
    public interface IBinarySearcher<T>
    {
        T Current { get; }
        bool MoveBefore();
        bool MoveAfter();
    }
}
