using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Application.Commands.SellUnit
{
	public class UpdateUnitReplicaStatusNotif : INotification
	{
		public UpdateUnitReplicaStatusNotif(int unitId, int unitStatusId, string unitStatus)
		{
			UnitId = unitId;
			UnitStatusId = unitStatusId;
			UnitStatus = unitStatus;
		}

		public int UnitId { get; set; }
		public int UnitStatusId { get; set; }
		public string UnitStatus { get; set; }
		
	}

	public class UpdateUnitReplicaStatusNotifHandler : INotificationHandler<UpdateUnitReplicaStatusNotif>
	{
		private readonly IUnitReplicaRepository _repository;
		private readonly ILogger<UpdateUnitReplicaStatusNotifHandler> _logger;

		public UpdateUnitReplicaStatusNotifHandler(IUnitReplicaRepository repository, ILogger<UpdateUnitReplicaStatusNotifHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task Handle(UpdateUnitReplicaStatusNotif notification, CancellationToken cancellationToken)
		{
			UnitReplica unit = _repository.GetById(notification.UnitId);

			if(unit != null)
			{
				unit.UpdateStatus(notification.UnitStatusId, notification.UnitStatus);

				_repository.Update(unit);

				await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
			}
		}
	}
}
