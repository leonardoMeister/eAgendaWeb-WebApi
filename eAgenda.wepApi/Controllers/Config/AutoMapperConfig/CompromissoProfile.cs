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
                        (origem.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial) ? "Encontro Presencial" : "Encontro Remoto"))
                .ForMember(destino => destino.DescricaoLocal,
                  opt => opt.MapFrom(origem =>
                        (origem.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial) ? $"Local: {origem.Local}" : $"Link: {origem.Link}"));

            CreateMap<Compromisso, VisualizarCompromissoViewModel>()
                 .ForMember(destino => destino.TipoLocal,
                  opt => opt.MapFrom(origem =>
                        (origem.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial) ? "Encontro Presencial" : "Encontro Remoto"))
                .ForMember(destino => destino.DescricaoLocal,
                  opt => opt.MapFrom(origem =>
                        (origem.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial) ? $"Local: {origem.Local}" : $"Link: {origem.Link}"));

        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<InserirCompromissoViewModel, Compromisso>()
                .ForMember(destino => destino.Contato, opt => opt.Ignore())
                .ForMember(destino => destino.HoraInicio,
                    opt => opt.MapFrom(origem => origem.Data.ToString("HH:mm:ss")))
                .ForMember(destino => destino.HoraTermino,
                opt => opt.MapFrom(origem => origem.DataTermino.ToString("HH:mm:ss")))
                .AfterMap((viewModel, compromisso) =>
                {
                    PegandoLinkOuLocalCompromisso(viewModel, compromisso);
                });

            CreateMap<EditarCompromissoViewModel, Compromisso>()
                .ForMember(destino => destino.Data, opt => opt.Ignore())
                .ForMember(destino => destino.Contato, opt => opt.Ignore())
                .ForMember(destino => destino.HoraInicio,
                    opt => opt.MapFrom(origem => origem.Data.ToString("HH:mm:ss")))
                .ForMember(destino => destino.HoraTermino,
                opt => opt.MapFrom(origem => origem.DataTermino.ToString("HH:mm:ss")))
                .AfterMap((viewModel, compromisso) =>
                {
                    PegandoLinkOuLocalCompromisso(viewModel, compromisso);
                });

        }

        private void PegandoLinkOuLocalCompromisso(FormsCompromissoViewModel viewModel, Compromisso compromisso)
        {
            if (viewModel.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial)
                compromisso.Local = viewModel.DescricaoLocalLink;
            else
                compromisso.Link = viewModel.DescricaoLocalLink;
        }
    }
}
