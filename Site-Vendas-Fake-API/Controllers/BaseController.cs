using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Site_Vendas_Fake_API.Controllers;

[Authorize]
[Route("api/[controller]")]
[EnableCors]
public class BaseController : Controller
{
    
}