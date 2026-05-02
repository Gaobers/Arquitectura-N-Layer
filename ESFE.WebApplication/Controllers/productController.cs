using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;
using ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProduct;
using ESFE.BusinessLogic.UseCases.Products.Commands.DeleteProduct; // Asegúrate de tener este
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESFE.WebApplication.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _mediator.Send(new GetProductQuery { Id = id });
            if (product == null) return NotFound();
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            // Para el Select de Marcas en la vista de creación
            var brands = await _mediator.Send(new GetBrandsQuery());
            ViewBag.Brands = new SelectList(brands, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDTO createProductDTO)
        {
            try
            {
                var result = await _mediator.Send(createProductDTO.Adapt<CreateProductCommand>());
                if (result > 0) return RedirectToAction(nameof(Index));

                return View(createProductDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(createProductDTO);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _mediator.Send(new GetProductQuery { Id = id });
            if (product == null) return NotFound();

            var brands = await _mediator.Send(new GetBrandsQuery());
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

            return View(product.Adapt<UpdateProductDTO>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductDTO updateProductDTO)
        {
            try
            {
                var result = await _mediator.Send(updateProductDTO.Adapt<UpdateProductCommand>());
                if (result > 0) return RedirectToAction(nameof(Index));

                return View(updateProductDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(updateProductDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand { Id = id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}