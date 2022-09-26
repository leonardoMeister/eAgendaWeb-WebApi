using eAgenda.Dominio.ModuloCompromisso;
using System;

namespace eAgenda.wepApi.ViewModels.CompromissoModels
{
    public class ListarCompromissosViewModel
    {
        public string Assunto { get; set; }
        public DateTime Data { get; set; }        
        public Guid Id { get; set; }

        public string NomeContato { get; set; }
        public string TipoLocal { get; set; }
        public string DescricaoLocal { get; set; }
       
    }
}
