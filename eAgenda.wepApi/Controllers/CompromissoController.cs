using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.wepApi.Controllers.Shared;
using eAgenda.wepApi.ViewModels.CompromissoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompromissoController : eAgendaControllerBase
    {
        private readonly ServicoCompromisso servico;
        private IMapper mapeadorCompromisso;


        public CompromissoController(ServicoCompromisso servicoTarefa, IMapper mapeadorTarefas)
        {
            this.servico = servicoTarefa;
            this.mapeadorCompromisso = mapeadorTarefas;
        }

        [HttpGet]
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

    }
}
