using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.wepApi.Controllers.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : eAgendaControllerBase
    {
        private readonly ServicoDespesa servico;
        private IMapper mapeadorContatos;

        public DespesaController(ServicoDespesa servico, IMapper mapeadorContatos)
        {
            this.servico = servico;
            this.mapeadorContatos = mapeadorContatos;
        }
    }
}
