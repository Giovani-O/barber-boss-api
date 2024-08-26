namespace BarberBoss.Exception.ExceptionBase;

/// <summary>
/// Base exception for the project
/// </summary>
public abstract class BarberBossException : SystemException
{
    protected BarberBossException(string message) : base(message)
    {
        
    }
    
    public abstract int StatusCode { get; }
    public abstract Dictionary<string, List<string>> GetErrors();
}