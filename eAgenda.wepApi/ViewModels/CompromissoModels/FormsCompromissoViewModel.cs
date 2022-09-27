using eAgenda.Dominio.ModuloCompromisso;
using System;

namespace eAgenda.wepApi.ViewModels.CompromissoModels
{
    public class FormsCompromissoViewModel
    {
        public string Assunto { get; set; }
        public DateTime Data { get; set; }
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }

        public DateTime DataTermino { get; set; }
        public Guid ContatoId { get; set; }        
        public string DescricaoLocalLink { get; set; }
    }

    public class InserirCompromissoViewModel : FormsCompromissoViewModel
    {

    }
    public class EditarCompromissoViewModel : FormsCompromissoViewModel
    {

    }

}
