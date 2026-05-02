using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrand;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.WebApplication.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator) => _mediator = mediator;

        public async Task<IActionResult> Index()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            return View(brands);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBrandDTO createBrandDTO)
        {
            try
            {
                var result = await _mediator.Send(createBrandDTO.Adapt<CreateBrandCommand>());
                if (result > 0) return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Sucedió un error al intentar crear la marca");
                return View(createBrandDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(createBrandDTO);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _mediator.Send(new GetBrandQuery { Id = id });
            if (brand == null) return NotFound();
            return View(brand.Adapt<UpdateBrandDTO>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBrandDTO updateBrandDTO)
        {
            try
            {
                var result = await _mediator.Send(updateBrandDTO.Adapt<UpdateBrandCommand>());
                if (result > 0) return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Sucedió un error al intentar editar la marca");
                return View(updateBrandDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(updateBrandDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteBrandCommand { Id = id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
