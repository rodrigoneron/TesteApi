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
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> Get([FromServices] AppDbContext context)
        {
            var clientes = await context.Clientes.ToListAsync();
            return clientes;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Cliente>>> Get([FromServices] AppDbContext context, int id)
        {
            var clientes = await context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(clientes);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> Post([FromServices] AppDbContext context, [FromBody] Cliente model)
        {
            if (ModelState.IsValid)
            {
                context.Clientes.Add(model);
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
        public IActionResult Put([FromServices] AppDbContext context, [FromBody] Cliente model, int id)
        {
            var ClienteAntigo = context.Clientes.First(c => c.Id == id);

            if (ClienteAntigo != null)
            {
                ClienteAntigo.Nome = String.IsNullOrEmpty(model.Nome) ? ClienteAntigo.Nome : model.Nome;
                ClienteAntigo.Email = String.IsNullOrEmpty(model.Email) ? ClienteAntigo.Email : model.Email;
                ClienteAntigo.Aldeia = String.IsNullOrEmpty(model.Aldeia) ? ClienteAntigo.Aldeia : model.Aldeia;
                context.SaveChanges();
                return Ok(ClienteAntigo);
            }
            return BadRequest("Algo deu errado!");
        }


        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromServices] AppDbContext context, int id)
        {
            var cadastro = context.Clientes.First(c => c.Id == id);

            if (cadastro != null)
            {
                context.Clientes.Remove(cadastro);
                context.SaveChanges();
                return Ok("Cadastro Removido!");
            }
            return BadRequest("Cadastro não existe!");
        }
    }
}
