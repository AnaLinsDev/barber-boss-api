using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Billings.Register;
public class BillingValidator : AbstractValidator<RequestBillingJson>
{
    public BillingValidator()
    {
        RuleFor(billing => billing.Date)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.DATE_REQUIRED);

        RuleFor(billing => billing.BarberName)
            .Length(2, 80)
            .WithMessage(ResourceErrorMessages.BARBER_NAME_LENGTH_INVALID);

        RuleFor(billing => billing.ClientName)
            .Length(2, 120)
            .WithMessage(ResourceErrorMessages.CLIENT_NAME_LENGTH_INVALID);

        RuleFor(billing => billing.ServiceName)
            .Length(2, 120)
            .WithMessage(ResourceErrorMessages.SERVICE_NAME_LENGTH_INVALID);

        RuleFor(billing => billing.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_HIGHER_THAN_ZERO);

        RuleFor(billing => billing)
            .Must(billing =>
                billing.Status != Status.Canceled || billing.Amount == 0)
            .WithMessage(ResourceErrorMessages.CANCELLED_BILLING_AMOUNT_MUST_BE_ZERO);

        RuleFor(billing => billing.PaymentMethod)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);

        RuleFor(billing => billing.Status)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.STATUS_TYPE_INVALID);
        
        RuleFor(billing => billing.Notes)
            .MaximumLength(500)
            .WithMessage(ResourceErrorMessages.NOTES_LENGTH_INVALID);
    }
}