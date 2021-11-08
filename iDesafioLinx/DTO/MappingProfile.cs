

using AutoMapper;
using BD.Dal;

namespace iDesafioLinx
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produtos, ProdutosDTO>();
            CreateMap<ProdutosDTO, Produtos>();
        }
    }
}
