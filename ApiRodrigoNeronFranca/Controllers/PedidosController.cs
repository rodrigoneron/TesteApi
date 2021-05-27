using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRodrigoNeronFranca.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Pedido>>> Get([FromServices] AppDbContext context)
        {
            var pedidos = await context.Pedidos.Include(x => x.Produtos).Include(x => x.Cliente).ToListAsync();
            return pedidos;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Pedido>>> Get([FromServices] AppDbContext context, int id)
        {
            var pedidos = await context.Pedidos.Include(x => x.Produtos).Include(x => x.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(pedidos);
        }

        [HttpGet]
        [Route("clientes/{id:int}")]
        public async Task<ActionResult<List<Pedido>>> GetbyCliente([FromServices] AppDbContext context, int id)
        {
            var pedidos = await context.Pedidos
                .Include(x => x.Produtos)
                .Include(x => x.Cliente).Where(x => x.ClienteId == id).ToListAsync();
            return Ok(pedidos);
        }

        [HttpGet]
        [Route("produtos/{id:int}")]
        public async Task<ActionResult<List<Pedido>>> GetbyProduto([FromServices] AppDbContext context, int id)
        {
            var pedidos = await context.Pedidos
                .Include(x => x.Produtos).Where(x => x.ProdutosId == id)
                .Include(x => x.Cliente).ToListAsync();
            return Ok(pedidos);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Pedido>>> Post([FromServices] AppDbContext context, [FromBody] Pedido model)
        {
            if (ModelState.IsValid)
            {
                context.Pedidos.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Put([FromServices] AppDbContext context, [FromBody] Pedido model, int id)
        {
            var PedidoAntigo = context.Pedidos.First(c => c.Id == id);

            if (PedidoAntigo != null)
            {
                PedidoAntigo.Numero = model.Numero == 0 ? PedidoAntigo.Numero : model.Numero;
                PedidoAntigo.Data = model.Data.ToShortDateString() == null ? PedidoAntigo.Data : model.Data;
                PedidoAntigo.Valor = model.Valor == 0  ? PedidoAntigo.Valor : model.Valor;
                PedidoAntigo.Quantidade = model.Quantidade == 0 ? PedidoAntigo.Quantidade : model.Quantidade;
                PedidoAntigo.Desconto = model.Desconto == 0 ? PedidoAntigo.Desconto : model.Desconto;
                context.SaveChanges();
                return Ok(PedidoAntigo);
            }
            return BadRequest("Algo deu errado!");
        }


        [HttpDelete]
        public IActionResult Delete([FromServices] AppDbContext context, int id)
        {
            var PedidoAntigo = context.Pedidos.First(c => c.Id == id);

            if (PedidoAntigo != null)
            {

                context.Pedidos.Remove(PedidoAntigo);
                context.SaveChanges();
                return Ok(PedidoAntigo);
            }
            return BadRequest("Algo deu errado!");
        }
    }
}
