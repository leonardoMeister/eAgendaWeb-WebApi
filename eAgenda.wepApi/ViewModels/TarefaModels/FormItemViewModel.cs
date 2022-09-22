using eAgenda.Dominio.ModuloTarefa;
using System;

namespace eAgenda.wepApi.ViewModels.TarefaModels
{
    public class FormItemViewModel
    {
        public string Titulo { get; set; }
        public StatusItemTarefa Status { get; set; }
        public Guid Id { get; set; }
        public bool Concluido { get; set; }
    }
}
