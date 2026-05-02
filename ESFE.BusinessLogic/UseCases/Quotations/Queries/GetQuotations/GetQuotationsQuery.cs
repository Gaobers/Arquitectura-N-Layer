using ESFE.BusinessLogic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Cotations.Queries.GetQuotations;

public record GetQuotationsQuery : IRequest<List<QuotationResponse>>;

