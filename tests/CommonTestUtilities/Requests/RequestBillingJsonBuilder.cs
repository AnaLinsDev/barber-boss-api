using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestBillingJsonBuilder
{
    public static RequestBillingJson Build()
    {
        var faker = new Faker();

        return new RequestBillingJson
        {
            Date = DateOnly.FromDateTime(faker.Date.Recent()),
            BarberName = faker.Name.FullName(),
            ClientName = faker.Name.FullName(),
            ServiceName = faker.Commerce.ProductName(),
            Amount = faker.Finance.Amount(1, 500),
            PaymentMethod = faker.PickRandom<PaymentMethod>(),
            Status = faker.PickRandom(Status.Paid, Status.Open),
            Notes = faker.Lorem.Sentence()
        };
    }

}
