﻿using Lukki.Domain.Common.Models;

namespace Lukki.Domain.OrderAggregate.ValueObjects;

public sealed class InOrderProductId : ValueObject
{
    public Guid Value { get; private set; }
    
    private InOrderProductId(Guid value)
    {
        Value = value;
    }

    public static InOrderProductId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static InOrderProductId Create(Guid value)
    {
        return new InOrderProductId(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}