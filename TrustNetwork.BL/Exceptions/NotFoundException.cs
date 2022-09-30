namespace TrustNetwork.BL.Exceptions;

public class NotFoundException : ArgumentException
{
    public NotFoundException() { }
    public NotFoundException(string? message) : base(message) { }
    public NotFoundException(string? message, string? paramName) : base(message, paramName) { }
}
