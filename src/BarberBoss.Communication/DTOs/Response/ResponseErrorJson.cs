namespace BarberBoss.Communication.DTOs.Response;

public class ResponseErrorJson
{
    public Dictionary<string, List<string>> ErrorMessages { get; set; }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = new Dictionary<string, List<string>>()
        {
            { "Error", [errorMessage] }
        };
    }

    public ResponseErrorJson(Dictionary<string, List<string>> errorMessage)
    {
        ErrorMessages = errorMessage;
    }
}