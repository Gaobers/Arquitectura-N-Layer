using ESFE.DataAccess.Interfaces;
using MediatR;
using ESFE.Entities;
using ESFE.DataAccess.Repositories;
using Mapster;

namespace ESFE.BusinessLogic.UseCases.Quotations.Commands.CreateQuotation
{
    internal sealed class CreateQuotationHandler(IEfRepository<Quotation> _repository) : IRequestHandler<CreateQuotationCommand, long>
    {
        public async Task<long> Handle(CreateQuotationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.BeginTransactionAsync();

                var newQuotation = command.Request.Adapt<Quotation>();

                var CreatedQuotation = await _repository.AddAsync(newQuotation, cancellationToken);

                await _repository.CommitAsync();

                return CreatedQuotation.QuotationId;

            }
            catch (Exception)
            { 

            await _repository.RollbackAsync();
            throw;
            
            }
        }
    }
}
