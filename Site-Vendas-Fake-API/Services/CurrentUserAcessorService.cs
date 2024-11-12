using System.Security.Claims;
using Site_Vendas_Fake_API.Exceptions;

namespace Site_Vendas_Fake_API.Services;

public class CurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) 
                            ?? throw new UserNotFoundException("Usuário não encontrado");
    
    public bool IsLoggedIn => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}