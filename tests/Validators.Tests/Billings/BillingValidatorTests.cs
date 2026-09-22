using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Billings;
public class BillingValidatorTests
{
    [Fact]
    public void ShouldBeValid_WhenRequestIsValid()
    {
        // Arrange
        var validator = new BillingValidator();

        var request = RequestBillingJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeTrue();
    }


    [Fact]
    public void ShouldBeInvalid_WhenDateIsEmpty()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.Date = default;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.DATE_REQUIRED);
    }

    [Fact]
    public void ShouldBeInvalid_WhenBarberNameIsTooShort()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.BarberName = "A";

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.BARBER_NAME_LENGTH_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenBarberNameIsTooLong()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.BarberName = new string('A', 81);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.BARBER_NAME_LENGTH_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenClientNameIsTooShort()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.ClientName = "A";

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.CLIENT_NAME_LENGTH_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenClientNameIsTooLong()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.ClientName = new string('A', 121);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.CLIENT_NAME_LENGTH_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenServiceNameIsTooShort()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.ServiceName = "A";

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.SERVICE_NAME_LENGTH_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenServiceNameIsTooLong()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.ServiceName = new string('A', 121);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.SERVICE_NAME_LENGTH_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenAmountIsNegative()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.Amount = -1;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_HIGHER_THAN_ZERO);
    }

    [Fact]
    public void ShouldBeInvalid_WhenCanceledBillingHasAmount()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.Status = Status.Canceled;
        request.Amount = 100;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage ==
            ResourceErrorMessages.CANCELLED_BILLING_AMOUNT_MUST_BE_ZERO);
    }

    [Fact]
    public void ShouldBeInvalid_WhenPaymentMethodIsInvalid()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.PaymentMethod = (PaymentMethod)999;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenStatusIsInvalid()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.Status = (Status)999;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.STATUS_TYPE_INVALID);
    }

    [Fact]
    public void ShouldBeInvalid_WhenNotesExceedMaximumLength()
    {
        // Arrange
        var validator = new BillingValidator();
        var request = RequestBillingJsonBuilder.Build();

        request.Notes = new string('A', 501);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(error =>
            error.ErrorMessage == ResourceErrorMessages.NOTES_LENGTH_INVALID);
    }
}
