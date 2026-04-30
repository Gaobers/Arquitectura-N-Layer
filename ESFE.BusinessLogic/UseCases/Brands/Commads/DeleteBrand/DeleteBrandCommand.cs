using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commads.DeleteBrand;

public record DeleteBrandCommand(int brandId) : IRequest<int>;
