using System.Net;

namespace BarberBoss.Exception.ExceptionBase;

/// <summary>
/// Exception thrown when a resource is not found
/// </summary>
public class NotFoundException : BarberBossException
{
    private readonly Dictionary<string, List<string>> _errors;

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public NotFoundException(Dictionary<string, List<string>> errors) : base(string.Empty)
    {
        _errors = errors;
    }
    
    public override Dictionary<string, List<string>> GetErrors() => _errors;
}