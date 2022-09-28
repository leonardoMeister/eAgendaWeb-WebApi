using eAgenda.Dominio.ModuloDespesa;
using eAgenda.wepApi.ViewModels.CategoriaModels;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.ViewModels.DespesaModels
{
    public class FormDespesaViewModels
    {
        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }
        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

    }

    public class InserirDespesaViewModel : FormDespesaViewModels
    {
        public List<ListarCategoriaViewModel> Categorias { get; set; }

    }

    public class EditarDespesaViewModel : FormDespesaViewModels
    {

    }

}
