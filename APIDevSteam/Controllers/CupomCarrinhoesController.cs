using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevSteam.Data;
using APIDevSteam.Models;
using APIDevSteam.Migrations;

namespace APIDevSteam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupomCarrinhoesController : ControllerBase
    {
        private readonly APIContext _context;

        public CupomCarrinhoesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/CupomCarrinhoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CupomCarrinhos>>> GetCuponsCarrinho()
        {
            return await _context.CuponsCarrinhos.ToListAsync();
        }

        // GET: api/CupomCarrinhoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CupomCarrinhos>> GetCupomCarrinho(Guid id)
        {
            var cupomCarrinho = await _context.CuponsCarrinhos.FindAsync(id);

            if (cupomCarrinho == null)
            {
                return NotFound();
            }

            return cupomCarrinho;
        }

        // PUT: api/CupomCarrinhoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupomCarrinho(Guid id, CupomCarrinhos cupomCarrinho)
        {
            if (id != cupomCarrinho.CupomCarrinhoId)
            {
                return BadRequest();
            }

            _context.Entry(cupomCarrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CupomCarrinhoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CupomCarrinhoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CupomCarrinhos>> PostCupomCarrinho(CupomCarrinhos cupomCarrinho)
        {
            _context.CuponsCarrinhos.Add(cupomCarrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCupomCarrinho", new { id = cupomCarrinho.CupomCarrinhoId }, cupomCarrinho);
        }

        // DELETE: api/CupomCarrinhoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupomCarrinho(Guid id)
        {
            var cupomCarrinho = await _context.CuponsCarrinhos.FindAsync(id);
            if (cupomCarrinho == null)
            {
                return NotFound();
            }

            _context.CuponsCarrinhos.Remove(cupomCarrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CupomCarrinhoExists(Guid id)
        {
            return _context.CuponsCarrinhos.Any(e => e.CupomCarrinhoId == id);
        }


        [HttpPost("DescontoValorTotal/{carrinhoId}/{cupomId}")]
        public async Task<IActionResult> AplicarCupom(Guid carrinhoId, Guid cupomId)
        {
            //se carrinho existe
            var carrinho = await _context.Carrinhos.FindAsync(carrinhoId);
            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado.");
            }
            //se cupom existe
            var cupom = await _context.Cupons.FindAsync(cupomId);
            if (cupom == null)
            {
                return NotFound("Cupom não encontrado.");
            }
            //se cupom está ativo
            if (cupom.Ativo == false)
            {
                return BadRequest("Cupom não está ativo.");
            }
            //se o pucom já foi aplicado no carrinho
            var cupomCarrinho = await _context.CuponsCarrinhos
                .FirstOrDefaultAsync(cc => cc.CarrinhoId == carrinhoId && cc.CupomId == cupomId);
            if (cupomCarrinho != null)
                return Ok(
                    new { ValorTotal = carrinho.ValorTotal, Desconto = cupomCarrinho.DescontoAplicado }
                );
            //se o carrinho já tem um cupom aplicado
            var cupomCarrinhoExistente = await _context.CuponsCarrinhos
                .FirstOrDefaultAsync(cc => cc.CarrinhoId == carrinhoId);


            {
                //se cupom não está expirado
                if (cupom.DataValidade < DateTime.Now)
                {
                    return BadRequest("Cupom expirado.");
                }
                //se cupom já foi utilizado
                if (cupom.LimiteUso <= 0)
                {
                    return BadRequest("Cupom já foi utilizado.");
                }
                //calcular o desconto do cupom no valor total do carrinho
                var desconto = (carrinho.ValorTotal * cupom.Desconto) / 100;
                carrinho.ValorTotal -= desconto;
                //atualizar o limite de uso do cupom
                cupom.LimiteUso--;
                //atualizar o carrinho
                _context.Entry(carrinho).State = EntityState.Modified;
                //atualizar o cupom
                _context.Entry(cupom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { ValorTotal = carrinho.ValorTotal, Desconto = desconto });







            }


        }
    }
