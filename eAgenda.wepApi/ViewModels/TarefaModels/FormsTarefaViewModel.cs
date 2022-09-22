using eAgenda.Dominio.ModuloTarefa;
using System;
using System.Collections.Generic;

namespace eAgenda.wepApi.ViewModels.TarefaModels
{
    public class FormsTarefaViewModel
    {
        public string Titulo { get; set; }
        public PrioridadeTarefaEnum Prioridade { get; set; }
        public List<FormItemViewModel> Itens { get; set; }               
    }

    public class InserirTarefaViewModel:FormsTarefaViewModel
    {

    }
    public class EditarTarefaViewModel: FormsTarefaViewModel
    {

    }

}
