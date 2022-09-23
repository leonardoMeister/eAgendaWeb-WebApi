using eAgenda.Dominio.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.wepApi.ViewModels.TarefaModels
{ 
    public class FormsTarefaViewModel
    {
        [Required(ErrorMessage ="O campo {0} é obrigatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
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
