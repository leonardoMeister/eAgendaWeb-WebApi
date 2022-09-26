using AutoMapper;
using eAgenda.Dominio.ModuloContato;
using eAgenda.wepApi.ViewModels.ContatoModels;
using System;

namespace eAgenda.wepApi.Controllers.Config.AutoMapperConfig
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            ConverterDeEntidadeParaViewModel();

            ConverterdeViewModelParaentidade();
        }

        private void ConverterdeViewModelParaentidade()
        {
            CreateMap<InserirContatoViewModel, Contato>();

            CreateMap<EditarContatoViewModel, Contato>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Contato, VisualizarContatoViewModel>();
        }
    }
}
