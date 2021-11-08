using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using BD.Dal;
using AutoMapper;
using System;

namespace iDesafioLinx
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly Service _services;
        private readonly IMapper _mapper;

        public ProdutosController(Service services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
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
        public ActionResult<Resultado> Post([FromForm] ProdutosDTO dto)
        {

            var imagebyte = "";

            if (dto.Imagem != null && dto.Imagem.Length > 0)
            {
                if (dto.Imagem.ContentType.Contains("image"))
                {
                    using (var ms = new MemoryStream())
                    {
                        dto.Imagem.CopyTo(ms);
                        imagebyte = Convert.ToBase64String(ms.ToArray());
                    }

                }
                else
                {
                    var erro = new Resultado();
                    erro.Inconsistencias.Add("Arquivo enviado não é uma imagem!");
                    return BadRequest(erro); 
                }
            }


            var cadastro = _mapper.Map<Produtos>(dto);
            cadastro.Imagem = imagebyte;

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
        public ActionResult<Resultado> Put([FromForm] ProdutosDTO dto)
        {
            var imagebyte = "";
            if (dto.Imagem != null && dto.Imagem.Length > 0)
            {
                if (dto.Imagem.ContentType.Contains("image"))
                {
                    using (var ms = new MemoryStream())
                    {
                        dto.Imagem.CopyTo(ms);
                        imagebyte = Convert.ToBase64String(ms.ToArray());
                    }
                }
                else
                {
                    var erro = new Resultado();
                    erro.Inconsistencias.Add("Arquivo enviado não é uma imagem!");
                    return BadRequest(erro);
                }
            }
            var cadastro = _mapper.Map<Produtos>(dto);
            cadastro.Imagem = imagebyte;

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