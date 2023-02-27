using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.UpsertSpouse;

public class UpsertSpouseCommandHandler : IRequestHandler<UpsertSpouseCommand, CommandResult>
{
        public UpsertSpouseCommandHandler(IBuyerRepository buyerRepository, ILogger<UpsertSpouseCommandHandler> logger)
        {
                _buyerRepository = buyerRepository;
                _logger = logger;
        }

        private readonly IBuyerRepository _buyerRepository;
        private readonly ILogger<UpsertSpouseCommandHandler> _logger;
        
        
        public async Task<CommandResult> Handle(UpsertSpouseCommand request, CancellationToken cancellationToken)
        {
                var principal = await _buyerRepository.GetByIdAsync(request.BuyerId);

                if (principal == default)
                {
                        throw new SalesDomainException(nameof(UpsertSpouseCommandHandler),
                                new Exception("Buyer not found."));
                }

                if (principal.GetCivilStatusId() != (int)CivilStatus.CivilStatusIs.Married)
                {
                        throw new SalesDomainException(nameof(UpsertSpouseCommandHandler),
                                new Exception("Buyer is not married."));
                }

                if (principal.PartnerId.HasValue && principal.PartnerId != request.SpouseId)
                {
                        throw new SalesDomainException(nameof(UpsertSpouseCommandHandler),
                                new Exception("Incorrect spouse id."));
                }

                Buyer partnerOrSpouse = default;

                if (principal.PartnerId.HasValue && principal.PartnerId.Value > 0)
                {
                        //fetch spouse
                        partnerOrSpouse= await _buyerRepository.GetByIdAsync(request.SpouseId);
                        if (partnerOrSpouse == default)
                        {
                                throw new SalesDomainException(nameof(UpsertSpouseCommandHandler),
                                        new Exception("Spouse not found."));
                        }

                        partnerOrSpouse.UpdateBasicInfo(request.Salutation, request.FirstName, request.MiddleName,
                                request.LastName, request.DoB, principal.GetCivilStatusId(), principal.IsCorporate);

                        partnerOrSpouse.UpdateContactDetails(request.EmailAddress, request.PhoneNo, request.MobileNo);

                        partnerOrSpouse.UpdateMiscInformation(request.Occupation, request.TIN, request.GovIssuedID,
                                request.GovIssuedIDValidUntil);

                    
                }
                else
                {
                        //not yet assigned a partner
                        partnerOrSpouse = new Buyer(request.Salutation, request.FirstName, request.MiddleName,
                                request.LastName, request.EmailAddress, request.PhoneNo, request.MobileNo, request.DoB,
                                principal.GetCivilStatusId(),
                                principal.Addresses.FirstOrDefault(i => i.Type == AddressTypeEnum.Home),
                                principal.IsCorporate, principal.Company);
                }

                 _buyerRepository.Upsert(partnerOrSpouse);

                principal.AddPartnerOrSpouse(partnerOrSpouse.Id);
                
                _buyerRepository.Upsert(principal);
                
                return CommandResult.Success(partnerOrSpouse.Id);

        }
}