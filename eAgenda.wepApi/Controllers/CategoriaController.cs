using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.wepApi.Controllers.Shared;
using eAgenda.wepApi.ViewModels.CategoriaModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : eAgendaControllerBase
    {
        private readonly ServicoCategoria servico;
        private IMapper mapeadorCategoria;

        public CategoriaController(ServicoCategoria servico, IMapper mapeadorContatos)
        {
            this.servico = servico;
            this.mapeadorCategoria = mapeadorContatos;
        }

        [HttpPost("inserir")]
        public ActionResult<InserirCategoriaViewModel> Inserir(InserirCategoriaViewModel categoriaVm)
        {

            var categoria = mapeadorCategoria.Map<Categoria>(categoriaVm);

            var categoriaResult = servico.Inserir(categoria);

            if (categoriaResult.IsFailed) return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVm
            });
        }

        [HttpPut("editar/{id:guid}")]
        public ActionResult<EditarCategoriaViewModel> Editar(Guid id, EditarCategoriaViewModel categoriaVm)
        {
            var categoriaAntigoResult = servico.SelecionarPorId(id);
            if (RegistroNaoEncontrado(categoriaAntigoResult)) return NotFoundInterno(categoriaAntigoResult);

            var contatoEditado = mapeadorCategoria.Map(categoriaVm, categoriaAntigoResult.Value);            

            categoriaAntigoResult = servico.Editar(contatoEditado);

            if (categoriaAntigoResult.IsFailed) return InternalError(categoriaAntigoResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVm
            });
        }


        [HttpGet("visualizar-todos")]
        public ActionResult<List<ListarCategoriaViewModel>> SelecionarTodos()
        {
            var categoriaResult = servico.SelecionarTodos();

            if (categoriaResult.IsFailed) return InternalError(categoriaResult);

            return Ok(
                new
                {
                    sucesso = true,
                    dados = mapeadorCategoria.Map<List<ListarCategoriaViewModel>>(categoriaResult.Value)
                });
        }

        [HttpGet("visualizar-completa/{id}")]
        public ActionResult<VisualizarCategoriaViewModels> SelecionarPorId(string id)
        {
            var categoriaResult = servico.SelecionarPorId(Guid.Parse(id));

            if (RegistroNaoEncontrado(categoriaResult)) return NotFoundInterno(categoriaResult);
            if (categoriaResult.IsFailed) return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategoria.Map<VisualizarCategoriaViewModels>(categoriaResult.Value)
            });
        }

        [HttpDelete("excluir/{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var categoriaSelecionado = servico.SelecionarPorId(id);
            if (categoriaSelecionado.IsFailed) return InternalError(categoriaSelecionado);

            var categoriaResult = servico.Excluir(categoriaSelecionado.Value);

            if (RegistroNaoEncontrado<Categoria>(categoriaResult)) return NotFoundInterno<Categoria>(categoriaResult);

            if (categoriaResult.IsFailed) return InternalError<Categoria>(categoriaResult);

            return Ok(new
            {
                sucesso = true
            });
        }

    }
}
