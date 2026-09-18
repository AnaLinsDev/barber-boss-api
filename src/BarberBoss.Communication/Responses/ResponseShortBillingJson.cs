using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses;
public class ResponseShortBillingJson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    public string BarberName { get; set; } = String.Empty;
    public string ClientName { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public Status Status { get; set; }
}

