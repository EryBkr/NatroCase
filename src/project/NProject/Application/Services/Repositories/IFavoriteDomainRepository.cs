using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFavoriteDomainRepository : IAsyncRepository<FavoriteDomain, Guid>, IRepository<FavoriteDomain, Guid>
{
}

