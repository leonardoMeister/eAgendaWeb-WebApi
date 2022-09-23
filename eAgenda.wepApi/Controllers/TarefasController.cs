using AutoMapper;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.wepApi.Controllers.Shared;
using eAgenda.wepApi.ViewModels.TarefaModels;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : eAgendaControllerBase
    {
        private readonly ServicoTarefa servico;
        private IMapper mapeadorTarefas;

        public TarefasController(ServicoTarefa servicoTarefa, IMapper mapeadorTarefas)
        {
            this.servico = servicoTarefa;
            this.mapeadorTarefas = mapeadorTarefas;
        }


        [HttpPut("{id:guid}")]
        public ActionResult<EditarTarefaViewModel> Editar(Guid id, EditarTarefaViewModel tarefaVm)
        {
            var tarefaResult = servico.SelecionarPorId(id);

            if (RegistroNaoEncontrado(tarefaResult)) return NotFoundInterno(tarefaResult);

            var tarefaEditada = mapeadorTarefas.Map(tarefaVm, tarefaResult.Value);
            tarefaResult = servico.Editar(tarefaEditada);

            if (tarefaResult.IsFailed) return InternalError(tarefaResult);            

            return Ok(new
            {
                sucesso = true,
                dados = tarefaVm
            });
        }

        [HttpGet]
        public ActionResult<List<ListarTarefaViewModel>> SelecionarTodasTarefas()
        {
            var tarefaResult = servico.SelecionarTodos(StatusTarefaEnum.Todos);

            if (tarefaResult.IsFailed) return InternalError(tarefaResult);

            return Ok(
                new
                {
                    sucesso = true,
                    dados = mapeadorTarefas.Map<List<ListarTarefaViewModel>>(tarefaResult.Value)
                });
        }

        [HttpPost]
        public ActionResult<InserirTarefaViewModel> Inserir(InserirTarefaViewModel tarefaVm)
        {            
            var tarefa = mapeadorTarefas.Map<Tarefa>(tarefaVm);
            var tarefaResult = servico.Inserir(tarefa);

            if (tarefaResult.IsFailed) return InternalError(tarefaResult);

            return Ok(new
            {
                sucesso = true,
                dados = tarefaVm
            });
        }

        [HttpGet("visualizar-completa/{id}")]
        public ActionResult<VisualizarTarefaViewModel> SelecionarPorId(string id)
        {
            var tarefaResult = servico.SelecionarPorId(Guid.Parse(id));

            if (RegistroNaoEncontrado(tarefaResult)) return NotFoundInterno(tarefaResult);
            if (tarefaResult.IsFailed) return InternalError(tarefaResult);

            return Ok(new
            {
                dados = mapeadorTarefas.Map<VisualizarTarefaViewModel>(tarefaResult.Value),
                sucesso = true
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var tarefaResult = servico.Excluir(id);

            if (RegistroNaoEncontrado<Tarefa>(tarefaResult)) return NotFoundInterno<Tarefa>(tarefaResult);
            if (tarefaResult.IsFailed) return InternalError<Tarefa>(tarefaResult);

            return NoContent();
        }
      
    }
}