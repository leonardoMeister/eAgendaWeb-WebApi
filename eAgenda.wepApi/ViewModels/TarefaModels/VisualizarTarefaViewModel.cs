using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.ViewModels.TarefaModels
{
    public class VisualizarTarefaViewModel 
    {
        public string Titulo { get; set; }
        public string Prioridade { get; set; }
        public string Situacao { get; set; }
        public DateTime DataCricacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal PercentualConcluido { get; set; }
        public List<VisualizarItenTarefaViewModel> Itens { get; set; }

        public Guid Id { get; set; }
    }
}
