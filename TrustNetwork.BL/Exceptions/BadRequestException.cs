namespace TrustNetwork.BL.Exceptions;

public class BadRequestException : ArgumentException
{
    public BadRequestException() { }
    public BadRequestException(string? message) : base(message) { }
    public BadRequestException(string? message, string? paramName) : base(message, paramName) { }
}
