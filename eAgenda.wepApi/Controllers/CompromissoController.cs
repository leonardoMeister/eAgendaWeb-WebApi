using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.wepApi.Controllers.Shared;
using eAgenda.wepApi.ViewModels.CompromissoModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompromissoController : eAgendaControllerBase
    {
        private readonly ServicoCompromisso servico;
        private readonly ServicoContato servicoContato;
        private IMapper mapeadorCompromisso;


        public CompromissoController(ServicoCompromisso servicoCompromisso,ServicoContato servicoContato, IMapper mapeadorCompromisso)
        {
            this.servico = servicoCompromisso;
            this.mapeadorCompromisso = mapeadorCompromisso;
            this.servicoContato = servicoContato;
        }

        [HttpPut("editar/{id:guid}")]
        public ActionResult<EditarCompromissoViewModel> Editar(Guid id, EditarCompromissoViewModel contatoVm)
        {
            var CompromissoAntigoResult = servico.SelecionarPorId(id);
            if (RegistroNaoEncontrado(CompromissoAntigoResult)) return NotFoundInterno(CompromissoAntigoResult);

            var resultadoContato = servicoContato.SelecionarPorId(contatoVm.ContatoId);
            if (resultadoContato.IsFailed) return InternalError(resultadoContato);

            var contatoEditado = mapeadorCompromisso.Map(contatoVm, CompromissoAntigoResult.Value);
            contatoEditado.Contato = resultadoContato.Value;


            CompromissoAntigoResult = servico.Editar(contatoEditado);

            if (CompromissoAntigoResult.IsFailed) return InternalError(CompromissoAntigoResult);

            return Ok(new
            {
                sucesso = true,
                dados = contatoVm
            });
        }

        [HttpPost("inserir")]
        public ActionResult<InserirCompromissoViewModel> Inserir(InserirCompromissoViewModel compromissoVm)
        {
            var resultadoContato = servicoContato.SelecionarPorId(compromissoVm.ContatoId);
            if (resultadoContato.IsFailed) return InternalError(resultadoContato);

            var compromisso= mapeadorCompromisso.Map<Compromisso>(compromissoVm);
            compromisso.Contato = resultadoContato.Value;

            var compromissoResult = servico.Inserir(compromisso);

            if (compromissoResult.IsFailed) return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVm
            });
        }


        [HttpGet("visualizar-todos")]
        public ActionResult<List<ListarCompromissosViewModel>> SelecionarTodosCompromissos()
        {
            var compromissoResult = servico.SelecionarTodosComContato();

            if (compromissoResult.IsFailed) return InternalError(compromissoResult);

            return Ok(
                new
                {
                    sucesso = true,
                    dados = mapeadorCompromisso.Map<List<ListarCompromissosViewModel>>(compromissoResult.Value)
                });
        }

        [HttpGet("visualizar-completa/{id}")]
        public ActionResult<VisualizarCompromissoViewModel> SelecionarPorId(string id)
        {
            var contatoResult = servico.SelecionarPorIdComContato(Guid.Parse(id));

            if (RegistroNaoEncontrado(contatoResult)) return NotFoundInterno(contatoResult);
            if (contatoResult.IsFailed) return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromisso.Map<VisualizarCompromissoViewModel>(contatoResult.Value)                
            });
        }

        [HttpDelete("excluir/{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var compromissoSelecionado = servico.SelecionarPorId(id);
            if (compromissoSelecionado.IsFailed) return InternalError(compromissoSelecionado);

            var contatoResult = servico.Excluir(compromissoSelecionado.Value);

            if (RegistroNaoEncontrado<Compromisso>(contatoResult)) return NotFoundInterno<Compromisso>(contatoResult);

            if (contatoResult.IsFailed) return InternalError<Compromisso>(contatoResult);

            return Ok(new
            {
                sucesso = true
            });
        }

    }
}
