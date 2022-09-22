using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.wepApi.ViewModels.TarefaModels;

namespace eAgenda.wepApi.Controllers.Config.AutoMapperConfig
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, ListarTarefaViewModel>()
                        .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origrem => origrem.Prioridade.GetDescription()))
                        .ForMember(destino => destino.Situacao, opt =>
                           opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluido" : "Pendente"));
            CreateMap<Tarefa, VisualizarTarefaViewModel>()
                .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origrem => origrem.Prioridade.GetDescription()))
                .ForMember(destino => destino.Situacao, opt =>
                   opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluido" : "Pendente"))
                .ForMember(destino => destino.QuantidadeItens, opt => opt.MapFrom(x => x.Itens.Count));
            CreateMap<ItemTarefa, VisualizarItenTarefaViewModel>().
                ForMember(destino => destino.Situacao, opt =>
                    opt.MapFrom(origem => origem.Concluido ? "Concluido" : "Pendente"));

            CreateMap<InserirTarefaViewModel, Tarefa>()
                .ForMember(destino => destino.Itens, opt => opt.Ignore())
                .AfterMap((viewModel, tarefa) =>
                {
                    foreach (var itemAntigo in viewModel.Itens)
                    {
                        var itemNovo = new ItemTarefa();
                        itemNovo.Titulo = itemAntigo.Titulo;
                        tarefa.AdicionarItem(itemNovo);
                    }
                });

            CreateMap<EditarTarefaViewModel, Tarefa>()
               .ForMember(destino => destino.Itens, opt => opt.Ignore())
               .AfterMap((viewModel, tarefa) =>
               {
                   CarregandoDependenciasDeEdicaoTarefa(viewModel, tarefa);
               });
        }

        private void CarregandoDependenciasDeEdicaoTarefa(EditarTarefaViewModel viewModel, Tarefa tarefa)
        {
            foreach (var itemVM in viewModel.Itens)
            {
                if (itemVM.Concluido)
                    tarefa.ConcluirItem(itemVM.Id);
                else
                    tarefa.MarcarPendente(itemVM.Id);
            }
            foreach (var itemVM in viewModel.Itens)
            {
                if (itemVM.Status == StatusItemTarefa.Adicionado)
                    tarefa.AdicionarItem(new ItemTarefa(itemVM.Titulo));
                else if (itemVM.Status == StatusItemTarefa.Removido)
                    tarefa.RemoverItem(itemVM.Id);
            }
        }

    }
}
