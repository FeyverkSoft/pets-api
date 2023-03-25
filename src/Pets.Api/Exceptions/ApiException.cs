namespace Pets.Api.Exceptions;

using System.Collections.Generic;
using System.Net;

public class ApiException : Exception
{
    private readonly HttpStatusCode _httpCode;

    public ApiException(HttpStatusCode httpCode, String code, String message)
        : this(httpCode, code, message, null)
    {
        _httpCode = httpCode;
    }

    public ApiException(HttpStatusCode httpCode, String code, String message, Exception innerException = null)
        : base(message, innerException)
    {
        Code = code;
        _httpCode = httpCode;
    }

    public ApiException(HttpStatusCode httpCode, String code, String message, IDictionary<String, Object> fields, Exception innerException = null)
        : base(message, innerException)
    {
        _httpCode = httpCode;
        Code = code;
        Fields = fields;
    }

    public String Code { get; }

    public IDictionary<String, Object> Fields { get; set; }

    public Int32 GetHttpStatusCode()
    {
        return (Int32)_httpCode;
    }
}