using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public class AverageRating : ValueObject
{
    
public double Value { get; private set; }
public int NumRatings { get; private set; }

public AverageRating(double value, int numRatings)
{
    Value = value;
    NumRatings = numRatings;
}

public static AverageRating CreateNew(double rating = 0, int numRatings = 0)
{
    return new AverageRating(rating, numRatings);
}

public void AddNewRating(int rating)
{
    Value = ((Value * NumRatings) + rating) / ++NumRatings;
}
internal void RemoveRating(int rating)
{
    Value = ((Value * NumRatings) - rating) / --NumRatings;

}

protected override IEnumerable<object> GetEqualityComponents()
{
    yield return Value;
}
}