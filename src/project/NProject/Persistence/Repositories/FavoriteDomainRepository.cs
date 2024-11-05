using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FavoriteDomainRepository : EfRepositoryBase<FavoriteDomain, Guid, BaseDbContext>, IFavoriteDomainRepository
{
    public FavoriteDomainRepository(BaseDbContext context) : base(context)
    {
    }
}
