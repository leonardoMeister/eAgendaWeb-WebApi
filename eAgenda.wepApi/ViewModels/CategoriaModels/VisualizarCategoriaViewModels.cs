using eAgenda.wepApi.ViewModels.DespesaModels;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.ViewModels.CategoriaModels
{
    public class VisualizarCategoriaViewModels
    {
        public VisualizarCategoriaViewModels()
        {
            Despesas = new List<ListarDespesaViewModels>();
        }

        public string Titulo { get; set; }
        public Guid Id { get; set; }

        public List<ListarDespesaViewModels> Despesas { get; set; }


    }
}
