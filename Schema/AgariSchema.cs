namespace Schema
{
    public class AgariSchema : GraphQL.Types.Schema
    {
        public AgariSchema(AgariQuery query)
        {
            Query = query;
        }
    }
}
