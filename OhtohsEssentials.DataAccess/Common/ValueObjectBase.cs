using System.Linq;
namespace OhtohsEssentials.DataAccess.Common;

public abstract class ValueObjectBase
{
    public abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return ((ValueObjectBase)obj).GetEqualityComponents()
            .SequenceEqual(GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((prev, next) => prev ^ next);
    }
}
