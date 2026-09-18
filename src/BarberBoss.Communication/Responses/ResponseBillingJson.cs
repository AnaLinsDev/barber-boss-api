using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses;
public class ResponseBillingJson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    public string BarberName { get; set; } = String.Empty;
    public string ClientName { get; set; } = String.Empty;
    public string ServiceName { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Status Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
