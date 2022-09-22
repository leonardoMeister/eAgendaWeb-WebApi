using System;

namespace eAgenda.wepApi.ViewModels.TarefaModels
{
    public class ListarTarefaViewModel
    {
        public string Titulo { get; set; }
         
        public string  Prioridade{ get; set; }

        public string Situacao { get; set; }

        public Guid Id { get; set; }
    } 
}
