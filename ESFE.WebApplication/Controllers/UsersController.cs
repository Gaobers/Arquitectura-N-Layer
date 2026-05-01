using ESFE.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

// Namespaces de tu Lógica de Negocio

namespace ProyectoWeb.Controllers;

public class GetRolesQuery : IRequest<IEnumerable<Role>>
{ }

public class GetUserAuthenticatedQuery : IRequest<User> { public string UserName { get; set; } public string Password { get; set; } }
public class GetUserQuery : IRequest<IEnumerable<User>>
{ }

public class UsuarioController : Controller
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(GetUserAuthenticatedQuery query)
    {
        try
        {
            var user = await _mediator.Send(query);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name,
                              user.UserName),
                    new Claim("Id", user.UserId.ToString())
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)
                );

                return RedirectToAction("Index");
            }

            throw new Exception("Credenciales incorrectas");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Credenciales incorrectas");
            return View(query);
        }
    }

    public async Task<IActionResult> Index()
    {
        var users = await _mediator.Send(new GetUserQuery());
        return View(users);
    }

    public async Task<IActionResult> Create()
    {
        var roles = await _mediator.Send(new GetRolesQuery());
        ViewData["RolId"] = new SelectList(roles, "RolId", "RolName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        try
        {
            await _mediator.Send(new CreateUserCommand(request));
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Sucedió un error al intentar guardar un nuevo usuario.");
            return View(request);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var users = await _mediator.Send(new GetUserQuery());
        var user = users.FirstOrDefault(x => x.UserId == id);

        var roles = await _mediator.Send(new GetRolesQuery());
        ViewData["RolId"] = new SelectList(roles, "RolId", "RolName");

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
        try
        {
            await _mediator.Send(new UpdateUserCommand(user));
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Sucedió un error al intentar editar el usuario.");
            return View(user);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var users = await _mediator.Send(new GetUserQuery());
        var user = users.FirstOrDefault(x => x.UserId == id);

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Sucedió un error al intentar eliminar el usuario.");
            return View();
        }
    }

    public async Task<IActionResult> CerrarSesion()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToAction("Login");
    }
}

