using eAgenda.wepApi.ViewModels.ContatoModels;
using System;

namespace eAgenda.wepApi.ViewModels.CompromissoModels
{
    public class VisualizarCompromissoViewModel
    {
        public string Assunto { get; set; }
        public DateTime Data { get; set; }
        public Guid Id { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }

        public VisualizarContatoViewModel Contato { get; set; }

        public string TipoLocal { get; set; }
        public string DescricaoLocal { get; set; }
    }
}
