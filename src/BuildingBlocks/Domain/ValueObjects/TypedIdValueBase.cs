using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects;

public abstract record TypedIdValueBase
{
    public Guid Value { get; }

    protected TypedIdValueBase(Guid value)
    {
        if (value == Guid.Empty)
            throw new TypedIdValueBaseException("Id cannot be empty!");

        Value = value;
    }
}
