using AutoMapper;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.wepApi.ViewModels.CompromissoModels;
using System;

namespace eAgenda.wepApi.Controllers.Config.AutoMapperConfig
{
    public class CompromissoProfile : Profile
    {
        public CompromissoProfile()
        {
            ConverterDeEntidadeParaViewMovel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeEntidadeParaViewMovel()
        {
            CreateMap<Compromisso, ListarCompromissosViewModel>()
                .ForMember(destino => destino.NomeContato, opt => opt.MapFrom(origem => origem.Contato.Nome))
                 .ForMember(destino => destino.TipoLocal, 
                  opt => opt.MapFrom(origem => 
                        (origem.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial)?"Encontro Presencial": "Encontro Remoto"))
                .ForMember(destino => destino.DescricaoLocal,
                  opt => opt.MapFrom(origem =>
                        (origem.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial) ? $"Local: {origem.Local}" : $"Link: {origem.Link}"));
        }

        private void ConverterDeViewModelParaEntidade()
        {
        }
    }
}
