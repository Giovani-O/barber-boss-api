using System.Net;

namespace BarberBoss.Exception.ExceptionBase;

/// <summary>
/// Exception thrown when a validation fails
/// </summary>
public class ErrorOnValidationException : BarberBossException
{
    private readonly Dictionary<string, List<string>> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(Dictionary<string, List<string>> errors) : base(string.Empty)
    {
        _errors = errors;
    }
    
    public override Dictionary<string, List<string>> GetErrors() => _errors;
}