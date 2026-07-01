using Mouts.DeveloperTest.Domain.ValueObjects;

namespace Mouts.DeveloperTest.Domain
{
    public sealed record CartItem(
     ProductId ProductId,
     int Quantity);
}
