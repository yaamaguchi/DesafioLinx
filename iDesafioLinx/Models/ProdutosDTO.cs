using Microsoft.AspNetCore.Http;

namespace iDesafioLinx
{
    public class ProdutosDTO
    {
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
