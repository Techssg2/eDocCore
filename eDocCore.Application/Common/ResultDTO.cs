using System.Collections.Generic;
using System.Net;

namespace eDocCore.Application.Common
{
    public class ResultDTO<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; init; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public string? TraceId { get; init; }
        public static ResultDTO<T> Success(T data, string? message = null, string? traceId = null) =>
            new() { IsSuccess = true, StatusCode = HttpStatusCode.OK, Message = message ?? "Thành công.", Data = data, TraceId = traceId };

        public static ResultDTO<T> Failure(HttpStatusCode statusCode, string message, T? data = default, string? traceId = null) =>
            new() { IsSuccess = false, StatusCode = (HttpStatusCode) statusCode, Message = message, Data = data, TraceId = traceId };
    }
    public class ResultDTO
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; init; }
        public string? Message { get; set; }
        public string? TraceId { get; init; }
        public static ResultDTO Success(string? message = null, string? traceId = null) =>
            new() { IsSuccess = true, StatusCode = HttpStatusCode.OK, Message = message ?? "Success", TraceId = traceId };

        public static ResultDTO Failure(HttpStatusCode statusCode, string message, string? traceId = null) =>
            new() { IsSuccess = false, StatusCode = (HttpStatusCode)statusCode, Message = message, TraceId = traceId };
    }
}
