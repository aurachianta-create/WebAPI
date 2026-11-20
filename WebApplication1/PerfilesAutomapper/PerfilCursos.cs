using AutoMapper;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.PerfilesAutomapper
{
    public class PerfilCursos : Profile
    {
        public PerfilCursos()
        {
             CreateMap<Curso, CursoDto>();
             CreateMap<CursoDto, Curso>();
        }
    }
}
