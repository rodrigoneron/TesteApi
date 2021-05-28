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
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] AppDbContext context)
        {
            var produtos = await context.Produtos.ToListAsync();
            return produtos;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] AppDbContext context, int id)
        {
            var produtos = await context.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(produtos);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Post([FromServices] AppDbContext context, [FromBody] Produto model)
        {
            if (ModelState.IsValid)
            {
                context.Produtos.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromServices] AppDbContext context, [FromBody] Produto model, int id)
        {
            var ProdutoAntigo = context.Produtos.First(c => c.Id == id);

            if (ProdutoAntigo != null)
            {
                ProdutoAntigo.Descricao = String.IsNullOrEmpty(model.Descricao) ? ProdutoAntigo.Descricao : model.Descricao;
                ProdutoAntigo.Valor = model.Valor == 0 ? ProdutoAntigo.Valor : model.Valor;
                ProdutoAntigo.Foto = String.IsNullOrEmpty(model.Foto) ? ProdutoAntigo.Foto : model.Foto;
                context.SaveChanges();
                return Ok(ProdutoAntigo);
            }
            return BadRequest("Algo deu errado!");
        }


        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromServices] AppDbContext context, int id)
        {
            var ProdutoAntigo = context.Produtos.First(c => c.Id == id);

            if (ProdutoAntigo != null)
            {

                context.Produtos.Remove(ProdutoAntigo);
                context.SaveChanges();
                return Ok(ProdutoAntigo);
            }
            return BadRequest("Algo deu errado!");
        }
    }
}
