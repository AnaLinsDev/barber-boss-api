namespace BarberBoss.Communication.Responses;
public class ResponseErrorJson
{
    public List<string> ErrorMessage { get; set; }

    public ResponseErrorJson(List<string> messages)
    {
        ErrorMessage = messages;
    }

    public ResponseErrorJson(string message)
    {
        ErrorMessage = [message];
    }
}
