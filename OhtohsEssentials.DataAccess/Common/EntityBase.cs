namespace OhtohsEssentials.DataAccess.Common;

public abstract class EntityBase<TId>
{
    public TId Id { get; }

    protected EntityBase(TId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EntityBase<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }
}
