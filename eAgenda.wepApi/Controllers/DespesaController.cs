using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.wepApi.Controllers.Shared;
using eAgenda.wepApi.ViewModels.DespesaModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : eAgendaControllerBase
    {
        private readonly ServicoDespesa servico;
        private IMapper mapeadorDespesa;
        private readonly ServicoCategoria servicoCategoria;

        public DespesaController(ServicoDespesa servico, ServicoCategoria servicoCategoria, IMapper mapeadorContatos)
        {
            this.servico = servico;
            this.servicoCategoria = servicoCategoria;
            this.mapeadorDespesa = mapeadorContatos;
        }

        [HttpPost("inserir")]
        public ActionResult<InserirDespesaViewModel> Inserir(InserirDespesaViewModel despesaVm)
        {
            if (despesaVm.Categorias != null)
                foreach (var item in despesaVm.Categorias)
                {
                    var resultado = servicoCategoria.SelecionarPorId(item.Id);
                    if (resultado.IsFailed) return NotFoundInterno(resultado);
                }

            var despesa = mapeadorDespesa.Map<Despesa>(despesaVm);

            var despesaResult = servico.Inserir(despesa);

            if (despesaResult.IsFailed) return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVm
            });
        }

        [HttpGet("visualizar-todos")]
        public ActionResult<List<ListarDespesaViewModels>> SelecionarTodosCompromissos()
        {
            var despesaResult = servico.SelecionarTodos();

            if (despesaResult.IsFailed) return InternalError(despesaResult);

            return Ok(
                new
                {
                    sucesso = true,
                    dados = mapeadorDespesa.Map<List<ListarDespesaViewModels>>(despesaResult.Value)
                });
        }

        [HttpGet("visualizar-completa/{id}")]
        public ActionResult<VisualizarDespesaViewModels> SelecionarPorId(string id)
        {
            var contatoResult = servico.SelecionarPorId(Guid.Parse(id));

            if (RegistroNaoEncontrado(contatoResult)) return NotFoundInterno(contatoResult);
            if (contatoResult.IsFailed) return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesa.Map<VisualizarDespesaViewModels>(contatoResult.Value)
            });
        }

        [HttpDelete("excluir/{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var despesaSelecionado = servico.SelecionarPorId(id);
            if (despesaSelecionado.IsFailed) return InternalError(despesaSelecionado);

            var despesaResult = servico.Excluir(despesaSelecionado.Value);

            if (RegistroNaoEncontrado<Despesa>(despesaResult)) return NotFoundInterno<Despesa>(despesaResult);

            if (despesaResult.IsFailed) return InternalError<Despesa>(despesaResult);

            return Ok(new
            {
                sucesso = true
            });
        }

    }
}
