using System;
using System.Collections.Generic;
using DesafioLinx.Models;
using BD.Dal;

namespace DesafioLinx.Business
{
    public class Service
    {
        private readonly IDalProdutos _dal;

        public Service (IDalProdutos dal)
        {
            _dal = dal;
        }

        public IEnumerable<Produtos> List()
        {
            return _dal.List();
        }

        public Produtos GetByCodigoBarras(string codigoBarras)
        {
            return _dal.FindByCodigoBarrasORNome(codigoBarras, "");
        }


        public Resultado Insert(Produtos cadastro)
        {
            var resultado = new Resultado()
            {
                Acao = "Inclusão de Produto"
            };

            if (_dal.FindByCodigoBarrasORNome(cadastro.CodigoBarras, "") != null)
            {
                resultado.Inconsistencias.Add("Código de Barras já cadastrado");
            }
            else
                _dal.Insert(cadastro);

            return resultado;
        }

        public Resultado Update(Produtos cadastro)
        {
            var resultado = new Resultado()
            {
                Acao = "Atualização de Produto"
            };

            if (!_dal.Update(cadastro))
            {
                resultado.Inconsistencias.Add("Não foi possível alterar o Produto");
            }

            return resultado;
        }

        public Resultado Delete(string codigoBarras)
        {
            var resultado = new Resultado()
            {
                Acao = "Exclusão de Produto"
            };

            if (String.IsNullOrWhiteSpace(codigoBarras))
            {
                resultado.Inconsistencias.Add("Código de Barras não informado");
            }
            else if (!_dal.Delete(codigoBarras))
            {
                resultado.Inconsistencias.Add("Não foi possível excluir o Produto");
            }

            return resultado;
        }
    }
}
