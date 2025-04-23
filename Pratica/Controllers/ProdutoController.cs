using Microsoft.AspNetCore.Mvc;
using Pratica.Models;

namespace Pratica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private static List<Produto> produtos = new List<Produto>() 
        {
            new Produto { Id = 1, Nome = "Mouse", Preco = 89.90M },
            new Produto { Id = 2, Nome = "Teclado", Preco = 500.00M }
        };

        // GET: Solicita informações para o servidor
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> ListarProdutos() 
        {
            return Ok(produtos);
        }

        [HttpGet("{pId}")]
        public ActionResult<Produto> BuscarProduto(int pId) 
        {
            try
            {
                var produto = produtos.FirstOrDefault(p => p.Id == pId);

                if (produto == null)
                    return NotFound();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Envia informações para o servidor
        [HttpPost]
        public ActionResult<Produto> Cadastrar([FromBody]Produto pProduto)
        {
            pProduto.Id = produtos.Max(p => p.Id) + 1;
            produtos.Add(pProduto);

            return CreatedAtAction(nameof(BuscarProduto), new { pId = pProduto.Id }, pProduto);
        }

        // PUT: Responsável por inserer e ou atualizar uma informação do servidor
        [HttpPut("{pId}")]
        public ActionResult Atualiza(int pId, [FromBody]Produto pProdutoAtualizado) 
        {
            Produto? produto = produtos.FirstOrDefault(p => p.Id == pId);

            if (produto == null)
                return NotFound();

            produto.Nome = pProdutoAtualizado.Nome;
            produto.Preco = pProdutoAtualizado.Preco;

            return NoContent();
        }

        // DELETE: Responsável por remover informação do sistema.
        [HttpDelete("{pId}")]
        public ActionResult Remover(int pId)
        {
            Produto? produto = produtos.FirstOrDefault(p => p.Id == pId);

            if (produto == null) 
                return NotFound();

            produtos.Remove(produto);

            return NoContent();
        }
    }
}
