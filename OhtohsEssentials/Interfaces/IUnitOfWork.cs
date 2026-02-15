using System.Data;

namespace OhtohsEssentials.Core.Interfaces;

public interface IUnitOfWork
{
    IDbTransaction BeginTransaction();
}
