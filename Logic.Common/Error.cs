namespace Logic.Common
{
    public abstract class Error
    {
        protected Error(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
