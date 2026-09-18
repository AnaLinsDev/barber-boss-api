namespace BarberBoss.Communication.Requests;
public class RequestGetAllBillings
{
    public string? OrderBy { get; set; }
    public string? Order { get; set; }
    public string? FilterBy { get; set; }
    public int Page { get; set; } = 1;
}
