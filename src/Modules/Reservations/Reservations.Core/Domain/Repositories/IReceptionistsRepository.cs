using Reservations.Core.Domain.Entities;

namespace Reservations.Core.Domain.Repositories;
internal interface IReceptionistsRepository
{
    Task AddAsync(Receptionist receptionist, CancellationToken cancellationToken);
}
