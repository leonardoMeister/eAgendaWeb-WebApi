using eAgenda.Dominio.ModuloDespesa;
using System;

namespace eAgenda.wepApi.ViewModels.DespesaModels
{
    public class ListarDespesaViewModels
    {
        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }
        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public Guid Id { get; set; }

    }
}
