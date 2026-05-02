using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Cotations.Commands.CreateQuotation;

public record CreateQuotationCommand(CreateQuotationRequest Request) : IRequest<long>;