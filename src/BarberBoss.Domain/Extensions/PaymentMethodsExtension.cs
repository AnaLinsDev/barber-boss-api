using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;

namespace BarberBoss.Domain.Extensions;
public static class PaymentMethodsExtension
{
    public static string PaymentMethodToString(this PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.Card => ResourceReportGenerationMessages.CARD,
            PaymentMethod.Money => ResourceReportGenerationMessages.MONEY,
            PaymentMethod.Pix => ResourceReportGenerationMessages.PIX,
            PaymentMethod.Other => ResourceReportGenerationMessages.OTHER,
            _ => string.Empty,
        };
    }
}
