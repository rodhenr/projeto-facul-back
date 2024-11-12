using Microsoft.AspNetCore.Mvc;
using Site_Vendas_Fake_API.Services;

namespace Site_Vendas_Fake_API.Controllers;

public class PedidosController : BaseController
{
    private readonly PedidoService _pedidoService;
    
    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<List<PedidoDto>> BuscarPedidos()
    {
        return await _pedidoService.BuscarPedidos();
    }
}