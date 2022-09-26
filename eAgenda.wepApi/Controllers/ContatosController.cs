using AutoMapper;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.wepApi.Controllers.Shared;
using eAgenda.wepApi.ViewModels.ContatoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : eAgendaControllerBase
    {
        private readonly ServicoContato servico;
        private IMapper mapeadorContatos;

        public ContatosController(ServicoContato servico, IMapper mapeadorContatos)
        {
            this.servico = servico;
            this.mapeadorContatos = mapeadorContatos;
        }


        [HttpGet("selecionar-todos")]
        public ActionResult<List<VisualizarContatoViewModel>> SelecionarTodosContatos()
        {
            var contatoResult = servico.SelecionarTodos();

            if (contatoResult.IsFailed) return InternalError(contatoResult);

            return Ok(
                new
                {
                    sucesso = true,
                    dados = mapeadorContatos.Map<List<VisualizarContatoViewModel>>(contatoResult.Value)
                });
        }

        [HttpPost("inserir")]
        public ActionResult<InserirContatoViewModel> Inserir(InserirContatoViewModel contatoVm)
        {
            var contato = mapeadorContatos.Map<Contato>(contatoVm);
            var contatoResult = servico.Inserir(contato);

            if (contatoResult.IsFailed) return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = contatoVm
            });
        }

        [HttpPut("editar/{id:guid}")]
        public ActionResult<EditarContatoViewModel> Editar(Guid id, EditarContatoViewModel contatoVm)
        {
            var contatoResult = servico.SelecionarPorId(id);

            if (RegistroNaoEncontrado(contatoResult)) return NotFoundInterno(contatoResult);

            var contatoEditado = mapeadorContatos.Map(contatoVm, contatoResult.Value);
            contatoResult = servico.Editar(contatoEditado);

            if (contatoResult.IsFailed) return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = contatoVm
            });
        }

        [HttpGet("selecionar-id/{id}")]
        public ActionResult<VisualizarContatoViewModel> SelecionarPorId(string id)
        {
            var contatoResult = servico.SelecionarPorId(Guid.Parse(id));

            if (RegistroNaoEncontrado(contatoResult)) return NotFoundInterno(contatoResult);
            if (contatoResult.IsFailed) return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<VisualizarContatoViewModel>(contatoResult.Value)
            });
        }

        [HttpDelete("excluir/{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var contatoSelecionado = servico.SelecionarPorId(id);
            if (contatoSelecionado.IsFailed) return InternalError(contatoSelecionado);

            var contatoResult = servico.Excluir(contatoSelecionado.Value);

            if (RegistroNaoEncontrado<Contato>(contatoResult)) return NotFoundInterno<Contato>(contatoResult);
            if (contatoResult.IsFailed) return InternalError<Contato>(contatoResult);

            return Ok(new
            {
                sucesso = true
            });
        }

    }
}
