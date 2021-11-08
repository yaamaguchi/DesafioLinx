using System.Collections.Generic;

namespace BD.Dal
{
    public class Produtos
    {
        public Produtos() { }
        public Produtos(string nome, string codigoBarras, double preco, string imagem)
        {
            CodigoBarras = codigoBarras;
            Preco = preco;
            Nome = nome;
            Imagem = imagem;
        }

        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Imagem { get; set; }

    }
}