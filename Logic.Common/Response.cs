using System;
using System.Threading.Tasks;

namespace Logic.Common
{
    public sealed class DataResponse<T, TError> : Response<T, TError> where TError : Error
    {
        internal DataResponse(T data) : base()
        {
            Data = data;
        }

        public T Data { get; init; }
    }

    public sealed class ErrorResponse<T, TError> : Response<T, TError> where TError : Error
    {
        internal ErrorResponse(TError error) : base()
        {
            Error = error;
        }

        public TError Error { get; init; }
    }

    public abstract class Response<T, TError> where TError : Error
    {
        protected internal Response()
        {
        }

        public Response<U, TError> Map<U>(Func<T, U> mapFunc)
        {
            if (this is DataResponse<T, TError> dataResponse)
            {
                return DataResponse<U, TError>.Success(mapFunc(dataResponse.Data));
            }

            if (this is ErrorResponse<T, TError> errorResponse)
            {
                return ErrorResponse<U, TError>.Failure(errorResponse.Error);
            }

            throw new NotSupportedException("Reached code that should be unreachable.");
        }

        public async Task<Response<U, TError>> Map<U>(Func<T, Task<U>> mapFunc)
        {
            if (this is DataResponse<T, TError> dataResponse)
            {
                return DataResponse<U, TError>.Success(await mapFunc(dataResponse.Data));
            }

            if (this is ErrorResponse<T, TError> errorResponse)
            {
                return ErrorResponse<U, TError>.Failure(errorResponse.Error);
            }

            throw new NotSupportedException("Reached code that should be unreachable.");
        }

        public static DataResponse<T, TError> Success(T data) => new(data);
        public static ErrorResponse<T, TError> Failure(TError error) => new(error);
    }
}
