namespace BarberBoss.Communication.Responses;
public class ResponseBillingsJson
{
    public IList<ResponseShortBillingJson> Billings { get; set; } = [];
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
}
