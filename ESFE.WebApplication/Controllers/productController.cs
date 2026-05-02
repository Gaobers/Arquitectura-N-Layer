using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;
using ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProduct;
using ESFE.BusinessLogic.UseCases.Products.Commands.DeleteProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http; // Necesario para IFormFile
using System.IO;

namespace ESFE.WebApplication.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env; // Para obtener la ruta de wwwroot

        public ProductsController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            ViewBag.Brands = new SelectList(brands, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDTO dto, IFormFile imagen)
        {
            try
            {
                if (imagen != null && imagen.Length > 0)
                {
                    // 1. Definir carpeta de destino (wwwroot/images/products)
                    string folder = Path.Combine(_env.WebRootPath, "images", "products");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    // 2. Crear nombre único para el archivo
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                    string filePath = Path.Combine(folder, fileName);

                    // 3. Guardar el archivo físicamente
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    // 4. Guardar la ruta relativa en el DTO
                    dto.ImagenRuta = "/images/products/" + fileName;
                }

                var result = await _mediator.Send(dto.Adapt<CreateProductCommand>());
                if (result > 0) return RedirectToAction(nameof(Index));

                return View(dto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
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
        public async Task<IActionResult> Edit(UpdateProductDTO dto, IFormFile imagen)
        {
            try
            {
                if (imagen != null && imagen.Length > 0)
                {
                    string folder = Path.Combine(_env.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    // Actualizar la ruta con la nueva imagen
                    dto.ImagenRuta = "/images/products/" + fileName;
                }

                var result = await _mediator.Send(dto.Adapt<UpdateProductCommand>());
                if (result > 0) return RedirectToAction(nameof(Index));

                return View(dto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
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