using MassTransit;
using Microsoft.Extensions.Logging;
using Reservations.Core.Domain.Entities;
using Reservations.Core.Domain.Repositories;
using Reservations.Core.Persistence;
using Staff.Contracts;

namespace Reservations.Core.EventSubscriptions;
internal class ReceptionistCreatedEventHandler : IConsumer<ReceptionistCreatedIntegrationEvent>
{
    private readonly ILogger<ReceptionistCreatedEventHandler> _logger;
    private readonly IReceptionistsRepository _receptionistsRepository;
    private readonly ReservationsUnitOfWork _unitOfWork;

    public ReceptionistCreatedEventHandler(ILogger<ReceptionistCreatedEventHandler> logger, IReceptionistsRepository receptionistsRepository,
        ReservationsUnitOfWork unitOfWork)
    {
        _logger = logger;
        _receptionistsRepository = receptionistsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<ReceptionistCreatedIntegrationEvent> context)
    {
        var message = context.Message;
        var receptionist = Receptionist.Create(message.Id, message.FullName, message.Email);
        await _receptionistsRepository.AddAsync(receptionist, context.CancellationToken);

        _logger.LogInformation($"Receptionist created consumed {receptionist.EmployeeId.Value} {receptionist.FullName.Value} {receptionist.Email.Value} {context.CorrelationId}");
        await _unitOfWork.CommitAsync(context.CancellationToken);
    }
}
