using System;

namespace eAgenda.wepApi.ViewModels.ContatoModels
{
    public class VisualizarContatoViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }

        public Guid Id { get; set; }
    }
}
