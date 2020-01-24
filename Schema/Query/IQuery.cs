using System.Threading.Tasks;
using GraphQL.Types;

namespace Schema.Query
{
    public interface IQuery<TResult>
    {
        Task<TResult> ExecuteAsync(ResolveFieldContext<object> context);
    }
}
