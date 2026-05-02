using ESFE.BusinessLogic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Brands.Queries.GetQuotation;

public record GetQuotationQuery(long QuotationId) : IRequest<QuotationResponse>;

