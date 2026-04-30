using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commads.CreateBrand;

public record CreateBrandCommand(CreateBrandRequest Request) : IRequest<int>;

