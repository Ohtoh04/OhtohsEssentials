namespace OhtohsEssentials.DataAccess.Common;

public abstract class AggregateRoot : EntityBase<Guid>
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}
