using System.ComponentModel.DataAnnotations;

namespace eAgenda.wepApi.ViewModels.ContatoModels
{
    public class FormContatoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Cargo { get; set; }
    }
    public class InserirContatoViewModel : FormContatoViewModel
    {
         
    }
    public class EditarContatoViewModel : FormContatoViewModel
    {

    }
}
