using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly ServicoContato servico;

        public ContatosController()
        {
            var config = new ConfiguracaoAplicacaoeAgenda();

            var dbContexto = new eAgendaDbContext(config.ConnectionStrings);
            var repo = new RepositorioContatoOrm(dbContexto);

            servico = new ServicoContato(repo, dbContexto);
        }

         

        [HttpGet]
        public List<Contato> SelecionarTodosContatos()
        {
            var contatoResult = servico.SelecionarTodos();

            return contatoResult.Value;
        }

        [HttpPost]
        public Contato Inserir(Contato contato)
        {
            var tarefaResult = servico.Inserir(contato);
            return tarefaResult.Value;
        }

        [HttpPut("{id:guid}")]
        public Contato Editar(Guid id, Contato contato)
        {
            var contatoEditado = servico.SelecionarPorId(id).Value;
            contatoEditado.Nome = contato.Nome;
            contatoEditado.Email = contato.Email;
            contatoEditado.Empresa = contato.Empresa;
            contatoEditado.Telefone = contato.Telefone;
            contatoEditado.Cargo = contato.Cargo;

            var tarefaResult = servico.Editar(contatoEditado);

            return tarefaResult.Value;
        }

        [HttpGet("{id}")]
        public Contato SelecionarPorId(string id)
        {
            var tarefaResult = servico.SelecionarPorId(Guid.Parse(id));

            return tarefaResult.Value;
        }

        [HttpDelete("{id:guid}")]
        public void Excluir(Guid id)
        {
            var contato = servico.SelecionarPorId(id).Value;

            var tarefaEditada = servico.Excluir(contato);
        }

    }
}
