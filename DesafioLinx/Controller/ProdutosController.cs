using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using DesafioLinx.Models;
using DesafioLinx.Business;
using BD.Dal;


namespace DesafioLinx.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly Service _services;

        public ProdutosController(Service services)
        {
            _services = services;
        }

        [HttpGet]
        public IEnumerable<Produtos> GetAll()
        {
            return _services.List();
        }

        [HttpGet("{codigoBarras}")]
        [ProducesResponseType(typeof(Produtos), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Produtos> Get(string codigoBarras)
        {
            var produto = GetProduto(codigoBarras);
            if (produto == null)
                return NotFound();

            return produto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public ActionResult<Resultado> Post(Produtos cadastro)
        {
            var resultado = _services.Insert(cadastro);
            if (resultado.Inconsistencias.Count > 0)
            {
                return BadRequest(resultado);
            }

            return resultado;
        }

        [HttpPut]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public ActionResult<Resultado> Put(Produtos cadastro)
        {
            if (GetProduto(cadastro.CodigoBarras) == null)
                return NotFound();

            var resultado = _services.Update(cadastro);
            if (resultado.Inconsistencias.Count > 0)
            {
                return BadRequest(resultado);
            }

            return resultado;
        }

        [HttpDelete("{codigoBarras}")]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Resultado> Delete(string codigoBarras)
        {
            var produto = GetProduto(codigoBarras);
            if (produto == null)
                return NotFound();

            return _services.Delete(produto.CodigoBarras);
        }

        private string GetJSONResultado(Resultado resultado)
        {
            return JsonSerializer.Serialize(resultado);
        }

        private Produtos GetProduto(string codigoBarras)
        {
            var produto = _services.GetByCodigoBarras(codigoBarras);

            return produto;
        }
    }
}