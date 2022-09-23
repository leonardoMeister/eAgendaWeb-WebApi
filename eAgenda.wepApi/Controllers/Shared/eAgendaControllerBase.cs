using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eAgenda.wepApi.Controllers.Shared
{
    [ApiController]
    public abstract class eAgendaControllerBase : ControllerBase
    {
        protected ActionResult InternalError<T>(Result<T> registroResult)
        {
            return StatusCode(500,
                                new
                                {
                                    sucesso = false,
                                    erros = registroResult.Errors.Select(x => x.Message)
                                });
        }
        protected ActionResult NotFoundInterno<T>(Result<T> registroResult)
        {
            return NotFound(new
            {
                sucesso = false,
                erros = registroResult.Errors.Select(x => x.Message)
            });
        }
        protected bool RegistroNaoEncontrado<T>(Result<T> registroResult)
        {
            return registroResult.Errors.Any(x => x.Message.Contains("não encontrada"));
        }

    }
}
