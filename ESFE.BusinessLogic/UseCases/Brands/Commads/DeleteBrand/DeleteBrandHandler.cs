using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commads.DeleteBrand;

internal sealed class DeleteBrandHandler(IEfRepository<Brand> _repository) : IRequestHandler<DeleteBrandCommand, int>
{
    public async Task<int> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
    {
        var existingBrand = await _repository.GetByIdAsync(command.brandId);

        if (existingBrand is null) return 0;

        await _repository.DeleteAsync(existingBrand, cancellationToken);

        return existingBrand.BrandId;
    }
}
