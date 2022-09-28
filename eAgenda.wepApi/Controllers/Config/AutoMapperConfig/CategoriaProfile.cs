using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.wepApi.ViewModels.CategoriaModels;
using eAgenda.wepApi.ViewModels.DespesaModels;
using System;

namespace eAgenda.wepApi.Controllers.Config.AutoMapperConfig
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            ConverterDeEntidadeParaViewMovel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeEntidadeParaViewMovel()
        {
            CreateMap<Categoria, ListarCategoriaViewModel>();

            CreateMap<Categoria, VisualizarCategoriaViewModels>()                   
                   .ForMember(destino => destino.Despesas, opt => opt.Ignore())
                   .AfterMap((categoria, categoriaViewModel) =>
                   {
                       if (categoria.Despesas == null) return;

                       foreach (var itemAntigo in categoria.Despesas)
                       {
                           var item = new ListarDespesaViewModels();
                           
                           item.Id = itemAntigo.Id;
                           item.Descricao = itemAntigo.Descricao;
                           item.Valor = itemAntigo.Valor;
                           item.Data = itemAntigo.Data;
                           item.FormaPagamento = itemAntigo.FormaPagamento;

                           categoriaViewModel.Despesas.Add(item);
                       }
                   });

        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<EditarCategoriaViewModel, Categoria>();
            CreateMap<InserirCategoriaViewModel, Categoria>();

        }
    }
}
