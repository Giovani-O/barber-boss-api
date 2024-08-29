using System.Net;

namespace BarberBoss.Exception.ExceptionBase;

public class ErrorOnExecution : BarberBossException
{
    private readonly Dictionary<string, List<string>> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnExecution(Dictionary<string, List<string>> errors) : base(string.Empty)
    {
        _errors = errors;
    }
    
    public override Dictionary<string, List<string>> GetErrors() => _errors;
}