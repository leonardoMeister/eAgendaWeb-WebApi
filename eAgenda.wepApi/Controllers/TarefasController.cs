using AutoMapper;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.wepApi.ViewModels.TarefaModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
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

            var listaErros = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros
                });
            }


            var tarefaResult = servico.SelecionarPorId(id);

            if (tarefaResult.Errors.Any(x => x.Message.Contains("não encontrada")))
            {
                return NotFound(new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }

            var tarefaEditada = mapeadorTarefas.Map(tarefaVm, tarefaResult.Value);

             tarefaResult = servico.Editar(tarefaEditada);

            if (tarefaResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }

            if (tarefaResult.IsSuccess) return tarefaVm;

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

            if (tarefaResult.IsFailed)
            {
                return StatusCode(500,
                    new
                    {
                        sucesso = false,
                        erros = tarefaResult.Errors.Select(x => x.Message)
                    });
            }

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

            var listaErros = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros
                });
            }

            var tarefa = mapeadorTarefas.Map<Tarefa>(tarefaVm);
            var tarefaResult = servico.Inserir(tarefa);

            if (tarefaResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }

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


            if (tarefaResult.Errors.Any(x => x.Message.Contains("não encontrada")))
            {
                return NotFound(new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }
            if (tarefaResult.IsFailed)
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });

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

            if (tarefaResult.Errors.Any(x => x.Message.Contains("não encontrada")))
            {
                return NotFound(new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }

            if (tarefaResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }

            return NoContent();
        }
    }
}