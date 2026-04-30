using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commads.UpdateBrand;

public record UpdateBrandCommand(UpdateBrandRequest Request) : IRequest<int>;
