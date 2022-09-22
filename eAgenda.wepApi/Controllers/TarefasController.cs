using AutoMapper;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.wepApi.Controllers.Config.AutoMapperConfig;
using eAgenda.wepApi.ViewModels.TarefaModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ServicoTarefa servico;
        private IMapper mapeadorTarefas;

        public TarefasController()
        {
            var config = new ConfiguracaoAplicacaoeAgenda();
            var dbContexto = new eAgendaDbContext(config.ConnectionStrings);
            var repo = new RepositorioTarefaOrm(dbContexto);
            servico = new ServicoTarefa(repo, dbContexto);

            var automapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<TarefaProfile>();

            });

            mapeadorTarefas = automapperConfig.CreateMapper();
        }

        [HttpPut("{id:guid}")]
        public EditarTarefaViewModel Editar(Guid id, EditarTarefaViewModel tarefaVm)
        {
            var tarefaSelecionada = servico.SelecionarPorId(id).Value;

            var tarefaEditada = mapeadorTarefas.Map(tarefaVm, tarefaSelecionada);

            var tarefaResult = servico.Editar(tarefaEditada);

            if (tarefaResult.IsSuccess) return tarefaVm;

            return null;
        }

        [HttpGet]
        public List<ListarTarefaViewModel> SelecionarTodasTarefas()
        {
            var tarefaResult = servico.SelecionarTodos(StatusTarefaEnum.Todos).Value;
            return mapeadorTarefas.Map<List<ListarTarefaViewModel>>(tarefaResult);
        }

        [HttpPost]
        public InserirTarefaViewModel Inserir(InserirTarefaViewModel tarefaVm)
        {
            var tarefa = mapeadorTarefas.Map<Tarefa>(tarefaVm);
            var tarefaResult = servico.Inserir(tarefa);

            if (tarefaResult.IsSuccess) return tarefaVm;
            return null;
        }

        [HttpGet("visualizar-completa/{id}")]
        public VisualizarTarefaViewModel SelecionarPorId(string id)
        {
            var tarefaResult = servico.SelecionarPorId(Guid.Parse(id)).Value;
            return mapeadorTarefas.Map<VisualizarTarefaViewModel>(tarefaResult);
        }

        [HttpDelete("{id:guid}")]
        public void Excluir(Guid id)
        {
            var tarefaEditada = servico.Excluir(id);
        }
    }
}